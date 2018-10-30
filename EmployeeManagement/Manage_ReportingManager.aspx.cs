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
    public partial class Manage_ReportingManager : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        header.Text = "Manage Reporting Manager";
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
                    ltrErr.Text = "";
                    BindEmployeeData();
                    Search();


                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void BindEmployeeData()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtemplist = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName FROM employee_master  where  EmpStatusFlag=0 and employee_master.IsDelete=0 and EmpId not in(select Emp_id from reporting_manager_master where IsDelete=0)");

                ddlEmployee.DataSource = dtemplist;
                ddlEmployee.DataTextField = "FullName";
                ddlEmployee.DataValueField = "EmpId";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select Reporting Manager", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager  73: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void Search()
        {

            try
            {
                ltrErr.Text = "";

                DataTable dtgvdata = BindGrid();
                btnExport.Visible = false;
                if (dtgvdata != null)
                {
                    grd.DataSource = dtgvdata;
                    grd.DataBind();
                    if (dtgvdata.Rows.Count > 0)
                        btnExport.Visible = true;
                }
                else
                {
                    ltrErr.Visible = true;
                    ltrErr.Text = "Record dose not exists for selected criteria";
                    grd.DataSource = null;

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 106: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected DataTable BindGrid()
        {
            objmysqldb.ConnectToDatabase();
            DataTable dtdata = new DataTable();
            try
            {
                dtdata = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,Reporting_manager_id,Sms_Sent,Email_Sent,Single_Sms_Email_Sent,PunchTime_Sms_Email_Sent,if(Sms_Sent=1,'YES','NO') as Sms,if(Email_Sent=1,'YES','NO') as Email,if(Single_Sms_Email_Sent=1,'YES','NO') as Single_Sms_Email,if(PunchTime_Sms_Email_Sent=1,'YES','NO') as PunchTime_Sms_Email FROM employee_master inner join reporting_manager_master on employee_master.EmpId=reporting_manager_master.Emp_id where  EmpStatusFlag=0 and employee_master.IsDelete=0 and reporting_manager_master.IsDelete=0");

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 121: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
            return dtdata;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            ltrErr.Text = "";
            objmysqldb.ConnectToDatabase();
            try
            {
                int emp_id = 0;
                int.TryParse(ddlEmployee.Items[ddlEmployee.SelectedIndex].Value.ToString(), out emp_id);

                if (ddlEmployee.SelectedIndex <= 0 || emp_id == 0)
                {
                    ltrErr.Text = "Reporting Manager name is mandatory";
                    return;
                }
                int sms = 0;
                int email = 0;
                int Single_sms_email = 0;
                int punchtime = 0;
                if (chksms.Checked)
                {
                    sms = 1;
                }
                if (chkEmail.Checked)
                {
                    email = 1;
                }
                if (chksinglesms.Checked)
                {
                    Single_sms_email = 1;
                }
                if (chksms_Email.Checked)
                {
                    punchtime = 1;
                }

                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                objmysqldb.OpenSQlConnection();
                DateTime currenttime = Logger.getIndiantimeDT();
                int retval = objmysqldb.InsertUpdateDeleteData("insert into reporting_manager_master (Emp_id,Sms_Sent,Email_Sent,Single_Sms_Email_Sent,PunchTime_Sms_Email_Sent,modify_datetime,DOC,IsDelete,IsUpdate,UserID) values (" + emp_id + "," + sms + "," + email + "," + Single_sms_email + "," + punchtime + "," + currenttime.Ticks + "," + currenttime.Ticks + ",0,1," + user_id + ")");
                if (retval != 1)
                {
                    ltrErr.Text = "Please Try Again.";

                    Logger.WriteCriticalLog("Manage_ReportingManager 172 Update error.");
                    return;
                }
                ltrErr.Text = "Reporting Manager add successfully.";
                Search();
                ddlEmployee.SelectedIndex = -1;
                chksms.Checked = false;
                chkEmail.Checked = false;
                chksinglesms.Checked = false;
                chksms_Email.Checked = false;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 185: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                DataTable dtReportingManger = BindGrid();
                if (dtReportingManger != null && dtReportingManger.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtReportingManger, "List_Of_Reporting_Manager");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 212: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grd.EditIndex = -1;
                Search();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 225: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (e.CommandName == "del")
                {

                    int reporting_manager_id = 0;
                    int.TryParse(e.CommandArgument.ToString(), out reporting_manager_id);
                    objmysqldb.ConnectToDatabase();
                    //pending
                    DataTable dtemp_data = objmysqldb.GetData("Select emp_id from employee_assign_to_reporting_manager  WHERE  Reporting_manager_id =" + reporting_manager_id + " and IsDelete=0");

                    if (dtemp_data != null && dtemp_data.Rows.Count > 0)
                    {
                        ltrErr.Text = "Employee Assign to select Reporting Manager so can't delete it";
                        return;
                    }
                    else
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        DateTime currenttime = Logger.getIndiantimeDT();
                        string query = "Update reporting_manager_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where Reporting_manager_id=" + reporting_manager_id + " ";
                        objmysqldb.OpenSQlConnection();
                        int res = objmysqldb.InsertUpdateDeleteData(query);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";
                            Logger.WriteCriticalLog("Manage_ReportingManager 258 Update error.");
                        }
                        else
                        {
                            grd.EditIndex = -1;
                            Search();
                            ltrErr.Text = "Reporting Manager Delete Successfully";
                        }
                    }

                    grd.EditIndex = -1;
                }
                else if (e.CommandName == "Edit")
                {

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 277: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string command = e.ToString();
                grd.EditIndex = e.NewEditIndex;
                Search();
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 297: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
               
                    int repManager = 0;
                  
                    int sms = 0;
                    int email = 0;
                    int singlesms = 0;
                    int punching = 0;


                    Label Manager = (Label)grd.Rows[e.RowIndex].FindControl("lblmanager_id");

                    CheckBox chksms = (CheckBox)grd.Rows[e.RowIndex].FindControl("cbSelectSMS");
                    CheckBox chkemail = (CheckBox)grd.Rows[e.RowIndex].FindControl("cbSelectemail");
                    CheckBox chksinglesms = (CheckBox)grd.Rows[e.RowIndex].FindControl("cbSelectsms_email");
                    CheckBox chkpunching = (CheckBox)grd.Rows[e.RowIndex].FindControl("cbSelectsms_email_punch");
                    if (chksms.Checked)
                    {
                        sms = 1;
                    }
                    if (chkemail.Checked)
                    {
                        email = 1;
                    }
                    if (chksinglesms.Checked)
                    {
                        singlesms = 1;
                    }
                    if (chkpunching.Checked)
                    {
                        punching = 1;
                    }
                    int.TryParse(Manager.Text.ToString(), out repManager);

                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    DateTime currenttime = Logger.getIndiantimeDT();
                    string query = "Update reporting_manager_master set Sms_Sent=" + sms + ",Email_Sent=" + email + ",Single_Sms_Email_Sent=" + singlesms + ",PunchTime_Sms_Email_Sent=" + punching + ", modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where Reporting_manager_id=" + repManager + " ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);

                    grd.EditIndex = -1;
                   
                    ltrErr.Text = "Reporting Manager Update Successfully";
                    Search();
            
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 354: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
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
                    if (((DataRowView)e.Row.DataItem)["Sms_Sent"].ToString() == "1")
                    {
                        CheckBox sms = (CheckBox)e.Row.FindControl("cbSelectSMS");
                        if (sms != null)
                            sms.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["Email_Sent"].ToString() == "1")
                    {
                        CheckBox sms = (CheckBox)e.Row.FindControl("cbSelectemail");
                        if (sms != null)
                            sms.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["Single_Sms_Email_Sent"].ToString() == "1")
                    {
                        CheckBox sms = (CheckBox)e.Row.FindControl("cbSelectsms_email");
                        if (sms != null)
                            sms.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["PunchTime_Sms_Email_Sent"].ToString() == "1")
                    {
                        CheckBox sms = (CheckBox)e.Row.FindControl("cbSelectsms_email_punch");
                        if (sms != null)
                            sms.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_ReportingManager 401: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
    }
}