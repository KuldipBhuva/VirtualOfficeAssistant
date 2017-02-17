using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Employee_System.Reports;
using Microsoft.Reporting.WebForms;

namespace Employee_System.ReportPage
{
    public partial class Rptworkpermitt : System.Web.UI.Page
    {
        ReportData rpt = new ReportData();
        DataTable objdt = new DataTable();
        DataTable objdt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dropbind();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            BindRDLC();
        }
        public void Dropbind()
        {
            try
            {
                rpt = new ReportData();
                objdt = new DataTable();
                objdt2 = new DataTable();

                rpt.Action = "Comapnydrp";
                // RD.JobCode = drpjob.SelectedValue;
                //   CM.Status = ddlstatussearch.SelectedValue;
                objdt = rpt.Reportalldatabind();
                if (objdt.Rows.Count > 0)
                {
                    //objdt = ds.Tables[0];
                    //objdt2 = ds.Tables[1];
                    drpcompany.DataSource = objdt;
                    drpcompany.DataValueField = "id";
                    drpcompany.DataTextField = "CompName";
                    drpcompany.DataBind();

                    drpcompany.Items.Insert(0, "-Select Company-");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void BindRDLC()
        {
            try
            {
                objdt = new DataTable();
                rpt = new ReportData();
                rpt.Comp_id = Convert.ToInt32(drpcompany.SelectedValue);
                rpt.days = Convert.ToInt32(txtdays.Text);
                objdt = rpt.ReportListworkpermitt();
                ReportDataSource Rds = new ReportDataSource();
                Rds.Name = "DataSet1";
                Rds.Value = objdt;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rpt_healthcard.rdlc");
                ReportViewer1.LocalReport.DataSources.Add(Rds);
                ReportViewer1.LocalReport.Refresh();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, UseHttpGet = true)]
        public void GetDashBoardItem(int iMonth, int iClientID)
        {
            BindRDLC();
        }

    }
}