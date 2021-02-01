using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DOAN3.Models.Entities;
using DOAN3.Bussiness;
using PagedList;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLySanPhamController : HomeAdminController
    {
        // GET: Admin/QuanLySanPham
        public QuanLySanPhamBus db = new QuanLySanPhamBus();
        QuanLyNhaCungCapBus ncc = new QuanLyNhaCungCapBus();
        QuanLyLoaiSanPhamBus lsp = new QuanLyLoaiSanPhamBus();
        QuanLyNhaSanXuatBus nsx = new QuanLyNhaSanXuatBus();
        public ActionResult DanhSachSanPham()
        {
            var lncc = ncc.LayDsNhaCungCap();
            ViewBag.MaNCC= new SelectList(lncc, "MaNCC", "TenNCC");
            var lnsx = nsx.LayDsNhaSanXuat();
            ViewBag.MaNSX = new SelectList(lnsx, "MaNSX", "TenNSX");
            var dslsp = lsp.LayDsLoaiSanPham();
            ViewBag.MaLoaiSP = new SelectList(dslsp, "MaLoaiSP", "TenLoai");
            return View();
        }
        public PartialViewResult SanPhamPartial(string maloai,int?page,string search)
        {
            var lsp = new List<SanPham>();
            int pagenuber = page ?? 1;
            if (maloai == "")
            {
                lsp = db.LayDanhSachSanPham();
                if (!string.IsNullOrEmpty(search))
                {
                    lsp = lsp.FindAll(n => n.TenSP.ToLower().Contains(search));
                }

            }
            else
            {
                lsp = db.LaySanPhamTheoLoai(maloai);
                if (!string.IsNullOrEmpty(search))
                {
                    lsp = lsp.FindAll(n => n.MaSP.ToLower().Contains(search));
                }
            }

            return PartialView(lsp.ToPagedList(pagenuber, 10));
        }
        public string HinhAnhUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Content/products-images/" + file.FileName));
            return file.FileName;
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveData(string strSanPham, int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SanPham l = serializer.Deserialize<SanPham>(strSanPham);
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
        public JsonResult GetData(string manv)
        {
            var ds = db.LaySanPham(manv);
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string manv)
        {
            if (db.Delete(manv))
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
