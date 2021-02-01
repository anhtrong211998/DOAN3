using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class ChiTietDonDatHang
    {
        public string MaDonDatHang { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
    }
}