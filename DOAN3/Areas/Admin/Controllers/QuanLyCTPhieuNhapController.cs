using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models.Entities;
using DOAN3.Models.DataAccess;
using System.Web.Script.Serialization;
using PagedList;
using DOAN3.Bussiness;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLyCTPhieuNhapController : HomeAdminController
    {
        // GET: Admin/QuanLyCTPhieuNhap
        ChiTietPNModel db = new ChiTietPNModel();
        public SanPhamModel spd = new SanPhamModel();
        public NhaCungCapModel ncc = new NhaCungCapModel();
        public ActionResult DanhSachCTPN(string ma,int? page)
        {
            var lsp = spd.LayDsSanPham();
            ViewBag.MaSP = new SelectList(lsp, "MaSP", "TenSP");
            var lncc = ncc.LayDSNhaCungCap();
            ViewBag.MaNCC = new SelectList(lncc, "MaNCC", "TenNCC");
            int pagenumber = page ?? 1;
            var ds = db.LayDsPnTheoMaFull(ma);
            return View(ds.ToPagedList(pagenumber,10));
        }
        public ActionResult DanhSPN()
        {
            return View();
        }
        public PartialViewResult DanhSachChiTietPN(string ma, int? page)
        {
            int pagenum = page ?? 1;
            var ds = db.LayDsPnTheoMaFull(ma);
            return PartialView(ds.ToPagedList(pagenum,10));
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string ma)
        {
            var ds = db.LayDsPnTheoMaFull(ma);
            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            //if (!string.IsNullOrEmpty(searchName))
            //{
            //    model = ds.FindAll(x => x.MaNCC.ToLower().Contains(searchName));
            //    totalrow = model.Count();
            //}
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string maphieunhap,string mancc,string ngaynhap,string masp,int soluongnhap,int dongia)
        {
            int thanhtien = soluongnhap * dongia;
            PhieuNhap pn = new PhieuNhap();
            pn.MaPhieuNhap = maphieunhap;
            pn.MaNCC = mancc;
            pn.NgayNhap = DateTime.Parse(ngaynhap);
            pn.ThanhTien = thanhtien;
            ChiTietPN cpn = new ChiTietPN();
            cpn.MaPhieuNhap = maphieunhap;
            cpn.MaSP = masp;
            cpn.SoLuongNhap = soluongnhap;
            cpn.DonGia = dongia;
            NhapHangBus mhb = new NhapHangBus();

            mhb.SuaHang(pn, thanhtien, cpn);
            ChiTietPNModel pnb = new ChiTietPNModel();
            //var ds = ssp.LaySanPham(masp);
            //ds.SoLuongTon += soluongnhap;
            //ds.NgayCapNhat = DateTime.Parse(ngaynhap);
            return Json(new { success = true, ms = "thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult GetData(string manv)
        //{
        //    var ds = db.LayDsPnTheoMaFull(manv);
        //    //var ds = db.LayDsPnTheoMa(manv);
        //    return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public JsonResult GetData(string ma)
        {
            var ds = db.LayPnTheoMaSP(ma);
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string manv,string masp)
        {
            if (db.Delete(manv,masp))
            {
                return Json(new { success = true, ms = "Xoa san pham thanh cong" });
            }
            else
            {
                return Json(new { success = false, ms = "Xoa khong thanh cong" });
            }
        }
    }
}