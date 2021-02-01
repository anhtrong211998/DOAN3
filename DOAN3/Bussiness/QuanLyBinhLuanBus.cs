using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;
namespace DOAN3.Bussiness
{
    public class QuanLyBinhLuanBus
    {
        public BinhLuanModel db = new BinhLuanModel();
        public List<BinhLuan> LayDsBinhLuan()
        {
            var ds = db.LayDSBinhLuan();
            return ds;
        }
        public BinhLuan LayBinhLuanTheoMa(string ma)
        {
            var ds = db.LayDSBinhLuanTheoMa(ma);
            return ds;
        }
        public Boolean Insert(BinhLuan nx)
        {
            return db.Insert(nx);
        }
        public Boolean InsertNULL(BinhLuan nx)
        {
            return db.InsertNULL(nx);
        }
        public Boolean Update(BinhLuan sx)
        {
            return db.Update(sx);
        }
        public Boolean UpdateNULL(BinhLuan sx)
        {
            return db.UpdateNULL(sx);
        }
        public Boolean Delete(string manv)
        {
            return db.Delete(manv);
        }
    }
}