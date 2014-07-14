using System.Threading.Tasks;
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
using Brain.Animate;


namespace Animation
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ShowcasePage : Page
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


        public ShowcasePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            await PersonWIthShaddow.MoveToAsync(0, new Point(-300, 0));
            await PersonWIthShaddow.MoveToAsync(6, new Point(0, 0), new QuarticEase());

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private async void Person_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await Person.AnimateAsync(new JumpAnimation());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Animate the target
            var animationName = button.Tag as string;
            var animationDefinition = GetAnimationDefinition(animationName);

            await target.AnimateAsync(new ResetAnimation());

            if (animationDefinition == null) return;

            await Task.WhenAll(new[]
            {
                target.AnimateAsync(animationDefinition),
                PersonWIthShaddow.AnimateAsync(animationDefinition),
                ((Control)sender).AnimateAsync(animationDefinition)
            });

            await((Control)sender).AnimateAsync(new ResetAnimation());   // Synchronous
            await target.AnimateAsync(new ResetAnimation());

            await Task.Delay(TimeSpan.FromSeconds(1));
            await PersonWIthShaddow.AnimateAsync(new ResetAnimation());
        }


        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Animate the target
            var animationName = button.Tag as string;
            var animationDefinition = GetAnimationDefinition(animationName);

            await target.AnimateAsync(new ResetAnimation());

            await Task.WhenAll(new[]
            {
                target.AnimateAsync(animationDefinition),
                Person.AnimateAsync(animationDefinition),
                ((Control)sender).AnimateAsync(animationDefinition)
            });

            await ((Control)sender).AnimateAsync(new ResetAnimation());   // Synchronous
            await target.AnimateAsync(new ResetAnimation());

            await Task.Delay(TimeSpan.FromSeconds(1));
            await Person.AnimateAsync(new ResetAnimation());
        }

        private static AnimationDefinition GetAnimationDefinition(string animationName)
        {
            AnimationDefinition animationDefinition = null;
            if (animationName != null)
            {
                switch (animationName.ToLower())
                {
                    case "flash": animationDefinition = new FlashAnimation(); break;
                    case "bounce": animationDefinition = new BounceAnimation(); break;
                    case "shake": animationDefinition = new ShakeAnimation(); break;
                    case "tada": animationDefinition = new TadaAnimation(); break;
                    case "swing": animationDefinition = new SwingAnimation(); break;
                    case "wobble": animationDefinition = new WobbleAnimation(); break;
                    case "pulse": animationDefinition = new PulseAnimation(); break;
                    case "breathing": animationDefinition = new BreathingAnimation(); break;
                    case "jump": animationDefinition = new JumpAnimation(); break;
                    case "flip": animationDefinition = new FlipAnimation(); break;
                    case "flipinx": animationDefinition = new FlipInXAnimation(); break;
                    case "flipoutx": animationDefinition = new FlipOutXAnimation(); break;
                    case "flipiny": animationDefinition = new FlipInYAnimation(); break;
                    case "flipouty": animationDefinition = new FlipOutYAnimation(); break;
                    case "fadein": animationDefinition = new FadeInAnimation(); break;
                    case "fadeout": animationDefinition = new FadeOutAnimation(); break;
                    case "fadeinleft": animationDefinition = new FadeInLeftAnimation(); break;
                    case "fadeinright": animationDefinition = new FadeInRightAnimation(); break;
                    case "fadeinup": animationDefinition = new FadeInUpAnimation(); break;
                    case "fadeindown": animationDefinition = new FadeInDownAnimation(); break;
                    case "fadeoutleft": animationDefinition = new FadeOutLeftAnimation(); break;
                    case "fadeoutright": animationDefinition = new FadeOutRightAnimation(); break;
                    case "fadeoutup": animationDefinition = new FadeOutUpAnimation(); break;
                    case "fadeoutdown": animationDefinition = new FadeOutDownAnimation(); break;
                    case "bouncein": animationDefinition = new BounceInAnimation(); break;
                    case "bounceout": animationDefinition = new BounceOutAnimation(); break;
                    case "bounceinleft": animationDefinition = new BounceInLeftAnimation(); break;
                    case "bounceinright": animationDefinition = new BounceInRightAnimation(); break;
                    case "bounceinup": animationDefinition = new BounceInUpAnimation(); break;
                    case "bounceindown": animationDefinition = new BounceInDownAnimation(); break;
                    case "bounceoutleft": animationDefinition = new BounceOutLeftAnimation(); break;
                    case "bounceoutright": animationDefinition = new BounceOutRightAnimation(); break;
                    case "bounceoutup": animationDefinition = new BounceOutUpAnimation(); break;
                    case "bounceoutdown": animationDefinition = new BounceOutDownAnimation(); break;
                    case "scalein": animationDefinition = new ScaleInAnimation(); break;
                    case "scaleout": animationDefinition = new ScaleOutAnimation(); break;
                    case "lightspeedinleft": animationDefinition = new LightSpeedInLeftAnimation(); break;
                    case "lightspeedinright": animationDefinition = new LightSpeedInRightAnimation(); break;
                    case "lightspeedoutleft": animationDefinition = new LightSpeedOutLeftAnimation(); break;
                    case "lightspeedoutright": animationDefinition = new LightSpeedOutRightAnimation(); break;
                    case "hinge": animationDefinition = new HingeAnimation(); break;
                    case "reset": animationDefinition = new ResetAnimation(); break;
                }
            }
            return animationDefinition;
        }

        private async void AllFlippers_Click(object sender, RoutedEventArgs e)
        {
            await target.AnimateAsync(new FlipAnimation());
            await target.AnimateAsync(new FlipOutXAnimation());
            await target.AnimateAsync(new FlipInXAnimation());
            await target.AnimateAsync(new FlipOutYAnimation());
            await target.AnimateAsync(new FlipInYAnimation());
            await target.AnimateAsync(new FlipAnimation());
        }

        private async void AllBounce_CLick(object sender, RoutedEventArgs e)
        {
            await target.AnimateAsync(new BounceOutAnimation());
            await target.AnimateAsync(new BounceInAnimation());
            await target.AnimateAsync(new BounceOutUpAnimation());
            await target.AnimateAsync(new BounceInUpAnimation());
            await target.AnimateAsync(new BounceOutDownAnimation());
            await target.AnimateAsync(new BounceInDownAnimation());
            await target.AnimateAsync(new BounceOutLeftAnimation());
            await target.AnimateAsync(new BounceInLeftAnimation());
            await target.AnimateAsync(new BounceOutRightAnimation());
            await target.AnimateAsync(new BounceInRightAnimation());
            await target.AnimateAsync(new BounceOutAnimation());
            await target.AnimateAsync(new BounceInAnimation());
        }


        private async void WalkButton_Click(object sender, RoutedEventArgs e)
        {
            //AnimationManager.PrepareControl(AnimatedHeadImage);
            await PersonWIthShaddow.AnimateAsync(WalkAnimation(PersonWIthShaddow));
        }

        public IEnumerable<Timeline> WalkAnimation(Control control)
        {
            return new Timeline[]
            {
                control.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(3, 240)
                    .AddEasingKeyFrame(3.5, 240)
                    .AddEasingKeyFrame(9.5, -240)
                    .AddEasingKeyFrame(10.0, -240)
                    .AddEasingKeyFrame(13, 0),

                control.AnimateProperty("(UIElement.Projection).(PlaneProjection.RotationY)")
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(3, 0)
                    .AddEasingKeyFrame(3.5, 180)
                    .AddEasingKeyFrame(9.5, 180)
                    .AddEasingKeyFrame(10, 0)
                    .AddEasingKeyFrame(13, 0),
            };
        }


        #endregion

        private void Header_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var header = (AnimatingTextBlock) sender;
            if (header == null) return;

            header.AnimateIn();
        }
    }
}
