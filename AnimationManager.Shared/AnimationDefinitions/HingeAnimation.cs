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
    public enum Side
    {
        Left, Right
    }

    public class HingeAnimation : AnimationDefinition
    {
        public Side Side { get; set; }
        public double Distance { get; set; }

        public HingeAnimation()
        {
            Duration = 1.0;
            Side = Side.Left;
            Distance = 700;
        }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            var transform = GetTransform(element);

            if (Side == Side.Left)
                return new Timeline[]
                {
                    element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                        .AddDiscreteKeyFrame(0.0, new Point(0,0)),

                    element.AnimateProperty(AnimationProperty.Rotation)
                        .AddEasingKeyFrame(0.0, 0, new CubicEase {EasingMode = EasingMode.EaseInOut})
                        .AddEasingKeyFrame(Duration*0.2, 80, new CubicEase {EasingMode = EasingMode.EaseInOut})
                        .AddEasingKeyFrame(Duration*0.4, 60, new CubicEase {EasingMode = EasingMode.EaseInOut})
                        .AddEasingKeyFrame(Duration*0.6, 80, new CubicEase {EasingMode = EasingMode.EaseInOut})
                        .AddEasingKeyFrame(Duration*0.8, 60, new CubicEase {EasingMode = EasingMode.EaseInOut})
                        .AddEasingKeyFrame(Duration, 70),

                    element.AnimateProperty(AnimationProperty.TranslateY)
                        .AddEasingKeyFrame(Duration*0.7, transform.TranslateY)
                        .AddEasingKeyFrame(Duration, transform.TranslateY + Distance, new CubicEase { EasingMode = EasingMode.EaseIn}),

                    element.AnimateProperty(AnimationProperty.Opacity)
                        .AddEasingKeyFrame(0.0, 1)
                        .AddEasingKeyFrame(Duration*0.9, 1)
                        .AddEasingKeyFrame(Duration, 0),
                };
            return new Timeline[]
            {
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(1,0)),

                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddEasingKeyFrame(0.0, 0, new CubicEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration*0.2, -80, new CubicEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration*0.4, -60, new CubicEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration*0.6, -80, new CubicEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration*0.8, -60, new CubicEase {EasingMode = EasingMode.EaseInOut})
                    .AddEasingKeyFrame(Duration,-70),

                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddEasingKeyFrame(Duration*0.7, transform.TranslateY)
                    .AddEasingKeyFrame(Duration, transform.TranslateY + Distance, new CubicEase { EasingMode = EasingMode.EaseIn}),

                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddEasingKeyFrame(0.0, 1)
                    .AddEasingKeyFrame(Duration*0.9, 1)
                    .AddEasingKeyFrame(Duration, 0),
            };
        }
    }
}
