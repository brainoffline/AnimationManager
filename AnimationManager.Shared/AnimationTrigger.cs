using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

#if WINDOWS_PHONE
using System.Windows;
#endif

namespace Brain.Animate
{
    public class AnimationTrigger
    {
        internal static List<FrameworkElement> CloseElements = new List<FrameworkElement>();
        private static readonly object _lockObject = new object();

        /*************************************************************************/

        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.RegisterAttached("Open", typeof (AnimationDefinition), typeof (AnimationTrigger), new PropertyMetadata(default(AnimationDefinition)));

        public static void SetOpen(FrameworkElement element, AnimationDefinition animationDefinition)
        {
            element.SetValue(OpenProperty, animationDefinition);

            if (animationDefinition.OpacityFromZero)
                element.Opacity = 0;

            element.Loaded += async (sender, args) =>
            {
                var idleDefinition = GetIdle(element);
                if (idleDefinition == null)
                {
                    await element.AnimateAsync(animationDefinition);
                }
            };
        }

        public static AnimationDefinition GetOpen(FrameworkElement element)
        {
            return (AnimationDefinition) element.GetValue(OpenProperty);
        }

        /*************************************************************************/

        public static readonly DependencyProperty IdleProperty =
            DependencyProperty.RegisterAttached("Idle", typeof (AnimationDefinition), typeof (AnimationTrigger), new PropertyMetadata(default(AnimationDefinition)));

        public static void SetIdle(FrameworkElement element, AnimationDefinition animationDefinition)
        {
            element.SetValue(IdleProperty, animationDefinition);
            element.Loaded += async (sender, args) =>
            {
                var openAnimation = GetOpen(element);
                if (openAnimation != null)
                {
                    if (openAnimation.OpacityFromZero)
                        element.Opacity = 0;

                    await element.AnimateAsync(openAnimation);
                }

                animationDefinition.Forever = true;
                await element.AnimateAsync(animationDefinition);
            };

        }

        public static AnimationDefinition GetIdle(FrameworkElement element)
        {
            return (AnimationDefinition) element.GetValue(IdleProperty);
        }

        /*************************************************************************/

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof (AnimationDefinition), typeof (AnimationTrigger), new PropertyMetadata(default(AnimationDefinition)));

        public static void SetClose(FrameworkElement element, AnimationDefinition animationDefinition)
        {
            element.SetValue(CloseProperty, animationDefinition);

            element.Loaded += (sender, args) =>
            {
                lock (_lockObject)
                {
                    CloseElements.Add(element);
                }
            };
            element.Unloaded += (sender, args) =>
            {
                lock (_lockObject)
                {
                    CloseElements.Remove(element);
                }
            };
        }

        public static AnimationDefinition GetClose(FrameworkElement element)
        {
            return (AnimationDefinition) element.GetValue(CloseProperty);
        }

        /*************************************************************************/

        public static async Task AnimateClose()
        {
            var animations = new List<Task>();
            foreach (var element in CloseElements.ToList())
            {
                var closeDefinition = GetClose(element);
                if (closeDefinition != null)
                    animations.Add(element.AnimateAsync(closeDefinition));
            }

            await Task.WhenAll(animations.ToArray());
        }

    }

}
