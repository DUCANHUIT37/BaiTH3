using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Bai2_PaintEvent
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(paintCanvas);
            TextBlock paintText = new TextBlock
            {
                Text = "Paint Event",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(paintText, clickPosition.X);
            Canvas.SetTop(paintText, clickPosition.Y);
            paintCanvas.Children.Add(paintText);
        }
    }
}