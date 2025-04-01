namespace homework3_livecharts.Models;
using System;
using Avalonia.Threading;
using ReactiveUI;
using System.Windows.Input;


public class CommandHandler : ICommand
{
    private readonly Action _action;
    public event EventHandler CanExecuteChanged;

    public CommandHandler(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter) => _action();
}