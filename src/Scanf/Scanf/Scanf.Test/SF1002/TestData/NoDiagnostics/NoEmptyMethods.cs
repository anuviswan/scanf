using System;
using System.Collections.Generic;
using System.Text;

namespace Scanf.Test.SF1002.TestData.NoDiagnostics
{
    public class NoEmptyMethods
    {
        public void Foo()
        {
            Console.WriteLine("Non-Empty Method");
        }

        public int Bar()
        {
            Console.WriteLine("Non-Empty Method");
            return default;
        }
    }
}
