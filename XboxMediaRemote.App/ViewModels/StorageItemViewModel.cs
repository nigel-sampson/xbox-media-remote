using System;
using System.Threading.Tasks;
using Windows.Storage;
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

        public abstract Task LoadThumbnailAsync();
    }
}
