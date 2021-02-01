using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Bussiness;
using DOAN3.Models.Entities;
using PagedList;

namespace DOAN3.Controllers
{
    public class XemSanphamController : Controller
    {
        // GET: XemSanpham
        public QuanLySanPhamBus db = new QuanLySanPhamBus();
        public ActionResult SanphamTheoloai(string ma,int?page)
        {
            int pagenumber = page ?? 1;
            var ds = db.LaySanPhamTheoLoai(ma);
            return View(ds.ToPagedList(pagenumber,8));
        }
        public ActionResult SanPhamTheoNhaSanXuat(string ma, int? page)
        {
            int pagenumber = page ?? 1;
            var ds = db.LaySanPhamTheoLoai(ma);
            return View(ds.ToPagedList(pagenumber, 8));
        }
        public ActionResult XemSanphamChitiet(string ma)
        {
            var ds = db.LaySanPham(ma);
            return View(ds);
        }
        public ActionResult KetQuaTimKiem(string search, int? page)
        {
            var ds = db.LayDanhSachSanPham();
            ds = ds.FindAll(x => x.TenSP.ToLower().Contains(search));
            int pagenumber = page ?? 1;
            return View(ds.ToPagedList(pagenumber, 8));
        }
        [HttpPost]
        public ActionResult BinhLuan(string noidungbl,string masp,int danhgia)
        {
            QuanLyBinhLuanBus dbb = new QuanLyBinhLuanBus();
            BinhLuan b = new BinhLuan();           
            ThanhVien tk = (ThanhVien)Session["UserThanhVien"];
            if (tk != null)
            {
                b.MaBL = "aaaa";
                b.NoiDungBL = noidungbl;
                b.MaThanhVien = tk.Email;
                b.MaSP = masp;
                b.DanhGia = danhgia;
                if (dbb.Insert(b))
                {
                    return Json(new { success = true, ms = "Them thanh cong" });
                }
            }
            return Json(new {success=false, ms="Bạn cần đăng nhập"},JsonRequestBehavior.AllowGet);
        }
    }
}