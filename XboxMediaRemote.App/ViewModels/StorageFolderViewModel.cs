using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class StorageFolderViewModel : StorageItemViewModel
    {
        private readonly StorageFolder folder;

        public StorageFolderViewModel(StorageFolder folder)
            : base(folder)
        {
            this.folder = folder;
        }

        public StorageFolder Folder
        {
            get
            {
                return folder;
            }
        }

        public override async Task LoadThumbnailAsync()
        {
            try
            {
                using (var thumbnail = await Folder.GetThumbnailAsync(ThumbnailMode.ListView, 150, ThumbnailOptions.UseCurrentScale))
                {
                    if (thumbnail == null)
                        return;

                    ThumbnailImage = new BitmapImage();
                    ThumbnailImage.SetSource(thumbnail);
                }
            }
            catch
            {
                Debug.WriteLine("Unable to load thumbnail for {0}", Folder.Name);
            }
        }
    }
}
