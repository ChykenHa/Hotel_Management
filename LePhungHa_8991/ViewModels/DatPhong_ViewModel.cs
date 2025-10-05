using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    public class DatPhong_ViewModel
    {
        private readonly HotelDbContext db;

        public DatPhong_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<DATPHONG> GetAllDatPhong()
        {
            return db.DATPHONGs
                .Include(d => d.KHACHHANG)
                .Include(d => d.PHONG)
                .ToList();
        }

        public List<KHACHHANG> GetAllKhachHang()
        {
            return db.KHACHHANGs.ToList();
        }

        public List<PHONG> GetPhongTrong()
        {
            return db.PHONGs.Where(p => p.TrangThai == "Trống").ToList();
        }

        public bool ThemDatPhong(DATPHONG dp)
        {
            try
            {
                db.DATPHONGs.Add(dp);
                
                // Cập nhật trạng thái phòng
                var phong = db.PHONGs.Find(dp.MaPhong);
                if (phong != null)
                {
                    phong.TrangThai = "Đã đặt";
                }
                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                db.Entry(dp).State = EntityState.Detached;
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool SuaDatPhong(DATPHONG dp)
        {
            try
            {
                var existingDP = db.DATPHONGs.Find(dp.ID_DatPhong);
                if (existingDP != null)
                {
                    db.Entry(existingDP).CurrentValues.SetValues(dp);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool XoaDatPhong(int idDatPhong)
        {
            try
            {
                var dp = db.DATPHONGs.Find(idDatPhong);
                if (dp != null)
                {
                    // Cập nhật lại trạng thái phòng
                    var phong = db.PHONGs.Find(dp.MaPhong);
                    if (phong != null)
                    {
                        phong.TrangThai = "Trống";
                    }
                    
                    db.DATPHONGs.Remove(dp);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public List<DATPHONG> TimKiemDatPhong(string keyword)
        {
            return db.DATPHONGs
                .Include(d => d.KHACHHANG)
                .Include(d => d.PHONG)
                .Where(d => d.MaDatPhong.Contains(keyword) || 
                            d.KHACHHANG.HoTen.Contains(keyword) ||
                            d.PHONG.TenPhong.Contains(keyword))
                .ToList();
        }

        public int DemSoDatPhong()
        {
            return db.DATPHONGs.Count();
        }

        public List<DATPHONG> LocTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return db.DATPHONGs
                .Include(d => d.KHACHHANG)
                .Include(d => d.PHONG)
                .Where(d => d.NgayNhanPhong >= tuNgay && d.NgayNhanPhong <= denNgay)
                .ToList();
        }
    }
}
