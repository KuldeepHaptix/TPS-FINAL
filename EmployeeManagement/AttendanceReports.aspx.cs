﻿using System;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Text;
using System.Net;


namespace EmployeeManagement
{
    public partial class AttendanceReports : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        DataTable dttotal1 = new DataTable();
        DataTable dttotal2 = new DataTable();
        string TimeIn12Format = DateTime.Now.ToString("hh:mm:ss tt");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int report = 0;
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;

                        int.TryParse(Request.QueryString["rep"].ToString(), out report);
                        reportIndex.Value = report.ToString();
                        #region display
                        if (report == 1)
                        {
                            header.Text = "Employee Monthly Attendance";
                            div0.Style["display"] = "block";
                            div3.Style["display"] = "none";
                            div4.Style["display"] = "none";
                            div5.Style["display"] = "none";
                            div10.Style["display"] = "none";
                            div20.Style["display"] = "none";
                        }
                        else if (report == 2 || report == 12)
                        {
                            if (report == 2)
                            {
                                header.Text = "Late Comers Report";
                                div0.Style["display"] = "block";
                                div1.Style["display"] = "none";
                                div2.Style["display"] = "none";
                                div3.Style["display"] = "none";
                                div10.Style["display"] = "none";
                                div20.Style["display"] = "block";
                                div24.Style["display"] = "none"; chlab.Checked = true; chlpre.Checked = true;
                            }
                            else
                            {
                                header.Text = "Present/Absent Report";
                                div0.Style["display"] = "block";
                                div1.Style["display"] = "none";
                                div2.Style["display"] = "none";
                                div3.Style["display"] = "none";
                                div10.Style["display"] = "none";
                                div20.Style["display"] = "block";
                                div24.Style["display"] = "none";
                                chlab.Visible = false; chlpre.Visible = false;
                            }
                        }
                        else if (report == 3)
                        {
                            header.Text = "Employeewise Monthly Attendance";
                            div0.Style["display"] = "none";
                            div10.Style["display"] = "block";
                            div20.Style["display"] = "none";
                        }
                        else if (report == 4)
                        {
                            header.Text = "Employee Total Working Hours Report";
                            div0.Style["display"] = "block";
                            div1.Style["display"] = "none";
                            div2.Style["display"] = "none";
                            div3.Style["display"] = "none";
                            div10.Style["display"] = "none";
                            div20.Style["display"] = "none";

                        }
                        else if (report == 5)
                        {
                            header.Text = "Monthwise Employee Attendance Report";
                            div0.Style["display"] = "none";
                            div10.Style["display"] = "block";
                            div11.Style["display"] = "none";
                            div12.Style["display"] = "none";
                            div13.Style["display"] = "none";
                            div20.Style["display"] = "none";
                        }

                        else if (report == 6)
                        {
                            header.Text = "Employee Monthly Salary";
                            div0.Style["display"] = "none";
                            div10.Style["display"] = "block";
                            div14.Style["display"] = "none";
                            div15.Style["display"] = "none";
                            div20.Style["display"] = "block";
                            div21.Style["display"] = "none";
                            div22.Style["display"] = "none";
                            div23.Style["display"] = "none";
                        }
                        else if (report == 7)
                        {
                            header.Text = "Employeewise Leave Details";
                            div0.Style["display"] = "block";
                            div3.Style["display"] = "none";
                            div4.Style["display"] = "none";
                            div5.Style["display"] = "none";
                            div10.Style["display"] = "none";
                            div20.Style["display"] = "none";
                        }
                        else if (report == 8)
                        {
                            header.Text = "Employee Monthly Leave Count Report";
                            div0.Style["display"] = "none";
                            div10.Style["display"] = "block";
                            div14.Style["display"] = "none";
                            div15.Style["display"] = "none";
                            div20.Style["display"] = "none";
                        }
                        else if (report == 9 || report == 13)
                        {
                            if (report == 9)
                            {
                                header.Text = "Employeewise Monthly Leave Count Report";
                                div0.Style["display"] = "none";
                                div10.Style["display"] = "block";
                                div20.Style["display"] = "none";
                            }
                            else
                            {
                                header.Text = "Employeewise Attendance Details Report";
                                div0.Style["display"] = "none";
                                div10.Style["display"] = "block";
                                div20.Style["display"] = "none";
                            }
                        }
                        else if (report == 10)
                        {
                            header.Text = "Daily Punch Detail";
                            div0.Style["display"] = "block";
                            div1.Style["display"] = "none";
                            div2.Style["display"] = "none";
                            div3.Style["display"] = "none";
                            div10.Style["display"] = "none";
                            div20.Style["display"] = "block";
                            div24.Style["display"] = "none"; chlab.Checked = true; chlpre.Checked = true;
                        }
                        else if (report == 11)
                        {
                            header.Text = "Employeewise Punch Detail";
                            div0.Style["display"] = "block";
                            div3.Style["display"] = "none";
                            div4.Style["display"] = "none";
                            div5.Style["display"] = "none";
                            div10.Style["display"] = "block";
                            div11.Style["display"] = "none";
                            div12.Style["display"] = "none";
                            div13.Style["display"] = "none";
                            div20.Style["display"] = "none";
                        }
                        tit.InnerText = header.Text;
                        txttitle.Text = header.Text;
                        #endregion
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
                    txttitle.Focus();

                    DateTime dtt = MySQLDB.GetIndianTime();
                    string date = (dtt.Day.ToString().Length == 1 ? "0" + dtt.Day.ToString() : dtt.Day.ToString()) +
                        "/" +
                        (dtt.Month.ToString().Length == 1 ? "0" + dtt.Month.ToString() : dtt.Month.ToString())
                        + "/" +
                        dtt.Year;
                    //fromdate.Text = date;//DateTime.Parse(MySQLDB.getIndiantime()).ToShortDateString().ToString();
                    todate.Text = date;//DateTime.Parse(MySQLDB.getIndiantime()).ToShortDateString().ToString();


