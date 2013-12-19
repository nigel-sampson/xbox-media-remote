using System;
using System.Reactive.Disposables;
using Caliburn.Micro;
using XboxMediaRemote.App.Views;

namespace XboxMediaRemote.App.ViewModels
{
    public class PageViewModelBase : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public PageViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public bool CanGoBack
        {
            get
            {
                return navigationService.CanGoBack;
            }
        }

        public void GoBack()
        {
            navigationService.GoBack();
        }

        protected IDisposable Loading()
        {
            View.GoToState(VisualStates.ContentStates.ContentLoading);

            return Disposable.Create(() => View.GoToState(VisualStates.ContentStates.ContentLoaded));
        }
    }
}
