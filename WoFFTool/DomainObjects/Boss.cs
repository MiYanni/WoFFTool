using System;
using WoFFTool.ImportObjects;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    public class Boss
    {
        public Boss() { }

        internal Boss(BossItem boss, int order)
        {
            Order = order;
            Name = boss.Mirage;
            Hp = boss.Hp;
            Elemental = new ElementalResistances
            {
                Fire = boss.Fire,
                Ice = boss.Ice,
                Thunder = boss.Thunder,
                Aero = boss.Aero,
                Water = boss.Water,
                Earth = boss.Earth,
                Light = boss.Light,
                Dark = boss.Dark
            };
            Ailment = new AilmentResistances
            {
                Poison = boss.Poison,
                Confuse = boss.Confuse,
                Sleep = boss.Sleep,
                Blind = boss.Blind,
                Oblivion = boss.Oblivion,
                Berserk = boss.Berserk,
                Slow = boss.Slow,
                Doom = boss.Doom
            };
            Exp = boss.Exp;
            Gil = boss.Gil;
            Drops = boss.Drop;
            Notes = boss.Notes;
        }

        public int Order { get; set; }
        public string Name { get; set; }
        public int? Hp { get; set; }
        public ElementalResistances Elemental { get; set; }
        public AilmentResistances Ailment { get; set; }
        public int? Exp { get; set; }
        public int? Gil { get; set; }
        public string Drops { get; set; }
        public string Notes { get; set; }
    }
}
