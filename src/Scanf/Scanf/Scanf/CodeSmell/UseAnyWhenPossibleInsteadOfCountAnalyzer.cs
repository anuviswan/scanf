using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using Scanf.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Scanf.CodeSmell
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseAnyWhenPossibleInsteadOfCountAnalyzer: DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.UseAnyWhenPossibleInsteadOfCountRule;

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1006_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1006_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1006_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private static string Category = DiagnosticsCategory.CodeSmell;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeBinaryExpression, SyntaxKind.GreaterThanExpression);
            context.RegisterSyntaxNodeAction(AnalyzeBinaryExpression, SyntaxKind.EqualsExpression);
        }

        private void AnalyzeBinaryExpression(SyntaxNodeAnalysisContext context)
        {
            var model = context.SemanticModel;
            if(context.Node is BinaryExpressionSyntax binaryExpression && IsConditionConstantZero(binaryExpression))
            {
                var invocationExpressions = TraverseConditions(binaryExpression);

                foreach(var expression in invocationExpressions)
                {
                    var symbol = model.GetSymbolInfo(expression).Symbol;
                    if((symbol.ContainingType.Name == nameof(Enumerable)) && symbol.Name == nameof(Enumerable.Count))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Rule, expression.GetLocation(), expression.Expression.ToString()));
                    }
                }
                
            }
        }

        private bool IsConditionConstantZero(BinaryExpressionSyntax binaryExpression)
        {
            if (binaryExpression.Left  is LiteralExpressionSyntax literalExprLeft 
                && literalExprLeft.Kind() == SyntaxKind.NumericLiteralExpression)
            {
                var token = literalExprLeft.DescendantTokens().FirstOrDefault();
                return token.Text == "0";
            }

            if (binaryExpression.Right is LiteralExpressionSyntax literalExprRight
                && literalExprRight.Kind() == SyntaxKind.NumericLiteralExpression)
            {
                var token = literalExprRight.DescendantTokens().FirstOrDefault();
                return token.Text == "0";
            }
            return false;
        }

        private IEnumerable<InvocationExpressionSyntax> TraverseConditions(BinaryExpressionSyntax binaryExpression)
        {
            if (binaryExpression.Left is InvocationExpressionSyntax invocationLeft)
            {
                yield return invocationLeft;
            }

            if (binaryExpression.Right is InvocationExpressionSyntax invocationRight)
            {
                yield return invocationRight;
            }
        }
    }
}
