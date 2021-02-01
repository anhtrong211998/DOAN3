using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class NhanVienModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<NhanVien> LayDSNhanVien()
        {
            dt = db.LayDuLieu("select * from NhanVien");
            List<NhanVien> ds = new List<NhanVien>();
            foreach (DataRow r in dt.Rows)
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = Convert.ToString(r[0]);
                nv.TenNV = Convert.ToString(r[1]);
                nv.HinhAnh = Convert.ToString(r[2]);
                nv.ChucVu = Convert.ToString(r[3]);
                nv.DiaChi = Convert.ToString(r[4]);
                nv.Email = Convert.ToString(r[5]);
                nv.SoDienThoai = Convert.ToString(r[6]);
                nv.TaiKhoan = Convert.ToString(r[7]);
                ds.Add(nv);
            }
            return ds;
        }
        public NhanVien LayNhanVien(string taikhoan)
        {
            dt = db.LayDuLieu("select MaNV,TenNV,HinhAnh,TaiKhoan from NhanVien where TaiKhoan='"+taikhoan+"'");
            NhanVien sp = new NhanVien();
            if (dt.Rows.Count > 0)
            {
                sp.MaNV = Convert.ToString(dt.Rows[0][0]);
                sp.TenNV = Convert.ToString(dt.Rows[0][1]);
                sp.HinhAnh = Convert.ToString(dt.Rows[0][2]);
                //sp.ChucVu = Convert.ToString(dt.Rows[0][3]);
                //sp.DiaChi = Convert.ToString(dt.Rows[0][4]);
                //sp.Email = Convert.ToString(dt.Rows[0][5]);
                //sp.SoDienThoai = Convert.ToString(dt.Rows[0][6]);
                sp.TaiKhoan = Convert.ToString(dt.Rows[0][3]);
            }
            else
                sp = null;
            return sp;
        }
        public NhanVien LayNhanVienTheoma(string ma)
        {
            dt = db.LayDuLieu("select MaNV,TenNV,HinhAnh,TaiKhoan from NhanVien where MaNV='" + ma + "'");
            NhanVien sp = new NhanVien();
            if (dt.Rows.Count > 0)
            {
                sp.MaNV = Convert.ToString(dt.Rows[0][0]);
                sp.TenNV = Convert.ToString(dt.Rows[0][1]);
                sp.HinhAnh = Convert.ToString(dt.Rows[0][2]);
                //sp.ChucVu = Convert.ToString(dt.Rows[0][3]);
                //sp.DiaChi = Convert.ToString(dt.Rows[0][4]);
                //sp.Email = Convert.ToString(dt.Rows[0][5]);
                //sp.SoDienThoai = Convert.ToString(dt.Rows[0][6]);
                sp.TaiKhoan = Convert.ToString(dt.Rows[0][3]);
            }
            else
                sp = null;
            return sp;
        }
        public Boolean Insert(NhanVien nv)
        {
            return db.ExcuteNonQuery("insert into NhanVien values('" + nv.MaNV + "',N'" + nv.TenNV + "',N'" + nv.HinhAnh + "',N'" + nv.ChucVu + "',N'" + nv.DiaChi + "',N'" + nv.Email + "','" + nv.SoDienThoai + "','" + nv.TaiKhoan + "')");
        }
        public Boolean InsertNULL(NhanVien nv)
        {
            return db.ExcuteNonQuery("insert into NhanVien values('" + nv.MaNV + "',N'" + nv.TenNV + "',N'" + nv.HinhAnh + "',N'" + nv.ChucVu + "',N'" + nv.DiaChi + "',N'" + nv.Email + "','" + nv.SoDienThoai + "',NULL)");
        }
        public Boolean Update(NhanVien sx)
        {
            return db.ExcuteNonQuery("update NhanVien set TenNV=N'" + sx.TenNV + "',HinhAnh=N'" + sx.HinhAnh + "',ChucVu=N'" + sx.ChucVu + "',DiaChi=N'" + sx.DiaChi + "',Email=N'" + sx.Email + "',SoDienThoai='" + sx.SoDienThoai + "',TaiKhoan='" + sx.TaiKhoan + "' where MaNV='" + sx.MaNV + "'");
        }
        public Boolean UpdateNULL(NhanVien sx)
        {
            return db.ExcuteNonQuery("update NhanVien set TenNV=N'" + sx.TenNV + "',HinhAnh=N'" + sx.HinhAnh + "',ChucVu=N'" + sx.ChucVu + "',DiaChi=N'" + sx.DiaChi + "',Email=N'" + sx.Email + "',SoDienThoai='" + sx.SoDienThoai + "',TaiKhoan=NULL where MaNV='" + sx.MaNV + "'");
        }
        public Boolean Delete(string manv)
        {
            return db.ExcuteNonQuery("delete NhanVien where MaNV='" + manv + "'");
        }
    }
}