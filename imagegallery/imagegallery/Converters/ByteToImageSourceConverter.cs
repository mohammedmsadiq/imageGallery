using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace imagegallery.Converters
{
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imgByte)
            {
                return ImageSource.FromStream(() => new MemoryStream(imgByte));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}