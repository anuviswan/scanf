using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Rename;

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

        public static async Task<Solution> RenameMethod(this MethodDeclarationSyntax methodDeclaration,Document document, string newName, CancellationToken cancellationToken)
        {
            var solution = document.Project.Solution;

            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            if (semanticModel == null) return default;

            var symbol = semanticModel.GetDeclaredSymbol(methodDeclaration, cancellationToken);

            var newSolution = await Renamer.RenameSymbolAsync(solution, symbol, newName, solution.Workspace.Options);
            return newSolution;
        }
    }
}
