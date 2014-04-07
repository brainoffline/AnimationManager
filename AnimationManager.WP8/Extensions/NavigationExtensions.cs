using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

#if WINDOWS_PHONE
using Microsoft.Phone.Controls;
#endif

namespace Brain.Animate
{
    public static class NavigationExtensions
    {
        public static bool NavigateTo(
            this NavigationService navigationService, 
            Uri source, 
            AnimationDefinition closeAnimation,
            AnimationDefinition openAnimation,
            bool sequential)
        {
#if NETFX_CORE
            var frame = Window.Current.Content as AnimationFrame;
#else
            var frame = Application.Current.RootVisual as AnimationFrame;
#endif

            if (frame != null)
                frame.SetNextNavigationAnimation(closeAnimation, openAnimation, sequential);

            return navigationService.Navigate(source);
        }


#if WINDOWS_PHONE
        public static async void AnimateClose(this PhoneApplicationFrame frame)
        {
            var animations = new List<Task>();
            foreach (var element in AnimationTrigger.CloseElements.ToList())
            {
                var closeDefinition = AnimationTrigger.GetClose(element);
                if (closeDefinition != null)
                    animations.Add(element.AnimateAsync(closeDefinition));
            }

            await Task.WhenAll(animations.ToArray());
        }
#endif

    }
}
