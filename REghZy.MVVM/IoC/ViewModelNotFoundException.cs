using System;
using System.Collections.Generic;
using System.Text;

namespace REghZy.MVVM.IoC {
    /// <summary>
    /// An exception thrown if a ViewModel couldn't be found by IoC
    /// </summary>
    public class ViewModelNotFoundException : Exception {
        /// <summary>
        /// The ViewModel type that couldn't be found
        /// </summary>
        public Type TargetType { get; }

        public ViewModelNotFoundException(Type targetType) : base($"The ViewModel type '{targetType.Name}' could not be found") {
            this.TargetType = targetType;
        }
    }
}
