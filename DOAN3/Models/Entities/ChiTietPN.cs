using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class ChiTietPN
    {
        public string MaPhieuNhap { get; set; }
        public string MaSP { get; set; }
        public int SoLuongNhap { get; set; }
        public int DonGia { get; set; } 
        public  PhieuNhap PhieuNhap { get; set; }
    }
}