using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.Bug;
using Scanf.Test.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.CustomExceptionsPublicAnalyzer,
Scanf.CodeSmell.EmptyMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1010Tests
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
                @"SF1010\TestData\NoDiagnostics\ClassWithPublicException.cs",
            };

            yield return new object[]
            {
                @"SF1010\TestData\NoDiagnostics\ClassWithPublicExceptionFromCustomException.cs",
            };

        }

        ////Diagnostic and CodeFix both triggered and checked for
        //[TestMethod]
        //[DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        //public async Task CodeThatRequireFix(string inputFile, params DiagnosticResultWrapper[] expectedResult)
        //{
        //    var expectedDiagnostics = new List<DiagnosticResult>();
        //    foreach (var expected in expectedResult)
        //    {
        //        var rule = VerifyCS.Diagnostic(ConstructorInvokingVirtualMethodAnalyzer.DiagnosticId);
        //        expectedDiagnostics.Add(rule.WithLocation(expected.Line, expected.Column).WithArguments(expected.Value.Trim()));
        //    }

        //    await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputFile), expectedDiagnostics.ToArray());
        //}
        //private static IEnumerable<object[]> GetInvalidData()
        //{
        //    yield return new object[]
        //    {
        //        @"SF1009\TestData\Diagnostics\ClassWithConstructorCallingVirtualMethods.cs",
        //        new[]
        //        {
        //            new DiagnosticResultWrapper{ Line = 9, Column= 13, Value = "Foo" }
        //        }
        //    };

        //    yield return new object[]
        //    {
        //        @"SF1009\TestData\Diagnostics\ClassWithConstructorWithTwoMethodCalls.cs",
        //        new[]
        //        {
        //            new DiagnosticResultWrapper{ Line = 9, Column= 13, Value = "Foo" },
        //            new DiagnosticResultWrapper{ Line = 10, Column= 13, Value = "Bar" }
        //        }
        //    };

        //}
    }
}
