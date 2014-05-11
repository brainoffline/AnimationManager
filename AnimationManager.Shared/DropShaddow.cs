using System.Windows;

#if NETFX_CORE || WINDOWS_81_PORTABLE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

#if WINDOWS_PHONE
using System.Windows.Controls;
#endif

namespace Brain.Animate
{
    [TemplatePart(Name = "ContentPresenter", Type = typeof(ContentPresenter))]
    public class DropShaddow : ContentControl
    {
        private const string ContentPresenterName = "ContentPresenter";
        private const string ShaddowName = "Shaddow";

        protected internal ContentPresenter ContentPresenter { get; set; }


        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register("OffsetX", typeof (double), typeof (DropShaddow), new PropertyMetadata(5d, OnOffsetChanged));

        public double OffsetX
        {
            get { return (double) GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register("OffsetY", typeof (double), typeof (DropShaddow), new PropertyMetadata(5d, OnOffsetChanged));

        public double OffsetY
        {
            get { return (double) GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }


        public DropShaddow()
        {
            DefaultStyleKey = typeof (DropShaddow);
        }

#if NETFX_CORE || WINDOWS_81_PORTABLE
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateMargin();
        }
#endif

#if WINDOWS_PHONE
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateMargin();
        }
#endif

        private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dropShaddow = d as DropShaddow;
            if (dropShaddow == null) return;

            dropShaddow.UpdateMargin();
        }

        private void UpdateMargin()
        {
            var shaddow = GetTemplateChild(ShaddowName) as FrameworkElement;
            var content = GetTemplateChild(ContentPresenterName) as FrameworkElement;
            if (content != null)
                content.Margin = new Thickness(OffsetX, 0 - OffsetY, 0 - OffsetX, OffsetY);
        }
    }
}
