using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = Scanf.Test.CSharpCodeFixVerifier<
    Scanf.CodeSmell.EmptyMethodAnalyzer,
    Scanf.EmptyMethodCodeFixProvider>;

namespace Scanf.Test
{
    [TestClass]
    public class ScanfUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public async Task CodeThatDoesNotRequireFix(string source)
        {
            await VerifyCS.VerifyAnalyzerAsync(source);
        }

        private static IEnumerable<object[]> GetValidData()
        {
            var codeWithNoEmptyMethods = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class Foo
        {   
            public void Bar()
            {
                var i = 4;
                Console.WriteLine(i);
            }

        }
    }";

            yield return new object[] { @"" };
            yield return new object[] { codeWithNoEmptyMethods };
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public async Task CodeThatRequireFix(string source,string fixedSource)
        {
            var r = VerifyCS.Diagnostic("SF1002");
            var expected = r.WithLocation(19,13).WithArguments("Foo1");
            await VerifyCS.VerifyCodeFixAsync(source, expected, fixedSource);
        }
        private static IEnumerable<object[]> GetInvalidData()
        {
            var codeThatHasEmptyMethod = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class Foo
        {   
            public void Bar()
            {
                var i = 4;
                Console.WriteLine(i);
            }

            public void Foo1()
            {
            }
        }
    }";

            var codeWithEmptyMethodRemoved = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class Foo
        {   
            public void Bar()
            {
                var i = 4;
                Console.WriteLine(i);
            }
        }
    }";

            yield return new object[] { codeThatHasEmptyMethod, codeWithEmptyMethodRemoved };
        }
    }
}
