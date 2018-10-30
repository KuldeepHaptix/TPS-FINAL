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

    public partial class ModuleGrp_AssignTo_Employee : System.Web.UI.Page
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
                        header.Text = "Assign Module Group To Employee";
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
                    bindGrplist();
                    BtnSave.Visible = false; Button2.Visible = false;
                    //int grpid = 0;
                    //int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                    //bindgvdata(grpid);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 56: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void bindGrplist()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtbindGrp = objmysqldb.GetData("select module_group_id,module_group_name from employee_app_module_group where IsDelete=0");
                ddlgrouplist.DataSource = dtbindGrp;
                ddlgrouplist.DataTextField = "module_group_name";
                ddlgrouplist.DataValueField = "module_group_id";
                ddlgrouplist.DataBind();
                ddlgrouplist.Items.Insert(0, new ListItem("Select Module Group", "0"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 74: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void ddlgrouplist_selectedindexchange(object sender, EventArgs e)
        {

            try
            {

                int grpid = 0;
                int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                bindgvdata(grpid);
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 94: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }

        protected void bindgvdata(int grpid)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                BtnSave.Visible = false; Button2.Visible = false;
                if (grpid == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ltrErr.Text = "Please select module group";
                    return;
                }
                DataTable dtbindGrp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,if(1=1,0,1)as selects from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and empid not in(select emp_id from employee_app_module_rights where module_group_id <> " + grpid + " and IsDelete=0) order by  empid");



                if (dtbindGrp != null && dtbindGrp.Rows.Count > 0)
                {
                    DataTable dtselectEmp = objmysqldb.GetData("SELECT emp_id FROM employee_app_module_rights where IsDelete=0 and module_group_id=" + grpid + " ");
                    if (dtselectEmp != null && dtselectEmp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtselectEmp.Rows.Count; i++)
                        {
                            DataRow[] drselect = dtbindGrp.Select("empid = " + int.Parse(dtselectEmp.Rows[i]["emp_id"].ToString()) + "");
                            foreach (DataRow dr in drselect)
                            {
                                dr["selects"] = "1";
                            }
                        }
                    }
                    GridView1.DataSource = dtbindGrp;

                    GridView1.DataBind();
                    BtnSave.Visible = true; Button2.Visible = true;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ltrErr.Text = "Receord does not exsits for selected criteria";
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 146: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (((DataRowView)e.Row.DataItem)["selects"].ToString() == "1")
                    {
                        CheckBox chkkselect = (CheckBox)e.Row.FindControl("cbSelect");
                        chkkselect.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 173: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                int grpid = 0;
                int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                if (grpid > 0)
                {
                    DataTable dtprevEmp = objmysqldb.GetData("SELECT emp_id FROM employee_app_module_rights where IsDelete=0 and module_group_id=" + grpid + " ");
                    string emp_ids = "";
                    objmysqldb.OpenSQlConnection();
                    objmysqldb.BeginSQLTransaction();
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox chkshow = (CheckBox)row.FindControl("cbSelect");
                        if (chkshow.Checked)
                        {
                            int emp_id = 0;
                            Label emp = (Label)row.FindControl("lblemp_id");
                            int.TryParse(emp.Text.ToString(), out emp_id);
                            DateTime currentTime = Logger.getIndiantimeDT();
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            int retval = objmysqldb.InsertUpdateDeleteData("update employee_app_module_rights set modify_datetime=" + currentTime.Ticks + ",IsDelete=0,IsUpdate=1,UserID=" + user_id + " where module_group_id=" + grpid + " and emp_id=" + emp_id + " ");

                            if (retval == 0)
                            {
                                retval = objmysqldb.InsertUpdateDeleteData("insert into employee_app_module_rights (module_group_id,emp_id,modify_datetime,DOC,IsDelete,IsUpdate,UserID) values (" + grpid + "," + emp_id + "," + currentTime.Ticks + "," + currentTime.Ticks + ",0,1," + user_id + ") ");

                                if (retval != 1)
                                {
                                    ltrErr.Text = "please try again";
                                    objmysqldb.RollBackSQLTransaction();
                                    Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 216 Update error.");
                                    return;
                                }
                            }
                            else
                            {
                                emp_ids += emp_id + ",";
                            }
                        }
                    }
                    emp_ids = emp_ids.TrimEnd(',');
                     if (!emp_ids.Equals("") && dtprevEmp != null && dtprevEmp.Rows.Count > 0)
                    {
                        DataRow[] drdata = dtprevEmp.Select("emp_id NOt IN (" + emp_ids + ")");
                        dtprevEmp = dtprevEmp.Clone();
                        if (drdata.Length > 0)
                        {
                            dtprevEmp = drdata.CopyToDataTable();
                        }
                    }
                    foreach (DataRow dr in dtprevEmp.Rows)
                    {
                        DateTime currentTime = Logger.getIndiantimeDT();
                        int retval = objmysqldb.InsertUpdateDeleteData("update employee_app_module_rights set modify_datetime=" + currentTime.Ticks + ",IsDelete=1,IsUpdate=1,UserID=" + user_id + " where module_group_id=" + grpid + " and emp_id=" + int.Parse(dr["emp_id"].ToString()) + " ");
                    }

                    objmysqldb.EndSQLTransaction();
                    bindgvdata(grpid);
                    ltrErr.Text = "Module group Assign to employee Successfully.";
                }
                else
                {
                    ltrErr.Text = "Please select module group";
                }
            }
            catch (Exception ex)
            {
                objmysqldb.RollBackSQLTransaction();
                Logger.WriteCriticalLog("ModuleGrp_AssignTo_Employee 254: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveData();
        }


    }
}