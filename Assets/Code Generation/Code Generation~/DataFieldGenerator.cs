using System.Collections.Generic;
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
    public class DataFieldGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var symbolProvider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (syntaxNode, _) => syntaxNode is FieldDeclarationSyntax fieldDeclarationSyntax &&
                                       fieldDeclarationSyntax.AttributeLists.AnyWithNameContaining(NameConstants
                                           .DataAttributeName), (syntaxContext, token) =>
                    {
                        var node = (FieldDeclarationSyntax) syntaxContext.Node;
                        var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(node.Declaration.Variables.First());
                        return (IFieldSymbol) symbol;
                    })
                .Where(symbol => symbol != null)
                .Where(symbol => symbol.Name.StartsWith("_") && !symbol.IsConst &&
                                 symbol.DeclaredAccessibility == Accessibility.Private)
                .Where(symbol => symbol.HasAttribute(NameConstants.DataAttributeFullName))
                .Select((symbol, _) => (Symbol: symbol, symbol.ContainingType))
                .Where(o => o.ContainingType != null);

            context.RegisterSourceOutput(symbolProvider, Generate);
        }

        void Generate(SourceProductionContext context, (IFieldSymbol Symbol, INamedTypeSymbol ContainingType) data)
        {
            Logger.Log("7");

            var compilationUnitSyntax = data.ContainingType.CreateCompilationUnitForClass(GetMemberList(data.Symbol));

            Logger.Log(compilationUnitSyntax.GetText(Encoding.UTF8).ToString());

            context.AddSource($"{data.ContainingType.Name}{data.Symbol.Name.RemoveFirst().FirstToUpper()}.g.cs",
                compilationUnitSyntax.GetText(Encoding.UTF8));
        }

        static IEnumerable<MemberDeclarationSyntax> GetMemberList(IFieldSymbol symbol)
        {
            var typeName = symbol.Type.GetFullName();
            var symbolName = symbol.Name;
            var symbolNameLowercase = symbol.Name.RemoveFirst().FirstToLower();
            var symbolNameUppercase = symbolNameLowercase.FirstToUpper();

            return new MemberDeclarationSyntax[]
            {
                PropertyDeclaration(
                        IdentifierName(typeName),
                        Identifier(symbolNameUppercase))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(
                        AccessorList(
                            List<AccessorDeclarationSyntax>(
                                new AccessorDeclarationSyntax[]
                                {
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithExpressionBody(
                                            ArrowExpressionClause(
                                                IdentifierName(symbolName)))
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithBody(
                                            Block(
                                                ExpressionStatement(
                                                    AssignmentExpression(
                                                        SyntaxKind.SimpleAssignmentExpression,
                                                        IdentifierName(symbolName),
                                                        IdentifierName("value"))),
                                                ForEachStatement(
                                                    IdentifierName(
                                                        Identifier(
                                                            TriviaList(),
                                                            SyntaxKind.VarKeyword,
                                                            "var",
                                                            "var",
                                                            TriviaList())),
                                                    Identifier("listener"),
                                                    IdentifierName($"_{symbolNameLowercase}Listeners"),
                                                    ExpressionStatement(
                                                        InvocationExpression(
                                                                MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    IdentifierName("listener"),
                                                                    IdentifierName($"On{symbolNameUppercase}")))
                                                            .WithArgumentList(
                                                                ArgumentList(
                                                                    SingletonSeparatedList<ArgumentSyntax>(
                                                                        Argument(
                                                                            IdentifierName("value")))))))))
                                }))),
                InterfaceDeclaration($"I{symbolNameUppercase}Listener")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            MethodDeclaration(
                                    PredefinedType(
                                        Token(SyntaxKind.VoidKeyword)),
                                    Identifier($"On{symbolNameUppercase}"))
                                .WithParameterList(
                                    ParameterList(
                                        SingletonSeparatedList<ParameterSyntax>(
                                            Parameter(
                                                    Identifier(
                                                        TriviaList(),
                                                        SyntaxKind.TypeKeyword,
                                                        symbolNameLowercase,
                                                        symbolNameLowercase,
                                                        TriviaList()))
                                                .WithType(
                                                    IdentifierName(typeName)))))
                                .WithSemicolonToken(
                                    Token(SyntaxKind.SemicolonToken)))),
                FieldDeclaration(
                    VariableDeclaration(
                            QualifiedName(
                                QualifiedName(
                                    QualifiedName(
                                        IdentifierName("System"),
                                        IdentifierName("Collections")),
                                    IdentifierName("Generic")),
                                GenericName(
                                        Identifier("HashSet"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(
                                                IdentifierName($"I{symbolNameUppercase}Listener"))))))
                        .WithVariables(
                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                VariableDeclarator(
                                        Identifier($"_{symbolNameLowercase}Listeners"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ImplicitObjectCreationExpression()))))),
                MethodDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.VoidKeyword)),
                        Identifier($"Add{symbolNameUppercase}Listener"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList<ParameterSyntax>(
                                Parameter(
                                        Identifier("listener"))
                                    .WithType(
                                        IdentifierName($"I{symbolNameUppercase}Listener")))))
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName($"_{symbolNameLowercase}Listeners"),
                                        IdentifierName("Add")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SingletonSeparatedList<ArgumentSyntax>(
                                            Argument(
                                                IdentifierName("listener")))))))
                    .WithSemicolonToken(
                        Token(SyntaxKind.SemicolonToken)),
                MethodDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.VoidKeyword)),
                        Identifier($"Remove{symbolNameUppercase}Listener"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList<ParameterSyntax>(
                                Parameter(
                                        Identifier("listener"))
                                    .WithType(
                                        IdentifierName($"I{symbolNameUppercase}Listener")))))
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName($"_{symbolNameLowercase}Listeners"),
                                        IdentifierName("Remove")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SingletonSeparatedList<ArgumentSyntax>(
                                            Argument(
                                                IdentifierName("listener")))))))
                    .WithSemicolonToken(
                        Token(SyntaxKind.SemicolonToken))
            };
        }
    }
}