using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

#if WINDOWS_PHONE
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Brain.Animate
{

    public class PanControl : ContentControl
    {
        private const string ContentPresenterName = "ContentPresenter";
        private const string OtherContentPresenterName = "OtherContentPresenter";

        /****************************************************************/

        public static readonly DependencyProperty OtherContentProperty =
            DependencyProperty.Register("OtherContent", typeof (object), typeof (PanControl), new PropertyMetadata(null, OtherContentPropertyChanged));

        public object OtherContent
        {
            get { return GetValue(OtherContentProperty); }
            set { SetValue(OtherContentProperty, value); }
        }

        private static void OtherContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panControl = d as PanControl;
            if (panControl == null) return;

            panControl.OnOtherContentChanged(e.OldValue, e.NewValue);
        }

        protected virtual void OnOtherContentChanged(object oldValue, object newValue)
        {
            
        }

        /****************************************************************/

        public static readonly DependencyProperty OtherContentTemplateProperty =
            DependencyProperty.Register("OtherContentTemplate", typeof (DataTemplate), typeof (PanControl), new PropertyMetadata(default(DataTemplate), OnOtherContentTemplatePropertyChanged));

        public DataTemplate OtherContentTemplate
        {
            get { return (DataTemplate) GetValue(OtherContentTemplateProperty); }
            set { SetValue(OtherContentTemplateProperty, value); }
        }

        private static void OnOtherContentTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panControl = d as PanControl;
            if (panControl == null) return;

            panControl.OnOtherContentTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
        }

        protected virtual void OnOtherContentTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            
        }

        /****************************************************************/

        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", 
            typeof (double), 
            typeof (PanControl), 
            new PropertyMetadata(0d, OnOffsetPropertyChanged));

        public double Offset
        {
            get { return (double) GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        private static void OnOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PanControl)d).OnOffsetChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnOffsetChanged(double oldValue, double newValue)
        {
            UpdateOffset();
        }

        /****************************************************************/

        public static readonly DependencyProperty PanRateProperty =
            DependencyProperty.Register("PanRate", typeof (double), typeof (PanControl), new PropertyMetadata(1d, OnPanRatePropertyChanged));

        public double PanRate
        {
            get { return (double) GetValue(PanRateProperty); }
            set { SetValue(PanRateProperty, value); }
        }

        private static void OnPanRatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PanControl) d).OnPanRateChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnPanRateChanged(double oldValue, double newValue)
        {
            
        }

        /****************************************************************/

        private bool _startAutoPan;

        public static readonly DependencyProperty AutoPanRateProperty =
            DependencyProperty.Register("AutoPanRate", typeof (double), typeof (PanControl), new PropertyMetadata(default(double), OnAutoPanRatePropertyChanged));

        public double AutoPanRate
        {
            get { return (double) GetValue(AutoPanRateProperty); }
            set { SetValue(AutoPanRateProperty, value); }
        }

        private static void OnAutoPanRatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PanControl) d).OnAutoPanRateChanged((double) e.OldValue, (double) e.NewValue);
        }

        private void OnAutoPanRateChanged(double oldValue, double newValue)
        {
            AnimationManager.StopAnimations(this);
#if NETFX_CORE || WINDOWS_81_PORTABLE
            if (!DesignMode.DesignModeEnabled)
#endif
                _startAutoPan = true;
            UpdateOffset();
        }


        /****************************************************************/


        public PanControl()
        {
            DefaultStyleKey = typeof (PanControl);
        }

#if NETFX_CORE || WINDOWS_81_PORTABLE
        protected override void OnApplyTemplate()
#elif WINDOWS_PHONE
        public override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();

            var content = GetTemplateChild(ContentPresenterName) as FrameworkElement;
            var otherContent = GetTemplateChild(OtherContentPresenterName) as FrameworkElement;

            if (content != null) 
                content.SizeChanged += (sender, args) => UpdateOffset();
            if (otherContent != null)
                otherContent.SizeChanged += (sender, args) => UpdateOffset();
            SizeChanged += (sender, args) => UpdateOffset();

            AnimationManager.PrepareElement(content);
            AnimationManager.PrepareElement(otherContent);

            if (content != null)
                content.Margin = new Thickness(-1, 0, -1, 0);

            if (otherContent != null)
                otherContent.Margin = new Thickness(-1, 0, -1, 0);

            UpdateOffset();
        }

        private async void UpdateOffset()
        {
            if (ActualWidth <= 0) return;

            var content = GetTemplateChild(ContentPresenterName) as FrameworkElement;
            if (content == null) return;

            var child = ((ContentPresenter) content).Content as FrameworkElement;
            if (child != null)
            {
                if (content.ActualWidth < child.ActualWidth)
                    content.Width = child.ActualWidth;
                content = child;
            }

            var otherContent = GetTemplateChild(OtherContentPresenterName) as FrameworkElement;
            if (otherContent == null) return;

            child = ((ContentPresenter)otherContent).Content as FrameworkElement;
            if (child != null)
            {
                if (otherContent.ActualWidth < child.ActualWidth)
                    otherContent.Width = child.ActualWidth;
                otherContent = child;
            }

            var contentWidth = content.ActualWidth;
            var otherContentWidth = otherContent.ActualWidth;

            if ((contentWidth <= 0.0) || 
                (otherContentWidth <= 0.0) ||
                Double.IsNaN(contentWidth) ||
                Double.IsNaN(otherContentWidth)) return;

            var offset = Offset%(contentWidth + otherContentWidth);
            double contentOffset = 0;
            double otherContentOffset = contentWidth;

            if (offset < 0)
            {
                var absOffset = Math.Abs(offset);
                if (absOffset <= contentWidth)
                {
                    contentOffset = absOffset;
                    otherContentOffset = absOffset - otherContentWidth;
                }
                else
                {
                    absOffset -= otherContentWidth;
                    otherContentOffset = absOffset;
                    contentOffset = -contentWidth + absOffset;
                }
            }
            else
            {
                if (offset <= contentWidth)
                {
                    contentOffset = -offset;
                    otherContentOffset = contentWidth - offset;
                }
                else
                {
                    offset -= contentWidth;
                    otherContentOffset = -offset;
                    contentOffset = otherContentWidth - offset;
                }
            }

            if (!(content.RenderTransform is CompositeTransform))
                content.RenderTransform = new CompositeTransform { CenterX = 0.5, CenterY = 0.5 };
            if (!(otherContent.RenderTransform is CompositeTransform))
                otherContent.RenderTransform = new CompositeTransform { CenterX = 0.5, CenterY = 0.5 };

            ((CompositeTransform)content.RenderTransform).TranslateX = contentOffset;
            ((CompositeTransform)otherContent.RenderTransform).TranslateX = otherContentOffset;

            if (_startAutoPan && 
                (Math.Abs(AutoPanRate) > double.Epsilon))   // != zero
            {
                _startAutoPan = false;
                var distance = (AutoPanRate > 0)
                    ? contentWidth + otherContentWidth
                    : -(contentWidth + otherContentWidth);

                var timeline = this.AnimateProperty("(Offset)")
                        .AddEasingKeyFrame(0.0, Offset)
                        .AddEasingKeyFrame(Math.Abs(AutoPanRate), Offset + distance);
                await this.AnimateAsync(new[] {timeline}, new AnimationParameters { Forever = true });
            }
        }
    }
}
