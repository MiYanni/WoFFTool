using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using WoFFTool.DomainObjects;
using WoFFTool.ImportObjects;

namespace WoFFTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ReadMirages();
            ReadBosses();
        }

        #region Menu
        private void ExitMenuItem_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        private void DigitsOnly_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            var text = (sender as TextBox).Text + e.Text;
            var regex = new Regex("^-?[0-9]*$");
            e.Handled = !regex.IsMatch(text);
        }

        private void Compare_Click(Object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonText = button.Content.ToString();
            button.Content = buttonText == "=" ? ">" : (buttonText == ">" ? "<" : "=");
            if(button.Name.StartsWith("Mirage"))
            {
                UpdateFilteredMirages_TextChanged(sender, null);
            }
            if(button.Name.StartsWith("Boss"))
            {
                UpdateFilteredBosses_TextChanged(sender, null);
            }
        }

        private bool ResistanceFilter(string text, int? resistance, string comparer)
        {
            bool compare()
            {
                var value = Convert.ToInt32(text);
                switch (comparer)
                {
                    case "=":
                        return resistance == value;
                    case ">":
                        return resistance >= value;
                    case "<":
                        return resistance <= value;
                }
                return true;
            }
            return ((String.IsNullOrEmpty(text) || text.Equals("-")) || compare());
        }

        public List<Mirage> Mirages { get; set; }

        public List<Mirage> FilteredMirages { get; set; }

        private void ReadMirages()
        {
            if(!File.Exists("Mirages.xml"))
            {
                //TODO: Create MessageBox
            }
            var serializer = new XmlSerializer(typeof(List<Mirage>));
            using (var reader = new StringReader(File.ReadAllText("Mirages.xml")))
            {
                Mirages = (List<Mirage>)serializer.Deserialize(reader);
                UpdateMirageDataGrid(Mirages);
            }
        }

        private void UpdateMirageDataGrid(List<Mirage> mirages)
        {
            MirageDataGrid.ItemsSource = null;
            MirageDataGrid.ItemsSource = mirages;
        }

        private void UpdateFilteredMirages_TextChanged(Object sender, TextChangedEventArgs e)
        {
            FilteredMirages = Mirages.Where(m =>
                (String.IsNullOrEmpty(MirageNameFilterTxtBox.Text) || m.Name.ToLower().StartsWith(MirageNameFilterTxtBox.Text.ToLower())) &&
                (String.IsNullOrEmpty(MirageSizeFilterTxtBox.Text) || m.Size.GetName().ToLower().StartsWith(MirageSizeFilterTxtBox.Text.ToLower())) &&
                ((String.IsNullOrEmpty(MirageWeightFilterTxtBox.Text) || MirageWeightFilterTxtBox.Text.Equals("-")) || m.Weight == Convert.ToInt32(MirageWeightFilterTxtBox.Text)) &&

                ResistanceFilter(MirageFireFilterTxtBox.Text, m.Elemental.Fire, MirageFireFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageIceFilterTxtBox.Text, m.Elemental.Ice, MirageIceFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageThunderFilterTxtBox.Text, m.Elemental.Thunder, MirageThunderFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageAeroFilterTxtBox.Text, m.Elemental.Aero, MirageAeroFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageWaterFilterTxtBox.Text, m.Elemental.Water, MirageWaterFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageEarthFilterTxtBox.Text, m.Elemental.Earth, MirageEarthFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageLightFilterTxtBox.Text, m.Elemental.Light, MirageLightFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageDarkFilterTxtBox.Text, m.Elemental.Dark, MirageDarkFilterCompareBtn.Content.ToString()) &&

                ResistanceFilter(MiragePoisonFilterTxtBox.Text, m.Ailment.Poison, MiragePoisonFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageConfuseFilterTxtBox.Text, m.Ailment.Confuse, MirageConfuseFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageSleepFilterTxtBox.Text, m.Ailment.Sleep, MirageSleepFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageBlindFilterTxtBox.Text, m.Ailment.Blind, MirageBlindFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageOblivionFilterTxtBox.Text, m.Ailment.Oblivion, MirageOblivionFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageBerserkFilterTxtBox.Text, m.Ailment.Berserk, MirageBerserkFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageSlowFilterTxtBox.Text, m.Ailment.Slow, MirageSlowFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(MirageDoomFilterTxtBox.Text, m.Ailment.Doom, MirageDoomFilterCompareBtn.Content.ToString()) &&

                (String.IsNullOrEmpty(MiragePrismtunityFilterTxtBox.Text) || m.Prismtunity.ToLower().Contains(MiragePrismtunityFilterTxtBox.Text.ToLower())) &&
                (String.IsNullOrEmpty(MirageMementoLocationFilterTxtBox.Text) || m.MementoLocation.ToLower().Contains(MirageMementoLocationFilterTxtBox.Text.ToLower()))
            ).ToList();
            UpdateMirageDataGrid(FilteredMirages);
        }

        public List<Boss> Bosses { get; set; }

        public List<Boss> FilteredBosses { get; set; }

        private void ReadBosses()
        {
            if (!File.Exists("Bosses.xml"))
            {
                //TODO: Create MessageBox
            }
            var serializer = new XmlSerializer(typeof(List<Boss>));
            using (var reader = new StringReader(File.ReadAllText("Bosses.xml")))
            {
                Bosses = (List<Boss>)serializer.Deserialize(reader);
                UpdateBossDataGrid(Bosses);
            }
        }

        private void UpdateBossDataGrid(List<Boss> bosses)
        {
            BossDataGrid.ItemsSource = null;
            BossDataGrid.ItemsSource = bosses;
        }

        private void UpdateFilteredBosses_TextChanged(Object sender, TextChangedEventArgs e)
        {
            FilteredBosses = Bosses.Where(b =>
                (String.IsNullOrEmpty(BossNameFilterTxtBox.Text) || b.Name.ToLower().StartsWith(BossNameFilterTxtBox.Text.ToLower())) &&
                ((String.IsNullOrEmpty(BossHpFilterTxtBox.Text) || BossHpFilterTxtBox.Text.Equals("-")) || b.Hp == Convert.ToInt32(BossHpFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(BossOrderFilterTxtBox.Text) || BossOrderFilterTxtBox.Text.Equals("-")) || b.Order == Convert.ToInt32(BossOrderFilterTxtBox.Text)) &&

                ResistanceFilter(BossFireFilterTxtBox.Text, b.Elemental.Fire, BossFireFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossIceFilterTxtBox.Text, b.Elemental.Ice, BossIceFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossThunderFilterTxtBox.Text, b.Elemental.Thunder, BossThunderFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossAeroFilterTxtBox.Text, b.Elemental.Aero, BossAeroFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossWaterFilterTxtBox.Text, b.Elemental.Water, BossWaterFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossEarthFilterTxtBox.Text, b.Elemental.Earth, BossEarthFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossLightFilterTxtBox.Text, b.Elemental.Light, BossLightFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossDarkFilterTxtBox.Text, b.Elemental.Dark, BossDarkFilterCompareBtn.Content.ToString()) &&

                ResistanceFilter(BossPoisonFilterTxtBox.Text, b.Ailment.Poison, BossPoisonFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossConfuseFilterTxtBox.Text, b.Ailment.Confuse, BossConfuseFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossSleepFilterTxtBox.Text, b.Ailment.Sleep, BossSleepFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossBlindFilterTxtBox.Text, b.Ailment.Blind, BossBlindFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossOblivionFilterTxtBox.Text, b.Ailment.Oblivion, BossOblivionFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossBerserkFilterTxtBox.Text, b.Ailment.Berserk, BossBerserkFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossSlowFilterTxtBox.Text, b.Ailment.Slow, BossSlowFilterCompareBtn.Content.ToString()) &&
                ResistanceFilter(BossDoomFilterTxtBox.Text, b.Ailment.Doom, BossDoomFilterCompareBtn.Content.ToString()) &&

                ((String.IsNullOrEmpty(BossExpFilterTxtBox.Text) || BossExpFilterTxtBox.Text.Equals("-")) || b.Exp == Convert.ToInt32(BossExpFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(BossGilFilterTxtBox.Text) || BossGilFilterTxtBox.Text.Equals("-")) || b.Gil == Convert.ToInt32(BossGilFilterTxtBox.Text)) &&
                (String.IsNullOrEmpty(BossDropsFilterTxtBox.Text) || b.Drops.ToLower().Contains(BossDropsFilterTxtBox.Text.ToLower())) &&
                (String.IsNullOrEmpty(BossNotesFilterTxtBox.Text) || b.Notes.ToLower().Contains(BossNotesFilterTxtBox.Text.ToLower()))
            ).ToList();
            UpdateBossDataGrid(FilteredBosses);
        }

        #region Dev

        #region Import
        private void ExpTableConvertBtn_Click(Object sender, RoutedEventArgs e)
        {
            var expItems = Importer.ConvertExpTable();
        }

        private void BossTableConvertBtn_Click(Object sender, RoutedEventArgs e)
        {
            var bossItems = Importer.ConvertBossTable();
        }

        private void SkillTableConvertBtn_Click(Object sender, RoutedEventArgs e)
        {
            var skillItems = Importer.ConvertSkillTable();
        }

        private void PrismtunityMementoTableConvertBtn_Click(Object sender, RoutedEventArgs e)
        {
            var prismtunityMementoItems = Importer.ConvertPrismtunityMementoTable();
        }

        private void ResistanceTableConvertBtn_Click(Object sender, RoutedEventArgs e)
        {
            var resistanceItems = Importer.ConvertResistanceTable();
        }
        #endregion

        #region Mirage
        private void CreateMiragesBtn_Click(Object sender, RoutedEventArgs e)
        {
            var resistanceItems = Importer.ConvertResistanceTable();
            var prismtunityMementoItems = Importer.ConvertPrismtunityMementoTable();

            var names = resistanceItems.Select(r => r.Mirage).Union(prismtunityMementoItems.Select(pm => pm.Mirage));
            var mirages = names.Select(n => new Mirage(resistanceItems.SingleOrDefault(r => n == r.Mirage), prismtunityMementoItems.SingleOrDefault(pm => n == pm.Mirage))).ToList();

            var serializer = new XmlSerializer(typeof(List<Mirage>));
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, mirages);
                File.WriteAllText("Mirages.xml", stringWriter.ToString());
            }
        }

        private void LoadMiragesBtn_Click(Object sender, RoutedEventArgs e)
        {
            ReadMirages();
        }
        #endregion

        #region Boss
        private void CreateBossesBtn_Click(Object sender, RoutedEventArgs e)
        {
            var bossItems = Importer.ConvertBossTable();
            var bosses = bossItems.Select((b, i) => new Boss(b, i + 1)).ToList();

            var serializer = new XmlSerializer(typeof(List<Boss>));
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, bosses);
                File.WriteAllText("Bosses.xml", stringWriter.ToString());
            }
        }

        private void LoadBossesBtn_Click(Object sender, RoutedEventArgs e)
        {
            ReadBosses();
        }
        #endregion

        #endregion
    }
}
