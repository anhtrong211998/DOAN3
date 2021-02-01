using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;
using DOAN3.Common;

namespace DOAN3.Areas.Admin.Controllers
{
    public class QuanLyTaiKhoanAdminController : HomeAdminController
    {
        // GET: Admin/QuanLyTaiKhoanAdmin
        public TaiKhoanBus db = new TaiKhoanBus();
        public ActionResult DanhSachTaiKhoan()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDSTaiKhoan();

            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(x => x.UserName.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string UserName,string MatKhau, int id)
        {
            TaiKhoan l = new TaiKhoan();
            l.UserName = UserName;
            l.MatKhau = Encrypter.MD5Hash(MatKhau);
            l.Remember = true;
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
                    return Json(new { success = true, ms = "Sua tai khoan thanh cong" });
                }
                else
                {
                    return Json(new { success = false, ms = "Sua khong thanh cong" });
                }
            }
        }
        [HttpGet]
        public JsonResult GetData(string username)
        {
            var ds = db.LayTaiKhoanTheoMa(username);
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string username)
        {
            if (db.Delete(username))
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