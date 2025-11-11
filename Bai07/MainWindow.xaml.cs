using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaTicketApp
{
    public partial class MainWindow : Window
    {
        private const int PRICE_A = 5000;  
        private const int PRICE_B = 6500;  
        private const int PRICE_C = 8000;  

        private readonly SolidColorBrush WHITE = new SolidColorBrush(Colors.White);        
        private readonly SolidColorBrush YELLOW = new SolidColorBrush(Colors.Yellow);      
        private readonly SolidColorBrush BLUE = new SolidColorBrush(Colors.Blue);          
        private Dictionary<Button, string> seatStates = new Dictionary<Button, string>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeSeats();
        }
        private void InitializeSeats()
        {
            for (int i = 1; i <= 15; i++)
            {
                Button seat = (Button)this.FindName($"Seat{i}");
                if (seat != null)
                {
                    seatStates[seat] = "available";
                    seat.Background = WHITE;
                    seat.Foreground = Brushes.Black;
                }
            }
        }
        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button clickedSeat = (Button)sender;
            string currentState = seatStates[clickedSeat];

            switch (currentState)
            {
                case "available":
                    seatStates[clickedSeat] = "selected";
                    clickedSeat.Background = BLUE;
                    clickedSeat.Foreground = Brushes.White;
                    break;

                case "selected":
                    seatStates[clickedSeat] = "available";
                    clickedSeat.Background = WHITE;
                    clickedSeat.Foreground = Brushes.Black;
                    break;

                case "sold":
                    MessageBox.Show($"Ghế số {clickedSeat.Content} đã được bán!",
                                    "Thông báo",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    break;
            }
            UpdateTemporaryTotal();
        }
        private void UpdateTemporaryTotal()
        {
            int total = 0;

            foreach (var seat in seatStates)
            {
                if (seat.Value == "selected")
                {
                    string zone = seat.Key.Tag.ToString();
                    total += GetPriceByZone(zone);
                }
            }

            TotalAmountTextBox.Text = total.ToString("N0") + " VNĐ";
        }

        private int GetPriceByZone(string zone)
        {
            switch (zone)
            {
                case "A":
                    return PRICE_A;
                case "B":
                    return PRICE_B;
                case "C":
                    return PRICE_C;
                default:
                    return 0;
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            int total = 0;
            List<string> selectedSeats = new List<string>();

            foreach (var seat in seatStates)
            {
                if (seat.Value == "selected")
                {
                    seatStates[seat.Key] = "sold";
                    seat.Key.Background = YELLOW;
                    seat.Key.Foreground = Brushes.Black;

                    string zone = seat.Key.Tag.ToString();
                    total += GetPriceByZone(zone);
                    selectedSeats.Add(seat.Key.Content.ToString());
                }
            }

            if (total > 0)
            {
                TotalAmountTextBox.Text = total.ToString("N0") + " VNĐ";

                string message = $"Đã bán vé cho ghế: {string.Join(", ", selectedSeats)}\n" +
                                $"Tổng tiền: {total.ToString("N0")} VNĐ";
                MessageBox.Show(message, "Xác nhận bán vé",
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn ghế nào!", "Thông báo",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            bool hasSelection = false;

            foreach (var seat in seatStates)
            {
                if (seat.Value == "selected")
                {
                    hasSelection = true;
                    seatStates[seat.Key] = "available";
                    seat.Key.Background = WHITE;
                    seat.Key.Foreground = Brushes.Black;
                }
            }
            TotalAmountTextBox.Text = "0 VNĐ";

            if (hasSelection)
            {
                MessageBox.Show("Đã hủy bỏ các ghế đã chọn!", "Thông báo",
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Không có ghế nào đang được chọn!", "Thông báo",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có muốn kết thúc và reset toàn bộ không?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                InitializeSeats();
                TotalAmountTextBox.Text = "0 VNĐ";
                MessageBox.Show("Đã reset toàn bộ hệ thống!", "Thông báo",
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}