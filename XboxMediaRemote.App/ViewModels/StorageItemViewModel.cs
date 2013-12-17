using System;
using Windows.Storage;
using Caliburn.Micro;

namespace XboxMediaRemote.App.ViewModels
{
    public class StorageItemViewModel : PropertyChangedBase
    {
        private readonly IStorageItem item;

        public StorageItemViewModel(IStorageItem item)
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
    }
}
