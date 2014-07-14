using System;
using System.Collections.Generic;

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
    public class FadeInAnimation : AnimationDefinition
    {
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public FadeInAnimation()
        {
            Duration = 0.4;
            OpacityFromZero = true;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
            if (Math.Abs(DistanceX) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                        .AddEasingKeyFrame(0.0, transform.TranslateX + DistanceX)
                        .AddEasingKeyFrame(Duration, transform.TranslateX, new CubicEase()));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                        .AddEasingKeyFrame(0.0, transform.TranslateY + DistanceY)
                        .AddEasingKeyFrame(Duration, transform.TranslateY, new CubicEase()));
            }

            return list;
        }
    }

    public class FadeOutAnimation : AnimationDefinition
    {
        public double DistanceX { get; set; }
        public double DistanceY { get; set; }

        public FadeOutAnimation()
        {
            Duration = 0.4;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            var list = new List<Timeline>
            {
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };

            if (Math.Abs(DistanceX) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateX)
                           .AddEasingKeyFrame(0.0, transform.TranslateX)
                           .AddEasingKeyFrame(Duration, transform.TranslateX + DistanceX));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                           .AddEasingKeyFrame(0.0, transform.TranslateY)
                           .AddEasingKeyFrame(Duration, transform.TranslateY + DistanceY));
            }

            return list;
        }
    }


    public class FadeInUpAnimation : FadeInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeInUpAnimation()
        {
            Distance = 20;
        }
    }


    public class FadeInDownAnimation : FadeInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeInDownAnimation()
        {
            Distance = -20;
        }
    }


    public class FadeInLeftAnimation : FadeInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeInLeftAnimation()
        {
            Distance = 20;
        }
    }


    public class FadeInRightAnimation : FadeInAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeInRightAnimation()
        {
            Distance = -20;
        }
    }



    public class FadeOutUpAnimation : FadeOutAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeOutUpAnimation()
        {
            Distance = -20;
        }
    }


    public class FadeOutDownAnimation : FadeOutAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeOutDownAnimation()
        {
            Distance = 20;
        }
    }


    public class FadeOutLeftAnimation : FadeOutAnimation
    {
        public double Distance
        {
            get { return DistanceX; }
            set { DistanceX = value; }
        }

        public FadeOutLeftAnimation()
        {
            Distance = -20;
        }
    }


    public class FadeOutRightAnimation : FadeOutAnimation
    {
        public double Distance
        {
            get { return DistanceY; }
            set { DistanceY = value; }
        }

        public FadeOutRightAnimation()
        {
            Distance = 20;
        }
    }

}