using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;

namespace DOAN3.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        public ActionResult Xemgiohang()
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
            //return View();
        }
        [HttpPost]
        public ActionResult ThemGioHang(string masp)
        {
            QuanLySanPhamBus db = new QuanLySanPhamBus();
            SanPham sp = db.LaySanPham(masp);
            List<itemGioHang> gh = null;
            if (Session["giohang"] == null)
            {
                itemGioHang a = new itemGioHang();
                a.MaSP = sp.MaSP;
                a.TenSP = sp.TenSP;
                a.HinhAnh = sp.HinhAnh;
                a.DonGia = sp.DonGia - ((sp.DonGia * sp.GiamGia) / 100);
                a.SoLuong = 1;
                gh = new List<itemGioHang>();
                gh.Add(a);
                Session["giohang"] = new List<itemGioHang>();
            }
            else
            {
                gh = Session["giohang"] as List<itemGioHang>;
                itemGioHang test = gh.FirstOrDefault(m => m.MaSP == masp);
                if (test == null)
                {
                    itemGioHang a = new itemGioHang();
                    a.MaSP = sp.MaSP;
                    a.TenSP = sp.TenSP;
                    a.HinhAnh = sp.HinhAnh;
                    a.DonGia = sp.DonGia - ((sp.DonGia * sp.GiamGia) / 100);
                    a.SoLuong = 1;
                    gh.Add(a);
                }
                else
                {

                    test.SoLuong = test.SoLuong + 1;
                }
                Session["giohang"] = gh;
            }
            Session["giohang"] = gh;
            int tongtien = 0;
            int soluong = 0;
            foreach (itemGioHang ct in gh)
            {
                tongtien += ct.SoLuong * ct.DonGia;
            }
            soluong = gh.Count;
            return Json(new { success = true, ms = "Them san pham thanh cong", data = gh, tongtien = tongtien, Soluong = soluong }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SuaGioHang(string masp, int sl)
        {
            List<itemGioHang> l = Session["GioHang"] as List<itemGioHang>;
            l.Find(m => m.MaSP == masp).SoLuong = sl;
            int tongtien = 0;
            foreach (itemGioHang ct in l)
            {
                tongtien += ct.SoLuong * ct.DonGia;
            }
            return Json(new { success = true, ms = "Sua san pham thanh cong", tongtien = tongtien }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult XoaGioHang(string masp)
        {
            int tongtien = 0;
            int soluong = 0;
            List<itemGioHang> l = Session["giohang"] as List<itemGioHang>;
            if (l != null)
            {
                l.RemoveAt(l.FindIndex(m => m.MaSP == masp));
                foreach (itemGioHang ct in l)
                {
                    tongtien += ct.SoLuong * ct.DonGia;
                }
                soluong = l.Count;
            }
            else
            {
                soluong = 0;
            }

            return Json(new { success = true, ms = "Xoa san pham thành công", tongtien = tongtien, soluong = soluong }, JsonRequestBehavior.AllowGet);
        }
    }
}