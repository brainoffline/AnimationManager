using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Point = Windows.Foundation.Point;
#endif

#if WINDOWS_PHONE
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Point = System.Windows.Point;
#endif


namespace Brain.Animate
{
    public static class AnimationEntensions
    {

        /***********************************************************************************/

        /// <summary>
        /// Animate a FrameworkElement using a preset animation sequence
        /// </summary>
        /*
        public static Storyboard AnimationStoryboard(this FrameworkElement element, string animationName, Action completedAction = null)
        {
            return AnimationManager.AnimationStoryboard(element, animationName, completedAction);
        }
         */

        public static Task<FrameworkElement> AnimateAsync(
            this FrameworkElement element,
            AnimationDefinition animationDefinition
            )
        {
            var tcs = new TaskCompletionSource<FrameworkElement>();
            var sb = AnimationManager.AnimationStoryboard(element, animationDefinition, () => tcs.SetResult(element));
            if (sb == null)
                return null;
            Task t = AnimationManager.SplashScreenGone();
#if NETFX_CORE || WINDOWS_81_PORTABLE
            t.ContinueWith(x => element.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, sb.Begin));
#endif
#if WINDOWS_PHONE
            t.ContinueWith(x => element.Dispatcher.BeginInvoke(sb.Begin));
#endif
            return tcs.Task;
            
        }


