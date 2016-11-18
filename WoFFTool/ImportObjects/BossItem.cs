using FileHelpers;

namespace WoFFTool.ImportObjects
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst]
    class BossItem
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Mirage;
        public int? Hp;
        public int? Fire;
        public int? Ice;
        public int? Thunder;
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
        public int? Exp;
        public int? Gil;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Drop;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Notes;
    }
}
