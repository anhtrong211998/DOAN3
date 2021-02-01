using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;


namespace DOAN3.Bussiness
{
    public class QuanLyNhanVienBus
    {
        public NhanVienModel db = new NhanVienModel();
        public List<NhanVien> LayDsNhanvien()
        {
            var ds = db.LayDSNhanVien();
            return ds;
        }
        public NhanVien LayNhanVien(string taikhoan)
        {
            var nv = db.LayNhanVien(taikhoan);
            return nv;
        }
        public NhanVien LayNhanVienTheoMa(string ma)
        {
            var ds = db.LayNhanVienTheoma(ma);
            return ds;
        }
        public Boolean Insert(NhanVien nx)
        {
            return db.Insert(nx);
        }
        public Boolean InsertNULL(NhanVien nx)
        {
            return db.InsertNULL(nx);
        }
        public Boolean Update(NhanVien sx)
        {
            return db.Update(sx);
        }
        public Boolean UpdateNULL(NhanVien sx)
        {
            return db.UpdateNULL(sx);
        }
        public Boolean Delete(string manv)
        {
            return db.Delete(manv);
        }
    }
}