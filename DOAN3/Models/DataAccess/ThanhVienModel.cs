using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class ThanhVienModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<ThanhVien> LayDSThanhVien()
        {
            dt = db.LayDuLieu("select * from ThanhVien");
            List<ThanhVien> ds = new List<ThanhVien>();
            foreach (DataRow r in dt.Rows)
            {
                ThanhVien nv = new ThanhVien();
                nv.Email = Convert.ToString(r[0]);
                nv.MatKhau = Convert.ToString(r[1]);
                nv.HoTen = Convert.ToString(r[2]);
                nv.DiaChi = Convert.ToString(r[3]);
                nv.SoDienThoai = Convert.ToString(r[4]);               
                ds.Add(nv);
            }
            return ds;
        }
        public ThanhVien LayThanhVienTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from ThanhVien where Email='"+ma+"'");
            ThanhVien nv = new ThanhVien();
            if (dt.Rows.Count > 0)
            {

                nv.Email = Convert.ToString(dt.Rows[0][0]);
                nv.MatKhau = Convert.ToString(dt.Rows[0][1]);
                nv.HoTen = Convert.ToString(dt.Rows[0][2]);
                nv.DiaChi = Convert.ToString(dt.Rows[0][3]);
                nv.SoDienThoai = Convert.ToString(dt.Rows[0][4]);
            }
            else nv = null;
            return nv;
        }
        public Boolean Insert(ThanhVien nv)
        {
            return db.ExcuteNonQuery("insert into ThanhVien values(N'" + nv.Email + "',N'" + nv.MatKhau + "',N'" + nv.HoTen + "',N'" + nv.DiaChi + "','" + nv.SoDienThoai + "')");
        }
        public Boolean Update(ThanhVien sx)
        {
            return db.ExcuteNonQuery("update ThanhVien set MatKhau=N'" + sx.MatKhau + "',HoTen=N'" + sx.HoTen + "',DiaChi=N'" + sx.DiaChi + "',SoDienThoai='" + sx.SoDienThoai + "' where Email='" + sx.Email + "'");
        }
        public Boolean Delete(string makh)
        {
            return db.ExcuteNonQuery("delete ThanhVien where Email='" + makh + "'");
        }
        public List<object> CheckTaiKhoan(string username, string password)
        {
            List<object> ls = new List<object>();
            dt = db.LayDuLieu("select * from ThanhVien where Email='" + username + "'");
            if (dt.Rows.Count <= 0)
            { ls.Add(0); }
            else
            {
                    if (Convert.ToString(dt.Rows[0][1]) == password)
                    {
                        ls.Add(1);
                        ThanhVien ctk = new ThanhVien();
                        ctk.Email = Convert.ToString(dt.Rows[0][0]);
                    ctk.HoTen = Convert.ToString(dt.Rows[0][2]);
                        ls.Add(ctk);
                    }
                    else { ls.Add(-1); }
            }
            return ls;
        }
        public List<object> QuenMatKhau(string username)
        {
            List<object> ls = new List<object>();
            dt = db.LayDuLieu("select * from ThanhVien where Email='" + username + "'");
            if (dt.Rows.Count <= 0)
            { ls.Add(0); }
            else
            {
                    ls.Add(1);
                    ThanhVienSession ctk = new ThanhVienSession();
                    ctk.Email = Convert.ToString(dt.Rows[0][0]);
                    ls.Add(ctk);
            }
            return ls;
        }
    }
}