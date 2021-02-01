using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLyThanhVienBus
    {
        public ThanhVienModel db = new ThanhVienModel();
        public List<ThanhVien> LayDsThanhVien()
        {
            var ds = db.LayDSThanhVien();
            return ds;
        }
        public ThanhVien LayThanhVienTheoMa(string ma)
        {
            var ds = db.LayThanhVienTheoMa(ma);
            return ds;
        }
        public Boolean Insert(ThanhVien nx)
        {
            return db.Insert(nx);
        }
        public Boolean Update(ThanhVien sx)
        {
            return db.Update(sx);
        }
        public Boolean Delete(string manv)
        {
            return db.Delete(manv);
        }
    }
}