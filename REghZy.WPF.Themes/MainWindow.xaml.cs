using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using REghZy.MathsF;
using REghZy.Streams;

namespace REghZy.WPF.Themes {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Task.Run((() => {
                Console.WriteLine("d");
            }));

            BinaryWriter writer = new BinaryWriter(null);
            writer.Write((int) 32);

            DataOutputStream output = new DataOutputStream(null);
            output.WriteInt(25);

            // Encoding.Unicode.GetBytes()
            // "ok".ToCharArray();
        }
    }
}