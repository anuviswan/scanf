using System.Linq.Expressions;

namespace Scanf.Test.SF1009.TestData.Diagnostics
{
    public class ClassWithConstructorWithTwoMethodCalls
    {
        public ClassWithConstructorWithTwoMethodCalls()
        {
            Bar();
            Foo();
        }
        public virtual void Foo()
        {
            Expression.Empty();
        }

        public virtual void Bar()
        {
            Expression.Empty();
        }
    }
}
