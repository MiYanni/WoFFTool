using FileHelpers;

namespace WoFFTool.ImportObjects
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst]
    internal class PrismtunityMementoItem
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Mirage;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Prismtunity;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string MementoLocation;
        [FieldNullValue(false)]
        public bool Dlc;
        [FieldNullValue(false)]
        public bool SuiGeneris;
    }
}
