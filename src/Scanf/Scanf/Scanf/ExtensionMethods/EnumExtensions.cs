using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Scanf.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T source) where T :Enum
        {
            return source.GetType()
                .GetMember(source.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()
                ?.Description
                ?? source.ToString();
        }

    }
}
