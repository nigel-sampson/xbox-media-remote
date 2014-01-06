using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Caliburn.Micro;

namespace XboxMediaRemote.App.ViewModels
{
    public abstract class StorageItemViewModel : PropertyChangedBase
    {
        private readonly IStorageItem item;

        protected StorageItemViewModel(IStorageItem item)
        {
            this.item = item;
        }

        public IStorageItem Item
        {
            get
            {
                return item;
            }
        }

        public string Name
        {
            get
            {
                return item.Name;
            }
        }

        public BitmapImage ThumbnailImage
        {
            get;
            set;
        }

        public bool HasThumbnailImage
        {
            get
            {
                return ThumbnailImage != null;
            }
        }

        public abstract Task LoadThumbnailAsync();
    }
}
