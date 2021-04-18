using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Text;

namespace TestSolution.Console
{
    public class SF1004
    {
        public void NonPureVoidMethod()
        {
            Expression.Empty();
        }


        public int NonPureMethodWithIntReturnType()
        {
            return default;
        }

        [Pure]
        [Conditional("DEBUG")]
        public void PureVoidMethod()
        {
            Expression.Empty();
        }
    }
}
