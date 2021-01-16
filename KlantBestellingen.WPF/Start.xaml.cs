using KlantBestellingen.WPF.Languages;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        private void BtnDutch_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-BE");
            Translations.Culture = new CultureInfo("nl-BE");
            StartOrderSystem();
        }

        private void BtnEnglish_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Translations.Culture = new CultureInfo("en-US");
            StartOrderSystem();
        }

        private void StartOrderSystem()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
