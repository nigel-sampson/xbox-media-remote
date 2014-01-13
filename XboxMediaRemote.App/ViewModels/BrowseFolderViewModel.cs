using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Caliburn.Micro;
using PropertyChanged;
using XboxMediaRemote.App.Resources;
using XboxMediaRemote.App.Services;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class BrowseFolderViewModel : StorageListPageViewModelBase
    {
        private readonly IApplicationSettingsService settingsService;

        public BrowseFolderViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IApplicationSettingsService settingsService)
            : base(navigationService, eventAggregator)
        {
            this.settingsService = settingsService;

            SortOptions = new BindableCollection<MediaSortViewModel>
            {
                new MediaSortViewModel(MediaSort.Alphabetical, Strings.MediaSortAlphabetical),
                new MediaSortViewModel(MediaSort.Type, Strings.MediaSortType),
                new MediaSortViewModel(MediaSort.DateCreated, Strings.MediaSortDateCreated)
            };

            SelectedSortOption = SortOptions[0];
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            await BindFilesAndFoldersAsync();
        }

        private async Task BindFilesAndFoldersAsync()
        {
            using (Loading())
            {
                SelectedSortOption = SortOptions.Single(s => s.Sort == settingsService.Sort);

                var folders = await Parameter.GetFoldersAsync();
                var files = await Parameter.GetFilesAsync();

                var folderViewModels = folders.Select(f => new StorageFolderViewModel(f)).ToList();
                var fileViewModels = files.Select(f => new StorageFileViewModel(f)).ToList();

                var itemViewModels = folderViewModels.Concat<StorageItemViewModel>(fileViewModels);

                var groups = GroupItems(itemViewModels);

                GroupedStorageItems.AddRange(groups);
            }

            await LoadThumbnailsAsync();
        }

        private IEnumerable<StorageItemGroupViewModel> GroupItems(IEnumerable<StorageItemViewModel> itemViewModels)
        {
            switch (SelectedSortOption.Sort)
            {
                case MediaSort.Alphabetical:
                    return AlphaNumericGroups.GetExpressions()
                        .Select(e => new StorageItemGroupViewModel(e.Key, itemViewModels.Where(i => e.Value.IsMatch(i.Name))))
                        .Where(g => g.Items.Any());
                case MediaSort.Type:
                    return itemViewModels.GroupBy(GetItemTypeGroup)
                        .Select(g => new StorageItemGroupViewModel(g.Key, g));
                case MediaSort.DateCreated:
                    return itemViewModels.GroupBy(i => i.Item.DateCreated.Date)
                        .Select(g => new StorageItemGroupViewModel(g.Key.ToString("D"), g));
                default:
                    throw new InvalidOperationException("Invalid Media Sort");
            }
        }

        private string GetItemTypeGroup(StorageItemViewModel item)
        {
            var folderViewModel = item as StorageFolderViewModel;

            if (folderViewModel != null)
                return Strings.GroupFolders;

            var fileViewModel = item as StorageFileViewModel;

            if (fileViewModel == null)
                return Strings.GroupOther;

            switch (fileViewModel.MediaType)
            {
                case MediaType.Music:
                    return Strings.GroupMusic;
                case MediaType.Picture:
                    return Strings.GroupsPictures;
                case MediaType.Video:
                    return Strings.GroupVideos;
                default:
                    return Strings.GroupOther;
            }
        }

        public StorageFolder Parameter
        {
            get;
            set;
        }

        public BindableCollection<MediaSortViewModel> SortOptions
        {
            get; private set;
        }

        public MediaSortViewModel SelectedSortOption
        {
            get; set;
        }

        public async void OnSelectedSortOptionChanged()
        {
            // We want to supress the reload during the first setup
            if (GroupedStorageItems.Count == 0)
                return;

            settingsService.Sort = SelectedSortOption.Sort;

            GroupedStorageItems.Clear();
            
            await BindFilesAndFoldersAsync();
        }
    }
}
