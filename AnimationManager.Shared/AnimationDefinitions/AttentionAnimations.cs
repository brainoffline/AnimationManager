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
    public class FlashAnimation : AnimationDefinition
    {
        public FlashAnimation()
        {
            Duration = 1.0;
            Count = 2;
        }

        public int Count { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            if (Count == 0) Count = 2;
            double halfDuration = (Duration/Count/2);
            double position = halfDuration;

            var animation = element.AnimateProperty(AnimationProperty.Opacity)
                .AddEasingKeyFrame(0.0, 1);
            for (int i = 0; i < Count; i++)
            {
                animation.AddEasingKeyFrame(position, 0);
                position += halfDuration;
                animation.AddEasingKeyFrame(position, 1);
                position += halfDuration;
            }
            return new Timeline[] { animation };
        }
    }

    public class FloatAnimation : AnimationDefinition
    {
        public double Distance { get; set; }

        public FloatAnimation()
        {
            Forever = true;
            AutoReverse = true;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration, Distance, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
            };
        }
    }

    public class BounceAnimation : AnimationDefinition
    {
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public BounceAnimation()
        {
            DistanceY = -30;
            DistanceX = 0;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var animations = new List<Timeline>();
            if (Math.Abs(DistanceY) > 0.001)
                animations.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                        .AddEasingKeyFrame(0.0, 0)
                        .AddEasingKeyFrame(Duration*0.2, 0)
                        .AddEasingKeyFrame(Duration*0.4, DistanceY)
                        .AddEasingKeyFrame(Duration*0.5, 0)
                        .AddEasingKeyFrame(Duration*0.6, DistanceY/2)
                        .AddEasingKeyFrame(Duration*0.8, 0)
                        .AddEasingKeyFrame(Duration, 0)
                    );
            if (Math.Abs(DistanceX) > 0.001)
                animations.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                        .AddEasingKeyFrame(0.0, 0)
                        .AddEasingKeyFrame(Duration * 0.2, 0)
                        .AddEasingKeyFrame(Duration * 0.4, DistanceX)
                        .AddEasingKeyFrame(Duration * 0.5, 0)
                        .AddEasingKeyFrame(Duration * 0.6, DistanceX / 2)
                        .AddEasingKeyFrame(Duration * 0.8, 0)
                        .AddEasingKeyFrame(Duration, 0)
                    );
            return animations;
        }
    }

    public class ShakeAnimation : AnimationDefinition
    {
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public ShakeAnimation()
        {
            DistanceX = 10;
            DistanceY = 0;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var animations = new List<Timeline>();

            if (Math.Abs(DistanceX) > 0.001)
                animations.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                        .AddEasingKeyFrame(0.0, 0)
                        .AddEasingKeyFrame(Duration * 0.1, -DistanceX)
                        .AddEasingKeyFrame(Duration * 0.2, DistanceX)
                        .AddEasingKeyFrame(Duration * 0.3, -DistanceX)
                        .AddEasingKeyFrame(Duration * 0.4, DistanceX)
                        .AddEasingKeyFrame(Duration * 0.5, -DistanceX)
                        .AddEasingKeyFrame(Duration * 0.6, DistanceX)
                        .AddEasingKeyFrame(Duration * 0.7, -DistanceX)
                        .AddEasingKeyFrame(Duration * 0.8, DistanceX)
                        .AddEasingKeyFrame(Duration * 0.9, -DistanceX)
                        .AddEasingKeyFrame(Duration, 0)
                    );
            if (Math.Abs(DistanceY) > 0.001)
                animations.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                        .AddEasingKeyFrame(0.0, 0)
                        .AddEasingKeyFrame(Duration * 0.1, -DistanceY)
                        .AddEasingKeyFrame(Duration * 0.2, DistanceY)
                        .AddEasingKeyFrame(Duration * 0.3, -DistanceY)
                        .AddEasingKeyFrame(Duration * 0.4, DistanceY)
                        .AddEasingKeyFrame(Duration * 0.5, -DistanceY)
                        .AddEasingKeyFrame(Duration * 0.6, DistanceY)
                        .AddEasingKeyFrame(Duration * 0.7, -DistanceY)
                        .AddEasingKeyFrame(Duration * 0.8, DistanceY)
                        .AddEasingKeyFrame(Duration * 0.9, -DistanceY)
                        .AddEasingKeyFrame(Duration, 0)
                    );

            return animations;
        }
    }

    public class TadaAnimation : AnimationDefinition
    {
        public double MinScale { get; set; }
        public double MaxScale { get; set; }

        public TadaAnimation()
        {
            MinScale = 0.9;
            MaxScale = 1.1;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.1, MinScale)
                    .AddEasingKeyFrame(Duration*0.2, MinScale)
                    .AddEasingKeyFrame(Duration*0.3, MaxScale)
                    .AddEasingKeyFrame(Duration*0.9, MaxScale)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.1, MinScale)
                    .AddEasingKeyFrame(Duration*0.2, MinScale)
                    .AddEasingKeyFrame(Duration*0.3, MaxScale)
                    .AddEasingKeyFrame(Duration*0.9, MaxScale)
                    .AddEasingKeyFrame(Duration, 1),
                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(Duration*0.1, -3)
                    .AddEasingKeyFrame(Duration*0.2, -3)
                    .AddEasingKeyFrame(Duration*0.3, 3)
                    .AddEasingKeyFrame(Duration*0.4, -3)
                    .AddEasingKeyFrame(Duration*0.5, 3)
                    .AddEasingKeyFrame(Duration*0.6, -3)
                    .AddEasingKeyFrame(Duration*0.7, 3)
                    .AddEasingKeyFrame(Duration*0.8, -3)
                    .AddEasingKeyFrame(Duration*0.9, 3)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class SwingAnimation : AnimationDefinition
    {
        public double Distance { get; set; }

        public SwingAnimation()
        {
            Distance = 15;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.5, 0)),

                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(Duration*0.2, Distance)
                    .AddEasingKeyFrame(Duration*0.4, -(Distance * 0.66))
                    .AddEasingKeyFrame(Duration*0.6, Distance * 0.33)
                    .AddEasingKeyFrame(Duration*0.8, -(Distance * 0.33))
                    .AddEasingKeyFrame(Duration, 0)
            };
        }
    }

    public class WobbleAnimation : AnimationDefinition
    {
        public double Distance { get; set; }

        public WobbleAnimation()
        {
            Distance = 25;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(Duration*0.15, -5)
                    .AddEasingKeyFrame(Duration*0.3, 3)
                    .AddEasingKeyFrame(Duration*0.45, -3)
                    .AddEasingKeyFrame(Duration*0.6, 2)
                    .AddEasingKeyFrame(Duration*0.75, -1)
                    .AddEasingKeyFrame(Duration, 0),
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.15, 0 - Distance)
                    .AddEasingKeyFrame(Duration*0.3, Distance * 0.8)
                    .AddEasingKeyFrame(Duration*0.45, 0 - (Distance * 0.6))
                    .AddEasingKeyFrame(Duration*0.6, Distance * 0.4)
                    .AddEasingKeyFrame(Duration*0.75, 0 - (Distance * 0.2))
                    .AddEasingKeyFrame(Duration, 0)
            };
        }
    }

    public class PulseAnimation : AnimationDefinition
    {
        public double MaxScale { get; set; }

        public PulseAnimation()
        {
            MaxScale = 1.1;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration/2, MaxScale, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration, 1, new QuadraticEase {EasingMode = EasingMode.EaseInOut}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration/2, MaxScale, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration, 1, new QuadraticEase {EasingMode = EasingMode.EaseInOut})
            };
        }
    }

    public class BreathingAnimation : AnimationDefinition
    {
        public double MaxScale { get; set; }

        public BreathingAnimation()
        {
            MaxScale = 1.1;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1, new QuadraticEase())
                    .AddEasingKeyFrame(Duration*0.5, MaxScale, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 1, new QuadraticEase()),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1, new QuadraticEase())
                    .AddEasingKeyFrame(Duration*0.5, MaxScale, new QuadraticEase())
                    .AddEasingKeyFrame(Duration, 1, new QuadraticEase())
            };
        }
    }

    public class JumpAnimation : AnimationDefinition
    {
        public double Distance { get; set; }

        public JumpAnimation()
        {
            Distance = -20;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.5, 1)),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.2, 0.6)
                    .AddEasingKeyFrame(Duration*0.4, 1.2)
                    .AddEasingKeyFrame(Duration*0.8, 1)
                    .AddEasingKeyFrame(Duration*1.0, 0.6)
                    .AddEasingKeyFrame(Duration*1.2, 1),
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration*0.2, 0)
                    .AddEasingKeyFrame(Duration*0.4, Distance)
                    .AddEasingKeyFrame(Duration*0.6, Distance)
                    .AddEasingKeyFrame(Duration*0.8, Distance/2)
                    .AddEasingKeyFrame(Duration*1.0, 0),
            };
        }
    }

}
