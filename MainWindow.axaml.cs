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
    
    public MainWindow()
    {
        InitializeComponent();

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
        int width;
        int height;
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
        
        int[,] image = new int[height, width];
        
        for (int row = 0, lineIndex = 1; row < height && lineIndex < lines.Length; row++, lineIndex++)
        {
            string line = lines[lineIndex].Replace(" ", ""); 
            if (line.Length != width) continue; 

            for (int col = 0; col < width; col++)
            {
                image[row, col] = line[col] == '1' ? 1 : 0;
                int.TryParse($"line[col]",
                System.Globalization.NumberStyles.HexNumber,
                null,
                out image[row, col]);
            }
        }

        Canvas.NewImage(image, width, height);
    }

    
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
    private void FlipH(object? sender, RoutedEventArgs e)
    {
        Canvas.FlipHorizontal();
    }
    
    private void FlipV(object? sender, RoutedEventArgs e)
    {
        Canvas.FlipVertical();
    }
    
    
    
    private void ChangeColor(object? sender, SelectionChangedEventArgs e)
    {
        if (ColorComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedColor = selectedItem.Content!.ToString()!;
            Canvas.UpdateColor(selectedColor);
            
        }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        // TB1.Text = $"{this.Width} {this.Height}";
        Canvas.ChangeSize(this.Width, this.Height);
        
    }

    

}

