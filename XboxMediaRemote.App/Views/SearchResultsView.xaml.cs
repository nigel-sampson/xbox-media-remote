using System;
using Windows.UI.Xaml;

namespace XboxMediaRemote.App.Views
{
    public sealed partial class SearchResultsView
    {
        public SearchResultsView()
        {
            InitializeComponent();
        }

        private void OnHeaderSelected(object sender, RoutedEventArgs e)
        {
            ResultsZoom.ToggleActiveView();
        }
    }
}
