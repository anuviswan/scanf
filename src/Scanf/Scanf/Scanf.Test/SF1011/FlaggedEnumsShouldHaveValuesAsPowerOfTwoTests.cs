using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.Bug;
using Scanf.Test.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.Bug.FlaggedEnumsShouldHaveValuesAsPowerOfTwoAnalyzer,
Scanf.CodeSmell.CustomExceptionsPublicCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1011Tests
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
                @"SF1011\TestData\NoDiagnostics\EnumWithFlagAttribute.cs",
            };

            yield return new object[]
            {
                @"SF1011\TestData\NoDiagnostics\EnumWithoutFlagAttribute.cs",
            };
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, params DiagnosticResultWrapper[] expectedResult)
        {
            var expectedDiagnostics = new List<DiagnosticResult>();
            foreach (var expected in expectedResult)
            {
                var rule = VerifyCS.Diagnostic(FlaggedEnumsShouldHaveValuesAsPowerOfTwoAnalyzer.DiagnosticId);
                expectedDiagnostics.Add(rule.WithLocation(expected.Line, expected.Column).WithArguments(expected.Value.Trim()));
            }

            await VerifyCS.VerifyAnalyzerAsync(File.ReadAllText(inputFile), expectedDiagnostics.ToArray());
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1011\TestData\Diagnostics\EnumWithFlagAttribute.cs",
                new[]
                {
                    new DiagnosticResultWrapper{ Line = 5, Column= 5, Value = "EnumWithFlagAttribute" }
                }
            };
            yield return new object[]
            {
                @"SF1011\TestData\Diagnostics\EnumWithFlagAttributeExplicitValues.cs",
                new[]
                {
                    new DiagnosticResultWrapper{ Line = 5, Column= 5, Value = "EnumWithFlagAttribute" }
                }
            };
        }
    }
}
