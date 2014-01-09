using System;
using System.Threading.Tasks;
using Windows.Media.PlayTo;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Caliburn.Micro;
using XboxMediaRemote.App.Events;
using XboxMediaRemote.App.Resources;

namespace XboxMediaRemote.App.ViewModels
{
    public class PlayToViewModel : ViewModelBase, IHandle<MediaSelectedEventArgs>
    {
        private readonly IEventAggregator eventAggregator;
        private PlayToManager playToManager;

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
                            args.SourceRequest.SetSource(source);
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
            
        }

        private async Task<PlayToSource> CreateSourceAsync()
        {
            switch (CurrentFile.MediaType)
            {
                case MediaType.Image:

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

                    var mediaElement = new MediaElement();

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
    }
}
