using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Areas.Admin.Models;
using DOAN3.Areas.Admin.Models.Entities;
using DOAN3.Models.Entities;
using DOAN3.Common;

namespace DOAN3.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: Admin/Login
        public TaiKhoanModel db = new TaiKhoanModel();
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(TaiKhoan tk)
        {
            // var result = db.CheckAcount(l.UserName, Encrypter.MD5Hash(l.PassWord));


            if (ModelState.IsValid)
            {
                var lk = db.CheckTaiKhoan(tk.UserName, Encrypter.MD5Hash(tk.MatKhau));
                var result = Convert.ToInt32(lk[0]);
                switch (result)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;
                    case 1:

                        TaiKhoan ac = lk[1] as TaiKhoan;
                        Session.Add("User_Session", ac);
                        //SessionHelper.SetSession(new UserSession() { UserName = l.UserName });
                        return RedirectToAction("Index", "HomeAdmin", ac);
                    case -1:
                        ModelState.AddModelError("", "Tài khoản đang bị khóa");
                        break;
                    case -2:
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                        break;
                    default:
                        ModelState.AddModelError("", "User name và mật khẩu không đúng");
                        break;
                };
            }
            else ModelState.AddModelError("", "User name và mật khẩu không đúng");
            return View(tk);
        }
        [HttpGet]
        public ActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuenMatKhau(TaiKhoan tk)
        {
            // var result = db.CheckAcount(l.UserName, Encrypter.MD5Hash(l.PassWord));


            if (ModelState.IsValid)
            {
                var lk = db.QuenMatKhau(tk.UserName);
                var result = Convert.ToInt32(lk[0]);
                switch (result)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;
                    case 1:

                        matkhausesion acc = lk[1] as matkhausesion;
                        Session.Add("matkhau_Session", acc);
                        //SessionHelper.SetSession(new UserSession() { UserName = l.UserName });
                        return RedirectToAction("DoiMatKhau", "LoginAdmin", acc);
                    case -1:
                        ModelState.AddModelError("", "Tài khoản đang bị khóa");
                        break;
                    default:
                        ModelState.AddModelError("", "User name không đúng");
                        break;
                };
            }
            else ModelState.AddModelError("", "User name không đúng");
            return View(tk);
        }
        [HttpGet]
        public ActionResult TaotaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaotaiKhoan(string Username, string Password)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.UserName = Username;
            tk.MatKhau = Encrypter.MD5Hash(Password);
            tk.Remember = true;
            if (db.Insert(tk))
            {
                return Json(new { success = true, ms = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, ms = "Thêm khong thành công" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(string Password)
        {
            matkhausesion ds = (matkhausesion)Session["matkhau_Session"];
            TaiKhoan tk = new TaiKhoan();
            tk.UserName = ds.tentaikhoan;
            tk.MatKhau = Encrypter.MD5Hash(Password);
            tk.Remember = true;
            if (db.Update(tk))
            {
                return Json(new { success = true, ms = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, ms = "Thêm khong thành công" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DangXuat()
        {
            Session["User_Session"]=null; 
            return RedirectToAction("Dangnhap", "LoginAdmin");
        }
    }
}