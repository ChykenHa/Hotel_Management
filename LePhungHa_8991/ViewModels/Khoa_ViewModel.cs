using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LePhungHa_8991.Models;

namespace LePhungHa_8991.ViewModels
{
    internal class Khoa_ViewModel
    {
        private readonly HotelDbContext db;

        public Khoa_ViewModel()
        {
            db = new HotelDbContext();
        }

        public List<KHOA> listKhoa()
        {
            // Tạo danh sách mẫu nếu không có trong database
            return new List<KHOA>
            {
                new KHOA { MaKhoa = "CNTT", TenKhoa = "Công nghệ thông tin" },
                new KHOA { MaKhoa = "QTDN", TenKhoa = "Quản trị doanh nghiệp" },
                new KHOA { MaKhoa = "KT", TenKhoa = "Kế toán" }
            };
        }
    }
}
