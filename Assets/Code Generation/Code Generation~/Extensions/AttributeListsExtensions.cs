using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Extensions
{
    public static class AttributeListsExtensions
    {
        public static bool AnyWithNameContaining(this SyntaxList<AttributeListSyntax> attributeLists,
            string name)
        {
            return attributeLists.Any(list =>
                list.Attributes.Any(attr => ((IdentifierNameSyntax) attr.Name).Identifier.Text.Contains(name)));
        }
    }
}