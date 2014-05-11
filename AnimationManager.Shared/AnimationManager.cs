using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.Foundation;
using Point = Windows.Foundation.Point;
#endif

#if WINDOWS_PHONE
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Point = System.Windows.Point;
#endif



namespace Brain.Animate
{
    public class AnimationParameters
    {
        public double SpeedRatio;
        public double RepeatCount;
        public double RepeatDuration;
        public double Delay;
        public bool AutoReverse;
        public bool Forever;
        public bool OpacityFromZero;

        public AnimationParameters()
        { }

        public AnimationParameters(string animationName)
        {
            if (animationName.Contains('{'))
                animationName = animationName.Replace("{", "");
            if (animationName.Contains('}'))
                animationName = animationName.Replace("}", "");

            var values = animationName.Split(',');

            SpeedRatio = 0;
            RepeatCount = 0;
            RepeatDuration = 0;
            Delay = 0;
            AutoReverse = false;
            Forever = false;

            for (int i = 0; i < values.Count(); i++)
            {
                var keyvalue = values[i].Split('=');
                if (keyvalue.Length != 2) continue;

                switch (keyvalue[0].ToLower())
                {
                    case "speedratio":
                        Double.TryParse(keyvalue[1], out SpeedRatio);
                        break;
                    case "repeatcount":
                        Double.TryParse(keyvalue[1], out RepeatCount);
                        break;
                    case "repeatduration":
                        Double.TryParse(keyvalue[1], out RepeatDuration);
                        break;
                    case "forever":
                        bool.TryParse(keyvalue[1], out Forever);
                        break;
                    case "autoreverse":
                        bool.TryParse(keyvalue[1], out AutoReverse);
                        break;
                    case "delay":
                        Double.TryParse(keyvalue[1], out Delay);
                        break;
                    case "opacityfromzero":
                        bool.TryParse(keyvalue[1], out OpacityFromZero);
                        break;
                }
            }
        }
    }


    public class AnimationManager
    {
        public delegate IEnumerable<Timeline> CreateAnimation(FrameworkElement element);

        private readonly Dictionary<FrameworkElement, List<Storyboard>> _frameworkStoryboards = new Dictionary<FrameworkElement, List<Storyboard>>();

        private static AnimationManager _manager;
        private static AnimationManager Manager
        {
            get { return _manager ?? (_manager = new AnimationManager()); }
        }


        private static bool RegisteredSplashScreen;
        private static ManualResetEvent _splashScreenFinished;

        public static void RegisterSplashScreen(SplashScreen splashScreen)
        {
#if NETFX_CORE || WINDOWS_81_PORTABLE
            lock (Manager)
            {
                _splashScreenFinished = new ManualResetEvent(false);
                splashScreen.Dismissed += (sender, args) =>
                {
                    RegisteredSplashScreen = false;
                    _splashScreenFinished.Set();
                };
                RegisteredSplashScreen = true;
            }
#endif

#if WINDOWS_PHONE
            lock (Manager)
            {
                _splashScreenFinished = new ManualResetEvent(false);
                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.0));
                    RegisteredSplashScreen = false;
                    _splashScreenFinished.Set();
                });
                RegisteredSplashScreen = true;
            }
