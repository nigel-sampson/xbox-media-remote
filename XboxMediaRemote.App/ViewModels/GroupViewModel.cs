using System;
using System.Collections.Generic;
using Caliburn.Micro;
using PropertyChanged;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class GroupViewModel<T> : PropertyChangedBase
    {
        private readonly string title;

        public GroupViewModel(string title, IEnumerable<T> items)
        {
            this.title = title;

            Items = new BindableCollection<T>(items);     
        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        public BindableCollection<T> Items
        {
            get;
            private set;
        }
    }
}
