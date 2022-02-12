using System;

namespace REghZyMVVM.Commands {
    /// <summary>
    /// A relay command, that allows passing a parameter to the command
    /// </summary>
    public class RelayCommandParam<T> : BaseRelayCommand {
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommandParam(Action<T> execute, Func<bool> canExecute = null) : base(canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Execute callback cannot be null");
            }

            this.execute = execute;
        }

        /// <summary>
        /// Executes the <see cref="RelayCommand"/> on the current command target
        /// </summary>
        /// <param name="parameter">
        /// Extra data as the command's parameter. Can be null
        /// </param>
        public override void Execute(object parameter) {
            // let c# deal with the class cast exception :--)
            // if (parameter == null || parameter is T) {
            this.execute((T) parameter);
            // }
            // else {
            //     throw new InvalidCastException($"Parameter type ({parameter.GetType().Name}) cannot be used for the callback method (which requires type {typeof(T).Name})");
            // }
        }
    }
}