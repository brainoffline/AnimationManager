using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Brain.Animate;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Animation.WP8
{
    public partial class ShowcasePage : PhoneApplicationPage
    {
        public ShowcasePage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await PersonWIthShaddow.MoveToAsync(0, new Point(-300, 0));
            await PersonWIthShaddow.MoveToAsync(6, new Point(0, 0), new QuarticEase());
        }


        protected override async void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;

            await Task.WhenAll(new[]
            {
                AnimationTrigger.AnimateClose(),
                Person.ScaleToAsync(0.5, new Point(1.6, 1.6), new BackEase())
            });

            NavigationService.GoBack();
        }

        private async void Person_OnTap(object sender, GestureEventArgs e)
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

            await Task.WhenAll(new[]
            {
                target.AnimateAsync(animationDefinition),
                PersonWIthShaddow.AnimateAsync(animationDefinition),
                ((Control)sender).AnimateAsync(animationDefinition)
            });

            await ((Control)sender).AnimateAsync(new ResetAnimation());   // Synchronous
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

    }

}