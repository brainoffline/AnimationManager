using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Brain.Animate.Extensions;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

#if WINDOWS_PHONE
using System.Windows.Controls;
#endif


namespace Brain.Animate
{
    public class ListAnimation
    {
        /*******************************************************************************************/

        public static readonly DependencyProperty LoadItemProperty = DependencyProperty.RegisterAttached(
            "LoadItem", typeof (AnimationDefinition), typeof (ListAnimation), new PropertyMetadata(default(AnimationDefinition)));

        public static void SetLoadItem(ItemsControl element, AnimationDefinition value)
        {
            element.SetValue(LoadItemProperty, value);
            element.SizeChanged += OnSizeChanged;
        }

        public static AnimationDefinition GetLoadItem(ItemsControl element)
        {
            return (AnimationDefinition) element.GetValue(LoadItemProperty);
        }


        public static readonly DependencyProperty LoadItemDelayProperty = DependencyProperty.RegisterAttached(
            "LoadItemDelay", typeof (double), typeof (ListAnimation), new PropertyMetadata(0.05));

        public static void SetLoadItemDelay(ItemsControl element, double value)
        {
            element.SetValue(LoadItemDelayProperty, value);
        }

        public static double GetLoadItemDelay(ItemsControl element)
        {
            return (double) element.GetValue(LoadItemDelayProperty);
        }


        /*******************************************************************************************/

        public static readonly DependencyProperty UnloadItemProperty = DependencyProperty.RegisterAttached(
            "UnloadItem", typeof (AnimationDefinition), typeof (ListAnimation), new PropertyMetadata(default(AnimationDefinition)));

        public static void SetUnloadItem(ItemsControl element, AnimationDefinition value)
        {
            element.SetValue(UnloadItemProperty, value);
        }

        public static AnimationDefinition GetUnloadItem(ItemsControl element)
        {
            return (AnimationDefinition) element.GetValue(UnloadItemProperty);
        }


        public static readonly DependencyProperty UnloadItemDelayProperty = DependencyProperty.RegisterAttached(
            "UnloadItemDelay", typeof (double), typeof (ListAnimation), new PropertyMetadata(0.05));

        public static void SetUnloadItemDelay(ItemsControl element, double value)
        {
            element.SetValue(UnloadItemDelayProperty, value);
        }

        public static double GetUnloadItemDelay(ItemsControl element)
        {
            return (double) element.GetValue(UnloadItemDelayProperty);
        }

        /*******************************************************************************************/



        private static async void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            var itemsControl = sender as ItemsControl;
            if (itemsControl == null) return;
#if WINDOWS_81_PORTABLE
            if (sizeChangedEventArgs.PreviousSize.Width <= 0)
                return;

            //itemsControl.SizeChanged -= OnSizeChanged;
#endif

            var animationDefinition = GetLoadItem(itemsControl);
            if (animationDefinition == null) return;

            var itemDelay = GetLoadItemDelay(itemsControl);

            await itemsControl.AnimateItems(animationDefinition, itemDelay);
        }


        public static async Task UnloadItemsAnimation(ItemsControl itemsControl)
        {
            var animationDefinition = GetUnloadItem(itemsControl);
            if (animationDefinition == null) return;

            var itemDelay = GetUnloadItemDelay(itemsControl);

            await itemsControl.AnimateItems(animationDefinition, itemDelay);
        }



    }
}
