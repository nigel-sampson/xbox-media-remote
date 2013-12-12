using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
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

        public MediaHubViewModel(WinRTContainer container)
        {
            this.container = container;

            Servers = new BindableCollection<MediaServerViewModel>();
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var localFolders = new[]
            {
                KnownFolders.MusicLibrary,
                KnownFolders.PicturesLibrary,
                KnownFolders.Playlists,
                KnownFolders.VideosLibrary
            };

            var localViewModels = localFolders.Select(f => new MediaServerViewModel(f, isLocal: true));

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();
            var serverViewModels = serverFolders.Select(f => new MediaServerViewModel(f, isLocal: false));

            Servers.AddRange(localViewModels);
            Servers.AddRange(serverViewModels);
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

        public async void OnSelectedServerChanged()
        {
            var folders = await SelectedServer.Folder.GetFoldersAsync();
            var files = await SelectedServer.Folder.GetFilesAsync();

            Debug.WriteLine("Folders");

            foreach (var folder in folders)
            {
                Debug.WriteLine("{0}: {1}, {2}", folder.DisplayName, folder.Path, folder.FolderRelativeId);
            }

            Debug.WriteLine("Files");

            foreach (var file in files)
            {
                Debug.WriteLine("{0}: {1}, {2}", file.DisplayName, file.Path, file.FolderRelativeId);
            }
        }

        public BindableCollection<MediaServerViewModel> Servers
        {
            get;
            private set;
        }
    }
}
