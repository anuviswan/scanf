using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scanf.Utils.ExtensionMethods;

namespace Scanf.NamingConvention
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AsyncMethodCodeFixProvider)), Shared]
    public class AsyncMethodCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(AsyncMethodAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var methodDeclaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().First();


            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CF_1008_Title_RenameMethod,
                    createChangedSolution: c => RenameMethod(context.Document, methodDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1008_Title_RenameMethod)),
                diagnostic);
        }

        private async Task<Solution> RenameMethod(Document document, MethodDeclarationSyntax methodDeclaration, CancellationToken cancellationToken)
        {
            var existingName = methodDeclaration.Identifier.Text;
            var newName = $"{existingName}Async";

            if(existingName.EndsWith("async"))
            {
                var lastIndex = existingName.LastIndexOf("async");
                newName = $"{existingName.Remove(lastIndex, 5)}Async"; 
            }
            var newSolution = await methodDeclaration.RenameMethod(document, newName, cancellationToken);
            return newSolution;
        }
    }
}
