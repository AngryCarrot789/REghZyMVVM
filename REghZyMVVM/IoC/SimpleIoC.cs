using System;
using System.Collections.Generic;
using REghZyMVVM.ViewModels;

namespace REghZyMVVM.IoC {
    /// <summary>
    /// A simple IoC container, containing ViewModels and Services
    /// <para>
    /// You could create a private static instance of this class, and use public static wrapper methods
    /// </para>
    /// </summary>
    public class SimpleIoC {
        private readonly Dictionary<Type, BaseViewModel> viewmodels;
        private readonly Dictionary<Type, IService> services;

        public Dictionary<Type, BaseViewModel> ViewModels => this.viewmodels;

        public Dictionary<Type, IService> Services => this.services;

        public SimpleIoC() {
            this.viewmodels = new Dictionary<Type, BaseViewModel>();
            this.services = new Dictionary<Type, IService>();
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
        /// Gets the service instance of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically the base type)</typeparam>
        /// <returns>The instance of the service</returns>
        /// <exception cref="ServiceNotFoundException">Thrown if there isn't a ViewModel of that type</exception>
        /// <exception cref="InvalidCastException">Thrown if the target service type doesn't match the actual service type</exception>
        public T GetService<T>() where T : IService {
            if (this.services.TryGetValue(typeof(T), out IService service)) {
                if (service is T t) {
                    return t;
                }

                throw new InvalidCastException($"The target service type '{typeof(T).Name}' is incompatiable with actual service type '{service.GetType().Name}'");
            }

            throw new ServiceNotFoundException(typeof(T));
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
        /// Registers (or replaces) the given service of the given generic type
        /// </summary>
        /// <typeparam name="T">The service type (typically an interface, for an API service)</typeparam>
        /// <param name="service"></param>
        public void SetService<T>(T service) where T : IService {
            this.services[typeof(T)] = service;
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
        /// Returns whether this IoC manager contains a given service
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <returns></returns>
        public bool HasService<T>() where T : IService {
            return this.services.ContainsKey(typeof(T));
        }
    }
}
