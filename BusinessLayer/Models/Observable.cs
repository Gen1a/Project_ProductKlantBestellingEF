using System.ComponentModel;

namespace BusinessLayer.Models
{
    /// <summary>
    /// Handles notifying an instance of a class about a change to a proeprty of an existing object.
    /// eg. Changing an existing Klant's name from 'Jan' to 'Piet'.
    /// </summary>
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
