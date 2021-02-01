using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;


namespace DOAN3.Models.DataAccess
{
    public class BinhLuanModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<BinhLuan> LayDSBinhLuan()
        {
            dt = db.LayDuLieu("select * from BinhLuan");
            List<BinhLuan> ds = new List<BinhLuan>();
            foreach (DataRow r in dt.Rows)
            {
                BinhLuan nv = new BinhLuan();
                nv.MaBL = Convert.ToString(r[0]);
                nv.NoiDungBL = Convert.ToString(r[1]);
                nv.MaThanhVien = Convert.ToString(r[2]);
                nv.MaSP = Convert.ToString(r[3]);
                nv.DanhGia = Convert.ToInt32(r[4]);
                ds.Add(nv);
            }
            return ds;
        }
        public BinhLuan LayDSBinhLuanTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from BinhLuan where MaBL='"+ma+"'");
            BinhLuan nv = new BinhLuan();
            if (dt.Rows.Count > 0) { 
                nv.MaBL = Convert.ToString(dt.Rows[0][0]);
                nv.NoiDungBL = Convert.ToString(dt.Rows[0][1]);
                nv.MaThanhVien = Convert.ToString(dt.Rows[0][2]);
                nv.MaSP = Convert.ToString(dt.Rows[0][3]);
                nv.DanhGia = Convert.ToInt32(dt.Rows[0][4]);
            }
            else { nv = null; }
            return nv;
        }
        public Boolean Insert(BinhLuan nv)
        {
            return db.ExcuteNonQuery("insert into BinhLuan values('" + nv.MaBL + "',N'" + nv.NoiDungBL + "',N'" + nv.MaThanhVien + "','" + nv.MaSP + "','" + nv.DanhGia + "')");
        }
        public Boolean InsertNULL(BinhLuan nv)
        {
            return db.ExcuteNonQuery("insert into BinhLuan values('" + nv.MaBL + "',N'" + nv.NoiDungBL + "',NULL,NULL,'" + nv.DanhGia + "')");
        }
        public Boolean Update(BinhLuan sx)
        {
            return db.ExcuteNonQuery("update BinhLuan set NoiDungBL=N'" + sx.NoiDungBL + "',MaThanhVien=N'" + sx.MaThanhVien + "',MaSP='" + sx.MaSP + "',DanhGia='" + sx.DanhGia + "' where MaBL='" + sx.MaBL + "'");
        }
        public Boolean UpdateNULL(BinhLuan sx)
        {
            return db.ExcuteNonQuery("update BinhLuan set NoiDungBL=N'" + sx.NoiDungBL + "',MaThanhVien=NULL,MaSP=NULL,DanhGia='" + sx.DanhGia + "' where MaBL='" + sx.MaBL + "'");
        }
        public Boolean Delete(string manv)
        {
            return db.ExcuteNonQuery("delete BinhLuan where MaBL='" + manv + "'");
        }
    }
}