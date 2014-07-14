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
        Away, Closer, Steady
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
            var transform = GetTransform(element);

            var list = new List<Timeline>();

            if (FromDirection != ZDirection.Steady)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.ScaleX)
                        .AddEasingKeyFrame(0.0, (FromDirection == ZDirection.Away ? 0.3 : 2.0))
                        .AddEasingKeyFrame(Duration, 1, new BackEase {Amplitude = Amplitude}));
                list.Add(
                    element.AnimateProperty(AnimationProperty.ScaleY)
                        .AddEasingKeyFrame(0.0, (FromDirection == ZDirection.Away ? 0.3 : 2.0))
                        .AddEasingKeyFrame(Duration, 1, new BackEase {Amplitude = Amplitude}));
            };
            list.Add(
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration/4, 1));
            if (Math.Abs(DistanceX) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, transform.TranslateX + DistanceX)
                    .AddEasingKeyFrame(Duration, transform.TranslateX, new BackEase {Amplitude = Amplitude}));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, transform.TranslateY + DistanceY)
                    .AddEasingKeyFrame(Duration, transform.TranslateY, new BackEase {Amplitude = Amplitude}));
            }

            return list;
        }
    }

    public class BounceInUpAnimation : BounceInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public BounceInUpAnimation()
        {
            Distance = 500;
        }
    }

    public class BounceInDownAnimation : BounceInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public BounceInDownAnimation()
        {
            Distance = -500;
        }
    }

    public class BounceInLeftAnimation : BounceInAnimation
    {
        public double Distance
        {
            get { return DistanceX; }
            set { DistanceX = value; }
        }

        public BounceInLeftAnimation()
        {
            Distance = 500;
        }
    }

    public class BounceInRightAnimation : BounceInAnimation
    {
        public double Distance
        {
            get { return DistanceX; }
            set { DistanceX = value; }
        }

        public BounceInRightAnimation()
        {
            Distance = -500;
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
            var transform = GetTransform(element);

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
                    //.AddEasingKeyFrame(0.0, transform.TranslateX)
                    .AddEasingKeyFrame(Duration, transform.TranslateX + DistanceX, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                    //.AddEasingKeyFrame(0.0, transform.TranslateY)
                    .AddEasingKeyFrame(Duration, transform.TranslateY + DistanceY, new BackEase {Amplitude = Amplitude, EasingMode = EasingMode.EaseIn}));
            }
            return list;
        }
    }

    public class BounceOutUpAnimation : BounceOutAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public BounceOutUpAnimation()
        {
            Distance = -500;
        }
    }

    public class BounceOutDownAnimation : BounceOutAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public BounceOutDownAnimation()
        {
            Distance = 500;
        }
    }

    public class BounceOutLeftAnimation : BounceOutAnimation
    {
        public double Distance
        {
            get { return DistanceX; }
            set { DistanceX = value; }
        }

        public BounceOutLeftAnimation()
        {
            Distance = -500;
        }
    }

    public class BounceOutRightAnimation : BounceOutAnimation
    {
        public double Distance
        {
            get { return DistanceX; }
            set { DistanceX = value; }
        }

        public BounceOutRightAnimation()
        {
            Distance = 500;
        }
    }

}
