using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#endif
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
#endif

namespace Brain.Animate
{
    public enum RotateAnimationDirection
    {
        RotateUp, RotateDown
    }

    public class RotateAnimation : AnimationDefinition
    {
        public Double? StartRotation { get; set; }
        public Double? EndRotation { get; set; }
        public EasingFunctionBase Easing { get; set; }

        public RotateAnimation()
        {
            Duration = 1.0;
            StartRotation = 0.0;
            Easing = new QuadraticEase();
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            double startRotation = 0.0;
            double endRotation;

            if (StartRotation.HasValue)
                startRotation = StartRotation.Value;
            else
            {
                var compositeTransform = element.RenderTransform as CompositeTransform;
                if (compositeTransform != null)
                    startRotation = compositeTransform.Rotation;
            }
            if (EndRotation.HasValue)
                endRotation = EndRotation.Value;
            else
                endRotation = startRotation + 360.0;

            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.5, 0.5)),

                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, startRotation)
                    .AddEasingKeyFrame(Duration, endRotation, Easing)
                    .AddDiscreteKeyFrame(Duration, startRotation),
            };
        }
    }

    public class RotateInAnimation : AnimationDefinition
    {
        public RotateAnimationDirection RotateDirection;

        public RotateInAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>();

            if (RotateDirection == RotateAnimationDirection.RotateUp)
            {
                list.Add(element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, 1.5)));
                list.Add(element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, 45)
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase()));
            }
            else
            {
                list.Add(element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, -0.5)));
                list.Add(element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, -45)
                    .AddEasingKeyFrame(Duration, 0.0, new QuadraticEase()));
            }
            list.Add(element.AnimateProperty(AnimationProperty.Opacity)
                .AddEasingKeyFrame(0.0, 0)
                .AddEasingKeyFrame(Duration/4, 1, new QuadraticEase())
                .AddEasingKeyFrame(Duration, 1));

            return list;
        }
    }

    public class RotateOutAnimation : AnimationDefinition
    {
        public RotateAnimationDirection RotateDirection;

        public RotateOutAnimation()
        {
            Duration = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>();
            if (RotateDirection == RotateAnimationDirection.RotateUp)
            {
                list.Add(element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, 1.5)));
                list.Add(element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 45, new QuadraticEase {EasingMode = EasingMode.EaseOut}));
            }
            else
            {
                list.Add(element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.0, -0.5)));
                list.Add(element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, -45, new QuadraticEase {EasingMode = EasingMode.EaseOut}));
            }
            list.Add(element.AnimateProperty(AnimationProperty.Opacity)
                .AddEasingKeyFrame(0.0, 1)
                .AddEasingKeyFrame(Duration/2, 1)
                .AddEasingKeyFrame(Duration, 0));
            return list;
        }
    }

}
