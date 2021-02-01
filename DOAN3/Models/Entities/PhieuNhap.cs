using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class PhieuNhap
    {
        public string MaPhieuNhap { get; set; }
        public string MaNCC { get; set; }
        public DateTime NgayNhap { get; set; }
        public int ThanhTien { get; set; }
        public virtual List<ChiTietPN> ChiTietPNs { get; set; }
    }
}