        public static Task<FrameworkElement> AnimateAsync(this FrameworkElement element, IEnumerable<Timeline> animations, AnimationParameters animationParameters = null)
        {
            var tcs = new TaskCompletionSource<FrameworkElement>();
            var sb = AnimationManager.AnimationStoryboard(element, animations, animationParameters, () => tcs.SetResult(element));
            Task t = AnimationManager.SplashScreenGone();
#if NETFX_CORE || WINDOWS_81_PORTABLE
            t.ContinueWith(x => element.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, sb.Begin));
#endif
#if WINDOWS_PHONE
            t.ContinueWith(x => element.Dispatcher.BeginInvoke(sb.Begin));
#endif
            return tcs.Task;
        }

        public static void StopAnimations(this FrameworkElement element)
        {
            AnimationManager.StopAnimations(element);
        }



        /***********************************************************************************/

        private static IEnumerable<Timeline> RotateAnimation(FrameworkElement element, double seconds, double rotation, EasingFunctionBase easingFunction = null)
        {
            return new Timeline[]
            {
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.Rotation)")
                    .AddEasingKeyFrame(seconds, rotation, easingFunction),
            };
        }

        public static Storyboard RotateStoryboard(this FrameworkElement element, 
            double seconds, double rotation, EasingFunctionBase easingFunction = null,
            AnimationParameters animationParameters = null,
            Action completedAction = null)
        {
            return AnimationManager.AnimationStoryboard( element, RotateAnimation(element, seconds, rotation, easingFunction), null, completedAction);
        }

        public static async Task<FrameworkElement> RotateAsync(this FrameworkElement element, 
            double seconds, double rotation, EasingFunctionBase easingFunction = null, AnimationParameters animationParameters = null)
        {
            if (!(seconds <= 0))
                return await AnimateAsync(element, RotateAnimation(element, seconds, rotation, easingFunction), animationParameters);

            AnimationManager.PrepareElement(element);
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null) return element;

            transform.Rotation = rotation;
            return element;
        }




        private static IEnumerable<Timeline> MoveToAnimation(this FrameworkElement element,
            double seconds, Point point, EasingFunctionBase easingFunction = null)
        {
            return new Timeline[]
            {
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.TranslateX)")
                       .AddEasingKeyFrame(seconds, point.X, easingFunction),
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.TranslateY)")
                       .AddEasingKeyFrame(seconds, point.Y, easingFunction)
            };
        }

        public static Storyboard MoveToStoryboard(this FrameworkElement element,
            double seconds, Point point, EasingFunctionBase easingFunction = null, 
            AnimationParameters animationParameters = null, Action completedAction = null)
        {
            return AnimationManager.AnimationStoryboard(element, MoveToAnimation(element, seconds, point, easingFunction), animationParameters, completedAction);
        }

        public static async Task<FrameworkElement> MoveToAsync(this FrameworkElement element,
            double seconds, Point point, EasingFunctionBase easingFunction = null, AnimationParameters animationParameters = null)
        {
            if (seconds > 0.0)
                return await AnimateAsync(element, MoveToAnimation(element, seconds, point, easingFunction), animationParameters);

            AnimationManager.PrepareElement(element);
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null) return element;

            transform.TranslateX = point.X;
            transform.TranslateY = point.Y;
            return element;
        }




        private static IEnumerable<Timeline> ScaleToAnimation(this FrameworkElement element,
            double seconds, Point scale, EasingFunctionBase easingFunction = null)
        {
            return new Timeline[]
            {
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.ScaleX)")
                       .AddEasingKeyFrame(seconds, scale.X, easingFunction),
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.ScaleY)")
                       .AddEasingKeyFrame(seconds, scale.Y, easingFunction)
            };
        }

        public static Storyboard ScaleToStoryboard(this FrameworkElement element,
            double seconds, Point scale, EasingFunctionBase easingFunction = null, 
            AnimationParameters animationParameters = null, Action completedAction = null)
        {
            return AnimationManager.AnimationStoryboard(element, ScaleToAnimation(element, seconds, scale, easingFunction), animationParameters, completedAction);
        }

        public static async Task<FrameworkElement> ScaleToAsync(this FrameworkElement element,
            double seconds, Point scale, EasingFunctionBase easingFunction = null, AnimationParameters animationParameters = null)
        {
            if (seconds > 0)
                return await AnimateAsync(element, ScaleToAnimation(element, seconds, scale, easingFunction), animationParameters);

            AnimationManager.PrepareElement(element);
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null) return element;

            transform.ScaleX = scale.X;
            transform.ScaleY = scale.Y;
            return element;
        }



        private static IEnumerable<Timeline> SkewToAnimation(this FrameworkElement element,
            double seconds, Point skew, EasingFunctionBase easingFunction = null)
        {
            return new Timeline[]
            {
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.SkewX)")
                       .AddEasingKeyFrame(seconds, skew.X, easingFunction),
                element.AnimateProperty("(UIElement.RenderTransform).(CompositeTransformation.SkewY)")
                       .AddEasingKeyFrame(seconds, skew.Y, easingFunction)
            };
        }


        public static Storyboard SkewToStoryboard(this FrameworkElement element,
            double seconds, Point skew, EasingFunctionBase easingFunction = null, 
            AnimationParameters animationParameters = null, Action completedAction = null)
        {
            return AnimationManager.AnimationStoryboard(element, SkewToAnimation(element, seconds, skew, easingFunction), animationParameters, completedAction);
        }

        public async static Task<FrameworkElement> SkewToAsync(this FrameworkElement element,
            double seconds, Point skew, EasingFunctionBase easingFunction = null, AnimationParameters animationParameters = null)
        {
            if (seconds > 0)
                return await AnimateAsync(element, SkewToAnimation(element, seconds, skew, easingFunction), animationParameters);

            AnimationManager.PrepareElement(element);
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
                return element;

            transform.SkewX = skew.X;
            transform.SkewY = skew.Y;
            return element;
        }



        /***********************************************************************************/

        /// <summary>
        /// Start the definition of an individual animation definition
        /// <code>
        /// var sb = new Storyboard();
        /// var a1 = grid.AnimateProperty<DoubleAnimationUsingKeyFrames>(
        ///         "(UIElement.Projection).(PlaneProjection.RotationY)")
        ///         .AddEasingKeyFrame(0, 0)
        ///         .AddEasingKeyFrame(0.3, -90)
        ///         .AddEasingKeyFrame(0.6, 0, new CubicEase { EasingMode = EasingMode.EaseOut});
        /// sb.Children.Add(a1);
        /// sb.Completed += (o, o1) => { };
        /// sb.Begin();
        /// </code>
        /// </summary>
        public static DoubleAnimationUsingKeyFrames AnimateProperty(this FrameworkElement element, string path)
        {
            return element.AnimateProperty<DoubleAnimationUsingKeyFrames>(path);
        }

        public static PointAnimationUsingKeyFrames AnimatePointProperty(this FrameworkElement element, string path)
        {
            return element.AnimateProperty<PointAnimationUsingKeyFrames>(path);
        }

        public static ColorAnimationUsingKeyFrames AnimateColorProperty(this FrameworkElement element, string path)
        {
            return element.AnimateProperty<ColorAnimationUsingKeyFrames>(path);
        }

        public static ObjectAnimationUsingKeyFrames AnimateObjectProperty(this FrameworkElement element, string path)
        {
            return element.AnimateProperty<ObjectAnimationUsingKeyFrames>(path);
        }



        public static T AnimateProperty<T>(this FrameworkElement element, string path) where T : Timeline, new()
        {
            var animation = new T();

            if (element.Projection == null)
                element.Projection = new PlaneProjection();
            if (element.RenderTransform == null)
                element.RenderTransform = new CompositeTransform();

            Storyboard.SetTarget(animation, element);
#if WINDOWS_PHONE
            Storyboard.SetTargetProperty(animation, new PropertyPath(path));
#elif NETFX_CORE || WINDOWS_81_PORTABLE
            Storyboard.SetTargetProperty(animation, path);
#endif
            return animation;
        }


        public static DoubleAnimationUsingKeyFrames AddEasingKeyFrame(
            this DoubleAnimationUsingKeyFrames animation,
            double seconds, double value,
            EasingFunctionBase easingFunction = null)
        {
            var keyFrame = new EasingDoubleKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value,
                EasingFunction = easingFunction
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static PointAnimationUsingKeyFrames AddEasingKeyFrame(
            this PointAnimationUsingKeyFrames animation,
            double seconds, Point value,
            EasingFunctionBase easingFunction = null)
        {
            var keyFrame = new EasingPointKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value,
                EasingFunction = easingFunction
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static ColorAnimationUsingKeyFrames AddEasingKeyFrame(
            this ColorAnimationUsingKeyFrames animation,
            double seconds, Color value,
            EasingFunctionBase easingFunction = null)
        {
            var keyFrame = new EasingColorKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value,
                EasingFunction = easingFunction
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static DoubleAnimationUsingKeyFrames AddDiscreteKeyFrame(
            this DoubleAnimationUsingKeyFrames animation,
            double seconds, double value)
        {
            var keyFrame = new DiscreteDoubleKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static PointAnimationUsingKeyFrames AddDiscreteKeyFrame(
            this PointAnimationUsingKeyFrames animation,
            double seconds, Point value)
        {
            var keyFrame = new DiscretePointKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value,
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static ColorAnimationUsingKeyFrames AddDiscreteKeyFrame(
            this ColorAnimationUsingKeyFrames animation,
            double seconds, Color value)
        {
            var keyFrame = new DiscreteColorKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value,
            };
            animation.KeyFrames.Add(keyFrame);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


        public static ObjectAnimationUsingKeyFrames AddDiscreteKeyFrame(
            this ObjectAnimationUsingKeyFrames animation,
            double seconds, object value)
        {
            var keyframe = new DiscreteObjectKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(seconds)),
                Value = value
            };
            animation.KeyFrames.Add(keyframe);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            animation.EnableDependentAnimation = true;
#endif
            return animation;
        }


    }
}

