using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LePhungHa_8991.Models;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class QuanLyPhong : Window
    {
        private Phong_ViewModel viewModel;
        private LoaiPhong_ViewModel loaiPhongVM;
        private PHONG selectedPhong;

        public QuanLyPhong()
        {
            InitializeComponent();
            viewModel = new Phong_ViewModel();
            loaiPhongVM = new LoaiPhong_ViewModel();
            
            LoadData();
            LoadLoaiPhong();
        }

        private void LoadData()
        {
            try
            {
                dgv_Phong.ItemsSource = null;
                dgv_Phong.ItemsSource = viewModel.GetAllPhong();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLoaiPhong()
        {
            try
            {
                var loaiPhongs = loaiPhongVM.GetAllLoaiPhong();
                cbo_LoaiPhong.ItemsSource = loaiPhongs;
                
                // Debug: Kiểm tra số lượng
                if (loaiPhongs == null || loaiPhongs.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu loại phòng trong database!\nVui lòng chạy DatabaseScript.sql để tạo dữ liệu.", 
                        "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải loại phòng:\n{ex.Message}\n\nInner: {ex.InnerException?.Message}", 
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateStatistics()
        {
            txt_TongSo.Text = viewModel.DemSoPhong().ToString();
            txt_PhongTrong.Text = viewModel.DemPhongTheoTrangThai("Trống").ToString();
            txt_DaDat.Text = viewModel.DemPhongTheoTrangThai("Đã đặt").ToString();
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaPhong.Text) || 
                string.IsNullOrWhiteSpace(txt_TenPhong.Text) ||
                cbo_LoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PHONG phong = new PHONG
            {
                MaPhong = txt_MaPhong.Text.Trim(),
                TenPhong = txt_TenPhong.Text.Trim(),
                MaLoaiPhong = cbo_LoaiPhong.SelectedValue.ToString(),
                Tang = txt_Tang.Text.Trim(),
                TrangThai = (cbo_TrangThai.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Trống",
                GhiChu = txt_GhiChu.Text.Trim()
            };

            if (viewModel.ThemPhong(phong))
            {
                MessageBox.Show("Thêm phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPhong == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_TenPhong.Text) ||
                cbo_LoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedPhong.TenPhong = txt_TenPhong.Text.Trim();
            selectedPhong.MaLoaiPhong = cbo_LoaiPhong.SelectedValue.ToString();
            selectedPhong.Tang = txt_Tang.Text.Trim();
            selectedPhong.TrangThai = (cbo_TrangThai.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Trống";
            selectedPhong.GhiChu = txt_GhiChu.Text.Trim();

            if (viewModel.SuaPhong(selectedPhong))
            {
                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
                selectedPhong = null;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPhong == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa phòng {selectedPhong.TenPhong}?", 
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.XoaPhong(selectedPhong.MaPhong))
                {
                    MessageBox.Show("Xóa phòng thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearInputs();
                    selectedPhong = null;
                }
            }
        }

        private void LamMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadData();
            selectedPhong = null;
        }

        private void TimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_TimKiem.Text) && 
                txt_TimKiem.Text != "Nhập từ khóa tìm kiếm...")
            {
                dgv_Phong.ItemsSource = null;
                dgv_Phong.ItemsSource = viewModel.TimKiemPhong(txt_TimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void dgv_Phong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_Phong.SelectedItem != null)
            {
                selectedPhong = dgv_Phong.SelectedItem as PHONG;
                
                txt_MaPhong.Text = selectedPhong.MaPhong;
                txt_MaPhong.IsEnabled = false;
                txt_TenPhong.Text = selectedPhong.TenPhong;
                cbo_LoaiPhong.SelectedValue = selectedPhong.MaLoaiPhong;
                txt_Tang.Text = selectedPhong.Tang;
                txt_GhiChu.Text = selectedPhong.GhiChu;
                
                // Set trạng thái
                foreach (ComboBoxItem item in cbo_TrangThai.Items)
                {
                    if (item.Content.ToString() == selectedPhong.TrangThai)
                    {
                        cbo_TrangThai.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void cbo_LoaiPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Event for ComboBox selection changed
            if (cbo_LoaiPhong.SelectedItem != null)
            {
                var loaiPhong = cbo_LoaiPhong.SelectedItem as LOAIPHONG;
                // You can add additional logic here if needed
            }
        }

        private void txt_TimKiem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_TimKiem.Text == "Nhập từ khóa tìm kiếm...")
            {
                txt_TimKiem.Text = "";
            }
        }

        private void txt_TimKiem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TimKiem.Text))
            {
                txt_TimKiem.Text = "Nhập từ khóa tìm kiếm...";
            }
        }

        private void ClearInputs()
        {
            txt_MaPhong.Text = "";
            txt_MaPhong.IsEnabled = true;
            txt_TenPhong.Text = "";
            txt_Tang.Text = "";
            txt_GhiChu.Text = "";
            cbo_LoaiPhong.SelectedIndex = -1;
            cbo_TrangThai.SelectedIndex = 0;
        }
    }
}
