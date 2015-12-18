namespace IconsReminder.Utility
{
    using System;
    using System.Windows.Input;

    public class CustomCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public CustomCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = (x) => { return true; };
        }

        public CustomCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
