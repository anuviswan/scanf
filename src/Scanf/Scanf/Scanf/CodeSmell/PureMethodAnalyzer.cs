﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using System.Linq;
using Scanf.Utils.ExtensionMethods;
using System.Diagnostics.Contracts;

namespace Scanf.CodeSmell
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PureMethodAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = DiagnosticCodes.PureMethodRule;

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.SFA_1004_AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.SFA_1004_AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.SFA_1004_AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private static string Category = DiagnosticsCategory.CodeSmell;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.MethodDeclaration);
        }

        private void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            var returnType = methodDeclaration.ReturnType;
            var isReturnTypeVoid = ((PredefinedTypeSyntax)returnType).Keyword.Kind() == SyntaxKind.VoidKeyword;
            var hasPureAttribute = methodDeclaration.HasAttribute<PureAttribute>(); ;

            if (isReturnTypeVoid && hasPureAttribute)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), methodDeclaration.Identifier.Value));
            }
        }
    }
}
