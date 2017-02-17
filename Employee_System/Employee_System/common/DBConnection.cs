using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


public class DBConnection
{
    #region "Variable Declaration"
    SqlConnection _con;
    #endregion

    #region "Property Declaration"
    public SqlConnection con
    {
        get
        {
            return _con;
        }
    }
    #endregion

    public DBConnection()
    {
        connection();
    }

    public void connection()
    {
        _con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeConnectionString"].ToString());
    }

    public void open()
    {
        con.Open();
    }

    public void close()
    {
        con.Close();
    }
}