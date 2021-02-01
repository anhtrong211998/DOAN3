using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.DataAccess;
using DOAN3.Models.Entities;
using DOAN3.Areas.Admin.Models.Entities;

namespace DOAN3.Areas.Admin.Models
{
    public class TaiKhoanModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<TaiKhoan> LayDSTaiKhoan()
        {
            dt = db.LayDuLieu("select * from TaiKhoan");
            List<TaiKhoan> ds = new List<TaiKhoan>();
            foreach (DataRow r in dt.Rows)
            {
                TaiKhoan l = new TaiKhoan();
                l.UserName = Convert.ToString(r[0]);
                l.MatKhau = Convert.ToString(r[1]);
                l.Remember = Convert.ToBoolean(r[2]);
                ds.Add(l);
            }
            return ds;
        }
        public TaiKhoan LayTaiKhoanTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from TaiKhoan where UserName='"+ma+"'");
            TaiKhoan l = new TaiKhoan();
            if (dt.Rows.Count > 0)
            {

                l.UserName = Convert.ToString(dt.Rows[0][0]);
                l.MatKhau = Convert.ToString(dt.Rows[0][1]);
                l.Remember = Convert.ToBoolean(dt.Rows[0][2]);
            }
            else l = null;
            return l;
        }
        public Boolean Insert(TaiKhoan l)
        {
            return db.ExcuteNonQuery("insert into TaiKhoan values('" + l.UserName + "','" + l.MatKhau + "','" + l.Remember + "')");
        }
        public Boolean Update(TaiKhoan l)
        {
            return db.ExcuteNonQuery("update TaiKhoan set MatKhau='" + l.MatKhau + "',Remember='" + l.Remember + "' where TaiKhoan='" + l.UserName + "'");
        }
        public Boolean Delete(string username)
        {
            return db.ExcuteNonQuery("delete TaiKhoan where TaiKhoan='" + username + "'");
        }
        public List<object> CheckTaiKhoan(string username, string password)
        {
            List<object> ls = new List<object>();
            dt = db.LayDuLieu("select * from TaiKhoan where TaiKhoan='" + username + "'");
            if (dt.Rows.Count <= 0)
            { ls.Add(0); }
            else
            {
                if (Convert.ToBoolean(dt.Rows[0][2]) == false)
                { ls.Add(-1); }
                else
                {
                    if (Convert.ToString(dt.Rows[0][1]) == password)
                    {
                        ls.Add(1);
                        TaiKhoan ctk = new TaiKhoan();
                        ctk.UserName = Convert.ToString(dt.Rows[0][0]);
                        ls.Add(ctk);
                    }
                    else { ls.Add(-2); }
                }
            }
            return ls;
        }
        public List<object> QuenMatKhau(string username)
        {
            List<object> ls = new List<object>();
            dt = db.LayDuLieu("select * from TaiKhoan where TaiKhoan='" + username + "'");
            if (dt.Rows.Count <= 0)
            { ls.Add(0); }
            else
            {
                if (Convert.ToBoolean(dt.Rows[0][2]) == false)
                { ls.Add(-1); }
                else
                {
                    ls.Add(1);
                    matkhausesion ctk = new matkhausesion();
                    ctk.tentaikhoan = Convert.ToString(dt.Rows[0][0]);
                    ls.Add(ctk);
                }
            }
            return ls;
        }
    }
}