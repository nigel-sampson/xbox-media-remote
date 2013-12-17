using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace XboxMediaRemote.App.Views
{
    public sealed partial class BrowseFolderView
    {
        public BrowseFolderView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            InitialiseSemanticZoom();
        }

        private void InitialiseSemanticZoom()
        {
            if (GroupsViewSource.View == null)
                return;

            ((ListViewBase)ItemsZoom.ZoomedOutView).ItemsSource = GroupsViewSource.View.CollectionGroups;
        }

        private void OnHeaderSelected(object sender, RoutedEventArgs e)
        {
            ItemsZoom.ToggleActiveView();
        }
    }
}
