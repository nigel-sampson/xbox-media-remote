using System;
using Windows.ApplicationModel;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels.Settings
{
    [ImplementPropertyChanged]
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            var version = Package.Current.Id.Version;

            Version = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            Credits = new BindableCollection<CreditViewModel>
            {
                new CreditViewModel("Caliburn Micro", "http://caliburnmicro.codeplex.com"),
                new CreditViewModel("Fody", "https://github.com/Fody/Fody"),
                new CreditViewModel("Interactive Extensions", "http://rx.codeplex.com/"),
                new CreditViewModel("Reactive Extensions", "http://rx.codeplex.com/"),
                new CreditViewModel("Modern UI Icons", "http://modernuiicons.com/"),
            };
        }

        public string Version
        {
            get; set;
        }

        public BindableCollection<CreditViewModel> Credits
        {
            get; private set;
        }
    }
}
