using System;
using Windows.Storage;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaServerListViewModel
    {
        private readonly StorageFolder folder;
        private readonly bool isLocal;

        public MediaServerListViewModel(StorageFolder folder, bool isLocal = false)
        {
            this.folder = folder;
            this.isLocal = isLocal;
        }

        public string Name
        {
            get
            {
                return folder.DisplayName;
            }
        }

        public StorageFolder Folder
        {
            get
            {
                return folder;
            }
        }

        public bool IsLocal
        {
            get
            {
                return isLocal;
            }
        }
    }
}
