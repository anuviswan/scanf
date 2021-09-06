using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanf.Utils.ExtensionMethods
{
    public static class MemberDeclarationSyntaxExtensions
    {
        public static bool HasAttribute<TAttribute>(this MemberDeclarationSyntax source) where TAttribute : Attribute
        {
            var attributeName = typeof(TAttribute).Name.Replace("Attribute", string.Empty);
            var attributes = source.AttributeLists;
            return attributes.Any(x => x.Attributes.Any(c => c.Name.GetText().ToString() == attributeName));
        }
    }
}
