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
    public partial class Time_Group_List : System.Web.UI.Page
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
                        header.Text = "Attendance Time Group List";
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
                    getBinddata();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Time_Group_List 52: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }


        private void getBinddata()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dttimegrpDetails = objmysqldb.GetData("SELECT Group_id,Group_Name,Absent_SMS_After,OutPuch_SMS_After,Changes_Applicable_Date,DATE_FORMAT(DATE_ADD('0001-01-01 00:00:00',INTERVAL Changes_Applicable_Date/10000000 SECOND_MICROSECOND), '%d/%m/%Y')as date FROM group_master where IsDelete=0");
                if (dttimegrpDetails != null)
                {

                    grd.DataSource = dttimegrpDetails;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Time_Group_List 79: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
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
                DataTable dttimegrpDetails = objmysqldb.GetData("SELECT Group_id,Group_Name,Absent_SMS_After,OutPuch_SMS_After,Changes_Applicable_Date,DATE_FORMAT(DATE_ADD('0001-01-01 00:00:00',INTERVAL Changes_Applicable_Date/10000000 SECOND_MICROSECOND), '%d/%m/%Y')as date FROM group_master where IsDelete=0");
                objmysqldb.disposeConnectionObj();
                if (dttimegrpDetails != null && dttimegrpDetails.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dttimegrpDetails, "List_Of_Attendance_Time_Group_Details");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Time_Group_List 118: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }


        protected void btnadd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("~/Manage_TimeGroup.aspx?TimegrpId=0", false);
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Time_Group_List 131: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();

                int grp_id = 0;
                if (e.CommandName == "Edit")
                {
                    int.TryParse(e.CommandArgument.ToString(), out grp_id);
                    if (grp_id > 0)
                    {
                        Response.Redirect("~/Manage_TimeGroup.aspx?TimegrpId=" + grp_id + "", false);
                    }
                }
                else if (e.CommandName == "del")
                {

                    int.TryParse(e.CommandArgument.ToString(), out grp_id);
                    DateTime currenttimes = Logger.getIndiantimeDT();
                    DataTable dttimegrpDetails = objmysqldb.GetData("SELECT emp_id from time_group_assign_emplyee_wise where Group_id=" + grp_id + " and IsDelete=0 ");
                    if (dttimegrpDetails != null && dttimegrpDetails.Rows.Count == 0)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        objmysqldb.OpenSQlConnection();
                        string query = "update group_master set IsDelete=1,modify_datetime=" + currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_id + "  where Group_id=" + grp_id + "";
                        int res = objmysqldb.InsertUpdateDeleteData(query);

                        query = "update employee_punchtime_details_datewise set IsDelete=1,modify_datetime=" + currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_id + "  where time_grp_id=" + grp_id + "";
                        res = objmysqldb.InsertUpdateDeleteData(query);

                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";
                            Logger.WriteCriticalLog("Time_Group_List 170 Update error.");
                        }
                        else
                        {
                            grd.EditIndex = -1;
                            getBinddata();
                            ltrErr.Text = "Attendance Time Group Deleted Successfully.";
                        }
                    }
                    else
                    {
                        ltrErr.Text = "Employee Assign to select Time Group so you can't delete it.";
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Time_Group_List 188: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
    }
}