using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StudentEntryApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public MainWindow()
        {
            InitializeComponent();
            dgSinhVien.ItemsSource = Students;
            cmbChuyenNganh.SelectedIndex = 0; 
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sinh Viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ Tên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cmbChuyenNganh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Chuyên Ngành.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!rbNam.IsChecked.Value && !rbNu.IsChecked.Value)
            {
                MessageBox.Show("Vui lòng chọn Giới Tính.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string mssv = txtMSSV.Text;
            string hoTen = txtHoTen.Text;
            string chuyenNganh = ((ComboBoxItem)cmbChuyenNganh.SelectedItem).Content.ToString();
            string gioiTinh = rbNam.IsChecked.Value ? "Nam" : "Nữ";
            int soMon = lstMonHoc.SelectedItems.Count;
            foreach (var student in Students)
            {
                if (student.MSSV == mssv)
                {
                    MessageBox.Show("Mã Sinh Viên đã tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Students.Add(new Student
            {
                MSSV = mssv,
                HoTen = hoTen,
                ChuyenNganh = chuyenNganh,
                GioiTinh = gioiTinh,
                SoMon = soMon
            });

            ClearForm();
        }

        private void btnXoaChon_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMSSV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            cmbChuyenNganh.SelectedIndex = 0;
            rbNam.IsChecked = false;
            rbNu.IsChecked = false;
            lstMonHoc.SelectedItems.Clear();
        }
    }

    public class Student
    {
        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public string ChuyenNganh { get; set; }
        public string GioiTinh { get; set; }
        public int SoMon { get; set; }
    }
}