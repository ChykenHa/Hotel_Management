using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class PHONG
    {
        [Key]
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string MaLoaiPhong { get; set; }
        public string Tang { get; set; }
        public string TrangThai { get; set; } // "Trống", "Đã đặt", "Đang sử dụng", "Bảo trì"
        public string GhiChu { get; set; }
        
        [ForeignKey("MaLoaiPhong")]
        public virtual LOAIPHONG LOAIPHONG { get; set; }
        public virtual ICollection<DATPHONG> DATPHONGs { get; set; }
    }
}
