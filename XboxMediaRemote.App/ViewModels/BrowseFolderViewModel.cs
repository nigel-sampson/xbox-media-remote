using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.PlayTo;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using PropertyChanged;
using XboxMediaRemote.Core.Extensions;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class BrowseFolderViewModel : PageViewModelBase
    {
        private readonly INavigationService navigationService;
        private PlayToManager playToManager;

        public BrowseFolderViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.navigationService = navigationService;

            GroupedStorageItems = new BindableCollection<StorageItemGroupViewModel>();
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            using (Loading())
            {
                var folders = await Parameter.GetFoldersAsync();
                var files = await Parameter.GetFilesAsync();

                var folderViewModels = folders.Select(f => new StorageFolderViewModel(f)).ToList();
                var fileViewModels = files.Select(f => new StorageFileViewModel(f)).ToList();

                var itemViewModels = folderViewModels.Concat<StorageItemViewModel>(fileViewModels);

                var groups = AlphaNumericGroups.GetExpressions()
                    .Select(e => new StorageItemGroupViewModel(e.Key, itemViewModels.Where(i => e.Value.IsMatch(i.Name))))
                    .Where(g => g.Items.Any());

                GroupedStorageItems.AddRange(groups);

                await folderViewModels.WhenAllAsync(f => f.LoadThumbnailAsync());
                await fileViewModels.WhenAllAsync(f => f.LoadThumbnailAsync());
            }

            playToManager = PlayToManager.GetForCurrentView();
            
            playToManager.DefaultSourceSelection = false;
            playToManager.SourceRequested += OnPlayToSourceRequested;
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            playToManager.SourceRequested -= OnPlayToSourceRequested;
        }

        private void OnPlayToSourceRequested(PlayToManager sender, PlayToSourceRequestedEventArgs args)
        {
            var deferral = args.SourceRequest.GetDeferral();

            Execute.OnUIThread(async () =>
            {
                var mediaElement = new MediaElement();

                var stream = await latestFile.File.OpenReadAsync();

                mediaElement.SetSource(stream, latestFile.File.ContentType);

                args.SourceRequest.SetSource(mediaElement.PlayToSource);

                deferral.Complete();    
            });
        }

        private StorageFileViewModel latestFile;

        public void SelectItem(ItemClickEventArgs e)
        {
            var itemViewModel = (StorageItemViewModel) e.ClickedItem;

            var folderViewModle = itemViewModel as StorageFolderViewModel;

            if (folderViewModle != null)
            {
                navigationService.NavigateToViewModel<BrowseFolderViewModel>(folderViewModle.Folder);
            }
            else
            {
                latestFile = (StorageFileViewModel) itemViewModel;

                PlayToManager.ShowPlayToUI();    
            }
        }

        public StorageFolder Parameter
        {
            get;
            set;
        }

        public BindableCollection<StorageItemGroupViewModel> GroupedStorageItems
        {
            get; 
            private set; 
        }
    }
}
