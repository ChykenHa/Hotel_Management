using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class TrangChu : Window
    {
        private DispatcherTimer timer;
        private Phong_ViewModel phongVM;
        private KhachHang_ViewModel khachHangVM;
        private DatPhong_ViewModel datPhongVM;

        public TrangChu()
        {
            InitializeComponent();
            
            phongVM = new Phong_ViewModel();
            khachHangVM = new KhachHang_ViewModel();
            datPhongVM = new DatPhong_ViewModel();
            
            LoadStatistics();
            StartClock();
        }

        private void LoadStatistics()
        {
            try
            {
                txt_TongPhong.Text = phongVM.DemSoPhong().ToString();
                txt_TongKhachHang.Text = khachHangVM.DemSoKhachHang().ToString();
                txt_TongDatPhong.Text = datPhongVM.DemSoDatPhong().ToString();
                txt_PhongTrong.Text = phongVM.DemPhongTheoTrangThai("Trống").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            txt_DateTime.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }

        private void QuanLyPhong_Click(object sender, RoutedEventArgs e)
        {
            QuanLyPhong qlp = new QuanLyPhong();
            qlp.ShowDialog();
            LoadStatistics(); // Refresh statistics after closing
        }

        private void QuanLyKhachHang_Click(object sender, RoutedEventArgs e)
        {
            QuanLyKhachHang qlkh = new QuanLyKhachHang();
            qlkh.ShowDialog();
            LoadStatistics();
        }

        private void QuanLyDatPhong_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDatPhong qldp = new QuanLyDatPhong();
            qldp.ShowDialog();
            LoadStatistics();
        }

        private void QuanLyDichVu_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDichVu qldv = new QuanLyDichVu();
            qldv.ShowDialog();
        }

        private void QuanLyLoaiPhong_Click(object sender, RoutedEventArgs e)
        {
            QuanLyLoaiPhong qllp = new QuanLyLoaiPhong();
            qllp.ShowDialog();
            LoadStatistics();
        }

        private void DangXuat_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                DangNhap loginWindow = new DangNhap();
                loginWindow.Show();
                this.Close();
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            if (border != null)
            {
                border.Background = new SolidColorBrush(Colors.White);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            timer?.Stop();
        }
    }
}
