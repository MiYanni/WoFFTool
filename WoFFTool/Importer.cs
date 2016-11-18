using FileHelpers;
using System.Collections.Generic;
using WoFFTool.DataObjects;

namespace WoFFTool
{
    internal static class Importer
    {
        public static IEnumerable<ExpItem> ConvertExpTable()
        {
            const string fileName = @".\Data\ExpTable.csv";
            var engine = new FileHelperEngine(typeof(ExpItem));
            return (ExpItem[])engine.ReadFile(fileName);
        }
    }
}
