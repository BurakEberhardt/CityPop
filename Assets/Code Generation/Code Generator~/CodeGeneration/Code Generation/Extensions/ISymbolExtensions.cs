using System.Linq;
using Microsoft.CodeAnalysis;

namespace CodeGeneration.Extensions
{
    public static class ISymbolExtensions
    {
        public static bool HasAttribute(this ISymbol symbol, string fullName)
        {
            return symbol.GetAttributes().Any(attr =>
            {
                var attributeClass = attr.AttributeClass;
                if (attributeClass == null)
                    return false;
                
                return fullName == $"{attributeClass.ContainingNamespace}.{attributeClass.Name}";
            });
        }
    }
}