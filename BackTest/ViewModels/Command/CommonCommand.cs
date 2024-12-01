using System.Windows.Input;

namespace BackTest.ViewModels
{
    public class CommonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _action;

        public CommonCommand(Action action_) 
        {
            _action = action_;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(_action != null)
            {
                _action();
            }
        }
    }
}
