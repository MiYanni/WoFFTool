using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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

        private void MirageNameFilterTxtBox_TextChanged(Object sender, TextChangedEventArgs e)
        {
            var searchText = MirageNameFilterTxtBox.Text;
            FilteredMirages = String.IsNullOrEmpty(searchText) ? Mirages : Mirages.Where(m => m.Name.ToLower().StartsWith(searchText.ToLower())).ToList();
            UpdateMirageDataGrid(FilteredMirages);
        }
    }
}
