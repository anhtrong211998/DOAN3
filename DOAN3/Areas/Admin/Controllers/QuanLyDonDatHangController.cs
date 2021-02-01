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
    public class QuanLyDonDatHangController : HomeAdminController
    {
        // GET: Admin/QuanLyDonDatHang
        public DonDatHangModel db = new DonDatHangModel();
        public QuanLyDonHangBus nhb = new QuanLyDonHangBus();
        public QuanLySanPhamBus ssp = new QuanLySanPhamBus();
        public ActionResult DanhSachHDChuaXacNhan()
        {
            var lsp = ssp.LayDanhSachSanPham();
            ViewBag.MaSP = new SelectList(lsp, "MaSP", "MaSP");
            return View();
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pagesize, string searchName)
        {
            var ds = db.LayDSDHChuaXacNan();

            int totalrow = ds.Count();
            int sl = totalrow;
            var model = ds.Skip((page - 1) * pagesize).Take(pagesize);
            if (!string.IsNullOrEmpty(searchName))
            {
                model = ds.FindAll(x => x.MaDonDatHang.ToLower().Contains(searchName));
                totalrow = model.Count();
            }
            return Json(new { data = model, total = totalrow, sl = sl }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult NhapHang(string mankh, string uudai, string masp, int soluong, int dongia)
        {
            int tongtien = soluong * dongia;
            DonDatHang sp = new DonDatHang();
            //sp.MaPhieuNhap = mapn;
            sp.MaKH = mankh;
            sp.UuDai =uudai;
            sp.ThanhTien = tongtien;
            List<ChiTietDonDatHang> lct = new List<ChiTietDonDatHang>();
            ChiTietDonDatHang pn = new ChiTietDonDatHang();
            pn.MaSP = masp;
            pn.SoLuong = soluong;
            pn.DonGia = dongia;
            var ds = ssp.LaySanPham(masp);
            pn.TenSP = ds.TenSP;
           
            lct.Add(pn);
            QuanLyDonHangBus mhb = new QuanLyDonHangBus();
            mhb.DatHangDon(sp, lct);
            return Json(new { success = true, ms = "thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string strPN, int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DonDatHang l = serializer.Deserialize<DonDatHang>(strPN);
            //if (id == 0)
            //{
            //    if (db.Insert(l))
            //    {
            //        return Json(new { success = true, ms = "Them thanh cong" });
            //    }
            //    else
            //    {
            //        return Json(new { succes = false, ms = "Them khong thanh cong" });
            //    }
            //}
            //else
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