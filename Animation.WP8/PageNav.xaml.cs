using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Brain.Animate;
using Brain.Animate.Extensions;
using Brain.Animate.NavigationAnimations;
using Microsoft.Phone.Controls;

namespace Animation.WP8
{
    public partial class PageNav : PhoneApplicationPage
    {
        public PageNav()
        {
            InitializeComponent();

            switch (new Random().Next(7))
            {
                case 0: LayoutRoot.Background = new SolidColorBrush(Colors.Blue); break;
                case 1: LayoutRoot.Background = new SolidColorBrush(Colors.DarkGray); break;
                case 2: LayoutRoot.Background = new SolidColorBrush(Colors.Magenta); break;
                case 3: LayoutRoot.Background = new SolidColorBrush(Colors.Orange); break;
                case 4: LayoutRoot.Background = new SolidColorBrush(Colors.Purple); break;
                case 5: LayoutRoot.Background = new SolidColorBrush(Colors.Red); break;
                case 6: LayoutRoot.Background = new SolidColorBrush(Colors.Brown); break;
            }
        }

        public int Number { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string number;
            if (NavigationContext.QueryString.TryGetValue("Number", out number))
                Number = Convert.ToInt32(number);
            else
                Number = 1;

            Heading.Text = string.Format("page {0}", Number);
        }

        private void SetFrameNavigationAnimation(NavigationAnimationDefinition navigationAnimation)
        {
#if NETFX_CORE
            var frame = Window.Current.Content as AnimationFrame;
#else
            var frame = Application.Current.RootVisual as AnimationFrame;
#endif
            if (frame == null) return;

            frame.NavigationAnimation = navigationAnimation;
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageNav.xaml?Number=" + (Number + 1), UriKind.Relative));
        }

        private void Next2ButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateTo(
                new Uri("/PageNav.xaml?Number=" + (Number + 1), UriKind.Relative),
                new BounceOutUpAnimation(), new BounceInUpAnimation(), true
                );
        }

        private void ExpandFromHereButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateTo(
                new Uri("/PageNav.xaml?Number=" + (Number + 1), UriKind.Relative),
                null, 
                new ScaleFromElementAnimation { FromElement = sender as Button }, false);
        }

        private void DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(null);
        }

        private void JumpDoubleLeftButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new JumpDoubleLeftNavigationAnimation());
        }

        private void SuperScaleButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new SuperScaleNavigationAnimation());
        }

        private void CentreFlipButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new CentreFlipNavigationAnimation());
        }

        private void FadeDownButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new FadeDownNavigationAnimation());
        }

        private void SwipeLeftButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new SwipeLeftNavigationAnimation());
        }

        private void HingeButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new HingeNavigationAnimation());
        }

        private void RotateScaleButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new RotateScaleUpNavigationAnimation());
        }

        private void RotateScaleDownButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new RotateScaleDownNavigationAnimation());
        }

        private void TurnstileButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new TurnstileNavigationAnimation());
        }

        private void SwapButtonClick(object sender, RoutedEventArgs e)
        {
            SetFrameNavigationAnimation(new SwapPageNavigationAnimation());
        }

    }
}