using System;
using System.Windows;
using System.Windows.Controls;
using LePhungHa_8991.Models;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class QuanLyKhachHang : Window
    {
        private KhachHang_ViewModel viewModel;
        private KHACHHANG selectedKhachHang;

        public QuanLyKhachHang()
        {
            InitializeComponent();
            viewModel = new KhachHang_ViewModel();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgv_KhachHang.ItemsSource = null;
                dgv_KhachHang.ItemsSource = viewModel.GetAllKhachHang();
                txt_TongSo.Text = viewModel.DemSoKhachHang().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaKH.Text) || 
                string.IsNullOrWhiteSpace(txt_HoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            KHACHHANG kh = new KHACHHANG
            {
                MaKH = txt_MaKH.Text.Trim(),
                HoTen = txt_HoTen.Text.Trim(),
                CMND = txt_CMND.Text.Trim(),
                SoDienThoai = txt_SoDienThoai.Text.Trim(),
                Email = txt_Email.Text.Trim(),
                DiaChi = txt_DiaChi.Text.Trim(),
                NgaySinh = dp_NgaySinh.SelectedDate,
                GioiTinh = (cbo_GioiTinh.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            if (viewModel.ThemKhachHang(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            if (selectedKhachHang == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedKhachHang.MaKH = txt_MaKH.Text.Trim();
            selectedKhachHang.HoTen = txt_HoTen.Text.Trim();
            selectedKhachHang.CMND = txt_CMND.Text.Trim();
            selectedKhachHang.SoDienThoai = txt_SoDienThoai.Text.Trim();
            selectedKhachHang.Email = txt_Email.Text.Trim();
            selectedKhachHang.DiaChi = txt_DiaChi.Text.Trim();
            selectedKhachHang.NgaySinh = dp_NgaySinh.SelectedDate;
            selectedKhachHang.GioiTinh = (cbo_GioiTinh.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (viewModel.SuaKhachHang(selectedKhachHang))
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
                selectedKhachHang = null;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (selectedKhachHang == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng {selectedKhachHang.HoTen}?", 
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.XoaKhachHang(selectedKhachHang.ID_KH))
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearInputs();
                    selectedKhachHang = null;
                }
            }
        }

        private void LamMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadData();
            selectedKhachHang = null;
        }

        private void TimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_TimKiem.Text))
            {
                dgv_KhachHang.ItemsSource = null;
                dgv_KhachHang.ItemsSource = viewModel.TimKiemKhachHang(txt_TimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void dgv_KhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_KhachHang.SelectedItem != null)
            {
                selectedKhachHang = dgv_KhachHang.SelectedItem as KHACHHANG;
                
                txt_MaKH.Text = selectedKhachHang.MaKH;
                txt_HoTen.Text = selectedKhachHang.HoTen;
                txt_CMND.Text = selectedKhachHang.CMND;
                txt_SoDienThoai.Text = selectedKhachHang.SoDienThoai;
                txt_Email.Text = selectedKhachHang.Email;
                txt_DiaChi.Text = selectedKhachHang.DiaChi;
                dp_NgaySinh.SelectedDate = selectedKhachHang.NgaySinh;
                
                if (!string.IsNullOrEmpty(selectedKhachHang.GioiTinh))
                {
                    foreach (ComboBoxItem item in cbo_GioiTinh.Items)
                    {
                        if (item.Content.ToString() == selectedKhachHang.GioiTinh)
                        {
                            cbo_GioiTinh.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void dp_NgaySinh_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // DatePicker event - you can add logic here if needed
            if (dp_NgaySinh.SelectedDate.HasValue)
            {
                var age = DateTime.Now.Year - dp_NgaySinh.SelectedDate.Value.Year;
                // You could display age or validate date here
            }
        }

        private void ClearInputs()
        {
            txt_MaKH.Text = "";
            txt_HoTen.Text = "";
            txt_CMND.Text = "";
            txt_SoDienThoai.Text = "";
            txt_Email.Text = "";
            txt_DiaChi.Text = "";
            dp_NgaySinh.SelectedDate = null;
            cbo_GioiTinh.SelectedIndex = -1;
        }
    }
}
