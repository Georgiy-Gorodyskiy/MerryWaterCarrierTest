using MerryWaterCarrierTest.Models;
using System;
using System.Windows.Data;

namespace MerryWaterCarrierTest.DisplayConverters
{
    internal class EmployeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as Employee)?.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}