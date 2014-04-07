using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Brain.Animate;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Animation.WP8
{
    public partial class IntroductionPage : PhoneApplicationPage
    {
        public IntroductionPage()
        {
            InitializeComponent();
        }

        private async void ShowcaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.WhenAll(
                AnimationTrigger.AnimateClose(),
                ShowcaseButton.AnimateAsync(new FlipAnimation()),
                ShowcaseButton.AnimateAsync(new BounceOutDownAnimation()),
                NavigationButton.AnimateAsync(new BounceOutDownAnimation()),
                ExperimentsButton.AnimateAsync(new BounceOutDownAnimation()),
                SponsorText.AnimateAsync(new LightSpeedOutLeftAnimation())
            );

            NavigationService.Navigate(new Uri("/ShowcasePage.xaml", UriKind.Relative));
        }

        private void NavigationButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageNav.xaml", UriKind.Relative));
        }

        private void ExperimentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ExperimentPage.xaml", UriKind.Relative));
        }
    }
}