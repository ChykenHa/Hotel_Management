using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LePhungHa_8991.Models
{
    public class KHACHHANG
    {
        [Key]
        public int ID_KH { get; set; }
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        
        public virtual ICollection<DATPHONG> DATPHONGs { get; set; }
    }
}
