using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class NhaSanXuatModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<NhaSanXuat> LayDSNhaSanXuat()
        {
            dt = db.LayDuLieu("select * from NhaSanXuat");
            List<NhaSanXuat> ds = new List<NhaSanXuat>();
            foreach(DataRow r in dt.Rows)
            {
                NhaSanXuat sx = new NhaSanXuat();
                sx.MaNSX = Convert.ToString(r[0]);
                sx.TenNSX = Convert.ToString(r[1]);
                sx.Logo = Convert.ToString(r[2]);
                ds.Add(sx);
            }
            return ds;
        }
        public NhaSanXuat LayDSNhaSanXuatTheoma(string ma)
        {
            dt = db.LayDuLieu("select * from NhaSanXuat where MaNSX='"+ma+"'");
            NhaSanXuat sx = new NhaSanXuat();
            if (dt.Rows.Count > 0)
            {
                sx.MaNSX = Convert.ToString(dt.Rows[0][0]);
                sx.TenNSX = Convert.ToString(dt.Rows[0][1]);
                sx.Logo = Convert.ToString(dt.Rows[0][2]);
            }
            else sx = null;
            return sx;
        }
        public Boolean Insert(NhaSanXuat sx)
        {
            return db.ExcuteNonQuery("insert into NhaSanXuat values('" + sx.MaNSX + "',N'" + sx.TenNSX + "',N'" + sx.Logo + "')");
        }
        public Boolean Update(NhaSanXuat sx)
        {
            return db.ExcuteNonQuery("update NhaSanXuat set TenNSX=N'" + sx.TenNSX + "',Logo=N'" + sx.Logo + "' where MaNSX='" + sx.MaNSX + "'");
        }
        public Boolean Delete(string mansx)
        {
            return db.ExcuteNonQuery("delete NhaSanXuat where MaNSX='" + mansx + "'");
        }
    }
}