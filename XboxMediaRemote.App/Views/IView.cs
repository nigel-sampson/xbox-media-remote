using System;

namespace XboxMediaRemote.App.Views
{
    public interface IView
    {
        void GoToState(string stateName, bool useTransitions = true);
    }
}