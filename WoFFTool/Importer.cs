using FileHelpers;
using System.Collections.Generic;
using WoFFTool.DataObjects;

namespace WoFFTool
{
    internal static class Importer
    {
        public static IEnumerable<ExpItem> ConvertExpTable()
        {
            //const string fileName = @".\Data\ExpTable.csv";
            //var engine = new FileHelperEngine(typeof(ExpItem));
            //return (ExpItem[])engine.ReadFile(fileName);
            return Convert<ExpItem>(@".\Data\ExpTable.csv");
        }

        public static IEnumerable<BossItem> ConvertBossTable()
        {
            return Convert<BossItem>(@".\Data\BossStats.csv");
        }

        private static IEnumerable<T> Convert<T>(string fileName) where T : class
        {
            return (T[])new FileHelperEngine(typeof(T)).ReadFile(fileName);
        }
    }
}
