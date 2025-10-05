using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    public class LoaiPhong_ViewModel
    {
        private readonly HotelDbContext db;

        public LoaiPhong_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<LOAIPHONG> GetAllLoaiPhong()
        {
            return db.LOAIPHONGs.ToList();
        }

        public bool ThemLoaiPhong(LOAIPHONG lp)
        {
            try
            {
                db.LOAIPHONGs.Add(lp);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                db.Entry(lp).State = EntityState.Detached;
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        public bool SuaLoaiPhong(LOAIPHONG lp)
        {
            try
            {
                var existingLP = db.LOAIPHONGs.Find(lp.MaLoaiPhong);
                if (existingLP != null)
                {
                    db.Entry(existingLP).CurrentValues.SetValues(lp);
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

        public bool XoaLoaiPhong(string maLoaiPhong)
        {
            try
            {
                var lp = db.LOAIPHONGs.Find(maLoaiPhong);
                if (lp != null)
                {
                    db.LOAIPHONGs.Remove(lp);
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

        public List<LOAIPHONG> TimKiemLoaiPhong(string keyword)
        {
            return db.LOAIPHONGs
                .Where(l => l.MaLoaiPhong.Contains(keyword) || 
                            l.TenLoaiPhong.Contains(keyword))
                .ToList();
        }

        public int DemSoLoaiPhong()
        {
            return db.LOAIPHONGs.Count();
        }
    }
}
