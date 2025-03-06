using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Dialogs;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();

    }
    
    
    private async void Load(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this) ?? throw new Exception("TopLevel not found");

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select a file",
            AllowMultiple = false,
            FileTypeFilter = [new FilePickerFileType(" ") { Patterns = ["*.b2img.txt", "*.b16img.txt"] }]
        });

        if (files.Count >= 1)
        {
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);

            List<string> lines = [];
            while (streamReader.Peek() != -1)
            {
                lines.Add(streamReader.ReadLine());
            }

            int[,] image = {{}};
            int width = 1;
            int height = 1;
            FileDTO file = new(lines.ToArray(), image, width, height);
            FileHandler.LoadFile(file);
            image = file.Image;
            width = file.Width;
            height = file.Height;
            Canvas.NewImage(image, width, height);
        }
    }

    private async void Save(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this) ?? throw new Exception("TopLevel not found");

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File",
            FileTypeChoices = [new FilePickerFileType(" ") { Patterns = ["*.b16img.txt"] }]
        });

        if (file is not null)
        {   

            // FileDTO fileContent = new(image, width, height);
            FileDTO fileContent = Canvas.GetFileDTO();
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteLineAsync(FileHandler.SaveFile(fileContent));
        }
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

