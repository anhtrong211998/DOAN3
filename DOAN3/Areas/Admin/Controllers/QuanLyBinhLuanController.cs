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
    public class QuanLyBinhLuanController : HomeAdminController
    {
        // GET: Admin/QuanLyBinhLuan
        public QuanLyBinhLuanBus db = new QuanLyBinhLuanBus();
        QuanLyThanhVienBus tvb = new QuanLyThanhVienBus();
        public ActionResult DanhSachBinhLuan()
        {
            //var ds = tvb.LayDsThanhVien();
            //ViewBag.Email = new SelectList(ds, "Email", "HoTen");
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDsBinhLuan();
            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(n => n.MaSP.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strNhanvien,string danhgia, int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BinhLuan nv = serializer.Deserialize<BinhLuan>(strNhanvien);
            nv.DanhGia = int.Parse(danhgia);
            if (id == 0)
            {
                if (nv.MaThanhVien == "" && nv.MaSP=="")
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
                if (nv.MaThanhVien == "" && nv.MaSP == "")
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
            var ds = db.LayBinhLuanTheoMa(manv);
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