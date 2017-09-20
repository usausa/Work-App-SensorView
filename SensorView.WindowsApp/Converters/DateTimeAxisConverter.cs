namespace SensorView.WindowsApp.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using OxyPlot.Axes;

    public class DateTimeAxisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Axis.ToDouble(((DateTime?)value)?.AddSeconds(System.Convert.ToInt32(parameter, culture)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
