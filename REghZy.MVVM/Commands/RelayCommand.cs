using System;

namespace REghZy.MVVM.Commands {
    /// <summary>
    /// A simple relay command, which does not take any parameters
    /// </summary>
    public class RelayCommand : BaseRelayCommand {
        /// <summary>
        /// Creates a new command that can always execute
        /// </summary>
        private readonly Action execute;

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/>
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null) : base(canExecute) {
            if (execute == null) {
                throw new ArgumentNullException("execute", "Execute callback cannot be null");
            }

            this.execute = execute;
        }

        /// <summary>
        /// Executes the <see cref="RelayCommand"/> on the current command target
        /// </summary>
        /// <param name="parameter">
        /// Extra data as the command's parameter. This can be null
        /// </param>
        public override void Execute(object parameter) {
            this.execute();
        }
    }
}