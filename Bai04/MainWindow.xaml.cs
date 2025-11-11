using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bai4_MenuColorDialog
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            colorPickerDialog.Visibility = Visibility.Visible;
        }

        private void SelectColor_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                SolidColorBrush selectedColor = clickedButton.Background as SolidColorBrush;

                if (selectedColor != null)
                {
                    mainGrid.Background = selectedColor;
                }
            }

            colorPickerDialog.Visibility = Visibility.Collapsed;
        }

        private void CancelColor_Click(object sender, RoutedEventArgs e)
        {
            colorPickerDialog.Visibility = Visibility.Collapsed;
        }
    }
}