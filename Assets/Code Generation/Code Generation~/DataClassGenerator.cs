﻿using System.Collections.Generic;
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
    public class DataClassGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var symbolProvider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
                                       classDeclarationSyntax.AttributeLists.AnyWithNameContaining(NameConstants
                                           .DataAttributeName), (syntaxContext, token) =>
                    {
                        var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.Node);
                        return (INamedTypeSymbol)symbol;
                    })
                .Where(symbol => symbol != null)
                .Where(symbol => symbol.HasAttribute(NameConstants.DataAttributeFullName));

            context.RegisterSourceOutput(symbolProvider, Generate);
        }

        void Generate(SourceProductionContext context, INamedTypeSymbol symbol)
        {
            var compilationUnitSyntax = symbol.CreateCompilationUnitForClass(GetMemberList(symbol));

            // Logger.Log(compilationUnitSyntax.GetText(Encoding.UTF8).ToString());

            context.AddSource($"{symbol.Name}.g.cs", compilationUnitSyntax.GetText(Encoding.UTF8));
        }

        static IEnumerable<MemberDeclarationSyntax> GetMemberList(ISymbol symbol)
        {
            return new MemberDeclarationSyntax[]
            {
                InterfaceDeclaration("IAddedListener")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            MethodDeclaration(
                                    PredefinedType(
                                        Token(SyntaxKind.VoidKeyword)),
                                    Identifier($"OnAdded"))
                                .WithParameterList(
                                    ParameterList(
                                        SingletonSeparatedList<ParameterSyntax>(
                                            Parameter(
                                                    Identifier(symbol.Name.FirstToLower()))
                                                .WithType(
                                                    IdentifierName(symbol.Name)))))
                                .WithSemicolonToken(
                                    Token(SyntaxKind.SemicolonToken)))),
                InterfaceDeclaration("IRemovedListener")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            MethodDeclaration(
                                    PredefinedType(
                                        Token(SyntaxKind.VoidKeyword)),
                                    Identifier($"OnRemoved"))
                                .WithSemicolonToken(
                                    Token(SyntaxKind.SemicolonToken))))
            };
        }
    }
}