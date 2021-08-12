using System.Linq.Expressions;

namespace Scanf.Test.SF1006.TestData.NoDiagnostics
{
    public class MethodWithNoIfCondition
    {
        public void Foo()
        {
            Expression.Empty();
        }
    }
}
