using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Caliburn.Micro;
using XboxMediaRemote.App.ViewModels;

namespace XboxMediaRemote.App
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            DisplayRootViewFor<MediaHubViewModel>();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();

            container
                .Instance(container);

            container
                .PerRequest<MediaHubViewModel>()
                .PerRequest<BrowseMediaFolderViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }
    }
}
