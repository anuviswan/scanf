using System.Linq.Expressions;

namespace Scanf.Test.SF1002.TestData.NoDiagnostics
{
    public class MethodWithExpressionEmpty
    {
        public void Bar() => Expression.Empty();
    }
}
