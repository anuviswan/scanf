using System.Linq;
using System.Linq.Expressions;

namespace Scanf.Test.SF1006.TestData.NoDiagnostics
{
    public class MethodWithCountExpressionEqualsNonZero
    {
        public void Foo()
        {
            var collection = Enumerable.Range(1, 100);

            if(collection.Count() == 1)
            {
                Expression.Empty();
            }
        }
    }
}
