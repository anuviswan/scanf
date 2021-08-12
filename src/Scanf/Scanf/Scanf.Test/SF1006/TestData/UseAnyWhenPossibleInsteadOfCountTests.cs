using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
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
        public async Task CodeThatRequireFix(string inputFile, int line, int col, string value, string expectedCodeFixFile)
        {
            var inputSource = File.ReadAllText(inputFile);
            var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);
            var rule = VerifyCS.Diagnostic(UseAnyWhenPossibleInsteadOfCountAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyCodeFixAsync(inputSource, expected, expectedCodeFixSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1006\TestData\Diagnostics\MethodWithSingleIfConditionWithCountCall.cs",
                7,9,
                "GetData",
                 @"SF1006\TestData\Diagnostics\MethodWithSingleIfConditionWithCountCall.cs",
            };

        }
    }
}
