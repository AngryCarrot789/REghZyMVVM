using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZy.Utils;

namespace REghZy.MVVM.Framework.Converters {
    public class BoolToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }
            else {
                bool hide = false;
                bool invert = false;
                if (parameter != null) {
                    string format = parameter.ToString().ToLower();
                    int split = format.IndexOf('|');
                    if (split == -1) {
                        invert = format == "invert";
                    }
                    else {
                        invert = format.JSubstring(0, split) == "invert";
                        hide = format.JSubstring(split + 1) == "hide";
                    }
                }

                if (value is bool visible) {
                    if (invert) {
                        visible = !visible;
                    }

                    return visible ? Visibility.Visible : (hide ? Visibility.Hidden : Visibility.Collapsed);
                }
                else {
                    throw new Exception("Cannot convert unknown type: " + value.GetType());
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }
            else {
                bool hide = false;
                bool invert = false;
                if (parameter != null) {
                    string format = parameter.ToString().ToLower();
                    int split = format.IndexOf('|');
                    if (split == -1) {
                        invert = format == "invert";
                    }
                    else {
                        invert = format.JSubstring(0, split) == "invert";
                        hide = format.JSubstring(split + 1) == "hide";
                    }
                }

                if (value is Visibility visibility) {
                    if (invert) {
                        return hide ? visibility == Visibility.Hidden : visibility == Visibility.Collapsed;
                    }
                    else {
                        return visibility == Visibility.Visible;
                    }
                }
                else {
                    throw new Exception("Cannot convert back from unknown type: " + value.GetType());
                }
            }
        }
    }
}
