using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace REghZyMVVM.ViewModels {
    /// <summary>
    /// An abstract class that implements <see cref="INotifyPropertyChanged"/>, allowing data binding between a ViewModel and a View, 
    /// along with some helper function to, for example, run an <see cref="Action"/> before or after the PropertyRaised event has been risen
    /// <para>
    ///     This class should normally be inherited by a ViewModel, such as a MainViewModel for the main view
    /// </para>
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises a propertychanged event, allowing the view to be updated. Pass in your private property, new value, 
        /// can also pass the property name but that's done for you.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The private field that is used for "setting". This can</param>
        /// <param name="newValue">The new value of this property</param>
        /// <param name="propertyName">dont need to specify this, but the name of the property/field</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// If checkEquality is true, and the propery and newValue are equal, then nothing will happen.
        /// Otherwise, the property changed event will be raised
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">A reference to the private field, whose value may be replaced with newValue</param>
        /// <param name="newValue">The possible new value of this property</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChanged<T>(ref T property, T newValue, bool checkEquality, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            if (checkEquality && EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Calls the given pre-property-changed callback, raises the property changed event, and invokes the given post-property-changed callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The private field that is used for "setting"</param>
        /// <param name="newValue">The new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, Action<T> postChangedCallback = null, Action<T> preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            preChangedCallback?.Invoke(property);
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke(property);
        }

        /// <summary>
        /// If checkEquality is true, and the propery and newValue are equal, then nothing will happen.
        /// Otherwise, the given pre-property-changed callback will be invoked, then the property changed
        /// event will be raised, and then the post-property-changed callback will be raised
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, bool checkEquality, Action<T> postChangedCallback = null, Action<T> preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            if (checkEquality && EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            preChangedCallback?.Invoke(property);
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke(property);
        }

        /// <summary>
        /// Calls the given pre-property-changed callback, raises the property changed event, and invokes the given post-property-changed callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The private field that is used for "setting"</param>
        /// <param name="newValue">The new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, Action postChangedCallback = null, Action preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            preChangedCallback?.Invoke();
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke();
        }

        /// <summary>
        /// If checkEquality is true, and the propery and newValue are equal, then nothing will happen.
        /// Otherwise, the given pre-property-changed callback will be invoked, then the property changed
        /// event will be raised, and then the post-property-changed callback will be raised
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">the private field that is used for "setting"</param>
        /// <param name="newValue">the new value of this property</param>
        /// <param name="postChangedCallback">The method that gets called after the property changed, and contains the new value as a parameter</param>
        /// <param name="preChangedCallback">The method that gets called before the property changed, and contains the old value as a parameter</param>
        /// <param name="propertyName">Property name. This will be auto-filled by the compiler</param>
        public void RaisePropertyChangedWithCallback<T>(ref T property, T newValue, bool checkEquality, Action postChangedCallback = null, Action preChangedCallback = null, [CallerMemberName] string propertyName = null) {
            if (propertyName == null) {
                throw new ArgumentNullException("propertyName", "Property Name is null");
            }

            if (checkEquality && EqualityComparer<T>.Default.Equals(property, newValue)) {
                return;
            }

            preChangedCallback?.Invoke();
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            postChangedCallback?.Invoke();
        }
    }
}