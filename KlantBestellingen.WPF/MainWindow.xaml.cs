using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Klanten _customerWindow = new Klanten();
        private Producten _productsWindow = new Producten();

        public MainWindow()
        {
            InitializeComponent();
            // Subscribe to Closing event
            Closing += MainWindow_Closing;
            _customerWindow.Closing += _Window_Closing;
            _productsWindow.Closing += _Window_Closing;
        }

        /// <summary>
        /// Interferes the closing of a window. Hides it in the background instead of completely closing it (more efficient).
        /// This can be used for windows which only have 1 single 'view'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Make sure the event gets handled on the UI-thread
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate (object o)
            {
                ((Window)sender).Hide();
                return null;
            }, null);
            // Cancel the initial closing event
            e.Cancel = true;
        }

        /// <summary>
        /// Close the application when the Main window gets closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Opens the 'Klanten' window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Klanten_Click(object sender, RoutedEventArgs e)
        {
            if (_customerWindow != null)
                _customerWindow.Show();
        }

        /// <summary>
        /// Opens the 'Producten' window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Producten_Click(object sender, RoutedEventArgs e)
        {
            if (_productsWindow != null)
                _productsWindow.Show();
        }

        /// <summary>
        /// Closes the application when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Sluiten_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
