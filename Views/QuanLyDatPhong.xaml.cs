using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LePhungHa_8991.Models;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class QuanLyDatPhong : Window
    {
        private DatPhong_ViewModel viewModel;
        private DATPHONG selectedDatPhong;

        public QuanLyDatPhong()
        {
            InitializeComponent();
            viewModel = new DatPhong_ViewModel();
            LoadData();
            LoadComboBoxes();
        }

        private void LoadData()
        {
            try
            {
                dgv_DatPhong.ItemsSource = null;
                dgv_DatPhong.ItemsSource = viewModel.GetAllDatPhong();
                txt_TongSo.Text = viewModel.DemSoDatPhong().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                cbo_KhachHang.ItemsSource = viewModel.GetAllKhachHang();
                cbo_Phong.ItemsSource = viewModel.GetPhongTrong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaDatPhong.Text) || 
                cbo_KhachHang.SelectedValue == null ||
                cbo_Phong.SelectedValue == null ||
                dp_NgayNhan.SelectedDate == null ||
                dp_NgayTra.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dp_NgayTra.SelectedDate <= dp_NgayNhan.SelectedDate)
            {
                MessageBox.Show("Ngày trả phòng phải sau ngày nhận phòng!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DATPHONG dp = new DATPHONG
            {
                MaDatPhong = txt_MaDatPhong.Text.Trim(),
                ID_KH = (int)cbo_KhachHang.SelectedValue,
                MaPhong = cbo_Phong.SelectedValue.ToString(),
                NgayNhanPhong = dp_NgayNhan.SelectedDate.Value,
                NgayTraPhong = dp_NgayTra.SelectedDate.Value,
                SoNguoi = int.TryParse(txt_SoNguoi.Text, out int soNguoi) ? soNguoi : 1,
                TienCoc = decimal.TryParse(txt_TienCoc.Text, out decimal tienCoc) ? tienCoc : 0,
                TongTien = decimal.TryParse(txt_TongTien.Text, out decimal tongTien) ? tongTien : 0,
                TrangThai = (cbo_TrangThai.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Đã đặt",
                GhiChu = txt_GhiChu.Text.Trim()
            };

            if (viewModel.ThemDatPhong(dp))
            {
                MessageBox.Show("Thêm đặt phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                LoadComboBoxes();
                ClearInputs();
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDatPhong == null)
            {
                MessageBox.Show("Vui lòng chọn đặt phòng cần sửa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedDatPhong.NgayNhanPhong = dp_NgayNhan.SelectedDate.Value;
            selectedDatPhong.NgayTraPhong = dp_NgayTra.SelectedDate.Value;
            selectedDatPhong.SoNguoi = int.TryParse(txt_SoNguoi.Text, out int soNguoi) ? soNguoi : 1;
            selectedDatPhong.TienCoc = decimal.TryParse(txt_TienCoc.Text, out decimal tienCoc) ? tienCoc : 0;
            selectedDatPhong.TongTien = decimal.TryParse(txt_TongTien.Text, out decimal tongTien) ? tongTien : 0;
            selectedDatPhong.TrangThai = (cbo_TrangThai.SelectedItem as ComboBoxItem)?.Content.ToString();
            selectedDatPhong.GhiChu = txt_GhiChu.Text.Trim();

            if (viewModel.SuaDatPhong(selectedDatPhong))
            {
                MessageBox.Show("Cập nhật đặt phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
                selectedDatPhong = null;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDatPhong == null)
            {
                MessageBox.Show("Vui lòng chọn đặt phòng cần xóa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa đặt phòng {selectedDatPhong.MaDatPhong}?", 
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.XoaDatPhong(selectedDatPhong.ID_DatPhong))
                {
                    MessageBox.Show("Xóa đặt phòng thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    LoadComboBoxes();
                    ClearInputs();
                    selectedDatPhong = null;
                }
            }
        }

        private void LamMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadData();
            LoadComboBoxes();
            selectedDatPhong = null;
        }

        private void TimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_TimKiem.Text) && txt_TimKiem.Text != "Tìm kiếm...")
            {
                dgv_DatPhong.ItemsSource = null;
                dgv_DatPhong.ItemsSource = viewModel.TimKiemDatPhong(txt_TimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void Loc_Click(object sender, RoutedEventArgs e)
        {
            if (dp_TuNgay.SelectedDate != null && dp_DenNgay.SelectedDate != null)
            {
                dgv_DatPhong.ItemsSource = null;
                dgv_DatPhong.ItemsSource = viewModel.LocTheoNgay(
                    dp_TuNgay.SelectedDate.Value, 
                    dp_DenNgay.SelectedDate.Value);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khoảng ngày!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TinhTien_Click(object sender, RoutedEventArgs e)
        {
            if (dp_NgayNhan.SelectedDate != null && dp_NgayTra.SelectedDate != null)
            {
                TimeSpan soNgay = dp_NgayTra.SelectedDate.Value - dp_NgayNhan.SelectedDate.Value;
                // Giá mặc định 500,000 VND/ngày (bạn có thể lấy từ loại phòng)
                decimal giaMoiNgay = 500000;
                decimal tongTien = (decimal)soNgay.TotalDays * giaMoiNgay;
                txt_TongTien.Text = tongTien.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày nhận và trả phòng!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgv_DatPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_DatPhong.SelectedItem != null)
            {
                selectedDatPhong = dgv_DatPhong.SelectedItem as DATPHONG;
                
                txt_MaDatPhong.Text = selectedDatPhong.MaDatPhong;
                cbo_KhachHang.SelectedValue = selectedDatPhong.ID_KH;
                cbo_Phong.SelectedValue = selectedDatPhong.MaPhong;
                dp_NgayNhan.SelectedDate = selectedDatPhong.NgayNhanPhong;
                dp_NgayTra.SelectedDate = selectedDatPhong.NgayTraPhong;
                txt_SoNguoi.Text = selectedDatPhong.SoNguoi.ToString();
                txt_TienCoc.Text = selectedDatPhong.TienCoc.ToString();
                txt_TongTien.Text = selectedDatPhong.TongTien.ToString();
                txt_GhiChu.Text = selectedDatPhong.GhiChu;
                
                foreach (ComboBoxItem item in cbo_TrangThai.Items)
                {
                    if (item.Content.ToString() == selectedDatPhong.TrangThai)
                    {
                        cbo_TrangThai.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void cbo_Phong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event for ComboBox selection changed
        }

        private void dp_NgayNhan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // DatePicker event - auto calculate if both dates are selected
            if (dp_NgayNhan.SelectedDate != null && dp_NgayTra.SelectedDate != null)
            {
                if (dp_NgayTra.SelectedDate <= dp_NgayNhan.SelectedDate)
                {
                    MessageBox.Show("Ngày trả phòng phải sau ngày nhận phòng!", "Cảnh báo", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void dp_NgayTra_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // DatePicker event
            if (dp_NgayNhan.SelectedDate != null && dp_NgayTra.SelectedDate != null)
            {
                if (dp_NgayTra.SelectedDate <= dp_NgayNhan.SelectedDate)
                {
                    MessageBox.Show("Ngày trả phòng phải sau ngày nhận phòng!", "Cảnh báo", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void ClearInputs()
        {
            txt_MaDatPhong.Text = "";
            cbo_KhachHang.SelectedIndex = -1;
            cbo_Phong.SelectedIndex = -1;
            dp_NgayNhan.SelectedDate = null;
            dp_NgayTra.SelectedDate = null;
            txt_SoNguoi.Text = "1";
            txt_TienCoc.Text = "0";
            txt_TongTien.Text = "0";
            txt_GhiChu.Text = "";
            cbo_TrangThai.SelectedIndex = 0;
        }
    }
}
