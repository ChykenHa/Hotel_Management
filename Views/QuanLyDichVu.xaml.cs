using System;
using System.Windows;
using System.Windows.Controls;
using LePhungHa_8991.Models;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class QuanLyDichVu : Window
    {
        private DichVu_ViewModel viewModel;
        private DICHVU selectedDichVu;

        public QuanLyDichVu()
        {
            InitializeComponent();
            viewModel = new DichVu_ViewModel();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgv_DichVu.ItemsSource = null;
                dgv_DichVu.ItemsSource = viewModel.GetAllDichVu();
                txt_TongSo.Text = viewModel.DemSoDichVu().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaDichVu.Text) || 
                string.IsNullOrWhiteSpace(txt_TenDichVu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DICHVU dv = new DICHVU
            {
                MaDichVu = txt_MaDichVu.Text.Trim(),
                TenDichVu = txt_TenDichVu.Text.Trim(),
                DonGia = decimal.TryParse(txt_DonGia.Text, out decimal donGia) ? donGia : 0,
                DonViTinh = txt_DonViTinh.Text.Trim(),
                MoTa = txt_MoTa.Text.Trim(),
                TrangThai = chk_TrangThai.IsChecked ?? true
            };

            if (viewModel.ThemDichVu(dv))
            {
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedDichVu.TenDichVu = txt_TenDichVu.Text.Trim();
            selectedDichVu.DonGia = decimal.TryParse(txt_DonGia.Text, out decimal donGia) ? donGia : 0;
            selectedDichVu.DonViTinh = txt_DonViTinh.Text.Trim();
            selectedDichVu.MoTa = txt_MoTa.Text.Trim();
            selectedDichVu.TrangThai = chk_TrangThai.IsChecked ?? true;

            if (viewModel.SuaDichVu(selectedDichVu))
            {
                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
                selectedDichVu = null;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa dịch vụ {selectedDichVu.TenDichVu}?", 
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.XoaDichVu(selectedDichVu.MaDichVu))
                {
                    MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearInputs();
                    selectedDichVu = null;
                }
            }
        }

        private void LamMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadData();
            selectedDichVu = null;
        }

        private void TimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_TimKiem.Text) && txt_TimKiem.Text != "Tìm kiếm dịch vụ...")
            {
                dgv_DichVu.ItemsSource = null;
                dgv_DichVu.ItemsSource = viewModel.TimKiemDichVu(txt_TimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void dgv_DichVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_DichVu.SelectedItem != null)
            {
                selectedDichVu = dgv_DichVu.SelectedItem as DICHVU;
                
                txt_MaDichVu.Text = selectedDichVu.MaDichVu;
                txt_MaDichVu.IsEnabled = false;
                txt_TenDichVu.Text = selectedDichVu.TenDichVu;
                txt_DonGia.Text = selectedDichVu.DonGia.ToString();
                txt_DonViTinh.Text = selectedDichVu.DonViTinh;
                txt_MoTa.Text = selectedDichVu.MoTa;
                chk_TrangThai.IsChecked = selectedDichVu.TrangThai;
            }
        }

        private void ClearInputs()
        {
            txt_MaDichVu.Text = "";
            txt_MaDichVu.IsEnabled = true;
            txt_TenDichVu.Text = "";
            txt_DonGia.Text = "";
            txt_DonViTinh.Text = "";
            txt_MoTa.Text = "";
            chk_TrangThai.IsChecked = true;
        }
    }
}
