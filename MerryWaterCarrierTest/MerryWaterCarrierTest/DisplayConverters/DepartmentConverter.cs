using MerryWaterCarrierTest.Models;
using System;
using System.Windows.Data;

namespace MerryWaterCarrierTest.DisplayConverters
{
    public class DepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as Department)?.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
