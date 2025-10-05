using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class DICHVU
    {
        [Key]
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; } // true: Còn cung cấp, false: Ngừng cung cấp
        
        public virtual ICollection<CHITIETDICHVU> CHITIETDICHVUs { get; set; }
    }
}
