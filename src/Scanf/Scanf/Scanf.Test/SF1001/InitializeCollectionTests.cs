using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.AsyncVoidAnalyzer,
Scanf.CodeSmell.AsyncVoidCodeFixProvider>;


namespace Scanf.Test.SF1001
{
    public class SF1001Tests
    {
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, int line, int col, string value, string expectedCodeFixFile)
        {
            var inputSource = File.ReadAllText(inputFile);
            var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);
            var rule = VerifyCS.Diagnostic(InitializeCollectionAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyCodeFixAsync(inputSource, expected, expectedCodeFixSource);
        }

        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1001\TestData\Diagnostics\CollectionWithSizeInitialized.cs",
            };

            
        }
    }
}
