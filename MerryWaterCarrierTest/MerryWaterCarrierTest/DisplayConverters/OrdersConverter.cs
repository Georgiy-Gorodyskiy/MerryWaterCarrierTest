using MerryWaterCarrierTest.Models;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace MerryWaterCarrierTest.DisplayConverters
{
    public class OrdersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            var orders = (value as List<Order>);

            var ret = "";
            foreach (var order in orders)
            {
                ret += order.Name + " ";
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
