using System;
using System.Collections.Generic;
using System.Text;

namespace Scanf.Test.SF1003.TestData.Diagnostics
{
    public class MethodWithSingleLineComment
    {
        public void Foo()
        {
            // TODO : This should be caught
            int a = 3;
            Console.WriteLine(a);
        }
    }
}
