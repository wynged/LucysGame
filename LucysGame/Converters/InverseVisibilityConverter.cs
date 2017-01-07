using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LucysGame.Converters
{
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    class InverseVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(System.Windows.Visibility))
                throw new InvalidOperationException("The target must be visibility");
            if ((bool)value)
            {
                return System.Windows.Visibility.Hidden;
            }
            else
            {
                return System.Windows.Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
