using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFMusicplayer.Commands
{
    public class DelegateCommand : ICommand
    {
        readonly Action<object> _executeMethod;
        readonly Func<object, bool> _canexecuteMethod;

        public DelegateCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canexecuteMethod = canexecuteMethod;
        }

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return (_canexecuteMethod == null) ? true : _canexecuteMethod(parameter);


        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke(parameter);
        }
    }
}
