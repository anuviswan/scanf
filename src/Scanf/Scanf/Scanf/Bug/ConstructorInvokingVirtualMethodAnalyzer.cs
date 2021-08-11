using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using System.Collections.Immutable;
using System.Linq;

namespace Scanf.Bug
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConstructorInvokingVirtualMethodAnalyzer:DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.ConstructorInvokingVirtualMethodsRule;
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
            var methodInvocations = constructor.DescendantNodes().OfType<InvocationExpressionSyntax>();

            foreach(var methodInvocation in methodInvocations)
            {
                var invokedMethod = context.SemanticModel.GetSymbolInfo(methodInvocation).Symbol;
                var isFromSameClass = invokedMethod.ContainingType.Name == constructor.Identifier.Text;
                var isVirtual = invokedMethod.IsVirtual;

                if (isFromSameClass && isVirtual)
                {
                    
                    context.ReportDiagnostic(Diagnostic.Create(Rule, methodInvocation.GetLocation(), invokedMethod.Name));
                }
            }
            //if (methodInvocations is null) return;

            
        }
    }
}
