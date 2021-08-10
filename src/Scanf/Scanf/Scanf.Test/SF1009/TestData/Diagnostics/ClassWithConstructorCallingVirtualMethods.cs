using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scanf.Test.SF1009.TestData.Diagnostics
{
    public class ClassWithConstructorCallingVirtualMethods
    {
        public ClassWithConstructorCallingVirtualMethods()
        {
            Foo();
        }
        public virtual void Foo()
        {
            Expression.Empty();
        }
    }
}
