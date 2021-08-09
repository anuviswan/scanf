using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.ConstructorInvokingVirtualMethodAnalyzer,
Scanf.CodeSmell.EmptyMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1009Tests
    {
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
                @"SF1009\TestData\NoDiagnostics\ClassWithConstructorButNoMethodCalls.cs",
            };

            yield return new object[]
            {
                @"SF1009\TestData\NoDiagnostics\ClassWithNoExplicitConstructor.cs",
            };
            
            yield return new object[]
            {
                @"SF1009\TestData\NoDiagnostics\ClassWithConstructorButNoVirtualMethods.cs",
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, int line, int col, string value)
        {
            var rule = VerifyCS.Diagnostic(TodoAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputFile), expected);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1009\TestData\Diagnostics\ClassWithConstructorCallingVirtualMethods.cs",
                11,13,
                "// TODO : This should be caught"
            };

        }
    }
}
