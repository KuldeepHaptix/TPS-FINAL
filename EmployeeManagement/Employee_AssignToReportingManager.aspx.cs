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
    public partial class Employee_AssignToReportingManager : System.Web.UI.Page
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
                        header.Text = "Employee Assign To Reporting Manager";
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
                    btnassign.Enabled = false;
                    btnUnAssign.Enabled = false;
                    btnsave.Enabled = false;
                    getbindReportingManager();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void getbindReportingManager()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtReportingMg = objmysqldb.GetData("select Reporting_manager_id,Emp_id,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from reporting_manager_master inner join employee_master on reporting_manager_master.Emp_id =employee_master.empid where reporting_manager_master.IsDelete=0 and employee_master.IsDelete=0 and EmpStatusFlag=0");

                ddlmanager.DataSource = dtReportingMg;
                ddlmanager.DataTextField = "FullName";
                ddlmanager.DataValueField = "Reporting_manager_id";
                ddlmanager.DataBind();
                ddlmanager.Items.Insert(0, new ListItem("Select Reporting Manager", "0"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 74: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                btnassign.Enabled = false;
                btnUnAssign.Enabled = false;
                btnsave.Enabled = false;
                if (ddlmanager.SelectedIndex > 0)
                {
                    int id = 0;
                    int.TryParse(ddlmanager.Items[ddlmanager.SelectedIndex].Value.ToString(), out id);
                    if (id > 0)
                    {
                        dsBindGvData(id);
                    }
                    else
                    {
                        ltrErr.Text = "Please select reporting managet";
                        grd.DataSource = null;
                        GridAssign.DataSource = null;
                    }
                }
                else
                {
                    ltrErr.Text = "Please select reporting managet";
                    grd.DataSource = null;
                    GridAssign.DataSource = null;
                }
                disablecontrol();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 115: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void dsBindGvData(int id)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtAssignEmployee = objmysqldb.GetData("select CAST(Reporting_manager_id AS char(255))as Reporting_manager_id,empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone from employee_assign_to_reporting_manager inner join employee_master on employee_assign_to_reporting_manager.emp_id =employee_master.empid  where employee_assign_to_reporting_manager.IsDelete=0 and employee_master.IsDelete=0 and EmpStatusFlag=0 and Reporting_manager_id=" + id + " order by  empid");

                DataTable dtunassignEmp = objmysqldb.GetData("select cast(if(1=1,0,1) As char(55) )as Reporting_manager_id,empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and empid not in(select emp_id from employee_assign_to_reporting_manager where Reporting_manager_id=" + id + " and IsDelete=0) and empid <> (select emp_id from reporting_manager_master where Reporting_manager_id=" + id + "  and IsDelete=0) order by  empid");

                ViewState["assign"] = dtAssignEmployee;
                ViewState["Unassign"] = dtunassignEmp;
                if (dtAssignEmployee != null)
                {
                    GridAssign.DataSource = dtAssignEmployee;
                    GridAssign.DataBind();

                }
                else
                {
                    GridAssign.DataSource = null;
                }

                if (dtunassignEmp != null)
                {
                    grd.DataSource = dtunassignEmp;
                    grd.DataBind();
                }
                else
                {
                    grd.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 154: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void disablecontrol()
        {
            try
            {
                btnassign.Enabled = false;
                btnUnAssign.Enabled = false;
                btnsave.Enabled = false;
                if (grd.Rows.Count > 0)
                {
                    btnassign.Enabled = true;
                    btnsave.Enabled = true;
                }
                if (GridAssign.Rows.Count > 0)
                {
                    btnUnAssign.Enabled = true;
                    btnsave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 180: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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

        protected void GridAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void btnassign_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_id = "";
                foreach (GridViewRow row in grd.Rows)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                    if (chkcheck.Checked)
                    {
                        Label emp = (Label)row.FindControl("lblEmp_id");

                        emp_id += emp.Text.ToString() + ",";
                    }
                }
                emp_id = emp_id.TrimEnd(',');
                if (!emp_id.Equals(""))
                {
                    DataTable dtunassign = (DataTable)ViewState["Unassign"];

                    DataTable dtassign = (DataTable)ViewState["assign"];
                    DataRow[] drdata = dtunassign.Select("empId in(" + emp_id + ")");

                    if (drdata.Length > 0)
                    {
                        dtassign.Merge(drdata.CopyToDataTable());
                        GridAssign.DataSource = dtassign;
                        GridAssign.DataBind();
                    }
                    else
                    {
                        GridAssign.DataSource = dtassign.Clone();
                        GridAssign.DataBind();
                    }
                    ViewState["assign"] = dtassign;

                    drdata = dtunassign.Select("empId NOT in(" + emp_id + ")");

                    if (drdata.Length > 0)
                    {
                        dtunassign = drdata.CopyToDataTable();
                        grd.DataSource = dtunassign;
                        grd.DataBind();
                    }
                    else
                    {
                        grd.DataSource = dtunassign.Clone();
                        grd.DataBind();
                    }
                    ViewState["Unassign"] = dtunassign;
                }
                disablecontrol();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 262: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnUnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_id = "";
                foreach (GridViewRow row in GridAssign.Rows)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                    if (chkcheck.Checked)
                    {
                        Label emp = (Label)row.FindControl("lblEmp_id");

                        emp_id += emp.Text.ToString() + ",";
                    }
                }
                emp_id = emp_id.TrimEnd(',');
                if (!emp_id.Equals(""))
                {
                    DataTable dtunassign = (DataTable)ViewState["Unassign"];

                    DataTable dtassign = (DataTable)ViewState["assign"];
                    DataRow[] drdata = dtassign.Select("empId in(" + emp_id + ")");

                    if (drdata.Length > 0)
                    {
                        dtunassign.Merge(drdata.CopyToDataTable());

                        grd.DataSource = dtunassign;
                        grd.DataBind();
                    }
                    else
                    {
                        grd.DataSource = dtunassign.Clone();
                        grd.DataBind();
                    }
                    ViewState["Unassign"] = dtunassign;

                    drdata = dtassign.Select("empId NOT in(" + emp_id + ")");

                    if (drdata.Length > 0)
                    {
                        dtassign = drdata.CopyToDataTable();
                        GridAssign.DataSource = dtassign;
                        GridAssign.DataBind();
                    }
                    else
                    {
                        GridAssign.DataSource = dtassign.Clone();
                        GridAssign.DataBind();
                    }
                    ViewState["assign"] = dtassign;
                }
                disablecontrol();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 322: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            ltrErr.Text = "";
            try
            {
                int id = 0;
                int.TryParse(ddlmanager.Items[ddlmanager.SelectedIndex].Value.ToString(), out id);

                DataTable dtolddata = objmysqldb.GetData("SELECT employee_assign_to_Reporting_manager_id,emp_id FROM employee_assign_to_reporting_manager where Reporting_manager_id=" + id + " ");

                string emp_id = "";
                int emp_ids = 0;

                objmysqldb.OpenSQlConnection();
                objmysqldb.BeginSQLTransaction();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                foreach (GridViewRow row in GridAssign.Rows)
                {
                    Label emp = (Label)row.FindControl("lblEmp_id");
                    int.TryParse(emp.Text.ToString(), out emp_ids);
                    DateTime currentTime = Logger.getIndiantimeDT();
                    int retval = objmysqldb.InsertUpdateDeleteData("update employee_assign_to_reporting_manager set modify_datetime=" + currentTime.Ticks + ",IsUpdate=1,UserID=" + user_id + ",IsDelete=0 where emp_id=" + emp_ids + " and Reporting_manager_id=" + id + "  ");
                    if (retval == 0)
                    {
                        retval = objmysqldb.InsertUpdateDeleteData("insert into employee_assign_to_reporting_manager (emp_id,Reporting_manager_id,modify_datetime,DOC,IsUpdate,UserID,IsDelete) values (" + emp_ids + "," + id + "," + currentTime.Ticks + "," + currentTime.Ticks + ",1," + user_id + ",0) ");

                        if (retval != 1)
                        {
                            ltrErr.Text = "please try again";
                            objmysqldb.RollBackSQLTransaction();
                            Logger.WriteCriticalLog("Employee_AssignToReportingManager 357 Update error.");
                            return;
                        }
                    }
                    else
                    {
                        emp_id += emp.Text.ToString() + ",";
                    }
                }

                emp_id = emp_id.TrimEnd(',');

                if (!emp_id.Equals("") && dtolddata != null && dtolddata.Rows.Count > 0)
                {
                    //string[] employees = emp_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DataRow[] drdata = dtolddata.Select("emp_id NOT IN(" + emp_id + ") ");
                    dtolddata = dtolddata.Clone();
                    if (drdata.Length > 0)
                    {
                        dtolddata = drdata.CopyToDataTable();
                    }
                }
                DateTime currentTimes = Logger.getIndiantimeDT();
                foreach (DataRow dr in dtolddata.Rows)
                {
                    int retval = objmysqldb.InsertUpdateDeleteData("update employee_assign_to_reporting_manager set modify_datetime=" + currentTimes.Ticks + ",IsUpdate=1,UserID=" + user_id + ",IsDelete=1 where  emp_id=" + int.Parse(dr["emp_id"].ToString()) + " and Reporting_manager_id=" + id + "  ");
                }

                objmysqldb.EndSQLTransaction();
                dsBindGvData(id);
                ltrErr.Text = "Employee Assign to reporting manager Successfully.";
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToReportingManager 391: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                objmysqldb.RollBackSQLTransaction();
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}