#endif
        }

        public static Task<bool> SplashScreenGone()
        {
            var tcs = new TaskCompletionSource<bool>();

            if (!RegisteredSplashScreen)
                tcs.SetResult(false);
            else
            {
                Task.Run(() =>
                {
                    _splashScreenFinished.WaitOne();
                    tcs.SetResult(true);
                });
            }
            return tcs.Task;
        }

        private void AddStoryboard(FrameworkElement element, Storyboard storyboard)
        {
            lock (this)
            {
                List<Storyboard> storyboards = 
                    _frameworkStoryboards.ContainsKey(element) ? _frameworkStoryboards[element] : new List<Storyboard>();
                storyboards.Add(storyboard);
                _frameworkStoryboards[element] = storyboards;
            }
        }

        private void RemoveStoryboard(FrameworkElement element, Storyboard storyboard)
        {
            lock (this)
            {
                var storyboards = _frameworkStoryboards.ContainsKey(element) ? _frameworkStoryboards[element] : null;
                if (storyboards == null) return;

                storyboards.Remove(storyboard);
                if (storyboards.Count == 0)
                    _frameworkStoryboards.Remove(element);
            }
        }

        private void StopStoryboards(FrameworkElement element)
        {
            lock (this)
            {
                var storyboards = _frameworkStoryboards.ContainsKey(element) ? _frameworkStoryboards[element] : null;
                if (storyboards == null) return;

                foreach (var storyboard in storyboards.ToArray())
                {
                    storyboard.Stop();
                    storyboards.Remove(storyboard);
                }
                _frameworkStoryboards.Remove(element);
            }
        }

        private void PauseStoryboards(FrameworkElement element)
        {
            lock (this)
            {
                var storyboards = _frameworkStoryboards.ContainsKey(element) ? _frameworkStoryboards[element] : null;
                if (storyboards == null) return;

                foreach (var storyboard in storyboards.ToArray())
                {
                    storyboard.Pause();
                    storyboards.Remove(storyboard);
                }
                _frameworkStoryboards.Remove(element);
            }
        }

        public bool ContainsStoryboard(FrameworkElement element)
        {
            return _frameworkStoryboards.ContainsKey(element);
        }


        public static Storyboard AnimationStoryboard(
            FrameworkElement element,
            AnimationDefinition animationDefinition,
            Action completedAction = null)
        {
            var animations = animationDefinition.CreateAnimation(element);

            PrepareElement(element);

            var sb = new Storyboard();
            foreach (var animation in animations)
            {
                if (animationDefinition.PauseBefore > 0)
                    animation.BeginTime = TimeSpan.FromSeconds(animationDefinition.PauseBefore);

                Storyboard.SetTarget(animation, element);
                sb.Children.Add(animation);
            }

            if ((animationDefinition.PauseBefore > 0.0) || 
                (animationDefinition.PauseAfter > 0.0))
            {
                sb.Duration = new Duration(TimeSpan.FromSeconds(
                    animationDefinition.PauseBefore + 
                    animationDefinition.Duration + 
                    animationDefinition.PauseAfter));
            }

            sb.Completed += (sender, o) =>
            {
                Manager.RemoveStoryboard(element, sb);
                if (completedAction != null)
                    completedAction();
            };

            Manager.AddStoryboard(element, sb);

            if (animationDefinition.SpeedRatio > 0.0)
                sb.SpeedRatio = animationDefinition.SpeedRatio;
            if (animationDefinition.RepeatCount > 0)
                sb.RepeatBehavior = new RepeatBehavior(animationDefinition.RepeatCount);
            if (animationDefinition.RepeatDuration > 0)
                sb.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(animationDefinition.RepeatDuration));
            if (animationDefinition.Forever)
                sb.RepeatBehavior = RepeatBehavior.Forever;
            sb.AutoReverse = animationDefinition.AutoReverse;

            if (animationDefinition.OpacityFromZero)
                element.Opacity = 0;

            if (animationDefinition.Delay > 0)
                sb.BeginTime = TimeSpan.FromSeconds(animationDefinition.Delay);

            return sb;
        }

        public static Storyboard AnimationStoryboard(
            FrameworkElement element, 
            IEnumerable<Timeline> animations, 
            AnimationParameters animationParameters = null, 
            Action completedAction = null)
        {
            PrepareElement(element);

            var sb = new Storyboard();
            foreach (var animation in animations)
            {
                Storyboard.SetTarget(animation, element);
                sb.Children.Add(animation);
            }

            sb.Completed += (sender, o) =>
            {
                Manager.RemoveStoryboard(element, sb);
                if (completedAction != null)
                    completedAction();
            };

            Manager.AddStoryboard(element, sb);

            if (animationParameters != null)
            {
                if (animationParameters.SpeedRatio > 0.0)
                    sb.SpeedRatio = animationParameters.SpeedRatio;
                if (animationParameters.RepeatCount > 0)
                    sb.RepeatBehavior = new RepeatBehavior(animationParameters.RepeatCount);
                if (animationParameters.RepeatDuration > 0)
                    sb.RepeatBehavior = new RepeatBehavior(TimeSpan.FromSeconds(animationParameters.RepeatDuration));
                if (animationParameters.Forever)
                    sb.RepeatBehavior = RepeatBehavior.Forever;
                sb.AutoReverse = animationParameters.AutoReverse;

                if (animationParameters.OpacityFromZero)
                    element.Opacity = 0;

                if (animationParameters.Delay > 0)
                    sb.BeginTime = TimeSpan.FromSeconds(animationParameters.Delay);
            }

            return sb;
        }

        public static bool IsAnimating(FrameworkElement element)
        {
            return Manager.ContainsStoryboard(element);
        }

        public static void StopAnimations(FrameworkElement element)
        {
            Manager.StopStoryboards(element);
        }

        public static void PauseAnimations(FrameworkElement element)
        {
            Manager.PauseStoryboards(element);
        }


        /// <summary>
        /// This ensures the element has the appropriate projection and render transformation controls that are used in the pre-defined animations
        /// </summary>
        public static void PrepareElement(FrameworkElement element)
        {
            if (!(element.Projection is PlaneProjection))
                element.Projection = new PlaneProjection();
            if (!(element.RenderTransform is CompositeTransform))
                element.RenderTransform = new CompositeTransform {CenterX = 0.5, CenterY = 0.5};
            element.RenderTransformOrigin = new Point(0.5, 0.5);
        }


        public static void ClearAnimationProperties(FrameworkElement element)
        {
            var planeProjection = element.Projection as PlaneProjection;
            if (planeProjection == null)
                element.Projection = planeProjection = new PlaneProjection();

            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
                element.RenderTransform = transform = new CompositeTransform { CenterX = 0.5, CenterY = 0.5 };

            element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.Opacity = 1;

            transform.Rotation = 0;
            transform.TranslateX = 0;
            transform.TranslateY = 0;
            transform.ScaleX = 1;
            transform.ScaleY = 1;

            planeProjection.RotationX = 0;
            planeProjection.RotationY = 0;
            planeProjection.RotationZ = 0;
            planeProjection.GlobalOffsetX = 0;
            planeProjection.GlobalOffsetY = 0;
            planeProjection.GlobalOffsetZ = 0;
            planeProjection.CenterOfRotationX = 0;
            planeProjection.CenterOfRotationY = 0;
            planeProjection.CenterOfRotationZ = 0;
        }
    }
}
