using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using Scanf.Utils.ExtensionMethods;

namespace Scanf.CodeSmell
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CustomExceptionsPublicAnalyzer :DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.AsyncVoidRule;

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1010_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1010_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1010_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private static string Category = DiagnosticsCategory.CodeSmell;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            

        }
        private bool InheritsFrom<T>(INamedTypeSymbol symbol)
        {
            while (true)
            {
                if (symbol.ToString() == typeof(T).FullName)
                {
                    return true;
                }
                if (symbol.BaseType != null)
                {
                    symbol = symbol.BaseType;
                    continue;
                }
                break;
            }
            return false;
        }

    }
}
