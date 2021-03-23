using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using Scanf.Utils.ExtensionMethods;

namespace Scanf.CodeSmell
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class InitializeCollectionAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.InitializeCollectionRule;

        
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1001_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1001_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1001_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private static string Category = DiagnosticsCategory.CodeSmell;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.ObjectCreationExpression );
        }

        private void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var creation = (ObjectCreationExpressionSyntax)context.Node;
            var variableType = creation.Type as IdentifierNameSyntax;
            //if (variableType is null) return;

            var variableTypeInfo = context.SemanticModel.GetTypeInfo(context.Node);
            var symbolTypeInfo = context.SemanticModel.GetSymbolInfo(context.Node);

            
            var r = context.Compilation.GetTypeByMetadataName("System.Collections.Generic.List`1");

            var f = variableTypeInfo.Equals(r);
            //var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            //var isAsyncMethod = methodDeclaration.IsAsync();
            //var isEventHandler = methodDeclaration.IsEvantHandler(context);
            //var returnType = methodDeclaration.ReturnType;


            //if (returnType is PredefinedTypeSyntax && !isEventHandler)
            //{
            //    var isReturnTypeVoid = ((PredefinedTypeSyntax)returnType).Keyword.Kind() == SyntaxKind.VoidKeyword;

            //    if (isAsyncMethod && isReturnTypeVoid)
            //    {
            //        context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), methodDeclaration.Identifier.Value));
            //    }
            //}

        }
    }
}
