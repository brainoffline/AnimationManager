using System;
using System.Collections.Generic;
#if NETFX_CORE
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
                        .AddEasingKeyFrame(0.0, DistanceX)
                        .AddEasingKeyFrame(Duration, 0, new CubicEase()));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                        .AddEasingKeyFrame(0.0, DistanceY)
                        .AddEasingKeyFrame(Duration, 0, new CubicEase()));
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
                           .AddEasingKeyFrame(0.0, 0)
                           .AddEasingKeyFrame(Duration, DistanceX));
            }
            if (Math.Abs(DistanceY) > 0)
            {
                list.Add(
                    element.AnimateProperty(AnimationProperty.TranslateY)
                           .AddEasingKeyFrame(0.0, 0)
                           .AddEasingKeyFrame(Duration, DistanceY));
            }

            return list;
        }
    }


    public class FadeInUpAnimation : AnimationDefinition
    {
        public FadeInUpAnimation()
        {
            Duration = 0.4;
            Distance = 20;
            OpacityFromZero = true;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }


    public class FadeInDownAnimation : AnimationDefinition
    {
        public FadeInDownAnimation()
        {
            Duration = 0.4;
            Distance = 20;
            OpacityFromZero = true;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, -Distance)
                    .AddEasingKeyFrame(Duration, 0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }


    public class FadeInLeftAnimation : AnimationDefinition
    {
        public FadeInLeftAnimation()
        {
            Duration = 0.4;
            Distance = 20;
            OpacityFromZero = true;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, Distance)
                    .AddEasingKeyFrame(Duration, 0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }


    public class FadeInRightAnimation : AnimationDefinition
    {
        public FadeInRightAnimation()
        {
            Duration = 0.4;
            Distance = 20;
            OpacityFromZero = true;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, -Distance)
                    .AddEasingKeyFrame(Duration, 0, new CubicEase()),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, 1),
            };
        }
    }



    public class FadeOutUpAnimation : AnimationDefinition
    {
        public FadeOutUpAnimation()
        {
            Duration = 0.4;
            Distance = 20;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, -Distance),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }


    public class FadeOutDownAnimation : AnimationDefinition
    {
        public FadeOutDownAnimation()
        {
            Duration = 0.4;
            Distance = 20;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0)
            };
        }
    }


    public class FadeOutLeftAnimation : AnimationDefinition
    {
        public FadeOutLeftAnimation()
        {
            Duration = 0.4;
            Distance = 20;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, -Distance),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0)
            };
        }
    }


    public class FadeOutRightAnimation : AnimationDefinition
    {
        public FadeOutRightAnimation()
        {
            Duration = 0.4;
            Distance = 20;
        }

        public double Distance { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddEasingKeyFrame(0.0, 0)
                    .AddEasingKeyFrame(Duration, Distance),
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }

}