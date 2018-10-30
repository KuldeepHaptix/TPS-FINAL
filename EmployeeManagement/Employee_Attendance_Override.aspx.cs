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
    public partial class Employee_Attendance_Override : System.Web.UI.Page
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
                        header.Text = "EmployeeWise Attendance OverWrite";
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

                    BindEmployeeList();
                    btnsave.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 54: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        private void BindEmployeeList()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtemplist = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName FROM employee_master  where  EmpStatusFlag=0 and employee_master.IsDelete=0");

                ddlemplist.DataSource = dtemplist;
                ddlemplist.DataTextField = "FullName";
                ddlemplist.DataValueField = "EmpId";
                ddlemplist.DataBind();
                ddlemplist.Items.Insert(0, new ListItem("Select Reporting Manager", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 74: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void btnseacrh_Click(object sender, EventArgs e)
        {
            try
            {
                string[] date = hdnDate.Value.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (ddlemplist.SelectedIndex > 0 && date.Length == 2)
                {
                    bindGvdata(date);
                    lblDate.Text = hdnDate.Value.ToString();
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    ltrErr.Text = "Please Select Valid Date And Employee";
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 54: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        private void bindGvdata(string[] date)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                int Emp_id = 0;
                int.TryParse(ddlemplist.Items[ddlemplist.SelectedIndex].Value.ToString(), out Emp_id);
                string[] startdate = date[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                string[] EndDate = date[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                long StartTicks = new DateTime(int.Parse(startdate[2]), int.Parse(startdate[1]), int.Parse(startdate[0])).Ticks;
                long Endticks = new DateTime(int.Parse(EndDate[2]), int.Parse(EndDate[1]), int.Parse(EndDate[0])).Ticks;
                btnsave.Visible = false;
                if (StartTicks > 0 && Endticks > 0 && StartTicks <= Endticks)
                {
                    DataTable dtattendanceData = objmysqldb.GetData("select Type,Overlap,if(Overlap=1,Update_Type,Type)as Update_Type,if(Type=1,'Present',if(type=2,'Half Day','Absent')) as Prev_Status, DATE_FORMAT(DATE_ADD('0001-01-01 00:00:00',INTERVAL DateTicks/10000000 SECOND_MICROSECOND), '%d/%m/%Y')as date from employee_attendance_daily where emp_id=" + Emp_id + " and DateTicks >=" + StartTicks + "  and DateTicks <=" + Endticks + " and IsDelete=0 ");

                    if(dtattendanceData != null && dtattendanceData.Rows.Count > 0)
                    {
                        grd.DataSource = dtattendanceData;
                        grd.DataBind();
                        btnsave.Visible = true;
                    }
                    else
                    {
                        grd.DataSource = dtattendanceData;
                        grd.DataBind();
                        ltrErr.Text = "No record exsits for selected criteria";
                    }
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    ltrErr.Text = "Please Select Valid Date";
                    return;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 54: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    //Find the DropDownList in the Row
                    HtmlSelect ddlstatus = (e.Row.FindControl("ddlstatus") as HtmlSelect);
                    if (ddlstatus != null)
                    {
                        DataTable dtstatus = new DataTable();
                        dtstatus.Columns.Add("id");
                        dtstatus.Columns.Add("name");
                        dtstatus.Rows.Add("1", "Present");
                        dtstatus.Rows.Add("2", "Half Day");
                        dtstatus.Rows.Add("3", "Absent");

                        ddlstatus.DataSource = dtstatus;
                        ddlstatus.DataTextField = "name";
                        ddlstatus.DataValueField = "id";
                        ddlstatus.DataBind();

                        string rel = (e.Row.FindControl("lblnewType") as Label).Text;
                        ddlstatus.Items.FindByValue(rel).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 420: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                objmysqldb.OpenSQlConnection();
                objmysqldb.BeginSQLTransaction();
                int Emp_id = 0;
                int.TryParse(ddlemplist.Items[ddlemplist.SelectedIndex].Value.ToString(), out Emp_id);
                foreach (GridViewRow row in grd.Rows)
                {
                    Label date = (Label)row.FindControl("lbldate");
                    Label pre_type = (Label)row.FindControl("lbltype");
                    HtmlSelect ddlgrp = (HtmlSelect)row.FindControl("ddlstatus");
                   string status = ddlgrp.Value.ToString();
                   
                   
                }
            }
            catch(Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance_Override 420: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();

            }

        }
    }
}