using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scanf.CodeSmell
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseAnyWhenPossibleInsteadOfCountCodeFixProvider)), Shared]
    public class UseAnyWhenPossibleInsteadOfCountCodeFixProvider : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(UseAnyWhenPossibleInsteadOfCountAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var binaryExpression = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<BinaryExpressionSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CF_1006_Title_UseAny,
                    createChangedDocument: c => UseAnyWhenPossible(context.Document, binaryExpression, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1006_Title_UseAny)),
                diagnostic);
        }

        private async Task<Document> UseAnyWhenPossible(Document document, BinaryExpressionSyntax expression, CancellationToken cancellationToken)
        {
            var originalExpression = expression;
            var invocation = TraverseConditions(originalExpression);
            var countInvocation = (invocation.Expression as MemberAccessExpressionSyntax);
            var collectionInstanceName = (countInvocation.Expression as IdentifierNameSyntax).Identifier.Text;

            var expressionWithAny = SyntaxFactory.ParseExpression($"{(expression.Kind() == SyntaxKind.EqualsExpression?"!":string.Empty)}{collectionInstanceName}.Any()")
                .WithLeadingTrivia(originalExpression.GetLeadingTrivia())
                .WithTrailingTrivia(originalExpression.GetTrailingTrivia());
            
            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(originalExpression, expressionWithAny);
            return document.WithSyntaxRoot(newRoot);
        }

        private InvocationExpressionSyntax TraverseConditions(BinaryExpressionSyntax binaryExpression)
        {
            if (binaryExpression.Left is InvocationExpressionSyntax invocationLeft)
            {
                return invocationLeft;
            }

            if (binaryExpression.Right is InvocationExpressionSyntax invocationRight)
            {
                return invocationRight;
            }
            return null;
        }
    }

    
}
