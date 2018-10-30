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
    public partial class SalaryConfiguration : System.Web.UI.Page
    {
        int user_id = 0;
        MySQLDB objmysqldb = new MySQLDB();
        DataTable dtGroupWiseEmpID = new DataTable();
        DataTable Emplist = new DataTable();
        //DataTable allEmplist = new DataTable();
        int grpid = 0;
        string grid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                {
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    Label header = Master.FindControl("lbl_pageHeader") as Label;
                    header.Text = "Employee Salary Configuration";
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
            // bindEmpGroup();
            //BindGrid(-1);
            btnsave.Visible = false;
            if (!IsPostBack)
            {
                bindEmpGroup();
                BindGrid(0);
                //btnsave.Visible = true;
                //int grpid = 0;
                //int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                //bindgvdata(grpid);
            }


        }
        private void bindEmpGroup()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtgrplist = objmysqldb.GetData("SELECT * FROM employee_management.report_group_list");
                DataTable EmpList = new DataTable();
                ddlgrouplist.DataSource = dtgrplist;
                ddlgrouplist.DataTextField = "report_grp_name";
                ddlgrouplist.DataValueField = "report_grp_id";
                ddlgrouplist.DataBind();
                ddlgrouplist.Items.Insert(0, new ListItem("Select All Emp", "0"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("SalaryConfiguration 77: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {

                objmysqldb.disposeConnectionObj();
            }

        }
        protected void ddlgrouplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                BindGrid(grpid);
                Session["grpid"] = grpid;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Salary Configuration 96: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }


        }
        private void BindGrid(int grpid)
        {
            DataTable allEmplist = new DataTable();
            DataTable UnGroupEmp = new DataTable();
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                // btnsave.Visible = true;
                if (grpid > 0)
                {
                    dtGroupWiseEmpID = objmysqldb.GetData("SELECT * FROM report_group_list where report_grp_id=" + grpid + " And IsDelete=0");
                    if (dtGroupWiseEmpID.Rows.Count > 0 && dtGroupWiseEmpID != null)
                    {
                        //allEmplist = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id, EmpId,payscale,concat(EmpFirstName,' ',EmpMiddleName,' ',EmpLastName)As FullName FROM  employee_master where EmpId in(" + dtGroupWiseEmpID.Rows[0]["emp_ids"].ToString().TrimEnd(',') + ")  ");
                        allEmplist = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id,EmpId,if(payscale='','0',payscale) as payscale,concat(EmpFirstName,' ',EmpMiddleName,'  ',EmpLastName)As FullName FROM employee_master  where employee_master.IsDelete=0 and EmpStatusFlag=0 and EmpId in(" + dtGroupWiseEmpID.Rows[0]["emp_ids"].ToString().TrimEnd(',') + ")  ");


                        if (allEmplist != null)
                        {
                            //ViewState["griddatatable"] = allEmplist;
                            grdEmplist.DataSource = allEmplist;
                            grdEmplist.DataBind();
                            btnsave.Visible = true;
                        }
                    }
                }
                else
                {
                    if (grpid <= 0)
                    {
                        UnGroupEmp = objmysqldb.GetData("SELECT employee_master.FP_Id, EmpId,if(payscale='','0',payscale) as payscale,concat(EmpFirstName,' ',EmpMiddleName,' ' ,EmpLastName)As FullName FROM  employee_management.employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0");
                    }
                    if (UnGroupEmp.Rows.Count > 0 && UnGroupEmp != null)
                    {
                        //ViewState["griddatatable"] = UnGroupEmp;
                        grdEmplist.DataSource = UnGroupEmp;
                        grdEmplist.DataBind();
                        btnsave.Visible = true;

                    }

                }

            }

            catch (Exception ex)
            {
                Logger.WriteCriticalLog("SalaryConfiguration 149: exception:" + ex.Message + "::::::::" + ex.StackTrace);


            }
            finally
            {
                objmysqldb.disposeConnectionObj();

            }

        }
        protected void saveData()
        {
            int val = 0;
            try
            {
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                DataTable gridTable = (DataTable)grdEmplist.DataSource;
                DataTable dt = GetDataTable(grdEmplist);
                foreach (GridViewRow row in grdEmplist.Rows)
                {

                    TextBox txtpayscale1 = (TextBox)row.FindControl("txtpayscale");
                    // HtmlInputText txtpayscale1 = (HtmlInputText)row.FindControl("txtpayscale");
                    Label EmpId = (Label)grdEmplist.Rows[row.RowIndex].FindControl("lblEmp_id");
                    DateTime currenttime = Logger.getIndiantimeDT();
                    double salary = 0;
                    double.TryParse(txtpayscale1.Text.ToString(), out salary);
                    int empidint = int.Parse(EmpId.Text.ToString());
                    if (salary > 0)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        val = objmysqldb.InsertUpdateDeleteData("UPDATE employee_master SET modify_datetime='" + currenttime.Ticks + "',PayScale=" + salary + ",IsUpdate=1,UserID=" + user_id + "  WHERE EmpId=" + empidint + " ");
                    }

                }
                if (val == 1)
                {
                    ltrErr.Text = "Data  Updated Successfully....";
                    btnsave.Visible = true;
                    int gid = 0;
                    //if(Session.co)
                    if (Session["grpid"] != null)
                    {
                        int.TryParse(Session["grpid"].ToString(), out gid);
                    }
                    BindGrid(gid);

                }

            }
            catch (Exception ex)
            {

                Logger.WriteCriticalLog("SalaryConfiguration 194: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {

                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //objmysqldb.ConnectToDatabase();
            //objmysqldb.OpenSQlConnection();
            saveData();
        }

        protected void grdEmplist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                { }
            }
            catch (Exception aa)
            {
                Logger.WriteCriticalLog("SalaryConfiguration SetUp 225: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //objmysqldb.ConnectToDatabase();
            //objmysqldb.OpenSQlConnection();
            saveData();
        }

        public static DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }
            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
