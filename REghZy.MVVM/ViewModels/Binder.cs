using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace REghZy.MVVM.ViewModels {
    /// <summary>
    /// A class that helps with binding properties between view models
    /// </summary>
    public static class Binder {
        // private static readonly Dictionary<BaseViewModel, Dictionary<BaseViewModel, BinderInfo>> BINDERS;
        private static readonly object BINDER_LOCK = new object();
        private static BaseViewModel TARGET;
        private static string TARGET_PROPERTY;

        private static BinderInfo CreateBinder(BaseViewModel obj, string property, BaseViewModel target, string targetProperty) {
            if (obj == null) {
                throw new ArgumentNullException(nameof(obj), "View model cannot be null");
            }

            if (property == null) {
                throw new ArgumentNullException(nameof(property), "Property cannot be null");
            }

            if (target == null) {
                throw new ArgumentNullException(nameof(target), "Target view model cannot be null");
            }

            if (targetProperty == null) {
                throw new ArgumentNullException(nameof(targetProperty), "Target property cannot be null");
            }

            return new BinderInfo(obj, property, target, targetProperty);
        }

        /// <summary>
        /// Binds a property from 'fromObj', and sets the property in 'toObj'
        /// <para>
        /// Binding both ways is supported
        /// </para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="targetObj"></param>
        /// <param name="targetProperty"></param>
        public static void Bind(BaseViewModel obj, string property, BaseViewModel targetObj, string targetProperty) {
            BinderInfo info = CreateBinder(obj, property, targetObj, targetProperty);
            obj.PropertyChanged += (sender, args) => {
                if (args.PropertyName == property) {
                    info.Set();
                }
            };
        }

        private class BinderInfo {
            private readonly BaseViewModel obj;
            private readonly BaseViewModel target;
            private readonly string property;
            private readonly string targetProperty;

            private readonly Expression getProperty;
            private readonly Expression getTargetProperty;
            private readonly Action setTarget;

            private bool isBeingSet;
            // private BinderInfo flipped;

            public BinderInfo(BaseViewModel obj, string property, BaseViewModel target, string targetProperty) {
                this.obj = obj;
                this.property = property;
                this.target = target;
                this.targetProperty = targetProperty;
                this.getProperty = Expression.Property(Expression.Constant(this.obj), this.property);
                this.getTargetProperty = Expression.Property(Expression.Constant(this.target), this.targetProperty);
                this.setTarget = Expression.Lambda<Action>(Expression.Assign(this.getTargetProperty, this.getProperty)).Compile();
                // this.flipped = GetBinder(target, obj);
            }

            public void Set() {
                if (this.isBeingSet) {
                    return;
                }

                if (TARGET == this.obj && TARGET_PROPERTY == this.property) {
                    return;
                }

                object locker = BINDER_LOCK;
                try {
                    Monitor.Enter(locker);
                    TARGET = this.obj;
                    TARGET_PROPERTY = this.property;
                    this.isBeingSet = true;
                    this.setTarget();
                }
                catch (Exception e) {
                    throw new Exception($"Failed to set target ({this.obj.GetType().Name}::{this.property})", e);
                }
                finally {
                    this.isBeingSet = false;
                    TARGET = null;
                    TARGET_PROPERTY = null;
                    Monitor.Exit(locker);
                }
            }
        }
    }
}