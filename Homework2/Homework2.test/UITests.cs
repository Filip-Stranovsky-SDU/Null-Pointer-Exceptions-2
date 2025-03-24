using Avalonia.Headless.XUnit;
using Avalonia;
using Homework2.Views;
using Homework2.ViewModels;
using Avalonia.Headless;

public class UITests
{
    [AvaloniaFact]
    public void ExampleTest()
    {
        var window = new MainWindow()
        {
            DataContext = new MainWindowViewModel()
        };
        window.Show();

        Assert.NotNull(window);
    }
}
