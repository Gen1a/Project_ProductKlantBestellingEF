using BusinessLayer.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KlantBestellingen.WPF
{
    /// <summary>
    /// Interaction logic for Klanten.xaml
    /// </summary>
    public partial class Klanten : Window
    {
        private ObservableCollection<Klant> _klanten;

        public Klanten()
        {
            InitializeComponent();
            _klanten = new ObservableCollection<Klant>(Context.KlantManager.GeefKlanten());
            // Assign collection to datagrid
            dgKlanten.ItemsSource = _klanten;
            // Fire event when ObservableCollection changes
            _klanten.CollectionChanged += _klanten_CollectionChanged;
        }

        /// <summary>
        /// Notify BusinessLayer that a record has been deleted from the ObservableCollection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _klanten_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Klant klant in e.OldItems)
                {
                    Context.KlantManager.VerwijderKlant(klant);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Klant klant in e.NewItems)
                {
                    Context.KlantManager.VoegKlantToe(/*KlantFactory.MaakKlant(klant.Naam, klant.Adres, Context.IdFactory)*/ klant);
                }
            }
        }

        /// <summary>
        /// Handles pressing the 'Delete' key on keyboard when Klant selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgKlanten_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var grid = (DataGrid)sender;
            if (Key.Delete == e.Key)
            {
                if (!(MessageBox.Show("Zeker dat je de klant wenst te verwijderen?", "Bevestigen", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    // Cancel delete and return
                    e.Handled = true;
                    return;
                }

                // We moeten een while gebruiken en telkens testen want met foreach treden problemen op omdat de verzameling intussen telkens wijzigt!
                while (grid.SelectedItems.Count > 0)
                {
                    var row = grid.SelectedItems[0];
                    _klanten.Remove(row as Klant);
                }
            }
        }

        /// <summary>
        /// Handles clicking on the 'Voeg toe' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNieuweKlant_Click(object sender, RoutedEventArgs e)
        {
            // Validate data
            if (string.IsNullOrEmpty(TbKlantNaam?.Text) || string.IsNullOrEmpty(TbKlantAdres?.Text))
            {
                MessageBox.Show("Geef alle klantgegevens op!");
                return;
            }

            // Add new Klant to ObservableCollection
            // _klanten_CollectionChanged makes sure the changes also get pushed to BusinessLayer
            Klant klant = new Klant(TbKlantNaam.Text, TbKlantAdres.Text);
            _klanten.Add(klant);

            // Empty TextBoxes after adding new Klant
            TbKlantNaam.Text = "";
            TbKlantAdres.Text = "";
            BtnNieuweKlant.IsEnabled = false;
        }
        
        /// <summary>
        /// Handles input (KeyUp) in the TextBox fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_KeyUp(object sender, KeyEventArgs e)
        {
            // If input not empty enable Voeg Toe button
            if (!string.IsNullOrEmpty(TbKlantNaam.Text) && !string.IsNullOrEmpty(TbKlantAdres.Text))
            {
                BtnNieuweKlant.IsEnabled = true;
            }
            else
            {
                BtnNieuweKlant.IsEnabled = false;
            }
        }
    }
}
