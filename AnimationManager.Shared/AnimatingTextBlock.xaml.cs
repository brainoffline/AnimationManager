using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

#elif WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
#endif

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Brain.Animate
{
    public sealed partial class AnimatingTextBlock : UserControl
    {
        public AnimatingTextBlock()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AnimateIn();
        }

        public void AnimateIn()
        {
            if (LayoutRoot == null) return;
            if (Text == null) return;

            LayoutRoot.Children.Clear();
            var transparentBrush = new SolidColorBrush(Colors.Transparent);

            for (int i = 0; i < Text.Length; i++)
            {
                var s = Text.Substring(i, 1);
                if (string.IsNullOrWhiteSpace(s)) continue;

                var tb = new TextBlock
                {
                    FontFamily = FontFamily,
                    FontSize = FontSize,
                    FontStyle = FontStyle,
                    FontWeight = FontWeight
                };

                if (i == 0)
                    tb.Text = Text.Substring(0, 1);
                else
                {
                    tb.Inlines.Add(new Run {Text = Text.Substring(0, i), Foreground = transparentBrush});
                    tb.Inlines.Add(new Run {Text = Text.Substring(i, 1)});
                }
                LayoutRoot.Children.Add(tb);

#if NETFX_CORE || WINDOWS_81_PORTABLE
                if (DesignMode.DesignModeEnabled)
                    return;
#endif
                if (InAnimation != null)
                {
                    InAnimation.Delay = PauseBefore + (i * Interval);
                    tb.AnimateAsync(InAnimation);
                }

                if (IdleAnimation != null)
                {
                    IdleAnimation.PauseBefore = PauseBefore + (i * Interval);
                    IdleAnimation.PauseAfter = ((Text.Length - i) * Interval) + PauseAfter;
                    IdleAnimation.Forever = true;
                    tb.AnimateAsync(IdleAnimation);
                }
            }
        }

        public Task AnimateText(AnimationDefinition animationDefinition, double interval)
        {
            var animations = new List<Task>();

            if ((LayoutRoot != null) &&
                (Text != null) &&
                (animationDefinition != null))
            {
                LayoutRoot.Children.Clear();
                var transparentBrush = new SolidColorBrush(Colors.Transparent);

                var delay = animationDefinition.Delay;

                for (int i = 0; i < Text.Length; i++)
                {
                    var s = Text.Substring(i, 1);
                    if (string.IsNullOrWhiteSpace(s)) continue;

                    var tb = new TextBlock
                    {
                        FontFamily = FontFamily,
                        FontSize = FontSize,
                        FontStyle = FontStyle,
                        FontWeight = FontWeight
                    };
                    if (i == 0)
                        tb.Text = Text.Substring(0, 1);
                    else
                    {
                        tb.Inlines.Add(new Run {Text = Text.Substring(0, i), Foreground = transparentBrush});
                        tb.Inlines.Add(new Run {Text = Text.Substring(i, 1)});
                    }
                    LayoutRoot.Children.Add(tb);

#if NETFX_CORE || WINDOWS_81_PORTABLE
                    if (DesignMode.DesignModeEnabled)
                    return null;
#endif

                    animationDefinition.Delay = i*interval;
                    animations.Add(tb.AnimateAsync(animationDefinition));
                }

                animationDefinition.Delay = delay;
            }

            return Task.WhenAll(animations);
        }

        public void AnimateOut()
        {
            AnimateText(OutAnimation, Interval/2);
        }


        /*****************************************************************************/

        public static readonly DependencyProperty InAnimationProperty =
            DependencyProperty.Register("InAnimation", typeof (AnimationDefinition), typeof (AnimatingTextBlock), new PropertyMetadata(default(AnimationDefinition)));

        public AnimationDefinition InAnimation
        {
            get { return (AnimationDefinition) GetValue(InAnimationProperty); }
            set { SetValue(InAnimationProperty, value); }
        }

        /*****************************************************************************/

        public static readonly DependencyProperty IdleAnimationProperty = DependencyProperty.Register(
            "IdleAnimation", typeof (AnimationDefinition), typeof (AnimatingTextBlock), new PropertyMetadata(default(AnimationDefinition)));

        public AnimationDefinition IdleAnimation
        {
            get { return (AnimationDefinition) GetValue(IdleAnimationProperty); }
            set { SetValue(IdleAnimationProperty, value); }
        }

        /*****************************************************************************/

        public static readonly DependencyProperty OutAnimationProperty = DependencyProperty.Register(
            "OutAnimation", typeof (AnimationDefinition), typeof (AnimatingTextBlock), new PropertyMetadata(default(AnimationDefinition)));

        public AnimationDefinition OutAnimation
        {
            get { return (AnimationDefinition) GetValue(OutAnimationProperty); }
            set { SetValue(OutAnimationProperty, value); }
        }

        /*****************************************************************************/

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (AnimatingTextBlock), new PropertyMetadata(default(string), OnTextChanged));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as AnimatingTextBlock;
            if (target == null) return;

            target.OnTextChanged((string)e.OldValue, e.NewValue as string);
        }

        private void OnTextChanged(string oldString, string newString)
        {
            AnimateIn();
        }

        /*****************************************************************************/

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof (double), typeof (AnimatingTextBlock), new PropertyMetadata(0.03d));

        public double Interval
        {
            get { return (double) GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        /*****************************************************************************/

        public static readonly DependencyProperty PauseAfterProperty = DependencyProperty.Register(
            "PauseAfter", typeof (double), typeof (AnimatingTextBlock), new PropertyMetadata(default(double)));

        public double PauseAfter
        {
            get { return (double) GetValue(PauseAfterProperty); }
            set { SetValue(PauseAfterProperty, value); }
        }

        public static readonly DependencyProperty PauseBeforeProperty = DependencyProperty.Register(
            "PauseBefore", typeof (double), typeof (AnimatingTextBlock), new PropertyMetadata(default(double)));

        public double PauseBefore
        {
            get { return (double) GetValue(PauseBeforeProperty); }
            set { SetValue(PauseBeforeProperty, value); }
        }

    }
}
