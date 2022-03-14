using REghZy.MathsF;

namespace REghZy.WPF.Themes {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Vector3f a = new Vector3f(0.5f, 0.0f, 0.1f) * new Vector3f(5.0f, 10.0f, 15.0f);
            Vector3f ff = new Vector3f() * a;
        }
    }
}