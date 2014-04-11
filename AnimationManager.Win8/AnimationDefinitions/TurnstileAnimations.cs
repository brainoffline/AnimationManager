using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Media.Animation;
#endif

namespace Brain.Animate
{
    public class TurnstileLeftOutAnimation : AnimationDefinition
    {
        public TurnstileLeftOutAnimation()
        {
            Duration = 0.3;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration, -80, new QuadraticEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.8, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class TurnstileLeftInAnimation : AnimationDefinition
    {
        public TurnstileLeftInAnimation()
        {
            Duration = 0.3;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 80)
                    .AddEasingKeyFrame(Duration, 0, new QuadraticEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1),
            };
        }
    }

    public class TurnstileRightInAnimation : AnimationDefinition
    {
        public TurnstileRightInAnimation()
        {
            Duration = 0.3;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, -80)
                    .AddEasingKeyFrame(Duration, 0, new QuadraticEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.2, 1),
            };
        }
    }

    public class TurnstileRightOutAnimation : AnimationDefinition
    {
        public TurnstileRightOutAnimation()
        {
            Duration = 0.3;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0, 0)
                    .AddEasingKeyFrame(Duration, 80, new QuadraticEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration - 0.001, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }
}


