﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Hangman.Official.Converters
{
   
    public class CharacterToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
