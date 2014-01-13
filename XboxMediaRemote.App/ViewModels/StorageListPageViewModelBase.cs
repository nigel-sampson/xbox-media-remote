using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using XboxMediaRemote.App.Events;
using XboxMediaRemote.App.Services;
using XboxMediaRemote.Core.Extensions;

namespace XboxMediaRemote.App.ViewModels
{
    public class StorageListPageViewModelBase : PageViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public StorageListPageViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator) 
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;

            GroupedStorageItems = new BindableCollection<StorageItemGroupViewModel>();
        }

        protected Task LoadThumbnailsAsync()
        {
            return GroupedStorageItems.WhenAllAsync(g => g.Items.WhenAllAsync(i => i.LoadThumbnailAsync()));
        }

        public void SelectItem(ItemClickEventArgs e)
        {
            var folderViewModle = e.ClickedItem as StorageFolderViewModel;

            if (folderViewModle != null)
            {
                navigationService.NavigateToViewModel<BrowseFolderViewModel>(folderViewModle.Folder);
            }

            var fileViewModel = e.ClickedItem as StorageFileViewModel;

            if (fileViewModel != null)
            {
                eventAggregator.PublishOnUIThread(new MediaSelectedEventArgs(fileViewModel));
            }
        }

        public BindableCollection<StorageItemGroupViewModel> GroupedStorageItems
        {
            get;
            private set;
        }
    }
}
