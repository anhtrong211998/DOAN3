using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLyNhaSanXuatBus
    {
        public NhaSanXuatModel db = new NhaSanXuatModel();
        public List<NhaSanXuat> LayDsNhaSanXuat()
        {
            var ds = db.LayDSNhaSanXuat();
            return ds;
        }
        public NhaSanXuat LayNhaSanXuatTheoMa(string ma)
        {
            var ds = db.LayDSNhaSanXuatTheoma(ma);
            return ds;
        }
        public Boolean Insert(NhaSanXuat nx)
        {
            return db.Insert(nx);
        }
        public Boolean Update(NhaSanXuat sx)
        {
            return db.Update(sx);
        }
        public Boolean Delete(string mansx)
        {
            return db.Delete(mansx);
        }
    }
}