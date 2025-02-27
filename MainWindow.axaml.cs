using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    private IBrush? color = Brushes.Black;
    private int[,] image;
    private int width;
    private int height;
    private int pixelSize;
    public MainWindow()
    {
        InitializeComponent();
        width = 10;
        height = 10;
        pixelSize = 20;
        image = new int[height, width];

        UpdatePixelSize();
        this.ReDraw();
        
        

    }
    
    private void ReDraw()
    {
        Canvas.Children.Clear();
        UpdateCanvasSize();
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //image[i, j] = 0;
                this.DrawPixel(i, j);
            }
        }
    }
    
    private async void Load(object? sender, RoutedEventArgs e)
    {
        var load = new OpenFileDialog
        {
            Title = "Select a file",
            AllowMultiple = false
        };
        var result = await load.ShowAsync(this);
        if (result?.Length > 0)
        {
            loadFile(result[0]);
        }
    }

    public void loadFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        if (lines.Length < 2)
        {
            throw new Exception("File doesn't include enough information");
        }
        string[] size = lines[0].Split(' ');
        if (size.Length < 2)
        {
            throw new Exception("Missing height or width");
        }
        
        if (!int.TryParse(size[0], out width) || !int.TryParse(size[1], out height) || width < 1 || height < 1)
        {
            throw new Exception("Invalid values");
        }
        
        Canvas.Children.Clear();
        Canvas.Height = height * pixelSize;
        Canvas.Width = width * pixelSize;


        image = new int[height, width];
        
        for (int row = 0, lineIndex = 1; row < height && lineIndex < lines.Length; row++, lineIndex++)
        {
            string line = lines[lineIndex].Replace(" ", ""); 
            if (line.Length != width) continue; 

            for (int col = 0; col < width; col++)
            {
                image[row, col] = line[col] == '1' ? 1 : 0;
            }
        }
        ReDraw();
    }

    private void DrawPixel(int x, int y)
    {
        var pixel = new Avalonia.Controls.Shapes.Rectangle()
        {
            Width = pixelSize,
            Height = pixelSize,
            Fill = image[x, y] == 1 ? color : Brushes.White,
        };
        Canvas.SetLeft(pixel, y * pixelSize);
        Canvas.SetTop(pixel, x * pixelSize);
        Canvas.Children.Add(pixel);
    }
    
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
    
    private void Press(object? sender, PointerPressedEventArgs e)
    {
        var position = e.GetPosition(Canvas);
        int x = (int)(position.Y / pixelSize);
        int y = (int)(position.X / pixelSize);

        if (x >= 0 && x < height && y >= 0 && y < width)
        {
            image[x, y] = image[x, y] == 1 ? 0 : 1;
            DrawPixel(x, y);
        }
        
        
    }
    
    private void ChangeColor(object? sender, SelectionChangedEventArgs e)
    {
        if (ColorComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedColor = selectedItem.Content!.ToString()!;
            switch (selectedColor)
            {
                case "Black":
                    color = Brushes.Black;
                    break;
                case "Red":
                    color = Brushes.Red;
                    break;
                case "Green":
                    color = Brushes.Green;
                    break;
                case "Blue":
                    color = Brushes.Blue;
                    break;
                case "Yellow":
                    color = Brushes.Yellow;
                    break;
                case "Purple":
                    color = Brushes.Purple;
                    break;
                case "Rubber":
                    color = Brushes.White;
                    break;
                default:
                    color = Brushes.Black;
                    break;
            }
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
        TB1.Text = $"{this.Width} {this.Height}";
        UpdatePixelSize();
        ReDraw();    
    }

    private void UpdatePixelSize() {
        int maxPixelYSize = (int) (this.Height - 140) / height;
        int maxPixelXSize = (int) (this.Width - 20) / width;
        pixelSize = Math.Min(maxPixelXSize, maxPixelYSize);
    }
    private void UpdateCanvasSize() {
        Canvas.Width = width * pixelSize;
        Canvas.Height = height * pixelSize;
    }

}

