using System;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    public class ElementalResistances
    {
        public int? Fire { get; set; }
        public int? Ice { get; set; }
        public int? Lightning { get; set; }
        public int? Aero { get; set; }
        public int? Water { get; set; }
        public int? Earth { get; set; }
        public int? Light { get; set; }
        public int? Dark { get; set; }
    }
}
