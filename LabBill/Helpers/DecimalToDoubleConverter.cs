using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace LabBill.Helpers;
public class DecimalToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal price)
        {
            return ((double)price);
        }
        return 0.0d;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is double d)
        {
            return (decimal)d;
        }
        return 0.0m;
    }
}
