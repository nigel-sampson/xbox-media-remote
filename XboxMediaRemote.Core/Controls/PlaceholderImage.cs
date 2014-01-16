using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace XboxMediaRemote.Core.Controls
{
    [TemplateVisualState(GroupName = ImageStates, Name = ImageLoaded)]
    [TemplateVisualState(GroupName = ImageStates, Name = ImageUnloaded)]
    public class PlaceholderImage : ContentControl
    {
        private bool? _showImage;

        private const string ImageStates = "ImageStates";
        private const string ImageLoaded = "ImageLoaded";
        private const string ImageUnloaded = "ImageUnloaded";

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof (ImageSource), typeof (PlaceholderImage), new PropertyMetadata(null, OnSourceChanged));

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof (Stretch), typeof (PlaceholderImage), new PropertyMetadata(Stretch.UniformToFill));

        public PlaceholderImage()
        {
            DefaultStyleKey = typeof (PlaceholderImage);
        }

        public ImageSource Source
        {
            get { return (ImageSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Stretch Stretch
        {
            get { return (Stretch) GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var showImage = _showImage ?? false;

            if (showImage)
                ShowImage();
            else
                HideImage();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var placeholder = d as PlaceholderImage;

            if (placeholder == null)
                return;

            var oldValue = e.OldValue as ImageSource;
            var newValue = e.NewValue as ImageSource;

            placeholder.OnSourceChanged(oldValue, newValue);
        }

        private void OnSourceChanged(ImageSource oldValue, ImageSource newValue)
        {
            var oldBitmapValue = oldValue as BitmapImage;
            var newBitmapValue = newValue as BitmapImage;

            if (oldBitmapValue != null)
            {
                oldBitmapValue.ImageOpened -= OnBitmapImageLoaded;
            }

            if (newBitmapValue != null)
            {
                if (newBitmapValue.PixelWidth > 0 && newBitmapValue.PixelHeight > 0)
                {
                    ShowImage();
                }
                else
                {
                    HideImage();        
                }

                newBitmapValue.ImageOpened += OnBitmapImageLoaded;
            }
            else if (newValue != null)
            {
                ShowImage();
            }
            else
            {
                ShowImage();
            }
        }

        private void ShowImage()
        {
            _showImage = true;
            VisualStateManager.GoToState(this, ImageLoaded, true);
        }

        private void HideImage()
        {
            _showImage = false;
            VisualStateManager.GoToState(this, ImageUnloaded, true);
        }

        private void OnBitmapImageLoaded(object sender, RoutedEventArgs e)
        {
            ShowImage();
        }
    }
}
