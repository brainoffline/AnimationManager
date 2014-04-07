using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Animation.Annotations;
using Animation.Common;
using Brain.Animate;

namespace Animation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClockPage : INotifyPropertyChanged
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

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


        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ClockPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            _timer = new DispatcherTimer();
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1.0);
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            TimerOnTick(null, null);
            _timer.Start();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            _timer.Stop();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private void TimerOnTick(object sender, object o)
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
                this.AnimateProperty("(ClockPage.HoursDegrees)")
                    .AddEasingKeyFrame(0.3, targetHoursDegrees, new BackEase()));
            sb.Children.Add(
                this.AnimateProperty("(ClockPage.MinutesDegrees)")
                    .AddEasingKeyFrame(0.3, targetMinutesDegrees, new BackEase()));
            sb.Children.Add(
                this.AnimateProperty("(ClockPage.SecondsDegrees)")
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

            SecondsArc.InvalidateMeasure();
        }

        public static readonly DependencyProperty HoursDegreesProperty = DependencyProperty.Register(
            "HoursDegrees", typeof (double), typeof (ClockPage), new PropertyMetadata(default(double)));

        public double HoursDegrees
        {
            get { return (double) GetValue(HoursDegreesProperty); }
            set { SetValue(HoursDegreesProperty, value); RaisePropertyChanged(); }
        }


        public static readonly DependencyProperty MinutesDegreesProperty = DependencyProperty.Register(
            "MinutesDegrees", typeof (double), typeof (ClockPage), new PropertyMetadata(default(double)));

        public double MinutesDegrees
        {
            get { return (double) GetValue(MinutesDegreesProperty); }
            set { SetValue(MinutesDegreesProperty, value); RaisePropertyChanged(); }
        }



        public static readonly DependencyProperty SecondsDegreesProperty = DependencyProperty.Register(
            "SecondsDegrees", typeof (double), typeof (ClockPage), new PropertyMetadata(default(double), OnSecondsDegrees));

        public double SecondsDegrees
        {
            get { return (double)GetValue(SecondsDegreesProperty); }
            set { SetValue(SecondsDegreesProperty, value); RaisePropertyChanged(); }
        }

        private static void OnSecondsDegrees(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ClockPage) d).SecondsDegreesChanged(Convert.ToDouble(e.OldValue), Convert.ToDouble(e.NewValue));
        }

        private void SecondsDegreesChanged(double oldValue, double newValue)
        {
        }


        /*
        protected async override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;

            await Task.WhenAll(new[]
            {
                Animations.AnimateClose(),
                TimeGrid.AnimateAsync(new BounceOutDownAnimation())
            });

            NavigationService.GoBack();
        }
         */

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
                    .AddEasingKeyFrame(Duration, 1.0, new BackEase {Amplitude = 0.4, EasingMode = EasingMode.EaseIn})
            };
        }
    }

}
