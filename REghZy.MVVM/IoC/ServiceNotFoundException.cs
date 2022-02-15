using System;
using System.Collections.Generic;
using System.Text;

namespace REghZy.MVVM.IoC {
    /// <summary>
    /// An exception thrown if a Service couldn't be found by IoC
    /// </summary>
    public class ServiceNotFoundException : Exception {
        /// <summary>
        /// The Service type that couldn't be found
        /// </summary>
        public Type TargetType { get; }

        public ServiceNotFoundException(Type targetType) : base($"The Service type '{targetType.Name}' could not be found") {
            this.TargetType = targetType;
        }
    }
}
