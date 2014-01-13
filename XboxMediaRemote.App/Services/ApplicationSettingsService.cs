using System;
using Windows.Storage;
using XboxMediaRemote.App.ViewModels;

namespace XboxMediaRemote.App.Services
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        protected static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        public MediaSort Sort
        {
            get
            {
                if (!LocalSettings.Values.ContainsKey("Sort"))
                    return MediaSort.Alphabetical;

                var sort = (string) LocalSettings.Values["Sort"];

                return (MediaSort) Enum.Parse(typeof (MediaSort), sort);
            }
            set
            {
                LocalSettings.Values["Sort"] = value.ToString();
            }
        }
    }
}
