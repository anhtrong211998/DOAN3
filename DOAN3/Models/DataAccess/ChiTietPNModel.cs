using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class ChiTietPNModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        DataTable dt;
        public void LuuDanhSachPN(List<ChiTietPN> ds)
        {
            dt = new DataTable();
            dt.Columns.Add("MaPhieuNhap");
            dt.Columns.Add("MaSP");
            dt.Columns.Add("SoLuongNhap");
            dt.Columns.Add("DonGia");
            foreach (ChiTietPN item in ds)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.MaPhieuNhap;
                dr[1] = item.MaSP;
                dr[2] = item.SoLuongNhap;
                dr[3] = item.DonGia;
                dt.Rows.Add(dr);
            }
            db.UpdateDataBase(dt, "ChiTietPN");
        }
        public void LuuCTPN(ChiTietPN l)
        {
            string se = "insert into ChiTietPN values('" + l.MaPhieuNhap + "', '" + l.MaSP + "', '" + l.SoLuongNhap + "', '" + l.DonGia + "')";
            db.ExcuteNonQuery(se);
        }
        public void SuaCTPN(ChiTietPN l)
        {
            string se = "update ChiTietPN set SoLuongNhap='" + l.SoLuongNhap + "',DonGia='" + l.DonGia + "' where MaPhieuNhap='" + l.MaPhieuNhap + "' and MaSP='"+l.MaSP+"'";
            db.ExcuteNonQuery(se);
        }
        public List<ChiTietPN> LayDsPnTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from ChiTietPN inner join PhieuNhap on ChiTietPN.MaPhieuNhap = PhieuNhap.MaPhieuNhap where PhieuNhap.MaPhieuNhap='" + ma + "'");
            List<ChiTietPN> ds = new List<ChiTietPN>();
            foreach (DataRow r in dt.Rows)
            {
                ChiTietPN l = new ChiTietPN();
                l.MaPhieuNhap = Convert.ToString(r[0]);
                l.MaSP = Convert.ToString(r[1]);
                l.SoLuongNhap = Convert.ToInt32(r[2]);
                l.DonGia = Convert.ToInt32(r[3]);
                ds.Add(l);
            }
            return ds;
        }
        public List<ChiTietPN> LayDsPnTheoMaFull(string ma)
        {
            dt = db.LayDuLieu("select cp.MaPhieuNhap,cp.MaSP,cp.SoLuongNhap,cp.DonGia,p.MaNCC,p.NgayNhap,p.ThanhTien from ChiTietPN cp inner join PhieuNhap p on cp.MaPhieuNhap = p.MaPhieuNhap where p.MaPhieuNhap = '"+ma+"'");
            List<ChiTietPN> ds = new List<ChiTietPN>();
            foreach (DataRow r in dt.Rows)
            {
                ChiTietPN l = new ChiTietPN();
                l.MaPhieuNhap = Convert.ToString(r[0]);
                l.MaSP = Convert.ToString(r[1]);
                l.SoLuongNhap = Convert.ToInt32(r[2]);
                l.DonGia = Convert.ToInt32(r[3]);
                var MaNCC = Convert.ToString(r[4]);
                l.PhieuNhap = new PhieuNhap
                {
                    MaNCC = Convert.ToString(r[4]),
                    NgayNhap = Convert.ToDateTime(r[5]),
                    ThanhTien = Convert.ToInt32(r[6])
                };               
                //try
                //{
                //    l.PhieuNhap.MaNCC = MaNCC;
                //    l.PhieuNhap.NgayNhap = Convert.ToDateTime(r[5]);
                //    l.PhieuNhap.NgayNhap = Convert.ToDateTime(r[6]);
                //}
                //catch (NullReferenceException e)
                //{
                //    var aa = e.ToString();
                //}
                
                ds.Add(l);
            }
            return ds;
        }
        public ChiTietPN LayPnTheoMaSP(string ma)
        {
            dt = db.LayDuLieu("select cp.MaPhieuNhap,cp.MaSP,cp.SoLuongNhap,cp.DonGia,p.MaNCC,p.NgayNhap,p.ThanhTien from ChiTietPN cp inner join PhieuNhap p on cp.MaPhieuNhap = p.MaPhieuNhap where cp.MaPhieuNhap = '" + ma + "'");
            ChiTietPN l = new ChiTietPN();
            if (dt.Rows.Count > 0)
            {

                l.MaPhieuNhap = Convert.ToString(dt.Rows[0][0]);
                l.MaSP = Convert.ToString(dt.Rows[0][1]);
                l.SoLuongNhap = Convert.ToInt32(dt.Rows[0][2]);
                l.DonGia = Convert.ToInt32(dt.Rows[0][3]);
                l.PhieuNhap =new PhieuNhap
                {
                    MaNCC = Convert.ToString(dt.Rows[0][4]),
                    NgayNhap = Convert.ToDateTime(dt.Rows[0][5]),
                    ThanhTien = Convert.ToInt32(dt.Rows[0][6])
                };
               
            }
            else l = null;
            return l;
        }
        public int LaySoluongTheoMaSPN(string ma, string masp)
        {
            dt = db.LayDuLieu("select SoLuongNhap from ChiTietPN inner join PhieuNhap on ChiTietPN.MaPhieuNhap = PhieuNhap.MaPhieuNhap where (PhieuNhap.MaPhieuNhap='" + ma + "' and ChiTietPN.MaSP='" + masp + "')");
            if (dt.Rows.Count <= 0)
            {
                return -1;
            }
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public Boolean Insert(ChiTietPN l)
        {
            return db.ExcuteNonQuery("insert into ChiTietPN values('" + l.MaPhieuNhap + "','" + l.MaSP + "','" + l.SoLuongNhap + "','" + l.DonGia + "')");
        }
        public Boolean Update(ChiTietPN l)
        {
            return db.ExcuteNonQuery("update ChiTietPN set MaSP='" + l.MaSP + "',SoLuongNhap='" + l.SoLuongNhap + "',DonGia='" + l.DonGia + "' where MaPhieuNhap='" + l.MaPhieuNhap + "'");
        }
        public Boolean Delete(string maloaisp,string masp)
        {
            return db.ExcuteNonQuery("delete ChiTietPN where (MaPhieuNhap='" + maloaisp + "' and MaSP='"+masp+"')");
        }
    }

}