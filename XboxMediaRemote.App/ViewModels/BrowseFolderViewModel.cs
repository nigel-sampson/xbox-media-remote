using System;
using System.Linq;
using Windows.Storage;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class BrowseFolderViewModel : StorageListPageViewModelBase
    {
        public BrowseFolderViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService, eventAggregator)
        {

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
            }

            await LoadThumbnailsAsync();
        }

        public StorageFolder Parameter
        {
            get;
            set;
        }
    }
}
