using System;
using System.Windows.Input;

namespace REghZy.MVVM.Commands {
    /// <summary>
    /// A base relay command class, that implements ICommand, and also has a simple
    /// implementation for dealing with the <see cref="CanExecuteChanged"/> event handler
    /// </summary>
    public abstract class BaseRelayCommand : ICommand {
        /// <summary>
        /// True if command is executing, false otherwise
        /// </summary>
        protected readonly Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of <see cref="BaseRelayCommand"/>
        /// </summary>
        /// <param name="canExecute">The execution status logic</param>
        public BaseRelayCommand(Func<bool> canExecute = null) {
            this.canExecute = canExecute;
        }

        public abstract void Execute(object parameter);

        /// <summary>
        /// Raised when <see cref="RaiseCanExecuteChanged"/> is called
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether this <see cref="BaseRelayCommand"/> can execute in its current state
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. This may be null, if the command does not require a parameter
        /// </param>
        /// <returns>
        /// True if the command can be executed, otherwise false if it cannot be executed
        /// </returns>
        public bool CanExecute(object parameter) {
            return this.canExecute == null ? true : this.canExecute();
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event to indicate that the
        /// return value of the <see cref="CanExecute"/> method likely changed
        /// <para>
        /// This can be called from a view model, which, for example, may cause a
        /// button to become greyed out (disabled) if <see cref="CanExecute"/> returns false
        /// </para>
        /// </summary>
        public void RaiseCanExecuteChanged() {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}