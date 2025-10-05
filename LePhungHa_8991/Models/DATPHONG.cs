using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class DATPHONG
    {
        [Key]
        public int ID_DatPhong { get; set; }
        public string MaDatPhong { get; set; }
        public int ID_KH { get; set; }
        public string MaPhong { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
        public int SoNguoi { get; set; }
        public decimal TienCoc { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } // "Đã đặt", "Đã nhận phòng", "Đã trả phòng", "Đã hủy"
        public string GhiChu { get; set; }
        
        [ForeignKey("ID_KH")]
        public virtual KHACHHANG KHACHHANG { get; set; }
        
        [ForeignKey("MaPhong")]
        public virtual PHONG PHONG { get; set; }
        
        public virtual ICollection<CHITIETDICHVU> CHITIETDICHVUs { get; set; }
    }
}
