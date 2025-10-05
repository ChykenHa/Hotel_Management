using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class LOAIPHONG
    {
        [Key]
        public string MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal GiaTheoGio { get; set; }
        public decimal GiaTheoNgay { get; set; }
        public int SoNguoiToiDa { get; set; }
        public string MoTa { get; set; }
        
        public virtual ICollection<PHONG> PHONGs { get; set; }
    }
}
