using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Converter
{
    internal class ProcurementStatusConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProcurementStatus status)
            {
                switch (status)
                {
                    case ProcurementStatus.Pending:
                        return Color.Parse("#E27816");
                    case ProcurementStatus.Failed:
                        return Color.Parse("#DE3730");
                    case ProcurementStatus.Successful:
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
