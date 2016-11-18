using FileHelpers;

namespace WoFFTool.ImportObjects
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst]
    internal class ExpItem
    {
        public int Level { get; set; }  
        public int ExpNextLevel { get; set; } 
        public int ExpTotal { get; set; }
        public int Delta { get; set; }
        public int Sp { get; set; }
        public int SpTotal { get; set; }
    }
}
