﻿using System;

namespace Scanf.Test.SF1011.TestData.Diagnostics
{
    [Flags]
    public enum EnumWithFlagAttribute
    {
        None = 0,
        First,
        Second = 3,
        Third,
        Four
    }
}
