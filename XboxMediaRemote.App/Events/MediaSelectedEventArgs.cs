using System;
using XboxMediaRemote.App.ViewModels;

namespace XboxMediaRemote.App.Events
{
    public class MediaSelectedEventArgs : EventArgs
    {
        private readonly StorageFileViewModel storageFile;

        public MediaSelectedEventArgs(StorageFileViewModel storageFile)
        {
            this.storageFile = storageFile;
        }

        public StorageFileViewModel StorageFile
        {
            get
            {
                return storageFile;
            }
        }
    }
}
