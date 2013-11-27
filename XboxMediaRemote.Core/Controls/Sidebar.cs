using System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace XboxMediaRemote.Core.Controls
{
    [TemplateVisualState(GroupName = ContentStates, Name = Portrait)]
    [TemplateVisualState(GroupName = ContentStates, Name = Landscape)]
    [TemplatePart(Name = "PortraitPopup", Type = typeof(Popup))]
    public class Sidebar : Control
    {
        // What don't I like about the current implementation.
        // The orientation thing is kinda weird, let the page control it?
        // Would be nice to to have to not have to listen to window size changed events but don't think this is going to change
        // Should it be boolean properties for both?

        private const string ContentStates = "ApplicationViewStates";
        private const string Portrait = "Portrait";
        private const string Landscape = "Landscape";

        public static readonly DependencyProperty ExpandedContentTemplateProperty =
            DependencyProperty.Register("ExpandedContentTemplate", typeof(DataTemplate), typeof(Sidebar), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedContentTemplateSelectorProperty =
            DependencyProperty.Register("ExpandedContentTemplateSelector", typeof(DataTemplateSelector), typeof(Sidebar), new PropertyMetadata(null));

        public static readonly DependencyProperty ExpandedContentTransitionsProperty =
            DependencyProperty.Register("ExpandedContentTransitions", typeof(TransitionCollection), typeof(Sidebar), new PropertyMetadata(null));

        public static readonly DependencyProperty CollapsedContentTemplateProperty =
            DependencyProperty.Register("CollapsedContentTemplate", typeof(DataTemplate), typeof(Sidebar), new PropertyMetadata(null));

        public static readonly DependencyProperty CollapsedContentTemplateSelectorProperty =
            DependencyProperty.Register("CollapsedContentTemplateSelector", typeof(DataTemplateSelector), typeof(Sidebar), new PropertyMetadata(null));

        public static readonly DependencyProperty CollapsedContentTransitionsProperty =
            DependencyProperty.Register("CollapsedContentTransitions", typeof(TransitionCollection), typeof(Sidebar), new PropertyMetadata(null));

        private Popup portraitPopup;

        public Sidebar()
        {
            DefaultStyleKey = typeof(Sidebar);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            IsExpanded = true;
        }

        public void Expand()
        {
            IsExpanded = true;
            UpdateVisualState(useTransitions: true);
            UpdatePopupIsOpen();
        }

        public void Collapse()
        {
            IsExpanded = false;
            UpdateVisualState(useTransitions: true);
            UpdatePopupIsOpen();
        }

        public bool IsExpanded
        {
            get;
            private set;
        }

        public ApplicationViewOrientation Orientation
        {
            get;
            private set;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += OnWindowSizeChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            var applicationView = ApplicationView.GetForCurrentView();

            Orientation = applicationView.Orientation;

            UpdatePopupSize();
            UpdateVisualState(useTransitions: true);
        }

        private void UpdatePopupSize()
        {
            if (portraitPopup == null)
                return;

            var popupChild = portraitPopup.Child as FrameworkElement;

            if (popupChild == null)
                return;

            popupChild.Height = Window.Current.Bounds.Height;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            portraitPopup = (Popup)GetTemplateChild("PortraitPopup");

            portraitPopup.Closed += (s, e) => IsExpanded = false;

            UpdatePopupSize();
            UpdateVisualState(useTransitions: false);
        }

        private void UpdateVisualState(bool useTransitions)
        {
            var visualState = Orientation == ApplicationViewOrientation.Landscape ?
                IsExpanded ? Landscape : Portrait :
                Portrait;

            VisualStateManager.GoToState(this, visualState, useTransitions);
        }

        private void UpdatePopupIsOpen()
        {
            if (portraitPopup == null)
                return;

            portraitPopup.IsOpen = Orientation == ApplicationViewOrientation.Portrait && IsExpanded;
        }

        public DataTemplate ExpandedContentTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ExpandedContentTemplateProperty);
            }
            set
            {
                SetValue(ExpandedContentTemplateProperty, value);
            }
        }

        public DataTemplateSelector ExpandedContentTemplateSelector
        {
            get
            {
                return (DataTemplateSelector)GetValue(ExpandedContentTemplateSelectorProperty);
            }
            set
            {
                SetValue(ExpandedContentTemplateSelectorProperty, value);
            }
        }

        public TransitionCollection ExpandedContentTransitions
        {
            get
            {
                return (TransitionCollection)GetValue(ExpandedContentTransitionsProperty);
            }
            set
            {
                SetValue(ExpandedContentTransitionsProperty, value);
            }
        }

        public DataTemplate CollapsedContentTemplate
        {
            get
            {
                return (DataTemplate)GetValue(CollapsedContentTemplateProperty);
            }
            set
            {
                SetValue(CollapsedContentTemplateProperty, value);
            }
        }

        public DataTemplateSelector CollapsedContentTemplateSelector
        {
            get
            {
                return (DataTemplateSelector)GetValue(CollapsedContentTemplateSelectorProperty);
            }
            set
            {
                SetValue(CollapsedContentTemplateSelectorProperty, value);
            }
        }

        public TransitionCollection CollapsedContentTransitions
        {
            get
            {
                return (TransitionCollection)GetValue(CollapsedContentTransitionsProperty);
            }
            set
            {
                SetValue(CollapsedContentTransitionsProperty, value);
            }
        }
    }
}