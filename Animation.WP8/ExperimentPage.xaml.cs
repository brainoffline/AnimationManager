using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Windows.Phone.Speech.Synthesis;
using Brain.Animate;
using Brain.Animate.Extensions;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Animation.WP8
{
    public partial class ExperimentPage : PhoneApplicationPage
    {
        public const string Whatever = "Whatever";
        public List<string> ExperimentTitleList { get; set; }

        public ExperimentPage()
        {
            InitializeComponent();

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

            /*
            SizeChanged += (sender, args) =>
            {
                var items = list.FindAllChildItems<ListBoxItem>().ToList();
                int i = 0;
                foreach (ListBoxItem listBoxItem in items)
                {
                    listBoxItem.Tap += ListBoxItemOnTap;
                    i++;
                }
            };
             */
        }

        private async void ListBoxItemOnTap(object sender, GestureEventArgs gestureEventArgs)
        {
            var listBoxItem = sender as ListBoxItem;
            if (listBoxItem == null) return;

            var text = listBoxItem.Content as string;
            if (text == Whatever) return;


            // Animate the list items out
            var items = list.FindAllChildItems<ListBoxItem>().ToList();
            var sb = new Storyboard();
            int i = 0;
            foreach (ListBoxItem item in items)
            {
                AnimationManager.ClearAnimationProperties(item);
                if (item == listBoxItem)
                    sb.Children.Add(AnimationManager.AnimationStoryboard(listBoxItem, new WobbleAnimation()));
                else
                    sb.Children.Add(AnimationManager.AnimationStoryboard(item, new BounceOutDownAnimation {Delay = i++ * 0.02}));
            }
            await sb.PlayAsync();   // wait for storyboard to complete

            // Navigate to new page
            switch (text)
            {
                case "Storyboard Scrub":
                    NavigationService.Navigate(new Uri("/Experiments/StoryboardScrubPage.xaml", UriKind.Relative));
                    break;
                case "Clock":
                    NavigationService.Navigate(new Uri("/Experiments/Clock.xaml", UriKind.Relative));
                    break;
            }
        }

        private async void List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem == null) return;

            //AnimatedItemsControl.CloseItemsAnimation(list);
            await list.AnimateItems(new BounceOutLeftAnimation(), selectedItemAnimationDefinition: new PulseAnimation());

            var text = list.SelectedItem as string;
            list.SelectedItem = null;       // Clear Selection

            // Navigate to new page
            switch (text)
            {
                default:
                    await list.AnimateItems(new BounceInRightAnimation());
                    break;
                case "Storyboard Scrub":
                    NavigationService.Navigate(new Uri("/Experiments/StoryboardScrubPage.xaml", UriKind.Relative));
                    break;
                case "Clock":
                    NavigationService.Navigate(new Uri("/Experiments/Clock.xaml", UriKind.Relative));
                    break;
            }

        }
    }
}