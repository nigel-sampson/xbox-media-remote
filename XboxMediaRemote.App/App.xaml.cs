using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Caliburn.Micro;
using XboxMediaRemote.App.Resources;
using XboxMediaRemote.App.Services;
using XboxMediaRemote.App.ViewModels;
using XboxMediaRemote.App.ViewModels.Settings;

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
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IApplicationSettingsService, ApplicationSettingsService>();

            container
                .PerRequest<MediaHubViewModel>()
                .PerRequest<BrowseFolderViewModel>()
                .PerRequest<SearchResultsViewModel>()
                .PerRequest<AboutViewModel>()
                .PerRequest<PrivacyPolicyViewModel>();

            var settings = container.RegisterSettingsService();

            settings.RegisterFlyoutCommand<AboutViewModel>(Strings.SettingsAbout);
            settings.RegisterFlyoutCommand<PrivacyPolicyViewModel>(Strings.SettingsPrivacyPolicy);
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
