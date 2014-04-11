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

    public class FlipAnimation : AnimationDefinition
    {
        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.4, 170)
                    .AddEasingKeyFrame(Duration*0.5, 190)
                    .AddEasingKeyFrame(Duration*0.8, 360)
                    .AddEasingKeyFrame(Duration, 360),
                element.AnimateProperty(AnimationProperty.GlobalOffsetZ)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.4, 150)
                    .AddEasingKeyFrame(Duration*0.5, 150)
                    .AddEasingKeyFrame(Duration*0.8, 0)
                    .AddEasingKeyFrame(Duration, 0),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.5, 1)
                    .AddEasingKeyFrame(Duration*0.8, 1.1)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.5, 1)
                    .AddEasingKeyFrame(Duration*0.8, 1.1)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }

    public class FlipInXAnimation : AnimationDefinition
    {
        public FlipInXAnimation()
        {
            OpacityFromZero = true;
        }

        public bool Reverse { get; set; }
        public Double Centre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationY)
                    .AddDiscreteKeyFrame(0.0, Centre),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase())
            };

            if (Reverse)
            {
                list.Add(element.AnimateProperty(AnimationProperty.RotationX)
                    .AddEasingKeyFrame(0.0, -90)
                    .AddEasingKeyFrame(Duration*0.4, 10)
                    .AddEasingKeyFrame(Duration*0.7, -10)
                    .AddEasingKeyFrame(Duration, 0));
            }
            else
                list.Add(element.AnimateProperty(AnimationProperty.RotationX)
                    .AddEasingKeyFrame(0.0, 90)
                    .AddEasingKeyFrame(Duration*0.4, -10)
                    .AddEasingKeyFrame(Duration*0.7, 10)
                    .AddEasingKeyFrame(Duration, 0));
            return list;
        }
    }

    public class FlipOutXAnimation : AnimationDefinition
    {
        public FlipOutXAnimation()
        {
            Duration = 0.4;
        }

        public bool Reverse { get; set; }
        public Double Centre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationY)
                    .AddDiscreteKeyFrame(0.0, Centre),
                element.AnimateProperty(AnimationProperty.RotationX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Reverse ? 90 : -90, new QuarticEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class FlipInYAnimation : AnimationDefinition
    {
        public FlipInYAnimation()
        {
            OpacityFromZero = true;
        }

        public bool Reverse { get; set; }
        public Double Centre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, Centre),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1, new CubicEase()),
            };

            if (Reverse)
            {
                list.Add(element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0.0, -90)
                    .AddEasingKeyFrame(Duration*0.4, 10)
                    .AddEasingKeyFrame(Duration*0.7, -10)
                    .AddEasingKeyFrame(Duration, 0));
            }
            else
                list.Add(element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0.0, 90)
                    .AddEasingKeyFrame(Duration*0.4, -10)
                    .AddEasingKeyFrame(Duration*0.7, 10)
                    .AddEasingKeyFrame(Duration, 0));
            return list;
        }
    }

    public class FlipOutYAnimation : AnimationDefinition
    {
        public FlipOutYAnimation()
        {
            Duration = 0.4;
        }

        public bool Reverse { get; set; }
        public Double Centre { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.CentreOfRotationX)
                    .AddDiscreteKeyFrame(0.0, Centre),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Reverse ? 90 : -90, new QuarticEase {EasingMode = EasingMode.EaseOut}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

}
