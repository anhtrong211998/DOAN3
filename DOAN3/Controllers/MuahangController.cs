using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;

namespace DOAN3.Controllers
{
    public class MuahangController : Controller
    {
        // GET: Muahang
        public ActionResult Muahang()
        {
            List<itemGioHang> ds = null;
            int tongtien = 0;
            ViewBag.tongtien = "";
            if (Session["giohang"] != null)
            {
                ds = Session["giohang"] as List<itemGioHang>;
                foreach (itemGioHang ct in ds)
                {
                    tongtien += ct.SoLuong * ct.DonGia;
                }
            }
            ViewBag.tongtien = tongtien;
            return View(ds);
        }
        [HttpPost]
        public ActionResult ThanhToan(string email,string hoten,string sodienthoai,string diachi)
        {
            KhachHang kh = new KhachHang();
            kh.Email = email;
            kh.TenKH = hoten;
            kh.SoDienThoai = sodienthoai;
            kh.DiaChiGiaoHang = diachi;

            int thanhtien = 0;
            List<ChiTietDonDatHang> dsl = new List<ChiTietDonDatHang>();
            List<itemGioHang> ds = new List<itemGioHang>();
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<itemGioHang>();
                //thanhtien = 0;
            }
            else
            {
                
                ds = Session["GioHang"] as List<itemGioHang>;
                for (int i = 0; i < ds.Count(); i++)
                {
                    ChiTietDonDatHang cthd = new ChiTietDonDatHang();
                    cthd.MaSP = ds.ElementAtOrDefault(i).MaSP;
                    cthd.TenSP = ds.ElementAtOrDefault(i).TenSP;
                    cthd.SoLuong = ds.ElementAtOrDefault(i).SoLuong;
                    cthd.DonGia = ds.ElementAtOrDefault(i).DonGia;
                    dsl.Add(cthd);
                    thanhtien += Convert.ToInt32(dsl.Sum(s => s.DonGia * s.SoLuong));
                }

            }
            QuanLyDonHangBus mhb = new QuanLyDonHangBus();
            //mhb.MuaHang(kh, thanhtien, dsl);
            mhb.DatHang(kh, thanhtien,dsl);
            ds.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}