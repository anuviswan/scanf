using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using Scanf.Test.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.CustomExceptionsPublicAnalyzer,
Scanf.CodeSmell.CustomExceptionsPublicCodeFixProvider>;

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

            yield return new object[]
            {
                @"SF1010\TestData\NoDiagnostics\NonExceptionClass.cs",
            };

        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, DiagnosticResultWrapper expectedResult, string expectedCodeFixFile)
        {
            var inputSource = File.ReadAllText(inputFile);
            var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);
            var rule = VerifyCS.Diagnostic(CustomExceptionsPublicAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(expectedResult.Line, expectedResult.Column).WithArguments(expectedResult.Value);
            await VerifyCS.VerifyCodeFixAsync(inputSource, expected, expectedCodeFixSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1010\TestData\Diagnostics\ClassWithInternalException.cs",
                new DiagnosticResultWrapper{ Line = 5, Column= 5, Value = "FooException" },
                @"SF1010\TestData\Diagnostics\ClassWithInternalException_Fix_PublicAccessor.cs",
            };
            yield return new object[]
{
                @"SF1010\TestData\Diagnostics\ClassWithInheritedExceptionInternal.cs",
                new DiagnosticResultWrapper{ Line = 9, Column= 5, Value = "ChildException" },
                @"SF1010\TestData\Diagnostics\ClassWithInheritedExceptionInternal_Fix_PublicAccessor.cs",
};
        }
    }
}
