using System;
using System.Threading.Tasks;
using Windows.Media.PlayTo;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Caliburn.Micro;
using PropertyChanged;
using XboxMediaRemote.App.Events;
using XboxMediaRemote.App.Resources;

namespace XboxMediaRemote.App.ViewModels
{
    [ImplementPropertyChanged]
    public class PlayToViewModel : ViewModelBase, IHandle<MediaSelectedEventArgs>
    {
        private readonly IEventAggregator eventAggregator;
        private PlayToManager playToManager;
        private PlayToConnection currentConnection;

        public PlayToViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            playToManager = PlayToManager.GetForCurrentView();

            playToManager.SourceRequested += OnSourceRequsted;
            playToManager.SourceSelected += OnSourceSelected;

            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            playToManager.SourceRequested -= OnSourceRequsted;
            playToManager.SourceSelected -= OnSourceSelected;

            eventAggregator.Unsubscribe(this);
        }

        private void OnSourceRequsted(PlayToManager sender, PlayToSourceRequestedEventArgs args)
        {
            var deferral = args.SourceRequest.GetDeferral();

            Execute.OnUIThread(async () =>
                {
                    if (CurrentFile != null)
                    {
                        var source = await CreateSourceAsync();

                        if (source == null)
                            args.SourceRequest.DisplayErrorString(Strings.PlayToInvalidMediaType);
                        else
                        {
                            args.SourceRequest.SetSource(source);

                            CurrentConnection = source.Connection;
                        }
                    }
                    else
                    {
                        args.SourceRequest.DisplayErrorString(Strings.PlayToNoFile);
                    }

                    deferral.Complete();
                });
        }

        private void OnSourceSelected(PlayToManager sender, PlayToSourceSelectedEventArgs args)
        {
            Execute.OnUIThread(() =>
            {
                SourceName = args.FriendlyName;

                SourceIcon = new BitmapImage();
                SourceIcon.SetSource(args.Icon);    
            });
        }

        private async Task<PlayToSource> CreateSourceAsync()
        {
            switch (CurrentFile.MediaType)
            {
                case MediaType.Picture:

                    var imageStream = await CurrentFile.File.OpenReadAsync();

                    var imageSource = new BitmapImage();

                    await imageSource.SetSourceAsync(imageStream);

                    var image = new Image
                                {
                                    Source = imageSource
                                };

                    return image.PlayToSource;
                case MediaType.Video:

                    var videoStream = await CurrentFile.File.OpenReadAsync();

                    var mediaElement = new MediaElement
                    {
                        AutoPlay = false
                    };

                    mediaElement.SetSource(videoStream, CurrentFile.File.ContentType);

                    return mediaElement.PlayToSource;

                default:
                    return null;
            }
        }

        public void Handle(MediaSelectedEventArgs message)
        {
            CurrentFile = message.StorageFile;

            ShowPlayToUI();
        }

        public void ShowPlayToUI()
        {
            PlayToManager.ShowPlayToUI();
        }

        public StorageFileViewModel CurrentFile
        {
            get;
            set;
        }

        public PlayToConnection CurrentConnection
        {
            get
            {
                return currentConnection;
            }
            set
            {
                if (currentConnection == value)
                    return;

                if (currentConnection != null)
                {
                    currentConnection.StateChanged -= OnConnectionStateChanged;
                }

                currentConnection = value;

                if (currentConnection != null)
                {
                    currentConnection.StateChanged += OnConnectionStateChanged;
                }

                NotifyOfPropertyChange();
            }
        }

        private void OnConnectionStateChanged(PlayToConnection sender, PlayToConnectionStateChangedEventArgs args)
        {
            NotifyOfPropertyChange(() => CurrentConnection);
        }

        public string SourceName
        {
            get;
            set;
        }

        public BitmapImage SourceIcon
        {
            get;
            set;
        }
    }
}
