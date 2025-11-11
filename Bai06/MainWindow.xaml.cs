using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private double currentValue = 0;
        private double memoryValue = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;
        private double lastValue = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (isNewEntry)
            {
                DisplayTextBox.Text = number;
                isNewEntry = false;
            }
            else
            {
                if (DisplayTextBox.Text == "0")
                    DisplayTextBox.Text = number;
                else
                    DisplayTextBox.Text += number;
            }
        }
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry)
            {
                DisplayTextBox.Text = "0.";
                isNewEntry = false;
            }
            else if (!DisplayTextBox.Text.Contains("."))
            {
                DisplayTextBox.Text += ".";
            }
        }
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Content.ToString();

            if (!isNewEntry && currentOperator != "")
            {
                Calculate();
            }
            else
            {
                currentValue = double.Parse(DisplayTextBox.Text);
            }

            currentOperator = op;
            isNewEntry = true;
        }

        private void Calculate()
        {
            double displayValue = double.Parse(DisplayTextBox.Text);
            lastValue = displayValue;

            switch (currentOperator)
            {
                case "+":
                    currentValue = currentValue + displayValue;
                    break;
                case "-":
                    currentValue = currentValue - displayValue;
                    break;
                case "*":
                    currentValue = currentValue * displayValue;
                    break;
                case "/":
                    if (displayValue != 0)
                        currentValue = currentValue / displayValue;
                    else
                    {
                        MessageBox.Show("Cannot divide by zero", "Error");
                        C_Click(null, null);
                        return;
                    }
                    break;
            }

            DisplayTextBox.Text = FormatResult(currentValue);
        }

        private string FormatResult(double value)
        {
            if (Math.Abs(value) < 1e-10)
                return "0";

            string result = value.ToString();

            if (result.Length > 16)
            {
                if (result.Contains("."))
                    result = value.ToString("G10");
                else
                    result = value.ToString("E6");
            }

            return result;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (currentOperator != "")
            {
                Calculate();
                currentOperator = "";
            }
            isNewEntry = true;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "0";
            currentValue = 0;
            currentOperator = "";
            isNewEntry = true;
            lastValue = 0;
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "0";
            isNewEntry = true;
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewEntry && DisplayTextBox.Text.Length > 0)
            {
                DisplayTextBox.Text = DisplayTextBox.Text.Substring(0, DisplayTextBox.Text.Length - 1);
                if (DisplayTextBox.Text == "" || DisplayTextBox.Text == "-")
                {
                    DisplayTextBox.Text = "0";
                    isNewEntry = true;
                }
            }
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(DisplayTextBox.Text);
            value = -value;
            DisplayTextBox.Text = FormatResult(value);
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(DisplayTextBox.Text);
            if (value >= 0)
            {
                value = Math.Sqrt(value);
                DisplayTextBox.Text = FormatResult(value);
                isNewEntry = true;
            }
            else
            {
                MessageBox.Show("Invalid input", "Error");
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(DisplayTextBox.Text);
            if (currentOperator == "+" || currentOperator == "-")
            {
                value = currentValue * value / 100;
            }
            else
            {
                value = value / 100;
            }
            DisplayTextBox.Text = FormatResult(value);
            isNewEntry = true;
        }

        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(DisplayTextBox.Text);
            if (value != 0)
            {
                value = 1 / value;
                DisplayTextBox.Text = FormatResult(value);
                isNewEntry = true;
            }
            else
            {
                MessageBox.Show("Cannot divide by zero", "Error");
            }
        }

        private void MC_Click(object sender, RoutedEventArgs e)
        {
            memoryValue = 0;
        }

        private void MR_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = FormatResult(memoryValue);
            isNewEntry = true;
        }

        private void MS_Click(object sender, RoutedEventArgs e)
        {
            memoryValue = double.Parse(DisplayTextBox.Text);
            isNewEntry = true;
        }

        private void MPlus_Click(object sender, RoutedEventArgs e)
        {
            memoryValue += double.Parse(DisplayTextBox.Text);
            isNewEntry = true;
        }
    }
}