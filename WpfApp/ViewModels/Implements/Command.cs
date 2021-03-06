﻿using System;
using System.Windows.Input;

namespace MultiBuffer.WpfApp.ViewModels.Implements
{
    public class Command : ICommand
    {
        public Command(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }

        readonly Action action;
    }
}
