using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
#endif

namespace Brain.Animate
{
    public class ScaleInAnimation : AnimationDefinition
    {
        public double StartScale = 0.7;

        public ScaleInAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, StartScale)
                    .AddEasingKeyFrame(Duration, 1.0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, StartScale)
                    .AddEasingKeyFrame(Duration, 1.0, new CubicEase()),
            };
        }
    }

    public class ScaleOutAnimation : AnimationDefinition
    {
        public double EndScale = 0.7;

        public ScaleOutAnimation()
        {
            Duration = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, EndScale, new CubicEase {EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, EndScale, new CubicEase {EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }


#if WINDOWS_PHONE
    public class ScaleFromElementAnimation : AnimationDefinition
    {
        public FrameworkElement FromElement;

        public ScaleFromElementAnimation()
        {
            OpacityFromZero = true;
            Duration = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var frame = Application.Current.RootVisual as Frame;
            if (frame == null) return new Timeline[] {};
            if (element == null) return new Timeline[] {};

            var ttv = FromElement.TransformToVisual(Application.Current.RootVisual);
            var screenCoords = ttv.Transform(new Point(0, 0));
            var size = FromElement.RenderSize;
            var fromScaleX = size.Width/frame.ActualWidth;
            var fromScaleY = size.Height/frame.ActualHeight;

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, 0.0)),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, screenCoords.X)
                    .AddEasingKeyFrame(Duration, 0, new QuadraticEase()),
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, screenCoords.Y)
                    .AddEasingKeyFrame(Duration, 0, new QuadraticEase()),

                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, fromScaleX)
                    .AddEasingKeyFrame(Duration, 1.0, new QuadraticEase()),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, fromScaleY)
                    .AddEasingKeyFrame(Duration, 1.0, new QuadraticEase()),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/4, 1)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }
#endif

}
