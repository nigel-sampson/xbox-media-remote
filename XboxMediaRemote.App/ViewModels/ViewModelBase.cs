using System;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using XboxMediaRemote.App.Views;

namespace XboxMediaRemote.App.ViewModels
{
    public class ViewModelBase : Screen
    {
        public IView View
        {
            get
            {
                return (IView) GetView();
            }
        }

        public override void NotifyOfPropertyChange([CallerMemberName]string propertyName = "")
        {
            base.NotifyOfPropertyChange(propertyName);
        }
    }
}
