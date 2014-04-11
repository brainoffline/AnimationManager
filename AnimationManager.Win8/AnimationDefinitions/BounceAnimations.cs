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
    public enum ZDirection
    {
        Away, Closer
    }



    public class BounceInAnimation : AnimationDefinition
    {
        public ZDirection FromDirection { get; set; }
        public double Amplitude { get; set; }
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public BounceInAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
            FromDirection = ZDirection.Away;
            Amplitude = 0.4;
            DistanceX = 0;
            DistanceY = 0;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, (FromDirection == ZDirection.Away ? 0.3 : 2.0))
                    .AddEasingKeyFrame(Duration, 1, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, (FromDirection == ZDirection.Away ? 0.3 : 2.0))
                    .AddEasingKeyFrame(Duration, 1, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, 1),
            };
            if (Math.Abs(DistanceX) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, DistanceX)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, DistanceY)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}));
            }

            return list;
        }
    }

    public class BounceInUpAnimation : AnimationDefinition
    {
        public double Distance { get; set; }
        public double Amplitude { get; set; }

        public BounceInUpAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
            Distance = 500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, 1),
            };
        }
    }

    public class BounceInDownAnimation : AnimationDefinition
    {
        public double Distance { get; set; }
        public double Amplitude { get; set; }

        public BounceInDownAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
            Distance = -500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/2, 1),
            };
        }
    }

    public class BounceInLeftAnimation : AnimationDefinition
    {
        public double Distance { get; set; }
        public double Amplitude { get; set; }

        public BounceInLeftAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
            Distance = 500;
            Amplitude = 0.4;
        }
        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }

    public class BounceInRightAnimation : AnimationDefinition
    {
        public double Distance { get; set; }
        public double Amplitude { get; set; }

        public BounceInRightAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
            Distance = -500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new BackEase {Amplitude = Amplitude}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }

    public class BounceOutAnimation : AnimationDefinition
    {
        public ZDirection ToDirection { get; set; }
        public double Amplitude { get; set; }
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public BounceOutAnimation()
        {
            Duration = 0.4;
            ToDirection = ZDirection.Away;
            Amplitude = 0.4;
            DistanceX = 0;
            DistanceY = 0;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, (ToDirection == ZDirection.Away ? 0.3 : 2.0), new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, (ToDirection == ZDirection.Away ? 0.3 : 2.0), new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0)
            };
            if (Math.Abs(DistanceX) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, DistanceX, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, DistanceY, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}));
            }
            return list;
        }
    }

    public class BounceOutUpAnimation : AnimationDefinition
    {
        public double Amplitude { get; set; }
        public double Distance { get; set; }

        public BounceOutUpAnimation()
        {
            Duration = 0.4;
            Distance = -500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class BounceOutDownAnimation : AnimationDefinition
    {
        public double Amplitude { get; set; }
        public double Distance { get; set; }

        public BounceOutDownAnimation()
        {
            Duration = 0.4;
            Distance = 500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class BounceOutLeftAnimation : AnimationDefinition
    {
        public double Amplitude { get; set; }
        public double Distance { get; set; }

        public BounceOutLeftAnimation()
        {
            Duration = 0.4;
            Distance = -500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

    public class BounceOutRightAnimation : AnimationDefinition
    {
        public double Amplitude { get; set; }
        public double Distance { get; set; }

        public BounceOutRightAnimation()
        {
            Duration = 0.4;
            Distance = 500;
            Amplitude = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration/2, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

}
