using System.Windows;

namespace WoFFTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExpTableConvertBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            var expItems = Importer.ConvertExpTable();
        }

        private void BossTableConvertBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            var bossItems = Importer.ConvertBossTable();
        }

        private void SkillTableConvertBtn_Click(System.Object sender, RoutedEventArgs e)
        {
            var skillItems = Importer.ConvertSkillTable();
        }
    }
}
