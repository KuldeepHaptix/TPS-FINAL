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
    public partial class Employee_Attendance : System.Web.UI.Page
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
                        header.Text = "Employee Attendance Entry";
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
                    DateTime dtt = MySQLDB.GetIndianTime();
                    string date = (dtt.Day.ToString().Length == 1 ? "0" + dtt.Day.ToString() : dtt.Day.ToString()) +
                        "/" +
                        (dtt.Month.ToString().Length == 1 ? "0" + dtt.Month.ToString() : dtt.Month.ToString())
                        + "/" +
                        dtt.Year;
                    txtattDate.Text = date;
                    int year = 0;
                    int month = 0;
                    int day = 0;
                    string[] arrdob = date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrdob.Length == 3)
                    {
                        int.TryParse(arrdob[2].ToString(), out year);
                        int.TryParse(arrdob[1].ToString(), out month);
                        int.TryParse(arrdob[0].ToString(), out day);
                        bindGvdata(year, month, day);
                    }
                    else
                    {
                        ltrErr.Text = "Attendance Date is not in proper format.";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance 73: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
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
                string date = txtattDate.Text.ToString();
                int year = 0;
                int month = 0;
                int day = 0;
                string[] arrdob = date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                long dateticks = 0;
                if (arrdob.Length == 3)
                {
                    int.TryParse(arrdob[2].ToString(), out year);
                    int.TryParse(arrdob[1].ToString(), out month);
                    int.TryParse(arrdob[0].ToString(), out day);
                    dateticks = new DateTime(year, month, day).Ticks;
                }
                if (arrdob.Length != 3 && dateticks == 0)
                {
                    ltrErr.Text = "Attendance Date is not in proper format.";
                    return;
                }
                DataTable dtAttendanceData = objmysqldb.GetData("select emp_id,DateTicks,Type,Overlap,Update_Type from employee_attendance_daily where  DateTicks=" + dateticks + " ");

                objmysqldb.OpenSQlConnection();
                objmysqldb.BeginSQLTransaction();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                string emp_ids = "";
                foreach (GridViewRow row in grd.Rows)
                {
                    int emp_id = 0;
                    int type = 0;
                    Label emp = (Label)row.FindControl("lblEmp_id");
                    //CheckBox chkpresent = (CheckBox)row.FindControl("cbSelectPresent");
                    RadioButton chkpresent = (RadioButton)row.FindControl("cbSelectPresent");
                    RadioButton chkhalfday = (RadioButton)row.FindControl("cbSelectHalfday");
                    RadioButton chkabsent = (RadioButton)row.FindControl("cbSelectabsent");
                    Label overrideatt = (Label)row.FindControl("lbloverride");
                    int attUpdate = 0;
                    int.TryParse(overrideatt.Text.ToString(), out attUpdate);
                    int.TryParse(emp.Text.ToString(), out emp_id);

                    //if (attUpdate == 1)
                    //{
                    //    continue;
                    //}
                    if (chkpresent.Checked)
                    {
                        type = 1;
                    }
                    if (chkhalfday.Checked)
                    {
                        if (type > 0)
                        {
                            ltrErr.Text = "Please enter valid Attendance";
                            objmysqldb.RollBackSQLTransaction();
                            bindGvdata(year, month, day);
                            return;
                        }
                        type = 2;
                    }
                    if (chkabsent.Checked)
                    {
                        if (type > 0)
                        {
                            ltrErr.Text = "Please enter valid Attendance";
                            objmysqldb.RollBackSQLTransaction();
                            bindGvdata(year, month, day);
                            return;
                        }
                        type = 3;
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    if (type > 0)//insert Update 
                    {
                        //tye same  old and nw?
                        DataRow[] drdata = dtAttendanceData.Select("emp_id=" + emp_id + "");
                        int retval = 0;
                        if (drdata.Length > 0)
                        {
                            int oldtype = 0;
                            int.TryParse(drdata[0]["Type"].ToString(), out oldtype);
                            if (type == oldtype)
                            {
                                retval = objmysqldb.InsertUpdateDeleteData(" update employee_attendance_daily set Type=" + type + ",UserID=" + user_id + ",modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,IsDelete=0 where emp_id=" + emp_id + " and DateTicks=" + dateticks + " ");
                            }
                            else
                            {
                                retval = objmysqldb.InsertUpdateDeleteData(" update employee_attendance_daily set Type=" + type + ",Overlap=1,Update_Type=" + oldtype + ",UserID=" + user_id + ",modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,IsDelete=0 where emp_id=" + emp_id + " and DateTicks=" + dateticks + " ");
                            }
                        }
                        if (retval == 0)
                        {
                            retval = objmysqldb.InsertUpdateDeleteData("insert into employee_attendance_daily (emp_id,DateTicks,Type,Overlap,Update_Type,UserID,modify_datetime,DOC,IsUpdate,IsDelete) values (" + emp_id + " ," + dateticks + "," + type + ",0,0," + user_id + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1,0)");

                            if (retval != 1)
                            {
                                ltrErr.Text = "please try again";
                                objmysqldb.RollBackSQLTransaction();
                                Logger.WriteCriticalLog("Employee_Attendance 178 Update error.");
                                return;
                            }
                        }
                        else
                        {
                            emp_ids += emp_id.ToString() + ",";
                        }
                    }
                }
                emp_ids = emp_ids.TrimEnd(',');

                if (!emp_ids.Equals("") && dtAttendanceData != null && dtAttendanceData.Rows.Count > 0)
                {
                    //string[] employees = emp_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DataRow[] drdata = dtAttendanceData.Select("emp_id NOT IN(" + emp_ids + ") ");
                    dtAttendanceData = dtAttendanceData.Clone();
                    if (drdata.Length > 0)
                    {
                        dtAttendanceData = drdata.CopyToDataTable();
                    }
                }
                DateTime currentTimes = Logger.getIndiantimeDT();
                foreach (DataRow dr in dtAttendanceData.Rows)
                {
                    int retval = objmysqldb.InsertUpdateDeleteData(" update employee_attendance_daily set UserID=" + user_id + ",modify_datetime=" + currentTimes.Ticks + ",IsUpdate=1,IsDelete=1 where emp_id=" + int.Parse(dr["emp_id"].ToString()) + " and DateTicks=" + dateticks + " ");
                }

                objmysqldb.EndSQLTransaction();
                bindGvdata(year, month, day);
                ltrErr.Text = "Employee Attendance save Successfully.";
            }
            catch (Exception ex)
            {
                objmysqldb.RollBackSQLTransaction();
                Logger.WriteCriticalLog("Employee_Attendance 215: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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

                    if (((DataRowView)e.Row.DataItem)["Present"].ToString() == "1")
                    {
                        //CheckBox chkpre = (CheckBox)e.Row.FindControl("cbSelectPresent");
                        RadioButton chkpre = (RadioButton)e.Row.FindControl("cbSelectPresent");
                        chkpre.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["HalfDay"].ToString() == "1")
                    {
                        RadioButton chkhalf = (RadioButton)e.Row.FindControl("cbSelectHalfday");
                        chkhalf.Checked = true;
                    }
                    if (((DataRowView)e.Row.DataItem)["Absent"].ToString() == "1")
                    {
                        RadioButton chkabsent = (RadioButton)e.Row.FindControl("cbSelectabsent");
                        chkabsent.Checked = true;
                    }
                }
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    if (((DataRowView)e.Row.DataItem)["override"].ToString() == "1")
                //    {
                //        e.Row.Enabled = false;
                //        //var linkButton = e.Row.FindControl("lnkdelete") as LinkButton;
                //        //linkButton.Enabled = false;
                //        //linkButton.OnClientClick = "";
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance 266:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void txtattDateChange(object sender, EventArgs e)
        {
            try
            {
                string date = txtattDate.Text.ToString();
                int year = 0;
                int month = 0;
                int day = 0;
                string[] arrdob = date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrdob.Length == 3)
                {
                    int.TryParse(arrdob[2].ToString(), out year);
                    int.TryParse(arrdob[1].ToString(), out month);
                    int.TryParse(arrdob[0].ToString(), out day);
                    bindGvdata(year, month, day);
                }
                else
                {
                    ltrErr.Text = "Attendance Date is not in proper format.";
                    return;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Attendance 295: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void bindGvdata(int year, int month, int day)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtAllEmpList = objmysqldb.GetData("SELECT  EmpId, Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone,EmpDeptID,EmpDesignId,department_master.Department_Name,designation_master.Designation_Name FROM employee_master inner join  department_master on employee_master.EmpDeptID=department_master.department_id inner join designation_master on employee_master.EmpDesignId = designation_master.designation_id  where employee_master.IsDelete=0 and EmpStatusFlag=0");
                DataColumn dc = new DataColumn("Present");
                dc.DefaultValue = "0";
                dtAllEmpList.Columns.Add(dc);

                DataColumn dc1 = new DataColumn("HalfDay");
                dc1.DefaultValue = "0";
                dtAllEmpList.Columns.Add(dc1);

                DataColumn dc2 = new DataColumn("Absent");
                dc2.DefaultValue = "0";
                dtAllEmpList.Columns.Add(dc2);

                DataColumn dc3 = new DataColumn("override");
                dc3.DefaultValue = "0";
                dtAllEmpList.Columns.Add(dc3);

                long ticks = new DateTime(year, month, day).Ticks;
                DataTable dtAttendanceData = objmysqldb.GetData("select emp_id,DateTicks,Type,Overlap,Update_Type from employee_attendance_daily where IsDelete=0 and DateTicks=" + ticks + " ");
                if (dtAttendanceData != null && dtAttendanceData.Rows.Count > 0)
                {
                    int Interprited = 0;
                    int emp_id = 0;
                    for (int i = 0; i < dtAttendanceData.Rows.Count; i++)
                    {
                        int.TryParse(dtAttendanceData.Rows[i]["emp_id"].ToString(), out emp_id);
                        DataRow[] drdatas = dtAllEmpList.Select("EmpId=" + emp_id + " ");
                        if (drdatas.Length > 0)
                        {
                            int.TryParse(dtAttendanceData.Rows[i]["Overlap"].ToString(), out Interprited);
                            int type = 0;
                            drdatas[0]["override"] = Interprited.ToString();
                            //if (Interprited == 0)
                            //{
                            int.TryParse(dtAttendanceData.Rows[i]["Type"].ToString(), out type);
                            //}
                            //else
                            //{
                            //    int.TryParse(dtAttendanceData.Rows[i]["Update_Type"].ToString(), out type);
                            //}
                            if (type == 1) //present
                            {
                                drdatas[0]["Present"] = "1";
                            }
                            else if (type == 2) //HalfDay
                            {
                                drdatas[0]["HalfDay"] = "1";
                            }
                            else if (type == 3) //Absent
                            {
                                drdatas[0]["Absent"] = "1";
                            }
                        }
                    }
                }
                if (dtAllEmpList != null && dtAllEmpList.Rows.Count > 0)
                {
                    grd.DataSource = dtAllEmpList;
                    grd.DataBind();
                    btnsave.Visible = true; Button1.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnsave.Visible = false; Button1.Visible = false;
                }

            }
            catch (Exception ex)
            {

                Logger.WriteCriticalLog("Employee_Attendance 375:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Savedata();
        }
    }
}