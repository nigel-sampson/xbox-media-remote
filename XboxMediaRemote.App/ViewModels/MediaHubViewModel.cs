using System;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;

namespace XboxMediaRemote.App.ViewModels
{
    public class MediaHubViewModel : ViewModelBase
    {
        private readonly WinRTContainer container;

        public MediaHubViewModel(WinRTContainer container)
        {
            this.container = container;

            Servers = new BindableCollection<MediaServerListViewModel>();
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();
            var serverViewModels = serverFolders.Select(f => new MediaServerListViewModel(f));

            Servers.AddRange(serverViewModels);
        }

        public void RegisterFrame(Frame frame)
        {
            container.RegisterNavigationService(frame);
        }

        public BindableCollection<MediaServerListViewModel> Servers
        {
            get;
            private set;
        }
    }
}
