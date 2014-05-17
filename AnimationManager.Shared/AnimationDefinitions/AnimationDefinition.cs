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

        public abstract IEnumerable<Timeline> CreateAnimation(FrameworkElement element);
    }
}