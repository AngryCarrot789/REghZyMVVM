namespace REghZy.WPF.Controls {
    public class Range {
        public readonly double valueMin;
        public readonly double valueMax;
        public readonly double scaledMin;
        public readonly double scaledMax;
        public readonly double difference;

        public Range(double valueMin, double valueMax, double scaledMin, double scaledMax) {
            this.valueMin = valueMin;
            this.valueMax = valueMax;
            this.scaledMin = scaledMin;
            this.scaledMax = scaledMax;
            this.difference = scaledMax - scaledMin;
        }

        public double GetMapped(double value) {
            return Map(this.valueMin, this.valueMax, this.scaledMin, this.scaledMax, value);
        }

        public static double Map(double fromA, double toA, double fromB, double toB, double value) {
            return fromB + ((toB - fromB) / (toA - fromA)) * (value - fromA);
        }

        public double GetScaledFromSliderValue(double value) {
            double mapped = GetMapped(value);
            return Lerp(mapped, this.scaledMax, PercentageOfSlider(mapped));
        }

        public double PercentageOfSlider(double value) {
            return (value - this.scaledMin) / this.difference;
        }

        public static double Lerp(double a, double b, double amount) {
            return a + (b - a) * amount;
        }

        public bool IsBetween(double value) {
            return value >= this.valueMin && value < this.valueMax;
        }
    }
}