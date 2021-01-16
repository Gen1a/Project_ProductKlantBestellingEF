using System;
using KlantBestellingen.WPF.Languages;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KlantBestellingen.WPF.ValueConverters
{
    public class PaidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string output = Translations.No;
            if ((bool)value)
                output = Translations.Yes;

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
