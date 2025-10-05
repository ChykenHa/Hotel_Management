using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    public class KhachHang_ViewModel
    {
        private readonly HotelDbContext db;

        public KhachHang_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<KHACHHANG> GetAllKhachHang()
        {
            return db.KHACHHANGs.ToList();
        }

        public bool ThemKhachHang(KHACHHANG kh)
        {
            try
            {
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                db.Entry(kh).State = EntityState.Detached;
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool SuaKhachHang(KHACHHANG kh)
        {
            try
            {
                var existingKH = db.KHACHHANGs.Find(kh.ID_KH);
                if (existingKH != null)
                {
                    db.Entry(existingKH).CurrentValues.SetValues(kh);
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

        public bool XoaKhachHang(int idKH)
        {
            try
            {
                var kh = db.KHACHHANGs.Find(idKH);
                if (kh != null)
                {
                    db.KHACHHANGs.Remove(kh);
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

        public List<KHACHHANG> TimKiemKhachHang(string keyword)
        {
            return db.KHACHHANGs
                .Where(k => k.MaKH.Contains(keyword) || 
                            k.HoTen.Contains(keyword) ||
                            k.CMND.Contains(keyword) ||
                            k.SoDienThoai.Contains(keyword))
                .ToList();
        }

        public int DemSoKhachHang()
        {
            return db.KHACHHANGs.Count();
        }

        public KHACHHANG GetKhachHangById(int id)
        {
            return db.KHACHHANGs.Find(id);
        }
    }
}
