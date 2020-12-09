using System;
using System.Collections.Generic;
using System.Text;

namespace Scanf.Test.SF1003.TestData.Diagnostics
{
    class MethodWithMultiLineComment
    {
        public void Foo()
        {
            /* This a ordinary comment
             * TODO : This should be caught
             * This should be ignored as well
             */
            int a = 3;
            Console.WriteLine(a);
        }
    }
}
