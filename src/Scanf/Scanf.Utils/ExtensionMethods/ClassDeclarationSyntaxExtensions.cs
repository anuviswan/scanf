using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scanf.Utils.ExtensionMethods
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static SyntaxKind GetAccessModifier(this ClassDeclarationSyntax classDeclaration)
        {
            var modifiers = new List<SyntaxKind> { SyntaxKind.PublicKeyword, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword };
            foreach (var modifer in classDeclaration.Modifiers)
            {
                if (modifiers.Contains(modifer.Kind()))
                {
                    return modifer.Kind();
                }
            }
            return SyntaxKind.PrivateKeyword;
        }
    }
}
