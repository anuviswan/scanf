using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Scanf.Test.SF1004.TestData.Diagnostics
{
    public class PureMethodWithMultipleAttribute
    {
        [Pure]
        [Conditional("DEBUG")]
        public void PureMethodWithMultipleAttributesAndVoidReturnType()
        {
            Expression.Empty();
        }
    }
}
