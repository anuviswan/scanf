using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
    Scanf.CodeSmell.EmptyMethodAnalyzer,
    Scanf.CodeSmell.EmptyMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF002Tests
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
                @"SF1002\TestData\NoDiagnostics\NoEmptyMethods.cs"
            };
            yield return new object[]
            {
                @"SF1002\TestData\NoDiagnostics\MethodWithExpressionEmpty.cs"
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputSrc,string expectedSrc,int line,int col)
        {
            var rule = VerifyCS.Diagnostic(EmptyMethodAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line,col).WithArguments("Bar");
            await VerifyCS.VerifyCodeFixAsync(File.ReadAllText(inputSrc), expected, File.ReadAllText(expectedSrc));
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[] 
            { 
                @"SF1002\TestData\Diagnostics\UnusedEmptyMethods.cs",
                @"SF1002\TestData\Diagnostics\UnusedEmptyMethods_Fix_RaiseException.cs",
                13,9
            };
        }
    }
}
