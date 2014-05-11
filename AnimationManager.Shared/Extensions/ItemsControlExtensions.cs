using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
#endif
#if WINDOWS_PHONE
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
#endif

namespace Brain.Animate.Extensions
{
    public static class ItemsControlExtensions
    {
#if WINDOWS_PHONE

        public static async Task AnimateItems(
            this Selector selector, 
            AnimationDefinition animationDefinition, 
            double itemDelay = 0.05, 
            AnimationDefinition selectedItemAnimationDefinition = null)
        {
            if (selector == null) return;
            if (animationDefinition == null) return;
            if (itemDelay <= 0.0)
                itemDelay = 0.05;

            selector.UpdateLayout();

            var animations = new List<Task>();

            double baseDelay = animationDefinition.Delay;
            for (int index = 0; index < selector.Items.Count; index++)
            {
                var container = (FrameworkElement)selector.ItemContainerGenerator.ContainerFromIndex(index);
                if (container == null) continue;

                if ((index == selector.SelectedIndex) && (selectedItemAnimationDefinition != null))
                {
                    animationDefinition.Delay = baseDelay;
                    AnimationManager.ClearAnimationProperties(container);
                    animations.Add(
                        container.AnimateAsync(selectedItemAnimationDefinition));
                }
                else
                {
                    animationDefinition.Delay = baseDelay + (itemDelay*index);
                    AnimationManager.ClearAnimationProperties(container);
                    animations.Add(
                        container.AnimateAsync(animationDefinition));
                }
            }

            animationDefinition.Delay = baseDelay;

            await Task.WhenAll(animations.ToArray());
        }

#elif WINDOWS_81_PORTABLE || WINDOWS_PHONE_APP

        public static async Task AnimateItems(
            this Selector itemsControl,
            AnimationDefinition animationDefinition,
            double itemDelay = 0.05,
            object selectedObject = null,
            AnimationDefinition selectedItemAnimationDefinition = null)
        {
            if (itemsControl == null) return;
            if (animationDefinition == null) return;
            if (itemDelay <= 0.0)
                itemDelay = 0.05;

            itemsControl.UpdateLayout();

            var animations = new List<Task>();

            double baseDelay = animationDefinition.Delay;
            for (int index = 0; index < itemsControl.Items.Count; index++)
            {
                var container = (FrameworkElement)itemsControl.ContainerFromItem(itemsControl.Items[index]);
                //var container = (FrameworkElement)itemsControl.ItemContainerGenerator.ContainerFromIndex(index);
                if (container == null) continue;

                bool found = false;
                if (selectedItemAnimationDefinition != null)
                {
                    if ((index == itemsControl.SelectedIndex) ||
                        ((selectedObject != null) && (itemsControl.Items != null) && (selectedObject == itemsControl.Items[index]))
                        )
                    {
                        found = true;

                        animationDefinition.Delay = baseDelay;
                        AnimationManager.ClearAnimationProperties(container);
                        animations.Add(
                            container.AnimateAsync(selectedItemAnimationDefinition));
                    }
                }
                if (!found)
                {
                    animationDefinition.Delay = baseDelay + (itemDelay * index);
                    AnimationManager.ClearAnimationProperties(container);
                    animations.Add(
                        container.AnimateAsync(animationDefinition));
                }
            }

            animationDefinition.Delay = baseDelay;

            await Task.WhenAll(animations.ToArray());
        }


#endif


        public static async Task AnimateItems( this ItemsControl itemsControl,  AnimationDefinition animationDefinition,  double itemDelay = 0.05)
        {
            if (itemsControl == null) return;
            if (animationDefinition == null) return;
            if (itemDelay <= 0.0)
                itemDelay = 0.05;

            itemsControl.UpdateLayout();

            var animations = new List<Task>();

            double baseDelay = animationDefinition.Delay;

            for (int index = 0; index < itemsControl.Items.Count; index++)
            {
#if NETFX_CORE || WINDOWS_81_PORTABLE
                var container = (FrameworkElement)itemsControl.ContainerFromIndex(index);
#endif
#if WINDOWS_PHONE
                var container = (FrameworkElement) itemsControl.ItemContainerGenerator.ContainerFromIndex(index);
#endif
                if (container == null) continue;

                animationDefinition.Delay = baseDelay + (itemDelay*index);
                AnimationManager.ClearAnimationProperties(container);
                animations.Add(
                    container.AnimateAsync(animationDefinition));
            }

            animationDefinition.Delay = baseDelay;

            await Task.WhenAll(animations.ToArray());
        }


        public static IList<WeakReference> VisibleItems(this ItemsControl itemsControl)
        {
            var result = new List<WeakReference>();

            if (VisualTreeHelper.GetChildrenCount(itemsControl) == 0) return result;

            var scrollHost = VisualTreeHelper.GetChild(itemsControl, 0) as ScrollViewer;
            if (scrollHost == null) return result;

            if (itemsControl.Items == null) return result;
            itemsControl.UpdateLayout();

            int index;
            for (index = 0; index < itemsControl.Items.Count; index++)
            {
#if NETFX_CORE || WINDOWS_81_PORTABLE
                var container = (FrameworkElement)itemsControl.ContainerFromIndex(index);
#endif
#if WINDOWS_PHONE
                var container = (FrameworkElement)itemsControl.ItemContainerGenerator.ContainerFromIndex(index);
#endif
                if (container != null)
                {
                    GeneralTransform itemTransform = null;
                    try
                    {
                        itemTransform = container.TransformToVisual(scrollHost);
                    }
                    catch (ArgumentException)   // not always in visual tree
                    {
                        return result;
                    }

#if NETFX_CORE || WINDOWS_81_PORTABLE
                    var boundingBox = new Rect(
                        itemTransform.TransformPoint(new Point()),
                        itemTransform.TransformPoint(new Point(container.ActualWidth, container.ActualHeight)));
#elif WINDOWS_PHONE
                    var boundingBox = new Rect(
                        itemTransform.Transform(new Point()), 
                        itemTransform.Transform(new Point(container.ActualWidth, container.ActualHeight)));
#endif

                    if (boundingBox.Bottom > 0)
                    {
                        result.Add(new WeakReference(container));
                        index++;
                        break;
                    }
                }
            }

            for (; index < itemsControl.Items.Count; index++)
            {
#if NETFX_CORE || WINDOWS_81_PORTABLE
                var container = (FrameworkElement)itemsControl.ContainerFromIndex(index);
#endif
#if WINDOWS_PHONE
                var container = (FrameworkElement)itemsControl.ItemContainerGenerator.ContainerFromIndex(index);
#endif
                GeneralTransform itemTransform = null;
                try
                {
                    itemTransform = container.TransformToVisual(scrollHost);
                }
                catch (ArgumentException)   // not always in visual tree
                {
                    return result;
                }

#if NETFX_CORE || WINDOWS_81_PORTABLE
                var boundingBox = new Rect(
                    itemTransform.TransformPoint(new Point()),
                    itemTransform.TransformPoint(new Point(container.ActualWidth, container.ActualHeight)));
#elif WINDOWS_PHONE
                var boundingBox = new Rect(
                    itemTransform.Transform(new Point()), 
                    itemTransform.Transform(new Point(container.ActualWidth, container.ActualHeight)));
#endif

                if (boundingBox.Top >= scrollHost.ActualHeight)
                    break;

                result.Add(new WeakReference(container));
            }

            return result;
        }
    }
}
