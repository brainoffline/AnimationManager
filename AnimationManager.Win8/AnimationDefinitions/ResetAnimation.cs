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
    public class ResetAnimation : AnimationDefinition
    {
        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.Opacity)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.Rotation)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.TranslateX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.TranslateY)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddDiscreteKeyFrame(0.0, 1),
                element.AnimateProperty(AnimationProperty.RotationX)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationY)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimateProperty(AnimationProperty.RotationZ)
                    .AddDiscreteKeyFrame(0.0, 0),
                element.AnimatePointProperty(AnimationProperty.RenderTransformOrigin)
                    .AddDiscreteKeyFrame(0.0, new Point(0.5, 0.5)),
            };
        }
    }
}
