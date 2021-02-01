using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLyKhachHangBus
    {
        public KhachHangModel db = new KhachHangModel();
        public List<KhachHang> LayDsKhachHang()
        {
            var ds = db.LayDSKhachHang();
            return ds;
        }
        public KhachHang LayKhachHangTheoMa(string ma)
        {
            var ds = db.LayKhachHangtheoma(ma);
            return ds;
        }
        public Boolean Insert(KhachHang nx)
        {
            return db.Insert(nx);
        }
        public Boolean Update(KhachHang sx)
        {
            return db.Update(sx);
        }
        public Boolean Delete(string manv)
        {
            return db.Delete(manv);
        }
    }
}