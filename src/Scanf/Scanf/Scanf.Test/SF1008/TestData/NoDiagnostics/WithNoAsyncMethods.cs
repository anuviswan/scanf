using System.Linq.Expressions;

namespace Scanf.Test.SF1008.TestData.NoDiagnostics
{
    class WithNoAsyncMethods
    {
        public void Foo()
        {
            Expression.Empty();
        }

        public int Bar()
        {
            Expression.Empty();
            return default;
        }
    }
}
