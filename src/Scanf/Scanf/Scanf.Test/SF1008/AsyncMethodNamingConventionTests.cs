using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.NamingConvention;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.NamingConvention.AsyncMethodAnalyzer,
Scanf.NamingConvention.AsyncMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1008Tests
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
                @"SF1008\TestData\NoDiagnostics\WithNoAsyncMethods.cs",
            };

            yield return new object[]
            {
                @"SF1008\TestData\NoDiagnostics\AsyncMethodFollowingNamingConventions.cs",
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, int line, int col, string value, string expectedCodeFixFile)
        {
            var inputSource = File.ReadAllText(inputFile);
            var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);
            var rule = VerifyCS.Diagnostic(AsyncMethodAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyCodeFixAsync(inputSource, expected, expectedCodeFixSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1008\TestData\Diagnostics\AsyncMethodWithoutNamingConventions.cs",
                7,9,
                "GetData",
                 @"SF1008\TestData\Diagnostics\AsyncMethodWithoutNamingConventions_Fix_RenameMethods.cs",
            };

            yield return new object[]
            {
                @"SF1008\TestData\Diagnostics\AsyncMethodWithCaseInsensitiveSuffix.cs",
                7,9,
                "GetDataasync",
                 @"SF1008\TestData\Diagnostics\AsyncMethodWithCaseInsensitiveSuffix_Fix_RenameMethods.cs",
            };

        }
    }
}
