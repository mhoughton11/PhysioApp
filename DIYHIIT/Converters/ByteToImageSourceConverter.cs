using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace DIYHIIT.Converters
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object pobjValue, Type pobjTargetType, object pobjParameter, System.Globalization.CultureInfo pobjCulture)
        {
            ImageSource objImageSource;
            //
            if (pobjValue != null)
            {
                byte[] bytImageData = (byte[])pobjValue;
                objImageSource = ImageSource.FromStream(() => new MemoryStream(bytImageData));
            }
            else
            {
                objImageSource = null;
            }
            //
            return objImageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
