using System.Collections.Generic;
using Windows.UI.Xaml.Media;
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
    public abstract class AnimationDefinition
    {
        public double Duration { get; set; }
        public double PauseBefore { get; set; }
        public double PauseAfter { get; set; }
        public double SpeedRatio { get; set; }
        public double RepeatCount { get; set; }
        public double RepeatDuration { get; set; }
        public double Delay { get; set; }
        public bool AutoReverse { get; set; }
        public bool Forever { get; set; }

        public bool OpacityFromZero { get; protected set; }

        protected AnimationDefinition()
        {
            Duration = 1.0;
        }

        protected CompositeTransform GetTransform(FrameworkElement element)
        {
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
                element.RenderTransform = transform = new CompositeTransform();
            return transform;
        }

        protected PlaneProjection GetProjection(FrameworkElement element)
        {
            var projection = element.Projection as PlaneProjection;
            if (projection == null)
                element.Projection = projection = new PlaneProjection();
            return projection;
        }

        public abstract IEnumerable<Timeline> CreateAnimation(FrameworkElement element);
    }
}