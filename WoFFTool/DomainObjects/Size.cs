using System;
using System.Linq;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    public enum Size
    {
        S,
        M,
        L,
        XL
    }

    public static class SizeExtensions
    {
        public static Size[] Values { get; } = Enum.GetValues(typeof(Size)).Cast<Size>().ToArray();

        public static string GetName(this Size value)
        {
            return Enum.GetName(typeof(Size), value);
        }
    }
}
