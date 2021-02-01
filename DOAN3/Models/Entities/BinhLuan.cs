using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN3.Models.Entities
{
    public class BinhLuan
    {
        public string MaBL { get; set; }
        public string NoiDungBL { get; set; }
        public string MaThanhVien { get; set; }
        public string MaSP { get; set; }
        public int DanhGia { get; set; }
    }
}