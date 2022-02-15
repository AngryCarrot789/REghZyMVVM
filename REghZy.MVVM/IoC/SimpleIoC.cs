using System;
using System.Collections.Generic;
using REghZy.MVVM.ViewModels;

namespace REghZy.MVVM.IoC {
    /// <summary>
    /// A simple IoC container, containing ViewModels and Services
    /// <para>
    /// You could create a private static instance of this class, and use public static wrapper methods
    /// </para>
    /// </summary>
    public class SimpleIoC {
        private readonly Dictionary<Type, BaseViewModel> viewmodels;
        private readonly Dictionary<Type, object> services;

        public Dictionary<Type, BaseViewModel> ViewModels => this.viewmodels;

        public Dictionary<Type, object> Services => this.services;

        public SimpleIoC() {
            this.viewmodels = new Dictionary<Type, BaseViewModel>();
            this.services = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the ViewModel instance of the given type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <returns>The instance of the ViewModel</returns>
        /// <exception cref="ViewModelNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target ViewModel type doesn't match the actual ViewModel type (somehow)</exception>
        public T GetViewModel<T>() where T : BaseViewModel {
            if (this.viewmodels.TryGetValue(typeof(T), out BaseViewModel viewModel)) {
                if (viewModel is T t) {
                    return t;
                }

                throw new InvalidCastException($"The target ViewModel type '{typeof(T).Name}' is incompatiable with actual service type '{viewModel.GetType().Name}'");
            }

            throw new ViewModelNotFoundException(typeof(T));
        }

        /// <summary>
        /// Gets the ViewModel instance of the given type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <returns>The instance of the ViewModel</returns>
        /// <exception cref="ViewModelNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target ViewModel type doesn't match the actual ViewModel type (somehow)</exception>
        public BaseViewModel GetViewModel(Type type) {
            if (this.viewmodels.TryGetValue(type, out BaseViewModel viewModel)) {
                if (type.IsInstanceOfType(viewModel)) {
                    return viewModel;
                }

                throw new InvalidCastException($"The target ViewModel type '{type}' is incompatiable with actual service type '{viewModel.GetType()}'");
            }

            throw new ViewModelNotFoundException(type);
        }

        /// <summary>
        /// Gets the service instance of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically the base type)</typeparam>
        /// <returns>The instance of the service</returns>
        /// <exception cref="ServiceNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target service type doesn't match the actual service type</exception>
        public T GetService<T>() {
            if (this.services.TryGetValue(typeof(T), out object service)) {
                if (service is T t) {
                    return t;
                }

                throw new InvalidCastException($"The target service type '{typeof(T)}' is incompatiable with actual service type '{(service == null ? "NULL" : service.GetType().Name)}'");
            }

            throw new ServiceNotFoundException(typeof(T));
        }

        /// <summary>
        /// Gets the service instance of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically the base type)</typeparam>
        /// <returns>The instance of the service</returns>
        /// <exception cref="ServiceNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target service type doesn't match the actual service type</exception>
        public object GetService(Type type) {
            if (this.services.TryGetValue(type, out object service)) {
                if (type.IsInstanceOfType(service)) {
                    return service;
                }

                throw new InvalidCastException($"The target service type '{type}' is incompatiable with actual service type '{(service == null ? "NULL" : service.GetType().Name)}'");
            }

            throw new ServiceNotFoundException(type);
        }

        /// <summary>
        /// Registers (or replaces) a viewmode of the given generic type
        /// </summary>
        /// <typeparam name="T">The ViewModel type</typeparam>
        /// <param name="viewModel"></param>
        public void SetViewModel<T>(T viewModel) where T : BaseViewModel {
            this.viewmodels[typeof(T)] = viewModel;
        }

        /// <summary>
        /// Registers (or replaces) a viewmode of the given generic type
        /// </summary>
        /// <param name="viewModel"></param>
        public void SetViewModel(Type type, BaseViewModel viewModel) {
            this.viewmodels[type] = viewModel;
        }

        /// <summary>
        /// Registers (or replaces) the given service of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically an interface, for an API service)</typeparam>
        /// <param name="service"></param>
        public void SetService<T>(T service) {
            this.services[typeof(T)] = service;
        }

        /// <summary>
        /// Registers (or replaces) the given service of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically an interface, for an API service)</typeparam>
        /// <param name="service"></param>
        public void SetService(Type type, object service) {
            this.services[type] = service;
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given ViewModel
        /// </summary>
        /// <typeparam name="T">The viewmodel type</typeparam>
        /// <returns></returns>
        public bool HasViewModel<T>() where T : BaseViewModel {
            return this.viewmodels.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given ViewModel
        /// </summary>
        /// <returns></returns>
        public bool HasViewModel(Type type) {
            return this.viewmodels.ContainsKey(type);
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given service
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <returns></returns>
        public bool HasService<T>() {
            return this.services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Returns whether this IoC manager contains a given service
        /// </summary>
        /// <returns></returns>
        public bool HasService(Type type) {
            return this.services.ContainsKey(type);
        }
    }
}
