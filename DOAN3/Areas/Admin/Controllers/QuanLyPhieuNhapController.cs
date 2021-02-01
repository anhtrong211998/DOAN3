using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DOAN3.Models.Entities;
using DOAN3.Models.DataAccess;
using DOAN3.Bussiness;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLyPhieuNhapController : HomeAdminController
    {
        // GET: Admin/QuanLyPhieuNhap
        public PhieuNhapModel db = new PhieuNhapModel();
        public NhapHangBus nhb = new NhapHangBus();
        public QuanLyNhaCungCapBus ncc = new QuanLyNhaCungCapBus();
        public QuanLySanPhamBus ssp = new QuanLySanPhamBus();
        public ActionResult DanhSachPhieuNhap()
        {
            var lncc = ncc.LayDsNhaCungCap();
            ViewBag.MaNCC = new SelectList(lncc, "MaNCC", "TenNCC");
            var lsp = ssp.LayDanhSachSanPham();
            ViewBag.MaSP = new SelectList(lsp, "MaSP", "MaSP");
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDSPhieuNhap();

            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(x => x.MaNCC.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NhapHang(string mancc,string masp,string ngaynhap,int soluongnhap,int dongia)
        {
            int tongtien=soluongnhap * dongia;
            PhieuNhap sp = new PhieuNhap();
            //sp.MaPhieuNhap = mapn;
            sp.MaNCC = mancc;
            sp.NgayNhap = DateTime.Parse(ngaynhap);
            ChiTietPN pn = new ChiTietPN();
            pn.MaSP = masp;
            pn.SoLuongNhap = soluongnhap;
            pn.DonGia = dongia;
            NhapHangBus mhb = new NhapHangBus();

            mhb.NhaphangDon(sp,tongtien,pn);
            ChiTietPNModel pnb = new ChiTietPNModel();
            //var ds = ssp.LaySanPham(masp);
            //ds.SoLuongTon += soluongnhap;
            //ds.NgayCapNhat = DateTime.Parse(ngaynhap);
            return Json(new {success=true, ms = "thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strPN, int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PhieuNhap l = serializer.Deserialize<PhieuNhap>(strPN);
            if (id == 0)
            {
                if (db.Insert(l))
                {
                    return Json(new { success = true, ms = "Them thanh cong" });
                }
                else
                {
                    return Json(new { succes = false, ms = "Them khong thanh cong" });
                }
            }
            else
            {
                if (db.Update(l))
                {
                    return Json(new { success = true, ms = "Sua san pham thanh cong" });
                }
                else
                {
                    return Json(new { success = false, ms = "Sua khong thanh cong" });
                }
            }
        }
        [HttpGet]
        public JsonResult GetData(string maloaisp)
        {
            var ds = db.LayPhieuNhapTheoMa(maloaisp);
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string maloaisp)
        {
            if (db.Delete(maloaisp))
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