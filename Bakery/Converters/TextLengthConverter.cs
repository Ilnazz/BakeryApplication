using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Bakery.Converters
{
    public class TextLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text == false)
                return null;

            if (parameter == null || int.TryParse(parameter.ToString(), out var textLength) == false)
                return null;

            var textWithoutNewLines = Regex.Replace(text, "\n", "");

            if (textWithoutNewLines.Length < textLength)
                return textWithoutNewLines;

            return textWithoutNewLines.Substring(0, textLength) + "...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
