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
    }
}
