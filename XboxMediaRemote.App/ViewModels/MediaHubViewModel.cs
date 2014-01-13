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
        private INavigationService navigationService;

        public MediaHubViewModel(WinRTContainer container, IEventAggregator eventAggregator)
        {
            this.container = container;

            Servers = new BindableCollection<MediaServerViewModel>();

            PlayTo = new PlayToViewModel(eventAggregator);

            PlayTo.ConductWith(this);
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var localFolders = new[]
            {
                new { Folder = KnownFolders.MusicLibrary, Type = MediaType.Music },
                new { Folder = KnownFolders.PicturesLibrary, Type = MediaType.Picture },
                new { Folder = KnownFolders.VideosLibrary, Type = MediaType.Video }
            };

            var localViewModels = localFolders.Select(f => new MediaServerViewModel(f.Folder, isLocal: true, type: f.Type));

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();
            var serverViewModels = serverFolders.Select(f => new MediaServerViewModel(f, isLocal: false, type: MediaType.Unknown));

            Servers.AddRange(localViewModels);
            Servers.AddRange(serverViewModels);
        }

        public void SearchMedia(string query)
        {
            navigationService.UriFor<SearchResultsViewModel>()
                .WithParam(v => v.Query, query)
                .Navigate();
        }

        public void RegisterFrame(Frame frame)
        {
            container.RegisterNavigationService(frame);

            navigationService = container.GetInstance<INavigationService>();
        }

        public MediaServerViewModel SelectedServer
        {
            get;
            set;
        }

        public void OnSelectedServerChanged()
        {
            navigationService.NavigateToViewModel<BrowseFolderViewModel>(SelectedServer.Folder);
        }

        public BindableCollection<MediaServerViewModel> Servers
        {
            get;
            private set;
        }

        public PlayToViewModel PlayTo
        {
            get; private set;
        }
    }
}
