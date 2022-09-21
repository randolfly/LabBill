using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;

namespace LabBill.Helpers;
public class DateTimeToDateTimeOffsetConverter : IValueConverter
{
    /// <summary>
    /// 布尔值转换为可见性
    /// </summary>

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            return (DateTimeOffset)dateTime;
        }
        return (DateTimeOffset)DateTime.Now;

    }


    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {

        if (value is DateTimeOffset dateTimeOffset)
        {
            DateTime dateTime = dateTimeOffset.DateTime;
            return dateTime;
        }
        return DateTime.Now;
    }
}
