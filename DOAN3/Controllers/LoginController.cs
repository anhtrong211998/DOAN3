using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;
using DOAN3.Models.DataAccess;
using DOAN3.Common;


namespace DOAN3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ThanhVienModel db = new ThanhVienModel();
        public ActionResult DangNhapNguoiDung()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapNguoiDung(ThanhVien tk)
        {
            if (ModelState.IsValid)
            {
                var lk = db.CheckTaiKhoan(tk.Email, Encrypter.MD5Hash(tk.MatKhau));
                var result = Convert.ToInt32(lk[0]);
                switch (result)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        break;
                    case 1:

                        ThanhVien ac = lk[1] as ThanhVien;
                        Session.Add("UserThanhVien", ac);
                        //SessionHelper.SetSession(new UserSession() { UserName = l.UserName });
                        return RedirectToAction("Index", "Home", ac);
                    case -1:
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
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(string email,string matkhau, string hoten, string sodienthoai, string diachi)
        {
            ThanhVien tv = new ThanhVien();
            tv.Email = email;
            tv.MatKhau = Encrypter.MD5Hash(matkhau);
            tv.HoTen = hoten;
            tv.SoDienThoai = sodienthoai;
            tv.DiaChi = diachi;
            if (db.Insert(tv))
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
        public ActionResult FogotPass()
        {
            return View();
        }
    }
}