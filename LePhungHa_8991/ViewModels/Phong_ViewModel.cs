using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    public class Phong_ViewModel
    {
        private readonly HotelDbContext db;

        public Phong_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<PHONG> GetAllPhong()
        {
            return db.PHONGs.Include(p => p.LOAIPHONG).ToList();
        }

        public List<LOAIPHONG> GetAllLoaiPhong()
        {
            return db.LOAIPHONGs.ToList();
        }

        public bool ThemPhong(PHONG phong)
        {
            try
            {
                db.PHONGs.Add(phong);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                db.Entry(phong).State = EntityState.Detached;
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool SuaPhong(PHONG phong)
        {
            try
            {
                var existingPhong = db.PHONGs.Find(phong.MaPhong);
                if (existingPhong != null)
                {
                    db.Entry(existingPhong).CurrentValues.SetValues(phong);
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

        public bool XoaPhong(string maPhong)
        {
            try
            {
                var phong = db.PHONGs.Find(maPhong);
                if (phong != null)
                {
                    db.PHONGs.Remove(phong);
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

        public List<PHONG> TimKiemPhong(string keyword)
        {
            return db.PHONGs.Include(p => p.LOAIPHONG)
                .Where(p => p.MaPhong.Contains(keyword) || 
                            p.TenPhong.Contains(keyword) ||
                            p.Tang.Contains(keyword) ||
                            p.TrangThai.Contains(keyword))
                .ToList();
        }

        public int DemSoPhong()
        {
            return db.PHONGs.Count();
        }

        public int DemPhongTheoTrangThai(string trangThai)
        {
            return db.PHONGs.Count(p => p.TrangThai == trangThai);
        }
    }
}
