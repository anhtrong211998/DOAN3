using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public int DonGia { get; set; }
        public int GiamGia { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string MaNCC { get; set; }
        public string MaNSX { get; set; }
        public string MaLoaiSP { get; set; }
    }
}