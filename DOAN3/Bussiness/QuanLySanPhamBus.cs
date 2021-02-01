using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Bussiness
{
    public class QuanLySanPhamBus
    {
        public SanPhamModel db = new SanPhamModel();
        public List<SanPham> LayDanhSachSanPham()
        {
            var ds = db.LayDsSanPham();
            return ds;
        }
        public List<SanPham> LaySanPhamTheoLoai(string ma)
        {
            var ds = db.LayDsSanPhamTheoLoai(ma);
            return ds;
        }
        public List<SanPham> LaySanPhamTheoNCC(string ma)
        {
            var ds = db.LayDsSanPhamTheoNCC(ma);
            return ds;
        }
        public List<SanPham> LaySanPhamTheoNhaSanXuat(string ma)
        {
            var ds = db.LayDsSanPhamTheoNSX(ma);
            return ds;
        }
        public SanPham LaySanPham(string ma)
        {
            var ds = db.LaySanPhamTheoMa(ma);
            return ds;
        }
        public List<SanPham> LaySPMoi()
        {
            var ds = db.LayDSSPMoi();
            return ds;
        }
        public List<SanPham> LaySPGiamGia()
        {
            var ds = db.LayDSSPGiamGia();
            return ds;
        }
        public List<SanPham> LaySPNenmua()
        {
            var ds = db.LayDSSPNenMua();
            return ds;
        }
        public List<SanPham> LaySPBanChay(int so, string ngaythang)
        {
            var ds = db.LaySanPhamBanChay(so, ngaythang);
            return ds;
        }
        public Boolean Insert(SanPham sp)
        {
            return db.Insert(sp);
        }
        public Boolean Update(SanPham sp)
        {
            return db.Update(sp);
        }
        public Boolean Delete(string ma)
        {
            return db.XoaSanPham(ma);
        }
    }
}