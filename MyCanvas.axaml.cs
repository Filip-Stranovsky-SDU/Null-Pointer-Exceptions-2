using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace AvaloniaApplication1;
public class MyCanvas : Canvas
{
    private IBrush? color = Brushes.Black;
    private int[,] image;
    private int imageWidth;
    private int imageHeight;
    private int pixelSize;

    public MyCanvas()
    {
        imageWidth = 10;
        imageHeight = 10;
        pixelSize = 20;
        image = new int[imageHeight, imageWidth];

        UpdatePixelSize();
        this.ReDraw();
    }
    private void ReDraw()
    {
        Children.Clear();
        UpdateCanvasSize();
        
        for (int i = 0; i < imageHeight; i++)
        {
            for (int j = 0; j < imageWidth; j++)
            {
                //image[i, j] = 0;
                this.DrawPixel(i, j);
            }
        }
    }

    private void DrawPixel(int x, int y)
    {
        var pixel = new Avalonia.Controls.Shapes.Rectangle()
        {
            Width = pixelSize,
            Height = pixelSize,
            Fill = image[x, y] == 1 ? color : Brushes.White,
        };
        SetLeft(pixel, y * pixelSize);
        SetTop(pixel, x * pixelSize);
        Children.Add(pixel);
    }

    private void Press(object? sender, PointerPressedEventArgs e)
    {
        var position = e.GetPosition(this);
        int x = (int)(position.Y / pixelSize);
        int y = (int)(position.X / pixelSize);

        if (x >= 0 && x < imageHeight && y >= 0 && y < imageWidth)
        {
            image[x, y] = image[x, y] == 1 ? 0 : 1;
            DrawPixel(x, y);
        }
        
        
    }


    private void UpdatePixelSize()
    {
        int maxPixelYSize = (int) (this.Height - 140) / imageHeight;
        int maxPixelXSize = (int) (this.Width - 20) / imageWidth;
        pixelSize = Math.Min(maxPixelXSize, maxPixelYSize);
    }
    private void UpdateCanvasSize()
    {
        Width = imageWidth * pixelSize;
        Height = imageHeight * pixelSize;
    }
    public void ChangeSize(double imageWidth, double imageHeight)
    {
        UpdatePixelSize();
        ReDraw();    
    }
}