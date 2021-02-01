using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class DonDatHang
    {
        public string MaDonDatHang { get; set; }
        public DateTime NgayDat { get; set; }
        public Boolean TinhTrangGiaoHang { get; set; }
        public DateTime NgayGiao { get; set; }
        public Boolean DaThanhToan { get; set; }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public string UuDai { get; set; }
        public int ThanhTien { get; set; }
    }
}