using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Scanf.Test.SF1004.TestData.Diagnostics
{
    public class MethodWithPureAttribute
    {
        [Pure]
        public void PureMethodWithVoidReturnType()
        {
            Expression.Empty();
        }
    }
}
