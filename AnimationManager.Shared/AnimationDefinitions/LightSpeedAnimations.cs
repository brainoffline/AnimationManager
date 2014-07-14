using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Media.Animation;
#endif

namespace Brain.Animate
{
    public class LightSpeedInRightAnimation : AnimationDefinition
    {
        public LightSpeedInRightAnimation()
        {
            OpacityFromZero = true;
        }
        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0, 1)),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.6, 1),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, transform.TranslateX + 700)
                    .AddEasingKeyFrame(Duration*0.6, transform.TranslateX - 30, new QuadraticEase())
                    .AddEasingKeyFrame(Duration*0.8, transform.TranslateX),

                element.AnimateProperty(AnimationProperty.SkewX)
                    .AddEasingKeyFrame(0.0, -30)
                    .AddEasingKeyFrame(Duration*0.6, 30)
                    .AddEasingKeyFrame(Duration*0.8, -15)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class LightSpeedOutRightAnimation : AnimationDefinition
    {
        public LightSpeedOutRightAnimation()
        {
            Duration = 0.6;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0, 1)),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, transform.TranslateX)
                    .AddEasingKeyFrame(Duration, transform.TranslateX + 700),

                element.AnimateProperty(AnimationProperty.SkewX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, -30)
                    .AddEasingKeyFrame(Duration + 0.01, 0),
            };
        }
    }


    public class LightSpeedInLeftAnimation :AnimationDefinition
    {
        public LightSpeedInLeftAnimation()
        {
            OpacityFromZero = true;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0, 1)),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, transform.TranslateX - 700)
                    .AddEasingKeyFrame(Duration*0.6, transform.TranslateX + 30, new QuadraticEase())
                    .AddEasingKeyFrame(Duration*0.8, transform.TranslateX),

                element.AnimateProperty(AnimationProperty.SkewX)
                    .AddEasingKeyFrame(0.0, 30)
                    .AddEasingKeyFrame(Duration*0.6, -30)
                    .AddEasingKeyFrame(Duration*0.8, 15)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class LightSpeedOutLeftAnimation : AnimationDefinition
    {
        public LightSpeedOutLeftAnimation()
        {
            Duration = 0.6;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0, 1)),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),

                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, transform.TranslateX)
                    .AddEasingKeyFrame(Duration, transform.TranslateX - 700),

                element.AnimateProperty(AnimationProperty.SkewX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 30)
                    .AddEasingKeyFrame(Duration + 0.01, 0),
            };
        }
    }

}
