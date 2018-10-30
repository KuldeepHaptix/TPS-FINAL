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
    public partial class Employee_AssignToTimeGroup : System.Web.UI.Page
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
                        header.Text = "Employee Assign To Time Group";
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
                    BindSearchCriteria();
                    Search();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 52: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void BindSearchCriteria()
        {
            objmysqldb.ConnectToDatabase();
            try
            {

                DataTable dtschList = objmysqldb.GetData("Select School_List_ID,School_Name,School_Id from school_list where IsDelete=0 order by School_List_ID");

                ddlschool.DataSource = dtschList;
                ddlschool.DataTextField = "School_Name";
                ddlschool.DataValueField = "School_List_ID";
                ddlschool.DataBind();
                ddlschool.Items.Insert(0, new ListItem("Select Organization", "-1"));

                DataTable dtdepartment = objmysqldb.GetData("Select department_id,Department_Name from department_master where IsDelete=0 order by department_id");

                ddldepartment.DataSource = dtdepartment;
                ddldepartment.DataTextField = "Department_Name";
                ddldepartment.DataValueField = "department_id";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, new ListItem("Select Department", "1000"));


                DataTable dtdesignation = objmysqldb.GetData("Select designation_id,Designation_Name from designation_master where IsDelete=0 order by designation_id");
                ddlDesignation.DataSource = dtdesignation;
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "designation_id";
                ddlDesignation.DataBind();

                ddlDesignation.Items.Insert(0, new ListItem("Select Designation", "1000"));

                DataTable dttimegrp = objmysqldb.GetData("Select Group_id,Group_Name from group_master where IsDelete=0 order by Group_id");
                ddltimegrp.DataSource = dttimegrp;
                ddltimegrp.DataTextField = "Group_Name";
                ddltimegrp.DataValueField = "Group_id";
                ddltimegrp.DataBind();

                ddltimegrp.Items.Insert(0, new ListItem("Select Time Group", "1000"));

                objmysqldb.disposeConnectionObj();



            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 101: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void Search()
        {

            try
            {
                ltrErr.Text = "";
                DataTable dtEmpList = BindGrid();
                if (dtEmpList != null && dtEmpList.Rows.Count > 0)
                {
                    grd.DataSource = dtEmpList;
                    grd.DataBind();
                    btnsave.Visible = true; Button1.Visible = true;
                }
                else
                {
                    ltrErr.Visible = true;
                    ltrErr.Text = "Record dose not exists for selected criteria";
                    grd.DataSource = null;
                    grd.DataBind();
                    btnsave.Visible = false; Button1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 128: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        private DataTable BindGrid()
        {
            DataTable dtEmpList = new DataTable();
            DataTable dtdata = new DataTable();
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                dtEmpList = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone,EmpDeptID,EmpDesignId,school_list.School_List_ID,department_master.Department_Name,designation_master.Designation_Name FROM employee_master inner join  department_master on employee_master.EmpDeptID=department_master.department_id inner join designation_master on employee_master.EmpDesignId = designation_master.designation_id inner join  employee_assign_schoolwise  on employee_master.EmpId=employee_assign_schoolwise.Employee_ID inner join school_list on employee_assign_schoolwise.School_ID = school_list.School_ID  where employee_master.IsDelete=0 and employee_assign_schoolwise.IsDelete=0 and EmpStatusFlag=0");

                DataTable dtAllEmpList = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone,EmpDeptID,EmpDesignId,department_master.Department_Name,designation_master.Designation_Name FROM employee_master inner join  department_master on employee_master.EmpDeptID=department_master.department_id inner join designation_master on employee_master.EmpDesignId = designation_master.designation_id  where employee_master.IsDelete=0 and EmpStatusFlag=0");

                DataTable dttimegrp = objmysqldb.GetData("SELECT Time_group_assign_emp_id,emp_id,time_group_assign_emplyee_wise.Group_id,OutPuch_SMS,Absent_Sms,Late_SMS,Email_Sent,Group_name FROM time_group_assign_emplyee_wise left join group_master on  time_group_assign_emplyee_wise.Group_id=group_master.Group_id");



                if (dtEmpList != null && dtAllEmpList != null)
                {
                    #region  filter
                    int id = 0;
                    if (ddlschool.SelectedIndex > 0)
                    {
                        int.TryParse(ddlschool.Items[ddlschool.SelectedIndex].Value.ToString(), out id);
                        DataRow[] drvalue = dtEmpList.Select("School_List_ID = " + id + " ");
                        //dtEmpList = dtEmpList.Clone();
                        if (drvalue.Length > 0)
                        {
                            dtAllEmpList = drvalue.CopyToDataTable();
                        }
                    }
                    if (ddldepartment.SelectedIndex > 0)
                    {
                        int.TryParse(ddldepartment.Items[ddldepartment.SelectedIndex].Value.ToString(), out id);
                        DataRow[] drvalue = dtAllEmpList.Select("EmpDeptID = " + id + " ");
                        dtAllEmpList = dtAllEmpList.Clone();
                        if (drvalue.Length > 0)
                        {
                            dtAllEmpList = drvalue.CopyToDataTable();
                        }
                    }
                    if (ddlDesignation.SelectedIndex > 0)
                    {
                        int.TryParse(ddlDesignation.Items[ddlDesignation.SelectedIndex].Value.ToString(), out id);
                        DataRow[] drvalue = dtAllEmpList.Select("EmpDesignId = " + id + " ");
                        dtAllEmpList = dtAllEmpList.Clone();
                        if (drvalue.Length > 0)
                        {
                            dtAllEmpList = drvalue.CopyToDataTable();
                        }
                    }
                    if (!txtemployee.Text.Equals(""))
                    {
                        string values = "'%" + txtemployee.Text + "%'";
                        DataRow[] drvalue = dtAllEmpList.Select("FullName like (" + values + ") ");
                        dtAllEmpList = dtAllEmpList.Clone();
                        if (drvalue.Length > 0)
                        {
                            dtAllEmpList = drvalue.CopyToDataTable();
                        }
                    }
                    if (!txtphone.Text.Equals(""))
                    {
                        string values = "'%" + txtphone.Text + "%'";
                        DataRow[] drvalue = dtAllEmpList.Select("EmpPhone like (" + values + ") ");
                        dtAllEmpList = dtAllEmpList.Clone();
                        if (drvalue.Length > 0)
                        {
                            dtAllEmpList = drvalue.CopyToDataTable();
                        }
                    }
                    if (ddltimegrp.SelectedIndex > 0)
                    {
                        int.TryParse(ddltimegrp.Items[ddltimegrp.SelectedIndex].Value.ToString(), out id);
                        DataRow[] drvalue = dttimegrp.Select("Group_id = " + id + " ");
                        //dttimegrp = dttimegrp.Clone();
                        //if (drvalue.Length > 0)
                        //{
                        //    dttimegrp = drvalue.CopyToDataTable();
                        //}
                        string emp_id = "";
                        foreach (DataRow dr in drvalue)
                        {
                            emp_id += dr["emp_id"].ToString() + ",";
                        }
                        emp_id = emp_id.TrimEnd(',');
                        if (!emp_id.Equals(""))
                        {
                            DataRow[] drvalues = dtAllEmpList.Select("EmpId In (" + emp_id + ") ");
                            dtAllEmpList = dtAllEmpList.Clone();
                            if (drvalues.Length > 0)
                            {
                                dtAllEmpList = drvalues.CopyToDataTable();
                            }
                        }
                        else
                        {
                            dtAllEmpList = dtAllEmpList.Clone();
                        }

                    }
                    #endregion
                    dtAllEmpList = dtAllEmpList.DefaultView.ToTable(true, "EmpId", "FullName", "EmpPhone", "Department_Name", "Designation_Name");
                    dtAllEmpList.Columns.Add("group_id");
                    dtAllEmpList.Columns.Add("timegrp");
                    dtAllEmpList.Columns.Add("late");
                    dtAllEmpList.Columns.Add("absent");
                    dtAllEmpList.Columns.Add("outPunch");
                    dtAllEmpList.Columns.Add("Email");
                    foreach (DataRow drr in dtAllEmpList.Rows)
                    {
                        DataRow[] drdata = dttimegrp.Select("emp_id=" + int.Parse(drr["EmpId"].ToString()) + " ");
                        drr["group_id"] = "1000";
                        drr["timegrp"] = "Select Time Group";
                        drr["late"] = "0";
                        drr["absent"] = "0";
                        drr["outPunch"] = "0";
                        drr["Email"] = "0";
                        if (drdata.Length > 0)
                        {
                            string val = drdata[0]["Group_id"].ToString();
                            if (!drdata[0]["Group_id"].ToString().Equals("0"))
                            {
                                drr["group_id"] = drdata[0]["Group_id"].ToString();
                                drr["timegrp"] = drdata[0]["Group_name"].ToString();
                            }

                            drr["late"] = drdata[0]["Late_SMS"].ToString();
                            drr["absent"] = drdata[0]["Absent_Sms"].ToString();
                            drr["outPunch"] = drdata[0]["OutPuch_SMS"].ToString();
                            drr["Email"] = drdata[0]["Email_Sent"].ToString();
                        }
                    }
                    dtdata = dtAllEmpList.Copy();
                    //Gv data

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 272: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                objmysqldb.disposeConnectionObj();
            }
            return dtdata;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Savedata();
        }

        private void Savedata()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                objmysqldb.OpenSQlConnection();
                objmysqldb.BeginSQLTransaction();
                foreach (GridViewRow row in grd.Rows)
                {
                    int emp_id = 0;
                    string timegrp = "";
                    int late = 0;
                    int absent = 0;
                    int forgetoutPunch = 0;
                    int Email = 0;
                    int grp_id = 0;

                    Label emp = (Label)row.FindControl("lblEmp_id");
                    Label grp = (Label)row.FindControl("lblgrp_id");

                    CheckBox chkleft = (CheckBox)row.FindControl("cbSelectlate");
                    CheckBox chkabsent = (CheckBox)row.FindControl("cbSelectabsent");
                    CheckBox chkforgetOut = (CheckBox)row.FindControl("cbSelectOutpunch");
                    CheckBox chkEmail = (CheckBox)row.FindControl("cbSelectEmail");
                    HtmlSelect ddlgrp = (HtmlSelect)row.FindControl("ddltime");
                    if (chkleft.Checked)
                    {
                        late = 1;
                    }
                    if (chkabsent.Checked)
                    {
                        absent = 1;
                    }
                    if (chkforgetOut.Checked)
                    {
                        forgetoutPunch = 1;
                    }
                    if (chkEmail.Checked)
                    {
                        Email = 1;
                    }
                    int.TryParse(emp.Text.ToString(), out emp_id);
                    timegrp = ddlgrp.Value.ToString();
                    if (!timegrp.Equals("1000"))
                    {
                        int.TryParse(timegrp.ToString(), out grp_id);
                    }

                    DateTime currenttime = Logger.getIndiantimeDT();


                    if (grp_id > 0)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        int retval = objmysqldb.InsertUpdateDeleteData("update time_group_assign_emplyee_wise set Group_id=" + grp_id + ",OutPuch_SMS=" + forgetoutPunch + ",Absent_Sms=" + absent + ",Late_SMS=" + late + ",Email_Sent=" + Email + ",modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where IsDelete=0 and emp_id=" + emp_id + " ");
                        if (retval == 0)
                        {
                            retval = objmysqldb.InsertUpdateDeleteData("insert into time_group_assign_emplyee_wise (emp_id,Group_id,OutPuch_SMS,Absent_Sms,Late_SMS,Email_Sent,modify_datetime,DOC,IsDelete,IsUpdate,UserID) values (" + emp_id + " ," + grp_id + "," + forgetoutPunch + "," + absent + "," + late + "," + Email + "," + currenttime.Ticks + "," + currenttime.Ticks + ",0,1," + user_id + ")");

                            if (retval != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("TimeGroup_AssignToEmployee 345 Update error.");
                                objmysqldb.RollBackSQLTransaction();
                                return;
                            }
                        }
                        DataTable dtgroup = new DataTable();
                        dtgroup = objmysqldb.GetData("select * from group_master where Group_id=" + grp_id + "");
                        DataTable dtgrouptime = new DataTable();
                        dtgrouptime = objmysqldb.GetData("select * from groupwise_time_config where Group_Time_Id=" + grp_id + "");
                        if (dtgrouptime != null && dtgrouptime.Rows.Count > 0)
                        {
                            DateTime currenttimes = Logger.getIndiantimeDT();
                            retval = objmysqldb.InsertUpdateDeleteData("update employeewise_punchtime_details_datewise set IsDelete=1,modify_datetime=" +currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_id + "  where emp_id=" + emp_id + "");

                            long dateticks = 0;
                            long.TryParse(dtgroup.Rows[0]["Changes_Applicable_Date"].ToString(), out dateticks);
                            DateTime dtcur = MySQLDB.GetIndianTime();
                            DateTime dtcurrent = new DateTime(dtcur.Year, dtcur.Month, dtcur.Day);

                            long curdateticks = 0;

                            long ticks = long.Parse(dtgroup.Rows[0]["Changes_Applicable_Date"].ToString());
                            DateTime dt = new DateTime(ticks);
                            String myString_new = String.Format("{0:dd/MM/yyyy}", dt);

                            TimeSpan dtDiff = dt - dtcurrent;
                            DateTime dtstartdate = dtcurrent;
                            if (dtDiff.Days < 0)
                            {
                                dtDiff = dtcurrent - dt;
                                dtstartdate = dt;
                            }
                            
                            for (int d1 = 0; d1 <= dtDiff.Days; d1++)
                            {
                                DateTime dtt1 = dtstartdate.AddDays(d1);
                                //foreach (DataRow dr in dtdata.Rows)
                                int days1 = (int)dtt1.DayOfWeek;
                                if (days1 == 0)
                                {
                                    days1 = 7;
                                }
                                DataRow[] drdaywise1 = dtgrouptime.Select("Day_id =" + days1 + " ");
                                foreach (DataRow dr in drdaywise1)
                                {

                                    retval = objmysqldb.InsertUpdateDeleteData("insert into employeewise_punchtime_details_datewise (Dateticks,emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min,modify_datetime,DOC,IsUpdate,UserID,IsDelete) values (" + dtt1.Ticks + "," + emp_id + "," + int.Parse(dr["Day_id"].ToString()) + "," + int.Parse(dr["In_hour"].ToString()) + "," + int.Parse(dr["In_min"].ToString()) + "," + int.Parse(dr["out_hour"].ToString()) + "," + int.Parse(dr["out_min"].ToString()) + "," + int.Parse(dr["Ext_Early_hour"].ToString()) + "," + int.Parse(dr["Ext_Early_min"].ToString()) + "," + int.Parse(dr["Ext_Late_hour"].ToString()) + "," + int.Parse(dr["Ext_Late_min"].ToString()) + "," + currenttimes.Ticks + "," + currenttimes.Ticks + ",1," + user_id + ",0)");
                                }
                            }
                        }

                    }
                }
                objmysqldb.EndSQLTransaction();
                Search();
                ltrErr.Text = "Time Group Assign Successfully.";
            }
            catch (Exception ex)
            {
                objmysqldb.RollBackSQLTransaction();
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 360: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                objmysqldb.disposeConnectionObj();
                objmysqldb.CloseSQlConnection();
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (((DataRowView)e.Row.DataItem)["late"].ToString() == "1")
                    {
                        CheckBox chkleft = (CheckBox)e.Row.FindControl("cbSelectlate");
                        chkleft.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["absent"].ToString() == "1")
                    {
                        CheckBox chkleft = (CheckBox)e.Row.FindControl("cbSelectabsent");
                        chkleft.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["outPunch"].ToString() == "1")
                    {
                        CheckBox chkleft = (CheckBox)e.Row.FindControl("cbSelectOutpunch");
                        chkleft.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["Email"].ToString() == "1")
                    {
                        CheckBox chkleft = (CheckBox)e.Row.FindControl("cbSelectEmail");
                        chkleft.Checked = true;
                    }

                    //Find the DropDownList in the Row
                    HtmlSelect ddltime = (e.Row.FindControl("ddltime") as HtmlSelect);
                    if (ddltime != null)
                    {
                        DataTable dttimegrp = objmysqldb.GetData("Select Group_id,Group_Name from group_master where IsDelete=0 order by Group_id");
                        ddltime.DataSource = dttimegrp;
                        ddltime.DataTextField = "Group_Name";
                        ddltime.DataValueField = "Group_id";
                        ddltime.DataBind();

                        ddltime.Items.Insert(0, new ListItem("Select Time Group", "1000"));

                        //Select the Country of Customer in DropDownList
                        string rel = (e.Row.FindControl("lblgrp_id") as Label).Text;
                        ddltime.Items.FindByValue(rel).Selected = true;
                    }


                    //e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                    //e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");  

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_AssignToTimeGroup 428: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Savedata();
        }

    }
}