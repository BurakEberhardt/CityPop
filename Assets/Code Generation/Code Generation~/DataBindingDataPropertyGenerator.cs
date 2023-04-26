﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration.Constants;
using CodeGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeGeneration
{
    [Generator]
    public class DataBindingDataPropertyGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var symbolProvider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
                                       classDeclarationSyntax.AttributeLists.AnyWithNameContaining(NameConstants
                                           .DataBindingAttributeName), (syntaxContext, token) =>
                    {
                        var node = (ClassDeclarationSyntax) syntaxContext.Node;
                        var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(node);
                        return symbol;
                    })
                .Where(symbol => symbol != null)
                .Select((symbol, _) =>
                {
                    var attributes = symbol.GetAttributes(NameConstants.DataBindingAttributeFullName);

                    Dictionary<INamedTypeSymbol, (Accessibility Accessibility, List<INamedTypeSymbol> Interfaces)>
                        dataInfos = new();

                    foreach (var attributeData in attributes)
                    {
                        INamedTypeSymbol type = null;
                        Accessibility? accessibility = null;

                        foreach (var constructorArgument in attributeData.ConstructorArguments)
                        {
                            switch (constructorArgument.Type.GetFullName())
                            {
                                case NameConstants.DataBindingAttributeConstructorTypeType:
                                    type = (INamedTypeSymbol) constructorArgument.Value;
                                    break;
                                case NameConstants.DataBindingAttributeConstructorAccessibilityType:
                                    if (constructorArgument.Value != null)
                                        accessibility = (Accessibility) (int) constructorArgument.Value;
                                    break;
                            }
                        }

                        if (type == null || accessibility == null)
                            continue;

                        dataInfos[type] = (accessibility.Value, new List<INamedTypeSymbol>());
                    }

                    foreach (var i in symbol.AllInterfaces)
                    {
                        if (dataInfos.TryGetValue(i.ContainingType, out var dataInfo))
                            dataInfo.Interfaces.Add(i);
                    }

                    return (Symbol: symbol, DataInfos: dataInfos);
                });

            context.RegisterSourceOutput(symbolProvider, Generate);
        }

        void Generate(SourceProductionContext context,
            (INamedTypeSymbol Symbol,
                Dictionary<INamedTypeSymbol, (Accessibility Accessibility, List<INamedTypeSymbol> Interfaces)> DataInfos
                ) data)
        {
            foreach (var kvp in data.DataInfos)
            {
                var compilationUnitSyntax =
                    data.Symbol.CreateCompilationUnitForClass(GetMemberList(kvp.Key, kvp.Value));

                // Logger.Log(compilationUnitSyntax.GetText(Encoding.UTF8).ToString());

                context.AddSource($"{data.Symbol.Name}{kvp.Key.Name}.g.cs",
                    compilationUnitSyntax.GetText(Encoding.UTF8));
            }
        }

        IEnumerable<MemberDeclarationSyntax> GetMemberList(INamedTypeSymbol dataBindingSymbol,
            (Accessibility Accessibility, List<INamedTypeSymbol> Interfaces) dataBindingData)
        {
            var fieldName = GetFieldNameFromDataType(dataBindingSymbol);

            return new MemberDeclarationSyntax[]
            {
                FieldDeclaration(
                        VariableDeclaration(
                                IdentifierName(dataBindingSymbol.GetFullName()))
                            .WithVariables(
                                SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    VariableDeclarator(
                                        Identifier(fieldName)))))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PrivateKeyword))),
                PropertyDeclaration(
                        IdentifierName(dataBindingSymbol.GetFullName()),
                        Identifier(dataBindingSymbol.Name))
                    .WithModifiers(
                        TokenList(
                            Token(dataBindingData.Accessibility.ToSyntaxKind())))
                    .WithAccessorList(
                        AccessorList(
                            List<AccessorDeclarationSyntax>(
                                new AccessorDeclarationSyntax[]
                                {
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithExpressionBody(
                                            ArrowExpressionClause(
                                                IdentifierName(fieldName)))
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithBody(
                                            Block(GetPropertyStatements(fieldName, dataBindingSymbol,
                                                dataBindingData.Interfaces)))
                                })))
            };
        }

        IEnumerable<StatementSyntax> GetPropertyStatements(string fieldName, INamedTypeSymbol dataBindingSymbol,
            List<INamedTypeSymbol> interfaces)
        {
            var statements = new List<StatementSyntax>(3);
            var bindings = new List<INamedTypeSymbol>();

            INamedTypeSymbol dataAddedInterface = null;
            INamedTypeSymbol dataRemovedInterface = null;
            foreach (var @interface in interfaces)
            {
                if (dataAddedInterface == null && @interface.Name == NameConstants.DataAddedListenerName)
                    dataAddedInterface = @interface;
                else if (dataRemovedInterface == null && @interface.Name == NameConstants.DataRemovedListenerName)
                    dataRemovedInterface = @interface;
                else if (dataAddedInterface != null && dataRemovedInterface != null ||
                         @interface.Name != NameConstants.DataAddedListenerName &&
                         @interface.Name != NameConstants.DataRemovedListenerName)
                    bindings.Add(@interface);
            }


            if (dataRemovedInterface != null || bindings.Count > 0)
            {
                statements.Add(CreateRemoveListenersBlock(fieldName, dataRemovedInterface, bindings));
            }

            statements.Add(CreateAssignmentBlock(fieldName));

            if (dataAddedInterface != null || bindings.Count > 0)
            {
                statements.Add(CreateAddListenersBlock(fieldName, dataAddedInterface, bindings));
            }

            return statements;
        }

        StatementSyntax CreateRemoveListenersBlock(string fieldName, INamedTypeSymbol dataRemovedInterface,
            List<INamedTypeSymbol> bindings)
        {
            var statements = new List<ExpressionStatementSyntax>(bindings.Count);

            foreach (var binding in bindings)
                statements.Add(CreateBindingRemoved(binding, fieldName));

            if (dataRemovedInterface != null)
                statements.Add(CreateDataRemovedListener(dataRemovedInterface));

            return IfStatement(
                BinaryExpression(
                    SyntaxKind.NotEqualsExpression,
                    IdentifierName(fieldName),
                    LiteralExpression(
                        SyntaxKind.NullLiteralExpression)),
                Block(statements));
        }

        StatementSyntax CreateAssignmentBlock(string fieldName)
        {
            return ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName(fieldName),
                    IdentifierName("value")));
        }

        StatementSyntax CreateAddListenersBlock(string fieldName, INamedTypeSymbol dataAddedInterface,
            List<INamedTypeSymbol> bindings)
        {
            var statements = new List<ExpressionStatementSyntax>(bindings.Count);

            foreach (var binding in bindings)
                statements.Add(CreateBindingAdded(binding, fieldName));

            if (dataAddedInterface != null)
                statements.Add(CreateDataAddedListener(dataAddedInterface, fieldName));

            return IfStatement(
                BinaryExpression(
                    SyntaxKind.NotEqualsExpression,
                    IdentifierName(fieldName),
                    LiteralExpression(
                        SyntaxKind.NullLiteralExpression)),
                Block(statements));
        }

        static string GetFieldNameFromDataType(ISymbol dataType)
        {
            return $"_{dataType.Name.FirstToLower()}";
        }

        ExpressionStatementSyntax CreateDataRemovedListener(INamedTypeSymbol @interface)
        {
            return ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        ParenthesizedExpression(
                            BinaryExpression(
                                SyntaxKind.AsExpression,
                                ThisExpression(),
                                IdentifierName(@interface.GetFullName()))),
                        IdentifierName(NameConstants.DataRemovedListenerMethodName))));
        }

        ExpressionStatementSyntax CreateDataAddedListener(INamedTypeSymbol @interface, string fieldName)
        {
            return ExpressionStatement(
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            ParenthesizedExpression(
                                BinaryExpression(
                                    SyntaxKind.AsExpression,
                                    ThisExpression(),
                                    IdentifierName(@interface.GetFullName()))),
                            IdentifierName(NameConstants.DataAddedListenerMethodName)))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    IdentifierName(fieldName))))));
        }


        ExpressionStatementSyntax CreateBindingRemoved(ISymbol @interface, string fieldName)
        {
            return ExpressionStatement(
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(fieldName),
                            IdentifierName(string.Format(NameConstants.DataRemoveBindingListenerName,
                                NameConstants.GetNameFromDataBindingListener(@interface.Name)))))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    ThisExpression())))));
        }

        ExpressionStatementSyntax CreateBindingAdded(ISymbol @interface, string fieldName)
        {
            return ExpressionStatement(
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(fieldName),
                            IdentifierName(string.Format(NameConstants.DataAddBindingListenerName,
                                NameConstants.GetNameFromDataBindingListener(@interface.Name)))))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList<ArgumentSyntax>(
                                Argument(
                                    ThisExpression())))));
        }
    }
}