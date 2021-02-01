using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLyNhaCungCapBus
    {
        public NhaCungCapModel db = new NhaCungCapModel();
        public List<NhaCungCap> LayDsNhaCungCap()
        {
            var ds = db.LayDSNhaCungCap();
            return ds;
        }
        public NhaCungCap LayNhaCungCapTheoMa(string ma)
        {
            var ds = db.LayNhaCungCapTheoMa(ma);
            return ds;
        }
        public Boolean Insert(NhaCungCap nx)
        {
            return db.Insert(nx);
        }
        public Boolean Update(NhaCungCap sx)
        {
            return db.Update(sx);
        }
        public Boolean Delete(string mancc)
        {
            return db.Delete(mancc);
        }
    }
}