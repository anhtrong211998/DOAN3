using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class ChiTietDonDatHangModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        DataTable dt;
        public void LuuDanhChiTietDH(List<ChiTietDonDatHang> ds)
        {
            dt = new DataTable();
            dt.Columns.Add("MaDonDatHang");
            dt.Columns.Add("MaSP");
            dt.Columns.Add("TenSP");
            dt.Columns.Add("SoLuong");
            dt.Columns.Add("DonGia");
            foreach(ChiTietDonDatHang item in ds)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.MaDonDatHang;
                dr[1] = item.MaSP;
                dr[2] = item.TenSP;
                dr[3] = item.SoLuong;
                dr[4] = item.DonGia;
                dt.Rows.Add(dr);
            }
            db.UpdateDataBase(dt, "ChiTietDonDatHang");
        }
        public void LuuChiTietDonDatHang(ChiTietDonDatHang l)
        {
            string se = "insert into ChiTietDonDatHang values('" + l.MaDonDatHang + "', '" + l.MaSP + "', '" + l.TenSP + "', '" + l.SoLuong + "', '" + l.DonGia + "')";
            db.ExcuteNonQuery(se);
        }
        public List<ChiTietDonDatHang> LayDshdTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from ChiTietDonDatHang inner join DonDatHang on ChiTietPN.MaDonDatHang = DonDatHang.MaDonDatHang where DonDatHang.MaDonDatHang='" + ma + "'");
            List<ChiTietDonDatHang> ds = new List<ChiTietDonDatHang>();
            foreach (DataRow r in dt.Rows)
            {
                ChiTietDonDatHang l = new ChiTietDonDatHang();
                l.MaDonDatHang = Convert.ToString(r[0]);
                l.MaSP = Convert.ToString(r[1]);
                l.TenSP = Convert.ToString(r[2]);
                l.SoLuong = Convert.ToInt32(r[3]);
                l.DonGia = Convert.ToInt32(r[4]);
                ds.Add(l);
            }
            return ds;
        }
        public Boolean Insert(ChiTietDonDatHang l)
        {
            return db.ExcuteNonQuery("insert into ChiTietDonDatHang values('" + l.MaDonDatHang + "', '" + l.MaSP + "', '" + l.TenSP + "', '" + l.SoLuong + "', '" + l.DonGia + "')");
        }
        public Boolean Update(ChiTietDonDatHang l)
        {
            return db.ExcuteNonQuery("update ChiTietDonDatHang set MaSP='" + l.MaSP + "',TenSP='" + l.TenSP + "',SoLuong='" + l.SoLuong + "',DonGia='" + l.DonGia + "' where MaDonDatHang='" + l.MaDonDatHang + "'");
        }
        public Boolean Delete(string maloaisp, string masp)
        {
            return db.ExcuteNonQuery("delete ChiTietDonDatHang where (MaDonDatHang='" + maloaisp + "' and MaSP='" + masp + "')");
        }
    }
}