using System;
using Windows.Storage;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaServerViewModel
    {
        private readonly StorageFolder folder;
        private readonly bool isLocal;

        public MediaServerViewModel(StorageFolder folder, bool isLocal)
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
