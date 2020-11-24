using System;
using System.Linq.Expressions;

namespace TestSolution.Console
{
    public class SF1002
    {
        public void Method1() // Should show warning
        {
        }

        public void Method2() // Should NOT show warning
        {
            int sample = 2;
            System.Console.WriteLine(sample);
        }

        public int Method3() // Should NOT show warning
        {
            return 0;
        }

        public void Method4() => System.Console.WriteLine("Mock"); // Should NOT raise warning

        public void Method5() => Expression.Empty(); // Should raise exception
    }
}
