using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    public class DichVu_ViewModel
    {
        private readonly HotelDbContext db;

        public DichVu_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<DICHVU> GetAllDichVu()
        {
            return db.DICHVUs.ToList();
        }

        public bool ThemDichVu(DICHVU dv)
        {
            try
            {
                db.DICHVUs.Add(dv);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                db.Entry(dv).State = EntityState.Detached;
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool SuaDichVu(DICHVU dv)
        {
            try
            {
                var existingDV = db.DICHVUs.Find(dv.MaDichVu);
                if (existingDV != null)
                {
                    db.Entry(existingDV).CurrentValues.SetValues(dv);
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

        public bool XoaDichVu(string maDichVu)
        {
            try
            {
                var dv = db.DICHVUs.Find(maDichVu);
                if (dv != null)
                {
                    db.DICHVUs.Remove(dv);
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

        public List<DICHVU> TimKiemDichVu(string keyword)
        {
            return db.DICHVUs
                .Where(d => d.MaDichVu.Contains(keyword) || 
                            d.TenDichVu.Contains(keyword))
                .ToList();
        }

        public int DemSoDichVu()
        {
            return db.DICHVUs.Count();
        }

        public List<DICHVU> GetDichVuHoatDong()
        {
            return db.DICHVUs.Where(d => d.TrangThai == true).ToList();
        }
    }
}
