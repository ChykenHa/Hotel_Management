using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LePhungHa_8991.Models;
using LePhungHa_8991.View;

namespace LePhungHa_8991
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        private readonly HotelDbContext db;

        public DangNhap()
        {
            InitializeComponent();
            db = new HotelDbContext();
            
            // Tạo tài khoản mặc định nếu chưa có
            TaoTaiKhoanMacDinh();
        }

        private void TaoTaiKhoanMacDinh()
        {
            try
            {
                if (!db.TAIKHOANs.Any())
                {
                    db.TAIKHOANs.Add(new TAIKHOAN { TenDangNhap = "admin", MatKhau = "123456" });
                    db.SaveChanges();
                }
            }
            catch { }
        }

        private void DangNhap_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TenDN.Text) || string.IsNullOrWhiteSpace(txt_MatKhau.Password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TAIKHOAN taiKhoan = db.TAIKHOANs.FirstOrDefault
                (tk => tk.TenDangNhap == txt_TenDN.Text && tk.MatKhau == txt_MatKhau.Password);
            
            if (taiKhoan != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                TrangChu trangChu = new TrangChu();
                trangChu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
