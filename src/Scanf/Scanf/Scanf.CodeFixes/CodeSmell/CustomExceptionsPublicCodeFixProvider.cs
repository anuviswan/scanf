using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scanf.Utils.ExtensionMethods;
using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scanf.CodeSmell
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CustomExceptionsPublicCodeFixProvider)), Shared]
    public class CustomExceptionsPublicCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(CustomExceptionsPublicAnalyzer.DiagnosticId); }
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

            var classDeclaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CF_1010_Title_MakeExceptionPublic,
                    createChangedDocument: c => ChangeModifierToPublic(context.Document, classDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1010_Title_MakeExceptionPublic)),
                diagnostic);
        }

        private async Task<Document> ChangeModifierToPublic(Document document, ClassDeclarationSyntax classDeclarationSyntax, CancellationToken cancellationToken)
        {
            var originalModifier = classDeclarationSyntax.GetAccessModifier();
            var indexOfOriginalModifier = classDeclarationSyntax.Modifiers.IndexOf(originalModifier);
            var modifierList = classDeclarationSyntax.Modifiers.RemoveAt(indexOfOriginalModifier);
            var newModifier = SyntaxFactory.Token(SyntaxKind.PublicKeyword);
            modifierList = modifierList.Add(newModifier);
            var newClassDeclaration = classDeclarationSyntax.WithModifiers(modifierList);


            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(classDeclarationSyntax, newClassDeclaration);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}
