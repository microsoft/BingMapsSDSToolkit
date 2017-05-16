using System;
using System.Windows;
using System.Windows.Data;

namespace BingSDSTestApp.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; } 

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                bool? val = null;
                if (value is bool)
                {
                    val = (bool)value;
                }
                else if (value is string)
                {
                    val = !string.IsNullOrWhiteSpace(value as string);
                }
                else if (value is int)
                {
                    val = true;
                }
                
                if (IsReversed && val.HasValue)
                {
                    val = !val.Value;
                }

                if (val.HasValue)
                {
                    return (val.Value)? Visibility.Visible : Visibility.Collapsed;
                }

                return (IsReversed)? Visibility.Collapsed: Visibility.Visible;
            }

            return (IsReversed)? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
