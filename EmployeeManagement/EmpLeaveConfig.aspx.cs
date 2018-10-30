using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class EmpLeaveConfig : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        DataTable dtGroupWiseEmpID = new DataTable();
        DataTable UngroupEmp = new DataTable();
        DataTable Emplist = new DataTable();
        DataTable leaveList = new DataTable();
        int id = 0;
        int grpid = 0;
        int mid = 1;
        int yid = 0;
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                btnsave.Visible = true;
                //ddlmonthlist.Enabled = false;
                //ddlyear.Enabled = false;
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        header.Text = "Employee Leave Configuration";
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
                if (!Page.IsPostBack)
                {
                    BindGrid(-1);
                    bindEmpGroup();
                    bindMonth();

                    //bindyear();

                    for (int i = 2005; i <= 2020; i++)
                    {
                        ddlyear.Items.Add(i.ToString());
                    }
                    ddlyear.Items.FindByValue(MySQLDB.GetIndianTime().Year.ToString()).Selected = true;
                    mid = int.Parse(ddlmonthlist.SelectedIndex.ToString());
                    mid = mid + 1;
                    BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), -1);
                    // btnsave.Visible = true;
                }
            }
            //BindGrid(-1);
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 71: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        private void bindEmpGroup()
        {
            objmysqldb.ConnectToDatabase();
            try
            {

                DataTable dtgrplist = objmysqldb.GetData("SELECT * FROM employee_management.report_group_list");
                //dtgrplist.Columns.Add("----Select Group-----");
                ddlgrouplist.DataSource = dtgrplist;
                ddlgrouplist.DataTextField = "report_grp_name";
                ddlgrouplist.DataValueField = "report_grp_id";
                ddlgrouplist.DataBind();
                ddlgrouplist.Items.Insert(0, new ListItem("Select All Employee", "-1"));
                ddlmonthlist.Enabled = true;
                ddlyear.Enabled = true;
                // btnsave.Visible = true;

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 94: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {

                objmysqldb.disposeConnectionObj();
            }

        }
        private void bindMonth()
        {
            DataTable dtmonths = new DataTable();
            try
            {
                DateTime month = Convert.ToDateTime("1/1/2017");
                for (int i = 0; i < 12; i++)
                {
                    DateTime nextMonth = month.AddMonths(i);
                    ListItem list = new ListItem();
                    list.Text = nextMonth.ToString("MMMM");
                    list.Value = nextMonth.Month.ToString();
                    ddlmonthlist.Items.Add(list);
                }
                //ddlmonthlist.Items.Insert(0, new ListItem("  Select Month", "0"));
                //ddlmonthlist.SelectedIndex = ddlmonthlist.Items.IndexOf(ddlmonthlist.Items.FindByValue(curdate[1].ToString()));
                ddlmonthlist.SelectedValue = MySQLDB.GetIndianTime().Month.ToString();

            }
            catch (Exception aa)
            { Logger.WriteCriticalLog("EmpLeaveConfiguration 123: exception:" + aa.Message + "::::::::" + aa.StackTrace); }

        }
        protected void ddlgrouplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int.TryParse(ddlgrouplist.SelectedValue.ToString(), out grpid);
                BindGrid(grpid);
                mid = ddlmonthlist.SelectedIndex;
                mid = mid + 1;
                BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), int.Parse(ddlgrouplist.SelectedIndex.ToString()));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfig 139: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        private void BindGrid(int grpid)
        {
            DataTable allEmplist = new DataTable();
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();

                btnsave.Visible = true;
                if (grpid > 0)
                {
                    dtGroupWiseEmpID = objmysqldb.GetData("SELECT * FROM report_group_list where report_grp_id=" + grpid + " And IsDelete=0");
                    if (dtGroupWiseEmpID.Rows.Count > 0 && dtGroupWiseEmpID != null)
                    {
                        allEmplist = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id,EmpId,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  As FullName FROM employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and EmpId in(" + dtGroupWiseEmpID.Rows[0]["emp_ids"].ToString().TrimEnd(',') + ")  ");

                        if (allEmplist != null)
                        {
                            allEmplist.Columns.Add("LCL");
                            allEmplist.Columns.Add("LWP");
                            allEmplist.Columns.Add("CL");
                            allEmplist.Columns.Add("ML");
                            grdEmplist.DataSource = allEmplist;
                            grdEmplist.DataBind();

                            btnsave.Visible = true;
                            btnsave.Enabled = true;
                        }
                    }
                }
                else
                {
                    UngroupEmp = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id,EmpId,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  As FullName FROM  employee_management.employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0");

                    if (UngroupEmp != null)
                    {
                        UngroupEmp.Columns.Add("LCL");
                        UngroupEmp.Columns.Add("LWP");
                        UngroupEmp.Columns.Add("CL");
                        UngroupEmp.Columns.Add("ML");
                        grdEmplist.DataSource = UngroupEmp;
                        grdEmplist.DataBind();

                        btnsave.Visible = true;
                        btnsave.Enabled = true;
                    }
                    btnsave.Visible = true;
                    btnsave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 194: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }

        }
        private DataTable getLeaveList()
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                leaveList = objmysqldb.GetData("SELECT * FROM employee_management.leave_master;");
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 211: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
            return leaveList;
        }

        protected void BindEmpLeave(int monthId, int YearId, int groupid)
        {
            //objmysqldb.OpenSQlConnection();
            //mid = ddlmonthlist.SelectedIndex;
            //  mid = mid + 1;
            btnsave.Visible = true;
            // BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), int.Parse(ddlgrouplist.SelectedIndex.ToString()));
            objmysqldb.ConnectToDatabase();
            DataTable dtEmpList = new DataTable();
            DataTable dtEmp = new DataTable();
            try
            {
                if (groupid > 0)
                {
                    dtGroupWiseEmpID = objmysqldb.GetData("SELECT * FROM employee_management.report_group_list where report_grp_id=" + groupid + " And IsDelete=0");
                    dtEmpList = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id,EmpId,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  As FullName FROM employee_master where employee_master.IsDelete=0  and EmpId in(" + dtGroupWiseEmpID.Rows[0]["emp_ids"].ToString().TrimEnd(',') + ")  ");
                }
                else
                {
                    dtEmpList = objmysqldb.GetData("SELECT employee_management.employee_master.FP_Id,EmpId,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName)) As FullName FROM  employee_management.employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0");

                    //if (dtEmpList != null)
                    //{
                    //    dtEmpList.Columns.Add("LCL");
                    //    dtEmpList.Columns.Add("LWP");
                    //    dtEmpList.Columns.Add("CL");
                    //    dtEmpList.Columns.Add("ML");
                    //}
                }
                string[] arrr = { "FP_Id", "EmpId", "FullName" };
                dtEmp = dtEmpList.DefaultView.ToTable(true, arrr).Copy();
                dtEmp.Columns.Add("LCL");
                dtEmp.Columns.Add("LWP");
                dtEmp.Columns.Add("CL");
                dtEmp.Columns.Add("ML");
                DataTable dtEmpLeaveDetail = objmysqldb.GetData("SELECT Leave_History_Monthwise.* FROM Leave_History_Monthwise WHERE Leave_History_Monthwise.Month_Id in(" + monthId + ") AND Leave_History_Monthwise.Month_Year=" + YearId + "");
                getLeaveList();
                DataRow[] drCLLeave = leaveList.Select("Is_CL_Leave=0");
                int CLLeave = int.Parse(drCLLeave[0]["Leave_Id"].ToString());
                DataRow[] drMLLeave = leaveList.Select("Is_CL_Leave=1");
                int MLLeave = int.Parse(drMLLeave[0]["Leave_Id"].ToString());
                int EmpId = 0;
                for (int i = 0; i < dtEmp.Rows.Count; i++)
                {
                    EmpId = int.Parse(dtEmp.Rows[i]["EmpId"].ToString());
                    DataRow[] drfind = dtEmpLeaveDetail.Select("Emp_Id=" + EmpId + " AND Month_Id='" + monthId + "' AND Month_Year=" + YearId + "");
                    for (int j = 0; j < drfind.Length; j++)
                    {
                        if (drfind[j]["Leave_Id"].ToString().Equals("-1"))
                        {
                            dtEmp.Rows[i]["LWP"] = drfind[j]["Total_Leave"].ToString();
                        }
                        else if (drfind[j]["Leave_Id"].ToString().Equals(CLLeave.ToString()))
                        {
                            dtEmp.Rows[i]["CL"] = drfind[j]["Total_Leave"].ToString();
                        }
                        else if (drfind[j]["Leave_Id"].ToString().Equals(MLLeave.ToString()))
                        {
                            dtEmp.Rows[i]["ML"] = drfind[j]["Total_Leave"].ToString();
                        }
                        else if (drfind[j]["Leave_Id"].ToString().Equals("-4"))
                        {
                            dtEmp.Rows[i]["LCL"] = drfind[j]["Total_Leave"].ToString();
                        }
                    }
                }
                if (dtEmp != null)
                {
                    grdEmplist.DataSource = dtEmp;
                    grdEmplist.DataBind();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 295: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                objmysqldb.disposeConnectionObj();
            }

        }
        protected void saveData()
        {
            getLeaveList();
            //   btnsave.Enabled = false;
            try
            {
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                mid = int.Parse(ddlmonthlist.SelectedIndex.ToString());
                mid = mid + 1;
                yid = int.Parse(ddlyear.SelectedValue.ToString());
                if (mid == 0 || yid == 0)
                {
                    ltrErr.Text = "Select Valid Month And Year...";
                    return;
                }
                int colpos = 2;
                int val = 0;
                System.Collections.Hashtable hsLeavePos = new System.Collections.Hashtable();
                colpos++;
                hsLeavePos.Add(-4, colpos);
                colpos++;
                hsLeavePos.Add(-1, colpos);
                colpos++;
                DataRow[] ClLeave = leaveList.Select("Is_CL_Leave=0");
                hsLeavePos.Add(int.Parse(ClLeave[0]["Leave_Id"].ToString()), colpos);
                colpos++;
                DataRow[] MlLeave = leaveList.Select("Is_CL_Leave=1");
                hsLeavePos.Add(int.Parse(MlLeave[0]["Leave_Id"].ToString()), colpos);
                foreach (GridViewRow row in grdEmplist.Rows)
                {
                    Label EmpId = (Label)grdEmplist.Rows[row.RowIndex].FindControl("lblEmp_id");
                    Label FP_Id = (Label)grdEmplist.Rows[row.RowIndex].FindControl("lblFpId");
                    TextBox txtlcl = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtlcl");
                    TextBox txtlwp = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtlwp");
                    TextBox txtcl = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtcl");
                    TextBox txtml = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtml");
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int empidint = int.Parse(EmpId.Text.ToString());
                    int FPIdint = int.Parse(FP_Id.Text.ToString());
                    double lcl = 0;
                    double.TryParse(txtlcl.Text.ToString(), out lcl);
                    double lwp = 0;
                    double.TryParse(txtlwp.Text.ToString(), out lwp);
                    double cl = 0;
                    double.TryParse(txtcl.Text.ToString(), out cl);
                    double ml = 0;
                    double.TryParse(txtml.Text.ToString(), out ml);
                    foreach (DictionaryEntry keyId in hsLeavePos)
                    {
                        int leaveid = (int)keyId.Key;
                        int firstindex = (int)keyId.Value;
                        string ColName = grdEmplist.Columns[firstindex].HeaderText.ToString();

                        double Total_Leave = 0;
                        if (ColName == "LWP")
                        {
                            //int leaveid = 1;
                            Total_Leave = lwp;
                            
                        }
                        if (ColName.ToLower() == "lcl")
                        {
                            Total_Leave = lcl;
                        }
                        if (ColName.ToLower() == "cl")
                        {
                            Total_Leave = cl;
                        }
                        if (ColName.ToLower() == "ml")
                        {
                            Total_Leave = ml;
                        }

                        if (Total_Leave > 0)
                        {
                            val = objmysqldb.InsertUpdateDeleteData("update Leave_History_Monthwise set IsUpdate=1,Total_Leave=" + Total_Leave + ",modify_datetime=" + currenttime.Ticks + ",DOC=" + currenttime.Ticks + " where Emp_Id=" + empidint + " and Leave_Id=" + leaveid + " and Month_Id=" + mid + " and Month_Year=" + yid + " ");
                            if (val == 0)
                            {
                                val = objmysqldb.InsertUpdateDeleteData("INSERT INTO  Leave_History_Monthwise(Emp_Id,Leave_Id,Total_Leave,Month_Id,Month_Year,modify_datetime,DOC,IsUpdate,Isdelete,UserID)values(" + empidint + "," + leaveid + "," + Total_Leave + "," + mid + "," + yid + "," + currenttime.Ticks + "," + currenttime.Ticks + " ,0,0," + user_id + ") ");
                            }
                        }
                    }
                    if (val == 1)
                    {
                        ltrErr.Text = "Data Updated Successfully..";
                        btnsave.Visible = true;
                        // BindEmpLeave(mid, yid);
                    }

                    //mid = int.Parse(ddlmonthlist.SelectedIndex.ToString());
                    //mid = mid + 1;

                }
                BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), int.Parse(ddlgrouplist.SelectedIndex.ToString()));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 399: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }

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
                Logger.WriteCriticalLog("EmpLeaveConfiguration 422: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            saveData();
        }

        protected void ddlmonthlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mid = int.Parse(ddlmonthlist.SelectedIndex.ToString());
                mid = mid + 1;
                BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), int.Parse(ddlgrouplist.SelectedIndex.ToString()));
            }
            catch (Exception aa)
            {
                Logger.WriteCriticalLog("EmpLeaveConfiguration 439: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            saveData();
        }

        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid = int.Parse(ddlmonthlist.SelectedIndex.ToString());
            mid = mid + 1;
            BindEmpLeave(mid, int.Parse(ddlyear.SelectedValue.ToString()), int.Parse(ddlgrouplist.SelectedIndex.ToString()));
        }
    }
}