                    bindempgrp();
                    bindMonthYear(date);
                    if (report == 3 || report == 5 || report == 9 || report == 11 || report == 13)
                    {
                        bindemp();
                        bindempgrp();
                        
                        //bindEmpGrpWise(emp_ids);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 194: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        private void bindempgrp()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtempgrp = objmysqldb.GetData("SELECT report_grp_id,report_grp_name,emp_ids FROM report_group_list where IsDelete=0");
                ddlgroup.DataSource = dtempgrp;
                ddlgroup.DataTextField = "report_grp_name";
                ddlgroup.DataValueField = "emp_ids";
                ddlgroup.DataBind();
                ddlgroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Employee group", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 211: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        private void bindemp()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtemp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and employee_master.FP_Id>0 order by  empid");
                ddlemp.DataSource = dtemp;
                ddlemp.DataTextField = "FullName";
                ddlemp.DataValueField = "empid";
                ddlemp.DataBind();
                ddlemp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Employee", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 233: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private void bindMonthYear(string date)
        {

            try
            {
                string[] curdate = date.Split('/');
                DataTable dtmonths = new DataTable();
                dtmonths.Columns.Add("id");
                dtmonths.Columns.Add("Name");
                dtmonths.Rows.Add("1", "January");
                dtmonths.Rows.Add("2", "February");
                dtmonths.Rows.Add("3", "March");
                dtmonths.Rows.Add("4", "April");
                dtmonths.Rows.Add("5", "May");
                dtmonths.Rows.Add("6", "June");
                dtmonths.Rows.Add("7", "July");
                dtmonths.Rows.Add("8", "August");
                dtmonths.Rows.Add("9", "September");
                dtmonths.Rows.Add("10", "October");
                dtmonths.Rows.Add("11", "November");
                dtmonths.Rows.Add("12", "December");
                ddlmonth.DataSource = dtmonths;
                ddlmonth.DataTextField = "Name";
                ddlmonth.DataValueField = "id";
                ddlmonth.DataBind();

                DataTable dtyears = new DataTable();
                dtyears.Columns.Add("id");
                dtyears.Columns.Add("Name");
                for (int i = 2010; i < 2031; i++)
                {
                    dtyears.Rows.Add(i.ToString(), i.ToString());
                }
                ddlyear.DataSource = dtyears;
                ddlyear.DataTextField = "Name";
                ddlyear.DataValueField = "id";
                ddlyear.DataBind();

                ddlmonth.SelectedIndex = ddlmonth.Items.IndexOf(ddlmonth.Items.FindByValue(curdate[1].ToString()));
                ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByValue(curdate[2].ToString()));
                //ddlmonth.Items.Insert(0, new ListItem("Select Month", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 285: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }

        [System.Web.Services.WebMethod]
        public static string getEmpList(string id)
        {
            MySQLDB dbc = new MySQLDB();
            try
            {
                string type = id;

                dbc.ConnectToDatabase();
                DataTable dtemp = new DataTable();
                if (id.Equals("-1") || id.Equals(""))
                {
                    dtemp = dbc.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid");
                }
                else
                {
                    dtemp = dbc.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where empid in(" + id + ")  order by  empid");
                }
                string response = "";
                for (int i = 0; i < dtemp.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        response = response + "-1,";
                        response = response + "Select Employee ###";
                    }
                    response = response + dtemp.Rows[i]["empid"].ToString() + ",";
                    response = response + dtemp.Rows[i]["FullName"].ToString() + "###";
                }
                response = response.TrimEnd('#');
                return response;

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 324: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                return "";
            }
            finally
            {
                dbc.disposeConnectionObj();
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
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                ltrErr.Text = "";
                int report = 0;
                int.TryParse(Request.QueryString["rep"].ToString(), out report);
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtheaderImg = new DataTable();
                if (emp_ids == "-1")
                {
                    dtheaderImg = objmysqldb.GetData("SELECT A4L_Img,LegelP_Img,LegelL_Img FROM report_group_list  WHERE IsDelete=0 and Is_Default=1 order by report_grp_id desc");
                }
                else
                {
                    string grpName = ddlgroup.Items[ddlgroup.SelectedIndex].Text.ToString();
                    dtheaderImg = objmysqldb.GetData("SELECT A4L_Img,LegelP_Img,LegelL_Img FROM report_group_list  WHERE IsDelete=0 and report_grp_name='" + grpName + "' order by report_grp_id desc");
                }
                string headerImg = HttpContext.Current.Server.MapPath("~/HeaderImages/") + "\\";

                if (report == 1)
                {
                    //string[] startdate = fromdate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] date = hdnDate.Value.ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (date.Length == 2)
                    {
                        string[] startdate = date[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] EndDate = date[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        long StartTicks = new DateTime(int.Parse(startdate[2]), int.Parse(startdate[1]), int.Parse(startdate[0])).Ticks;
                        long Endticks = new DateTime(int.Parse(EndDate[2]), int.Parse(EndDate[1]), int.Parse(EndDate[0])).Ticks;
                        if (StartTicks > 0 && Endticks > 0 && StartTicks <= Endticks)
                        {
                            EmployeeMonthlyAttendanceReport(StartTicks, Endticks);
                        }
                        else
                        {
                            ltrErr.Text = "Please Enter valid date range";
                        }
                    }
                    else
                    {
                        ltrErr.Text = "Please Enter valid date range";
                    }
                }
                else if (report == 2)
                {
                    string[] date = todate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    long dateTicks = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])).Ticks;
                    if (dateTicks > 0 && (chlpre.Checked || chlab.Checked))
                    {
                        if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                        {
                            headerImg += dtheaderImg.Rows[0]["A4L_Img"].ToString();
                        }
                        LateComersReport(date, headerImg, report);
                    }
                    else
                    {
                        ltrErr.Text = "Please Enter all valid details";
                    }
                }
                else if (report == 6)
                {
                    if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                    {
                        headerImg += dtheaderImg.Rows[0]["A4L_Img"].ToString();
                    }
                    GenerateEmployeeSalaryReport(headerImg, report);
                }
                else if (report == 8)
                {
                    //pending Leave calculation Table  after Complete this report
                    EmployeeMonthlyLeaveCountReport(headerImg);
                }
                else if (report == 9)
                {
                    if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                    {
                        headerImg += dtheaderImg.Rows[0]["LegelP_Img"].ToString();
                    }
                    EmployeeWiseMonthlyLeaveCountMethod(headerImg, report);
                }
                else if (report == 10)
                {
                    //DailyPuchingDetail();
                    string[] date = todate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    long dateTicks = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])).Ticks;
                    if (dateTicks > 0 && (chlpre.Checked || chlab.Checked))
                    {
                        if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                        {
                            headerImg += dtheaderImg.Rows[0]["LegelL_Img"].ToString();
                        }
                        DailyPuchingDetail(date, headerImg, report);
                    }
                    else
                    {
                        ltrErr.Text = "Please Enter all valid details";
                    }
                }
                else if (report == 11)
                {
                    //string[] date = todate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    string daterange = hdnDate.Value.ToString();
                    string[] date = daterange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    string startdate = date[0].ToString();
                    string[] startdatearray = startdate.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    string enddate = date[1].ToString();
                    string[] enddatearray = enddate.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    long startdateTicks = new DateTime(int.Parse(startdatearray[2]), int.Parse(startdatearray[1]), int.Parse(startdatearray[0])).Ticks;
                    long enddateTicks = new DateTime(int.Parse(enddatearray[2]), int.Parse(enddatearray[1]), int.Parse(enddatearray[0])).Ticks;
                    if (startdateTicks > 0 && enddateTicks > 0)
                    {
                        if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                        {
                            headerImg += dtheaderImg.Rows[0]["LegelL_Img"].ToString();
                        }
                        EmployeewisePunchDetail(startdateTicks, enddateTicks, headerImg, report);
                        lblDate.Text = daterange.ToString();
                    }
                }
                else if (report == 12)
                {
                    string date1 = todate.Text.ToString();
                    string[] date = todate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    long dateTicks = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])).Ticks;

                    if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                    {
                        headerImg += dtheaderImg.Rows[0]["A4L_Img"].ToString();
                    }
                    AbsentPresentDeatil(date1, headerImg, report);
                }
                else if (report == 13)
                {
                    if (dtheaderImg != null && dtheaderImg.Rows.Count > 0)
                    {
                        headerImg += dtheaderImg.Rows[0]["A4L_Img"].ToString();
                    }

                    EmployeeWiseAttaendanceReport(headerImg, report);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 506: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        #region not use work report 1
        private void EmployeeMonthlyAttendanceReport(long StartTicks, long Endticks)
        {
            objmysqldb.ConnectToDatabase();
            try
            {

                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtEmpList = new DataTable();
                DataTable dtEmpFPPunchData = new DataTable();
                Employee_Attendance(emp_ids, ref dtEmpList, ref dtEmpFPPunchData);

                dtEmpFPPunchData.Columns.Add("Ticks");
                for (int i = 0; i < dtEmpFPPunchData.Rows.Count; i++)
                {
                    String date = dtEmpFPPunchData.Rows[i]["Emp_day"].ToString() + "/" + dtEmpFPPunchData.Rows[i]["Emp_month"].ToString() + "/" + dtEmpFPPunchData.Rows[i]["Emp_year"].ToString();
                    long ticks = new DateTime(int.Parse(dtEmpFPPunchData.Rows[i]["Emp_year"].ToString()), int.Parse(dtEmpFPPunchData.Rows[i]["Emp_month"].ToString()), int.Parse(dtEmpFPPunchData.Rows[i]["Emp_day"].ToString())).Ticks;//DateTime.Parse(date.ToString()).Ticks;
                    dtEmpFPPunchData.Rows[i]["Ticks"] = ticks;
                }


                DataRow[] drEmpFPDetail = dtEmpFPPunchData.Select("Ticks>=" + StartTicks + " and Ticks<=" + Endticks, "Emp_hour,Emp_min,Emp_sec asc");
                if (drEmpFPDetail.Length > 0)
                {
                    dtEmpFPPunchData = drEmpFPDetail.CopyToDataTable();
                }
                else
                {
                    dtEmpFPPunchData = dtEmpFPPunchData.Clone();
                }

                //Sms config pending   total day count


                DataTable dt = new DataTable();
                dt.Columns.Add("intFPEmpId", Type.GetType("System.Int32"));
                dt.Columns.Add("FPEmpId");
                dt.Columns.Add("EmployeeName");
                dt.Columns.Add("EmpPhone");

                dt.Columns.Add("Total_Present_Days");
                dt.Columns.Add("Absent_Days");
                dt.Columns.Add("Fully_Present_Days");

                dt.Columns.Add("Late_Days");
                dt.Columns.Add("Early_Leaving_Days");
                dt.Columns.Add("Single_Punch_Days");
                dt.Columns.Add("Upto_LastMonth_AbsentDays");

                dt.Columns.Add("SortNumber", Type.GetType("System.Int32"));

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 174: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private void Employee_Attendance(string emp_ids, ref DataTable dtEmpList, ref DataTable dtEmpFPPunchData)
        {
            if (emp_ids.Equals("-1"))
            {
                dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpStatusFlag from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid");

                dtEmpFPPunchData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
            }
            else
            {
                dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpStatusFlag from  employee_master where empid in(" + emp_ids + ")  order by  empid");

                dtEmpFPPunchData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry where Emp_Id in(" + emp_ids + ") ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
            }
        }

        #endregion
        private void LateComersReport(string[] date, string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtEmpList = new DataTable();
                DataTable dtEmpFPPunchData = new DataTable();
                if (emp_ids.Equals("-1"))
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone,FP_Id,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.IsDelete=0 and EmpStatusFlag=0 and time_group_assign_emplyee_wise.IsDelete=0   order by  empid");

                    dtEmpFPPunchData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry  WHERE Emp_Attendance_Entry.Emp_day=" + int.Parse(date[0]) + " AND Emp_Attendance_Entry.Emp_month =" + int.Parse(date[1]) + " AND Emp_Attendance_Entry.Emp_year=" + int.Parse(date[2]) + " and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }
                else
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,EmpPhone,FP_Id,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id  where empid in(" + emp_ids + ") and time_group_assign_emplyee_wise.IsDelete=0 and employee_master.IsDelete=0 and employee_master.EmpStatusFlag=0 order by  empid");

                    dtEmpFPPunchData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry where Emp_Id in(" + emp_ids + ") and  (((Emp_Attendance_Entry.Emp_day)=" + date[0] + ") AND ((Emp_Attendance_Entry.Emp_month)=" + date[1] + ") AND ((Emp_Attendance_Entry.Emp_year)=" + date[2] + ")) and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");

                }
                DateTime dtdate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

                int intday = (int)dtdate.DayOfWeek;
                if (intday == 0)
                {
                    intday = 7;
                }
                DataTable dtgrplist = dtEmpList.DefaultView.ToTable(true, "Group_id");
                string grp_ids = "0,";
                //for (int i = 0; i < dtgrplist.Rows.Count; i++)
                //{
                //    grp_ids += dtgrplist.Rows[i]["Group_id"].ToString() + ",";
                //}

                grp_ids = grp_ids.TrimEnd(',');
                long ticks = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])).Ticks;

                //DataTable dtEmpTimeConfig = objmysqldb.GetData("SELECT groupwise_time_config.* FROM groupwise_time_config where Group_Time_Id in(" + grp_ids + ") and Day_id=" + intday + " ");
                DataTable dtEmpTimeConfig = new DataTable();
                if (emp_ids == "-1")
                {
                    dtEmpTimeConfig = objmysqldb.GetData("select employeewise_punchtime_details_datewise.* from employeewise_punchtime_details_datewise where  Day_id=" + intday + " and Dateticks=" + ticks + ";");
                }
                else
                {
                    dtEmpTimeConfig = objmysqldb.GetData("select employeewise_punchtime_details_datewise.* from employeewise_punchtime_details_datewise where emp_id  in(" + emp_ids.TrimEnd(',') + ")and Day_id=" + intday + " and Dateticks=" + ticks + ";");
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Emp Id", Type.GetType("System.Int32"));
                dt.Columns.Add("Employee Name");
                dt.Columns.Add("Mobile No.");

                dt.Columns.Add("Actual In Time");
                dt.Columns.Add("In Punch Time");
                dt.Columns.Add("Late By");

                dt.Columns.Add("Actual Out Time");
                dt.Columns.Add("Out Punch Time");
                dt.Columns.Add("Early By");
                dt.Columns.Add("Single");
                dt.Columns.Add("Ext Early");
                dt.Columns.Add("Ext Late");
                dt.Columns.Add("No Punch");
                dt.Columns.Add("SortNumber", Type.GetType("System.Int32"));

                foreach (DataRow dr in dtEmpList.Rows)
                {
                    int EmpId = int.Parse(dr["empid"].ToString());
                    string strfpempid = dr["FP_Id"].ToString();
                    string EmployeeName = dr["FullName"].ToString();
                    string EmpPhone = dr["EmpPhone"].ToString();
                    int grpid = int.Parse(dr["Group_id"].ToString());

                    if (strfpempid.Trim() != "" && strfpempid != "0")
                    {
                        int fpempid = int.Parse(strfpempid);
                        DataRow drnew = dt.NewRow();

                        drnew["Emp Id"] = fpempid;
                        drnew["Employee Name"] = EmployeeName;
                        drnew["Mobile No."] = EmpPhone;
                        drnew["SortNumber"] = EmpId;

                        #region Get In-Time Group Data

                        DataRow[] drfindtime = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");
                        int emphour_global = 0;
                        int empmin_global = 0;
                        if (drfindtime.Length > 0)
                        {

                            emphour_global = int.Parse(drfindtime[0]["In_hour"] != null && drfindtime[0]["In_hour"].ToString().Trim() != "" ? drfindtime[0]["In_hour"].ToString() : "0");
                            empmin_global = int.Parse(drfindtime[0]["In_min"] != null && drfindtime[0]["In_min"].ToString().Trim() != "" ? drfindtime[0]["In_min"].ToString() : "0");

                            int emphour = emphour_global;
                            int empmin = empmin_global;

                            if (emphour_global == 0 && empmin_global == 0)
                            {
                            }
                            else
                            {
                                string time = "";
                                getEmployeeTime(ref emphour, empmin, 0, ref time);
                                drnew["Actual In Time"] = time;//   + time +  ;
                            }
                        }

                        DataRow[] drfindpunchtime = dtEmpFPPunchData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        int punch_emphour_global = 0;
                        int punch_empmin_global = 0;
                        int punch_empsec_global = 0;

                        if (drfindpunchtime.Length > 0)
                        {
                            if (chlpre.Checked == false)
                            {
                                continue;
                            }
                            punch_emphour_global = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                            punch_empmin_global = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                            punch_empsec_global = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                            int emphour = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                            int empmin = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                            int Emp_sec = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                            string time = "";
                            getEmployeeTime(ref emphour, empmin, Emp_sec, ref time);
                            drnew["In Punch Time"] = time;//   + time +  ;

                            if (emphour_global == 0 && empmin_global == 0)
                            {
                                drnew["Late By"] = "-";//   + "-" +  ;
                            }
                            else
                            {
                                if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                {
                                    TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global.ToString() + ":" + punch_empmin_global.ToString());
                                    TimeSpan tm2 = TimeSpan.Parse(emphour_global.ToString() + ":" + empmin_global.ToString());
                                    TimeSpan difference = tm1 - tm2;

                                    int hours = difference.Hours;
                                    int minutes = difference.Minutes;
                                    string concat = "";

                                    if (hours == 0)
                                    {
                                        concat = "00:";
                                    }
                                    else
                                    {
                                        if (hours < 10)
                                        {
                                            concat = "0" + hours.ToString() + ":";
                                        }
                                        else
                                            concat = hours.ToString() + ":";
                                    }

                                    if (minutes == 0)
                                    {
                                        concat += "00:";
                                    }
                                    else
                                    {
                                        if (minutes < 10)
                                        {
                                            concat += "0" + minutes.ToString() + ":";
                                        }
                                        else
                                            concat += minutes.ToString() + ":";
                                    }

                                    if (punch_empsec_global == 0)
                                    {
                                        concat += "00";
                                    }
                                    else
                                    {
                                        if (punch_empsec_global < 10)
                                        {
                                            concat += "0" + punch_empsec_global.ToString();
                                        }
                                        else
                                            concat += punch_empsec_global.ToString();
                                    }
                                    drnew["Late By"] = concat;//   + concat +  ;
                                }
                                else
                                {
                                    drnew["Late By"] = "-";//   + "-" +  ;
                                }
                            }
                        }
                        else
                        {
                            if (chlab.Checked == false)
                            {
                                continue;
                            }
                            drnew["In Punch Time"] = "-";//   + "-" +  ;
                            drnew["Late By"] = "-";//  + "-" +  ;
                        }
                        #endregion

                        #region Get Out-Time Group Data

                        DataRow[] drfindtime_out = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");

                        int emphour_global_out = 0;
                        int empmin_global_out = 0;

                        if (drfindtime_out.Length > 0)
                        {
                            emphour_global_out = int.Parse(drfindtime_out[0]["out_hour"] != null && drfindtime_out[0]["out_hour"].ToString().Trim() != "" ? drfindtime_out[0]["out_hour"].ToString() : "0");
                            empmin_global_out = int.Parse(drfindtime_out[0]["out_min"] != null && drfindtime_out[0]["out_min"].ToString().Trim() != "" ? drfindtime_out[0]["out_min"].ToString() : "0");

                            int emphour = emphour_global_out;
                            int empmin = empmin_global_out;
                            if (emphour_global_out == 0 && empmin_global_out == 0)
                            {
                            }
                            else
                            {
                                string time = "";
                                getEmployeeTime(ref emphour, empmin, 0, ref time);
                                drnew["Actual Out Time"] = time;//   + time +  ;
                            }
                        }

                        DataRow[] drfindpunchtime_out = dtEmpFPPunchData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        int punch_emphour_global_out = 0;
                        int punch_empmin_global_out = 0;
                        int punch_empsec_global_out = 0;

                        if (drfindpunchtime_out.Length > 1)
                        {
                            //if (chkPreEmp.Checked == false)
                            //{
                            //    continue;
                            //}
                            punch_emphour_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                            punch_empmin_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                            punch_empsec_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                            int emphour = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                            int empmin = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                            int Emp_sec = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                            string time = "";
                            getEmployeeTime(ref emphour, empmin, Emp_sec, ref time);
                            drnew["Out Punch Time"] = time;//   + time +  ;

                            if (emphour_global_out == 0 && empmin_global_out == 0)
                            {
                                drnew["Early By"] = "-";//   + "-" +  ;
                            }
                            else
                            {
                                if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                {
                                    TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global_out.ToString() + ":" + punch_empmin_global_out.ToString());
                                    TimeSpan tm2 = TimeSpan.Parse(emphour_global_out.ToString() + ":" + empmin_global_out.ToString());
                                    TimeSpan difference = tm2 - tm1;

                                    int hours = difference.Hours;
                                    int minutes = difference.Minutes;
                                    int second = 60 - punch_empsec_global_out;

                                    string concat = "";

                                    if (hours == 0)
                                    {
                                        concat = "00:";
                                    }
                                    else
                                    {
                                        if (hours < 10)
                                        {
                                            concat = "0" + hours.ToString() + ":";
                                        }
                                        else
                                            concat = hours.ToString() + ":";
                                    }

                                    if (minutes == 0)
                                    {
                                        concat += "00:";
                                    }
                                    else
                                    {
                                        if (minutes < 10)
                                        {
                                            concat += "0" + minutes.ToString() + ":";
                                        }
                                        else
                                            concat += minutes.ToString() + ":";
                                    }

                                    if (second == 0)
                                    {
                                        concat += "00";
                                    }
                                    else
                                    {
                                        if (second < 10)
                                        {
                                            concat += "0" + second.ToString();
                                        }
                                        else
                                            concat += second.ToString();
                                    }

                                    drnew["Early By"] = concat;//   + concat +  ;
                                }
                                else
                                {
                                    drnew["Early By"] = "-";//   + "-" +  ;
                                }
                            }
                        }
                        else
                        {
                            //if (chkAbsentEmp.Checked == false)
                            //{
                            //    continue;
                            //}
                            drnew["Out Punch Time"] = "-";//  + "-" +  ;
                            drnew["Early By"] = "-";//  + "-" +  ;
                        }

                        #endregion

                        #region Get Extreme Early Time
                        string punchintime = "-";
                        string punchouttime = "-";
                        string actualintime = "-";
                        string actualouttime = "-";
                        string Elate = "-";
                        string Eearly = "-";
                        string singlepunch = "-";

                        DataRow[] drfindEmpPunchtime = dtEmpFPPunchData.Select("Emp_id=" + EmpId + " and Emp_Fp_id='" + strfpempid + "' and Emp_day=" + int.Parse(date[0]) + " and Emp_month=" + int.Parse(date[1]) + " and Emp_year=" + int.Parse(date[2]), "Emp_hour,Emp_min,Emp_sec asc");
                        if (drfindEmpPunchtime.Length > 0)
                        {
                            punch_emphour_global = int.Parse(drfindEmpPunchtime[0]["Emp_hour"].ToString());
                            punch_empmin_global = int.Parse(drfindEmpPunchtime[0]["Emp_min"].ToString());
                            punch_empsec_global = int.Parse(drfindEmpPunchtime[0]["Emp_sec"].ToString());
                            int inPunch = punch_emphour_global;
                            getEmployeeTime(ref inPunch, punch_empmin_global, punch_empsec_global, ref punchintime);

                            if (drfindpunchtime.Length == 1)
                            {
                                singlepunch = "Yes";
                                drnew["Single"] = singlepunch;
                            }
                            else
                            {
                                drnew["Single"] = singlepunch;
                            }
                        }
                        if (drfindEmpPunchtime.Length > 1)
                        {
                            punch_emphour_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_hour"].ToString());
                            punch_empmin_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_min"].ToString());
                            punch_empsec_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_sec"].ToString());
                            int outPunch = punch_emphour_global_out;
                            getEmployeeTime(ref outPunch, punch_empmin_global_out, punch_empsec_global_out, ref punchouttime);
                        }

                        string strdays = dtdate.DayOfWeek.ToString();
                        int intdays = intday;

                        DataRow[] drfindActualtimes = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");

                        int EL_emphour_global = 0;
                        int EL_empmin_global = 0;

                        int EE_emphour_global_out = 0;
                        int EE_empmin_global_out = 0;

                        if (drfindActualtimes.Length > 0)
                        {
                            emphour_global = int.Parse(drfindActualtimes[0]["In_hour"] != null && drfindActualtimes[0]["In_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["In_hour"].ToString() : "0");
                            empmin_global = int.Parse(drfindActualtimes[0]["In_min"] != null && drfindActualtimes[0]["In_min"].ToString().Trim() != "" ? drfindActualtimes[0]["In_min"].ToString() : "0");
                            int intactualintime = emphour_global;
                            getEmployeeTime(ref intactualintime, empmin_global, 0, ref actualintime);

                            emphour_global_out = int.Parse(drfindActualtimes[0]["out_hour"] != null && drfindActualtimes[0]["out_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["out_hour"].ToString() : "0");
                            empmin_global_out = int.Parse(drfindActualtimes[0]["out_min"] != null && drfindActualtimes[0]["out_min"].ToString().Trim() != "" ? drfindActualtimes[0]["out_min"].ToString() : "0");

                            int intactualouttime = emphour_global_out;
                            getEmployeeTime(ref intactualouttime, empmin_global_out, 0, ref actualouttime);

                            EE_emphour_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_hour"] != null && drfindActualtimes[0]["Ext_Early_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_hour"].ToString() : "0");

                            EE_empmin_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_min"] != null && drfindActualtimes[0]["Ext_Early_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_min"].ToString() : "0");

                            EL_emphour_global = int.Parse(drfindActualtimes[0]["Ext_Late_hour"] != null && drfindActualtimes[0]["Ext_Late_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_hour"].ToString() : "0");
                            EL_empmin_global = int.Parse(drfindActualtimes[0]["Ext_Late_min"] != null && drfindActualtimes[0]["Ext_Late_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_min"].ToString() : "0");
                        }
                        if ((emphour_global == 0 && empmin_global == 0) || (punch_emphour_global == 0 && punch_empmin_global == 0) || (EL_emphour_global == 0 && EL_empmin_global == 0))
                        {
                        }
                        else
                        {
                            if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                            {
                                if ((punch_emphour_global > EL_emphour_global) || (punch_emphour_global == EL_emphour_global && punch_empmin_global >= EL_empmin_global))
                                {
                                    Elate = "Yes";
                                    drnew["Ext Late"] = Elate;
                                }
                                else
                                {
                                    drnew["Ext Late"] = Elate;
                                }
                            }
                            else
                            {
                                drnew["Ext Late"] = Elate;
                            }
                        }
                        if ((emphour_global_out == 0 && empmin_global_out == 0) || (punch_emphour_global_out == 0 && punch_empmin_global_out == 0))
                        {
                        }
                        else
                        {
                            if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                            {
                                if ((punch_emphour_global_out < EE_emphour_global_out) || (punch_emphour_global_out == EE_emphour_global_out && punch_empmin_global_out < EE_empmin_global_out))
                                {
                                    Eearly = "Yes";
                                    drnew["Ext Early"] = Eearly;
                                    drnew["Ext Late"] = Elate;
                                }
                                else
                                {
                                    drnew["Ext Early"] = Eearly;
                                    drnew["Ext Late"] = Elate;
                                }
                            }
                            else
                            {
                                drnew["Ext Early"] = Eearly;
                                drnew["Ext Late"] = Elate;
                            }
                        }
                        if (punch_emphour_global == 0 && punch_empmin_global == 0 && punch_empsec_global == 0 && punch_emphour_global_out == 0 && punch_empmin_global_out == 0 && punch_empsec_global_out == 0)
                        {
                            drnew["No Punch"] = "Yes";
                            drnew["Ext Early"] = Eearly;
                            drnew["Ext Late"] = Elate;
                            drnew["Single"] = singlepunch;
                        }
                        else
                        {
                            drnew["No Punch"] = "-";
                            drnew["Ext Early"] = Eearly;
                            drnew["Ext Late"] = Elate;
                            drnew["Single"] = singlepunch;
                        }

                        #endregion
                        dt.Rows.Add(drnew);
                    }
                }
                dt.Columns["Ext Late"].ColumnName = "E.L";
                dt.Columns["Ext Early"].ColumnName = "E.E";
                ViewState["GVDATA"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    grd.DataSource = dt;
                    grd.DataBind();
                    btnExport.Visible = true;
                    dt.Columns.Remove("SortNumber");
                    #region Pdf code

                    Document doc = new Document();
                    doc = new Document(PageSize.A4.Rotate());     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dt.Columns.Count);
                    string rpt = "~/Reports/";
                    string filePath = HttpContext.Current.Server.MapPath(rpt);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }

                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;
                    DateTime now = DateTime.Now;
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<b><hr  width='100%'></b>");
                    // string dt1= DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    string dt1 = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Late Comers Report</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Report Time: </b> " + dt1.ToString() + "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Date: </b> " + dtdate.ToString("dd/MM/yyyy").Substring(0, 10) + "</td></tr>");
                    //sb.Append("<tr ><td align = 'left'><b>Report Time: </b> " + MySQLDB.GetIndianTime().ToString("dd/MM/yyyy").Substring(0, 10) + "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Date: </b> " + dtdate.ToString("dd/MM/yyyy").Substring(0, 10)+ "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\Late_Comers_Report.pdf", FileMode.Create));

                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dt, report));
                    doc.Add(paragraphfotter);
                    // Closing the document
                    doc.Close();

                    //string finalpathnew = HttpContext.Current.Server.MapPath(rpt) + "\\Late_Comers_Report.pdf";

                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports";
                    test = test + "//Late_Comers_Report.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);


                    #endregion

                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 1098: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, DataTable dtEmpList, int report)
        {
            if (report == 13)
            {
                double widths = 100 / 4;
                if (widths > 0)
                {
                    float[] headers = new float[dtEmpList.Columns.Count];  //Header Widths
                    for (int k = 0; k < headers.Length; k++)
                    {
                        headers[k] = float.Parse(widths.ToString());
                    }
                    tableLayout.SetWidths(headers);        //Set the pdf headers
                }

                tableLayout.WidthPercentage = 50;
                //tableLayout.AddCell(new PdfPCell(new Phrase(" Date :04/10/2017                                                  Employee List                           ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //Add header
                for (int i = 0; i < dtEmpList.Columns.Count; i++)
                {
                    AddCellToHeader(tableLayout, dtEmpList.Columns[i].ColumnName.ToString(),report);
                }
                for (int i = 0; i < dtEmpList.Rows.Count; i++)
                {
                    for (int j = 0; j < dtEmpList.Columns.Count; j++)
                    {
                        AddCellToBody(tableLayout, dtEmpList.Rows[i][j].ToString(),report);
                    }
                }

            }
            else if (report == 12)
            {
                double widths = 100 /8;
                if (widths > 0)
                {
                    float[] headers = new float[dtEmpList.Columns.Count];  //Header Widths
                    for (int k = 0; k < headers.Length; k++)
                    {
                        headers[k] = float.Parse(widths.ToString());
                    }
                    tableLayout.SetWidths(headers);        //Set the pdf headers
                }

                tableLayout.WidthPercentage = 100;
                //tableLayout.AddCell(new PdfPCell(new Phrase(" Date :04/10/2017                                                  Employee List                           ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //Add header
                for (int i = 0; i < dtEmpList.Columns.Count; i++)
                {
                    AddCellToHeader(tableLayout, dtEmpList.Columns[i].ColumnName.ToString(),report);
                }
                for (int i = 0; i < dtEmpList.Rows.Count; i++)
                {
                    for (int j = 0; j < dtEmpList.Columns.Count; j++)
                    {
                        AddCellToBody(tableLayout, dtEmpList.Rows[i][j].ToString(),report);
                    }
                }
            }
            else
            {
                double widths = 100 / dtEmpList.Columns.Count;
                if (widths > 0)
                {
                    float[] headers = new float[dtEmpList.Columns.Count];  //Header Widths
                    for (int k = 0; k < headers.Length; k++)
                    {
                        headers[k] = float.Parse(widths.ToString());
                    }
                    tableLayout.SetWidths(headers);        //Set the pdf headers
                }
                tableLayout.WidthPercentage = 100;
                //tableLayout.AddCell(new PdfPCell(new Phrase(" Date :04/10/2017                                                  Employee List                           ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //tableLayout.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.HELVETICA, 13, 1, new Color(153, 51, 0)))) { Colspan = dtEmpList.Columns.Count, Border = 0, PaddingBottom = 20, HorizontalAlignment = Element.ALIGN_CENTER });

                //Add header
                for (int i = 0; i < dtEmpList.Columns.Count; i++)
                {
                    AddCellToHeader(tableLayout, dtEmpList.Columns[i].ColumnName.ToString(),report);
                }
                for (int i = 0; i < dtEmpList.Rows.Count; i++)
                {
                    for (int j = 0; j < dtEmpList.Columns.Count; j++)
                    {
                        AddCellToBody(tableLayout, dtEmpList.Rows[i][j].ToString(),report);
                    }
                }
               
            }
            return tableLayout;
        }
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText,int report_ID)
        {
            if (report_ID == 12)
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA,12, 1, iTextSharp.text.Color.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, BackgroundColor = new iTextSharp.text.Color(0, 51, 102) });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA, 9, 1, iTextSharp.text.Color.WHITE))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = new iTextSharp.text.Color(0, 51, 102) });
            }
        }

        // Method to add single cell to the body
        private static void AddCellToBody(PdfPTable tableLayout, string cellText,int report_id)
        {
            if (report_id == 12)
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA, 12, 1, iTextSharp.text.Color.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, BackgroundColor = iTextSharp.text.Color.WHITE });
            }
            else
            {

                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.HELVETICA, 8, 1, iTextSharp.text.Color.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 1, BackgroundColor = iTextSharp.text.Color.WHITE });
            }
        }
        public void getEmployeeTime(ref int emphour, int empmin, int second, ref string time)
        {
            string strsecond = "00";
            if (second != 0)
            {
                strsecond = second.ToString();
            }
            if (emphour >= 12)
            {
                if (emphour > 12)
                    emphour = emphour - 12;

                string hour = "";
                if (emphour < 10)
                {
                    hour = "0" + emphour.ToString();
                }
                else
                {
                    hour = emphour.ToString();
                }

                string min = "";
                if (empmin < 10)
                {
                    min = "0" + empmin.ToString();
                }
                else
                {
                    min = empmin.ToString();
                }

                time = hour + ":" + min + ":" + strsecond + " PM";
            }
            else
            {
                string hour = "";
                if (emphour < 10)
                {
                    hour = "0" + emphour.ToString();
                }
                else
                {
                    hour = emphour.ToString();
                }

                string min = "";
                if (empmin < 10)
                {
                    min = "0" + empmin.ToString();
                }
                else
                {
                    min = empmin.ToString();
                }
                time = hour + ":" + min.ToString() + ":" + strsecond + " AM";
            }
        }

        private void GenerateEmployeeSalaryReport(string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtEmpList = new DataTable();
                if (emp_ids.Equals("-1"))
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,PayScale,FP_Id from  employee_master  where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid");
                }
                else
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,PayScale,FP_Id from  employee_master where empid in(" + emp_ids + ") and employee_master.IsDelete=0 and employee_master.EmpStatusFlag=0  order by  empid");
                }

                DataTable dtLeaveList = objmysqldb.GetData("SELECT Leave_Id,Leave_Name,Is_CL_Leave FROM employee_management.leave_master where IsDelete=0;");
                int monthid = 0;
                int.TryParse(ddlmonth.Items[ddlmonth.SelectedIndex].Value.ToString(), out monthid);

                int year = 0;
                int.TryParse(ddlyear.Items[ddlyear.SelectedIndex].Value.ToString(), out year);
                int total_days_in_month = DateTime.DaysInMonth(year, monthid);

                string startdate = "01/" + monthid + "/" + year;
                string enddate = "" + total_days_in_month + "/" + monthid + "/" + year;

                string[] arrr = { "empid", "FP_Id", "EmployeeName", "PayScale" };
                DataTable dt = dtEmpList.DefaultView.ToTable(true, arrr).Copy();

                DataRow[] drfind = dt.Select("FP_Id<>0 or FP_Id<>null");
                DataTable dtExport = dt.Clone();
                if (drfind.Length > 0)
                {
                    dtExport = drfind.CopyToDataTable();
                }
                DataView dv = dtExport.DefaultView;
                dv.Sort = "empid ASC";
                dtExport = dv.ToTable();

                int colpos = 3;
                Hashtable hsColPos = new System.Collections.Hashtable();

                string leavenames = "";
                foreach (DataRow dr in dtLeaveList.Rows)
                {
                    colpos++;
                    string leave_name = dr["Leave_Name"].ToString();
                    leavenames += leave_name + ",";
                    hsColPos.Add(int.Parse(dr["Leave_Id"].ToString()), colpos);
                    dtExport.Columns.Add("Total " + leave_name);
                }
                char[] ch1 = { ',' };
                string[] splt = leavenames.Split(ch1, StringSplitOptions.RemoveEmptyEntries);

                foreach (string leave_name in splt)
                {
                    dtExport.Columns.Add(leave_name);
                }
                colpos++;
                hsColPos.Add(-1, colpos);
                dtExport.Columns.Add("LWP");
                colpos++;
                hsColPos.Add(-2, colpos);
                dtExport.Columns.Add("Applicable Salary");
                //string monthids = "";

                DataTable dtCurrentMonthLeaves = objmysqldb.GetData("SELECT * FROM employee_management.leave_history_monthwise where Month_Id=" + monthid + " and Month_Year=" + year + " and IsDelete=0;");
                DataTable dtAllPreviousMonthLeaves = getpreviosMonth(objmysqldb, monthid, year);

                #region emp wise calculate data
                foreach (DataRow dr in dtExport.Rows)
                {
                    int empid = int.Parse(dr["empid"].ToString());
                    double PayScale = 0.0;
                    double.TryParse(dr["PayScale"].ToString(), out PayScale);
                    if (chksal.Checked && PayScale > 0)
                    {
                        //dr["EmployeeName"] = dr["EmployeeName"].ToString() + "&nbsp<b>[" + dr["PayScale"].ToString() + "]</b>";
                        dr["EmployeeName"] = dr["EmployeeName"].ToString() + " [ " + dr["PayScale"].ToString() + " ] ";
                    }

                    double singledaysalary = (double)PayScale / 30;
                    double TotalCL = 0.0;
                    double TotalML = 0.0;
                    double TotalLWP = 0.0;
                    double CurrCL = 0.0;
                    double CurrML = 0.0;
                    double CurrLWP = 0.0;
                    double dbluptoThisMonth = 0.0;
                    double dblThisMonth = 0.0;
                    int indexin = 0;
                    foreach (DataRow drleave in dtLeaveList.Rows)
                    {
                        int leaveid = int.Parse(drleave["Leave_Id"].ToString());
                        indexin = (int)hsColPos[leaveid];
                        string leave_name = drleave["Leave_Name"].ToString();

                        if (drleave["Is_CL_Leave"].ToString().Equals("1"))
                        {
                            object objPrevCLSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + " AND Leave_Id=" + leaveid + "");
                            object objCurrCLSum = dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + " AND Leave_Id=" + leaveid + "");
                            CurrCL = double.Parse(objCurrCLSum != null && objCurrCLSum.ToString() != "" ? objCurrCLSum.ToString() : "0.0");
                            TotalCL = double.Parse(objPrevCLSum != null && objPrevCLSum.ToString() != "" ? objPrevCLSum.ToString() : "0.0");
                            dbluptoThisMonth = CurrCL + TotalCL;
                            dblThisMonth = CurrCL;
                        }
                        else if (drleave["Is_CL_Leave"].ToString().Equals("0"))
                        {
                            object objPrevMLSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + "AND Leave_Id=" + leaveid + "");
                            object objCurrMLSum = dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + "AND Leave_Id=" + leaveid + "");
                            CurrML = double.Parse(objCurrMLSum != null && objCurrMLSum.ToString() != "" ? objCurrMLSum.ToString() : "0.0");
                            TotalML = double.Parse(objPrevMLSum != null && objPrevMLSum.ToString() != "" ? objPrevMLSum.ToString() : "0.0");
                            dbluptoThisMonth = CurrML + TotalML;
                            dblThisMonth = CurrML;
                        }
                        dr["Total " + leave_name] = dbluptoThisMonth;//   + dbluptoThisMonth +  ;
                        dr[leave_name] = dblThisMonth;//   + dblThisMonth +  ;
                    }
                    object objPrevLWPSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + " and Leave_Id=-1");
                    object objCurrLWPSum = dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + empid + " and Leave_Id=-1");
                    CurrLWP = double.Parse(objCurrLWPSum != null && objCurrLWPSum.ToString() != "" ? objCurrLWPSum.ToString() : "0.0");
                    TotalLWP = double.Parse(objPrevLWPSum != null && objPrevLWPSum.ToString() != "" ? objPrevLWPSum.ToString() : "0.0");
                    dbluptoThisMonth = CurrLWP + TotalLWP;
                    dblThisMonth = CurrLWP;
                    indexin = (int)hsColPos[-1];
                    //dr[indexin] =   + dbluptoThisMonth +  ;
                    dr["LWP"] = dblThisMonth;//   + dblThisMonth +  ;

                    int indexout = (int)hsColPos[-1];


                    double dblThisMonthout = dblThisMonth;//CurrCL + CurrML + CurrLWP;
                    // dr[indexout + dtLeaveList.Rows.Count] =   + dblThisMonthout +  ;

                    indexout = (int)hsColPos[-2];
                    double appsalary = PayScale - (dblThisMonthout * singledaysalary);
                    dr["Applicable Salary"] = convertStringToCurrency(RoundfloatingToInteger(appsalary).ToString());// "<p align='RIGHT'>Rs. " + convertStringToCurrency(RoundfloatingToInteger(appsalary).ToString()) + "</p>";
                    dr["PayScale"] = convertStringToCurrency(RoundfloatingToInteger(PayScale).ToString());// "<p align='RIGHT'>Rs. " + convertStringToCurrency(RoundfloatingToInteger(PayScale).ToString()) + "</p>";
                }
                #endregion
                dtExport.Columns["PayScale"].SetOrdinal(dtExport.Columns.Count - 2);
                dtExport = dtExport.DefaultView.ToTable(true, "FP_Id", "EmployeeName", "PayScale", "Applicable Salary", "ML", "CL", "LWP", "Total ML", "Total CL");
                dtExport.Columns["FP_Id"].ColumnName = "FP Id";
                dtExport.Columns["EmployeeName"].ColumnName = "Employee Name";
                ViewState["GVDATA"] = dtExport;
                if (dtExport != null && dtExport.Rows.Count > 0)
                {

                    grd.DataSource = dtExport;
                    grd.DataBind();
                    btnExport.Visible = true;

                    #region Pdf code
                    string rpt = "~/Reports/";
                    string filePath = HttpContext.Current.Server.MapPath(rpt);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    Document doc = new Document();
                    doc = new Document(PageSize.A4.Rotate());     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dtExport.Columns.Count);
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }


                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;

                    StringBuilder sb = new StringBuilder();
                    string aa = ddlmonth.Items[ddlmonth.SelectedIndex].Text.ToString() + " - " + ddlyear.Items[ddlyear.SelectedIndex].Text.ToString();
                    //Generate Invoice (Bill) Header.
                    sb.Append("<b><hr  width='100%'></b>");
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Employee Monthly Salary</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Month: </b>" + aa + " </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\Employee_Monthly_Salary.pdf", FileMode.Create));

                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dtExport, report));
                    doc.Add(paragraphfotter);
                    //byte[] b = File.ReadAllBytes(Server.MapPath("Late_Comers_Report.pdf"));
                    // Closing the document
                    doc.Close();

                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\Employee_Monthly_Salary.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports";
                    test = test + "//Employee_Monthly_Salary.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);

                    #endregion

                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 1435: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private DataTable getpreviosMonth(MySQLDB objmysqldb, int monthid, int year)
        {
            DataTable dt = new DataTable();
            try
            {
                DataTable dtholidayList = objmysqldb.GetData("SELECT leave_history_monthwise.* FROM leave_history_monthwise where  IsDelete=0");
                dt = dtholidayList.Clone();
                long ticks = new DateTime(year, monthid, 1).Ticks;
                DataTable dtacademicyear = objmysqldb.GetData("SELECT Academic_Year,Start_Date, DATE_FORMAT(DATE_ADD('0001-01-01 00:00:00', INTERVAL Start_Date/10000000 SECOND_MICROSECOND), '%d/%m/%Y')as st_date,End_Date,DATE_FORMAT(DATE_ADD('0001-01-01 00:00:00', INTERVAL End_Date/10000000 SECOND_MICROSECOND), '%d/%m/%Y')as en_date FROM academicyear where Start_Date <= " + ticks + " and End_Date >=" + ticks + ";");
                if (dtacademicyear != null && dtacademicyear.Rows.Count > 0)
                {
                    string StartDate = dtacademicyear.Rows[0]["st_date"].ToString();
                    string EndDate = dtacademicyear.Rows[0]["en_date"].ToString();
                    string[] stdate = StartDate.Split('/');
                    string[] endate = EndDate.Split('/');

                    Hashtable hs = new Hashtable();
                    if (StartDate != null && StartDate.Length >= 9 && EndDate != null && EndDate.Length >= 9 && stdate.Length == 3 && endate.Length == 3)
                    {

                        if (stdate[2].ToString().Equals(endate[2].ToString()))
                        {
                            for (int j = int.Parse(stdate[1]); j <= int.Parse(endate[1]); j++)
                            {
                                if (j < monthid)
                                {
                                    if (hs.ContainsKey(stdate[2].ToString()))
                                    {
                                        hs[stdate[2]] = hs[stdate[2]].ToString() + "," + j;
                                    }
                                    else
                                    {
                                        hs.Add(stdate[2].ToString(), j);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = int.Parse(stdate[1]); j < 13; j++)
                            {
                                if (int.Parse(stdate[2].ToString()) < year || (j < monthid && int.Parse(stdate[2].ToString()) == year))
                                {
                                    if (hs.ContainsKey(stdate[2].ToString()))
                                    {
                                        hs[stdate[2]] = hs[stdate[2]].ToString() + "," + j;
                                    }
                                    else
                                    {
                                        hs.Add(stdate[2].ToString(), j);
                                    }
                                }
                            }
                            for (int j = 1; j <= int.Parse(endate[1]); j++)
                            {
                                if (int.Parse(endate[2].ToString()) < year || (j < monthid && int.Parse(endate[2].ToString()) == year))
                                {
                                    if (hs.ContainsKey(endate[2].ToString()))
                                    {
                                        hs[endate[2]] = hs[endate[2]].ToString() + "," + j;
                                    }
                                    else
                                    {
                                        hs.Add(endate[2].ToString(), j);
                                    }
                                }
                            }

                        }
                    }


                    foreach (object obj in hs.Keys)
                    {
                        object objval = hs[obj];
                        DataRow[] drdata = dtholidayList.Select("Month_Id In(" + objval.ToString() + ") and Month_Year=" + int.Parse(obj.ToString()) + " ");
                        if (drdata.Length > 0)
                        {
                            dt.Merge(drdata.CopyToDataTable());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dt;
        }

        private void DailyPuchingDetail(string[] date, string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
            DataTable dtEmpList = new DataTable();
            DataTable dtEmpFPData = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                if (emp_ids.Equals("-1"))
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,FP_Id as FPEmpId,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.IsDelete=0 and EmpStatusFlag=0 and time_group_assign_emplyee_wise.IsDelete=0   order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry  WHERE Emp_Attendance_Entry.Emp_day=" + int.Parse(date[0]) + " AND Emp_Attendance_Entry.Emp_month =" + int.Parse(date[1]) + " AND Emp_Attendance_Entry.Emp_year=" + int.Parse(date[2]) + " and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }
                else
                {
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,FP_Id as FPEmpId,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id  where empid in(" + emp_ids + ") and time_group_assign_emplyee_wise.IsDelete=0 and employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry where Emp_Id in(" + emp_ids + ") and  (((Emp_Attendance_Entry.Emp_day)=" + date[0] + ") AND ((Emp_Attendance_Entry.Emp_month)=" + date[1] + ") AND ((Emp_Attendance_Entry.Emp_year)=" + date[2] + ")) and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");

                }

                DataTable dtEmpTimeConfig = new DataTable();

                int intday = 0;
                DateTime dtdate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                intday = (int)dtdate.DayOfWeek;
                if (intday == 0)
                {
                    intday = 7;
                }
                DataTable dtgrplist = dtEmpList.DefaultView.ToTable(true, "Group_id");
                string grp_ids = "0,";
                string empids = "0,";
                if (emp_ids == "-1")
                {
                    for (int i = 0; i < dtEmpList.Rows.Count; i++)
                    {
                        emp_ids += dtEmpList.Rows[i]["empid"].ToString() + ",";
                    }
                }
                //for (int i = 0; i < dtgrplist.Rows.Count; i++)
                //{
                //    grp_ids += dtgrplist.Rows[i]["Group_id"].ToString() + ",";
                //}
                grp_ids = grp_ids.TrimEnd(',');
                long ticks = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])).Ticks;

                dtEmpTimeConfig = objmysqldb.GetData("select employeewise_punchtime_details_datewise.* from employeewise_punchtime_details_datewise where emp_id  in(" + emp_ids.TrimEnd(',') + ")and Day_id=" + intday + " and Dateticks=" + ticks + ";");
                //dtEmpTimeConfig = objmysqldb.GetData("SELECT employee_punchtime_details_datewise.* FROM employee_punchtime_details_datewise where time_grp_id in(" + grp_ids + ") and Day_id=" + intday + " and Dateticks=" + ticks + ";");

                dt.Columns.Add("FPEmpId", Type.GetType("System.Int32"));
                dt.Columns.Add("EmployeeName");
                dt.Columns.Add("EmpPhone");
                dt.Columns.Add("Actual_In_Time");
                dt.Columns.Add("In_Punch_Time");
                dt.Columns.Add("Late_By");
                dt.Columns.Add("Out_Time_1");
                dt.Columns.Add("In_Time_1");
                dt.Columns.Add("Out_Time_2");
                dt.Columns.Add("In_Time_2");
                dt.Columns.Add("Out_Time_3");
                dt.Columns.Add("In_Time_3");
                dt.Columns.Add("Actual_Out_Time");
                dt.Columns.Add("Out_Punch_Time");
                dt.Columns.Add("Early_By");
                dt.Columns.Add("Single_Punch");
                dt.Columns.Add("Ext_Early");
                dt.Columns.Add("Ext_Late");
                dt.Columns.Add("No_Punch");
                dt.Columns.Add("SortNumber", Type.GetType("System.Int32"));

                foreach (DataRow dr in dtEmpList.Rows)
                {
                    int EmpId = int.Parse(dr["EmpId"].ToString());
                    string strfpempid = dr["FPEmpId"].ToString();
                    string EmployeeName = dr["EmployeeName"].ToString();
                    string EmpPhone = dr["EmpPhone"].ToString();
                    int grpid = int.Parse(dr["Group_id"].ToString());

                    if (strfpempid.Trim() != "" && strfpempid != "0")
                    {
                        int fpempid = int.Parse(strfpempid);
                        DataRow drnew = dt.NewRow();

                        drnew["FPEmpId"] = fpempid;
                        drnew["EmployeeName"] = EmployeeName;
                        drnew["EmpPhone"] = EmpPhone;
                        drnew["SortNumber"] = EmpId;

                        #region Get In-Time Group Data

                        DataRow[] drfindtime = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");
                        int emphour_global = 0;
                        int empmin_global = 0;

                        if (drfindtime.Length > 0)
                        {

                            emphour_global = int.Parse(drfindtime[0]["In_hour"] != null && drfindtime[0]["In_hour"].ToString().Trim() != "" ? drfindtime[0]["In_hour"].ToString() : "0");
                            empmin_global = int.Parse(drfindtime[0]["In_min"] != null && drfindtime[0]["In_min"].ToString().Trim() != "" ? drfindtime[0]["In_min"].ToString() : "0");

                            int emphour = emphour_global;
                            int empmin = empmin_global;

                            if (emphour_global == 0 && empmin_global == 0)
                            {
                            }
                            else
                            {
                                string time = "";
                                //XGlobals.getEmployeeTime(ref emphour, empmin, 0, ref time);
                                getEmployeeTime(ref emphour, empmin, 0, ref time);
                                //  drnew["Actual_In_Time"] =   + time +  ;
                                drnew["Actual_In_Time"] = time;
                            }
                        }


                        DataRow[] drfindpunchtime = dtEmpFPData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        int punch_emphour_global = 0;
                        int punch_empmin_global = 0;
                        int punch_empsec_global = 0;

                        if (drfindpunchtime.Length > 0)
                        {
                            if (chlpre.Checked == false)
                            {
                                continue;
                            }
                            punch_emphour_global = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                            punch_empmin_global = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                            punch_empsec_global = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                            int emphour = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                            int empmin = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                            int Emp_sec = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                            string time = "";
                            getEmployeeTime(ref emphour, empmin, Emp_sec, ref time);
                            drnew["In_Punch_Time"] = time;//  + time +  ;


                            if (emphour_global == 0 && empmin_global == 0)
                            {
                                drnew["Late_By"] = "-";
                            }
                            else
                            {
                                if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                {
                                    TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global.ToString() + ":" + punch_empmin_global.ToString());
                                    TimeSpan tm2 = TimeSpan.Parse(emphour_global.ToString() + ":" + empmin_global.ToString());
                                    TimeSpan difference = tm1 - tm2;

                                    int hours = difference.Hours;
                                    int minutes = difference.Minutes;
                                    string concat = "";

                                    if (hours == 0)
                                    {
                                        concat = "00:";
                                    }
                                    else
                                    {
                                        if (hours < 10)
                                        {
                                            concat = "0" + hours.ToString() + ":";
                                        }
                                        else
                                            concat = hours.ToString() + ":";
                                    }

                                    if (minutes == 0)
                                    {
                                        concat += "00:";
                                    }
                                    else
                                    {
                                        if (minutes < 10)
                                        {
                                            concat += "0" + minutes.ToString() + ":";
                                        }
                                        else
                                            concat += minutes.ToString() + ":";
                                    }

                                    if (punch_empsec_global == 0)
                                    {
                                        concat += "00";
                                    }
                                    else
                                    {
                                        if (punch_empsec_global < 10)
                                        {
                                            concat += "0" + punch_empsec_global.ToString();
                                        }
                                        else
                                            concat += punch_empsec_global.ToString();
                                    }
                                    drnew["Late_By"] = concat;
                                }
                                else
                                {
                                    drnew["Late_By"] = "-";
                                }
                            }
                        }
                        else
                        {
                            if (chlab.Checked == false)
                            {
                                continue;
                            }
                            drnew["In_Punch_Time"] = "-";
                            drnew["Late_By"] = "-";
                        }

                        #endregion

                        #region Get Out-Time Group Data

                        DataRow[] drfindtime_out = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");

                        int emphour_global_out = 0;
                        int empmin_global_out = 0;

                        if (drfindtime_out.Length > 0)
                        {
                            emphour_global_out = int.Parse(drfindtime_out[0]["out_hour"] != null && drfindtime_out[0]["out_hour"].ToString().Trim() != "" ? drfindtime_out[0]["out_hour"].ToString() : "0");
                            empmin_global_out = int.Parse(drfindtime_out[0]["out_min"] != null && drfindtime_out[0]["out_min"].ToString().Trim() != "" ? drfindtime_out[0]["out_min"].ToString() : "0");

                            int emphour = emphour_global_out;
                            int empmin = empmin_global_out;
                            if (emphour_global_out == 0 && empmin_global_out == 0)
                            {
                            }
                            else
                            {
                                string time = "";
                                getEmployeeTime(ref emphour, empmin, 0, ref time);
                                drnew["Actual_Out_Time"] = time;
                            }
                        }

                        DataRow[] drfindpunchtime_out = dtEmpFPData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        int punch_emphour_global_out = 0;
                        int punch_empmin_global_out = 0;
                        int punch_empsec_global_out = 0;

                        if (drfindpunchtime_out.Length > 1)
                        {
                            //if (chkPreEmp.Checked == false)
                            //{
                            //    continue;
                            //}
                            punch_emphour_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                            punch_empmin_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                            punch_empsec_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                            int emphour = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                            int empmin = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                            int Emp_sec = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                            string time = "";
                            getEmployeeTime(ref emphour, empmin, Emp_sec, ref time);
                            drnew["Out_Punch_Time"] = time;

                            DataRow[] drxx = dtEmpFPData.Select("Emp_id=" + EmpId + " and Emp_Fp_id='" + strfpempid + "' and Emp_day=" + dtdate.Day + " and Emp_month=" + dtdate.Month + " and Emp_year=" + dtdate.Year, "Emp_hour,Emp_min,Emp_sec asc");
                            DataTable dtt = new DataTable();
                            dtt.Columns.Add("all");


                            if (drxx.Length > 2)
                            {
                                int i = 0;
                                for (i = 1; i < drxx.Length - 1; i++)
                                {
                                    int punchh = int.Parse(drxx[i]["Emp_hour"].ToString());
                                    int punchm = int.Parse(drxx[i]["Emp_min"].ToString());
                                    int punchs = int.Parse(drxx[i]["Emp_sec"].ToString());
                                    // DataTable dtt = new DataTable();

                                    string tm1 = (punchh.ToString() + ":" + punchm.ToString() + ":" + punchs.ToString());
                                    DataRow drx = dtt.NewRow();
                                    drx["all"] = tm1.ToString();
                                    dtt.Rows.Add(drx);
                                }
                                string out1 = "";
                                string in1 = "";
                                string out2 = "";
                                string in2 = "";
                                string out3 = "";
                                string in3 = "";




                                for (int j = 0; j < dtt.Rows.Count; j++)
                                {
                                    out1 = dtt.Rows[0]["all"].ToString();
                                    if (dtt.Rows.Count > 1)
                                    {
                                        in1 = dtt.Rows[1]["all"].ToString();
                                    }
                                    if (dtt.Rows.Count > 2)
                                    {
                                        out2 = dtt.Rows[2]["all"].ToString();
                                        if (dtt.Rows.Count > 3)
                                        {
                                            in2 = dtt.Rows[3]["all"].ToString();
                                        }
                                    }
                                    if (dtt.Rows.Count > 4)
                                    {
                                        out3 = dtt.Rows[4]["all"].ToString();
                                        if (dtt.Rows.Count > 5)
                                        {
                                            in3 = dtt.Rows[5]["all"].ToString();
                                        }
                                    }
                                }
                                if (out1 != null)
                                {
                                    drnew["Out_Time_1"] = out1.ToString();
                                }
                                else { drnew["Out_Time_1"] = "-"; }

                                if (in1 != "")
                                {
                                    drnew["In_Time_1"] = in1.ToString();
                                }
                                else if (drnew["In_Time_1"] == "")
                                {
                                    drnew["In_Time_1"] = out1.ToString();
                                }
                                else { drnew["In_Time_1"] = "-"; }

                                if (out2 != "")
                                {
                                    drnew["Out_Time_2"] = out2.ToString();
                                }
                                else if (drnew["Out_Time_2"] == "")
                                {
                                    drnew["Out_Time_2"] = in1.ToString();
                                }
                                else { drnew["Out_Time_2"] = "-"; }

                                if (in2 != "")
                                {
                                    drnew["In_Time_2"] = in2.ToString();
                                }
                                else if (drnew["In_Time_2"] == "")
                                {
                                    drnew["In_Time_2"] = out2.ToString();
                                }
                                else
                                {
                                    drnew["In_Time_2"] = "-";
                                }

                                if (out3 != "")
                                {
                                    drnew["Out_Time_3"] = out3.ToString();
                                }
                                else if (drnew["Out_Time_3"] == "")
                                {
                                    drnew["Out_Time_3"] = in2.ToString();
                                }
                                else { drnew["Out_Time_3"] = "-"; }

                                if (in3 != "")
                                {
                                    drnew["In_Time_3"] = in3.ToString();
                                }
                                else if (drnew["In_Time_3"] == "")
                                {
                                    drnew["In_Time_3"] = out3.ToString();
                                }
                                else { drnew["In_Time_3"] = "-"; }
                            }

                            if (emphour_global_out == 0 && empmin_global_out == 0)
                            {
                                drnew["Early_By"] = "-";
                            }
                            else
                            {
                                if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                {
                                    TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global_out.ToString() + ":" + punch_empmin_global_out.ToString());
                                    TimeSpan tm2 = TimeSpan.Parse(emphour_global_out.ToString() + ":" + empmin_global_out.ToString());
                                    TimeSpan difference = tm2 - tm1;

                                    int hours = difference.Hours;
                                    int minutes = difference.Minutes;
                                    int second = 60 - punch_empsec_global_out;

                                    string concat = "";

                                    if (hours == 0)
                                    {
                                        concat = "00:";
                                    }
                                    else
                                    {
                                        if (hours < 10)
                                        {
                                            concat = "0" + hours.ToString() + ":";
                                        }
                                        else
                                            concat = hours.ToString() + ":";
                                    }

                                    if (minutes == 0)
                                    {
                                        concat += "00:";
                                    }
                                    else
                                    {
                                        if (minutes < 10)
                                        {
                                            concat += "0" + minutes.ToString() + ":";
                                        }
                                        else
                                            concat += minutes.ToString() + ":";
                                    }

                                    if (second == 0)
                                    {
                                        concat += "00";
                                    }
                                    else
                                    {
                                        if (second < 10)
                                        {
                                            concat += "0" + second.ToString();
                                        }
                                        else
                                            concat += second.ToString();
                                    }

                                    drnew["Early_By"] = concat;
                                }
                                else
                                {
                                    drnew["Early_By"] = "-";
                                }
                            }
                        }
                        else
                        {
                            //if (chkAbsentEmp.Checked == false)
                            //{
                            //    continue;
                            //}
                            drnew["Out_Punch_Time"] = "-";
                            drnew["Early_By"] = "-";
                        }

                        #endregion

                        #region Get Extreme Early Time
                        string punchintime = "-";
                        string punchouttime = "-";
                        string actualintime = "-";
                        string actualouttime = "-";
                        string Elate = "-";
                        string Eearly = "-";
                        string singlepunch = "-";

                        DataRow[] drfindEmpPunchtime = dtEmpFPData.Select("Emp_id=" + EmpId + " and Emp_Fp_id='" + strfpempid + "' and Emp_day=" + dtdate.Day + " and Emp_month=" + dtdate.Month + " and Emp_year=" + dtdate.Year, "Emp_hour,Emp_min,Emp_sec asc");
                        if (drfindEmpPunchtime.Length > 0)
                        {
                            punch_emphour_global = int.Parse(drfindEmpPunchtime[0]["Emp_hour"].ToString());
                            punch_empmin_global = int.Parse(drfindEmpPunchtime[0]["Emp_min"].ToString());
                            punch_empsec_global = int.Parse(drfindEmpPunchtime[0]["Emp_sec"].ToString());
                            int inPunch = punch_emphour_global;
                            getEmployeeTime(ref inPunch, punch_empmin_global, punch_empsec_global, ref punchintime);

                            if (drfindpunchtime.Length == 1)
                            {
                                singlepunch = "Yes";
                                drnew["Single_Punch"] = singlepunch;
                            }
                            else
                            {
                                drnew["Single_Punch"] = singlepunch;
                            }
                        }

                        if (drfindEmpPunchtime.Length > 1)
                        {
                            punch_emphour_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_hour"].ToString());
                            punch_empmin_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_min"].ToString());
                            punch_empsec_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_sec"].ToString());
                            int outPunch = punch_emphour_global_out;
                            getEmployeeTime(ref outPunch, punch_empmin_global_out, punch_empsec_global_out, ref punchouttime);
                        }

                        string strdays = dtdate.DayOfWeek.ToString();
                        //int intdays = 0;
                        int intdays = intday;
                        //XGlobals.getCurrentDayNameFromSystem(strdays, ref intdays);
                        DataRow[] drfindActualtimes = dtEmpTimeConfig.Select("emp_id=" + EmpId + "");

                        int EL_emphour_global = 0;
                        int EL_empmin_global = 0;

                        int EE_emphour_global_out = 0;
                        int EE_empmin_global_out = 0;

                        if (drfindActualtimes.Length > 0)
                        {
                            emphour_global = int.Parse(drfindActualtimes[0]["In_hour"] != null && drfindActualtimes[0]["In_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["In_hour"].ToString() : "0");
                            empmin_global = int.Parse(drfindActualtimes[0]["In_min"] != null && drfindActualtimes[0]["In_min"].ToString().Trim() != "" ? drfindActualtimes[0]["In_min"].ToString() : "0");
                            int intactualintime = emphour_global;
                            getEmployeeTime(ref intactualintime, empmin_global, 0, ref actualintime);

                            emphour_global_out = int.Parse(drfindActualtimes[0]["out_hour"] != null && drfindActualtimes[0]["out_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["out_hour"].ToString() : "0");
                            empmin_global_out = int.Parse(drfindActualtimes[0]["out_min"] != null && drfindActualtimes[0]["out_min"].ToString().Trim() != "" ? drfindActualtimes[0]["out_min"].ToString() : "0");

                            int intactualouttime = emphour_global_out;
                            getEmployeeTime(ref intactualouttime, empmin_global_out, 0, ref actualouttime);

                            EE_emphour_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_hour"] != null && drfindActualtimes[0]["Ext_Early_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_hour"].ToString() : "0");

                            EE_empmin_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_min"] != null && drfindActualtimes[0]["Ext_Early_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_min"].ToString() : "0");

                            EL_emphour_global = int.Parse(drfindActualtimes[0]["Ext_Late_hour"] != null && drfindActualtimes[0]["Ext_Late_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_hour"].ToString() : "0");
                            EL_empmin_global = int.Parse(drfindActualtimes[0]["Ext_Late_min"] != null && drfindActualtimes[0]["Ext_Late_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_min"].ToString() : "0");
                        }

                        if ((emphour_global == 0 && empmin_global == 0) || (punch_emphour_global == 0 && punch_empmin_global == 0) || (EL_emphour_global == 0 && EL_empmin_global == 0))
                        {
                        }
                        else
                        {
                            if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                            {
                                if ((punch_emphour_global > EL_emphour_global) || (punch_emphour_global == EL_emphour_global && punch_empmin_global >= EL_empmin_global))
                                {
                                    Elate = "Yes";
                                    drnew["Ext_Late"] = Elate;
                                }
                                else
                                {
                                    drnew["Ext_Late"] = Elate;
                                }
                            }
                            else
                            {
                                drnew["Ext_Late"] = Elate;
                            }
                        }
                        if ((emphour_global_out == 0 && empmin_global_out == 0) || (punch_emphour_global_out == 0 && punch_empmin_global_out == 0))
                        {
                        }
                        else
                        {
                            if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                            {
                                if ((punch_emphour_global_out < EE_emphour_global_out) || (punch_emphour_global_out == EE_emphour_global_out && punch_empmin_global_out < EE_empmin_global_out))
                                {
                                    Eearly = "Yes";
                                    drnew["Ext_Early"] = Eearly;
                                    drnew["Ext_Late"] = Elate;
                                }
                                else
                                {
                                    drnew["Ext_Early"] = Eearly;
                                    drnew["Ext_Late"] = Elate;
                                }
                            }
                            else
                            {
                                drnew["Ext_Early"] = Eearly;
                                drnew["Ext_Late"] = Elate;
                            }
                        }

                        if (punch_emphour_global == 0 && punch_empmin_global == 0 && punch_empsec_global == 0 && punch_emphour_global_out == 0 && punch_empmin_global_out == 0 && punch_empsec_global_out == 0)
                        {
                            drnew["No_Punch"] = "Yes";
                            drnew["Ext_Early"] = Eearly;
                            drnew["Ext_Late"] = Elate;
                            drnew["Single_Punch"] = singlepunch;
                        }
                        else
                        {
                            drnew["No_Punch"] = "-";
                            drnew["Ext_Early"] = Eearly;
                            drnew["Ext_Late"] = Elate;
                            drnew["Single_Punch"] = singlepunch;
                        }
                        #endregion

                        dt.Rows.Add(drnew);
                    }
                }
                DataView dv = dt.DefaultView;
                dv.Sort = "SortNumber ASC, FPEmpId";
                dt = dv.ToTable();

                DataTable dtGeneralDetail = new DataTable();
                dtGeneralDetail.Columns.Add("Report_Time");

                DataRow dtGen = dtGeneralDetail.NewRow();
                dtGen["Report_Time"] = MySQLDB.GetIndianTime().ToString("dd/MM/yyyy").Substring(0, 10);
                dtGeneralDetail.Rows.Add(dtGen);

                DataSet ds = new DataSet();
                dt.TableName = "dtLateComesAttendance";
                dtGeneralDetail.TableName = "dtGeneralDetail";
                ds.Tables.Add(dt);
                ds.Tables.Add(dtGeneralDetail);
                dt.Columns["FPEmpId"].ColumnName = "Emp Id";
                dt.Columns["EmployeeName"].ColumnName = "Employee Name";
                dt.Columns["EmpPhone"].ColumnName = "Mobile No.";
                dt.Columns["Actual_In_Time"].ColumnName = "Actual In Time";
                dt.Columns["In_Punch_Time"].ColumnName = "In Punch Time";
                dt.Columns["Late_By"].ColumnName = "Late By";
                dt.Columns["Out_Time_1"].ColumnName = "Out Time 1";
                dt.Columns["In_Time_1"].ColumnName = "In Time 1";
                dt.Columns["Out_Time_2"].ColumnName = "Out Time 2";
                dt.Columns["In_Time_2"].ColumnName = "In Time 2";
                dt.Columns["Out_Time_3"].ColumnName = "Out Time 3";
                dt.Columns["In_Time_3"].ColumnName = "In Time 3";
                dt.Columns["Actual_Out_Time"].ColumnName = "Actual Out Time";
                dt.Columns["Out_Punch_Time"].ColumnName = "Out Punch Time";
                dt.Columns["Early_By"].ColumnName = "Early By";
                dt.Columns["Single_Punch"].ColumnName = "Single";
                dt.Columns["Ext_Early"].ColumnName = "E.E";
                dt.Columns["Ext_Late"].ColumnName = "E.L";
                dt.Columns["No_Punch"].ColumnName = "No Punch";

                ViewState["GVDATA"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    grd.DataSource = dt;
                    grd.DataBind();
                    btnExport.Visible = true;

                    #region Pdf code
                    dt.Columns.Remove("sortNumber");

                    string rpt = "~/Reports/";
                    string filePath = HttpContext.Current.Server.MapPath(rpt);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    Document doc = new Document();
                    doc = new Document(PageSize.LEGAL.Rotate());     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dt.Columns.Count);
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }


                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;

                    StringBuilder sb = new StringBuilder();
                    //Generate Invoice (Bill) Header.
                    sb.Append("<b><hr  width='100%'></b>");
                    string dt2 = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Daily Punch Details</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Report Time: </b> " + dt2.ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Date: </b> " + dtdate.ToString("dd/MM/yyyy").Substring(0, 10) + "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\Daily_Punch_Detail.pdf", FileMode.Create));

                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dt, report));
                    doc.Add(paragraphfotter);
                    // Closing the document
                    doc.Close();

                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\Daily_Punch_Detail.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports";
                    test = test + "//Daily_Punch_Detail.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);

                    #endregion
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ee)
            {
                Logger.WriteCriticalLog("AttendanceReports 2239: exception:" + ee.Message + "::::::::" + ee.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private void EmployeewisePunchDetail(long startdateticks, long enddateticks, string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dt = new DataTable();
                DataTable dtEmpList = new DataTable();
                DataTable dtEmpFPData = new DataTable();
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();

                dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,FP_Id as FPEmpId,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.IsDelete=0 and EmpStatusFlag=0 and time_group_assign_emplyee_wise.IsDelete=0   order by  empid");
                int empSelect = int.Parse(ddlemp.SelectedValue.ToString());
                if (empSelect == -1 && emp_ids.ToString().Equals("-1"))
                {
                    dtEmpList = dtEmpList.Copy();
                }
                else if (empSelect > 0)
                {
                    DataRow[] drEmpList = dtEmpList.Select("EmpId=" + ddlemp.SelectedValue);
                    if (drEmpList.Length == 0)
                    {
                        ltrErr.Text = "Assign Time Group to the Employee";
                        return;
                    }
                    dtEmpList = drEmpList.CopyToDataTable();
                }
                else if (!emp_ids.ToString().Equals("-1"))
                {
                    DataRow[] drEmpList = dtEmpList.Select("EmpId IN(" + emp_ids + ")");
                    dtEmpList = drEmpList.CopyToDataTable();
                }
                dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry ORDER BY Emp_Attendance_Entry.Emp_FP_Id, Emp_Attendance_Entry.Emp_hour, Emp_Attendance_Entry.Emp_min, Emp_Attendance_Entry.Emp_sec;");
                dtEmpFPData.Columns.Add("Ticks");
                for (int i = 0; i < dtEmpFPData.Rows.Count; i++)
                {
                    String dtDate = dtEmpFPData.Rows[i]["Emp_day"].ToString() + "/" + dtEmpFPData.Rows[i]["Emp_month"].ToString() + "/" + dtEmpFPData.Rows[i]["Emp_year"].ToString();
                    long ticks = new DateTime(int.Parse(dtEmpFPData.Rows[i]["Emp_year"].ToString()), int.Parse(dtEmpFPData.Rows[i]["Emp_month"].ToString()), int.Parse(dtEmpFPData.Rows[i]["Emp_day"].ToString())).Ticks;
                    dtEmpFPData.Rows[i]["Ticks"] = ticks;
                }

                long fromticks = startdateticks;
                long toticks = enddateticks;
                DataRow[] drEmpFPDetail = dtEmpFPData.Select("Ticks>=" + fromticks + " and Ticks<=" + toticks, "Emp_hour,Emp_min,Emp_sec asc");
                if (drEmpFPDetail.Length > 0)
                {
                    dtEmpFPData = drEmpFPDetail.CopyToDataTable();
                }
                else
                {
                    dtEmpFPData = dtEmpFPData.Clone();
                }
                DataTable dtdemo = new DataTable();
                dtdemo = dtEmpFPData.Clone();
                if (empSelect == -1)
                {
                    dtdemo = dtEmpFPData.Copy();
                }
                else if (empSelect > 0)
                {
                    DataRow[] demo = dtEmpFPData.Select("Emp_Id=" + ddlemp.SelectedValue);

                    if (demo.Length > 0)
                    {
                        dtdemo = demo.CopyToDataTable();
                    }
                    else
                    {
                        dtdemo = dtdemo.Clone();
                    }
                }
                string daterange = hdnDate.Value.ToString();
                string[] date = daterange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string dtpfrom = date[0].ToString();

                string dtpto = date[1].ToString();
                DateTime dtFromDate = new DateTime(int.Parse(dtpfrom.Substring(6)), int.Parse(dtpfrom.Substring(3, 2)), int.Parse(dtpfrom.Substring(0, 2)));
                DateTime dtToDate = new DateTime(int.Parse(dtpto.Substring(6)), int.Parse(dtpto.Substring(3, 2)), int.Parse(dtpto.Substring(0, 2)));

                TimeSpan totalDays = dtToDate.Subtract(dtFromDate);
                DataTable dtEmpTimeConfig = new DataTable();
                // dtEmpTimeConfig = objmysqldb.GetData("SELECT employee_punchtime_details_datewise.* FROM employee_punchtime_details_datewise");
                dtEmpTimeConfig = objmysqldb.GetData("SELECT employeewise_punchtime_details_datewise.* FROM employeewise_punchtime_details_datewise");
                dt.Columns.Add("Emp Id", Type.GetType("System.Int32"));
                dt.Columns.Add("Employee Name");
                dt.Columns.Add("FP Emp Id", Type.GetType("System.Int32"));
                dt.Columns.Add("Date");
                dt.Columns.Add("Mobile No.");
                dt.Columns.Add("Actual In Time");
                dt.Columns.Add("In Punch Time");
                dt.Columns.Add("Late By");
                dt.Columns.Add("Out Time 1");
                dt.Columns.Add("Diff Time 1");

                dt.Columns.Add("In Time 1");
                dt.Columns.Add("Out Time 2");
                dt.Columns.Add("Diff Time 2");

                dt.Columns.Add("In Time 2");
                dt.Columns.Add("Out Time 3");
                dt.Columns.Add("Diff Time 3");

                dt.Columns.Add("In Time 3");
                dt.Columns.Add("Out Punch Time");
                dt.Columns.Add("Diff Time 4");
                dt.Columns.Add("Actual Out Time");

                dt.Columns.Add("Early By");
                dt.Columns.Add("Single");
                dt.Columns.Add("Ext Early");
                dt.Columns.Add("Ext Late");
                dt.Columns.Add("No Punch");

                dt.Columns.Add("Odd Even Count", Type.GetType("System.Int32"));
                dt.Columns.Add("SortNumber", Type.GetType("System.Int32"));
                DataTable dtfinal = dt.Clone();
                string Emp_ids = "";
                string rpt = "~/Reports/11";
                string filePath = HttpContext.Current.Server.MapPath(rpt);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (DataRow dr in dtEmpList.Rows)
                {
                    dt = dt.Clone();
                    int empId = 0;
                    string EmployeeName = "";
                    for (int k = 0; k <= int.Parse(totalDays.Days.ToString()); k++)
                    {
                        empId = int.Parse(dr["EmpId"].ToString());
                        string strfpempid = dr["FPEmpId"].ToString();
                        EmployeeName = dr["EmployeeName"].ToString();
                        string EmpPhone = dr["EmpPhone"].ToString();
                        DateTime dtt1 = dtFromDate.AddDays(k);
                        string Date = dtt1.ToString();

                        //int SortNumber = int.Parse(dr["SortNumber"].ToString());
                        int grpid = int.Parse(dr["Group_id"].ToString());

                        if (strfpempid.Trim() != "" && strfpempid != "0")
                        {
                            int fpempid = int.Parse(strfpempid);
                            DataRow drnew = dt.NewRow();

                            drnew["Emp Id"] = empId;
                            drnew["FP Emp Id"] = fpempid;
                            drnew["Employee Name"] = EmployeeName;
                            drnew["FP Emp Id"] = fpempid;
                            drnew["Mobile No."] = EmpPhone;
                            drnew["Date"] = dtt1.ToString("dd/MM/yyyy").Substring(0, 10);
                            // drnew["SortNumber"] = ;

                            #region Get In-Time Group Data
                            int intday = 0;
                            //string searchday = (DateTime.Parse(Date.ToString()).DayOfWeek).ToString();
                            intday = (int)dtt1.DayOfWeek; //(int)DateTime.Parse(Date.ToString()).DayOfWeek;
                            if (intday == 0)
                            { intday = 7; }
                            DataRow[] drfindtime = dtEmpTimeConfig.Select("emp_id=" + empId + "");
                            int emphour_global = 0;
                            int empmin_global = 0;

                            if (drfindtime.Length > 0)
                            {

                                emphour_global = int.Parse(drfindtime[0]["In_hour"] != null && drfindtime[0]["In_hour"].ToString().Trim() != "" ? drfindtime[0]["In_hour"].ToString() : "0");
                                empmin_global = int.Parse(drfindtime[0]["In_min"] != null && drfindtime[0]["In_min"].ToString().Trim() != "" ? drfindtime[0]["In_min"].ToString() : "0");

                                int emphour = emphour_global;
                                int empmin = empmin_global;

                                if (emphour_global == 0 && empmin_global == 0)
                                {
                                }
                                else
                                {
                                    string time = "";
                                    getEmployeeTime(ref emphour, empmin, 0, ref time);
                                    drnew["Actual In Time"] = time;
                                }
                            }


                            DataRow[] drfindpunchtime = dtEmpFPData.Select("Emp_Id=" + empId + " and Emp_FP_Id=" + fpempid + " and Emp_Day=" + dtt1.Day + " and Emp_Month=" + dtt1.Month + " and Emp_Year=" + dtt1.Year);
                            int punch_emphour_global = 0;
                            int punch_empmin_global = 0;
                            int punch_empsec_global = 0;

                            if (drfindpunchtime.Length > 0)
                            {
                                //if (chlpre.Checked == false)
                                //{
                                //    continue;
                                //}
                                punch_emphour_global = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                                punch_empmin_global = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                                punch_empsec_global = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                                int emphour = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                                int empmin = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                                int Emp_sec = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());

                                TimeSpan time = new TimeSpan(emphour, empmin, Emp_sec);
                                drnew["In Punch Time"] = time;

                                if (emphour_global == 0 && empmin_global == 0)
                                {
                                    drnew["Late By"] = "-";
                                }
                                else
                                {
                                    if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                    {
                                        TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global.ToString() + ":" + punch_empmin_global.ToString());
                                        TimeSpan tm2 = TimeSpan.Parse(emphour_global.ToString() + ":" + empmin_global.ToString());
                                        TimeSpan difference = tm1 - tm2;

                                        int hours = difference.Hours;
                                        int minutes = difference.Minutes;
                                        string concat = "";

                                        if (hours == 0)
                                        {
                                            concat = "00:";
                                        }
                                        else
                                        {
                                            if (hours < 10)
                                            {
                                                concat = "0" + hours.ToString() + ":";
                                            }
                                            else
                                                concat = hours.ToString() + ":";
                                        }

                                        if (minutes == 0)
                                        {
                                            concat += "00:";
                                        }
                                        else
                                        {
                                            if (minutes < 10)
                                            {
                                                concat += "0" + minutes.ToString() + ":";
                                            }
                                            else
                                                concat += minutes.ToString() + ":";
                                        }

                                        if (punch_empsec_global == 0)
                                        {
                                            concat += "00";
                                        }
                                        else
                                        {
                                            if (punch_empsec_global < 10)
                                            {
                                                concat += "0" + punch_empsec_global.ToString();
                                            }
                                            else
                                                concat += punch_empsec_global.ToString();
                                        }
                                        drnew["Late By"] = concat;
                                    }
                                    else
                                    {
                                        drnew["Late By"] = "-";
                                    }
                                }
                            }
                            else
                            {
                                //if (chlab.Checked == false)
                                //{
                                //    continue;
                                //}
                                drnew["In Punch Time"] = "-";
                                drnew["Late By"] = "-";
                            }

                            #endregion

                            #region Get Out-Time Group Data

                            //DataRow[] drfindtime_out = dtEmpTimeConfig.Select("Emp_Id=" + empId + "and Emp_Day=" + (intday + 1));
                            DataRow[] drfindtime_out = dtEmpTimeConfig.Select("emp_id=" + empId + "");

                            int emphour_global_out = 0;
                            int empmin_global_out = 0;

                            if (drfindtime_out.Length > 0)
                            {
                                emphour_global_out = int.Parse(drfindtime_out[0]["out_hour"] != null && drfindtime_out[0]["out_hour"].ToString().Trim() != "" ? drfindtime_out[0]["out_hour"].ToString() : "0");
                                empmin_global_out = int.Parse(drfindtime_out[0]["out_min"] != null && drfindtime_out[0]["out_min"].ToString().Trim() != "" ? drfindtime_out[0]["out_min"].ToString() : "0");

                                int emphour = emphour_global_out;
                                int empmin = empmin_global_out;
                                if (emphour_global_out == 0 && empmin_global_out == 0)
                                {
                                }
                                else
                                {
                                    string time = "";
                                    getEmployeeTime(ref emphour, empmin, 0, ref time);
                                    drnew["Actual Out Time"] = time;
                                }
                            }

                            DataRow[] drfindpunchtime_out = dtEmpFPData.Select("Emp_Id=" + empId + " and Emp_FP_Id=" + fpempid + " and Emp_Day=" + dtt1.Day + " and Emp_Month=" + dtt1.Month + " and Emp_Year=" + dtt1.Year);

                            int punch_emphour_global_out = 0;
                            int punch_empmin_global_out = 0;
                            int punch_empsec_global_out = 0;

                            if (drfindpunchtime_out.Length > 1)
                            {
                                punch_emphour_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                                punch_empmin_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                                punch_empsec_global_out = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                                int emphour = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_hour"].ToString());
                                int empmin = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_min"].ToString());
                                int Emp_sec = int.Parse(drfindpunchtime_out[drfindpunchtime_out.Length - 1]["Emp_sec"].ToString());

                                TimeSpan time = new TimeSpan(emphour, empmin, Emp_sec);
                                drnew["Out Punch Time"] = time;

                                DataRow[] drxx = dtEmpFPData.Select("Emp_Id=" + empId + " and Emp_FP_Id=" + fpempid + " and Emp_Day=" + dtt1.Day + " and Emp_Month=" + dtt1.Month + " and Emp_Year=" + dtt1.Year);

                                DataTable dtt = new DataTable();
                                dtt.Columns.Add("all");


                                if (drxx.Length > 2)
                                {
                                    int i = 0;
                                    for (i = 1; i < drxx.Length - 1; i++)
                                    {
                                        int punchh = int.Parse(drxx[i]["Emp_hour"].ToString());
                                        int punchm = int.Parse(drxx[i]["Emp_min"].ToString());
                                        int punchs = int.Parse(drxx[i]["Emp_sec"].ToString());

                                        TimeSpan tm1 = new TimeSpan(punchh, punchm, punchs);

                                        DataRow drx = dtt.NewRow();
                                        drx["all"] = tm1.ToString();
                                        dtt.Rows.Add(drx);
                                    }
                                    string out1 = "";
                                    string in1 = "";
                                    string out2 = "";
                                    string in2 = "";
                                    string out3 = "";
                                    string in3 = "";

                                    for (int j = 0; j < dtt.Rows.Count; j++)
                                    {
                                        out1 = dtt.Rows[0]["all"].ToString();
                                        if (dtt.Rows.Count > 1)
                                        {
                                            in1 = dtt.Rows[1]["all"].ToString();
                                        }
                                        if (dtt.Rows.Count > 2)
                                        {
                                            out2 = dtt.Rows[2]["all"].ToString();
                                            if (dtt.Rows.Count > 3)
                                            {
                                                in2 = dtt.Rows[3]["all"].ToString();
                                            }
                                        }
                                        if (dtt.Rows.Count > 4)
                                        {
                                            out3 = dtt.Rows[4]["all"].ToString();
                                            if (dtt.Rows.Count > 5)
                                            {
                                                in3 = dtt.Rows[5]["all"].ToString();
                                            }
                                        }
                                    }
                                    if (out1 != null)
                                    {
                                        drnew["Out Time 1"] = out1.ToString();
                                    }
                                    else { drnew["Out Time 1"] = "-"; }

                                    if (in1 != "")
                                    {
                                        drnew["In Time 1"] = in1.ToString();
                                    }
                                    else if (drnew["In Time 1"] == "")
                                    {
                                        drnew["In Time 1"] = out1.ToString();
                                    }
                                    else { drnew["In Time 1"] = "-"; }

                                    if (out2 != "")
                                    {
                                        drnew["Out Time 2"] = out2.ToString();
                                    }
                                    else if (drnew["Out Time 2"] == "")
                                    {
                                        drnew["Out Time 2"] = in1.ToString();
                                    }
                                    else { drnew["Out Time 2"] = "-"; }

                                    if (in2 != "")
                                    {
                                        drnew["In Time 2"] = in2.ToString();
                                    }
                                    else if (drnew["In Time 2"] == "")
                                    {
                                        drnew["In Time 2"] = out2.ToString();
                                    }
                                    else
                                    {
                                        drnew["In Time 2"] = "-";
                                    }

                                    if (out3 != "")
                                    {
                                        drnew["Out Time 3"] = out3.ToString();
                                    }
                                    else if (drnew["Out Time 3"] == "")
                                    {
                                        drnew["Out Time 3"] = in2.ToString();
                                    }
                                    else { drnew["Out Time 3"] = "-"; }

                                    if (in3 != "")
                                    {
                                        drnew["In Time 3"] = in3.ToString();
                                    }
                                    else if (drnew["In Time 3"] == "")
                                    {
                                        drnew["In Time 3"] = out3.ToString();
                                    }
                                    else { drnew["In Time 3"] = "-"; }

                                    if (drnew["Out Punch Time"].ToString() != "" && (drnew["Out Time 1"].ToString() == "" || drnew["Out Time 1"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["Out Time 1"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() != "" && (drnew["In Time 1"].ToString() == "" || drnew["In Time 1"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["In Time 1"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() != "" && (drnew["Out Time 2"].ToString() == "" || drnew["Out Time 2"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["Out Time 2"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() != "" && (drnew["In Time 2"].ToString() == "" || drnew["In Time 2"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        //drnew["Out_Punch_Time"] = arr[2];
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["In Time 2"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() != "" && (drnew["Out Time 3"].ToString() == "" || drnew["Out Time 3"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["Out Time 3"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() != "" && (drnew["In Time 3"].ToString() == "" || drnew["In Time 3"].ToString() == "-"))
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        //drnew["Out_Punch_Time"] = arr[2]; 
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["In Time 3"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";
                                    }

                                    if (drnew["Out Time 1"].ToString() == "" && drnew["In Punch Time"].ToString() == "")
                                    {
                                        drnew["Diff Time 1"] = "";
                                    }
                                    else if (drnew["Out Time 1"].ToString() == "-" || drnew["In Punch Time"].ToString() == "-")
                                    {
                                        drnew["Diff Time 1"] = "-";
                                    }
                                    else
                                    {
                                        TimeSpan tm1 = TimeSpan.Parse(drnew["Out Time 1"].ToString());
                                        String aaa = drnew["In Punch Time"].ToString();
                                        //string[] arr = aaa.Split(':', ':');
                                        // TimeSpan tm2 = TimeSpan.Parse(arr[2]);
                                        TimeSpan tm2 = TimeSpan.Parse(aaa);
                                        TimeSpan difference = tm1 - tm2;
                                        drnew["Diff Time 1"] = difference.ToString();
                                    }

                                    if (drnew["Out Time 2"].ToString() == "" && drnew["In Time 1"].ToString() == "")
                                    {
                                        drnew["Diff Time 2"] = "";
                                    }
                                    else if (drnew["Out Time 2"].ToString() == "-" || drnew["In Time 1"].ToString() == "-")
                                    {
                                        drnew["Diff Time 2"] = "-";
                                    }
                                    else
                                    {
                                        TimeSpan tm1 = TimeSpan.Parse(drnew["Out Time 2"].ToString());
                                        TimeSpan tm2 = TimeSpan.Parse(drnew["In Time 1"].ToString());
                                        TimeSpan difference = tm1 - tm2;
                                        drnew["Diff Time 2"] = difference.ToString();
                                    }

                                    if (drnew["Out Time 3"].ToString() == "" && drnew["In Time 2"].ToString() == "")
                                    {
                                        drnew["Diff Time 3"] = "";
                                    }
                                    else if (drnew["Out Time 3"].ToString() == "-" || drnew["In Time 2"].ToString() == "-")
                                    {
                                        drnew["Diff Time 3"] = "-";
                                    }
                                    else
                                    {
                                        TimeSpan tm1 = TimeSpan.Parse(drnew["Out Time 3"].ToString());
                                        TimeSpan tm2 = TimeSpan.Parse(drnew["In Time 2"].ToString());
                                        TimeSpan difference = tm1 - tm2;
                                        drnew["Diff Time 3"] = difference.ToString();
                                    }

                                    if (drnew["Out Punch Time"].ToString() == "" && drnew["In Time 3"].ToString() == "")
                                    {
                                        drnew["Diff Time 4"] = "";
                                    }
                                    else if (drnew["Out Punch Time"].ToString() == "-" || drnew["In Time 3"].ToString() == "-")
                                    {
                                        drnew["Diff Time 4"] = "-";
                                    }
                                    else
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        TimeSpan tm1 = TimeSpan.Parse(aaa);
                                        TimeSpan tm2 = TimeSpan.Parse(drnew["In Time 3"].ToString());
                                        TimeSpan difference = tm1 - tm2;
                                        drnew["Diff Time 4"] = difference.ToString();
                                    }
                                }
                                else
                                {
                                    if (drnew["Out Punch Time"].ToString() != "" && drnew["Out Time 1"].ToString() == "")
                                    {
                                        String aaa = drnew["Out Punch Time"].ToString();
                                        string[] arr = aaa.Split('<', '>');
                                        //drnew["Out_Punch_Time"] = arr[2];
                                        drnew["Out Punch Time"] = aaa;
                                        drnew["Out Time 1"] = drnew["Out Punch Time"].ToString();
                                        drnew["Out Punch Time"] = "-";

                                        if (drnew["Out Time 1"].ToString() == "" && drnew["In Punch Time"].ToString() == "")
                                        {
                                            drnew["Diff Time 1"] = "";
                                        }
                                        else if (drnew["Out Time 1"].ToString() == "-" || drnew["In Punch Time"].ToString() == "-")
                                        {
                                            drnew["Diff Time 1"] = "-";
                                        }
                                        else
                                        {
                                            TimeSpan tm1 = TimeSpan.Parse(drnew["Out Time 1"].ToString());
                                            String aaa1 = drnew["In Punch Time"].ToString();
                                            // string[] arr1 = aaa1.Split('<', '>');
                                            TimeSpan tm2 = TimeSpan.Parse(aaa1);
                                            TimeSpan difference = tm1 - tm2;
                                            drnew["Diff Time 1"] = difference.ToString();
                                        }


                                        drnew["In Time 1"] = "-";
                                        drnew["In Time 2"] = "-";
                                        drnew["Out Time 2"] = "-";
                                        drnew["Diff Time 2"] = "-";
                                        drnew["In Time 3"] = "-";
                                        drnew["Out Time 3"] = "-";
                                        drnew["Diff Time 3"] = "-";
                                        drnew["Diff Time 4"] = "-";
                                    }
                                    else
                                    {
                                        drnew["In Time 1"] = "-";
                                        drnew["Out Time 1"] = "-";
                                        drnew["Diff Time 1"] = "-";
                                        drnew["In Time 2"] = "-";
                                        drnew["Out Time 2"] = "-";
                                        drnew["Diff Time 2"] = "-";
                                        drnew["In Time 3"] = "-";
                                        drnew["Out Time 3"] = "-";
                                        drnew["Diff Time 3"] = "-";
                                        drnew["Diff Time 4"] = "-";
                                    }
                                }
                                if (emphour_global_out == 0 && empmin_global_out == 0)
                                {
                                    drnew["Early By"] = "-";
                                }
                                else
                                {
                                    if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                    {
                                        TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global_out.ToString() + ":" + punch_empmin_global_out.ToString() + ":" + punch_empsec_global_out.ToString());
                                        TimeSpan tm2 = TimeSpan.Parse(emphour_global_out.ToString() + ":" + empmin_global_out.ToString());
                                        TimeSpan difference = tm2 - tm1;
                                        drnew["Early By"] = difference.ToString();
                                    }
                                    else
                                    {
                                        drnew["Early By"] = "-";
                                    }
                                }
                            }
                            else
                            {
                                drnew["In Time 1"] = "-";
                                drnew["Out Time 1"] = "-";
                                drnew["Diff Time 1"] = "-";
                                drnew["In Time 2"] = "-";
                                drnew["Out Time 2"] = "-";
                                drnew["Diff Time 2"] = "-";
                                drnew["In Time 3"] = "-";
                                drnew["Out Time 3"] = "-";
                                drnew["Diff Time 3"] = "-";
                                drnew["Diff Time 4"] = "-";
                                drnew["Early By"] = "-";
                            }

                            int count = 0;
                            if (drnew["In Punch Time"] != "-")
                            {
                                count++;
                            }
                            if (drnew["Out Time 1"] != "-")
                            {
                                count++;
                            }
                            if (drnew["In Time 1"] != "-")
                            {
                                count++;
                            }
                            if (drnew["Out Time 2"] != "-")
                            {
                                count++;
                            }
                            if (drnew["In Time 2"] != "-")
                            {
                                count++;
                            }
                            if (drnew["Out Time 3"] != "-")
                            {
                                count++;
                            }
                            if (drnew["In Time 3"] != "-")
                            {
                                count++;
                            }
                            if (drnew["Out Punch Time"] != "-")
                            {
                                count++;
                            }
                            if (count == 1 || count == 3 || count == 5 || count == 7)
                            {
                                drnew["Odd Even Count"] = 1;
                            }
                            else
                            {
                                drnew["Odd Even Count"] = 0;
                            }
                            #endregion

                            #region Get Extreme Early Time
                            string punchintime = "-";
                            string punchouttime = "-";
                            string actualintime = "-";
                            string actualouttime = "-";
                            string Elate = "-";
                            string Eearly = "-";
                            string singlepunch = "-";

                            DataRow[] drfindEmpPunchtime = dtEmpFPData.Select("Emp_Id=" + empId + " and Emp_FP_Id=" + fpempid + " and Emp_Day=" + dtt1.Day + " and Emp_Month=" + dtt1.Month + " and Emp_Year=" + dtt1.Year);

                            if (drfindEmpPunchtime.Length > 0)
                            {
                                punch_emphour_global = int.Parse(drfindEmpPunchtime[0]["Emp_hour"].ToString());
                                punch_empmin_global = int.Parse(drfindEmpPunchtime[0]["Emp_min"].ToString());
                                punch_empsec_global = int.Parse(drfindEmpPunchtime[0]["Emp_sec"].ToString());
                                int inPunch = punch_emphour_global;
                                getEmployeeTime(ref inPunch, punch_empmin_global, punch_empsec_global, ref punchintime);

                                if (drfindpunchtime.Length == 1)
                                {
                                    singlepunch = "Yes";
                                    drnew["Single"] = singlepunch;
                                }
                                else
                                {
                                    drnew["Single"] = singlepunch;
                                }
                            }

                            if (drfindEmpPunchtime.Length > 1)
                            {
                                punch_emphour_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_hour"].ToString());
                                punch_empmin_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_min"].ToString());
                                punch_empsec_global_out = int.Parse(drfindEmpPunchtime[drfindEmpPunchtime.Length - 1]["Emp_sec"].ToString());
                                int outPunch = punch_emphour_global_out;
                                getEmployeeTime(ref outPunch, punch_empmin_global_out, punch_empsec_global_out, ref punchouttime);
                            }

                            string strdays = dtt1.DayOfWeek.ToString();//DateTime.Parse(Date.ToString()).DayOfWeek.ToString();
                            int intdays = intday;
                            DataRow[] drfindActualtimes = dtEmpTimeConfig.Select("emp_id=" + empId + "");
                            //DataRow[] drfindActualtimes = dtEmpTimeConfig.Select("Emp_Id=" + empId + "and Emp_Day=" + intdays);

                            int EL_emphour_global = 0;
                            int EL_empmin_global = 0;

                            int EE_emphour_global_out = 0;
                            int EE_empmin_global_out = 0;

                            if (drfindActualtimes.Length > 0)
                            {
                                emphour_global = int.Parse(drfindActualtimes[0]["In_hour"] != null && drfindActualtimes[0]["In_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["In_hour"].ToString() : "0");
                                empmin_global = int.Parse(drfindActualtimes[0]["In_min"] != null && drfindActualtimes[0]["In_min"].ToString().Trim() != "" ? drfindActualtimes[0]["In_min"].ToString() : "0");
                                int intactualintime = emphour_global;
                                getEmployeeTime(ref intactualintime, empmin_global, 0, ref actualintime);

                                emphour_global_out = int.Parse(drfindActualtimes[0]["out_hour"] != null && drfindActualtimes[0]["out_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["out_hour"].ToString() : "0");
                                empmin_global_out = int.Parse(drfindActualtimes[0]["out_min"] != null && drfindActualtimes[0]["out_min"].ToString().Trim() != "" ? drfindActualtimes[0]["out_min"].ToString() : "0");

                                int intactualouttime = emphour_global_out;
                                getEmployeeTime(ref intactualouttime, empmin_global_out, 0, ref actualouttime);

                                EE_emphour_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_hour"] != null && drfindActualtimes[0]["Ext_Early_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_hour"].ToString() : "0");

                                EE_empmin_global_out = int.Parse(drfindActualtimes[0]["Ext_Early_min"] != null && drfindActualtimes[0]["Ext_Early_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Early_min"].ToString() : "0");

                                EL_emphour_global = int.Parse(drfindActualtimes[0]["Ext_Late_hour"] != null && drfindActualtimes[0]["Ext_Late_hour"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_hour"].ToString() : "0");
                                EL_empmin_global = int.Parse(drfindActualtimes[0]["Ext_Late_min"] != null && drfindActualtimes[0]["Ext_Late_min"].ToString().Trim() != "" ? drfindActualtimes[0]["Ext_Late_min"].ToString() : "0");
                            }

                            if ((emphour_global == 0 && empmin_global == 0) || (punch_emphour_global == 0 && punch_empmin_global == 0) || (EL_emphour_global == 0 && EL_empmin_global == 0))
                            {
                            }
                            else
                            {
                                if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                {
                                    if ((punch_emphour_global > EL_emphour_global) || (punch_emphour_global == EL_emphour_global && punch_empmin_global >= EL_empmin_global))
                                    {
                                        Elate = "Yes";
                                        drnew["Ext Late"] = Elate;
                                    }
                                    else
                                    {
                                        drnew["Ext Late"] = Elate;
                                    }
                                }
                                else
                                {
                                    drnew["Ext Late"] = Elate;
                                }
                            }
                            if ((emphour_global_out == 0 && empmin_global_out == 0) || (punch_emphour_global_out == 0 && punch_empmin_global_out == 0))
                            {
                            }
                            else
                            {
                                if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                {
                                    if ((punch_emphour_global_out < EE_emphour_global_out) || (punch_emphour_global_out == EE_emphour_global_out && punch_empmin_global_out < EE_empmin_global_out))
                                    {
                                        Eearly = "Yes";
                                        drnew["Ext Early"] = Eearly;
                                        drnew["Ext Late"] = Elate;
                                    }
                                    else
                                    {
                                        drnew["Ext Early"] = Eearly;
                                        drnew["Ext Late"] = Elate;
                                    }
                                }
                                else
                                {
                                    drnew["Ext Early"] = Eearly;
                                    drnew["Ext Late"] = Elate;
                                }
                            }

                            if (punch_emphour_global == 0 && punch_empmin_global == 0 && punch_empsec_global == 0 && punch_emphour_global_out == 0 && punch_empmin_global_out == 0 && punch_empsec_global_out == 0)
                            {
                                drnew["No Punch"] = "Yes";
                                drnew["Ext Early"] = Eearly;
                                drnew["Ext Late"] = Elate;
                                drnew["Single"] = singlepunch;
                            }
                            else
                            {
                                drnew["No Punch"] = "-";
                                drnew["Ext Early"] = Eearly;
                                drnew["Ext Late"] = Elate;
                                drnew["Single"] = singlepunch;
                            }
                            #endregion
                            dt.Rows.Add(drnew);


                        }
                    }
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        continue;
                    }
                    //PDF code


                    #region

                    DataTable dttemp = dt.Copy();
                    dttemp.Columns.Remove("Emp Id");
                    dttemp.Columns.Remove("Employee Name");
                    dttemp.Columns.Remove("FP Emp Id");
                    dttemp.Columns.Remove("odd even count");
                    dttemp.Columns.Remove("sortNumber");
                    Document doc = new Document();
                    doc = new Document(PageSize.LEGAL.Rotate());     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dttemp.Columns.Count);
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }


                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;

                    StringBuilder sb = new StringBuilder();
                    string aa = ddlmonth.Items[ddlmonth.SelectedIndex].Text.ToString() + " - " + ddlyear.Items[ddlyear.SelectedIndex].Text.ToString();
                    //Generate Invoice (Bill) Header.
                    sb.Append("<b><hr  width='100%'></b>");
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Employeewise Punch Detail</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Employee Name: </b> " + EmployeeName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Report Time: </b> " + MySQLDB.GetIndianTime().ToString("dd/MM/yyyy").Substring(0, 10) + "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\" + empId + ".pdf", FileMode.Create));
                    Emp_ids += empId.ToString() + ",";
                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dttemp, report));
                    doc.Add(paragraphfotter);
                    //byte[] b = File.ReadAllBytes(Server.MapPath("Late_Comers_Report.pdf"));
                    // Closing the document
                    doc.Close();

                    //Process.Start(Server.MapPath("Daily_Punch_Detail.pdf"));

                    #endregion
                    dtfinal.Merge(dt);
                }
                string[] employees = Emp_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (employees.Length > 0)
                {
                    Document document = new Document();

                    // step 2: we create a writer that listens to the document
                    PdfCopy writer = new PdfCopy(document, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf", FileMode.Create));
                    if (writer == null)
                    {
                        return;
                    }

                    // step 3: we open the document
                    document.Open();

                    for (int j = 0; j < employees.Length; j++)
                    {
                        // we create a reader for a certain document
                        PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(rpt) + "\\" + employees[j].ToString() + ".pdf");
                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }
                    writer.Close();
                    document.Close();
                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports//11";
                    test = test + "//All.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                }
                dt = dtfinal.Copy();
                // dt.Columns.Remove("Emp Id");
                DataView dv = dt.DefaultView;
                dv.Sort = "SortNumber ASC, Emp Id";
                dt = dv.ToTable();

                DataTable dtGeneralDetail = new DataTable();
                dtGeneralDetail.Columns.Add("Report_Time");

                DataRow dtGen = dtGeneralDetail.NewRow();
                dtGen["Report_Time"] = MySQLDB.GetIndianTime().ToString("dd/MM/yyyy").Substring(0, 10);
                dtGeneralDetail.Rows.Add(dtGen);

                DataSet ds = new DataSet();
                dt.TableName = "dtLateComersAttendance";
                dtGeneralDetail.TableName = "dtGeneralDetail";
                ds.Tables.Add(dt);
                ds.Tables.Add(dtGeneralDetail);
                ViewState["GVDATA"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    grd.DataSource = dt;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ee)
            {
                Logger.WriteCriticalLog("AttendanceReports 3214: exception:" + ee.Message + "::::::::" + ee.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }


        private void EmployeeWiseMonthlyLeaveCountMethod(string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                //pending Leave calculation Table  after Complete this report
                double total_CL_Per_Year = 10;
                double total_ML_Per_Year = 3;

                double total_CL_ML_Per_Month = 2;

                double total_LCL_Make_One_Full_Leave = 3;

                DataTable dt = new DataTable();
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                string strempid = ddlemp.SelectedValue.ToString();
                DataTable dtEmpList = new DataTable();
                DataTable dtEmpFPData = new DataTable();

                int monthid = 0;
                int.TryParse(ddlmonth.Items[ddlmonth.SelectedIndex].Value.ToString(), out monthid);

                int year = 0;
                int.TryParse(ddlyear.Items[ddlyear.SelectedIndex].Value.ToString(), out year);
                int total_days_in_month = DateTime.DaysInMonth(year, monthid);

                string startdate = "01/" + monthid + "/" + year;
                string enddate = "" + total_days_in_month + "/" + monthid + "/" + year;

                if (strempid.Equals("-1") && emp_ids.ToString().Equals("-1"))
                {

                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,PayScale,FP_Id as FPEmpId,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.IsDelete=0 and EmpStatusFlag=0 and time_group_assign_emplyee_wise.IsDelete=0 and employee_master.FP_Id>0  order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry  WHERE  Emp_Attendance_Entry.Emp_month =" + monthid + " AND Emp_Attendance_Entry.Emp_year=" + year + " and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }
                else
                {
                    if (strempid.Equals("-1") && !emp_ids.ToString().Equals("-1"))
                    {
                        strempid = emp_ids;
                    }
                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,PayScale,FP_Id as FPEmpId,EmpPhone,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.empid in(" + strempid + ") and time_group_assign_emplyee_wise.IsDelete=0  and employee_master.IsDelete=0 and employee_master.EmpStatusFlag=0 and employee_master.FP_Id>0 order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry where Emp_Id in(" + strempid + ") and  ((Emp_Attendance_Entry.Emp_month)=" + monthid + ") AND ((Emp_Attendance_Entry.Emp_year)=" + year + ") and IsDelete=0  ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }

                DataTable dtgrplist = dtEmpList.DefaultView.ToTable(true, "Group_id");
                string grp_ids = "0,";

                DataTable dtEmpTimeConfig = new DataTable();

                grp_ids = grp_ids.TrimEnd(',');
                long stticks = new DateTime(year, monthid, 1).Ticks;
                long endticks = new DateTime(year, monthid, total_days_in_month).Ticks;
                //int empidforholiday = int.Parse(strempid);
                DataTable dtCurrentMonthLeaves = objmysqldb.GetData("SELECT * FROM employee_management.leave_history_monthwise where Month_Id=" + monthid + " and Month_Year=" + year + " and IsDelete=0;");

                DataTable dtAllPreviousMonthLeaves = getpreviosMonth(objmysqldb, monthid, year);

                // DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup where IsDelete=0 and Holiday_Year=" + year + " and Holiday_Month=" + monthid + ";");
                DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup inner join assign_holidayprofile_employee on assign_holidayprofile_employee.holiday_profile_id=holiday_setup.Holiday_Profile_Id where holiday_setup.IsDelete=0 and holiday_setup.Holiday_Year=" + year + " and holiday_setup.Holiday_Month=" + monthid + " and assign_holidayprofile_employee.IsDelete=0");



                DataTable dtLeaveList = objmysqldb.GetData("SELECT Leave_Id,Leave_Name as Leave_Display_Name,Is_CL_Leave FROM employee_management.leave_master where IsDelete=0;");
                if (emp_ids == "-1")
                {
                    dtEmpTimeConfig = objmysqldb.GetData("SELECT employeewise_punchtime_details_datewise.* FROM employeewise_punchtime_details_datewise where Dateticks >=" + stticks + " and  Dateticks <= " + endticks + "");
                }
                else
                {
                    dtEmpTimeConfig = objmysqldb.GetData("SELECT employeewise_punchtime_details_datewise.* FROM employeewise_punchtime_details_datewise where emp_id in(" + emp_ids.TrimEnd(',') + ") and Dateticks >=" + stticks + " and  Dateticks <= " + endticks + "");
                }
                DataTable dtSingleRecord = new DataTable();

                dtSingleRecord.Columns.Add("intFPEmpId", Type.GetType("System.Int32"));
                dtSingleRecord.Columns.Add("FPEmpId");
                dtSingleRecord.Columns.Add("EmployeeName");
                dtSingleRecord.Columns.Add("EmpPhone");
                dtSingleRecord.Columns.Add("Month");

                int colpos = 3;
                Hashtable hsColPos = new System.Collections.Hashtable();
                colpos++;
                dtSingleRecord.Columns.Add("Total LWP");
                colpos++;
                string leavenames = "";
                int Ccount = 1;
                DataRow drColName = dtSingleRecord.NewRow();
                drColName["Total LWP"] = "Total LWP";
                hsColPos.Add(-1, colpos);

                foreach (DataRow dr in dtLeaveList.Rows)
                {
                    colpos++;
                    string leave_name = dr["Leave_Display_Name"].ToString();
                    leavenames += leave_name + ",";
                    hsColPos.Add(int.Parse(dr["Leave_Id"].ToString()), colpos);
                    dtSingleRecord.Columns.Add("C" + Ccount);
                    drColName["C" + Ccount] = "Total " + leave_name.ToString();

                    Ccount++;
                }
                colpos++;
                dtSingleRecord.Columns.Add("Upto Previous Total");
                drColName["Upto Previous Total"] = "Total";
                char[] ch1 = { ',' };
                string[] splt = leavenames.Split(ch1, StringSplitOptions.RemoveEmptyEntries);
                int Lcount = 1;

                foreach (string leave_name in splt)
                {
                    dtSingleRecord.Columns.Add("L" + Lcount);
                    drColName["L" + Lcount] = leave_name.ToString();
                    Lcount++;
                    colpos++;
                }
                colpos++;
                dtSingleRecord.Columns.Add("LWP");
                drColName["LWP"] = "LWP";
                dtSingleRecord.Columns.Add("Total");
                drColName["Total"] = "Total";
                dtSingleRecord.Columns.Add("LCL");
                drColName["LCL"] = "LCL";
                dtSingleRecord.Columns.Add("No Punch/Working Hrs.");
                drColName["No Punch/Working Hrs."] = "No Punch/Working Hrs.";
                dtSingleRecord.Columns.Add("Single");
                drColName["Single"] = "Single";
                dtSingleRecord.Columns.Add("Late");
                drColName["Late"] = "Late";
                dtSingleRecord.Columns.Add("Early");
                drColName["Early"] = "Early";
                dtSingleRecord.Columns.Add("EL");
                drColName["EL"] = "EL";
                dtSingleRecord.Columns.Add("EE");
                drColName["EE"] = "EE";

                dt.Columns.Add("Day");
                dt.Columns.Add("In Time");
                dt.Columns.Add("In Punch Time");
                dt.Columns.Add("Late");
                dt.Columns.Add("Out Time");
                dt.Columns.Add("Out Punch Time");
                dt.Columns.Add("Early");
                dt.Columns.Add("Single");
                dt.Columns.Add("EE");
                dt.Columns.Add("EL");
                dt.Columns.Add("No Punch/Working Hrs.");
                dt.Columns.Add("FP Emp Id");
                dt.Columns.Add("Employee Name");
                dtSingleRecord.Rows.Add(drColName);

                DataRow[] drfindemp = null;
                if (ddlemp.SelectedIndex == 0)
                {
                    drfindemp = dtEmpList.Select();
                }
                else
                {
                    drfindemp = dtEmpList.Select("EmpId=" + strempid);
                }
                double LeaveCounts = 0;
                double PrevLWP = 0;
                double CurrLWP = 0;
                string LeaveName = string.Empty;
                int grpid = 0;
                string Emp_ids = "";
                string rpt = "~/Reports/09";
                string filePath = HttpContext.Current.Server.MapPath(rpt);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (DataRow dr in drfindemp)
                {
                    DataTable dtSingleRecordcopy = dtSingleRecord.Copy();
                    int EmpId = int.Parse(dr["EmpId"].ToString());
                    string strfpempid = dr["FPEmpId"].ToString();
                    string EmployeeName = dr["EmployeeName"].ToString();
                    string EmpPhone = dr["EmpPhone"].ToString();
                    int.TryParse(dr["Group_id"].ToString(), out grpid);
                    if (strfpempid.Trim() != "" && strfpempid != "0")
                    {
                        int fpempid = int.Parse(strfpempid);

                        dtSingleRecordcopy.Rows[0]["FPEmpId"] = fpempid.ToString();
                        dtSingleRecordcopy.Rows[0]["intFPEmpId"] = fpempid;
                        dtSingleRecordcopy.Rows[0]["EmployeeName"] = EmployeeName;
                        dtSingleRecordcopy.Rows[0]["EmpPhone"] = EmpPhone.ToString();
                        dtSingleRecordcopy.Rows[0]["Month"] = ddlmonth.Value.ToString();

                        double Total_CL_Taken_In_this_Month = 0;
                        double Total_ML_Taken_In_this_Month = 0;
                        double Total_LWP_Taken_In_this_Month = 0;
                        double Total_LCL_Taken_In_this_Month = 0;

                        object objPrevLWPSum = null;

                        if (dtAllPreviousMonthLeaves != null && dtAllPreviousMonthLeaves.Rows.Count > 0)
                        {
                            objPrevLWPSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-1");
                        }
                        //  Total_LWP_Taken_In_this_Month = objEmpLeave.GetEmployeeLeaveCountBetweenTwoDates(1 + "/" + monthid + "/" + year, total_days_in_month + "/" + monthid + "/" + year, int.Parse(Application_Global.AcademicYearId), EmpId, -1);
                        string StartDate = 1 + "/" + monthid + "/" + year;
                        string EndDate = total_days_in_month + "/" + monthid + "/" + year;
                        string[] start_date = StartDate.ToString().Split('/');
                        string[] end_date = EndDate.ToString().Split('/');


                        DataTable dtleave_history_monthwise = objmysqldb.GetData("select * from employee_management.leave_history_monthwise where Month_Id=" + monthid + " and Month_Year=" + year + " and IsDelete=0 and Emp_Id=" + EmpId + ";");
                        DataRow[] drlwp = dtleave_history_monthwise.Select("Leave_Id='-4'");
                        if (drlwp.Length > 0)
                        {
                            Total_LWP_Taken_In_this_Month = double.Parse(drlwp[0]["Total_Leave"].ToString());
                        }

                        DataRow[] drcl = dtleave_history_monthwise.Select("Leave_Id='1'");
                        if (drcl.Length > 0)
                        {
                            Total_CL_Taken_In_this_Month = double.Parse(drcl[0]["Total_Leave"].ToString());
                        }
                        DataRow[] drml = dtleave_history_monthwise.Select("Leave_Id='2'");
                        if (drml.Length > 0)
                        {
                            Total_ML_Taken_In_this_Month = double.Parse(drml[0]["Total_Leave"].ToString());
                        }

                        object objCurrLWPSum = Total_LWP_Taken_In_this_Month;//dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-1");
                        LeaveName = dtSingleRecordcopy.Rows[0][(int)hsColPos[-1]].ToString();

                        PrevLWP = objPrevLWPSum != null && objPrevLWPSum.ToString().Trim() != "" ? double.Parse(objPrevLWPSum.ToString()) : 0.0;
                        CurrLWP = objCurrLWPSum != null && objCurrLWPSum.ToString().Trim() != "" ? double.Parse(objCurrLWPSum.ToString()) : 0.0;
                        dtSingleRecordcopy.Rows[0][(int)hsColPos[-1]] = LeaveName.ToString() + " - " + (PrevLWP).ToString();
                        LeaveName = dtSingleRecordcopy.Rows[0][(int)hsColPos[-1] + (dtLeaveList.Rows.Count * 2) + 2].ToString();
                        dtSingleRecordcopy.Rows[0][(int)hsColPos[-1] + (dtLeaveList.Rows.Count * 2) + 2] = LeaveName + " - " + CurrLWP;

                        DataRow[] drlcl = dtleave_history_monthwise.Select("Leave_Id='-1'");
                        if (drlcl.Length > 0)
                        {
                            Total_LCL_Taken_In_this_Month = double.Parse(drlcl[0]["Total_Leave"].ToString());
                        }
                        object objCurrLCLSum = Total_LCL_Taken_In_this_Month;

                        LeaveCounts = 0;
                        LeaveCounts = objCurrLCLSum != null && objCurrLCLSum.ToString().Trim() != "" ? double.Parse(objCurrLCLSum.ToString()) : 0.0;
                        LeaveName = string.Empty;
                        LeaveName = dtSingleRecordcopy.Rows[0]["LCL"].ToString();
                        dtSingleRecordcopy.Rows[0]["LCL"] = LeaveName.ToString() + " - " + LeaveCounts.ToString();
                        LeaveName = string.Empty;
                        LeaveName = dtSingleRecordcopy.Rows[0][((int)hsColPos[-1] + dtLeaveList.Rows.Count + 1)].ToString();
                        LeaveCounts = 0;

                        double total_leave = 0.0;
                        double total_leave_previous = 0.0;
                        total_leave += objCurrLWPSum != null && objCurrLWPSum.ToString().Trim() != "" ? double.Parse(objCurrLWPSum.ToString()) : 0.0;
                        total_leave_previous += objPrevLWPSum != null && objPrevLWPSum.ToString().Trim() != "" ? double.Parse(objPrevLWPSum.ToString()) : 0.0;

                        foreach (DataRow dr1 in dtLeaveList.Rows)
                        {
                            object objPrevLeaveSum = null;
                            if (dtAllPreviousMonthLeaves != null && dtAllPreviousMonthLeaves.Rows.Count > 0)
                            {
                                objPrevLeaveSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=" + int.Parse(dr1["Leave_Id"].ToString()) + "");
                            }
                            LeaveName = string.Empty;
                            LeaveCounts = 0;
                            LeaveName = dtSingleRecordcopy.Rows[0][(int)hsColPos[int.Parse(dr1["Leave_Id"].ToString())]].ToString();
                            LeaveCounts = objPrevLeaveSum != null && objPrevLeaveSum.ToString().Trim() != "" ? double.Parse(objPrevLeaveSum.ToString()) : 0.0;
                            dtSingleRecordcopy.Rows[0][(int)hsColPos[int.Parse(dr1["Leave_Id"].ToString())]] = LeaveName.ToString() + " - " + LeaveCounts.ToString();
                            LeaveName = string.Empty;
                            LeaveCounts = 0;

                            object objCurrLeaveSum = 0;
                            LeaveName = dtSingleRecordcopy.Rows[0][((int)hsColPos[int.Parse(dr1["Leave_Id"].ToString())] + dtLeaveList.Rows.Count + 1)].ToString();
                            if (LeaveName.Equals("CL"))
                            {
                                objCurrLeaveSum = Total_CL_Taken_In_this_Month;
                            }
                            else
                            {
                                objCurrLeaveSum = Total_ML_Taken_In_this_Month;
                            }
                            LeaveCounts = objCurrLeaveSum != null && objCurrLeaveSum.ToString().Trim() != "" ? double.Parse(objCurrLeaveSum.ToString()) : 0.0;
                            dtSingleRecordcopy.Rows[0][((int)hsColPos[int.Parse(dr1["Leave_Id"].ToString())] + dtLeaveList.Rows.Count + 1)] = LeaveName.ToString() + " - " + LeaveCounts.ToString();

                            total_leave += objCurrLeaveSum != null && objCurrLeaveSum.ToString().Trim() != "" ? double.Parse(objCurrLeaveSum.ToString()) : 0.0;
                            total_leave_previous += objPrevLeaveSum != null && objPrevLeaveSum.ToString().Trim() != "" ? double.Parse(objPrevLeaveSum.ToString()) : 0.0;

                        }

                        LeaveName = string.Empty;
                        LeaveName = dtSingleRecordcopy.Rows[0]["Total"].ToString();

                        dtSingleRecordcopy.Rows[0]["Total"] = LeaveName + " - " + total_leave;
                        LeaveName = string.Empty;
                        LeaveName = dtSingleRecordcopy.Rows[0]["Upto Previous Total"].ToString();

                        dtSingleRecordcopy.Rows[0]["Upto Previous Total"] = LeaveName.ToString() + " - " + total_leave_previous;
                        DataRow[] drfindpunchtimeglobal = dtEmpFPData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        if (drfindpunchtimeglobal.Length > 0)
                        {
                            string[] unqarr = { "Emp_day", "Emp_month", "Emp_year" };
                            DataTable dtuniqdata = drfindpunchtimeglobal.CopyToDataTable().DefaultView.ToTable(true, unqarr);
                            string holidays = "";
                            for (int holiday = 0; holiday < dtholiday.Rows.Count; holiday++)
                            {
                                holidays += dtholiday.Rows[holiday]["Holiday_Day"].ToString() + ",";
                            }
                            holidays = holidays.TrimEnd(',');
                            DataRow[] findholiday = null;
                            if (holidays != "")
                            {
                                findholiday = dtuniqdata.Select("Emp_day NOT IN(" + holidays + ")");
                                dtuniqdata = findholiday.CopyToDataTable();
                            }
                            else
                            {
                                dtuniqdata = dtuniqdata.Clone();
                            }

                            int totalworkingdays = total_days_in_month - dtholiday.Rows.Count;
                            int totalabsentdays = totalworkingdays - dtuniqdata.Rows.Count;

                            int st_day = 1;//int.Parse(dtpfrom.Text.Substring(0, 2));
                            int st_month = monthid;//int.Parse(dtpfrom.Text.Substring(3, 2));
                            int st_year = year;//int.Parse(dtpfrom.Text.Substring(6));

                            DateTime dtDate = new DateTime(st_year, st_month, st_day);

                            int totalfullpresentdays = 0;
                            int totallatedays = 0;
                            int totalearlydays = 0;
                            int totalElatedays = 0;
                            int totalEearlydays = 0;
                            int temp_totalEearlydays = 0;
                            int totalsinglepunchdays = 0;
                            int totalnopunchdays = 0;

                            for (int i = 0; i < total_days_in_month; i++)
                            {
                                DataRow drnew = dt.NewRow();
                                drnew["FP Emp Id"] = fpempid;
                                drnew["Employee Name"] = EmployeeName;
                                DataRow[] drholiday = dtholiday.Select("Holiday_Day=" + dtDate.Day + " and Holiday_Month=" + dtDate.Month + " and Holiday_Year=" + dtDate.Year);
                                if (drholiday.Length == 0)
                                {
                                    bool isSinglePunch = false;
                                    bool isLateCome = false;
                                    bool isEarlyLeave = false;
                                    bool isExtremeLateCome = false;
                                    bool isExtremeEarlyLeave = false;

                                    string punchintime = "-";
                                    string punchouttime = "-";
                                    string actualintime = "-";
                                    string actualouttime = "-";
                                    string lateby = "-";
                                    string earlyby = "-";
                                    string Elate = "-";
                                    string Eearly = "-";
                                    string NoPunch = "-";
                                    string singlepunch = "-";

                                    int punch_emphour_global = 0;
                                    int punch_empmin_global = 0;
                                    int punch_empsec_global = 0;

                                    TimeSpan tm1 = new TimeSpan();
                                    TimeSpan tm2 = new TimeSpan();

                                    DataRow[] drfindpunchtime = drfindpunchtimeglobal.CopyToDataTable().Select("Emp_day=" + dtDate.Day + " and Emp_month=" + dtDate.Month + " and Emp_year=" + dtDate.Year, "Emp_hour,Emp_min,Emp_sec asc");
                                    if (drfindpunchtime.Length > 0)
                                    {
                                        punch_emphour_global = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                                        punch_empmin_global = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                                        punch_empsec_global = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());
                                        int inPunch = punch_emphour_global;
                                        getEmployeeTime(ref inPunch, punch_empmin_global, punch_empsec_global, ref punchintime);

                                        TimeSpan tm11 = TimeSpan.Parse(punch_emphour_global.ToString() + ":" + punch_empmin_global.ToString());
                                        tm1 = tm11;
                                        if (drfindpunchtime.Length == 1)
                                        {
                                            isSinglePunch = true;
                                            //totalsinglepunchdays++;
                                        }
                                    }
                                    int punch_emphour_global_out = 0;
                                    int punch_empmin_global_out = 0;
                                    int punch_empsec_global_out = 0;

                                    if (drfindpunchtime.Length > 1)
                                    {
                                        punch_emphour_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_hour"].ToString());
                                        punch_empmin_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_min"].ToString());
                                        punch_empsec_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_sec"].ToString());
                                        int outPunch = punch_emphour_global_out;
                                        getEmployeeTime(ref outPunch, punch_empmin_global_out, punch_empsec_global_out, ref punchouttime);

                                        TimeSpan tm22 = TimeSpan.Parse(punch_emphour_global_out.ToString() + ":" + punch_empmin_global_out.ToString());
                                        tm2 = tm22;
                                    }
                                    // DataTable dtEmpTimeConfig = new DataTable();
                                    int intday = 0;
                                    //string searchday = (DateTime.Parse(Date.ToString()).DayOfWeek).ToString();
                                    intday = (int)dtDate.DayOfWeek;
                                    if (intday == 0)
                                    { intday = 7; }
                                    DataRow[] drfindtime = dtEmpTimeConfig.Select("emp_id=" + EmpId + " and Day_id=" + intday);
                                    int emphour_global = 0;
                                    int empmin_global = 0;

                                    int emphour_global_out = 0;
                                    int empmin_global_out = 0;

                                    int EL_emphour_global = 0;
                                    int EL_empmin_global = 0;

                                    int EE_emphour_global_out = 0;
                                    int EE_empmin_global_out = 0;
                                    if (drfindtime.Length > 0)
                                    {
                                        emphour_global = int.Parse(drfindtime[0]["In_hour"] != null && drfindtime[0]["In_hour"].ToString().Trim() != "" ? drfindtime[0]["In_hour"].ToString() : "0");
                                        empmin_global = int.Parse(drfindtime[0]["In_min"] != null && drfindtime[0]["In_min"].ToString().Trim() != "" ? drfindtime[0]["In_min"].ToString() : "0");

                                        int intactualintime = emphour_global;
                                        getEmployeeTime(ref intactualintime, empmin_global, 0, ref actualintime);

                                        emphour_global_out = int.Parse(drfindtime[0]["out_hour"] != null && drfindtime[0]["out_hour"].ToString().Trim() != "" ? drfindtime[0]["out_hour"].ToString() : "0");
                                        //if(emphour_global_out==13)
                                        //    {}
                                        empmin_global_out = int.Parse(drfindtime[0]["out_min"] != null && drfindtime[0]["out_min"].ToString().Trim() != "" ? drfindtime[0]["out_min"].ToString() : "0");

                                        int intactualouttime = emphour_global_out;
                                        getEmployeeTime(ref intactualouttime, empmin_global_out, 0, ref actualouttime);

                                        EE_emphour_global_out = int.Parse(drfindtime[0]["Ext_Early_hour"] != null && drfindtime[0]["Ext_Early_hour"].ToString().Trim() != "" ? drfindtime[0]["Ext_Early_hour"].ToString() : "0");
                                        EE_empmin_global_out = int.Parse(drfindtime[0]["Ext_Early_min"] != null && drfindtime[0]["Ext_Early_min"].ToString().Trim() != "" ? drfindtime[0]["Ext_Early_min"].ToString() : "0");

                                        EL_emphour_global = int.Parse(drfindtime[0]["Ext_Late_hour"] != null && drfindtime[0]["Ext_Late_hour"].ToString().Trim() != "" ? drfindtime[0]["Ext_Late_hour"].ToString() : "0");
                                        EL_empmin_global = int.Parse(drfindtime[0]["Ext_Late_min"] != null && drfindtime[0]["Ext_Late_min"].ToString().Trim() != "" ? drfindtime[0]["Ext_Late_min"].ToString() : "0");
                                    }
                                    if ((emphour_global == 0 && empmin_global == 0) || (punch_emphour_global == 0 && punch_empmin_global == 0) || (EL_emphour_global == 0 && EL_empmin_global == 0))
                                    {
                                    }
                                    else
                                    {
                                        if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                        {
                                            if ((punch_emphour_global > EL_emphour_global) || (punch_emphour_global == EL_emphour_global && punch_empmin_global >= EL_empmin_global))
                                            {
                                                isExtremeLateCome = true;
                                            }
                                            else
                                            {
                                                isLateCome = true;
                                            }
                                        }
                                        else
                                        {
                                            isExtremeLateCome = false;
                                            isLateCome = false;
                                        }
                                    }
                                    if ((emphour_global_out == 0 && empmin_global_out == 0) || (punch_emphour_global_out == 0 && punch_empmin_global_out == 0))
                                    {
                                    }
                                    else
                                    {
                                        if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                        {
                                            if ((punch_emphour_global_out < EE_emphour_global_out) || (punch_emphour_global_out == EE_emphour_global_out && punch_empmin_global_out < EE_empmin_global_out))
                                            {
                                                isExtremeEarlyLeave = true;
                                            }
                                            else
                                            {
                                                isEarlyLeave = true;
                                            }
                                        }
                                        else
                                        {
                                            isExtremeEarlyLeave = false;
                                            isEarlyLeave = false;
                                        }
                                    }
                                    if (punch_emphour_global == 0 && punch_empmin_global == 0 && punch_empsec_global == 0 && punch_emphour_global_out == 0 && punch_empmin_global_out == 0 && punch_empsec_global_out == 0)
                                    {

                                        totalnopunchdays++;
                                        NoPunch = "Yes";
                                        drnew["No Punch/Working Hrs."] = NoPunch;
                                    }
                                    else
                                    {
                                        drnew["No Punch/Working Hrs."] = NoPunch;
                                    }
                                    if (isEarlyLeave || isSinglePunch || isLateCome || isExtremeEarlyLeave || isExtremeLateCome)
                                    {
                                        if (isEarlyLeave)
                                        {
                                            totalearlydays++;
                                            earlyby = GetDifferenceInTime(emphour_global_out, empmin_global_out, punch_emphour_global_out, punch_empmin_global_out, punch_empsec_global_out, true);
                                        }
                                        if (isExtremeEarlyLeave)
                                        {
                                            totalEearlydays++;
                                            // earlyby = XGlobals.GetDifferenceInTime(EE_emphour_global_out, EE_empmin_global_out, punch_emphour_global_out, punch_empmin_global_out, punch_empsec_global_out, true);
                                            earlyby = GetDifferenceInTime(emphour_global_out, empmin_global_out, punch_emphour_global_out, punch_empmin_global_out, punch_empsec_global_out, true);
                                            Eearly = "Yes";
                                        }
                                        if (isSinglePunch)
                                        {
                                            totalsinglepunchdays++;
                                            singlepunch = "Yes";
                                        }
                                        if (isLateCome)
                                        {
                                            totallatedays++;
                                            lateby = GetDifferenceInTime(emphour_global, empmin_global, punch_emphour_global, punch_empmin_global, punch_empsec_global, false);
                                        }
                                        if (isExtremeLateCome)
                                        {
                                            totalElatedays++;
                                            //lateby = XGlobals.GetDifferenceInTime(EL_emphour_global, EL_empmin_global, punch_emphour_global, punch_empmin_global, punch_empsec_global, false);
                                            lateby = GetDifferenceInTime(emphour_global, empmin_global, punch_emphour_global, punch_empmin_global, punch_empsec_global, false);
                                            Elate = "Yes";
                                        }
                                        if (isExtremeLateCome && isExtremeEarlyLeave)
                                        {
                                            temp_totalEearlydays++;
                                        }


                                    }
                                    else
                                    {
                                        if (punch_emphour_global == 0 && punch_empmin_global == 0)
                                        {

                                        }
                                        else
                                        {
                                            totalfullpresentdays++;
                                        }
                                    }
                                    drnew["In Time"] = actualintime;
                                    drnew["In Punch Time"] = punchintime;
                                    drnew["Late"] = lateby;
                                    drnew["Out Time"] = actualouttime;
                                    drnew["Out Punch Time"] = punchouttime;
                                    drnew["Early"] = earlyby;
                                    drnew["Single"] = singlepunch;
                                    drnew["EE"] = Eearly;
                                    drnew["EL"] = Elate;
                                    if (NoPunch.Equals("-") && tm1.Hours != 0 && tm2.Hours != 0)
                                    {
                                        TimeSpan difference = tm2 - tm1;

                                        int hours = difference.Hours;
                                        int minutes = difference.Minutes;

                                        drnew["No Punch/Working Hrs."] = +hours + " hr " + minutes + " min";
                                    }
                                    else
                                    {
                                        drnew["No Punch/Working Hrs."] = NoPunch;
                                    }
                                }
                                else
                                {
                                    drnew["In Time"] = drholiday[0]["Holiday_Reason"].ToString();
                                }
                                drnew["Day"] = dtDate.Day;
                                dt.Rows.Add(drnew);
                                dtDate = dtDate.AddDays(1);
                            }
                            temp_totalEearlydays = totalEearlydays - temp_totalEearlydays;
                            double single_punch_in_this_month = double.Parse(totalsinglepunchdays.ToString());
                            double early_punch_in_this_month = double.Parse(totalearlydays.ToString());
                            double nopunch_punch_in_this_month = double.Parse(totalnopunchdays.ToString());
                            double Late_punch_in_this_month = double.Parse(totallatedays.ToString());
                            double EL_punch_in_this_month = double.Parse(totalElatedays.ToString());
                            double EE_punch_in_this_month = double.Parse(temp_totalEearlydays.ToString());
                            double total_leave_final = 0.0;

                            total_leave_final = (0.5 * single_punch_in_this_month) + Math.Floor((early_punch_in_this_month / 3)) + (1 * nopunch_punch_in_this_month) + Math.Floor((Late_punch_in_this_month / 3)) + (0.5 * EL_punch_in_this_month) + (0.5 * EE_punch_in_this_month);

                            string ml_previousmonth = dtSingleRecordcopy.Rows[0]["C2"].ToString();

                            ml_previousmonth = ml_previousmonth.Replace("Total ML -", "");

                            int mlpreviousmonth = int.Parse(ml_previousmonth);
                            if (mlpreviousmonth == 0 || mlpreviousmonth == 1 || mlpreviousmonth == 2)
                            {
                                dtSingleRecordcopy.Rows[0]["L2"] = "ML - 1";
                                total_leave_final--;
                            }
                            if (total_leave_final <= 2)
                            {
                                dtSingleRecordcopy.Rows[0]["L1"] = "CL-" + total_leave_final.ToString();
                                total_leave_final = total_leave_final - total_leave_final;
                            }
                            else
                            {
                                dtSingleRecordcopy.Rows[0]["L1"] = "CL-2";
                                total_leave_final = total_leave_final - 2;
                            }
                            dtSingleRecordcopy.Rows[0]["LWP"] = "LWP-" + total_leave_final.ToString();


                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["No Punch/Working Hrs."].ToString();
                            dtSingleRecordcopy.Rows[0]["No Punch/Working Hrs."] = "No Punch - " + totalnopunchdays.ToString();
                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["Late"].ToString();
                            dtSingleRecordcopy.Rows[0]["Late"] = LeaveName + " - " + totallatedays.ToString();
                            //((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["LateCount"]).Text = totallatedays.ToString();
                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["Early"].ToString();
                            dtSingleRecordcopy.Rows[0]["Early"] = LeaveName + " - " + totalearlydays.ToString();
                            //((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["Early"]).Text = totalearlydays.ToString();
                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["EL"].ToString();
                            dtSingleRecordcopy.Rows[0]["EL"] = LeaveName + " - " + totalElatedays.ToString();
                            // ((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["EXL"]).Text = totalElatedays.ToString();
                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["EE"].ToString();
                            dtSingleRecordcopy.Rows[0]["EE"] = LeaveName + " - " + totalEearlydays.ToString();
                            // ((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["EXE"]).Text = totalEearlydays.ToString();
                            LeaveName = string.Empty;
                            LeaveName = dtSingleRecordcopy.Rows[0]["Single"].ToString();
                            dtSingleRecordcopy.Rows[0]["Single"] = LeaveName + " - " + totalsinglepunchdays.ToString();
                            // ((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["SPunch"]).Text = totalsinglepunchdays.ToString();

                            //((CrystalDecisions.CrystalReports.Engine.TextObject)objRpt.ReportDefinition.ReportObjects["FNoPunch"]).Text = totalnopunchdays.ToString();
                        }
                        else
                        {
                        }
                    }
                    DataTable dttotal = dtSingleRecordcopy.DefaultView.ToTable(true, "EE", "EL", "No Punch/Working Hrs.", "Late", "Early", "EmployeeName", "Single");
                    dt.Merge(dttotal);
                    dt.Rows[dt.Rows.Count - 1]["In Time"] = "Total";
                    dt.Rows[dt.Rows.Count - 1]["Out Time"] = "Total";
                    dttotal1.Merge(dt);

                    DataTable dtuptoprevious = dtSingleRecordcopy.DefaultView.ToTable(true, "Total LWP", "C1", "C2", "Upto Previous Total");
                    //double Total_LWP_in_previous_month = double.Parse(dtuptoprevious.Rows[0]["Total LWP"].ToString());
                    //double Total_CL_in_previous_month = double.Parse(dtuptoprevious.Rows[0]["C1"].ToString());
                    //double Total_ML_in_previous_month = double.Parse(dtuptoprevious.Rows[0]["C2"].ToString());
                    //double Total_uptoprevious_in_previous_month = double.Parse(dtuptoprevious.Rows[0]["Upto Previous Total"].ToString());

                    DataTable dtcurrentmonth = dtSingleRecordcopy.DefaultView.ToTable(true, "L1", "L2", "EE", "EL", "No Punch/Working Hrs.", "Late", "Early", "LWP", "Single", "LCL");




                    DataTable dtPdfMain = dt.DefaultView.ToTable(true, "Day", "In Time", "In Punch Time", "Late", "Out Time", "Out Punch Time", "Early", "Single", "EE", "EL", "No Punch/Working Hrs.");


                    DataTable dtmonthly = dt.Copy();
                    dt = dt.Clone();
                    DataRow dtadd = dtmonthly.NewRow();
                    dtmonthly.Rows.Add(dtadd);//blank row
                    DataRow dtadd1 = dtmonthly.NewRow();
                    dtmonthly.Rows.Add(dtadd1);//blank row

                    DataRow dtadd2 = dtmonthly.NewRow();
                    dtadd2["In Time"] = "Upto Previous Month";
                    dtadd2["Late"] = dtuptoprevious.Rows[0]["Total LWP"].ToString();
                    dtadd2["Early"] = "Current Month";
                    dtadd2["Single"] = dtcurrentmonth.Rows[0]["No Punch/Working Hrs."].ToString();
                    dtadd2["EE"] = dtcurrentmonth.Rows[0]["LWP"].ToString();
                    dtmonthly.Rows.Add(dtadd2);

                    DataRow dtadd6 = dtmonthly.NewRow();
                    dtadd6["Late"] = dtuptoprevious.Rows[0]["C1"].ToString();
                    dtadd6["Single"] = dtcurrentmonth.Rows[0]["L1"].ToString();
                    dtadd6["EE"] = dtcurrentmonth.Rows[0]["Single"].ToString();
                    dtmonthly.Rows.Add(dtadd6);

                    DataRow dtadd7 = dtmonthly.NewRow();
                    dtadd7["Late"] = dtuptoprevious.Rows[0]["C2"].ToString();
                    dtadd7["Single"] = dtcurrentmonth.Rows[0]["L2"].ToString();
                    dtadd7["EE"] = dtcurrentmonth.Rows[0]["LCL"].ToString();
                    dtmonthly.Rows.Add(dtadd7);

                    DataRow dtadd8 = dtmonthly.NewRow();
                    dtadd8["Late"] = dtuptoprevious.Rows[0]["Upto Previous Total"].ToString();
                    dtadd8["Single"] = dtcurrentmonth.Rows[0]["EE"].ToString();
                    dtmonthly.Rows.Add(dtadd8);

                    DataRow dtadd3 = dtmonthly.NewRow();
                    dtadd3["Single"] = dtcurrentmonth.Rows[0]["EL"].ToString();
                    dtmonthly.Rows.Add(dtadd3);

                    DataRow dtadd4 = dtmonthly.NewRow();
                    dtadd4["Single"] = dtcurrentmonth.Rows[0]["Late"].ToString();
                    dtmonthly.Rows.Add(dtadd4);

                    DataRow dtadd5 = dtmonthly.NewRow();
                    dtadd5["Single"] = dtcurrentmonth.Rows[0]["Early"].ToString();
                    dtmonthly.Rows.Add(dtadd5);//current month

                    dttotal2.Merge(dtmonthly);

                    #region Pdf Code

                    //DataTable dtPdfMain = dtmonthly.DefaultView.ToTable(true, "Day", "In Time", "In Punch Time", "Late", "Out Time", "Out Punch Time", "Early", "Single", "EE", "EL", "NoPunch");

                    Document doc = new Document();
                    doc = new Document(PageSize.LEGAL);     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dtPdfMain.Columns.Count);
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }


                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;

                    StringBuilder sb = new StringBuilder();
                    string aa = ddlmonth.Items[ddlmonth.SelectedIndex].Text.ToString() + " - " + ddlyear.Items[ddlyear.SelectedIndex].Text.ToString();
                    //Generate Invoice (Bill) Header.
                    sb.Append("<b><hr  width='100%'></b>");
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Employeewise Monthly Leave Count Details</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Month: </b> " + aa + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Report Time: </b> " + MySQLDB.GetIndianTime().ToString("dd/MM/yyyy").Substring(0, 10) + "</td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Employee Name: </b> " + EmployeeName + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Mobile Number: </b> " + EmpPhone + "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");


                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<br />");
                    sb1.Append("<br />");
                    sb1.Append("<table  width='100%' cellspacing='0' cellpadding='1' border='1'  >");
                    sb1.Append("<tr><td align='left' style='font-size:9px;font-face:HELVETICA'><b>Upto Previous Month</b></td><td colspan='2' align='center' style='font-size:9px;font-face:HELVETICA'><b>Current Month</b></td></tr>");
                    sb1.Append("<tr><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtuptoprevious.Rows[0]["Total LWP"].ToString() + " </td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["Late"].ToString() + "</td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["LWP"].ToString() + "</td></tr>");
                    sb1.Append("<tr><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtuptoprevious.Rows[0]["C1"].ToString() + " </td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["Early"].ToString() + "</td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["L2"].ToString() + "</td></tr>");
                    sb1.Append("<tr><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtuptoprevious.Rows[0]["C2"].ToString() + " </td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["EL"].ToString() + "</td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["L1"].ToString() + "</td></tr>");
                    sb1.Append("<tr><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtuptoprevious.Rows[0]["Upto Previous Total"].ToString() + " </td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["EE"].ToString() + "</td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["Single"].ToString() + "</td></tr>");
                    sb1.Append("<tr><td align='left' style='font-size:8px;font-face:HELVETICA'> </td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["No Punch/Working Hrs."].ToString() + "</td><td align='left' style='font-size:8px;font-face:HELVETICA'>" + dtcurrentmonth.Rows[0]["LCL"].ToString() + "</td></tr>");
                    sb1.Append("</table>");
                    sb1.Append("<br />");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\" + EmpId + ".pdf", FileMode.Create));
                    Emp_ids += EmpId.ToString() + ",";
                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    StringReader sr1 = new StringReader(sb1.ToString());
                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dtPdfMain, report));
                    htmlparser.Parse(sr1);
                    doc.Add(paragraphfotter);
                    //byte[] b = File.ReadAllBytes(Server.MapPath("Late_Comers_Report.pdf"));
                    // Closing the document
                    doc.Close();

                    //Process.Start(Server.MapPath("Daily_Punch_Detail.pdf"));

                    #endregion


                }
                dt.TableName = "dtEmployeewiseLeave";
                dtSingleRecord.TableName = "dtSingle";
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(dtSingleRecord);


                string[] employees = Emp_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (employees.Length > 0)
                {
                    Document document = new Document();

                    // step 2: we create a writer that listens to the document
                    PdfCopy writer = new PdfCopy(document, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf", FileMode.Create));
                    if (writer == null)
                    {
                        return;
                    }

                    // step 3: we open the document
                    document.Open();

                    for (int j = 0; j < employees.Length; j++)
                    {
                        // we create a reader for a certain document
                        PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(rpt) + "\\" + employees[j].ToString() + ".pdf");
                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }
                    writer.Close();
                    document.Close();
                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports//09";
                    test = test + "//All.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                }


                if (dttotal1 != null && dttotal1.Rows.Count > 0)
                {
                    if (dttotal1.Columns.Contains("EmployeeName"))
                    {
                        dttotal1.Columns.Remove("EmployeeName");
                    }
                    dttotal1.Columns["FP Emp Id"].SetOrdinal(1);
                    dttotal1.Columns["Employee Name"].SetOrdinal(2);
                    grd.DataSource = dttotal1;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }


                if (dttotal2.Columns.Contains("EmployeeName"))
                {
                    dttotal2.Columns.Remove("EmployeeName");
                }
                dttotal2.Columns["FP Emp Id"].SetOrdinal(1);
                dttotal2.Columns["Employee Name"].SetOrdinal(2);
                ViewState["GVDATA"] = dttotal2;
            }
            catch (Exception ee)
            {
                Logger.WriteCriticalLog("AttendanceReports 4070: exception:" + ee.Message + "::::::::" + ee.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        private void EmployeeWiseAttaendanceReport(string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                //DateTime dt = Convert.ToDateTime(date);
                //long Ticks = dt.Ticks;
                DataTable dtbindmain = new DataTable();
                int month_id = int.Parse(ddlmonth.Items[ddlmonth.SelectedIndex].Value.ToString());
                int year = 0;
                int.TryParse(ddlyear.Value.ToString(), out year);
                //string year=ddlyear.DataTextField.ToString();
                DateTime startOfMonth = new DateTime(year, month_id, 1);   //new DateTime(year, month, 1);
                DateTime endOfMonth = new DateTime(year, month_id, DateTime.DaysInMonth(year, month_id));
                long startTicks = startOfMonth.Ticks;
                long EndDateTicks = endOfMonth.Ticks;
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtEmp = new DataTable();
                DataTable dtAttendance = new DataTable();
                string emp_id = "";
                emp_ids = emp_ids.TrimEnd(',');
                DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup inner join assign_holidayprofile_employee on assign_holidayprofile_employee.holiday_profile_id=holiday_setup.Holiday_Profile_Id where holiday_setup.IsDelete=0 and holiday_setup.Holiday_Year=" + year + " and holiday_setup.Holiday_Month=" + month_id + " and assign_holidayprofile_employee.IsDelete=0");
                dtEmp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,FP_Id as FPEmpId from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid;");
                if (emp_ids.Equals("-1"))
                {

                    emp_id = ddlemp.Items[ddlemp.SelectedIndex].Value.ToString();
                    emp_ids = emp_id;
                    dtAttendance = objmysqldb.GetData("SELECT emp_id,DateTicks,Type,Overlap,Update_Type FROM employee_management.employee_attendance_daily where IsDelete=0 and DateTicks>=" + startTicks + " and DateTicks<=" + EndDateTicks + " and emp_id=" + emp_id + "");
                    ddlemp.DataSource = dtEmp;
                    ddlemp.DataTextField = "EmployeeName";
                    ddlemp.DataValueField = "empid";
                    ddlemp.DataBind();
                    ddlemp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Employee", "-1"));

                }
                else
                {
                    if (ddlemp.SelectedIndex > 0)
                    {
                        emp_ids = ddlemp.Items[ddlemp.SelectedIndex].Value.ToString();
                    }
                    DataRow[] drBindEmp = dtEmp.Select("empid in(" + emp_ids + ")");
                    if (drBindEmp.Length > 0)
                    {
                        DataTable dtEmp1 = drBindEmp.CopyToDataTable();
                        ddlemp.DataSource = dtEmp1;
                        ddlemp.DataTextField = "EmployeeName";
                        ddlemp.DataValueField = "empid";
                        ddlemp.DataBind();
                        ddlemp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Employee", "-1"));
                    }
                }
                string rpt = "~/Reports/13";
                string filePath = HttpContext.Current.Server.MapPath(rpt);
                //dtEmp.Columns.Add("Status");
                DataTable dtemp2 = dtEmp.Select("empid in(" + emp_ids + ")").CopyToDataTable();
                foreach (DataRow dre in dtemp2.Rows)
                {
                    #region loop
                    dtAttendance = objmysqldb.GetData("SELECT emp_id,DateTicks,Type,Overlap,Update_Type FROM employee_management.employee_attendance_daily where IsDelete=0 and DateTicks>=" + startTicks + " and DateTicks<=" + EndDateTicks + " and emp_id in(" + dre["empid"].ToString() + ")");
                    if (!dtAttendance.Columns.Contains("Status"))
                    {
                        dtAttendance.Columns.Add("Status");
                    }
                    if (!dtAttendance.Columns.Contains("ContactNo"))
                    {
                        dtAttendance.Columns.Add("ContactNo");
                    }
                    if (!dtAttendance.Columns.Contains("FullName"))
                    {
                        dtAttendance.Columns.Add("FullName");
                    }
                    if (!dtAttendance.Columns.Contains("Date"))
                    {
                        dtAttendance.Columns.Add("Date");
                    }
                    string status = "";
                    string Type = "";
                    if (dtAttendance != null && dtAttendance.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAttendance.Rows.Count; i++)
                        {
                            string empid = dtAttendance.Rows[i]["emp_id"].ToString();


                            long Ticks = long.Parse(dtAttendance.Rows[i]["DateTicks"].ToString());
                            DateTime dt = new DateTime();
                            if (Ticks > 0)
                            {
                                DateTime dateTime3 = new DateTime(Ticks);
                                dt = dateTime3;
                                string value2 = string.Format("{0:dd-MM-yyyy}", dateTime3);
                                dtAttendance.Rows[i]["Date"] = value2;
                            }
                            DataRow[] drholiday = dtholiday.Select("Holiday_Day=" + dt.Day + " and Holiday_Month=" + dt.Month + " and Holiday_Year=" + dt.Year);
                            if (drholiday.Length == 0)
                            {
                                //int.TryParse(dtAttendance.Rows[i]["Type"].ToString(), out Type_id);
                                DataRow[] drFind = dtAttendance.Select("emp_id=" + empid + " and Overlap=1");
                                {
                                    if (drFind.Length > 0)
                                    {
                                        string type = drFind[0]["Update_Type"].ToString();
                                        if (type.Equals("1"))
                                        {
                                            status = "Present";
                                            //p++;
                                            //drFind[0]["PresentCount"] = a;
                                        }
                                        else if (type.Equals("2"))
                                        {
                                            status = "Halfday";

                                            //drFind[0]["HalfDayCount"] = a;
                                        }
                                        else
                                        {
                                            status = "Absent";
                                            //a++;
                                            //drFind[0]["AbsentCount"] = a;
                                        }
                                        drFind[0]["Status"] = status;

                                    }
                                    else
                                    {
                                        Type = dtAttendance.Rows[i]["Type"].ToString();
                                        if (Type.Equals("1"))
                                        {
                                            status = "Present";
                                            //p++;
                                            //dtAttendance.Rows[i]["PresentCount"] = h;

                                        }
                                        else if (Type.Equals("2"))
                                        {
                                            status = "Halfday";
                                            //h++;
                                            //dtAttendance.Rows[i]["HalfDayCount"] = h;
                                        }
                                        else
                                        {
                                            status = "Absent";
                                            //a++;
                                            //dtAttendance.Rows[i]["AbsentCount"] = a;
                                        }
                                        dtAttendance.Rows[i]["Status"] = status;

                                    }
                                }
                            }
                            else
                            {
                                dtAttendance.Rows[i]["Status"] = drholiday[0]["Holiday_Reason"].ToString();
                            }
                        }

                    }
                    foreach (DataRow dr in dtEmp.Rows)
                    {
                        string empId = dr["empid"].ToString();

                        DataRow[] dr1 = dtAttendance.Select("emp_id=" + empId + "");
                        if (dr1.Length > 0)
                        {
                            dr1[0]["FullName"] = dr["EmployeeName"];
                            dr1[0]["ContactNo"] = dr["EmpPhone"];

                        }

                    }
                    string[] arr = { "emp_id", "FullName", "ContactNo", "Status", "Date" };
                    string[] arr1 = { "Date", "Status" };
                    //string[] arr1 = { "Date", "Status", "emp_id", "FullName", "ContactNo", };
                    DataTable dtfinal = dtAttendance.DefaultView.ToTable(true, arr);
                    DataTable dtfinal2 = dtfinal.Copy();
                    // DataTable
                    DataTable dtPdf = dtAttendance.DefaultView.ToTable(true, arr1);
                    if (dtfinal2 != null && dtfinal2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtfinal2.Rows)
                        {
                            string empId = dr["emp_id"].ToString();
                            DataRow[] dr1 = dtEmp.Select("empid=" + empId + "");
                            if (dr1.Length > 0)
                            {
                                dr["FullName"] = dr1[0]["EmployeeName"];
                                dr["ContactNo"] = dr1[0]["EmpPhone"];
                            }

                        }
                        dtbindmain.Merge(dtfinal2);
                        //grd.DataSource = dtfinal;
                        //grd.DataBind();

                    }
                    #region StatusCount
                    DataTable dtCount = new DataTable();
                    dtCount.Columns.Add("Status");
                    dtCount.Columns.Add("Count", typeof(int));
                    dtCount.Columns.Add("Emp_id");
                    foreach (DataRow drCount in dtAttendance.Rows)
                    {
                        string emp_id1 = drCount["emp_id"].ToString();
                        DataRow drc = dtCount.NewRow();
                        DataRow[] drp = dtAttendance.Select("Status like 'Present' and emp_id=" + emp_id1 + "");
                        drc["Status"] = "Present";
                        drc["Count"] = drp.Length;
                        drc["Emp_id"] = emp_id1;
                        dtCount.Rows.Add(drc);

                        DataRow drc1 = dtCount.NewRow();
                        DataRow[] dra = dtAttendance.Select("Status like 'Absent' and emp_id=" + emp_id1 + " ");
                        drc1["Status"] = "Absent";
                        drc1["Count"] = dra.Length;
                        drc["Emp_id"] = emp_id1;
                        dtCount.Rows.Add(drc1);

                        DataRow drc2 = dtCount.NewRow();
                        DataRow[] drh = dtAttendance.Select("Status like 'Halfday' and emp_id=" + emp_id1 + " ");
                        drc2["Status"] = "Halfday";
                        drc2["Count"] = drh.Length;
                        drc["Emp_id"] = emp_id1;
                        dtCount.Rows.Add(drc2);
                    }
                    #endregion StatusCount


                    #region PDF

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    DataRow[] drfindemp = null;
                    if (ddlemp.SelectedIndex == 0)
                    {
                        drfindemp = dtEmp.Select();
                    }
                    else
                    {
                        drfindemp = dtEmp.Select("EmpId=" + emp_ids);
                    }
                    Document docnew = new Document();
                    docnew = new Document(PageSize.LEGAL);//page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dtPdf.Columns.Count);
                    string imgPath = headerImg; //Header IMG
                    double widths = 100 / dtPdf.Columns.Count;

                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }
                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;

                    StringBuilder sb = new StringBuilder();
                    string aa = ddlmonth.Items[ddlmonth.SelectedIndex].Text.ToString() + " - " + ddlyear.Items[ddlyear.SelectedIndex].Text.ToString();
                    sb.Append("<b><hr  width='100%'></b>");
                    sb.Append("<table width='100%' cellspacing='0'  cellpadding='2' >");
                    sb.Append("<tr><td  align='center' style='font-size:16px;font-face:HELVETICA'><b>Employeewise Monthly Attendance Details</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Month: </b> " + aa + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Report Time: </b> " + MySQLDB.GetIndianTime().ToString().Substring(0, 10) + "</td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>Employee Name: </b> " + dtfinal.Rows[0]["FullName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Mobile Number: </b> " + dtfinal.Rows[0]["ContactNo"].ToString() + "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("<br />"); 
                    sb.Append("<br />");

                    //StringBuilder sb1 = new StringBuilder();
                    //sb1.Append("<br />");
                    //sb1.Append("<br />");
                    //sb1.Append("<br />");

                    //sb1.Append("<table border='1' align='Left' width='30%'>");
                    //sb1.Append("<tr> <td width='30%' align='left' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[0]["Status"].ToString() + "</td> <td align='left' width='30%' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[0]["Count"].ToString() + "</td></tr>");
                    //sb1.Append("<tr> <td width='30%' align='left' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[1]["Status"].ToString() + "</td> <td width='30%' align='left' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[1]["Count"].ToString() + "</td></tr>");
                    //sb1.Append("<tr> <td width='30%' align='left' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[2]["Status"].ToString() + "</td> <td  width='30%' align='left' style='font-size:8px;font-face:HELVETICA'>" + dtCount.Rows[2]["Count"].ToString() + "</td></tr>");
                    //sb1.Append("</table>");
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<br/>");
                    sb1.Append("<br/>");
                    sb1.Append("<br />");
                    sb1.Append("<table border='1' width='25%'>");
                    sb1.Append("<tr> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[0]["Status"].ToString() + "</td> <td align='center' width='10%' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[0]["Count"].ToString() + "</td></tr>");
                    sb1.Append("<tr> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[1]["Status"].ToString() + "</td> <td  align='center' width='10%' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[1]["Count"].ToString() + "</td></tr>");
                    sb1.Append("<tr> <td align='center' width='10%'  style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[2]["Status"].ToString() + "</td> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[2]["Count"].ToString() + "</td></tr>");
                    sb1.Append("</table>");
                    PdfWriter.GetInstance(docnew, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\" + dre["empid"].ToString() + ".pdf", FileMode.Create));

                    //Open the PDF document
                    StringReader sr = new StringReader(sb.ToString());
                    StringReader sr1 = new StringReader(sb1.ToString());
                    docnew.Open();
                    if (jpg != null)
                    {
                        docnew.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(docnew);
                    //  htmlparser.Parse(sr);
                    htmlparser.Parse(sr);
                    docnew.Add(Add_Content_To_PDF(tableLayout, dtPdf, report));
                    htmlparser.Parse(sr1);
                    docnew.Add(paragraphfotter);
                    docnew.Close();
                    //             string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports//13";
                    //             test = test + "//" + dre["empid"].ToString() + ".pdf";
                    //             Page.ClientScript.RegisterStartupScript(
                    //this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                    #endregion EndPdf
                    #endregion
                }

                if (dtbindmain != null && dtbindmain.Rows.Count > 0)
                {
                    grd.DataSource = dtbindmain;
                    grd.DataBind();
                    btnExport.Visible = true;
                    ViewState["GVDATA"] = dtbindmain;
                    
                }
                string[] employees = emp_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (employees.Length > 1)
                {
                    Document document = new Document();

                    // step 2: we create a writer that listens to the document
                    PdfCopy writer = new PdfCopy(document, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf", FileMode.Create));
                    if (writer == null)
                    {
                        return;
                    }

                    // step 3: we open the document
                    document.Open();

                    for (int j = 0; j < employees.Length; j++)
                    {
                        // we create a reader for a certain document
                        PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath(rpt) + "\\" + employees[j].ToString() + ".pdf");
                        reader.ConsolidateNamedDestinations();

                        // step 4: we add content
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.AddPage(page);
                        }

                        PRAcroForm form = reader.AcroForm;
                        if (form != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                        reader.Close();
                    }
                    writer.Close();
                    document.Close();
                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\All.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports//13";
                    test = test + "//All.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                }
                else
                {
                    if (employees.Length == 1)
                    {
                        string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports//13";
                        test = test + "//" + employees[0].ToString() + ".pdf";
                        Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                    }
                }

            }
            catch (Exception ex)
            {

                //Logger.WriteActivityLog("AttendanceReports EmployeeWiseAttaendanceReport: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                //Response.Write("AttendanceReports  EmployeeWiseAttaendanceReport:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();

            }

        }
        private void AbsentPresentDeatil(string date, string headerImg, int report)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                string[] starr = date.Split('/');
                DateTime dt = new DateTime(int.Parse(starr[2]), int.Parse(starr[1]), int.Parse(starr[0]));
                long Ticks = dt.Ticks;
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup inner join assign_holidayprofile_employee on assign_holidayprofile_employee.holiday_profile_id=holiday_setup.Holiday_Profile_Id where holiday_setup.IsDelete=0 and holiday_setup.Holiday_Year=" + dt.Year + " and holiday_setup.Holiday_Month=" + dt.Month + " and assign_holidayprofile_employee.IsDelete=0");

                DataTable dtEmp = new DataTable();
                emp_ids = emp_ids.TrimEnd(',');
                // string result = Regex.Replace(input, ",+", ",").Trim(',');
                DataTable dtAttendance = objmysqldb.GetData("SELECT emp_id as Emp_Id,DateTicks,Type,Overlap,Update_Type FROM employee_management.employee_attendance_daily where IsDelete=0 and DateTicks=" + Ticks + " and  emp_id in(" + emp_ids + ")");
                dtEmp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS EmployeeName,EmpPhone,FP_Id as FPEmpId from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid;");
                //dtEmp.Columns.Add("Status");
                dtAttendance.Columns.Add("Status");
                dtAttendance.Columns.Add("ContactNo");
                dtAttendance.Columns.Add("FullName");


                string status = "";
                string Type = "";
                int p = 0, a = 0, h = 0;

                if (dtAttendance != null && dtAttendance.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAttendance.Rows.Count; i++)
                    {
                        string empid = dtAttendance.Rows[i]["emp_id"].ToString();
                        DataRow[] drholiday = dtholiday.Select("Holiday_Day=" + dt.Day + " and Holiday_Month=" + dt.Month + " and Holiday_Year=" + dt.Year);
                        if (drholiday.Length == 0)
                        {
                            //int.TryParse(dtAttendance.Rows[i]["Type"].ToString(), out Type_id);
                            DataRow[] drFind = dtAttendance.Select("emp_id=" + empid + " and Overlap=1");
                            {
                                if (drFind.Length > 0)
                                {
                                    string type = drFind[0]["Update_Type"].ToString();
                                    if (type.Equals("1"))
                                    {
                                        status = "Present";
                                        //p++;
                                        //drFind[0]["PresentCount"] = a;
                                    }
                                    else if (type.Equals("2"))
                                    {
                                        status = "Halfday";
                                        h++;
                                        //drFind[0]["HalfDayCount"] = a;
                                    }
                                    else
                                    {
                                        status = "Absent";
                                        //a++;
                                        //drFind[0]["AbsentCount"] = a;
                                    }
                                    drFind[0]["Status"] = status;

                                }
                                else
                                {
                                    Type = dtAttendance.Rows[i]["Type"].ToString();
                                    if (Type.Equals("1"))
                                    {
                                        status = "Present";
                                        //p++;
                                        //dtAttendance.Rows[i]["PresentCount"] = h;

                                    }
                                    else if (Type.Equals("2"))
                                    {
                                        status = "Halfday";
                                        //h++;
                                        //dtAttendance.Rows[i]["HalfDayCount"] = h;
                                    }
                                    else
                                    {
                                        status = "Absent";
                                        //a++;
                                        //dtAttendance.Rows[i]["AbsentCount"] = a;
                                    }
                                    dtAttendance.Rows[i]["Status"] = status;

                                }
                            }
                        }
                        else
                        {
                            dtAttendance.Rows[i]["Status"] = drholiday[0]["Holiday_Reason"].ToString();
                        }

                    }
                    foreach (DataRow dr in dtEmp.Rows)
                    {
                        string empId = dr["empid"].ToString();

                        DataRow[] dr1 = dtAttendance.Select("emp_id=" + empId + "");
                        if (dr1.Length > 0)
                        {
                            dr1[0]["FullName"] = dr["EmployeeName"];
                            dr1[0]["ContactNo"] = dr["EmpPhone"];
                        }

                    }

                    string[] arr = { "Emp_Id", "FullName", "ContactNo", "Status" };
                    DataTable dtfinal = dtAttendance.DefaultView.ToTable(true, arr);
                    if(dtfinal!=null &&  dtfinal.Rows.Count>0)
                    {
                        grd.DataSource = dtfinal;
                        grd.DataBind();
                        ViewState["GVDATA"] = dtfinal;
                        btnExport.Visible = true;
                    }
                    
                    DataTable dtCount = new DataTable();
                    dtCount.Columns.Add("Status");
                    dtCount.Columns.Add("Count", typeof(int));
                    DataRow drc = dtCount.NewRow();
                    DataRow[] drp = dtfinal.Select("Status like 'Present' ");
                    drc["Status"] = "Present";
                    drc["Count"] = drp.Length;
                    dtCount.Rows.Add(drc);

                    DataRow drc1 = dtCount.NewRow();
                    DataRow[] dra = dtfinal.Select("Status like 'Absent' ");
                    drc1["Status"] = "Absent";
                    drc1["Count"] = dra.Length;
                    dtCount.Rows.Add(drc1);

                    DataRow drc2 = dtCount.NewRow();
                    DataRow[] drh = dtfinal.Select("Status like 'Halfday' ");
                    drc2["Status"] = "Halfday";
                    drc2["Count"] = drh.Length;
                    dtCount.Rows.Add(drc2);
                    #region PDF
                    string rpt = "~/Reports/";
                    string filePath = HttpContext.Current.Server.MapPath(rpt);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    Document doc = new Document();
                    doc = new Document(PageSize.A4.Rotate());     //page size
                    //Create PDF Table
                    PdfPTable tableLayout = new PdfPTable(dtfinal.Columns.Count);
                    //Create a PDF file in specific path
                    string imgPath = headerImg; //Header IMG
                    //set image
                    iTextSharp.text.Image jpg = null;
                    if (File.Exists(imgPath))
                    {
                        jpg = iTextSharp.text.Image.GetInstance(imgPath);

                        jpg.Alignment = Element.ALIGN_CENTER;
                    }
                    Paragraph paragraphfotter = new Paragraph("Powered by Expedite Solutions  " + MySQLDB.GetIndianTime().Ticks + "", new Font(Font.HELVETICA, 12, 1, Color.BLACK));
                    paragraphfotter.Alignment = Element.ALIGN_LEFT;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<b><hr  width='100%'></b>");
                    sb.Append("<table width='100%' cellspacing='0'  cellpadding='2' >");
                    sb.Append("<tr><td align='center' style='font-size:16px;font-face:HELVETICA'><b>Employee Attendance Summary</b></td></tr>");
                    sb.Append("<tr ><td align = 'left'><b>ReportDate: </b>" + dt.ToShortDateString() + " </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<br/>");
                    sb1.Append("<br/>");
                    sb1.Append("<br />");
                    sb1.Append("<table border='1' width='25%'>");
                    sb1.Append("<tr> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[0]["Status"].ToString() + "</td> <td align='center' width='10%' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[0]["Count"].ToString() + "</td></tr>");
                    sb1.Append("<tr> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[1]["Status"].ToString() + "</td> <td  align='center' width='10%' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[1]["Count"].ToString() + "</td></tr>");
                    sb1.Append("<tr> <td align='center' width='10%'  style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[2]["Status"].ToString() + "</td> <td width='10%' align='center' style='font-size:10px;font-face:HELVETICA'>" + dtCount.Rows[2]["Count"].ToString() + "</td></tr>");
                    sb1.Append("</table>");
                    PdfWriter.GetInstance(doc, new FileStream(HttpContext.Current.Server.MapPath(rpt) + "\\Employee Attendance Summary.pdf", FileMode.Create));
                    StringReader sr1 = new StringReader(sb1.ToString());
                    StringReader sr = new StringReader(sb.ToString());
                    //Open the PDF document

                    doc.Open();
                    if (jpg != null)
                    {
                        doc.Add(jpg);
                    }
                    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    htmlparser.Parse(sr);
                    //Add Content to PDF
                    doc.Add(Add_Content_To_PDF(tableLayout, dtfinal, report));
                    htmlparser.Parse(sr1);
                    doc.Add(paragraphfotter);
                    //byte[] b = File.ReadAllBytes(Server.MapPath("Late_Comers_Report.pdf"));
                    // Closing the document
                    doc.Close();

                    //Process.Start(HttpContext.Current.Server.MapPath(rpt) + "\\Employee_Monthly_Salary.pdf");
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Reports";
                    test = test + "//Employee Attendance Summary.pdf";
                    Page.ClientScript.RegisterStartupScript(
       this.GetType(), "OpenWindow", "window.open('OpenDocument.html?url=" + test + "','_newtab');", true);
                    #endregion PDF
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteActivityLog("AttendanceReports 4835: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                //Response.Write("AttendanceReports 4835: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        private string convertStringToCurrency(string value)
        {
            try
            {
                string[] decimalValue = { };
                if (value.Contains('.'))
                {
                    decimalValue = value.Split('.');
                    value = decimalValue[0];
                }

                if (value.Length > 3)
                {
                    int sepVal = 3;
                    while (true)
                    {
                        int insertAt = value.Length - sepVal;
                        value = value.Insert(insertAt, ",");

                        if (insertAt < 3)
                        {
                            break;
                        }
                        else
                        {
                            sepVal += 3;
                        }
                    }
                }

                if (decimalValue != null && decimalValue.Length > 1)
                {
                    value += "." + decimalValue[1];
                }
            }
            catch (Exception ex)
            {
            }
            return value;
        }

        private double RoundfloatingToInteger(double finalvalue)
        {
            double EndTotal = 0.0;
            try
            {
                if ((finalvalue - Math.Floor(double.Parse(finalvalue.ToString()))) >= 0.5)
                {
                    EndTotal = Math.Ceiling(double.Parse(finalvalue.ToString()));
                }
                else
                {
                    EndTotal = Math.Floor(double.Parse(finalvalue.ToString()));
                }
            }
            catch (Exception ee)
            {
            }
            return EndTotal;
        }

        #region not use work report 8
        private void EmployeeMonthlyLeaveCountReport(string headerImg)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                //pending Leave calculation Table  after Complete this report

                DataTable dt = new DataTable();
                string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
                DataTable dtEmpList = new DataTable();
                DataTable dtEmpFPData = new DataTable();

                int monthid = 0;
                int.TryParse(ddlmonth.Items[ddlmonth.SelectedIndex].Value.ToString(), out monthid);

                int year = 0;
                int.TryParse(ddlyear.Items[ddlyear.SelectedIndex].Value.ToString(), out year);
                int total_days_in_month = DateTime.DaysInMonth(year, monthid);

                string startdate = "01/" + monthid + "/" + year;
                string enddate = "" + total_days_in_month + "/" + monthid + "/" + year;

                if (emp_ids.Equals("-1"))
                {

                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,PayScale,FP_Id,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master.IsDelete=0 and EmpStatusFlag=0 and time_group_assign_emplyee_wise.IsDelete=0   order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry  WHERE  Emp_Attendance_Entry.Emp_month =" + monthid + " AND Emp_Attendance_Entry.Emp_year=" + year + " and IsDelete=0 ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }
                else
                {

                    dtEmpList = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,PayScale,FP_Id,Group_id from  employee_master inner join time_group_assign_emplyee_wise on  employee_master.empid=time_group_assign_emplyee_wise.emp_id where employee_master where empid in(" + emp_ids + ") and time_group_assign_emplyee_wise.IsDelete=0 and employee_master.IsDelete=0 and employee_master.EmpStatusFlag=0 order by  empid");

                    dtEmpFPData = objmysqldb.GetData("SELECT Emp_Attendance_Entry.* FROM Emp_Attendance_Entry where Emp_Id in(" + emp_ids + ") and  ((Emp_Attendance_Entry.Emp_month)=" + monthid + ") AND ((Emp_Attendance_Entry.Emp_year)=" + year + ")) and IsDelete=0  ORDER BY Emp_FP_Id, Emp_hour, Emp_min, Emp_sec");
                }

                DataTable dtgrplist = dtEmpList.DefaultView.ToTable(true, "Group_id");
                string grp_ids = "0,";
                for (int i = 0; i < dtgrplist.Rows.Count; i++)
                {
                    grp_ids += dtgrplist.Rows[i]["Group_id"].ToString() + ",";
                }
                grp_ids = grp_ids.TrimEnd(',');
                long stticks = new DateTime(year, monthid, 1).Ticks;
                long endticks = new DateTime(year, monthid, total_days_in_month).Ticks;
                DataTable dtEmpTimeConfig = new DataTable();
                if (emp_ids == "-1")
                {
                    dtEmpTimeConfig = objmysqldb.GetData("SELECT employeewise_punchtime_details_datewise.* FROM employeewise_punchtime_details_datewise where  Dateticks >=" + stticks + " and  Dateticks <= " + endticks + "");
                }
                else
                {
                    dtEmpTimeConfig = objmysqldb.GetData("SELECT employeewise_punchtime_details_datewise.* FROM employeewise_punchtime_details_datewise where emp_id in(" + emp_ids.TrimEnd(',') + ") and Dateticks >=" + stticks + " and  Dateticks <= " + endticks + "");
                }

                DataTable dtLeaveList = objmysqldb.GetData("SELECT Leave_Id,Leave_Name,Is_CL_Leave FROM employee_management.leave_master where IsDelete=0;");

                DataTable dtCurrentMonthLeaves = objmysqldb.GetData("SELECT * FROM employee_management.leave_history_monthwise where Month_Id=" + monthid + " and Month_Year=" + year + " and IsDelete=0;");
                DataTable dtAllPreviousMonthLeaves = getpreviosMonth(objmysqldb, monthid, year);

                //DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup where IsDelete=0 and Holiday_Year=" + year + " and Holiday_Month=" + monthid + ";");
                DataTable dtholiday = objmysqldb.GetData("SELECT * FROM employee_management.holiday_setup inner join assign_holidayprofile_employee on assign_holidayprofile_employee.holiday_profile_id=holiday_setup.Holiday_Profile_Id where holiday_setup.IsDelete=0 and holiday_setup.Holiday_Year=" + year + " and holiday_setup.Holiday_Month=" + monthid + " and assign_holidayprofile_employee.IsDelete=0");

                dt.Columns.Add("intFPEmpId");
                dt.Columns.Add("EmployeeName");
                int colpos = 1;
                Hashtable hsColPos = new System.Collections.Hashtable();

                colpos++;
                hsColPos.Add(-4, colpos);
                dt.Columns.Add("Total LCL");
                colpos++;
                hsColPos.Add(-1, colpos);
                dt.Columns.Add("Total LWP");

                string leavenames = "";
                foreach (DataRow dr in dtLeaveList.Rows)
                {
                    colpos++;
                    string leave_name = dr["Leave_Name"].ToString();
                    leavenames += leave_name + ",";
                    hsColPos.Add(int.Parse(dr["Leave_Id"].ToString()), colpos);
                    dt.Columns.Add("Total " + leave_name);
                }


                dt.Columns.Add("LCL");

                dt.Columns.Add("LWP");
                char[] ch1 = { ',' };
                string[] splt = leavenames.Split(ch1, StringSplitOptions.RemoveEmptyEntries);

                foreach (string leave_name in splt)
                {
                    dt.Columns.Add(leave_name);
                }


                //colpos++;
                //hsColPos.Add(-1, colpos);
                dt.Columns.Add("Total");

                dt.Columns.Add("Pr");
                dt.Columns.Add("Ab");
                dt.Columns.Add("Full");
                dt.Columns.Add("Single");
                dt.Columns.Add("L");
                dt.Columns.Add("E");
                dt.Columns.Add("EL");
                dt.Columns.Add("EE");
                int grpid = 0;
                foreach (DataRow dr in dtEmpList.Rows)
                {
                    int EmpId = int.Parse(dr["empid"].ToString());
                    int.TryParse(dr["Group_id"].ToString(), out grpid);
                    string strfpempid = dr["FP_Id"].ToString();
                    string EmployeeName = dr["FullName"].ToString();
                    if (strfpempid.Trim() != "" && strfpempid != "0")
                    {
                        int fpempid = int.Parse(strfpempid);
                        DataRow drnew = dt.NewRow();

                        drnew["intFPEmpId"] = fpempid;
                        drnew["EmployeeName"] = EmployeeName;

                        double Total_CL_Taken_In_this_Month = 0;
                        double Total_ML_Taken_In_this_Month = 0;
                        double Total_LWP_Taken_In_this_Month = 0;
                        double Total_LCL_Taken_In_this_Month = 0;

                        //pending Leave calculation Table  after Complete this report

                        //Total_LWP_Taken_In_this_Month = objEmpLeave.GetEmployeeLeaveCountBetweenTwoDates(1 + "/" + monthid + "/" + year, total_days_in_month + "/" + monthid + "/" + year, int.Parse(Application_Global.AcademicYearId), EmpId, -1);

                        //Total_LCL_Taken_In_this_Month = objEmpLeave.GetEmployeeLeaveCountBetweenTwoDates(1 + "/" + monthid + "/" + year, total_days_in_month + "/" + monthid + "/" + year, int.Parse(Application_Global.AcademicYearId), EmpId, -4);


                        object objPrevLCLSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-4");
                        object objPrevLWPSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-1");

                        drnew["Total LCL"] = objPrevLCLSum != null && objPrevLCLSum.ToString().Trim() != "" ? double.Parse(objPrevLCLSum.ToString()) : 0.0;//[(int)hsColPos[-4]]
                        drnew["Total LWP"] = objPrevLWPSum != null && objPrevLWPSum.ToString().Trim() != "" ? double.Parse(objPrevLWPSum.ToString()) : 0.0;

                        object objCurrLCLSum = Total_LCL_Taken_In_this_Month;//dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-4");
                        object objCurrLWPSum = Total_LWP_Taken_In_this_Month;//dtCurrentMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=-1");

                        drnew["LCL"] = objCurrLCLSum != null && objCurrLCLSum.ToString().Trim() != "" ? double.Parse(objCurrLCLSum.ToString()) : 0.0;
                        drnew["LWP"] = objCurrLWPSum != null && objCurrLWPSum.ToString().Trim() != "" ? double.Parse(objCurrLWPSum.ToString()) : 0.0;

                        double total_leave = 0.0;
                        total_leave += objCurrLWPSum != null && objCurrLWPSum.ToString().Trim() != "" ? double.Parse(objCurrLWPSum.ToString()) : 0.0;


                        foreach (DataRow dr1 in dtLeaveList.Rows)
                        {
                            string leave_name = dr1["Leave_Name"].ToString();
                            int leavid = int.Parse(dr1["Leave_Id"].ToString());

                            int index = (int)hsColPos[int.Parse(dr1["Leave_Id"].ToString())];
                            object objPrevLeaveSum = dtAllPreviousMonthLeaves.Compute("sum(Total_Leave)", "Emp_Id=" + EmpId + " and Leave_Id=" + int.Parse(dr1["Leave_Id"].ToString()) + "");
                            drnew["Total " + leave_name] = objPrevLeaveSum != null && objPrevLeaveSum.ToString().Trim() != "" ? double.Parse(objPrevLeaveSum.ToString()) : 0.0;


                            //pending Leave calculation Table  after Complete this report

                            //Total_CL_Taken_In_this_Month = objEmpLeave.GetEmployeeLeaveCountBetweenTwoDates(1 + "/" + monthid + "/" + year, total_days_in_month + "/" + monthid + "/" + year, int.Parse(Application_Global.AcademicYearId), EmpId, leavid);
                            object objCurrLeaveSum = Total_CL_Taken_In_this_Month;
                            drnew[leave_name] = objCurrLeaveSum != null && objCurrLeaveSum.ToString().Trim() != "" ? double.Parse(objCurrLeaveSum.ToString()) : 0.0;

                            total_leave += objCurrLeaveSum != null && objCurrLeaveSum.ToString().Trim() != "" ? double.Parse(objCurrLeaveSum.ToString()) : 0.0;
                        }
                        drnew["Total"] = total_leave;// "<center>" + total_leave +  ;

                        DataRow[] drfindpunchtimeglobal = dtEmpFPData.Select("Emp_Id=" + EmpId + " and Emp_FP_Id=" + fpempid);
                        if (drfindpunchtimeglobal.Length > 0)
                        {
                            string[] unqarr = { "Emp_day", "Emp_month", "Emp_year" };
                            DataTable dtuniqdata = drfindpunchtimeglobal.CopyToDataTable().DefaultView.ToTable(true, unqarr);
                            string holidays = "";
                            for (int holiday = 0; holiday < dtholiday.Rows.Count; holiday++)
                            {
                                holidays += dtholiday.Rows[holiday]["Holiday_Day"].ToString() + ",";
                            }
                            holidays = holidays.TrimEnd(',');
                            DataRow[] findholiday = null;
                            //DataRow[] findholiday = dtuniqdata.Select("Emp_day NOT IN(" + holidays + ")");
                            //dtuniqdata = findholiday.CopyToDataTable();
                            if (holidays != "")
                            {
                                findholiday = dtuniqdata.Select("Emp_day NOT IN(" + holidays + ")");
                                dtuniqdata = findholiday.CopyToDataTable();
                            }
                            else
                            {
                                dtuniqdata = dtuniqdata.Clone();
                            }

                            drnew["Pr"] = dtuniqdata.Rows.Count.ToString();// "<center>" + dtuniqdata.Rows.Count.ToString() +  ;
                            int totalworkingdays = total_days_in_month - dtholiday.Rows.Count;
                            int totalabsentdays = totalworkingdays - dtuniqdata.Rows.Count;

                            drnew["Ab"] = totalabsentdays.ToString();// "<center>" + totalabsentdays.ToString() +  ;
                            int st_day = 1;//int.Parse(dtpfrom.Text.Substring(0, 2));
                            int st_month = monthid;//int.Parse(dtpfrom.Text.Substring(3, 2));
                            int st_year = year;//int.Parse(dtpfrom.Text.Substring(6));

                            DateTime dtDate = new DateTime(st_year, st_month, st_day);

                            int totalfullpresentdays = 0;
                            int totallatedays = 0;
                            int totalearlydays = 0;
                            int totalElatedays = 0;
                            int totalEearlydays = 0;
                            int totalsinglepunchdays = 0;
                            for (int i = 0; i < total_days_in_month; i++)
                            {
                                DataRow[] drholiday = dtholiday.Select("Holiday_Day=" + dtDate.Day + " and Holiday_Month=" + dtDate.Month + " and Holiday_Year=" + dtDate.Year);
                                if (drholiday.Length == 0)
                                {
                                    bool isSinglePunch = false;
                                    bool isLateCome = false;
                                    bool isEarlyLeave = false;
                                    bool isExtremeLateCome = false;
                                    bool isExtremeEarlyLeave = false;

                                    int punch_emphour_global = 0;
                                    int punch_empmin_global = 0;
                                    int punch_empsec_global = 0;

                                    DataRow[] drfindpunchtime = drfindpunchtimeglobal.CopyToDataTable().Select("Emp_day=" + dtDate.Day + " and Emp_month=" + dtDate.Month + " and Emp_year=" + dtDate.Year, "Emp_hour,Emp_min,Emp_sec asc");
                                    if (drfindpunchtime.Length > 0)
                                    {
                                        punch_emphour_global = int.Parse(drfindpunchtime[0]["Emp_hour"].ToString());
                                        punch_empmin_global = int.Parse(drfindpunchtime[0]["Emp_min"].ToString());
                                        punch_empsec_global = int.Parse(drfindpunchtime[0]["Emp_sec"].ToString());
                                        if (drfindpunchtime.Length == 1)
                                        {
                                            isSinglePunch = true;
                                        }
                                    }

                                    int punch_emphour_global_out = 0;
                                    int punch_empmin_global_out = 0;
                                    int punch_empsec_global_out = 0;

                                    if (drfindpunchtime.Length > 1)
                                    {
                                        punch_emphour_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_hour"].ToString());
                                        punch_empmin_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_min"].ToString());
                                        punch_empsec_global_out = int.Parse(drfindpunchtime[drfindpunchtime.Length - 1]["Emp_sec"].ToString());
                                    }

                                    //DataTable dtEmpTimeConfig = new DataTable();
                                    string strday = dtDate.DayOfWeek.ToString();
                                    int intday = (int)dtDate.DayOfWeek;
                                    if (intday == 0)
                                    { intday = 7; }
                                    //dtEmpTimeConfig = objEmp.GetEmployeeTimeConfiguration(intday);
                                    long toticks = new DateTime(year, monthid, dtDate.Day).Ticks;
                                    DataRow[] drfindtime = dtEmpTimeConfig.Select("time_grp_id=" + grpid + " and Dateticks=" + toticks + " and Day_id=" + intday + " ");
                                    int emphour_global = 0;
                                    int empmin_global = 0;

                                    int emphour_global_out = 0;
                                    int empmin_global_out = 0;

                                    int EL_emphour_global = 0;
                                    int EL_empmin_global = 0;

                                    int EE_emphour_global_out = 0;
                                    int EE_empmin_global_out = 0;

                                    if (drfindtime.Length > 0)
                                    {
                                        emphour_global = int.Parse(drfindtime[0]["In_hour"] != null && drfindtime[0]["In_hour"].ToString().Trim() != "" ? drfindtime[0]["In_hour"].ToString() : "0");
                                        empmin_global = int.Parse(drfindtime[0]["In_min"] != null && drfindtime[0]["In_min"].ToString().Trim() != "" ? drfindtime[0]["In_min"].ToString() : "0");

                                        emphour_global_out = int.Parse(drfindtime[0]["out_hour"] != null && drfindtime[0]["out_hour"].ToString().Trim() != "" ? drfindtime[0]["out_hour"].ToString() : "0");
                                        empmin_global_out = int.Parse(drfindtime[0]["out_min"] != null && drfindtime[0]["out_min"].ToString().Trim() != "" ? drfindtime[0]["out_min"].ToString() : "0");

                                        EE_emphour_global_out = int.Parse(drfindtime[0]["Ext_Early_hour"] != null && drfindtime[0]["Ext_Early_hour"].ToString().Trim() != "" ? drfindtime[0]["Ext_Early_hour"].ToString() : "0");
                                        EE_empmin_global_out = int.Parse(drfindtime[0]["Ext_Early_min"] != null && drfindtime[0]["Ext_Early_min"].ToString().Trim() != "" ? drfindtime[0]["Ext_Early_min"].ToString() : "0");

                                        EL_emphour_global = int.Parse(drfindtime[0]["Ext_Late_hour"] != null && drfindtime[0]["Ext_Late_hour"].ToString().Trim() != "" ? drfindtime[0]["Ext_Late_hour"].ToString() : "0");
                                        EL_empmin_global = int.Parse(drfindtime[0]["Ext_Late_min"] != null && drfindtime[0]["Ext_Late_min"].ToString().Trim() != "" ? drfindtime[0]["Ext_Late_min"].ToString() : "0");
                                    }

                                    if ((emphour_global == 0 && empmin_global == 0) || (punch_emphour_global == 0 && punch_empmin_global == 0) || (EL_emphour_global == 0 && EL_empmin_global == 0))
                                    {
                                    }
                                    else
                                    {
                                        if ((punch_emphour_global > emphour_global) || (punch_emphour_global == emphour_global && punch_empmin_global >= empmin_global))
                                        {
                                            if ((punch_emphour_global > EL_emphour_global) || (punch_emphour_global == EL_emphour_global && punch_empmin_global >= EL_empmin_global))
                                            {
                                                isExtremeLateCome = true;
                                            }
                                            else
                                            {
                                                isLateCome = true;
                                            }
                                        }
                                        else
                                        {
                                            isExtremeLateCome = false;
                                            isLateCome = false;
                                        }
                                    }
                                    if ((emphour_global_out == 0 && empmin_global_out == 0) || (punch_emphour_global_out == 0 && punch_empmin_global_out == 0))
                                    {
                                    }
                                    else
                                    {
                                        if ((punch_emphour_global_out < emphour_global_out) || (punch_emphour_global_out == emphour_global_out && punch_empmin_global_out < empmin_global_out))
                                        {
                                            if ((punch_emphour_global_out < EE_emphour_global_out) || (punch_emphour_global_out == EE_emphour_global_out && punch_empmin_global_out < EE_empmin_global_out))
                                            {
                                                isExtremeEarlyLeave = true;
                                            }
                                            else
                                            {
                                                isEarlyLeave = true;
                                            }
                                        }
                                        else
                                        {
                                            isExtremeEarlyLeave = false;
                                            isEarlyLeave = false;
                                        }
                                    }

                                    if (isEarlyLeave || isSinglePunch || isLateCome || isExtremeEarlyLeave || isExtremeLateCome)
                                    {
                                        if (isEarlyLeave)
                                        {
                                            totalearlydays++;
                                        }

                                        if (isExtremeEarlyLeave)
                                        {
                                            totalEearlydays++;
                                        }

                                        if (isSinglePunch)
                                        {
                                            totalsinglepunchdays++;
                                        }

                                        if (isLateCome)
                                        {
                                            totallatedays++;
                                        }

                                        if (isExtremeLateCome)
                                        {
                                            totalElatedays++;
                                        }
                                    }
                                    else
                                    {
                                        if (punch_emphour_global == 0 && punch_empmin_global == 0)
                                        {

                                        }
                                        else
                                        {
                                            totalfullpresentdays++;
                                        }
                                    }
                                }
                                dtDate = dtDate.AddDays(1);
                            }
                            drnew["Full"] = totalfullpresentdays.ToString();// "<center>" + totalfullpresentdays.ToString() +  ;
                            drnew["L"] = totallatedays.ToString();//   + totallatedays.ToString() +  ;
                            drnew["E"] = totalearlydays.ToString();//   + totalearlydays.ToString() +  ;
                            drnew["EL"] = totalElatedays.ToString();//   + totalElatedays.ToString() +  ;
                            drnew["EE"] = totalEearlydays.ToString();//   + totalEearlydays.ToString() +  ;
                            drnew["Single"] = totalsinglepunchdays.ToString();//   + totalsinglepunchdays.ToString() +  ;
                        }
                        else
                        {
                        }
                        dt.Rows.Add(drnew);
                    }

                }
                ViewState["GVDATA"] = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    grd.DataSource = dt;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    grd.DataSource = null;
                    grd.DataBind();
                    btnExport.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AttendanceReports 174: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        #endregion
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                //objmysqldb.ConnectToDatabase();

                DataTable dtreportData = (DataTable)ViewState["GVDATA"];
                objmysqldb.disposeConnectionObj();
                if (dtreportData != null && dtreportData.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtreportData, txttitle.Text.ToString());
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 4568: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        public static string GetDifferenceInTime(int emphour_global, int empmin_global, int punch_emphour_global, int punch_empmin_global, int punch_empsec_global, bool isEarlyLeave)
        {
            TimeSpan tm1 = TimeSpan.Parse(punch_emphour_global.ToString() + ":" + punch_empmin_global.ToString() + ":" + punch_empsec_global.ToString());
            TimeSpan tm2 = TimeSpan.Parse(emphour_global.ToString() + ":" + empmin_global.ToString());
            TimeSpan difference;

            if (isEarlyLeave)
            {
                difference = tm2 - tm1;
            }
            else
            {
                difference = tm1 - tm2;
            }

            int hours = difference.Hours;
            int minutes = difference.Minutes;
            string concat = "";

            if (hours == 0)
            {
                concat = "00:";
            }
            else
            {
                if (hours < 10)
                {
                    concat = "0" + hours.ToString() + ":";
                }
                else
                    concat = hours.ToString() + ":";
            }

            if (minutes == 0)
            {
                concat += "00:";
            }
            else
            {
                if (minutes < 10)
                {
                    concat += "0" + minutes.ToString() + ":";
                }
                else
                    concat += minutes.ToString() + ":";
            }

            if (punch_empsec_global == 0)
            {
                concat += "00";
            }
            else
            {
                if (isEarlyLeave)
                {
                    int secrem = 60 - punch_empsec_global;
                    if (secrem < 10)
                    {
                        concat += "0" + secrem.ToString();
                    }
                    else
                        concat += secrem.ToString();
                }
                else
                {
                    if (punch_empsec_global < 10)
                    {
                        concat += "0" + punch_empsec_global.ToString();
                    }
                    else
                        concat += punch_empsec_global.ToString();
                }
            }
            return concat;
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string emp_ids = ddlgroup.Items[ddlgroup.SelectedIndex].Value.ToString();
            if (emp_ids.Equals("-1"))
            {
                bindemp();
            }
            else
            {
                DataTable dtemp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and employee_master.FP_Id>0 where empid in(" + emp_ids + ")  order by  empid");
                ddlemp.DataSource = dtemp;
                ddlemp.DataTextField = "FullName";
                ddlemp.DataValueField = "empid";
                ddlemp.DataBind();

                
            }
            


        }


    }

}