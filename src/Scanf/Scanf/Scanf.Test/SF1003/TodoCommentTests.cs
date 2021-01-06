using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
    Scanf.CodeSmell.TodoAnalyzer,
    Scanf.CodeSmell.EmptyMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1003Tests
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
                @"SF1003\TestData\NoDiagnostics\NoComments.cs"
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputSrc, int line, int col,string value)
        {
            var rule = VerifyCS.Diagnostic(TodoAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputSrc), expected);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1003\TestData\Diagnostics\MethodWithSingleLineComment.cs",
                11,13,
                "// TODO : This should be caught"
            };

            yield return new object[]
            {
                @"SF1003\TestData\Diagnostics\MethodWithMultiLineComment.cs",
                11,13,
                @"/* This a ordinary comment
             * TODO : This should be caught
             * This should be ignored as well
             */"
            };
        }
    }
}
