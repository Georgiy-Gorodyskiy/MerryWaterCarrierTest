using MerryWaterCarrierTest.Models;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace MerryWaterCarrierTest.DisplayConverters
{
    public class TagsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value == null)
                return string.Empty;
            var tags = (value as List<Tag>);

            var ret = "";
            foreach (var tag in tags)
            {
                ret += tag.Name + " ";
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
