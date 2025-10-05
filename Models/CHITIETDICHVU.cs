using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class CHITIETDICHVU
    {
        [Key]
        public int ID_ChiTiet { get; set; }
        public int ID_DatPhong { get; set; }
        public string MaDichVu { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime NgaySuDung { get; set; }
        
        [ForeignKey("ID_DatPhong")]
        public virtual DATPHONG DATPHONG { get; set; }
        
        [ForeignKey("MaDichVu")]
        public virtual DICHVU DICHVU { get; set; }
    }
}
