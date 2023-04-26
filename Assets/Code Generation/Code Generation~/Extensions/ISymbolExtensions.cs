using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeGeneration.Extensions
{
    public static class SymbolExtensions
    {
        public static string GetFullName(this ISymbol symbol)
        {
            return symbol.ToString();
        }
        
        public static bool HasAttribute(this ISymbol symbol, string fullName)
        {
            return symbol.GetAttributes().Any(attr =>
            {
                var attributeClass = attr.AttributeClass;
                if (attributeClass == null)
                    return false;
                
                return fullName == attributeClass.GetFullName();
            });
        }
        
        public static AttributeData[] GetAttributes(this ISymbol symbol, string fullName)
        {
            return symbol.GetAttributes().Where(attr =>
            {
                var attributeClass = attr.AttributeClass;
                if (attributeClass == null)
                    return false;
                
                return fullName == attributeClass.GetFullName();
            }).ToArray();
        }
        
        public static CompilationUnitSyntax CreateCompilationUnitForClass(this ISymbol symbol, IEnumerable<MemberDeclarationSyntax> members)
        {
            var namespaceName = symbol.ContainingNamespace?.ToString();
            
             var main = SingletonList<MemberDeclarationSyntax>(
                ClassDeclaration(symbol.Name)
                    .WithModifiers(
                        TokenList(
                            new[]
                            {
                                Token(SyntaxKind.PublicKeyword),
                                Token(SyntaxKind.PartialKeyword)
                            }))
                    .WithMembers(List(members)));

            if (!string.IsNullOrEmpty(namespaceName))
            {
                main = SingletonList<MemberDeclarationSyntax>(
                    NamespaceDeclaration(IdentifierName(namespaceName))
                        .WithMembers(main));
            }

            return CompilationUnit()
                .WithMembers(main)
                .NormalizeWhitespace();
        }
    }
}