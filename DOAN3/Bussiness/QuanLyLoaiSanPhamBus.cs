using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLyLoaiSanPhamBus
    {
        public LoaiSanPhamModel db = new LoaiSanPhamModel();
        public List<LoaiSanPham> LayDsLoaiSanPham()
        {
            var ds = db.LayDSLoaiSanPham();
            return ds;
        }
        public LoaiSanPham LayLoaiSanPhamTheoMa(string ma)
        {
            var ds = db.LayLoaiSanPhamTheoMa(ma);
            return ds;
        }
        public Boolean Insert(LoaiSanPham l)
        {
            return db.Insert(l);
        }
        public Boolean Update(LoaiSanPham l)
        {
            return db.Update(l);
        }
        public Boolean Delete(string maloaisp)
        {
            return db.Delete(maloaisp);
        }
    }
}