using System;
using System.Linq;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    internal enum Size
    {
        S,
        M,
        L,
        XL
    }

    internal static class SizeExtensions
    {
        public static Size[] Values { get; } = Enum.GetValues(typeof(Size)).Cast<Size>().ToArray();

        public static string GetName(this Size value)
        {
            return Enum.GetName(typeof(Size), value);
        }
    }
}
