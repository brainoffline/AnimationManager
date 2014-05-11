using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
#endif

#if WINDOWS_PHONE
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
#endif


namespace Brain.Animate
{
    public class FrameFadeBackAnimation : AnimationDefinition
    {
        public FrameFadeBackAnimation()
        {
            Duration = 0.4;
            HorizontalCentre = 0.5;
        }

        public Double HorizontalCentre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(HorizontalCentre, 0.5)),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/4, 1)
                    .AddEasingKeyFrame(Duration, 0.7),
            };
        }
    }

    public class FrameFadeInAnimation : AnimationDefinition
    {
        public FrameFadeInAnimation()
        {
            Duration = 0.4;
            HorizontalCentre = 0.5;
        }

        public Double HorizontalCentre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(HorizontalCentre, 0.5)),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0.7)
                    .AddEasingKeyFrame(Duration/4, 0.7)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }


    public class FrameMoveInLeftAnimation : AnimationDefinition
    {
        public FrameMoveInLeftAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
        }

        public bool JumpUp { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, 0.5)),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1.0)
                    .AddEasingKeyFrame(Duration/2, JumpUp ? 0.95 : 1.05)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase()),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1.0)
                    .AddEasingKeyFrame(Duration/2, JumpUp ? 0.95 : 1.05)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase()),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, width)
                    .AddEasingKeyFrame(Duration, 0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/4, 1),
            };
        }
    }

    public class FrameMoveOutRightAnimation : AnimationDefinition
    {
        public FrameMoveOutRightAnimation()
        {
            Duration = 0.4;
        }
        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, width*1.1, new CubicEase()),
                /*
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/4, 0),*/
            };
        }
    }

    public class SwapLeftOutAnimation : AnimationDefinition
    {
        public SwapLeftOutAnimation()
        {
            Duration = 0.5;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, -width/2, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase {EasingMode = EasingMode.EaseInOut}),

                element.AnimateObjectProperty(AnimationProperty.ZIndex)
                    .AddDiscreteKeyFrame(Duration/2, 0),

                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration/2, -25, new QuadraticEase {EasingMode = EasingMode.EaseOut})
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
            };
        }
    }

    public class SwapRightOutAnimation : AnimationDefinition
    {
        public SwapRightOutAnimation()
        {
            Duration = 0.5;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, width/2, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase {EasingMode = EasingMode.EaseInOut}),

                element.AnimateObjectProperty(AnimationProperty.ZIndex)
                    .AddDiscreteKeyFrame(Duration/2, 0),

                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration/2, -25, new QuadraticEase {EasingMode = EasingMode.EaseOut})
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0.95), //, new CubicEase { EasingMode = EasingMode.EaseIn });
            };
        }
    }

    public class SwapRightInAnimation : AnimationDefinition
    {
        public SwapRightInAnimation()
        {
            Duration = 0.5;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, width/2, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase()),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1),

                element.AnimateObjectProperty(AnimationProperty.ZIndex)
                    .AddDiscreteKeyFrame(Duration/2, 1),

                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration/2, 5, new QuadraticEase {EasingMode = EasingMode.EaseOut})
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration/2, 0.95)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration/2, 0.95)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }

    public class SwapLeftInAnimation : AnimationDefinition
    {
        public SwapLeftInAnimation()
        {
            Duration = 0.5;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            var frame = Window.Current.Content as Frame;
            var width = (frame != null) ? frame.ActualWidth : 1024;
#elif WINDOWS_PHONE
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;
            var width = (frame != null) ? frame.ActualWidth : 250;
#endif

            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, -width/2, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase()),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1),

                element.AnimateObjectProperty(AnimationProperty.ZIndex)
                    .AddDiscreteKeyFrame(Duration/2, 1),

                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration/2, 5, new QuadraticEase {EasingMode = EasingMode.EaseOut})
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration/2, 0.95)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 0.95)
                    .AddEasingKeyFrame(Duration/2, 0.95)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }


}
