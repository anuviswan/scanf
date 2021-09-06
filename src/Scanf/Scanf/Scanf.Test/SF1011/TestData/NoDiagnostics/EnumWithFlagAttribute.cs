using System;

namespace Scanf.Test.SF1011.TestData.NoDiagnostics
{
    [Flags]
    public enum EnumWithFlagAttribute
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 4
    }
}
