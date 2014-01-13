using System;
using XboxMediaRemote.App.ViewModels;

namespace XboxMediaRemote.App.Services
{
    public interface IApplicationSettingsService
    {
        MediaSort Sort { get; set; }
    }
}