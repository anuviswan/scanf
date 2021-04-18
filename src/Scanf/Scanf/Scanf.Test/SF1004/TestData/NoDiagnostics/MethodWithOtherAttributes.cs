using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Scanf.Test.SF1004.TestData.NoDiagnostics
{
    public class MethodWithOtherAttributes
    {
        [Conditional("Debug")]
        public void NonPureMethodWithOtherAttributesAndVoidReturnType()
        {
            Expression.Empty();
        }


        [MockAttribute]
        public int NonPureMethodWithOtherAttributesAndIntReturnType()
        {
            return default;
        }
    }

    public class MockAttribute : Attribute
    {

    }
}
