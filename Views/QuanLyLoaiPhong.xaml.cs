using System;
using System.Windows;
using System.Windows.Controls;
using LePhungHa_8991.Models;
using LePhungHa_8991.ViewModels;

namespace LePhungHa_8991.View
{
    public partial class QuanLyLoaiPhong : Window
    {
        private LoaiPhong_ViewModel viewModel;
        private LOAIPHONG selectedLoaiPhong;

        public QuanLyLoaiPhong()
        {
            InitializeComponent();
            viewModel = new LoaiPhong_ViewModel();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgv_LoaiPhong.ItemsSource = null;
                dgv_LoaiPhong.ItemsSource = viewModel.GetAllLoaiPhong();
                txt_TongSo.Text = viewModel.DemSoLoaiPhong().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_MaLoaiPhong.Text) || 
                string.IsNullOrWhiteSpace(txt_TenLoaiPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LOAIPHONG lp = new LOAIPHONG
            {
                MaLoaiPhong = txt_MaLoaiPhong.Text.Trim(),
                TenLoaiPhong = txt_TenLoaiPhong.Text.Trim(),
                GiaTheoGio = decimal.TryParse(txt_GiaTheoGio.Text, out decimal giaGio) ? giaGio : 0,
                GiaTheoNgay = decimal.TryParse(txt_GiaTheoNgay.Text, out decimal giaNgay) ? giaNgay : 0,
                SoNguoiToiDa = int.TryParse(txt_SoNguoiToiDa.Text, out int soNguoi) ? soNguoi : 2,
                MoTa = txt_MoTa.Text.Trim()
            };

            if (viewModel.ThemLoaiPhong(lp))
            {
                MessageBox.Show("Thêm loại phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLoaiPhong == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần sửa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedLoaiPhong.TenLoaiPhong = txt_TenLoaiPhong.Text.Trim();
            selectedLoaiPhong.GiaTheoGio = decimal.TryParse(txt_GiaTheoGio.Text, out decimal giaGio) ? giaGio : 0;
            selectedLoaiPhong.GiaTheoNgay = decimal.TryParse(txt_GiaTheoNgay.Text, out decimal giaNgay) ? giaNgay : 0;
            selectedLoaiPhong.SoNguoiToiDa = int.TryParse(txt_SoNguoiToiDa.Text, out int soNguoi) ? soNguoi : 2;
            selectedLoaiPhong.MoTa = txt_MoTa.Text.Trim();

            if (viewModel.SuaLoaiPhong(selectedLoaiPhong))
            {
                MessageBox.Show("Cập nhật loại phòng thành công!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                ClearInputs();
                selectedLoaiPhong = null;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLoaiPhong == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng cần xóa!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa loại phòng {selectedLoaiPhong.TenLoaiPhong}?", 
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (viewModel.XoaLoaiPhong(selectedLoaiPhong.MaLoaiPhong))
                {
                    MessageBox.Show("Xóa loại phòng thành công!", "Thông báo", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearInputs();
                    selectedLoaiPhong = null;
                }
            }
        }

        private void LamMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            LoadData();
            selectedLoaiPhong = null;
        }

        private void TimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_TimKiem.Text) && txt_TimKiem.Text != "Tìm kiếm loại phòng...")
            {
                dgv_LoaiPhong.ItemsSource = null;
                dgv_LoaiPhong.ItemsSource = viewModel.TimKiemLoaiPhong(txt_TimKiem.Text);
            }
            else
            {
                LoadData();
            }
        }

        private void dgv_LoaiPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgv_LoaiPhong.SelectedItem != null)
            {
                selectedLoaiPhong = dgv_LoaiPhong.SelectedItem as LOAIPHONG;
                
                txt_MaLoaiPhong.Text = selectedLoaiPhong.MaLoaiPhong;
                txt_MaLoaiPhong.IsEnabled = false;
                txt_TenLoaiPhong.Text = selectedLoaiPhong.TenLoaiPhong;
                txt_GiaTheoGio.Text = selectedLoaiPhong.GiaTheoGio.ToString();
                txt_GiaTheoNgay.Text = selectedLoaiPhong.GiaTheoNgay.ToString();
                txt_SoNguoiToiDa.Text = selectedLoaiPhong.SoNguoiToiDa.ToString();
                txt_MoTa.Text = selectedLoaiPhong.MoTa;
            }
        }

        private void ClearInputs()
        {
            txt_MaLoaiPhong.Text = "";
            txt_MaLoaiPhong.IsEnabled = true;
            txt_TenLoaiPhong.Text = "";
            txt_GiaTheoGio.Text = "";
            txt_GiaTheoNgay.Text = "";
            txt_SoNguoiToiDa.Text = "";
            txt_MoTa.Text = "";
        }
    }
}
