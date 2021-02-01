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
    public class QuanLyNhanVienController : HomeAdminController
    {
        // GET: Admin/QuanLyNhanVien
        public QuanLyNhanVienBus db = new QuanLyNhanVienBus();
        TaiKhoanBus tkb = new TaiKhoanBus();
        public ActionResult DanhSachNhanVien()
        {
            var ds = tkb.LayDSTaiKhoan();
            ViewBag.UserName = new SelectList(ds, "UserName", "UserName");
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDsNhanvien();
            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(n => n.TenNV.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strNhanvien, int id)
        {          
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NhanVien nv = serializer.Deserialize<NhanVien>(strNhanvien);
            if (id == 0)
            {
                if (nv.TaiKhoan == "")
                {
                    if (db.InsertNULL(nv))
                    {
                        return Json(new { success = true, ms = "Them thanh cong" });
                    }
                    else
                    {
                        return Json(new { success = false, ms = "Them khong thanh cong" });

                    }
                }
                if (db.Insert(nv))
                {
                    return Json(new { success = true, ms = "Them thanh cong" });
                }
                else
                {
                    return Json(new { success = false, ms = "Them khong thanh cong" });

                }
            }
            else
            {
                if (nv.TaiKhoan == "")
                {
                    if (db.UpdateNULL(nv))
                    {
                        return Json(new { success = true, ms = "Sua thanh cong" });
                    }
                    else
                    {
                        return Json(new { success = false, ms = "Sua khong thanh cong" });
                    }
                }
                if (db.Update(nv))
                {
                    return Json(new { success = true, ms = "Sua thanh cong" });
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
            var ds = db.LayNhanVienTheoMa(manv);
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