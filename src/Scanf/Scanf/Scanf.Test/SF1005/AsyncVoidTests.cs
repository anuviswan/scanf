﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scanf.CodeSmell;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
Scanf.CodeSmell.AsyncVoidAnalyzer,
Scanf.CodeSmell.AsyncVoidCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class SF1005Tests
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
                @"SF1005\TestData\NoDiagnostics\WithNoAsyncMethods.cs",
            };

            yield return new object[]
            {
                @"SF1005\TestData\NoDiagnostics\AsyncMethodsWithTaskReturnType.cs",
            };

            yield return new object[]
            {
                @"SF1005\TestData\NoDiagnostics\AsyncVoidForEventHandlers.cs",
            };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string inputFile, int line, int col, string value, string expectedCodeFixFile)
        {
            var inputSource = File.ReadAllText(inputFile);
            var expectedCodeFixSource = File.ReadAllText(expectedCodeFixFile);
            var rule = VerifyCS.Diagnostic(AsyncVoidAnalyzer.DiagnosticId);
            var expected = rule.WithLocation(line, col).WithArguments(value.Trim());
            await VerifyCS.VerifyCodeFixAsync(inputSource, expected, expectedCodeFixSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            yield return new object[]
            {
                @"SF1005\TestData\Diagnostics\AsyncMethodsWithVoid.cs",
                7,9,
                "MethodWithAsyncVoid",
                 @"SF1005\TestData\Diagnostics\AsyncMethodsWithVoid_Fix_ReturnTask.cs",
            };

        }
    }
}
