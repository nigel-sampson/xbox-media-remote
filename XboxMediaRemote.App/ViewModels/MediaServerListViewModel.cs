using System;
using Windows.Storage;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaServerListViewModel
    {
        private readonly StorageFolder folder;

        public MediaServerListViewModel(StorageFolder folder)
        {
            this.folder = folder;
        }

        public string Name
        {
            get
            {
                return folder.DisplayName;
            }
        }
    }
}
