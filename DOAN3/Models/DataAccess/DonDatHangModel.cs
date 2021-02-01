using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class DonDatHangModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        DataTable dt;
        public List<DonDatHang> LayDSPhieuNhap()
        {
            dt = db.LayDuLieu("select * from PhieuNhap");
            List<DonDatHang> ds = new List<DonDatHang>();
            foreach (DataRow r in dt.Rows)
            {
                DonDatHang l = new DonDatHang();
                l.MaDonDatHang = Convert.ToString(r[0]);
                l.NgayDat = Convert.ToDateTime(r[1]);
                l.TinhTrangGiaoHang = Convert.ToBoolean(r[2]);
                l.NgayGiao = Convert.ToDateTime(r[3]);
                l.DaThanhToan = Convert.ToBoolean(r[4]);
                l.MaKH = Convert.ToString(r[5]);
                l.MaNV = Convert.ToString(r[6]);
                l.UuDai = Convert.ToString(r[7]);
                l.ThanhTien = Convert.ToInt32(r[8]);
                ds.Add(l);
            }
            return ds;
        }
        public List<DonDatHang> LayDSDHChuaXacNan()
        {
            dt = db.LayDuLieu("select * from DonDatHang where TinhTrangGiaoHang='false'");
            List<DonDatHang> ds = new List<DonDatHang>();
            foreach (DataRow r in dt.Rows)
            {
                DonDatHang l = new DonDatHang();
                l.MaDonDatHang = Convert.ToString(r[0]);
                l.NgayDat = Convert.ToDateTime(r[1]);
                l.TinhTrangGiaoHang = Convert.ToBoolean(r[2]);
                l.NgayGiao = Convert.ToDateTime(r[3]);
                l.DaThanhToan = Convert.ToBoolean(r[4]);
                l.MaKH = Convert.ToString(r[5]);
                l.MaNV = Convert.ToString(r[6]);
                l.UuDai = Convert.ToString(r[7]);
                l.ThanhTien = Convert.ToInt32(r[8]);
                ds.Add(l);
            }
            return ds;
        }
        public DonDatHang LayPhieuNhapTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from DonDatHang where MaDonDatHang='" + ma + "'");
            DonDatHang l = new DonDatHang();
            if (dt.Rows.Count > 0)
            {

                l.MaDonDatHang = Convert.ToString(dt.Rows[0][0]);
                l.NgayDat = Convert.ToDateTime(dt.Rows[0][1]);
                l.TinhTrangGiaoHang = Convert.ToBoolean(dt.Rows[0][2]);
                l.NgayGiao = Convert.ToDateTime(dt.Rows[0][3]);
                l.DaThanhToan = Convert.ToBoolean(dt.Rows[0][4]);
                l.MaKH = Convert.ToString(dt.Rows[0][5]);
                l.MaNV = Convert.ToString(dt.Rows[0][6]);
                l.UuDai = Convert.ToString(dt.Rows[0][7]);
                l.ThanhTien = Convert.ToInt32(dt.Rows[0][8]);
            }
            else l = null;
            return l;
        }
        public Boolean Insert(DonDatHang l)
        {
            return db.ExcuteNonQuery("insert into DonDatHang values('" + l.MaDonDatHang + "','" + l.NgayDat + "','" + l.TinhTrangGiaoHang + "','" + l.NgayGiao + "','" + l.DaThanhToan + "','" + l.MaKH + "','" + l.MaNV + "','" + l.UuDai + "','" + l.ThanhTien + "')");
        }
        public Boolean Update(DonDatHang l)
        {
            return db.ExcuteNonQuery("update DonDatHang set NgayDat='" + l.NgayDat + "',TinhTrangGiaoHang='" + l.TinhTrangGiaoHang + "',NgayGiao='" + l.NgayGiao + "',DaThanhToan='" + l.DaThanhToan + "',MaKH='" + l.MaKH + "',MaNV='" + l.MaNV + "',UuDai='" + l.UuDai + "',ThanhTien='" + l.ThanhTien + "' where MaDonDatHang='" + l.MaDonDatHang + "'");
        }
        public Boolean Delete(string maloaisp)
        {
            return db.ExcuteNonQuery("delete DonDatHang where MaDonDatHang='" + maloaisp + "'");
        }
        public string LayDonHangCungNgay(string ngay)
        {

            string se = "Select top 1 MaDonDatHang from DonDatHang where MaDonDatHang like '" + ngay + "%' order by MaDonDatHang";
            DataTable dt = db.LayDuLieu(se);
            if (dt.Rows.Count <= 0)
                return "";
            else
                return Convert.ToString(dt.Rows[0][0]);
        }
        public void ThemDonHang(DonDatHang l)
        {
            String st = "insert into DonDatHang values('" + l.MaDonDatHang + "','" + l.NgayDat + "','" + l.TinhTrangGiaoHang + "',NULL,'" + l.DaThanhToan + "','" + l.MaKH + "',NULL,'" + l.UuDai + "','" + l.ThanhTien + "')";
            db.ExcuteNonQuery(st);
        }
    }
}