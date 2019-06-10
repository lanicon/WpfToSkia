﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SkiaSharp;
using WpfToSkia.ExtensionsMethods;

namespace WpfToSkia.SkiaElements
{
    public class SkiaTextBlock : SkiaFrameworkElement
    {
        public override void Render(SKCanvas canvas, Rect bounds)
        {
            TextBlock textBlock = WpfElement as TextBlock;

            SKPaint paint = new SKPaint()
            {
                Typeface = SKTypeface.FromFamilyName(textBlock.FontFamily.ToString()),
                TextSize = textBlock.FontSize.ToFloat(),
            };

            if (textBlock.Foreground != null)
            {
                paint.Shader = textBlock.Foreground.ToSkiaShader(bounds.Width, bounds.Height);
            }

            if (textBlock.TextAlignment == TextAlignment.Left)
            {
                paint.TextAlign = SKTextAlign.Left;
            }
            else if (textBlock.TextAlignment == TextAlignment.Right)
            {
                paint.TextAlign = SKTextAlign.Right;
            }
            else
            {
                paint.TextAlign = SKTextAlign.Center;
            }

            double left = bounds.Left;
            double top = bounds.Top;
            float height = bounds.Height.ToFloat();

            canvas.DrawText(textBlock.Text, left.ToFloat(), top.ToFloat() + height, paint);
        }

        public override Size Measure(Size availableSize)
        {
            TextBlock textBlock = WpfElement as TextBlock;

            SKPaint paint = new SKPaint()
            {
                Typeface = SKTypeface.FromFamilyName(textBlock.FontFamily.ToString()),
                TextSize = textBlock.FontSize.ToFloat(),
            };

            SKRect rect = new SKRect();
            paint.MeasureText(textBlock.Text, ref rect);

            return new Size(rect.Width, rect.Height);
        }
    }
}
