using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneration.Extensions
{
    public static class AccessibilityExtensions
    {
        public static SyntaxKind ToSyntaxKind(this Accessibility accessibility)
        {
            return accessibility switch
            {
                Accessibility.Private => SyntaxKind.PrivateKeyword,
                Accessibility.Protected => SyntaxKind.ProtectedKeyword,
                Accessibility.Public => SyntaxKind.PublicKeyword,
                _ => SyntaxKind.PublicKeyword
            };
        }
    }
}