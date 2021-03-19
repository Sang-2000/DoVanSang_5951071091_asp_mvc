using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using DoVanSang_5951071091_asp_mvc.Controllers;

namespace DoVanSang_5951071091_asp_mvc.Models
{
    public class db
    {

        // ket noi database
        SqlConnection con = new SqlConnection(@"Data Source = DESKTOP - LDKJOLM\SQLEXPRESS; Initial Catalog = demoCRUD; User ID = sa;Password=123");


        // select du lieu
        public DataSet Empget(Employee emp, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                cmd.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                cmd.Parameters.AddWithValue("@City", emp.City);
                cmd.Parameters.AddWithValue("@STATE", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Flag", emp.Flag);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "OK";
                return ds;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return ds;
            }
        }


        // inssert / update du lieu
        public string Empdml(Employee emp, out string msg)
        {
            msg = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                cmd.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                cmd.Parameters.AddWithValue("@City", emp.City);
                cmd.Parameters.AddWithValue("@STATE", emp.State);
                cmd.Parameters.AddWithValue("@Country", emp.Country);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Flag", emp.Flag);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "OK";
                return msg;

            }
            catch (Exception e)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                else
                {
                    msg = e.Message;
                    
                }
            }
            return msg;

        }
    }
}
