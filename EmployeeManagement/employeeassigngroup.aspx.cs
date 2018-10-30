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
    public partial class employeeassigngroup : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int emp_m_id = 0;
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
                        header.Text = "Assign Employee To Module Group";// "Assign Group to Employee";
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
                    Search();
                    bindemployee();
                    bindgroup();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 53: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void Search()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtmodule = objmysqldb.GetData("Select EmpId,concat(employee_master.EmpLastName,' ',employee_master.EmpFirstName,' ',employee_master.EmpMiddleName) as EmployeeName from employee_master where IsDelete=0 and EmpStatusFlag=0");
                DataTable dtcategory = objmysqldb.GetData("Select module_group_id,module_group_name,module_ids from employee_app_module_group where IsDelete=0");
                //DataTable dtgrouplist = objmysqldb.GetData("Select emp_module_id,module_group_id,emp_id from employee_app_module_rights where IsDelete=0");
                DataTable dtgrouplist = objmysqldb.GetData("Select employee_app_module_rights.emp_module_id,employee_app_module_rights.module_group_id,employee_app_module_rights.emp_id from employee_app_module_rights inner join employee_master on employee_app_module_rights.emp_id=employee_master.EmpId where employee_master.EmpStatusFlag=0 and employee_master.IsDelete=0 and employee_app_module_rights.IsDelete=0;");
                objmysqldb.disposeConnectionObj();
                dtgrouplist.Columns.Add("group_name");
                dtgrouplist.Columns.Add("EmployeeName");


                for (int i = 0; i < dtgrouplist.Rows.Count; i++)
                {
                    string group_name = " ";
                    string EmployeeName = "";

                    DataRow[] dr1 = dtmodule.Select("EmpId ='" + dtgrouplist.Rows[i]["emp_id"].ToString() + "'");
                    if (dr1.Length > 0)
                    {
                        foreach (DataRow dr in dr1)
                        {

                            EmployeeName = dr["EmployeeName"].ToString();
                        }
                        if (EmployeeName == "")
                        {

                        }
                        dtgrouplist.Rows[i]["EmployeeName"] = EmployeeName;
                    }
                    DataRow[] dr2 = dtcategory.Select("module_group_id ='" + dtgrouplist.Rows[i]["module_group_id"].ToString() + "'");
                    if (dr2.Length > 0)
                    {
                        foreach (DataRow dr in dr2)
                        {
                            group_name = dr["module_group_name"].ToString();
                        }
                        if (group_name == "")
                        {

                        }
                        dtgrouplist.Rows[i]["group_name"] = group_name;
                    }
                }
                if (dtgrouplist != null)
                {
                    grd.DataSource = dtgrouplist;
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
                Logger.WriteCriticalLog("employeeassigngroup 110: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void bindemployee()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                if (grp_idHidden.Value.ToString().Equals("0") || grp_idHidden.Value == "")
                {
                    DataTable dtmodulegroup = objmysqldb.GetData("SELECT distinct EmpId,CONCAT(employee_master.EmpLastName,' ',employee_master.EmpFirstName, ' ',employee_master.EmpMiddleName) AS EmployeeName FROM employee_master        left JOIN employee_app_module_rights ON employee_master.EmpId =employee_app_module_rights.emp_id where employee_app_module_rights.emp_id IS NULL  and employee_master.IsDelete=0 and employee_master.EmpStatusFlag=0;");
                    ddlemplist.DataSource = dtmodulegroup;
                    ddlemplist.DataTextField = "EmployeeName";
                    ddlemplist.DataValueField = "EmpId";
                    ddlemplist.DataBind();
                    ddlemplist.Items.Insert(0, new ListItem("Select Employee", "-1"));
                }
                else
                {
                    DataTable dtmodulegroup = objmysqldb.GetData("Select EmpId,concat(employee_master.EmpLastName,' ',employee_master.EmpFirstName,' ',employee_master.EmpMiddleName) as EmployeeName from employee_master where IsDelete=0 and EmpStatusFlag=0 order by EmpId");
                    ddlemplist.DataSource = dtmodulegroup;
                    ddlemplist.DataTextField = "EmployeeName";
                    ddlemplist.DataValueField = "EmpId";
                    ddlemplist.DataBind();
                    ddlemplist.Items.Insert(0, new ListItem("Select Employee", "-1"));
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 140: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void bindgroup()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtmodulegroup = objmysqldb.GetData("Select module_group_id,module_group_name from employee_app_module_group where IsDelete=0 order by module_group_id");
                ddlgrouplist.DataSource = dtmodulegroup;
                ddlgrouplist.DataTextField = "module_group_name";
                ddlgrouplist.DataValueField = "module_group_id";
                ddlgrouplist.DataBind();
                ddlgrouplist.Items.Insert(0, new ListItem("Select Group", "-1"));



            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 165: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (!grp_idHidden.Value.ToString().Equals("0") && grp_idHidden.Value != "")
                {
                    update_emp_group();
                    return;
                }
                int grpid = 0;
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(grp_idHidden.Value.ToString(), out grpid);
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int emp = 0;
                int.TryParse(ddlemplist.Items[ddlemplist.SelectedIndex].Value.ToString(), out emp);
                int grp = 0;
                int.TryParse(ddlgrouplist.Items[ddlgrouplist.SelectedIndex].Value.ToString(), out grp);
                DateTime currenttime = Logger.getIndiantimeDT();
                DataTable dtdata = objmysqldb.GetData("select * from employee_app_module_rights where module_group_id<>" + grp + " and emp_id=" + emp + "");
                DataTable dtdata_emp = objmysqldb.GetData("select * from employee_app_module_rights where module_group_id=" + grp + " and emp_id=" + emp + "");
                if (ddlgrouplist.SelectedIndex < 1 || ddlemplist.SelectedIndex < 1)
                {
                    ltrErr.Text = "Please Select All Details.";
                }
                else
                {

                    string query = "Insert into  employee_app_module_rights(module_group_id,emp_id,modify_datetime,DOC,IsDelete,IsUpdate,UserID) values (" + grp + "," + emp + "," + currenttime.Ticks + "," + currenttime.Ticks + ",0,1," + user_id + ")";
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("employeeassigngroup 209 Update error.");
                    }
                    else
                    {
                        Search();
                        bindgroup();
                        bindemployee();
                        ltrErr.Text = "Group Assign to Employee Successfully";

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 224: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
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

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dtemp = new DataTable();
            try
            {
                objmysqldb.ConnectToDatabase();
                ltrErr.Text = "";
                if (e.CommandName == "Edit")
                {
                    string arg = e.CommandArgument.ToString();
                    int.TryParse(e.CommandArgument.ToString(), out emp_m_id);
                    grp_idHidden.Value = emp_m_id.ToString();
                    dtemp = objmysqldb.GetData("Select * from employee_app_module_rights where emp_module_id=" + emp_m_id + "");
                    string emp = "";
                    bindemployee();
                    emp = dtemp.Rows[0]["emp_id"].ToString();
                    ddlemplist.SelectedIndex = ddlemplist.Items.IndexOf(ddlemplist.Items.FindByValue(emp));
                    ddlemplist.Disabled = true;
                    string grpid = "";
                    grpid = dtemp.Rows[0]["module_group_id"].ToString();
                    ddlgrouplist.SelectedIndex = ddlgrouplist.Items.IndexOf(ddlgrouplist.Items.FindByValue(grpid));
                }
                else if (e.CommandName == "del")
                {
                    ltrErr.Text = "";
                    string arg = e.CommandArgument.ToString();
                    int.TryParse(e.CommandArgument.ToString(), out emp_m_id);
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    DateTime currenttime = Logger.getIndiantimeDT();

                    string query = "update employee_app_module_rights set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where emp_module_id=" + emp_m_id + "";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";
                        Logger.WriteCriticalLog("employeeassigngroup 279 Update error.");
                    }
                    else
                    {
                        Search();
                        bindgroup();
                        bindemployee();
                        ltrErr.Text = "Employee Deleted Successfully.";

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 294: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void update_emp_group()
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int.TryParse(grp_idHidden.Value.ToString(), out emp_m_id);
                DateTime currenttime = Logger.getIndiantimeDT();
                int emp = 0;
                int.TryParse(ddlemplist.Items[ddlemplist.SelectedIndex].Value.ToString(), out emp);
                int grp = 0;
                int.TryParse(ddlgrouplist.Items[ddlgrouplist.SelectedIndex].Value.ToString(), out grp);
                string query = "update employee_app_module_rights set module_group_id=" + grp + ",emp_id=" + emp + ",modify_datetime=" + currenttime.Ticks + ",IsDelete=0,IsUpdate=1,UserId=" + user_id + " where emp_module_id=" + emp_m_id + "";
                int res = objmysqldb.InsertUpdateDeleteData(query);
                if (res != 1)
                {
                    ltrErr.Text = "Please Try Again.";
                    Logger.WriteCriticalLog("employeeassigngroup 320 Update error.");
                }
                else
                {
                    ltrErr.Text = "Update Employee Group Successfully.";
                    Search();
                    bindgroup();
                    grp_idHidden.Value = "";
                    bindemployee();
                    ddlemplist.Disabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 334: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtmodule = objmysqldb.GetData("Select EmpId,concat(employee_master.EmpLastName,' ',employee_master.EmpFirstName,' ',employee_master.EmpMiddleName) as EmployeeName from employee_master where IsDelete=0 and EmpStatusFlag=0");
                DataTable dtcategory = objmysqldb.GetData("Select module_group_id,module_group_name,module_ids from employee_app_module_group where IsDelete=0");
                DataTable dtgrouplist = objmysqldb.GetData("Select emp_module_id,module_group_id,emp_id from employee_app_module_rights where IsDelete=0");

                dtgrouplist.Columns.Add("group_name");
                dtgrouplist.Columns.Add("EmployeeName");
                for (int i = 0; i < dtgrouplist.Rows.Count; i++)
                {
                    string group_name = " ";
                    string EmployeeName = "";
                    DataRow[] dr1 = dtmodule.Select("EmpId ='" + dtgrouplist.Rows[i]["emp_id"].ToString() + "'");
                    if (dr1.Length > 0)
                    {
                        foreach (DataRow dr in dr1)
                        {

                            EmployeeName = dr["EmployeeName"].ToString();
                        }
                        dtgrouplist.Rows[i]["EmployeeName"] = EmployeeName;
                    }
                    DataRow[] dr2 = dtcategory.Select("module_group_id ='" + dtgrouplist.Rows[i]["module_group_id"].ToString() + "'");
                    if (dr2.Length > 0)
                    {
                        foreach (DataRow dr in dr2)
                        {
                            group_name = dr["module_group_name"].ToString();
                        }
                        dtgrouplist.Rows[i]["group_name"] = group_name;
                    }
                }
                dtgrouplist.Columns.Remove("module_group_id");
                dtgrouplist.Columns.Remove("emp_id");
                if (dtgrouplist != null && dtgrouplist.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtgrouplist, "List_Of_Employee_Assign_Group");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employeeassigngroup 397: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}