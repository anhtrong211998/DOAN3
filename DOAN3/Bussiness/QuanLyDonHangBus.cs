using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;


namespace DOAN3.Bussiness
{
    public class QuanLyDonHangBus
    {
        public void DatHang(KhachHang kh, int thanhtien, List<ChiTietDonDatHang> ds)
        {
            KhachHangModel db = new KhachHangModel();
            db.LuuKhachHang(kh);
            string ngay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            DonDatHangModel ddb = new DonDatHangModel();
            string MHMax = ddb.LayDonHangCungNgay(ngay);
            //Xử lý sinh mã hóa đơn theo quy tắc
            string ma = SinhMa(MHMax, ngay);
            //End

            DonDatHang dh = new DonDatHang();
            dh.MaDonDatHang = ma;
            dh.NgayDat = DateTime.Now;
            dh.TinhTrangGiaoHang = false;
            dh.DaThanhToan = false;
            dh.MaKH = kh.Email;
            dh.UuDai = "";
            //dh.NgayNhap = DateTime.Now;
            dh.ThanhTien = thanhtien;

            ddb.ThemDonHang(dh);

            ChiTietDonDatHangModel cdb = new ChiTietDonDatHangModel();
            foreach (ChiTietDonDatHang ct in ds)
            {
                ct.MaDonDatHang = ma;
            }
            cdb.LuuDanhChiTietDH(ds);
        }
        public void DatHangDon(DonDatHang dh, List<ChiTietDonDatHang> ds)
        {
            string ngay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            DonDatHangModel ddb = new DonDatHangModel();
            string MHMax = ddb.LayDonHangCungNgay(ngay);
            //Xử lý sinh mã hóa đơn theo quy tắc
            string ma = SinhMa(MHMax, ngay);
            //End

            dh.MaDonDatHang = ma;
            dh.NgayDat = DateTime.Now;
            dh.TinhTrangGiaoHang = false;
            dh.DaThanhToan = false;
            //dh.NgayNhap = DateTime.Now;

            ddb.ThemDonHang(dh);

            ChiTietDonDatHangModel cdb = new ChiTietDonDatHangModel();
            foreach (ChiTietDonDatHang ct in ds)
            {
                ct.MaDonDatHang = ma;
            }
            cdb.LuuDanhChiTietDH(ds);
        }
        public string SinhMa(string MHMax, string ngay)
        {
            int stt = 1;
            if (MHMax != "")
            { stt = Convert.ToInt16(MHMax.Substring(MHMax.Length - 4)) + 1; }
            string ma = stt.ToString();
            while (ma.Length < 4) { ma = "0" + ma; }
            ma = ngay + "." + ma;
            return ma;
        }
    }
}