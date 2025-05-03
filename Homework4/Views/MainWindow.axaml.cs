using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Homework4.ViewModels;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;
using System.Threading;

namespace Homework4.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void SaveButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            await vm.LogButtonAsync(this);
        }
    }
}