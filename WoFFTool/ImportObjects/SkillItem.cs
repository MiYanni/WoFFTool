using FileHelpers;

namespace WoFFTool.ImportObjects
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst]
    internal class SkillItem
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Skill;
        public int? Ap;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Element;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Description;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Mirages;
    }
}
