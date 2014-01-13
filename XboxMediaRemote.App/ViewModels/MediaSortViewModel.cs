using System;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class MediaSortViewModel : PropertyChangedBase
    {
        private readonly MediaSort sort;
        private readonly string description;

        public MediaSortViewModel(MediaSort sort, string description)
        {
            this.sort = sort;
            this.description = description;
        }

        public MediaSort Sort
        {
            get { return sort; }
        }

        public string Description
        {
            get { return description; }
        }
    }
}
