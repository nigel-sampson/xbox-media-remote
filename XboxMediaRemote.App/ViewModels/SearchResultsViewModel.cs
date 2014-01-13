using System;
using System.Linq;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Caliburn.Micro;
using XboxMediaRemote.App.Services;

namespace XboxMediaRemote.App.ViewModels
{
    public class SearchResultsViewModel : StorageListPageViewModelBase
    {
        public SearchResultsViewModel(INavigationService navigationService, IEventAggregator eventAggregator) 
            : base(navigationService, eventAggregator)
        {

        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            var localFolders = new[]
            {
                KnownFolders.MusicLibrary,
                KnownFolders.PicturesLibrary,
                KnownFolders.VideosLibrary
            };

            var serverFolders = await KnownFolders.MediaServerDevices.GetFoldersAsync();

            var queryOptions = new QueryOptions
            {
                FolderDepth = FolderDepth.Deep, 
                UserSearchFilter = Query
            };

            queryOptions.SetThumbnailPrefetch(ThumbnailMode.ListView, 150, ThumbnailOptions.UseCurrentScale);

            foreach (var folder in localFolders.Concat(serverFolders))
            {
                var itemQuery = folder.CreateItemQueryWithOptions(queryOptions);

                var items = await itemQuery.GetItemsAsync();

                if (items.Count == 0)
                    continue;

                var itemViewModels = items.Select<IStorageItem, StorageItemViewModel>(i =>
                {
                    if (i.IsOfType(StorageItemTypes.Folder))
                        return new StorageFolderViewModel((StorageFolder) i);

                    if (i.IsOfType(StorageItemTypes.File))
                        return new StorageFileViewModel((StorageFile) i);

                    return null;
                }).Where(v => v != null);

                GroupedStorageItems.Add(new StorageItemGroupViewModel(folder.DisplayName, itemViewModels));
            }

            await LoadThumbnailsAsync();
        }

        public string Query
        {
            get; set;
        }
    }
}
