using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using REghZy.Utils;

namespace REghZy.MVVM.Framework.Converters {
    public class BoolToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool boolean) {
                if (parameter == null) {
                    return value is bool b && b ? "True" : "False";
                }
                else if (parameter == DependencyProperty.UnsetValue) {
                    return "(Unset Parameter)";
                }
                else {
                    string content = parameter.ToString();
                    int split = content.IndexOf('|');
                    if (split == -1) {
                        throw new Exception("Missing the '|' splitter char in the parameter's content");
                    }
                    else if (boolean) {
                        return content.JSubstring(0, split);
                    }
                    else {
                        return content.JSubstring(split + 1);
                    }
                }
            }
            else if (value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }
            else if (value == null) {
                return "(null)";
            }
            else {
                return "(Not a boolean: " + value.GetType().Name + ")";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string tostr) {
                if (parameter == null) {
                    return tostr == "True";
                }
                else if (parameter == DependencyProperty.UnsetValue) {
                    return false;
                }
                else {
                    string content = parameter.ToString();
                    int split = content.IndexOf('|');
                    if (split == -1) {
                        throw new Exception("Missing the '|' splitter char in the parameter's content");
                    }
                    else {
                        return tostr == content.JSubstring(0, split);
                    }
                }
            }
            else if (value == DependencyProperty.UnsetValue) {
                return DependencyProperty.UnsetValue;
            }
            else if (value == null) {
                return false;
            }
            else {
                return false;
            }
        }
    }
}