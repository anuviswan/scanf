using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using Scanf.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Scanf.Bug
{

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class FlaggedEnumsShouldHaveValuesAsPowerOfTwoAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.FlaggedEnumShouldHaveValuesPowerOfTwo;

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1011_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1011_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1011_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private static string Category = DiagnosticsCategory.Bug;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeEnumDeclaration, SyntaxKind.EnumDeclaration);
        }

        private void AnalyzeEnumDeclaration(SyntaxNodeAnalysisContext context)
        {
            var enumDeclaration = (EnumDeclarationSyntax)context.Node;
            if(enumDeclaration.HasAttribute<FlagsAttribute>())
            {
                var enumMembers = enumDeclaration.DescendantNodes().OfType<EnumMemberDeclarationSyntax>().ToList();
                var valueList = new List<int>(enumMembers.Count);

                var previousKnownValue = -1;
                foreach(var member in enumMembers)
                {
                    if (member.EqualsValue is null)
                    {
                        valueList.Add(++previousKnownValue);
                        continue;
                    };
                    if (member.EqualsValue.Value.Kind() == SyntaxKind.NumericLiteralExpression)
                    {
                        var val = Convert.ToInt32(member.EqualsValue.Value.GetFirstToken().Value);
                        previousKnownValue = val;
                        valueList.Add(val);
                    }
                }
                if (!IsCollectionValuePowerOfTwo(valueList))
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, enumDeclaration.GetLocation(), enumDeclaration.Identifier.Value));
                }
            }
        }

        private bool IsCollectionValuePowerOfTwo(IEnumerable<int> values)
        {
            return values.All(x=>IsPowerOfTwo(x));
        }

        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }
    }
}
