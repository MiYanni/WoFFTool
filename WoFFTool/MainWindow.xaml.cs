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

        private void UpdateFilteredMirages_TextChanged(Object sender, TextChangedEventArgs e)
        {
            FilteredMirages = Mirages.Where(m =>
                (String.IsNullOrEmpty(MirageNameFilterTxtBox.Text) || m.Name.ToLower().StartsWith(MirageNameFilterTxtBox.Text.ToLower())) &&
                (String.IsNullOrEmpty(MirageSizeFilterTxtBox.Text) || m.Size.GetName().ToLower().StartsWith(MirageSizeFilterTxtBox.Text.ToLower())) &&
                ((String.IsNullOrEmpty(MirageWeightFilterTxtBox.Text) || MirageWeightFilterTxtBox.Text.Equals("-")) || m.Weight == Convert.ToInt32(MirageWeightFilterTxtBox.Text)) &&

                ((String.IsNullOrEmpty(MirageFireFilterTxtBox.Text) || MirageFireFilterTxtBox.Text.Equals("-")) || m.Elemental.Fire == Convert.ToInt32(MirageFireFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageIceFilterTxtBox.Text) || MirageIceFilterTxtBox.Text.Equals("-")) || m.Elemental.Ice == Convert.ToInt32(MirageIceFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageLightningFilterTxtBox.Text) || MirageLightningFilterTxtBox.Text.Equals("-")) || m.Elemental.Lightning == Convert.ToInt32(MirageLightningFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageAeroFilterTxtBox.Text) || MirageAeroFilterTxtBox.Text.Equals("-")) || m.Elemental.Aero == Convert.ToInt32(MirageAeroFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageWaterFilterTxtBox.Text) || MirageWaterFilterTxtBox.Text.Equals("-")) || m.Elemental.Water == Convert.ToInt32(MirageWaterFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageEarthFilterTxtBox.Text) || MirageEarthFilterTxtBox.Text.Equals("-")) || m.Elemental.Earth == Convert.ToInt32(MirageEarthFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageLightFilterTxtBox.Text) || MirageLightFilterTxtBox.Text.Equals("-")) || m.Elemental.Light == Convert.ToInt32(MirageLightFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageDarkFilterTxtBox.Text) || MirageDarkFilterTxtBox.Text.Equals("-")) || m.Elemental.Dark == Convert.ToInt32(MirageDarkFilterTxtBox.Text)) &&

                ((String.IsNullOrEmpty(MiragePoisonFilterTxtBox.Text) || MiragePoisonFilterTxtBox.Text.Equals("-")) || m.Ailment.Poison == Convert.ToInt32(MiragePoisonFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageConfuseFilterTxtBox.Text) || MirageConfuseFilterTxtBox.Text.Equals("-")) || m.Ailment.Confuse == Convert.ToInt32(MirageConfuseFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageSleepFilterTxtBox.Text) || MirageSleepFilterTxtBox.Text.Equals("-")) || m.Ailment.Sleep == Convert.ToInt32(MirageSleepFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageBlindFilterTxtBox.Text) || MirageBlindFilterTxtBox.Text.Equals("-")) || m.Ailment.Blind == Convert.ToInt32(MirageBlindFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageOblivionFilterTxtBox.Text) || MirageOblivionFilterTxtBox.Text.Equals("-")) || m.Ailment.Oblivion == Convert.ToInt32(MirageOblivionFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageBerserkFilterTxtBox.Text) || MirageBerserkFilterTxtBox.Text.Equals("-")) || m.Ailment.Berserk == Convert.ToInt32(MirageBerserkFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageSlowFilterTxtBox.Text) || MirageSlowFilterTxtBox.Text.Equals("-")) || m.Ailment.Slow == Convert.ToInt32(MirageSlowFilterTxtBox.Text)) &&
                ((String.IsNullOrEmpty(MirageDoomFilterTxtBox.Text) || MirageDoomFilterTxtBox.Text.Equals("-")) || m.Ailment.Doom == Convert.ToInt32(MirageDoomFilterTxtBox.Text)) &&

                (String.IsNullOrEmpty(MiragePrismtunityFilterTxtBox.Text) || m.Name.ToLower().StartsWith(MiragePrismtunityFilterTxtBox.Text.ToLower())) &&
                (String.IsNullOrEmpty(MirageMementoLocationFilterTxtBox.Text) || m.Name.ToLower().StartsWith(MirageMementoLocationFilterTxtBox.Text.ToLower()))
            ).ToList();
            UpdateMirageDataGrid(FilteredMirages);
        }

        private void DigitsOnly_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("^-?[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
