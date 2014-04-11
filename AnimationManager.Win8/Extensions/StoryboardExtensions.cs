using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml.Media.Animation;
#endif

#if WINDOWS_PHONE
using System.Windows.Media.Animation;
#endif

namespace Brain.Animate.Extensions
{
    public static class StoryboardExtensions
    {
        public static Task<Storyboard> PlayAsync(this Storyboard storyboard)
        {
            var tcs = new TaskCompletionSource<Storyboard>();

            storyboard.Completed += (sender, o) => tcs.SetResult(storyboard);
            storyboard.Begin();

            return tcs.Task;
        }
    }
}
