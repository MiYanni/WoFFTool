using FileHelpers;
using System.Collections.Generic;
using WoFFTool.ImportObjects;

namespace WoFFTool
{
    internal static class Importer
    {
        public static IEnumerable<ExpItem> ConvertExpTable()
        {
            return Convert<ExpItem>(@".\Data\ExpTable.csv");
        }

        public static IEnumerable<BossItem> ConvertBossTable()
        {
            return Convert<BossItem>(@".\Data\BossStats.csv");
        }

        public static IEnumerable<SkillItem> ConvertSkillTable()
        {
            return Convert<SkillItem>(@".\Data\GeneralSkills.csv");
        }

        public static IEnumerable<PrismtunityMementoItem> ConvertPrismtunityMementoTable()
        {
            return Convert<PrismtunityMementoItem>(@".\Data\Prismtunity+Memento.csv");
        }

        private static IEnumerable<T> Convert<T>(string fileName) where T : class
        {
            return (T[])new FileHelperEngine(typeof(T)).ReadFile(fileName);
        }
    }
}
