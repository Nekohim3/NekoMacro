using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using NekoMacro.MacrosBase;

namespace NekoMacro.UI
{
    [ValueConversion(typeof(CommandType), typeof(string))]
    public class CommandTypeToStringConverter : IValueConverter
    {
        public object Convert(object                           value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null) ? value.ToString() : "";
        }

        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolToReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class NullableBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null && ((bool?)value) == true) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class NullableBoolToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null && ((bool?)value) == false) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class StringNotEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null && (string)value != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #region Enum
    [ValueConversion(typeof(System.Enum), typeof(Visibility))]
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object                           value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return new BoolToVisibilityConverter().Convert(value?.Equals(parameter) ?? false, null, null, culture);
        }

        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(System.Enum), typeof(Visibility))]
    public class EnumToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object                           value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return new BoolToVisibilityReverseConverter().Convert(value?.Equals(parameter) ?? false, null, null, culture);
        }

        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Object (проверка на NULL)
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ObjIsNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object                           value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ObjIsNullToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object                           value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return (value != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
