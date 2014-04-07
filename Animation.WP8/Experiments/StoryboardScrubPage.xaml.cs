using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Brain.Animate;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Animation.WP8.Experiments
{
    public partial class StoryboardScrubPage : PhoneApplicationPage
    {
        private Storyboard _sb;

        public StoryboardScrubPage()
        {
            InitializeComponent();

            _sb = AnimationManager.AnimationStoryboard(square, new HingeAnimation { Duration = 1.0 });
            _sb.Begin();
            _sb.Pause();

            slider.ValueChanged += (sender, args) =>
            {
                var percentage = (slider.Value / slider.Maximum);
                _sb.Seek(TimeSpan.FromSeconds(1 * percentage));
            };

        }
    }
}