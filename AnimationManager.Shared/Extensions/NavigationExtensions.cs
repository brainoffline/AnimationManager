using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Brain.Animate.Extensions
{
    public static class NavigationExtensions
    {
        public static bool NavigateTo(
            this Frame f, 
            Type source, 
            AnimationDefinition closeAnimation,
            AnimationDefinition openAnimation,
            bool sequential)
        {
            var frame = f as AnimationFrame;

            if (frame != null)
                frame.SetNextNavigationAnimation(closeAnimation, openAnimation, sequential);

            return f.Navigate(source);
        }


        public static async void AnimateClose(this Frame frame)
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

    }
}
