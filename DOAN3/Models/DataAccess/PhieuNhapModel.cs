using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;
namespace DOAN3.Models.DataAccess
{
    public class PhieuNhapModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        DataTable dt;
        public List<PhieuNhap> LayDSPhieuNhap()
        {
            dt = db.LayDuLieu("select * from PhieuNhap");
            List<PhieuNhap> ds = new List<PhieuNhap>();
            foreach (DataRow r in dt.Rows)
            {
                PhieuNhap l = new PhieuNhap();
                l.MaPhieuNhap = Convert.ToString(r[0]);
                l.MaNCC = Convert.ToString(r[1]);
                l.NgayNhap = Convert.ToDateTime(r[2]);
                    l.ThanhTien = Convert.ToInt32(r[3]);
                ds.Add(l);
            }
            return ds;
        }
        public PhieuNhap LayPhieuNhapTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from PhieuNhap where MaPhieuNhap='" + ma + "'");
            PhieuNhap l = new PhieuNhap();
            if (dt.Rows.Count > 0)
            {

                l.MaPhieuNhap = Convert.ToString(dt.Rows[0][0]);
                l.MaNCC = Convert.ToString(dt.Rows[0][1]);
                l.NgayNhap = Convert.ToDateTime(dt.Rows[0][2]);
                l.ThanhTien = Convert.ToInt32(dt.Rows[0][3]);
            }
            else l = null;
            return l;
        }
        public Boolean Insert(PhieuNhap l)
        {
            return db.ExcuteNonQuery("insert into PhieuNhap values('" + l.MaPhieuNhap + "','" + l.MaNCC + "','" + l.NgayNhap + "','" + l.ThanhTien + "')");
        }
        public Boolean Update(PhieuNhap l)
        {
            return db.ExcuteNonQuery("update PhieuNhap set MaNCC='" + l.MaNCC + "',NgayNhap='" + l.NgayNhap + "',ThanhTien='" + l.ThanhTien + "' where MaPhieuNhap='" + l.MaPhieuNhap + "'");
        }
        public Boolean Delete(string maloaisp)
        {
            return db.ExcuteNonQuery("delete PhieuNhap where MaPhieuNhap='" + maloaisp + "'");
        }
        public string LayDonHangCungNgay(string ngay)
        {

            string se = "Select top 1 MaPhieuNhap from PhieuNhap where MaPhieuNhap like '" + ngay + "%' order by MaPhieuNhap";
            DataTable dt = db.LayDuLieu(se);
            if (dt.Rows.Count <= 0)
                return "";
            else
                return Convert.ToString(dt.Rows[0][0]);
        }
        public void ThemDonHang(PhieuNhap l)
        {
            String st = "insert into PhieuNhap values('" + l.MaPhieuNhap + "','" + l.MaNCC + "','" + l.NgayNhap + "','" + l.ThanhTien + "')";
            db.ExcuteNonQuery(st);
        }
        public void UpdatePN(PhieuNhap l)
        {
            string st = "update PhieuNhap set MaNCC='" + l.MaNCC + "',NgayNhap='" + l.NgayNhap + "',ThanhTien='" + l.ThanhTien + "' where MaPhieuNhap='" + l.MaPhieuNhap + "'";
            db.ExcuteNonQuery(st);
        }
    }
}