using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LightsOut.Converters {
    public class ToggleConverter :IValueConverter {
        private static readonly SolidColorBrush IsToggledBrush = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush IsNotToggledBrush = new SolidColorBrush(Colors.DarkGray);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                throw new ArgumentException("Value cannot be null", "value");
            var toggled = (bool) value;
            return toggled ? IsToggledBrush : IsNotToggledBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var brush = value as SolidColorBrush;
            if(brush == null)
                throw new ArgumentException("Value must be a non-null SolidColorBrush", "value");
            return brush == IsToggledBrush;
        }
    }
}
