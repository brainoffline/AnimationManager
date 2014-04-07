using Windows.UI.Xaml.Media.Animation;
using Animation.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using Brain.Animate;
using Brain.Animate.Extensions;

namespace Animation
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ExperimentPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public ExperimentPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            ExperimentTitleList = new List<string>
            {
                "Storyboard Scrub", 
                "Clock",
                Whatever,
                Whatever,
                Whatever,
                Whatever,
                Whatever,
                Whatever,
                Whatever,
                "Storyboard Scrub", 
                "Clock",
                Whatever,
                Whatever,
                Whatever,
            };

            DataContext = this;
            list.SelectionChanged += ListOnSelectionChanged;
        }

        private async void ListOnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count != 1) return;
            var text = args.AddedItems[0] as string;

            if (text == Whatever)
            {
                var item = list.ContainerFromItem(text) as ListBoxItem;
                if (item != null)
                    await item.AnimateAsync(new SwingAnimation());
                return;
            }

            // Animate the list items out
            var items = list.FindAllChildItems<ListBoxItem>().ToList();
            var sb = new Storyboard();
            int i = 0;
            foreach (ListBoxItem item in items)
            {
                AnimationManager.ClearAnimationProperties(item);
                if (item.IsSelected)
                    sb.Children.Add(AnimationManager.AnimationStoryboard(item, new WobbleAnimation()));
                else
                    sb.Children.Add(AnimationManager.AnimationStoryboard(item, new BounceOutDownAnimation { Delay = i++ * 0.02 }));
            }
            await sb.PlayAsync();   // wait for storyboard to complete

            // Navigate to new page
            switch (text)
            {
                case "Storyboard Scrub":
                    //Frame.Navigate(typeof (StoryboardScrubPage));
                    break;
                case "Clock":
                    Frame.Navigate(typeof(ClockPage));
                    break;
            }

        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        public const string Whatever = "Whatever";
        public List<string> ExperimentTitleList { get; set; }


    }
}
