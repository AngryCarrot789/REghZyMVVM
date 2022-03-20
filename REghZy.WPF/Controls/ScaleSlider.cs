using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;

namespace REghZy.WPF.Controls {
    public class ScaleSlider : Slider {
        private readonly Range[] scales;

        private int index;

        public Range ActiveRange => (this.index == -1 || (this.index + 1) >= this.scales.Length) ? null : this.scales[this.index];

        public Range Next => ((this.index + 1) == 0 || (this.index + 2) >= this.scales.Length) ? null : this.scales[this.index + 1];

        public bool HasNext => (this.index + 1) < this.scales.Length;

        public static readonly DependencyProperty ScaledValueProperty =
            DependencyProperty.Register(
                nameof(ScaledValue),
                typeof(double),
                typeof(ScaleSlider),
                new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScaledValuePropertyChanged));

        private static void OnScaledValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ScaleSlider slider = (ScaleSlider) d;
            if (slider.setInternal) {
                // prevent stack overflow; setting ScaledValue externally updates Value
                return;
            }


        }

        public double ScaledValue {
            get => (double) GetValue(ScaledValueProperty);
            set => SetValue(ScaledValueProperty, value);
        }

        private bool setInternal;

        public ScaleSlider() {
            this.Loaded += OnLoaded;
            this.scales = new Range[5];
            this.scales[0] = new Range(0.0d,  20.0d,  0.0d,  10.0d);
            this.scales[1] = new Range(20.0d, 50.0d,  10.0d, 15.0d);
            this.scales[2] = new Range(50.0d, 75.0d,  35.0d, 85.0d);
            this.scales[3] = new Range(75.0d, 90.0d,  85.0d, 95.0d);
            this.scales[4] = new Range(90.0d, 100.0d, 95.0d, 100.0d);
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            // this.index = GetRangeIndexFromValue(this.Value);
        }

        protected override void OnValueChanged(double oldValue, double value) {
            // v = 0.8, s = 0.333
            // pow(0.8, 10) =  0.107
            // pow(0.8, 5)  =  0.32768

            double x = Map(this.Minimum, this.Maximum, 0.0d, 1.0d, value);
            double v = Math.Sin(Math.Pow(x, 10)) / Math.Sin(Math.Pow(x, 5));
            if (double.IsNaN(v) || v < 0.0d) {
                v = 0.0d;
            }
            // if (x < 0.5) {
            //     v = Math.Pow(2, 20 * x - 10) / 2;
            // }
            // else {
            //     v = (2 - Math.Pow(2, -20 * x + 10)) / 2;
            // }

            this.ScaledValue = Map(0.0d, 1.0d, this.Minimum, this.Maximum, v);

            // if (this.HasNext) {
            //     Range next = this.Next;
            //     if (next != null && next.IsBetween(value)) {
            //         this.index++;
            //     }
            // }
            // Range active = this.ActiveRange;
            // if (active != null) {
            //     if (!active.IsBetween(value)) {
            //         this.index--;
            //     }
            //     active = this.ActiveRange;
            //     if (active != null) {
            //         this.ScaledValue = active.GetScaledFromSliderValue(value);
            //     }
            // }

            base.OnValueChanged(oldValue, value);
        }

        private int GetRangeIndexFromValue(double value) {
            for (int i = 0; i < this.scales.Length; i++) {
                Range range = this.scales[i];
                if (range.IsBetween(value)) {
                    return i;
                }
            }

            return -1;
        }

        public static double Map(double fromA, double toA, double fromB, double toB, double value) {
            return fromB + ((toB - fromB) / (toA - fromA)) * (value - fromA);
        }
    }
}