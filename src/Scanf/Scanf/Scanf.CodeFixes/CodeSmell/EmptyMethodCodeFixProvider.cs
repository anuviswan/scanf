using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scanf.CodeSmell
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EmptyMethodCodeFixProvider)), Shared]
    public class EmptyMethodCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(EmptyMethodAnalyzer.DiagnosticId); }
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
                    title: CodeFixResources.CF_1002_Title_RaiseException,
                    createChangedDocument: c => RaiseException(context.Document, methodDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1002_Title_RaiseException)),
                diagnostic);
        }

        private async Task<Document> RaiseException(Document document, MethodDeclarationSyntax methodDeclaration, CancellationToken cancellationToken)
        {
            var identifier = SyntaxFactory.IdentifierName(nameof(NotImplementedException));
            var argumentList = SyntaxFactory.ArgumentList(); 
            var objectExpression = SyntaxFactory.ObjectCreationExpression(identifier).WithArgumentList(argumentList);
            var exceptionStatement = SyntaxFactory.ThrowStatement(objectExpression);

            var oldMethodBody = methodDeclaration.Body;
            var oldMethodLeadingTrivia = oldMethodBody.GetLeadingTrivia();
            var newMthodWithLeadingTrivia = exceptionStatement.WithLeadingTrivia(oldMethodLeadingTrivia);
            
            var newMethodBody = methodDeclaration.Body.AddStatements(newMthodWithLeadingTrivia);

            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(oldMethodBody, newMethodBody);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}
