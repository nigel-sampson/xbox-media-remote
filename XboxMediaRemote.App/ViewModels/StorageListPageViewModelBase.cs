using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using XboxMediaRemote.Core.Extensions;

namespace XboxMediaRemote.App.ViewModels
{
    public class StorageListPageViewModelBase : PageViewModelBase
    {
        private readonly INavigationService navigationService;

        public StorageListPageViewModelBase(INavigationService navigationService) 
            : base(navigationService)
        {
            this.navigationService = navigationService;

            GroupedStorageItems = new BindableCollection<StorageItemGroupViewModel>();
        }

        protected Task LoadThumbnailsAsync()
        {
            return GroupedStorageItems.WhenAllAsync(g => g.Items.WhenAllAsync(i => i.LoadThumbnailAsync()));
        }

        public void SelectItem(ItemClickEventArgs e)
        {
            var itemViewModel = (StorageItemViewModel)e.ClickedItem;

            var folderViewModle = itemViewModel as StorageFolderViewModel;

            if (folderViewModle != null)
            {
                navigationService.NavigateToViewModel<BrowseFolderViewModel>(folderViewModle.Folder);
            }
        }

        public BindableCollection<StorageItemGroupViewModel> GroupedStorageItems
        {
            get;
            private set;
        }
    }
}
