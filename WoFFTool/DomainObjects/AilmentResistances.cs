using System;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    public class AilmentResistances
    {
        public int? Poison { get; set; }
        public int? Confuse { get; set; }
        public int? Sleep { get; set; }
        public int? Blind { get; set; }
        public int? Oblivion { get; set; }
        public int? Berserk { get; set; }
        public int? Slow { get; set; }
        public int? Doom { get; set; }
    }
}
