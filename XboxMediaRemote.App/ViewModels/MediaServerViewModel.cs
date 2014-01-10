using System;
using Windows.Storage;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaServerViewModel
    {
        private readonly StorageFolder folder;
        private readonly bool isLocal;
        private readonly MediaType type;

        public MediaServerViewModel(StorageFolder folder, bool isLocal, MediaType type)
        {
            this.folder = folder;
            this.isLocal = isLocal;
            this.type = type;
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

        public MediaType Type
        {
            get
            {
                return type;
            }
        }
    }
}
