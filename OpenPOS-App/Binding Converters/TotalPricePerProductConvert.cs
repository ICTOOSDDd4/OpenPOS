using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_APP.Models;

namespace OpenPOS_APP
{
    public class TotalPriceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var amount = ((CheckoutOverview.GetCheckoutItems()[(Product)value]) * ((Product)value).Price).ToString("C", CultureInfo.CreateSpecificCulture("nl-NL")); ;
                return amount;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}