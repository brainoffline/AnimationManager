using System;
using System.Globalization;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#endif

#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
#endif

namespace Brain.Animate
{
    public class ValueConverter<T> : IValueConverter
    {
        public T HasValue { get; set; }
        public T NoValue { get; set; }

#if NETFX_CORE
        public object Convert(object value, Type targetType, object parameter, string language)
#else
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#endif
        {
            var b = value as bool?;
            if (b.HasValue)
            {
                return b.Value ? HasValue : NoValue;
            }
            var str = value as string;
            if (str != null)
                return !string.IsNullOrWhiteSpace(str) ? HasValue : NoValue;

            return value != null ? HasValue : NoValue;
        }

#if NETFX_CORE
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#elif WINDOWS_PHONE
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
#endif
        {
            throw new NotImplementedException();
        }
    }

    public class ValueToVisibility : ValueConverter<Visibility>
    {
        public ValueToVisibility()
        {
            HasValue = Visibility.Visible;
            NoValue = Visibility.Collapsed;
        }
    }

    public class InverseValueToVisibility : ValueConverter<Visibility>
    {
        public InverseValueToVisibility()
        {
            HasValue = Visibility.Collapsed;
            NoValue = Visibility.Visible;
        }
    }

    public class ValueToBrush : ValueConverter<Brush> { }
    public class ValueToStringConverter : ValueConverter<string> { }
    public class ValueToIntConverter : ValueConverter<int> { }
    public class ValueToDoubleConverter : ValueConverter<double> { }

}
