using System;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class SaveCommand : ICommand
    {
        public Action<object> _excute { get; set; }
        public Predicate<object> _canexcute { get; set; }

        public SaveCommand(Action<object> ExcuteMethod,Predicate<object> CanExcuteMethod)
        {
            _excute = ExcuteMethod;
            _canexcute = CanExcuteMethod;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canexcute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _excute.Invoke(parameter);
        }
    }
}
