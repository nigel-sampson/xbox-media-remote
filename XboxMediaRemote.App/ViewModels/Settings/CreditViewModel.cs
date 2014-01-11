using System;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels.Settings
{
    [ImplementPropertyChanged]
    public class CreditViewModel : PropertyChangedBase
    {
        private readonly string name;
        private readonly string uri;

        public CreditViewModel(string name, string uri)
        {
            this.name = name;
            this.uri = uri;
        }

        public string Name
        {
            get { return name; }
        }

        public string Uri
        {
            get { return uri; }
        }
    }
}
