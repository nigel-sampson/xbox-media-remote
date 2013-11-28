using System;
using Windows.UI.Xaml;

namespace XboxMediaRemote.App.Views
{
    public sealed partial class MediaHubView
    {
        public MediaHubView()
        {
            InitializeComponent();
        }

        private void OnCollapseSidebar(object sender, RoutedEventArgs e)
        {
            HubSidebar.Collapse();
        }

        private void OnExpandSidebar(object sender, RoutedEventArgs e)
        {
            HubSidebar.Expand();
        }
    }
}
