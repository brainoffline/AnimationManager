using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Windows.UI.Core;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

#if WINDOWS_PHONE
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
#endif

namespace Brain.Animate
{
    public class SpriteCoordinate
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public partial class AnimatedImage : IDisposable
    {
#if NETFX_CORE || WINDOWS_81_PORTABLE
        private Timer _animationTimer;
#endif
#if WINDOWS_PHONE
        private DispatcherTimer _animationTimer;
#endif
        private int[] _animationSpriteList;
        private int _animationSpriteIndex;

        public AnimatedImage()
        {
            InitializeComponent();
            SpriteCoordinates = SpriteCoordinates ?? new List<SpriteCoordinate>();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            UpdateSprite();
        }

        public static readonly DependencyProperty SpriteSheetSourceProperty =
            DependencyProperty.Register("SpriteSheetSource", typeof (ImageSource), typeof (AnimatedImage), new PropertyMetadata(default(ImageSource)));

        public ImageSource SpriteSheetSource
        {
            get { return (ImageSource) GetValue(SpriteSheetSourceProperty); }
            set
            {
                SetValue(SpriteSheetSourceProperty, value);
                UpdateSprite();
            }
        }



        public static readonly DependencyProperty SpriteWidthProperty =
            DependencyProperty.Register("SpriteWidth", typeof (int), typeof (AnimatedImage), new PropertyMetadata(default(int)));

        public int SpriteWidth
        {
            get { return (int) GetValue(SpriteWidthProperty); }
            set
            {
                SetValue(SpriteWidthProperty, value);
                UpdateSprite();
            }
        }



        public static readonly DependencyProperty SpriteHeightProperty =
            DependencyProperty.Register("SpriteHeight", typeof (int), typeof (AnimatedImage), new PropertyMetadata(default(int)));

        public int SpriteHeight
        {
            get { return (int) GetValue(SpriteHeightProperty); }
            set
            {
                SetValue(SpriteHeightProperty, value);
                UpdateSprite();
            }
        }



        public static readonly DependencyProperty SpriteCountXProperty =
            DependencyProperty.Register("SpriteCountX", typeof (int), typeof (AnimatedImage), new PropertyMetadata(1));

        public int SpriteCountX
        {
            get { return (int) GetValue(SpriteCountXProperty); }
            set
            {
                SetValue(SpriteCountXProperty, value);
                UpdateSprite();
            }
        }



        public static readonly DependencyProperty SpriteCountYProperty =
            DependencyProperty.Register("SpriteCountY", typeof (int), typeof (AnimatedImage), new PropertyMetadata(1));

        public int SpriteCountY
        {
            get { return (int) GetValue(SpriteCountYProperty); }
            set
            {
                SetValue(SpriteCountYProperty, value);
                UpdateSprite();
            }
        }



        public static readonly DependencyProperty SpriteIndexProperty =
            DependencyProperty.Register("SpriteIndex", typeof (int), typeof (AnimatedImage), new PropertyMetadata(default(int)));

        public int SpriteIndex
        {
            get { return (int) GetValue(SpriteIndexProperty); }
            set
            {
                SetValue(SpriteIndexProperty, value);
                UpdateSprite();
            }
        }


        public static readonly DependencyProperty FrameTimeSpanProperty =
            DependencyProperty.Register("FrameTimeSpan", typeof (TimeSpan), typeof (AnimatedImage), new PropertyMetadata(default(TimeSpan)));

        public TimeSpan FrameTimeSpan
        {
            get { return (TimeSpan) GetValue(FrameTimeSpanProperty); }
            set { SetValue(FrameTimeSpanProperty, value); }
        }



        public static readonly DependencyProperty IsAnimatingProperty =
            DependencyProperty.Register("IsAnimating", typeof (bool), typeof (AnimatedImage), new PropertyMetadata(default(bool)));

        public bool IsAnimating
        {
            get { return (bool) GetValue(IsAnimatingProperty); }
            set
            {
                SetValue(IsAnimatingProperty, value);
                StopAnimation();
                if (value)
                    BeginAnimation();
            }
        }


        public static readonly DependencyProperty SpriteCoordinatesProperty =
            DependencyProperty.Register("SpriteCoordinates", typeof (List<SpriteCoordinate>), typeof (AnimatedImage), new PropertyMetadata(default(List<SpriteCoordinate>)));

        public List<SpriteCoordinate> SpriteCoordinates
        {
            get { return (List<SpriteCoordinate>) GetValue(SpriteCoordinatesProperty); }
            set { SetValue(SpriteCoordinatesProperty, value); }
        }



        private void UpdateSpriteWithCoordinates()
        {
            var index = Math.Abs(SpriteIndex) % SpriteCoordinates.Count;
            var sprite = SpriteCoordinates[index];

            if (sprite.Width != 0)
                SpriteWidth = sprite.Width;
            if (sprite.Height != 0)
                SpriteHeight = sprite.Height;
            SpriteTransform.TranslateX = sprite.X;
            SpriteTransform.TranslateY = sprite.Y;
        }

        private void UpdateSpriteWithIndex()
        {
            var index = Math.Abs(SpriteIndex) % (SpriteCountX * SpriteCountY);
            var indexX = index % SpriteCountX;
            var indexY = index / SpriteCountX;

            SpriteTransform.TranslateX = (-indexX) * SpriteWidth;
            SpriteTransform.TranslateY = (-indexY) * SpriteHeight;
        }

        private bool _updatingSprite;
        private void UpdateSprite()
        {
            if (_updatingSprite) return;

            _updatingSprite = true;
            if ((SpriteCoordinates == null) || (SpriteCoordinates.Count == 0))
                UpdateSpriteWithIndex();
            else
                UpdateSpriteWithCoordinates();
            _updatingSprite = false;
        }

        public void BeginAnimation(IEnumerable<string> spriteNames)
        {
            List<int> indicies = new List<int>();

            foreach (var spriteName in spriteNames)
            {
                var spriteCoordinate = SpriteCoordinates.FirstOrDefault(sprite => sprite.Name == spriteName);
                if (spriteCoordinate != null)
                    indicies.Add( SpriteCoordinates.IndexOf(spriteCoordinate) );
            }
            BeginAnimation(indicies);
        }

        public void BeginAnimation(IEnumerable<int> indicies = null)
        {
            //IsAnimating = false;

            _animationSpriteList = (indicies == null) ? null : indicies.ToArray();
            _animationSpriteIndex = 0;

            if (_animationSpriteList != null)
                SpriteIndex = _animationSpriteList[0];

#if NETFX_CORE || WINDOWS_81_PORTABLE
            _animationTimer = new Timer(async (state) => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
#endif
#if WINDOWS_PHONE
            _animationTimer = new DispatcherTimer();
            _animationTimer.Interval = FrameTimeSpan;
            _animationTimer.Tick += (sender, args) => {
#endif
                if (_animationSpriteList != null)
                {
                    _animationSpriteIndex = (_animationSpriteIndex + 1)%_animationSpriteList.Length;
                    SpriteIndex = _animationSpriteList[_animationSpriteIndex];
                }
                else
                {

                    if ((SpriteCoordinates == null) || (SpriteCoordinates.Count == 0))
                    {
                        var index = Math.Abs(SpriteIndex + 1)%(SpriteCountX*SpriteCountY);
                        SpriteIndex = index;
                    }
                    else
                    {
                        SpriteIndex = (SpriteIndex + 1)%SpriteCoordinates.Count;
                    }
                }
#if NETFX_CORE || WINDOWS_81_PORTABLE
            }), null, FrameTimeSpan, FrameTimeSpan);
#endif
#if WINDOWS_PHONE
            };
            _animationTimer.Start();
#endif
        }

        public void StopAnimation()
        {
            _animationSpriteList = null;
            _animationSpriteIndex = 0;

            if (_animationTimer != null)
            {
#if NETFX_CORE || WINDOWS_81_PORTABLE
                _animationTimer.Dispose();
#endif
#if WINDOWS_PHONE
                _animationTimer.Stop();
#endif
                _animationTimer = null;
            }
        }

        public void SetSpriteName(string name)
        {
            if (SpriteCoordinates == null)
                return;

            var spriteCoordinate = SpriteCoordinates.FirstOrDefault(sprite => sprite.Name == name);
            if (spriteCoordinate != null)
                SpriteIndex = SpriteCoordinates.IndexOf(spriteCoordinate);
        }

        public void Dispose()
        {
            StopAnimation();
        }
    }
}
