using System;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
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

            var localFolders = new []
            {
                KnownFolders.PicturesLibrary,
                KnownFolders.Playlists,
                KnownFolders.MusicLibrary,
                KnownFolders.VideosLibrary
            };

            var localViewModels = localFolders.Select(f => new MediaServerListViewModel(f, isLocal: true));

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();
            var serverViewModels = serverFolders.Select(f => new MediaServerListViewModel(f));

            Servers.AddRange(localViewModels);
            Servers.AddRange(serverViewModels);
        }

        public void RegisterFrame(Frame frame)
        {
            container.RegisterNavigationService(frame);
        }

        public MediaServerListViewModel SelectedServer
        {
            get;
            set;
        }

        public BindableCollection<MediaServerListViewModel> Servers
        {
            get;
            private set;
        }
    }
}
