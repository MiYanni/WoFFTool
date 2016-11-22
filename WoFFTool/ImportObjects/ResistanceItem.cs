using FileHelpers;

namespace WoFFTool.ImportObjects
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst]
    internal class ResistanceItem
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Mirage;
        public int? Fire;
        public int? Ice;
        public int? Lightning;
        public int? Aero;
        public int? Water;
        public int? Earth;
        public int? Light;
        public int? Dark;
        public int? Poison;
        public int? Confuse;
        public int? Sleep;
        public int? Blind;
        public int? Oblivion;
        public int? Berserk;
        public int? Slow;
        public int? Doom;
        public int? Weight;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Size;
        [FieldNullValue(false)]
        public bool Dlc;
        [FieldNullValue(false)]
        public bool SuiGeneris;
    }
}
