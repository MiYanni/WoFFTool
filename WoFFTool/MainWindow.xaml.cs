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
            var serializer = new XmlSerializer(typeof(List<Mirage>));
            using (var reader = new StringReader(File.ReadAllText("Mirages.xml")))
            {
                Mirages = (List<Mirage>)serializer.Deserialize(reader);
            }
            UpdateMirageDataGrid(Mirages);
        }

        private void UpdateMirageDataGrid(List<Mirage> mirages)
        {
            MirageDataGrid.ItemsSource = null;
            MirageDataGrid.ItemsSource = mirages;
        }

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

        public List<Mirage> Mirages { get; set; }

        public List<Mirage> FilteredMirages { get; set; }

        private void LoadMiragesBtn_Click(Object sender, RoutedEventArgs e)
        {
            var serializer = new XmlSerializer(typeof(List<Mirage>));
            using(var reader = new StringReader(File.ReadAllText("Mirages.xml")))
            {
                Mirages = (List<Mirage>)serializer.Deserialize(reader);
                UpdateMirageDataGrid(Mirages);
            }
        }

        private void MenuItem_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void DigitsOnly_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            // TODO: Change to only 1 '-' at the start.
            var regex = new Regex("^-?[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void Compare_Click(Object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var buttonText = button.Content.ToString();
            button.Content = buttonText == "=" ? ">" : (buttonText == ">" ? "<" : "=");
            UpdateFilteredMirages_TextChanged(sender, null);
        }
    }
}
