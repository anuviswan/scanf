using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                    title: CodeFixResources.CF_1005_Title_ReturnTask,
                    createChangedDocument: c => UseTaskReturnType(context.Document, methodDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1005_Title_ReturnTask)),
                diagnostic);
        }

        private async Task<Document> UseTaskReturnType(Document document, MethodDeclarationSyntax methodDeclaration, CancellationToken cancellationToken)
        {
            var originalReturnType = methodDeclaration.ReturnType;
            var newReturnType = SyntaxFactory.ParseTypeName(nameof(Task)).WithTrailingTrivia(originalReturnType.GetTrailingTrivia());
            var newMethodDeclaration = methodDeclaration.WithReturnType(newReturnType);

            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(methodDeclaration, newMethodDeclaration);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}
