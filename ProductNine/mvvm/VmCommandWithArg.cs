using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductNine.mvvm.vm
{
    public class VmCommandWithArg<T> : ICommand
    {
        private readonly Action<T> action;
        private readonly Func<T, bool> condition;

        public VmCommandWithArg(Action<T> action,
            Func<T, bool> condition)
        {
            this.action = action;
            this.condition = condition;
        }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return condition((T)parameter);
        }

        public void Execute(object? parameter)
        {
            action((T)parameter);
        }
    }
}
