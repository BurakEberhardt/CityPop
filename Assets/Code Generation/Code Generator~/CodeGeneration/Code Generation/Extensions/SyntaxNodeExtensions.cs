using System;
using Microsoft.CodeAnalysis;

namespace CodeGeneration.Extensions
{
    public static class SyntaxNodeExtensions
    {
        public static bool TryGetParent<T>(this SyntaxNode syntaxNode, out T parent)
            where T : SyntaxNode
        {
            parent = null;
            
            var nodeParent = syntaxNode.Parent;
            while (nodeParent != null)
            {
                if (nodeParent is T p)
                {
                    parent = p;
                    return true;
                }

                nodeParent = nodeParent.Parent;
            }

            return false;
        }
        
        public static T GetParentOrDefault<T>(this SyntaxNode syntaxNode)
            where T : SyntaxNode
        {
            return syntaxNode.TryGetParent<T>(out var parent) ? parent : default;
        }
    }
}