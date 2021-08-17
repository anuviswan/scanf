using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using Scanf.Test.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.UseAnyWhenPossibleInsteadOfCountAnalyzer,
Scanf.NamingConvention.AsyncMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1006Tests
    {
        //No diagnostics expected to show up
        [TestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public async Task CodeWithoutDiagnostics(string input)
        {
            var source = File.ReadAllText(input);
            await VerifyCS.VerifyAnalyzerAsync(source);
        }

        private static IEnumerable<object[]> GetValidData()
        {
            yield return new object[]
            {
                @"SF1006\TestData\NoDiagnostics\MethodWithNoIfCondition.cs",
            };

        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, DiagnosticResultWrapper []expectedResults, string expectedCodeFixFile)
        {
            var expectedDiagnostics = new List<DiagnosticResult>();
            foreach (var expected in expectedResults)
            {
                var rule = VerifyCS.Diagnostic(UseAnyWhenPossibleInsteadOfCountAnalyzer.DiagnosticId);
                expectedDiagnostics.Add(rule.WithLocation(expected.Line, expected.Column).WithArguments(expected.Value.Trim()));
            }

            await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputFile), expectedDiagnostics.ToArray());

            //var inputSource = File.ReadAllText(inputFile);
            //var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);

            //var expectedCollection = new List<DiagnosticResult>();
            //foreach (var expected in expectedResults)
            //{
            //    var rule = VerifyCS.Diagnostic(UseAnyWhenPossibleInsteadOfCountAnalyzer.DiagnosticId);
            //    expectedCollection.Add(rule.WithLocation(expected.Line, expected.Column).WithArguments(expected.Value.Trim()));
            //}

            //await VerifyCS.VerifyCodeFixAsync(inputSource, expectedCollection.ToArray(), expectedCodeFixSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {


            yield return new object[]
            {
                @"SF1006\TestData\Diagnostics\MethodWithSingleIfConditionWithCountCall.cs",
                new []
                {
                    new DiagnosticResultWrapper
                    {
                        Line = 11,
                        Column = 17,
                        Value = "Count"
                    }
                },
                @"SF1006\TestData\Diagnostics\MethodWithSingleIfConditionWithCountCall_Fix_UseAny.cs",
            };

            yield return new object[]
            {
                @"SF1006\TestData\Diagnostics\MethodWithMultipleIfConditionWithCountCall.cs",
                new []
                {
                    new DiagnosticResultWrapper
                    {
                        Line = 11,
                        Column = 17,
                        Value = "Count"
                    },
                    new DiagnosticResultWrapper
                    {
                        Line = 11,
                        Column = 44,
                        Value = "Count"
                    },
                },
                @"SF1006\TestData\Diagnostics\MethodWithMultipleIfConditionWithCountCall_Fix_UseAny.cs",
            };

        }
    }
}
