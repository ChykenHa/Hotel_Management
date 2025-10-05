using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext() : base("name=HotelConnectionString")
        {
        }

        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<LOAIPHONG> LOAIPHONGs { get; set; }
        public virtual DbSet<PHONG> PHONGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<DATPHONG> DATPHONGs { get; set; }
        public virtual DbSet<DICHVU> DICHVUs { get; set; }
        public virtual DbSet<CHITIETDICHVU> CHITIETDICHVUs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure relationships if needed
            modelBuilder.Entity<PHONG>()
                .HasMany(p => p.DATPHONGs)
                .WithRequired(d => d.PHONG)
                .HasForeignKey(d => d.MaPhong);
                
            modelBuilder.Entity<KHACHHANG>()
                .HasMany(k => k.DATPHONGs)
                .WithRequired(d => d.KHACHHANG)
                .HasForeignKey(d => d.ID_KH);
                
            modelBuilder.Entity<DATPHONG>()
                .HasMany(d => d.CHITIETDICHVUs)
                .WithRequired(c => c.DATPHONG)
                .HasForeignKey(c => c.ID_DatPhong);
                
            modelBuilder.Entity<DICHVU>()
                .HasMany(dv => dv.CHITIETDICHVUs)
                .WithRequired(c => c.DICHVU)
                .HasForeignKey(c => c.MaDichVu);
        }
    }
}
