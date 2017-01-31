using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Gauges
{
    public sealed partial class PumpingGauge : UserControl
    {
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(PumpingGauge), new PropertyMetadata(3.927, null));
        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register("EndAngle", typeof(double), typeof(PumpingGauge), new PropertyMetadata(-0.785, null));

        public static readonly DependencyProperty MinimumLitresProperty = DependencyProperty.Register("MinimumLitres", typeof(int), typeof(PumpingGauge), new PropertyMetadata(0, null));
        public static readonly DependencyProperty MaximumLitresProperty = DependencyProperty.Register("MaximumLitres", typeof(int), typeof(PumpingGauge), new PropertyMetadata(20000, null));

        public static readonly DependencyProperty DeliveredLitresProperty = DependencyProperty.Register("DeliveredLitres", typeof(int), typeof(PumpingGauge), new PropertyMetadata(0, null));

        public static readonly DependencyProperty PresetLitresProperty = DependencyProperty.Register("PresetLitres", typeof(int), typeof(PumpingGauge), new PropertyMetadata(0, null));

        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(PumpingGauge), new PropertyMetadata(Colors.Lime, null));

        public PumpingGauge()
        {
            InitializeComponent();
        }

        public Color ForegroundColor
        {
            get
            {
                var v = GetValue(ForegroundColorProperty);

                if (v is Color)
                {
                    return (Color)v;
                }

                return Colors.Lime;
            }

            set
            {
                // Only redraw the thermometer if the temperature has changed
                if (ForegroundColor != value)
                {
                    SetValue(ForegroundColorProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public int MaximumLitres
        {
            get
            {
                var v = GetValue(MaximumLitresProperty);

                if (v is int)
                {
                    return (int)v;
                }

                return 0;
            }

            set
            {
                // Only redraw the thermometer if the temperature has changed
                if (MaximumLitres != value)
                {
                    SetValue(MaximumLitresProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public int MinimumLitres
        {
            get
            {
                var v = GetValue(MinimumLitresProperty);

                if (v is int)
                {
                    return (int)v;
                }

                return 0;
            }

            set
            {
                // Only redraw the thermometer if the temperature has changed
                if (MinimumLitres != value)
                {
                    SetValue(MinimumLitresProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public double StartAngle
        {
            get
            {
                var v = GetValue(StartAngleProperty);

                if (v is double)
                {
                    return (double)v;
                }

                return 0.0;
            }

            set
            {
                if (StartAngle != value)
                {
                    SetValue(StartAngleProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public double EndAngle
        {
            get
            {
                var v = GetValue(EndAngleProperty);

                if (v is double)
                {
                    return (double)v;
                }

                return 0.0;
            }

            set
            {
                if (EndAngle != value)
                {
                    SetValue(EndAngleProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public int DeliveredLitres
        {
            get
            {
                var v = GetValue(DeliveredLitresProperty);

                if (v is int)
                {
                    return (int)v;
                }

                return 0;
            }

            set
            {
                // Only redraw the thermometer if the temperature has changed
                if (DeliveredLitres != value)
                {
                    SetValue(DeliveredLitresProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        public int PresetLitres
        {
            get
            {
                var v = GetValue(PresetLitresProperty);

                if (v is int)
                {
                    return (int)v;
                }

                return 0;
            }

            set
            {
                // Only redraw the thermometer if the temperature has changed
                if (PresetLitres != value)
                {
                    SetValue(PresetLitresProperty, value);

                    gauge.Invalidate();
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Height > Width)
            {
                Width = Height;
            }

            if (Width > Height)
            {
                Height = Width;
            }
        }

        private void gauge_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            using (CanvasDrawingSession drawingSession = args.DrawingSession)
            {
                Vector2 outputSize = new Vector2((float)(ActualWidth), (float)(ActualHeight));
                Vector2 sourceSize = new Vector2((float)100.0f, (float)-100.0f);

                drawingSession.Transform = GetDisplayTransform(outputSize, sourceSize);

                using (var geometry = CanvasGeometry.CreatePath(CreateBackgroundGauge(sender)))
                {
                    drawingSession.FillGeometry(geometry, Color.FromArgb(255, 25, 25, 25)); // Colors.DarkSlateGray);
                }

                using (var geometry2 = CanvasGeometry.CreatePath(CreateForegroundGauge(sender)))
                {
                    drawingSession.FillGeometry(geometry2, ForegroundColor);
                }

                using (var geometry3 = CanvasGeometry.CreatePath(CreateBorderGauge(sender)))
                {
                    drawingSession.DrawGeometry(geometry3, Colors.White, 2.0f);
                }

                outputSize = new Vector2((float)(ActualWidth), (float)(ActualHeight));
                sourceSize = new Vector2((float)100.0f, (float)100.0f);

                drawingSession.Transform = GetDisplayTransform(outputSize, sourceSize);

                string text = string.Format("{0:0} L", DeliveredLitres);

                FontWeight weight = new FontWeight();

                weight.Weight = 500;

                using (CanvasTextFormat textFormat = new CanvasTextFormat { FontSize = 10.0f, FontWeight = weight, WordWrapping = CanvasWordWrapping.NoWrap })
                using (CanvasTextLayout layout = new CanvasTextLayout(drawingSession, text, textFormat, 0.0f, 0.0f))
                {
                    float xLocation = -(float)layout.DrawBounds.Width / 2;
                    float yLocation = -(float)layout.DrawBounds.Height - 10;

                    outputSize = new Vector2((float)(ActualWidth), (float)(ActualHeight));
                    sourceSize = new Vector2(100.0f, 100.0f);

                    drawingSession.Transform = GetDisplayTransform(outputSize, sourceSize);

                    drawingSession.DrawTextLayout(layout, xLocation, yLocation, ForegroundColor);
                }

                text = string.Format("{0:0} L", PresetLitres);

                weight = new FontWeight();

                weight.Weight = 500;

                using (CanvasTextFormat textFormat = new CanvasTextFormat { FontSize = 15.0f, FontWeight = weight, WordWrapping = CanvasWordWrapping.NoWrap })
                using (CanvasTextLayout layout = new CanvasTextLayout(drawingSession, text, textFormat, 0.0f, 0.0f))
                {
                    float xLocation = -(float)layout.DrawBounds.Width / 2;
                    float yLocation = -(float)layout.DrawBounds.Height + 10;

                    drawingSession.DrawTextLayout(layout, xLocation, yLocation, ForegroundColor);
                }
            }
        }

        private Vector2 GetControlCenter()
        {
            return new Vector2((float)(ActualWidth / 2), (float)(ActualHeight / 2));
        }

        private Vector2 GetStartPosition()
        {
            Vector2 start = new Vector2();

            return start;
        }

        private float GetOuterRadius()
        {
            if (ActualWidth > ActualHeight)
            {
                return (float)(ActualHeight / 2);
            }
            else
            {
                return (float)(ActualWidth / 2);
            }
        }

        private Vector2 GetVector(float radius, float angle)
        {
            Vector2 vector = new Vector2();

            double x = radius * Math.Cos(-angle);
            double y = -radius * Math.Sin(-angle);

            vector.X = (float)x;
            vector.Y = (float)y;

            return vector;
        }

        private CanvasPathBuilder CreateBackgroundGauge(ICanvasResourceCreator sender)
        {
            var center = new Vector2(0, 0);

            float outerRadius = 50.0f;
            float innerRadius = outerRadius * 0.7f;

            var pathBuilder = new CanvasPathBuilder(sender);

            float startAngle = (float)StartAngle;
            float endAngle = (float)EndAngle;

            float sweepAngle = (float)(StartAngle - EndAngle);

            pathBuilder.BeginFigure(GetVector(outerRadius, startAngle));

            pathBuilder.AddArc(center, outerRadius, outerRadius, startAngle, -sweepAngle);

            pathBuilder.AddLine(GetVector(innerRadius, endAngle));

            pathBuilder.AddArc(center, innerRadius, innerRadius, endAngle, sweepAngle);

            pathBuilder.EndFigure(CanvasFigureLoop.Closed);

            return pathBuilder;
        }

        private CanvasPathBuilder CreateBorderGauge(ICanvasResourceCreator sender)
        {
            var center = new Vector2(0, 0);

            float outerRadius = 49.0f;
            float innerRadius = outerRadius * 0.7f;

            var pathBuilder = new CanvasPathBuilder(sender);

            float startAngle = (float)StartAngle;
            float endAngle = (float)EndAngle;

            float sweepAngle = (float)(StartAngle - EndAngle);

            pathBuilder.BeginFigure(GetVector(outerRadius, startAngle));

            pathBuilder.SetSegmentOptions(CanvasFigureSegmentOptions.ForceRoundLineJoin);

            pathBuilder.AddArc(center, outerRadius, outerRadius, startAngle, -sweepAngle);

            pathBuilder.AddLine(GetVector(innerRadius, endAngle));

            pathBuilder.AddArc(center, innerRadius, innerRadius, endAngle, sweepAngle);

            pathBuilder.EndFigure(CanvasFigureLoop.Closed);

            return pathBuilder;
        }

        private CanvasPathBuilder CreateForegroundGauge(ICanvasResourceCreator sender)
        {
            var center = new Vector2(0, 0);

            float outerRadius = 50.0f;
            float innerRadius = outerRadius * 0.7f;

            float startAngle = (float)StartAngle;

            float sweepAngle = (float)((Utilities.Clamp(DeliveredLitres, MinimumLitres, PresetLitres) / (PresetLitres - MinimumLitres)) * (StartAngle - EndAngle));

            float endAngle = (float)(startAngle - sweepAngle);

            var pathBuilder = new CanvasPathBuilder(sender);

            pathBuilder.BeginFigure(GetVector(outerRadius, startAngle));

            pathBuilder.AddArc(center, outerRadius, outerRadius, startAngle, -sweepAngle);

            //var vc = GetVector(innerRadius, endAngle);

            pathBuilder.AddLine(GetVector(innerRadius, endAngle));

            pathBuilder.AddArc(center, innerRadius, innerRadius, endAngle, sweepAngle);

            pathBuilder.EndFigure(CanvasFigureLoop.Closed);

            return pathBuilder;
        }

        private static Matrix3x2 GetDisplayTransform(Vector2 outputSize, Vector2 sourceSize)
        {
            // Scale the display to fill the control
            var scale = outputSize / sourceSize;
            var offset = new Vector2(outputSize.X / 2, outputSize.Y / 2);

            return Matrix3x2.CreateScale(scale) * Matrix3x2.CreateTranslation(offset);
        }
    }
}
