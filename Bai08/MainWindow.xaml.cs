using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BankAccountApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<BankAccount> accounts;

        public MainWindow()
        {
            InitializeComponent();
            accounts = new ObservableCollection<BankAccount>();
            AccountListView.ItemsSource = accounts;
            UpdateTotal();
        }
        public class BankAccount
        {
            public int STT { get; set; }
            public string AccountNumber { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public decimal Balance { get; set; }
            public string BalanceFormatted => Balance.ToString("N0");
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(AccountNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(CustomerNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(AddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(BalanceTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!decimal.TryParse(BalanceTextBox.Text, out decimal balance))
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (balance < 0)
            {
                MessageBox.Show("Số tiền không được âm!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void AddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            string accountNumber = AccountNumberTextBox.Text.Trim();
            string customerName = CustomerNameTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();
            decimal balance = decimal.Parse(BalanceTextBox.Text.Trim());

            var existingAccount = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (existingAccount != null)
            {
                existingAccount.CustomerName = customerName;
                existingAccount.Address = address;
                existingAccount.Balance = balance;

                AccountListView.Items.Refresh();
                UpdateTotal();

                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                var newAccount = new BankAccount
                {
                    STT = accounts.Count + 1,
                    AccountNumber = accountNumber,
                    CustomerName = customerName,
                    Address = address,
                    Balance = balance
                };

                accounts.Add(newAccount);
                UpdateTotal();

                MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ClearInputFields();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AccountNumberTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập số tài khoản cần xóa!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string accountNumber = AccountNumberTextBox.Text.Trim();
            var accountToDelete = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (accountToDelete == null)
            {
                MessageBox.Show("Không tìm thấy số tài khoản cần xóa!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản {accountNumber}?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                accounts.Remove(accountToDelete);

                UpdateSTT();
                UpdateTotal();

                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                ClearInputFields();
            }
        }

        private void UpdateSTT()
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                accounts[i].STT = i + 1;
            }
            AccountListView.Items.Refresh();
        }
        private void AccountListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountListView.SelectedItem is BankAccount selectedAccount)
            {
                AccountNumberTextBox.Text = selectedAccount.AccountNumber;
                CustomerNameTextBox.Text = selectedAccount.CustomerName;
                AddressTextBox.Text = selectedAccount.Address;
                BalanceTextBox.Text = selectedAccount.Balance.ToString();
            }
        }

        private void UpdateTotal()
        {
            decimal total = accounts.Sum(a => a.Balance);
            TotalAmountTextBox.Text = total.ToString("N0");
        }

        private void ClearInputFields()
        {
            AccountNumberTextBox.Clear();
            CustomerNameTextBox.Clear();
            AddressTextBox.Clear();
            BalanceTextBox.Clear();
            AccountListView.SelectedItem = null;
            AccountNumberTextBox.Focus();
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}