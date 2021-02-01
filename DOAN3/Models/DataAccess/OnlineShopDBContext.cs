using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace DOAN3.Models.DataAccess
{
    public class OnlineShopDBContext
    {
        public string stcon = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        public SqlConnection con;
        //khoi tao khong tham so 
        public OnlineShopDBContext()
        {
            con = new SqlConnection(stcon);
        }
        //Khoi tao co tham so voi ket noi thuong
        public OnlineShopDBContext(string SV,string DN)
        {
            stcon = @"server=" + SV + "; database=" + DN + "; Integrated security=true";
            con = new SqlConnection(stcon);
        }
        //khoi tao co tham so voi ket noi sa hoac tai khoan
        public OnlineShopDBContext(string SV, string DN,string UN,string PW)
        {
            stcon = @"server=" + SV + "; database=" + DN + "; UId=" + UN + "; Pwd=" + PW;
            con = new SqlConnection(stcon);
        }
        //Phuong thuc loc du lieu
        public DataView LocDuLieu(DataTable dt,string dk)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = dk;
            return dv;
        }
        //Phuong thuc mo ket noi 
        public void MoKetNoi()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        //Phuong thuc dong ket noi 
        public void DongKetNoi()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }
        //thuc thi cau lenh truy van hanh dong sql
        public bool ExcuteNonQuery(string sql)
        {
            try
            {
                MoKetNoi();
                SqlCommand cm = new SqlCommand(sql, con);
                cm.ExecuteNonQuery();
                DongKetNoi();
                return true;
            }
            catch { return false; }
        }
        //thuc thi cau lenh truy van sql
        public SqlDataReader ExcuteReader(string sql)
        {
            MoKetNoi();
            SqlCommand cm = new SqlCommand(sql, con);
            SqlDataReader sl = cm.ExecuteReader();
            //DongKetNoi();
            return sl;
        }
        //phuong thuc lay du lieu + phan trang 
        public DataTable FillDataTablePage(string sql, int pnum, int psize)
        {
            //b1: Khai báo đối tượng DataTable
            DataTable dt = new DataTable();
            //B2. Sử dụng đối tượng SqlDataAdapter, truyền vào câu lệnh truy vấn select, thông tin kết nối
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            // Gọi phương thức Fill để điền dữ liệu vào DataTable.
            da.Fill((pnum - 1) * psize, psize, dt);
            // Trả về kết quả   
            return dt;
        }
        //phuong thuc lay du lieu tu csdl
        public DataTable LayDuLieu(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        //Them mot dong vao databse
        public void InsertData(DataTable dt, params object[] Value)
        {
            // Tạo ra một dòng có cấu trúc giống bảng
            DataRow dr = dt.NewRow();
            // Gan gia tri cho tung truong
            for (int i = 0; i < Value.Length; i++)
            {
                dr[i] = Value[i];   // Gán giá trị cho các trường 
            } //them dong vao bang
            dt.Rows.Add(dr);
        }
        //Sua thong tin mot dong trong database
        public void UpdateRowTable(DataTable dt, params object[] NV)
        {
            // Duyệt qua các row trong DataTable
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                if (dt.Rows[d][0].ToString() == NV[0].ToString())
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // Sửa thông tin của từng trường trong Row
                        dt.Rows[d][i] = NV[i];
                    }
                    break;
                }
            }
        }
        public void UpadateRowTableV(DataTable dt, string dieukien, params object[] gtm)
        {
            DataView dv = LocDuLieu(dt, dieukien);
            if (dv.Count > 0)
            {
                for (int i = 0; i < gtm.Length; i++)
                    dv[0][i] = gtm[i];
            }
        }
        public void UpdateRowTabldeV(DataTable dt, params object[] NV)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "MaNV='" + NV[0].ToString() + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                for (int i = 0; i < dv.ToTable().Columns.Count; i++)
                {
                    dv[0][i] = NV[i];
                }
            }
        }
        //Xoa du lieu trong database
        public void DeleteDataTable(DataTable dt, string Ma)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == Ma)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }

            }

        }
        //lam viec voi store trong database
        public void StoreNonQuery(string tenStore, params object[] giatri)
        {
            MoKetNoi();
            SqlCommand cm;
            try
            {
                cm = new SqlCommand(tenStore, con);
                cm.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cm);
                for(int i = 1; i < cm.Parameters.Count; i++)
                {
                    cm.Parameters[i].Value = giatri[i - 1];
                }
                cm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable StoreReader(string tenStore, params object[] giatri)
        {
            DataTable dt = new DataTable();
            SqlCommand cm;
            MoKetNoi();
            try
            {
                cm = new SqlCommand(tenStore, con);
                cm.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cm);
                for(int i = 1; i < cm.Parameters.Count; i++)
                {
                    cm.Parameters[i].Value = giatri[i - 1];

                }
                new SqlDataAdapter(cm).Fill(dt);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        //cap nhat nhieu bang
        public Boolean UpdateDataBase(DataTable dt,string tenbang)
        {
            try
            {
                //b1: xac dinh nguon du lieu can cap nhat
                SqlDataAdapter da;
                da = new SqlDataAdapter("select * from " + tenbang, con);
                //b2 khai bao doi tuong dong bo du lieu
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                //b3 cap nhat du lieu
                da.Update(dt);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void DocTep(String tentep, out string Quyen, out string SN, out String DN, out String UN, out String PW)
        {
            StreamReader dr = new StreamReader(tentep);
            if (!dr.EndOfStream)
                Quyen = dr.ReadLine();
            else
                Quyen = "";

            if (!dr.EndOfStream)
                SN = dr.ReadLine();
            else
                SN = "";

            if (!dr.EndOfStream)
                DN = dr.ReadLine();
            else
                DN = "";

            if (!dr.EndOfStream)
                UN = dr.ReadLine();
            else
                UN = "";


            if (!dr.EndOfStream)
                PW = dr.ReadLine();
            else
                PW = "";
            dr.Close();

        }
        public static void GhiTep(String tentep, string Quyen, string SN, String DN, String UN, String PW)
        {
            StreamWriter dw = new StreamWriter(tentep);
            dw.WriteLine(Quyen);
            dw.WriteLine(SN);
            dw.WriteLine(DN);
            dw.WriteLine(UN);
            dw.WriteLine(PW);
            dw.Close();
        }
    }

}