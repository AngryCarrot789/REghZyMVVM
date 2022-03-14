using System.Collections.Generic;
using System.Text;

namespace REghZy.Utils {
    public class StringJoiner {
        private readonly StringBuilder Value;
        private readonly string EmptyValue;

        public string Delimiter { get; }
        public string Prefix { get; }
        public string Suffix { get; }

        public StringJoiner(string delimiter) : this(delimiter, string.Empty, string.Empty) { }
        public StringJoiner(string delimiter, string prefix, string suffix) {
            if (prefix == null || suffix == null) {
                this.Prefix = "";
                this.Suffix = "";
            }
            else {
                this.Prefix = prefix;
                this.Suffix = suffix;
            }

            this.Delimiter = delimiter ?? ", ";
            this.EmptyValue = this.Prefix + this.Suffix;
            this.Value = new StringBuilder(128);
            if (!string.IsNullOrEmpty(this.Prefix)) {
                this.Value.Append(this.Prefix);
            }
        }

        public StringJoiner Append(string value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(int value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(long value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(float value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(double value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(char value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append(bool value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public StringJoiner Append<T>(IEnumerable<T> values) {
            StringBuilder builder = PrepareBuilder();
            foreach (T value in values) {
                builder.Append(value);
            }
            return this;
        }

        public StringJoiner Append(char[] value) {
            PrepareBuilder().Append(value);
            return this;
        }

        public int Length() {
            return this.Value.Length + this.Suffix.Length;
        }

        private StringBuilder PrepareBuilder() {
            return this.Value.Length == 0 ? this.Value : this.Value.Append(this.Delimiter);
        }

        public override string ToString() {
            if (this.Value.Length == this.Prefix.Length) {
                return this.EmptyValue;
            }
            if (this.Suffix.Length == 0) {
                return this.Value.ToString();
            }

            int initialLength = this.Value.Length;
            string result = this.Value.Append(this.Suffix).ToString();
            this.Value.Remove(initialLength, this.Value.Length - initialLength);
            return result;
        }
    }
}
