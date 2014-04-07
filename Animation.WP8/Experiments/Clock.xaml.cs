using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using Brain.Animate;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Animation.WP8.Experiments
{
    public partial class Clock : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private string _theTime;
        private string _theSeconds;

        public string TheTime
        {
            get { return _theTime; }
            set { _theTime = value; RaisePropertyChanged(); }
        }

        public string TheSeconds
        {
            get { return _theSeconds; }
            set { _theSeconds = value; RaisePropertyChanged(); }
        }



        public Clock()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1.0);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            TimerOnTick(null, null);
            _timer.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _timer.Stop();
        }


        private void TimerOnTick(object sender, EventArgs e)
        {
            var time = DateTime.Now;

            var hour = time.Hour % 12;
            var minute = time.Minute;
            var second = time.Second;

            double targetHoursDegrees = ((360d / 12d) * hour) + ((360d / 12d / 60d) * minute);
            double targetMinutesDegrees = ((360d / 60d) * minute) + ((360d / 60d / 60d) * second);
            double targetSecondDegrees = (360d / 60d) * second;

            var sb = new Storyboard();
            sb.Children.Add(
                this.AnimateProperty("(MainPage.HoursDegrees)")
                    .AddEasingKeyFrame(0.3, targetHoursDegrees, new BackEase()));
            sb.Children.Add(
                this.AnimateProperty("(MainPage.MinutesDegrees)")
                    .AddEasingKeyFrame(0.3, targetMinutesDegrees, new BackEase()));
            sb.Children.Add(
                this.AnimateProperty("(MainPage.SecondsDegrees)")
                    .AddEasingKeyFrame(0.99, targetSecondDegrees, new CubicEase { EasingMode = EasingMode.EaseInOut }));
            sb.Begin();

            HoursBackArc1.AnimateAsync(new BounceInSmallAnimation { Delay = 0.0, Amplify = 1.1 });
            HoursArc1.AnimateAsync(new BounceInSmallAnimation { Delay = 0.0, Amplify = 1.1 });

            HoursBackArc2.AnimateAsync(new BounceInSmallAnimation { Delay = 0.1 });
            HoursArc2.AnimateAsync(new BounceInSmallAnimation { Delay = 0.1 });

            MinutesBackArc1.AnimateAsync(new BounceInSmallAnimation { Delay = 0.2 });
            MinutesArc1.AnimateAsync(new BounceInSmallAnimation { Delay = 0.2 });

            MinutesBackArc2.AnimateAsync(new BounceInSmallAnimation { Delay = 0.3 });
            MinutesArc2.AnimateAsync(new BounceInSmallAnimation { Delay = 0.3 });

            SecondsArc.AnimateAsync(new BounceInSmallAnimation { Delay = 0.4 });

            TheTime = time.ToString("hh:mm");
            TheSeconds = time.ToString(":ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty HoursDegreesProperty =
            DependencyProperty.Register("HoursDegrees", typeof(double), typeof(Clock), new PropertyMetadata(default(double)));

        public double HoursDegrees
        {
            get { return (double)GetValue(HoursDegreesProperty); }
            set { SetValue(HoursDegreesProperty, value); }
        }

        public static readonly DependencyProperty MinutesDegreesProperty =
            DependencyProperty.Register("MinutesDegrees", typeof(double), typeof(Clock), new PropertyMetadata(default(double)));

        public double MinutesDegrees
        {
            get { return (double)GetValue(MinutesDegreesProperty); }
            set { SetValue(MinutesDegreesProperty, value); }
        }

        public static readonly DependencyProperty SecondsDegreesProperty =
            DependencyProperty.Register("SecondsDegrees", typeof(double), typeof(Clock), new PropertyMetadata(default(double)));

        public double SecondsDegrees
        {
            get { return (double)GetValue(SecondsDegreesProperty); }
            set { SetValue(SecondsDegreesProperty, value); }
        }

        private void AnimatingTime_OnTap(object sender, GestureEventArgs e)
        {
            animatingTime.AnimateIn();
        }

        protected async override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;

            await Task.WhenAll(new[]
            {
                AnimationTrigger.AnimateClose(),
                TimeGrid.AnimateAsync(new BounceOutDownAnimation())
            });

            NavigationService.GoBack();
        }
    }


    public class BounceInSmallAnimation : AnimationDefinition
    {
        public BounceInSmallAnimation()
        {
            Duration = 0.4;
            Amplify = 1.0;
        }

        public double Amplify { get; set; }

        public override IEnumerable<Timeline> CreateAnimation(FrameworkElement element)
        {
            return new Timeline[]
            {
                element.AnimateProperty(AnimationProperty.ScaleX)
                    .AddEasingKeyFrame(0.0, 1.07*Amplify)
                    .AddEasingKeyFrame(Duration, 1.0, new BackEase {Amplitude = 0.4, EasingMode = EasingMode.EaseIn}),
                element.AnimateProperty(AnimationProperty.ScaleY)
                    .AddEasingKeyFrame(0.0, 1.07*Amplify)
                    .AddEasingKeyFrame(Duration, 1.0, new BackEase {Amplitude = 0.4, EasingMode = EasingMode.EaseIn}),
            };
        }
    }

}