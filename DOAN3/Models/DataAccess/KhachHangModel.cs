using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;


namespace DOAN3.Models.DataAccess
{
    public class KhachHangModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<KhachHang> LayDSKhachHang()
        {
            dt = db.LayDuLieu("select * from KhachHang");
            List<KhachHang> ds = new List<KhachHang>();
            foreach (DataRow r in dt.Rows)
            {
                KhachHang nv = new KhachHang();
                nv.Email = Convert.ToString(r[0]);
                nv.TenKH = Convert.ToString(r[1]);
                nv.SoDienThoai = Convert.ToString(r[2]);
                nv.DiaChiGiaoHang = Convert.ToString(r[3]);
                ds.Add(nv);
            }
            return ds;
        }
        public KhachHang LayKhachHangtheoma(string ma)
        {
            dt = db.LayDuLieu("select * from KhachHang where Email='"+ma+"'");
            KhachHang nv = new KhachHang();
            if (dt.Rows.Count > 0)
            {
                nv.Email = Convert.ToString(dt.Rows[0][0]);
                nv.TenKH = Convert.ToString(dt.Rows[0][1]);
                nv.SoDienThoai = Convert.ToString(dt.Rows[0][2]);
                nv.DiaChiGiaoHang = Convert.ToString(dt.Rows[0][3]);
            }
            else nv = null;
            return nv;
        }
        public Boolean Insert(KhachHang nv)
        {
            return db.ExcuteNonQuery("insert into KhachHang values(N'" + nv.Email + "',N'" + nv.TenKH + "','" + nv.SoDienThoai + "',N'" + nv.DiaChiGiaoHang + "')");
        }
        public Boolean Update(KhachHang sx)
        {
            return db.ExcuteNonQuery("update KhachHang set TenKH=N'" + sx.TenKH + "',SoDienThoai='" + sx.SoDienThoai + "',DiaChiGiaoHang=N'" + sx.DiaChiGiaoHang + "' where Email='" + sx.Email + "'");
        }
        public Boolean Delete(string makh)
        {
            return db.ExcuteNonQuery("delete KhachHang where Email='" + makh + "'");
        }
        public void LuuKhachHang(KhachHang kh)
        {
            DataTable dt = db.LayDuLieu("select * from KhachHang where(Email='" + kh.Email + "')");
            if (dt.Rows.Count <= 0)
            {
                string st = "insert into KhachHang values('" + kh.Email + "',N'" + kh.TenKH + "','" + kh.SoDienThoai + "',N'" + kh.DiaChiGiaoHang + "')";
                db.ExcuteNonQuery(st);
            }

        }
    }
}