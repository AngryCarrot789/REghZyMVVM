using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace REghZy.WPF.Controls {
    public class AutoClosingGridSplitter : GridSplitter {
        public static readonly DependencyProperty UseAutoCloseProperty =
            DependencyProperty.Register(
                nameof(UseAutoClose),
                typeof(bool),
                typeof(AutoClosingGridSplitter),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty MinSizeForAutoCloseProperty =
            DependencyProperty.Register(
                nameof(MinSizeForAutoClose),
                typeof(double),
                typeof(AutoClosingGridSplitter),
                new FrameworkPropertyMetadata(250.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty TargetDefinitionProperty =
            DependencyProperty.Register(
                nameof(TargetDefinition),
                typeof(DefinitionBase),
                typeof(AutoClosingGridSplitter),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty DragIncrement0Property =
            DependencyProperty.Register(
                nameof(DragIncrement0),
                typeof(double),
                typeof(AutoClosingGridSplitter),
                new FrameworkPropertyMetadata(1.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                nameof(IsOpen),
                typeof(bool),
                typeof(AutoClosingGridSplitter),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is AutoClosingGridSplitter splitter) {
                if (splitter.isOpenChanging) {
                    return;
                }
                else {
                    splitter.isSmall = ((bool) e.NewValue == false);
                }

                if (e.NewValue == e.OldValue) {
                    return;
                }

                DefinitionBase definition = splitter.TargetDefinition;
                if (definition == null) {
                    return;
                }

                if (definition is ColumnDefinition column) {
                    if ((bool) e.NewValue) {
                        column.Width = new GridLength(splitter.MinSizeForAutoClose + 1);
                        splitter.DragIncrement = splitter.DragIncrement0;
                    }
                    else {
                        column.Width = new GridLength(0.0d);
                        splitter.DragIncrement = splitter.MinSizeForAutoClose;
                    }
                }
            }
        }

        private bool isSmall = false;
        private bool isOpenChanging = false;

        public bool UseAutoClose {
            get => (bool) GetValue(UseAutoCloseProperty);
            set => SetValue(UseAutoCloseProperty, value);
        }

        public double MinSizeForAutoClose {
            get => (double) GetValue(MinSizeForAutoCloseProperty);
            set => SetValue(MinSizeForAutoCloseProperty, value);
        }

        public DefinitionBase TargetDefinition {
            get => (DefinitionBase) GetValue(TargetDefinitionProperty);
            set => SetValue(TargetDefinitionProperty, value);
        }

        public bool IsOpen {
            get => (bool) GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public double DragIncrement0 {
            get => (double) GetValue(DragIncrement0Property);
            set => SetValue(DragIncrement0Property, value);
        }

        public AutoClosingGridSplitter() {
            base.DragDelta += OnDragged;
        }

        private void OnDragged(object sender, DragDeltaEventArgs e) {
            if (this.TargetDefinition is ColumnDefinition column) {
                double sizeForAutoClose = this.MinSizeForAutoClose;
                double columnWidth = column.ActualWidth;
                if (this.isSmall) {
                    if (columnWidth <= 0.0d) {
                        this.isOpenChanging = true;
                        this.IsOpen = false;
                        this.isOpenChanging = false;
                    }
                    else if (columnWidth >= sizeForAutoClose && e.HorizontalChange < 0.0d) {
                        this.isSmall = false;
                        this.DragIncrement = this.DragIncrement0;
                        this.isOpenChanging = true;
                        this.IsOpen = true;
                        this.isOpenChanging = false;
                        column.Width = new GridLength(sizeForAutoClose);
                    }
                    else if (columnWidth > sizeForAutoClose) {
                        this.isSmall = false;
                        this.DragIncrement = this.DragIncrement0;
                        this.isOpenChanging = true;
                        this.IsOpen = true;
                        this.isOpenChanging = false;
                        column.Width = new GridLength(sizeForAutoClose);
                    }
                }
                else if (columnWidth < sizeForAutoClose) {
                    this.isSmall = true;
                    this.DragIncrement = sizeForAutoClose;
                    column.Width = new GridLength(sizeForAutoClose);
                }
                else {
                    this.isSmall = false;
                }

                // these dont work :(
                // if (column.ActualWidth != this.lastWidth) {
                //     if (this.lastWidth == 0 && column.ActualWidth >= sizeForAutoClose) {
                //         this.isSmall = false;
                //         this.DragIncrement = this.DragIncrement0;
                //         this.isOpenChangingInternal = true;
                //         this.IsOpen = true;
                //         this.isOpenChangingInternal = false;
                //         column.Width = new GridLength(sizeForAutoClose);
                //     }
                // }
                //
                // if (column.ActualWidth >= sizeForAutoClose) {
                //     this.isOpenChangingInternal = true;
                //     this.IsOpen = true;
                //     this.isOpenChangingInternal = false;
                // }
                //
                // this.lastWidth = column.ActualWidth;
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
            base.OnMouseDoubleClick(e);
            this.IsOpen = !this.IsOpen;
        }
    }
}