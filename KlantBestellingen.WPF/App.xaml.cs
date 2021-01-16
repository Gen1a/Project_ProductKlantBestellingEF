using KlantBestellingen.WPF.Languages;
using System.Windows;
using System.Globalization;
using System.Threading;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(Translations.UnexpectedErrorMessage
            + e.Exception.Message, Translations.UnexpectedError, MessageBoxButton.OK, MessageBoxImage.Warning);
            // We zeggen hier dat de exception door ons afgehandeld is
            e.Handled = true;
        }
    }
}
