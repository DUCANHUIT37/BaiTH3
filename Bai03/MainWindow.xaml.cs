using System;
using System.Windows;
using System.Windows.Media;

namespace Bai3_ChangeColor
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {
            byte red = (byte)random.Next(256);
            byte green = (byte)random.Next(256);
            byte blue = (byte)random.Next(256);
            Color randomColor = Color.FromRgb(red, green, blue);
            colorGrid.Background = new SolidColorBrush(randomColor);
        }
    }
}