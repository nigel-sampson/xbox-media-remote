using System;
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
    }
}
