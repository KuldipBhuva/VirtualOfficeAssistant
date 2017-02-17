using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security;

namespace Employee_System.Reports
{
    public class ReportData
    {

        SqlDataAdapter sdp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable objdt = new DataTable();
        DataSet objds = new DataSet();
        SqlConnection con;
        DBConnection objdb;

        public string Action { get; set; }
        public int days { get; set; }
        public int Comp_id { get; set; }
        public int Ptype { get; set; }
        
        public ReportData()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeConnectionString"].ToString());
        }
        [assembly:AllowPartiallyTrustedCallers]
        public DataTable ReportList()
        {
            try
            {
                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_ExpirydateReports";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
                // cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Parameters.Add(new SqlParameter("@days", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, days));
                cmd.Parameters.Add(new SqlParameter("@Comp_id", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comp_id));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
        [assembly: AllowPartiallyTrustedCallers]
        public DataTable Reportalldatabind()
        {
            try
            {

                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "Sp_Databind";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;




               
               



            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable ReportListPassport()
        {
            try
            {
                               
                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_PassportExpirydateReports";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
               // cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Parameters.Add(new SqlParameter("@days", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, days));
                cmd.Parameters.Add(new SqlParameter("@Comp_id", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comp_id));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }


        }

        public DataTable ReportListHealthcard()
        {
            try
            {

                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_HealthcardExpirydateReports";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
                // cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Parameters.Add(new SqlParameter("@days", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, days));
                cmd.Parameters.Add(new SqlParameter("@Comp_id", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comp_id));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable ReportListinsurance()
        {
            try
            {

                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_insuranceRpt";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
                // cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Parameters.Add(new SqlParameter("@days", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, days));
                cmd.Parameters.Add(new SqlParameter("@Comp_id", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comp_id));
                cmd.Parameters.Add(new SqlParameter("@type", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Ptype));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable ReportListworkpermitt()
        {
            try
            {

                objdb = new DBConnection();
                objdt = new DataTable();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_WorkpermittReports";
                cmd.CommandType = CommandType.StoredProcedure;
                sdp = new SqlDataAdapter(cmd);
                objdb.open();
                // cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Action));
                cmd.Parameters.Add(new SqlParameter("@days", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, days));
                cmd.Parameters.Add(new SqlParameter("@Comp_id", SqlDbType.Int, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comp_id));
                cmd.Connection = objdb.con;
                sdp.Fill(objdt);
                return objdt;


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }

    }

}






//    public ReportData()
//    {
//        this.ReportParameters = new List<Parameter>();
//        this.DataParameters = new List<Parameter>();
//    }

//    public bool IsLocal { get; set; }
//    public string ReportName { get; set; }
//    public List<Parameter> ReportParameters { get; set; }
//    public List<Parameter> DataParameters { get; set; }
//}

//public class Parameter
//{
//    public string ParameterName { get; set; }
//    public string Value { get; set; }
//}

