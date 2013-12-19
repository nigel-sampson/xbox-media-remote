using System;
using System.Linq;
using System.Runtime.InteropServices;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Caliburn.Micro;
using XboxMediaRemote.Core.Extensions;

namespace XboxMediaRemote.App.ViewModels
{
    public class SearchResultsViewModel : PageViewModelBase
    {
        public SearchResultsViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Results = new BindableCollection<StorageItemGroupViewModel>();
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

                Results.Add(new StorageItemGroupViewModel(folder.DisplayName, itemViewModels));
            }

            await Results.WhenAllAsync(g => g.Items.WhenAllAsync(i => i.LoadThumbnailAsync()));
        }

        public string Query
        {
            get; set;
        }

        public BindableCollection<StorageItemGroupViewModel> Results
        {
            get; private set;
        }
    }
}
