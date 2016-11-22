using System;
using System.Linq;
using WoFFTool.ImportObjects;

namespace WoFFTool.DomainObjects
{
    [Serializable]
    public class Mirage
    {
        public Mirage() { }

        internal Mirage(ResistanceItem resistance, PrismtunityMementoItem prismtunityMemento)
        {
            if(prismtunityMemento != null)
            {
                Name = prismtunityMemento.Mirage;
                Prismtunity = prismtunityMemento.Prismtunity;
                MementoLocation = prismtunityMemento.MementoLocation;
                IsDlc = prismtunityMemento.Dlc;
                IsSuiGeneris = prismtunityMemento.SuiGeneris;
            }

            if (resistance != null)
            {
                Name = resistance.Mirage;
                Elemental = new ElementalResistances
                {
                    Fire = resistance.Fire,
                    Ice = resistance.Ice,
                    Lightning = resistance.Lightning,
                    Aero = resistance.Aero,
                    Water = resistance.Water,
                    Earth = resistance.Earth,
                    Light = resistance.Light,
                    Dark = resistance.Dark
                };
                Ailment = new AilmentResistances
                {
                    Poison = resistance.Poison,
                    Confuse = resistance.Confuse,
                    Sleep = resistance.Sleep,
                    Blind = resistance.Blind,
                    Oblivion = resistance.Oblivion,
                    Berserk = resistance.Berserk,
                    Slow = resistance.Slow,
                    Doom = resistance.Doom
                };
                Weight = resistance.Weight;
                Size = SizeExtensions.Values.Single(s => s.GetName() == resistance.Size);
                IsDlc = resistance.Dlc;
                IsSuiGeneris = resistance.SuiGeneris;
            }
        }

        public string Name { get; set; }
        public bool IsDlc { get; set; }
        public bool IsSuiGeneris { get; set; }
        public ElementalResistances Elemental { get; set; }
        public AilmentResistances Ailment { get; set; }
        public int? Weight { get; set; }
        public Size Size { get; set; }
        public string Prismtunity { get; set; }
        public string MementoLocation { get; set; }
    }
}
