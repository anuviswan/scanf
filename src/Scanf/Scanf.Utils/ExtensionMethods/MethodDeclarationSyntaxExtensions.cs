using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Scanf.Utils.ExtensionMethods
{
    public static class MethodDeclarationSyntaxExtensions
    {
        public static bool IsAsync(this MethodDeclarationSyntax source)
        {
            return source.Modifiers.Any(SyntaxKind.AsyncKeyword); ;
        }


        public static bool IsEvantHandler(this MethodDeclarationSyntax source, SyntaxNodeAnalysisContext context)
        {
            // This method needs to be improvied further. It does a lame evaluation of whether the method is a possible event handler.
            foreach (var parameter in source.ParameterList.Parameters)
            {
                if(parameter.Type is IdentifierNameSyntax d && d.Identifier.Text.Equals(nameof(EventArgs)))
                {
                    return true;
                    
                }
            }
            return false;
        }
    }
}
