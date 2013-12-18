using System;
using System.Collections.Generic;
using System.Linq;

namespace XboxMediaRemote.App.ViewModels
{
    public class StorageItemGroupViewModel : GroupViewModel<StorageItemViewModel>
    {
        private readonly string description;

        public StorageItemGroupViewModel(string title, IEnumerable<StorageItemViewModel> items) 
            : base(title, items)
        {
            var itemsList = items.ToList();

            var folderCount = itemsList.OfType<StorageFolderViewModel>().Count();
            var fileCount = itemsList.OfType<StorageFileViewModel>().Count();

            description = String.Format("{0} folders, {1} files", folderCount, fileCount);
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}
