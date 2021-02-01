using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class LoaiSanPhamModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<LoaiSanPham> LayDSLoaiSanPham()
        {
            dt = db.LayDuLieu("select * from LoaiSanPham");
            List<LoaiSanPham> ds = new List<LoaiSanPham>();
            foreach(DataRow r in dt.Rows)
            {
                LoaiSanPham l = new LoaiSanPham();
                l.MaLoaiSP = Convert.ToString(r[0]);
                l.TenLoai = Convert.ToString(r[1]);
                l.MoTa = Convert.ToString(r[2]);
                ds.Add(l);
            }
            return ds;
        }
        public LoaiSanPham LayLoaiSanPhamTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from LoaiSanPham where MaLoaiSP='"+ma+"'");
            LoaiSanPham l = new LoaiSanPham();
            if (dt.Rows.Count > 0)
            {

                l.MaLoaiSP = Convert.ToString(dt.Rows[0][0]);
                l.TenLoai = Convert.ToString(dt.Rows[0][1]);
                l.MoTa = Convert.ToString(dt.Rows[0][2]);
            }
            else l = null;
            return l;
        }
        public Boolean Insert(LoaiSanPham l)
        {
            return db.ExcuteNonQuery("insert into LoaiSanPham values('" + l.MaLoaiSP + "',N'" + l.TenLoai + "',N'"+l.MoTa+"')");
        }
        public Boolean Update(LoaiSanPham l)
        {
            return db.ExcuteNonQuery("update LoaiSanPham set TenLoai=N'" + l.TenLoai + "',MoTa=N'" + l.MoTa + "' where MaLoaiSP='"+l.MaLoaiSP+"'");
        }
        public Boolean Delete(string maloaisp)
        {
            return db.ExcuteNonQuery("delete LoaiSanPham where MaLoaiSP='" + maloaisp + "'");
        }
    }
}