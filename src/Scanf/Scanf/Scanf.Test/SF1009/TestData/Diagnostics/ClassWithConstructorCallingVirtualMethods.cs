﻿using System.Linq.Expressions;

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
