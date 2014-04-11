using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#endif

#if WINDOWS_PHONE
using System.Windows.Media;
#endif

namespace Brain.Animate.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static IEnumerable<T> FindAllChildItems<T>(this DependencyObject obj) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(obj);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child == null) continue;

                if (child is T)
                    yield return child as T;

                foreach (var item in FindAllChildItems<T>(child).ToArray())
                    yield return item;
            }
        }

    }
}
