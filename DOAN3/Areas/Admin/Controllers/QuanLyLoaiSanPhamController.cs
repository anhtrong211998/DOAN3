using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;
using System.Web.Script.Serialization;
using PagedList;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLyLoaiSanPhamController : HomeAdminController
    {
        // GET: Admin/QuanLyLoaiSanPham
        public QuanLyLoaiSanPhamBus db = new QuanLyLoaiSanPhamBus();
        QuanLySanPhamBus spb = new QuanLySanPhamBus();
        public ActionResult DanhSachLoaiSanPham()
        {
            return View();
        }
        public ActionResult DsSanPham(string ma,int?page)
        {
            int pagenuber = page ?? 1;
            var lsp = spb.LaySanPhamTheoLoai(ma);
            //if (!string.IsNullOrEmpty(search))
            //{
            //    lsp = lsp.FindAll(n => n.MaSP.ToLower().Contains(search));
            //}
            return PartialView(lsp.ToPagedList(pagenuber, 10));
        }
        [HttpGet]
        public JsonResult LoadData(int page,int pagesize,string searchName)
        {
            var ds = db.LayDsLoaiSanPham();
           
            int totalrow = ds.Count();
            int sl = totalrow;
           var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(x => x.TenLoai.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model,total=totalrow,sl=sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strLoaiSanPham,int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            LoaiSanPham l = serializer.Deserialize<LoaiSanPham>(strLoaiSanPham);
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
            var ds = db.LayLoaiSanPhamTheoMa(maloaisp);
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