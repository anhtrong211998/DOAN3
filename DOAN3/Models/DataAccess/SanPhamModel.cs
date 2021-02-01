using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class SanPhamModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        DataTable dt;
        public List<SanPham> LayDsSanPham()
        {
            dt = db.LayDuLieu("select * from SanPham");
            List<SanPham> ds = new List<SanPham>();
            foreach(DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDSSPNenMua()
        {
            dt = db.LayDuLieu("select top 20 * from SanPham order by NgayCapNhat desc");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDSSPGiamGia()
        {
            dt = db.LayDuLieu("select top 10 * from SanPham order by GiamGia desc");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDSSPMoi()
        {
            dt = db.LayDuLieu("select top 5 * from SanPham order by NgayCapNhat asc");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LaySanPhamBanChay(int so, string ngaythang)
        {
            dt = db.StoreReader("LaySPBANCHAY", so, ngaythang);
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDsSanPhamTheoLoai(string maL)
        {
            dt = db.LayDuLieu("select * from SanPham where MaLoaiSP='"+maL+"'");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDsSanPhamTheoNCC(string maNCC)
        {
            dt = db.LayDuLieu("select * from SanPham where MaNCC='" + maNCC + "'");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public List<SanPham> LayDsSanPhamTheoNSX(string maNSX)
        {
            dt = db.LayDuLieu("select * from SanPham where MaNSX='" + maNSX + "'");
            List<SanPham> ds = new List<SanPham>();
            foreach (DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = Convert.ToString(r[0]);
                sp.TenSP = Convert.ToString(r[1]);
                sp.MoTa = Convert.ToString(r[2]);
                sp.DonGia = Convert.ToInt32(r[3]);
                sp.GiamGia = Convert.ToInt32(r[4]);
                sp.HinhAnh = Convert.ToString(r[5]);
                sp.SoLuongTon = Convert.ToInt32(r[6]);
                sp.NgayCapNhat = Convert.ToDateTime(r[7]);
                sp.MaNCC = Convert.ToString(r[8]);
                sp.MaNSX = Convert.ToString(r[9]);
                sp.MaLoaiSP = Convert.ToString(r[10]);
                ds.Add(sp);
            }
            return ds;
        }
        public SanPham LaySanPhamTheoMa(string MaSP)
        {
            dt = db.LayDuLieu("select * from SanPham where MaSP='" + MaSP + "'");
            SanPham sp = new SanPham();
            if (dt.Rows.Count > 0)
            {
                sp.MaSP = Convert.ToString(dt.Rows[0][0]);
                sp.TenSP = Convert.ToString(dt.Rows[0][1]);
                sp.MoTa = Convert.ToString(dt.Rows[0][2]);
                sp.DonGia = Convert.ToInt32(dt.Rows[0][3]);
                sp.GiamGia = Convert.ToInt32(dt.Rows[0][4]);
                sp.HinhAnh = Convert.ToString(dt.Rows[0][5]);
                sp.SoLuongTon = Convert.ToInt32(dt.Rows[0][6]);
                sp.NgayCapNhat = Convert.ToDateTime(dt.Rows[0][7]);
                sp.MaNCC = Convert.ToString(dt.Rows[0][8]);
                sp.MaNSX = Convert.ToString(dt.Rows[0][9]);
                sp.MaLoaiSP = Convert.ToString(dt.Rows[0][10]);
            }
            else sp = null;
            return sp;
        }
        public Boolean Insert(SanPham sp)
        {
            return db.ExcuteNonQuery("insert into SanPham values('" + sp.MaSP + "',N'" + sp.TenSP + "',N'" + sp.MoTa + "','" + sp.DonGia + "','" + sp.GiamGia + "',N'" + sp.HinhAnh + "','" + sp.SoLuongTon + "','" + sp.NgayCapNhat + "','" + sp.MaNCC + "','" + sp.MaNSX + "','" + sp.MaLoaiSP + "')");
        }
        public Boolean Update(SanPham sp)
        {
            return db.ExcuteNonQuery("update SanPham set TenSP=N'" + sp.TenSP + "', MoTa= N'" + sp.MoTa + "', DonGia= '" + sp.DonGia + "', GiamGia= '" + sp.GiamGia + "', HinhAnh=N'" + sp.HinhAnh + "', SoLuongTon= '" + sp.SoLuongTon + "', NgayCapNhat= '" + sp.NgayCapNhat + "', MaNCC= '" + sp.MaNCC + "', MaNSX= '" + sp.MaNSX + "', MaLoaiSP= '" + sp.MaLoaiSP + "' where MaSP='" + sp.MaSP + "'");
        }
        public Boolean XoaSanPham(string masp)
        {

            return db.ExcuteNonQuery("delete SanPham where MaSP='" + masp + "'");
        }
    }
}