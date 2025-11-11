using System;
using System.Windows;
using System.Windows.Interop;

namespace Bai01
{
    public partial class MainWindow : Window
    {
        private int eventCounter = 0;
        private bool isMoving = false;
        private bool isResizing = false;
        private Point lastLocation;
        private Size lastSize;

        public MainWindow()
        {
            InitializeComponent();
            LogEvent("Constructor được gọi");

            this.SourceInitialized += MainWindow_SourceInitialized;
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            var handle = (new WindowInteropHelper(this)).Handle;
            var source = HwndSource.FromHwnd(handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_ENTERSIZEMOVE = 0x0231;
            const int WM_EXITSIZEMOVE = 0x0232;

            switch (msg)
            {
                case WM_ENTERSIZEMOVE:
                    // Bắt đầu kéo hoặc resize
                    lastLocation = new Point(this.Left, this.Top);
                    lastSize = new Size(this.Width, this.Height);
                    break;

                case WM_EXITSIZEMOVE:
                    // Khi người dùng thả chuột sau khi di chuyển hoặc resize
                    if (this.Left != lastLocation.X || this.Top != lastLocation.Y)
                        LogEvent($"Window_LocationChanged: Vị trí mới (Left: {this.Left:F0}, Top: {this.Top:F0})");

                    if (this.Width != lastSize.Width || this.Height != lastSize.Height)
                        LogEvent($"Window_SizeChanged: Kích thước mới (Width: {this.Width:F0}, Height: {this.Height:F0})");
                    break;
            }

            return IntPtr.Zero;
        }

        private void LogEvent(string eventName)
        {
            eventCounter++;
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string logMessage = $"[{eventCounter}] {timestamp} - {eventName}\n";
            txtLog.AppendText(logMessage);
            txtLog.ScrollToEnd();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogEvent("Window_Loaded: Window đã được tải hoàn tất và hiển thị");
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LogEvent("Window_Activated: Window được kích hoạt (nhận focus)");
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            LogEvent("Window_Deactivated: Window mất focus");
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            string state = this.WindowState.ToString();
            LogEvent($"Window_StateChanged: Trạng thái thay đổi thành {state}");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogEvent("Window_Closing: Window đang chuẩn bị đóng");
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc muốn đóng ứng dụng không?",
                "Xác nhận đóng",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
                LogEvent("Window_Closing: Người dùng đã hủy việc đóng window");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LogEvent("Window_Closed: Window đã đóng hoàn toàn");
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            LogEvent("Button_Click: Người dùng nhấn nút Minimize");
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            LogEvent("Button_Click: Người dùng nhấn nút Maximize");
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            LogEvent("Button_Click: Người dùng nhấn nút Restore - Quay về trạng thái ban đầu");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            txtLog.Clear();
            eventCounter = 0;
            LogEvent("Log đã được xóa");
        }
    }
}
