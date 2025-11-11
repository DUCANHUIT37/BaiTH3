using System;
using System.Windows;

namespace Lab02Example
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation((a, b) => a + b);
        }

        private void BtnSubtract_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation((a, b) => a - b);
        }

        private void BtnMultiply_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation((a, b) => a * b);
        }

        private void BtnDivide_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out double num1, out double num2))
                return;

            if (num2 == 0)
            {
                MessageBox.Show("Không thể chia cho 0!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            txtAnswer.Text = (num1 / num2).ToString();
        }

        private void PerformOperation(Func<double, double, double> operation)
        {
            if (!ValidateInputs(out double num1, out double num2))
                return;

            txtAnswer.Text = operation(num1, num2).ToString();
        }

        private bool ValidateInputs(out double num1, out double num2)
        {
            num1 = 0;
            num2 = 0;

            if (!double.TryParse(txtNumber1.Text, out num1))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ vào Number 1!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNumber1.Focus();
                return false;
            }

            if (!double.TryParse(txtNumber2.Text, out num2))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ vào Number 2!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNumber2.Focus();
                return false;
            }

            return true;
        }
    }
}