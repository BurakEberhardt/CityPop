using System.IO;
using System.Linq;
using System.Text;
using CodeGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeGeneration
{
    [Generator]
    public class DataGenerator2 : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classProvider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (syntaxNode, _) =>
                    {
                        if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax)
                            return false;

                        if (!classDeclarationSyntax.AttributeLists.AnyWithNameContaining("Data"))
                            return false;

                        return true;
                    }, (syntaxContext, token) =>
                    {
                        var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(syntaxContext.Node);
                        return (SyntaxContext: syntaxContext, Symbol: symbol);
                    })
                .Where(o => o.Symbol != null)
                .Where(o => o.Symbol.HasAttribute("CityPop.Core.Shared.Attributes.DataAttribute"))
                .Select((o, _) => o.Symbol);

            context.RegisterSourceOutput(classProvider, Generate);
        }

        void Generate(SourceProductionContext context, ISymbol symbol)
        {
            var compilationUnitSyntax = CreateCompilationUnitSyntax(symbol.ContainingNamespace?.Name,  symbol.Name);

            File.WriteAllText(
                $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generator~\CodeGeneration\Code Generation\Debug\{symbol.Name}.txt",
                compilationUnitSyntax.GetText(Encoding.UTF8).ToString());

            context.AddSource($"{symbol.Name}.g.cs", compilationUnitSyntax.GetText(Encoding.UTF8));
        }

        CompilationUnitSyntax CreateCompilationUnitSyntax(string namespaceName, string className)
        {
            var main = SingletonList<MemberDeclarationSyntax>(
                ClassDeclaration("BodyVisualsData")
                    .WithModifiers(
                        TokenList(
                            new[]
                            {
                                Token(SyntaxKind.PublicKeyword),
                                Token(SyntaxKind.PartialKeyword)
                            }))
                    .WithMembers(
                        List<MemberDeclarationSyntax>(
                            new MemberDeclarationSyntax[]
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
                                                    Identifier($"On{className}"))
                                                .WithParameterList(
                                                    ParameterList(
                                                        SingletonSeparatedList<ParameterSyntax>(
                                                            Parameter(
                                                                    Identifier(className.ToPascalCase()))
                                                                .WithType(
                                                                    IdentifierName(className)))))
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
                                                    Identifier($"On{className}Removed"))
                                                .WithSemicolonToken(
                                                    Token(SyntaxKind.SemicolonToken))))
                            })));

            if (!string.IsNullOrEmpty(namespaceName))
            {
                main = SingletonList<MemberDeclarationSyntax>(
                    NamespaceDeclaration(
                            QualifiedName(
                                IdentifierName("CityPop"),
                                IdentifierName("Character")))
                        .WithMembers(main));
            }

            return CompilationUnit()
                .WithMembers(main)
                .NormalizeWhitespace();
        }
    }


    // [Generator]
    // public class DataGenerator : ISourceGenerator
    // {
    //     public void Execute(GeneratorExecutionContext context)
    //     {
    //         var syntaxReceiver = (MainSyntaxReceiver) context.SyntaxReceiver;
    //
    //         var index = 0;
    //         foreach (var capture in syntaxReceiver.Captures)
    //         {
    //             var semanticModel = context.Compilation.GetSemanticModel(capture.Class.SyntaxTree);
    //             
    //             File.WriteAllText(
    //                 $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generator~\CodeGeneration\Code Generation\Debug\semanticModel.txt",
    //                 semanticModel.SyntaxTree.GetText().ToString());
    //             
    //             var symbolInfo = semanticModel.GetSymbolInfo(capture.Class);
    //             
    //             File.WriteAllText(
    //                 $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generator~\CodeGeneration\Code Generation\Debug\symbolInfo.txt",
    //                 (symbolInfo.Symbol != null).ToString());
    //             
    //             if(symbolInfo.Symbol == null)
    //                 continue;
    //
    //             var namespaceName = symbolInfo.Symbol.ContainingNamespace?.Name;
    //
    //             File.WriteAllText(
    //                 $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generator~\CodeGeneration\Code Generation\Debug\namespace.txt",
    //                 namespaceName);
    //             
    //             var compilationUnitSyntax = CreateCompilationUnitSyntax(symbolInfo.Symbol.ContainingNamespace?.Name,
    //                 capture.Class.Identifier.Text);
    //             var fileName = (++index).ToString();
    //             File.WriteAllText(
    //                 $@"C:\Users\burak\Documents\Projects\CityPop\Assets\Code Generation\Code Generator~\CodeGeneration\Code Generation\Debug\{fileName}.txt",
    //                 compilationUnitSyntax.GetText(Encoding.UTF8).ToString());
    //
    //             context.AddSource($"{capture.Class.Identifier.Text}.g.cs", compilationUnitSyntax.GetText(Encoding.UTF8));
    //         }
    //     }
    //
    //     CompilationUnitSyntax CreateCompilationUnitSyntax(string namespaceName, string className)
    //     {
    //         var main = SingletonList<MemberDeclarationSyntax>(
    //             ClassDeclaration("BodyVisualsData")
    //                 .WithModifiers(
    //                     TokenList(
    //                         new[]
    //                         {
    //                             Token(SyntaxKind.PublicKeyword),
    //                             Token(SyntaxKind.PartialKeyword)
    //                         }))
    //                 .WithMembers(
    //                     List<MemberDeclarationSyntax>(
    //                         new MemberDeclarationSyntax[]
    //                         {
    //                             InterfaceDeclaration("IAddedListener")
    //                                 .WithModifiers(
    //                                     TokenList(
    //                                         Token(SyntaxKind.PublicKeyword)))
    //                                 .WithMembers(
    //                                     SingletonList<MemberDeclarationSyntax>(
    //                                         MethodDeclaration(
    //                                                 PredefinedType(
    //                                                     Token(SyntaxKind.VoidKeyword)),
    //                                                 Identifier($"On{className}"))
    //                                             .WithParameterList(
    //                                                 ParameterList(
    //                                                     SingletonSeparatedList<ParameterSyntax>(
    //                                                         Parameter(
    //                                                                 Identifier(className.ToPascalCase()))
    //                                                             .WithType(
    //                                                                 IdentifierName(className)))))
    //                                             .WithSemicolonToken(
    //                                                 Token(SyntaxKind.SemicolonToken)))),
    //                             InterfaceDeclaration("IRemovedListener")
    //                                 .WithModifiers(
    //                                     TokenList(
    //                                         Token(SyntaxKind.PublicKeyword)))
    //                                 .WithMembers(
    //                                     SingletonList<MemberDeclarationSyntax>(
    //                                         MethodDeclaration(
    //                                                 PredefinedType(
    //                                                     Token(SyntaxKind.VoidKeyword)),
    //                                                 Identifier($"On{className}Removed"))
    //                                             .WithSemicolonToken(
    //                                                 Token(SyntaxKind.SemicolonToken))))
    //                         })));
    //         
    //         if (!string.IsNullOrEmpty(namespaceName))
    //         {
    //             main = SingletonList<MemberDeclarationSyntax>(
    //                 NamespaceDeclaration(
    //                         QualifiedName(
    //                             IdentifierName("CityPop"),
    //                             IdentifierName("Character")))
    //                     .WithMembers(main));
    //         }
    //         
    //         return CompilationUnit()
    //             .WithMembers(main)
    //             .NormalizeWhitespace();
    //     }
    //
    //     public void Initialize(GeneratorInitializationContext context)
    //     {
    //         context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver());
    //     }
    //
    //     public class MainSyntaxReceiver : ISyntaxReceiver
    //     {
    //         public List<Capture> Captures { get; } = new();
    //
    //         public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    //         {
    //             if (syntaxNode is not AttributeSyntax {Name: IdentifierNameSyntax {Identifier.Text: "Data"}} attribute)
    //                 return;
    //
    //             if (!syntaxNode.TryGetParent<ClassDeclarationSyntax>(out var classDeclarationSyntax))
    //                 return;
    //
    //             Captures.Add(new Capture(attribute, classDeclarationSyntax));
    //         }
    //
    //         public record Capture(AttributeSyntax attribute, ClassDeclarationSyntax Class);
    //     }
    // }
}