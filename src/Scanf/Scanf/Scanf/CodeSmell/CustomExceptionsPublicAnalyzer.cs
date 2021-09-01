using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scanf.Helpers;
using Scanf.Utils.ExtensionMethods;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

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

            context.RegisterSyntaxNodeAction(AnalyzerExceptionClass,SyntaxKind.ClassDeclaration);
        }

        private void AnalyzerExceptionClass(SyntaxNodeAnalysisContext context)
        {
            var classDeclaration = (ClassDeclarationSyntax)context.Node;
            var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);

            if (InheritsFrom<System.Exception>(symbol))
            {
                var accessModifier = GetAccessModifier(classDeclaration);
                if(accessModifier != SyntaxKind.PublicKeyword)
                {
                    var index = classDeclaration.Modifiers.IndexOf(accessModifier);
                    context.ReportDiagnostic(Diagnostic.Create(Rule, classDeclaration.Modifiers[index].GetLocation(), classDeclaration.Modifiers.IndexOf(SyntaxKind.PrivateKeyword)));
                }
                
            }
        }

        private SyntaxKind GetAccessModifier(ClassDeclarationSyntax classDeclaration)
        {
            var modifiers = new List<SyntaxKind> { SyntaxKind.PublicKeyword, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword };
            foreach(var modifer in classDeclaration.Modifiers)
            {
                if (modifiers.Contains(modifer.Kind()))
                {
                    return modifer.Kind();
                }
            }
            return SyntaxKind.PrivateKeyword;
              
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
