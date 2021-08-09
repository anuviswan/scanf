using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Scanf.CodeSmell
{
    [DiagnosticAnalyzer(Microsoft.CodeAnalysis.LanguageNames.CSharp)]
    public class ConstructorInvokingVirtualMethodAnalyzer:DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.UnattendedTodoRule;
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1009_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1009_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1009_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        internal const string Category = DiagnosticsCategory.CodeSmell;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeConstructor,SyntaxKind.ConstructorDeclaration);
        }

        private void AnalyzeConstructor(SyntaxNodeAnalysisContext context)
        {
            var constructor = (ConstructorDeclarationSyntax)context.Node;

        }

        private void AnalyzeComment(SyntaxTreeAnalysisContext context)
        {

            SyntaxNode root = context.Tree.GetCompilationUnitRoot(context.CancellationToken);
            var commentTrivias = root.DescendantTrivia()
                                   .Where(x => x.IsKind(SyntaxKind.SingleLineCommentTrivia) || x.IsKind(SyntaxKind.MultiLineCommentTrivia));

            foreach (var comment in commentTrivias)
            {
                var commentTriviaAsString = comment.ToString();

                switch (comment.Kind())
                {
                    case SyntaxKind.SingleLineCommentTrivia:
                        {
                            var commentText = commentTriviaAsString.TrimStart('/');
                            _(comment.GetLocation(), commentText);
                            break;
                        }
                    case SyntaxKind.MultiLineCommentTrivia:
                        {
                            var commentLines = commentTriviaAsString.Substring(2, commentTriviaAsString.Length - 2)
                                                                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                                                    .Select(x => x.TrimStart().TrimStart('*')).ToArray();
                            _(comment.GetLocation(), commentLines);
                            break;
                        }
                }
            }

            void _(Location location, params string[] cmntText)
            {
                if (cmntText.Any(x => x.TrimStart().StartsWith("Todo", StringComparison.OrdinalIgnoreCase)))
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, location, cmntText));
                }
            }
        }
    }
}
