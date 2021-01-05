using KlantBestellingen.WPF.Languages;
using System.Windows;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            // Initialize App with dummy data
            Context.Populate();
            //
            //Translations.Culture = new System.Globalization.CultureInfo("nl-BE");
            Translations.Culture = new System.Globalization.CultureInfo("en-US");
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Er is een onverwachte fout opgetreden: "
            + e.Exception.Message, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Warning);
            // We zeggen hier dat de exception door ons afgehandeld is
            e.Handled = true;

            //MessageBox.Show("An unhandled exception just occurred: "
            //+ e.Exception.Message, Translations.ExceptionRaised, MessageBoxButton.OK, MessageBoxImage.Warning);
            //// We zeggen hier dat de exception door ons afgehandeld is
            //e.Handled = true;
        }
    }
}
