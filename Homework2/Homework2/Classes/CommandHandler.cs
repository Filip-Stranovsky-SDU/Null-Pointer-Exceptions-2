using System;
using Avalonia.Threading;
using ReactiveUI;
using System.Windows.Input;

namespace HeatOptimiser.Classes
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public CommandHandler(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            // Ensure the action runs on the UI thread
            Dispatcher.UIThread.Post(() => _action());
        }
    }
}