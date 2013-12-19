using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XboxMediaRemote.App.Views
{
    public class ViewBase : Page, IView
    {
        public void GoToState(string stateName, bool useTransitions = true)
        {
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }
    }
}
