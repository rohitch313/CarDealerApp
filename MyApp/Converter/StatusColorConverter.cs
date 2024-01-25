using System;
using System.Globalization;
using Microsoft.Maui.Graphics;
using MyApp.Model;

namespace MyApp.Converter
{
    internal class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is paymentStatus status)
            {
                switch (status)
                {
                    case paymentStatus.Pending:
                        return Color.Parse("#E27816");
                    case paymentStatus.Failed:
                        return Color.Parse("#DE3730");
                    case paymentStatus.Success:
                        return Color.Parse("#65B96D");
                    default:
                        return Color.Parse("White"); // Return a default color here
                }
            }

            return Color.Parse("White"); // Return a default color for other types or null
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
