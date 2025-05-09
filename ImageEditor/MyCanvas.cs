using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace AvaloniaApplication1;
public class MyCanvas : Canvas
{
    private int brushValue = 1;
    private int[,] image;
    private int imageWidth;
    private int imageHeight;
    private int pixelSize;

    protected readonly Dictionary<string, int> ColorValueTable = new(){
        {"Black", 1},
        {"White", 0},
        {"Blue", 2},
        {"Red", 3},
        {"Green", 4},
        {"Cyan", 5},
        {"Yellow", 6},
        {"Purple", 7},
        {"Orange", 8},
        {"Gray", 9},
        {"Magenta", 10},
        {"Light Gray", 11},
        {"Light Blue", 12},
        {"Lime", 13},
        {"Pink", 14},
        {"Brown", 15}
    };
    protected readonly List<IBrush> ValueBrushTable = new(){
        Brushes.White, Brushes.Black, Brushes.Blue, Brushes.Red,
        Brushes.Green, Brushes.Cyan, Brushes.Yellow, Brushes.Purple,
        Brushes.Orange, Brushes.Gray, Brushes.Magenta, Brushes.LightGray,
        Brushes.LightBlue, Brushes.Lime, Brushes.Pink, Brushes.Brown 
    };
    public MyCanvas()
    {   

        imageWidth = 10;
        imageHeight = 10;
        pixelSize = 20;
        image = new int[imageHeight, imageWidth];

        PointerPressed += Press;

        UpdatePixelSize(Width, Height);
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
            Fill = ValueBrushTable[image[x, y]],
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
            image[x, y] = image[x, y] == brushValue ? 0 : brushValue;
            
            DrawPixel(x, y);
        }
    }

    private void UpdatePixelSize(double width, double height)
    {
        int maxPixelYSize = (int) (height - 140) / imageHeight;
        int maxPixelXSize = (int) (width - 20) / imageWidth;
        pixelSize = Math.Min(maxPixelXSize, maxPixelYSize);
    }

    private void UpdateCanvasSize()
    {
        Width = imageWidth * pixelSize;
        Height = imageHeight * pixelSize;
    }

    public void ChangeSize(double width, double height)
    {
        UpdatePixelSize(width, height);
        UpdateCanvasSize();
        ReDraw();
    }

    public void FlipHorizontal()
    {   
        
        for(int x = 0; x<imageWidth/2; x++)
        {
            for(int y = 0; y<imageHeight; y++)
            {
                int temp = image[y, imageWidth-x-1];
                image[y, imageWidth-x-1] = image[y, x];
                image[y, x] = temp;
            }
        }
        ReDraw();
    }
    
    public void FlipVertical()
    {
        for(int x = 0; x<imageWidth; x++)
        {
            for(int y = 0; y<imageHeight/2; y++)
            {
                int temp = image[imageHeight-y-1, x];
                image[imageHeight-y-1, x] = image[y, x];
                image[y, x] = temp;
            }
        }
        ReDraw();
    }

    public void NewImage(int[,] newImage, int width, int height)
    {
        image = newImage;
        imageWidth = width;
        imageHeight = height;
        ReDraw();
    }

    public void UpdateColor(string selectedColor)
    {   
        if (!ColorValueTable.ContainsKey(selectedColor))
        {
            brushValue = 1;
        }
        brushValue = ColorValueTable[selectedColor];
    }

    public FileDTO GetFileDTO()
    {
        return new(image, imageWidth, imageHeight);    
    }
}