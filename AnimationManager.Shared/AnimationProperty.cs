using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Animate
{
    /// <summary>
    /// Some common paths than can be animated
    /// </summary>
    public class AnimationProperty
    {
        public const string Opacity = "(UIElement.Opacity)";

        public const string BackgroundColor = "(Control.Background).(SolidColorBrush.Color)";

        public const string RenderTransformOrigin = "(UIElement.RenderTransformOrigin)";

        public const string TranslateX = "(UIElement.RenderTransform).(CompositeTransform.TranslateX)";
        public const string TranslateY = "(UIElement.RenderTransform).(CompositeTransform.TranslateY)";

        public const string ScaleX = "(UIElement.RenderTransform).(CompositeTransform.ScaleX)";
        public const string ScaleY = "(UIElement.RenderTransform).(CompositeTransform.ScaleY)";

        public const string CenterX = "(UIElement.RenderTransform).(CompositeTransform.CenterX)";
        public const string CenterY = "(UIElement.RenderTransform).(CompositeTransform.CenterY)";

        public const string SkewX = "(UIElement.RenderTransform).(CompositeTransform.SkewX)";
        public const string SkewY = "(UIElement.RenderTransform).(CompositeTransform.SkewY)";

        public const string Rotation = "(UIElement.RenderTransform).(CompositeTransform.Rotation)";

        public const string CentreOfRotationX = "(UIElement.Projection).(PlaneProjection.CenterOfRotationX)";
        public const string CentreOfRotationY = "(UIElement.Projection).(PlaneProjection.CenterOfRotationY)";
        public const string CentreOfRotationZ = "(UIElement.Projection).(PlaneProjection.CenterOfRotationZ)";

        public const string LocalOffsetX = "(UIElement.Projection).(PlaneProjection.LocalOffsetX)";
        public const string LocalOffsetY = "(UIElement.Projection).(PlaneProjection.LocalOffsetY)";
        public const string LocalOffsetZ = "(UIElement.Projection).(PlaneProjection.LocalOffsetZ)";

        public const string GlobalOffsetX = "(UIElement.Projection).(PlaneProjection.GlobalOffsetX)";
        public const string GlobalOffsetY = "(UIElement.Projection).(PlaneProjection.GlobalOffsetY)";
        public const string GlobalOffsetZ = "(UIElement.Projection).(PlaneProjection.GlobalOffsetZ)";

        public const string RotationX = "(UIElement.Projection).(PlaneProjection.RotationX)";
        public const string RotationY = "(UIElement.Projection).(PlaneProjection.RotationY)";
        public const string RotationZ = "(UIElement.Projection).(PlaneProjection.RotationZ)";

        public const string ZIndex = "(Canvas.ZIndex)";
    }
}
