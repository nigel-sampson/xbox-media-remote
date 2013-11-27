using System;
using System.Linq;
using Windows.Storage;
using Caliburn.Micro;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaHubViewModel : ViewModelBase
    {
        public MediaHubViewModel()
        {
            Servers = new BindableCollection<MediaServerListViewModel>();    
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();
            var serverViewModels = serverFolders.Select(f => new MediaServerListViewModel(f));

            Servers.AddRange(serverViewModels);
        }

        public BindableCollection<MediaServerListViewModel> Servers
        {
            get;
            private set;
        }
    }
}
