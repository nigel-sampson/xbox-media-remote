using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using XboxMediaRemote.Core.Extensions;

namespace XboxMediaRemote.Core.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public VisibilityConverter()
        {
            SupportIsNullOrEmpty = true;
        }

        public bool Inverse
        {
            get;
            set;
        }

        public bool SupportIsNullOrEmpty
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible;

            if (value is string && SupportIsNullOrEmpty)
            {
                visible = !String.IsNullOrEmpty(value.ToString());
            }
            else
            {
                var defaultValue = value != null ? value.GetType().GetDefaultValue() : null;

                visible = !Equals(value, defaultValue);
            }

            if (Inverse)
                visible = !visible;

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
