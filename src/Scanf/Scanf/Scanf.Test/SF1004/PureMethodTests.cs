using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using VerifyCS = Scanf.Test.CSharpAnalyzerVerifier<
Scanf.CodeSmell.PureMethodAnalyzer>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1004Tests
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
                @"SF1004\TestData\NoDiagnostics\NoPureMethods.cs",
            };

            yield return new object[]
            {
                @"SF1004\TestData\NoDiagnostics\MethodWithOtherAttributes.cs",
            };

            yield return new object[]
            {
                @"SF1004\TestData\NoDiagnostics\MethodWithPureAttributeWithReturnType.cs",
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputSrc, int line, int col)
        {
            var rule = VerifyCS.Diagnostic(PureMethodAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col);
            await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputSrc), expected);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1004\TestData\Diagnostics\MethodWithPureAttribute.cs",
                8,9
            };

            yield return new object[]
            {
                @"SF1004\TestData\Diagnostics\PureMethodWithMultipleAttribute.cs",
                9,9
            };
        }
    }
}
