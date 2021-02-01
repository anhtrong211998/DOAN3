using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;
using System.Web.Script.Serialization;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLyKhacHangController : HomeAdminController
    {
        // GET: Admin/QuanLyKhacHang
        public QuanLyKhachHangBus db = new QuanLyKhachHangBus();
        public ActionResult DanhSachKhachHang()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDsKhachHang();

            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(x => x.TenKH.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strKhachHang, int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KhachHang l = serializer.Deserialize<KhachHang>(strKhachHang);
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
        public JsonResult GetData(string MaKH)
        {
            var ds = db.LayKhachHangTheoMa(MaKH);
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string MaKH)
        {
            if (db.Delete(MaKH))
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