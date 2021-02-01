using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Areas.Admin.Models;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class TaiKhoanBus
    {
        TaiKhoanModel db = new TaiKhoanModel();
        public List<TaiKhoan> LayDSTaiKhoan()
        {
            var ds = db.LayDSTaiKhoan();
            return ds;
        }
        public TaiKhoan LayTaiKhoanTheoMa(string ma)
        {
            var ds = db.LayTaiKhoanTheoMa(ma);
            return ds;
        }
        public Boolean Insert(TaiKhoan tk)
        {
            return db.Insert(tk);
        }
        public Boolean Update(TaiKhoan tk)
        {
            return db.Update(tk);
        }
        public Boolean Delete(string username)
        {
            return db.Delete(username);
        }
    }
}