using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XboxMediaRemote.App.ViewModels;

namespace XboxMediaRemote.App.Resources.TemplateSelectors
{
    public class TypeIconTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            var type = (MediaType)item;

            switch (type)
            {
                case MediaType.Music:
                    return Music;
                case MediaType.Picture:
                    return Pictures;
                case MediaType.Video:
                    return Video;
                default:
                    return Unknown;
            }
        }

        public DataTemplate Unknown
        {
            get; set;
        }

        public DataTemplate Music
        {
            get;
            set;
        }

        public DataTemplate Pictures
        {
            get;
            set;
        }

        public DataTemplate Video
        {
            get;
            set;
        }
    }
}
