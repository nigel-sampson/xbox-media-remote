using System;
using Windows.Storage;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class BrowseMediaFolderViewModel : PageViewModelBase
    {
        public BrowseMediaFolderViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var folders = await Parameter.GetFoldersAsync();
            var files = await Parameter.GetFilesAsync();
        }

        public StorageFolder Parameter
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }
    }
}
