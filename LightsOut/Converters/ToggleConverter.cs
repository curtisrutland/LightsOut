using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LightsOut.Converters {
    public class ToggleConverter : IValueConverter {
        private static readonly Brush NotToggledBrush = (RadialGradientBrush)Application.Current.Resources["NotToggledBrush"];
        private static readonly Brush ToggledBrush = (RadialGradientBrush)Application.Current.Resources["ToggledBrush"];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                throw new ArgumentException("Value cannot be null", "value");
            var toggled = (bool)value;
            return toggled ? ToggledBrush : NotToggledBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var brush = value as SolidColorBrush;
            if (brush == null)
                throw new ArgumentException("Value must be a non-null SolidColorBrush", "value");
            return brush == ToggledBrush;
        }
    }
}
