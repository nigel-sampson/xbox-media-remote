using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using PropertyChanged;
using XboxMediaRemote.App.Resources;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class StorageFileViewModel : StorageItemViewModel
    {
        private readonly StorageFile file;

        public StorageFileViewModel(StorageFile file)
            : base(file)
        {
            this.file = file;

            var contentType = file.ContentType;

            if (contentType.StartsWith("image/"))
                MediaType = MediaType.Picture;
            else if (contentType.StartsWith("video/"))
                MediaType = MediaType.Video;
            else if (contentType.StartsWith("audio/"))
                MediaType = MediaType.Music;
            else
                MediaType = MediaType.Unknown;
        }

        public StorageFile File
        {
            get
            {
                return file;
            }
        }

        public MediaType MediaType
        {
            get; set;
        }

        public string Description
        {
            get
            {
                switch (MediaType)
                {
                    case MediaType.Picture:
                        return Strings.MediaTypeImage;
                    case MediaType.Video:
                        return Strings.MediaTypeVideo;
                    default:
                        return String.Empty;
                }
            }
        }

        public override async Task LoadThumbnailAsync()
        {
            try
            {
                using (var thumbnail = await File.GetThumbnailAsync(ThumbnailMode.ListView, 150, ThumbnailOptions.UseCurrentScale))
                {
                    if (thumbnail == null)
                        return;

                    ThumbnailImage = new BitmapImage();
                    ThumbnailImage.SetSource(thumbnail);
                }
            }
            catch
            {
                Debug.WriteLine("Unable to load thumbnail for {0}", File.Name);
            }
            
        }
    }
}
