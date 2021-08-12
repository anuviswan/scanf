﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scanf.CodeSmell
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseAnyWhenPossibleInsteadOfCountCodeFixProvider)), Shared]
    public class UseAnyWhenPossibleInsteadOfCountCodeFixProvider : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(AsyncVoidAnalyzer.DiagnosticId); 

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var methodDeclaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<IfStatementSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CF_1006_Title_UseAny,
                    createChangedDocument: c => UseTaskReturnType(context.Document, methodDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.CF_1006_Title_UseAny)),
                diagnostic);
        }

        private async Task<Document> UseTaskReturnType(Document document, IfStatementSyntax ifCondition, CancellationToken cancellationToken)
        {
            var originalReturnType = ifCondition.Condition;
            //var newReturnType = SyntaxFactory.ParseTypeName(nameof(Task)).WithTrailingTrivia(originalReturnType.GetTrailingTrivia());
            //var newMethodDeclaration = ifCondition.WithReturnType(newReturnType);

            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
           // var newRoot = oldRoot.ReplaceNode(ifCondition, newMethodDeclaration);
            return document.WithSyntaxRoot(oldRoot);
        }
    }
}
