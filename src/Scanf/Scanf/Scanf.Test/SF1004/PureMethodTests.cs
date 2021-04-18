using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = Scanf.Test.CSharpAnalyzerVerifier<
Scanf.CodeSmell.PureMethodAnalyzer>;

namespace Scanf.Test
{
    [TestClass]
    public class PureMethodTests
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
        }
    }
}
