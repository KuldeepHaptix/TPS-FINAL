using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;


namespace EmployeeManagement
{
    public partial class EmployeeGroupForReports : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        header.Text = "Employee Report's Group List";
                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/login.aspx", false);
                }

                if (!IsPostBack)
                {
                    bindgvdata();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeGroupForReports 53: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        private void bindgvdata()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                btnExport.Visible = false;
                DataTable dtdata = objmysqldb.GetData("SELECT report_grp_id,report_grp_name,emp_ids FROM report_group_list where IsDelete=0");
                DataTable dtempdata = objmysqldb.GetData("SELECT EmpId,EmpFirstName FROM employee_master where IsDelete=0 and EmpStatusFlag=0");

                if (dtdata != null && dtdata.Rows.Count > 0 && dtempdata != null && dtempdata.Rows.Count > 0)
                {
                    dtdata.Columns.Add("emp_name");
                    for (int i = 0; i < dtdata.Rows.Count; i++)
                    {
                        string empids = dtdata.Rows[i]["emp_ids"].ToString();
                        if (!empids.Trim().Equals(""))
                        {
                            DataRow[] drdata = dtempdata.Select("EmpId IN (" + empids + ")");
                            string emp_name = "";
                            foreach (DataRow dr in drdata)
                            {
                                emp_name += dr["EmpFirstName"].ToString() + ",";
                            }
                            emp_name = emp_name.TrimEnd(',');
                            dtdata.Rows[i]["emp_name"] = emp_name;
                        }
                    }
                    ViewState["gvdata"] = dtdata;
                    grd.DataSource = dtdata;
                    grd.DataBind();

                    btnExport.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeGroupForReports 99: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                int reportgrpid = 0;
                int.TryParse(e.CommandArgument.ToString(), out reportgrpid);
                if (e.CommandName == "Edit")
                {
                    if (reportgrpid > 0)
                    {
                        Response.Redirect("~/ManageEmployeeGroupReports.aspx?repgrp=" + reportgrpid + "", false);
                    }
                }
                else if (e.CommandName == "del")
                {
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    DateTime currenttime = Logger.getIndiantimeDT();
                    string query = "Update report_group_list set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + reportgrpid + " ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";
                        Logger.WriteCriticalLog("EmployeeGroupForReports 131 Update error.");
                    }
                    else
                    {
                        ltrErr.Text = "Report Group Delete Successfully";
                    }
                    grd.EditIndex = -1;
                    bindgvdata();
                }
                else if (e.CommandName == "Img")
                {
                    if (reportgrpid > 0)
                    {
                        Response.Redirect("~/ManageEmployeeGroupReportsHeaderImage.aspx?repgrp=" + reportgrpid + "", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeGroupForReports 150:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtgrpList = (DataTable)ViewState["gvdata"];
                objmysqldb.disposeConnectionObj();
                if (dtgrpList != null && dtgrpList.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtgrpList, "List_Of_Reports_Group");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeGroupForReports 196: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btngrp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/ManageEmployeeGroupReports.aspx?repgrp=0", false);
        }
    }
}