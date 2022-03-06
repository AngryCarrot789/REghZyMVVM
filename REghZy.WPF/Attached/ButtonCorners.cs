using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace REghZy.WPF.Attached {
    public static class ButtonCorners {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(ButtonCorners));

        [Category("Appearance")]
        public static CornerRadius GetCornerRadius(ButtonBase button) {
            return (CornerRadius) button.GetValue(CornerRadiusProperty);
        }

        [Category("Appearance")]
        public static void SetCornerRadius(ButtonBase button, CornerRadius value) {
            button.SetValue(CornerRadiusProperty, value);
        }
    }
}