using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DOAN3.Models.Entities;

namespace DOAN3.Models.DataAccess
{
    public class NhaCungCapModel
    {
        public OnlineShopDBContext db = new OnlineShopDBContext();
        public DataTable dt;
        public List<NhaCungCap> LayDSNhaCungCap()
        {
            dt = db.LayDuLieu("select * from NhaCungCap");
            List<NhaCungCap> ds = new List<NhaCungCap>();
            foreach (DataRow r in dt.Rows)
            {
                NhaCungCap sx = new NhaCungCap();
                sx.MaNCC = Convert.ToString(r[0]);
                sx.TenNCC = Convert.ToString(r[1]);
                sx.DiaChi = Convert.ToString(r[2]);
                sx.Email = Convert.ToString(r[3]);
                sx.SoDienThoai = Convert.ToString(r[4]);
                ds.Add(sx);
            }
            return ds;
        }
        public NhaCungCap LayNhaCungCapTheoMa(string ma)
        {
            dt = db.LayDuLieu("select * from NhaCungCap where MaNCC");
            NhaCungCap sx = new NhaCungCap();
            if (dt.Rows.Count > 0)
            {

                sx.MaNCC = Convert.ToString(dt.Rows[0][0]);
                sx.TenNCC = Convert.ToString(dt.Rows[0][1]);
                sx.DiaChi = Convert.ToString(dt.Rows[0][2]);
                sx.Email = Convert.ToString(dt.Rows[0][3]);
                sx.SoDienThoai = Convert.ToString(dt.Rows[0][4]);
            }
            else sx = null;
            return sx;
        }
        public Boolean Insert(NhaCungCap sx)
        {
            return db.ExcuteNonQuery("insert into NhaCungCap values('" + sx.MaNCC + "',N'" + sx.TenNCC + "',N'" + sx.DiaChi + "',N'" + sx.Email + "','" + sx.SoDienThoai + "')");
        }
        public Boolean Update(NhaCungCap sx)
        {
            return db.ExcuteNonQuery("update NhaCungCap set TenNCC=N'" + sx.TenNCC + "',DiaChi=N'" + sx.DiaChi + "',Email=N'" + sx.Email + "',SoDienThoai='" + sx.SoDienThoai + "' where MaNCC='" + sx.MaNCC + "'");
        }
        public Boolean Delete(string mancc)
        {
            return db.ExcuteNonQuery("delete NhaCungCap where MaNCC='" + mancc + "'");
        }
    }
}