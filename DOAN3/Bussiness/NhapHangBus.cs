using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class NhapHangBus
    {
       
        public void NhaphangList(PhieuNhap kh,int thanhtien, List<ChiTietPN> ds)
        {
            string ngay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            PhieuNhapModel ddb = new PhieuNhapModel();
            string MHMax = ddb.LayDonHangCungNgay(ngay);
            //Xử lý sinh mã hóa đơn theo quy tắc
            string ma = SinhMa(MHMax, ngay);
            //End

            PhieuNhap dh = new PhieuNhap();
            dh.MaPhieuNhap = ma;
            //dh.NgayNhap = DateTime.Now;
            dh.ThanhTien = thanhtien;

            ddb.ThemDonHang(dh);

            ChiTietPNModel cdb = new ChiTietPNModel();
            foreach (ChiTietPN ct in ds)
            {
                ct.MaPhieuNhap = ma;
            }
            cdb.LuuDanhSachPN(ds);           
        }
        public void NhaphangDon(PhieuNhap kh, int thanhtien, ChiTietPN ds)
        {
            string ngay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            PhieuNhapModel ddb = new PhieuNhapModel();
            string MHMax = ddb.LayDonHangCungNgay(ngay);
            //Xử lý sinh mã hóa đơn theo quy tắc
            string ma = SinhMa(MHMax, ngay);
            //End
            kh.MaPhieuNhap = ma;
            //kh.NgayNhap = DateTime.Now;
            kh.ThanhTien = thanhtien;
            ddb.ThemDonHang(kh);
            ChiTietPNModel cdb = new ChiTietPNModel();
                ds.MaPhieuNhap = ma;
            cdb.LuuCTPN(ds);
            int sl = cdb.LaySoluongTheoMaSPN(ma, ds.MaSP);
            SanPhamModel ssp = new SanPhamModel();
            SanPham dss = ssp.LaySanPhamTheoMa(ds.MaSP);
            dss.SoLuongTon += sl;
        }
        public void SuaHang(PhieuNhap kh, int thanhtien, ChiTietPN ds)
        {
          
            PhieuNhapModel ddb = new PhieuNhapModel();
            //kh.NgayNhap = DateTime.Now;
            kh.ThanhTien = thanhtien;
            ddb.UpdatePN(kh);
            ChiTietPNModel cdb = new ChiTietPNModel();
            cdb.SuaCTPN(ds);
            //int sl = cdb.LaySoluongTheoMaSPN(ma, ds.MaSP);
            //SanPhamModel ssp = new SanPhamModel();
            //SanPham dss = ssp.LaySanPhamTheoMa(ds.MaSP);
            //dss.SoLuongTon += sl;
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