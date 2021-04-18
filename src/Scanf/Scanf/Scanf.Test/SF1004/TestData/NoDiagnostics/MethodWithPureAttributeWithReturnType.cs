using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Scanf.Test.SF1004.TestData.NoDiagnostics
{
    public class MethodWithPureAttributeWithReturnType
    {
        [Pure]
        public int PureMethodWithReturnType()
        {
            return default;
        }
    }
}
