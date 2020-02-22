using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFMusicplayer.Commands
{
    public class Command : ICommand
    {

        readonly Action<object> _executeMethod;
        readonly Func<object, bool> _canexecuteMethod;

        public Command(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canexecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke(parameter);
        }
    }
}
