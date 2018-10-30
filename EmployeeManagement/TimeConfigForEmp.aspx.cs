using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class TimeConfigForEmp : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        int user_id = 0;
        MySQLDB objmysqldb = new MySQLDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                {
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    Label header = Master.FindControl("lbl_pageHeader") as Label;
                    header.Text = "DateWise Time Config For Emp";
                   // string date = Request.QueryString["date"].ToString();
                    string date = "25-08-2018";
                    //string emp_id = Request.QueryString["Emp_id"].ToString();
                    string emp_id = "1";
                    if (!Page.IsPostBack)
                    {
                        lblDate1.Text = date;
                        BindData(emp_id);
                        
                        bindMonth(date);
                        //prevPage = Request.Url.ToString();
                    }
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

        }
        public void bindMonth(string date)
        {
            try
            {
                int Year = 0;
                DataTable dtMonth = new DataTable();
                dtMonth.Columns.Add("Date");
                int.TryParse(DateTime.Today.Year.ToString(), out Year);
                //DateTime startOfMonth = new DateTime(Year, month, 1);
                DateTime dtnow = Convert.ToDateTime(date);//Get Selected Month From Previous page
                int month = dtnow.Month;
                DateTime endOfMonth = new DateTime(Year, month, DateTime.DaysInMonth(Year, month));
                TimeSpan ts = endOfMonth.Subtract(dtnow);

                for (int d = 0; d < ts.Days; d++)
                {
                    string dtDate = dtnow.AddDays(d + 1).ToString("dd.MM.yy");

                    DataRow dr = dtMonth.NewRow();
                    dr["Date"] = dtDate;
                    dtMonth.Rows.Add(dr);
                }
                if (dtMonth != null && dtMonth.Rows.Count > 0)
                {
                    GvMonth.DataSource = dtMonth;
                    GvMonth.DataBind();
                }

            }
            catch (Exception ex)
            {


            }

        }
        public void BindData(string emp_id)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable EmpDetail = objmysqldb.GetData("select empid as emp_id,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and employee_master.FP_Id>0 order by  empid");

                string dt = lblDate1.Text.ToString();
                DateTime oDate = Convert.ToDateTime(dt.ToString());
                long Tikcs = oDate.Ticks;

                DataTable dtEmpAttendance = objmysqldb.GetData("SELECT distinct Dateticks,emp_id,Day_id,concat(In_hour,':',In_min)as InMin,concat(out_hour,':',out_min)As outHour,concat(Ext_Early_hour,':',Ext_Early_min)as ExtEarlyHour,concat(Ext_Late_hour,':',Ext_Late_min)as ExtLateHour FROM employee_management.employeewise_punchtime_details_datewise where Dateticks <" + Tikcs + " and emp_id=" + emp_id + "");
                if (dtEmpAttendance != null && dtEmpAttendance.Rows.Count > 0)
                {
                    string InTime = "";
                    string outTime = "";
                    string ExtEarly = "";
                    string ExtLate = "";
                    foreach (DataRow drEmp in dtEmpAttendance.Rows)
                    {
                        InTime = drEmp["InMin"].ToString();
                        outTime = drEmp["outHour"].ToString();
                        ExtEarly = drEmp["ExtEarlyHour"].ToString();
                        ExtLate = drEmp["ExtLateHour"].ToString();
                    }
                    txtinTime.Text = InTime;
                    txtoutTime.Text = outTime;
                    txtExtremeEarly.Text = ExtEarly;
                    txtExtremeLate.Text = ExtLate;
                }
                if (EmpDetail != null && EmpDetail.Rows.Count > 0)
                {
                    GrdEmpList.DataSource = EmpDetail;
                    GrdEmpList.DataBind();
                }


            }
            catch (Exception ex)
            {


            }
            finally
            {
                //objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();

            }


        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            objmysqldb.OpenSQlConnection();
            try
            {
                string Emp_Ids = "";
                DataTable dtAllDate = objmysqldb.GetData("SELECT Dateticks,emp_id,Day_id From  employee_management.employeewise_punchtime_details_datewise;");
                DateTime currentTime = Logger.getIndiantimeDT();
                int In_hour = 0;
                int In_min = 0;
                int out_hour = 0;
                int out_min = 0;
                int Ext_Early_hour = 0;
                int Ext_Early_min = 0;
                int Ext_Late_hour = 0;
                int Ext_Late_min = 0;
                string[] intime = txtinTime.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string[] outtime = txtoutTime.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string[] Earlytime = txtExtremeEarly.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string[] latetime = txtExtremeLate.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(intime[0], out In_hour);
                int.TryParse(intime[1], out In_min);
                int.TryParse(outtime[0], out out_hour);
                int.TryParse(outtime[1], out out_min);
                int.TryParse(Earlytime[0], out Ext_Early_hour);
                int.TryParse(Earlytime[1], out Ext_Early_min);
                int.TryParse(latetime[0], out Ext_Late_hour);
                int.TryParse(latetime[1], out Ext_Late_min);
                if (In_hour > 23 || In_min > 59 || out_hour > 23 || out_min > 59 || Ext_Early_hour > 23 || Ext_Early_min > 59 || Ext_Late_hour > 23 || Ext_Late_min > 59)
                {
                    ltrErr.Text = "Please Enter valid time ";
                    return;
                }
                //Control ctl = this.FindControlRecursive("btnAccept");
                string str = string.Empty;
                foreach (GridViewRow gvrow in GrdEmpList.Rows)
                {

                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chk.Checked && chk != null)
                    {
                        Label emp = (Label)gvrow.FindControl("lblemp_id");
                        Emp_Ids += emp.Text.ToString() + ",";
                    }

                }
                string dt = "";
                foreach (GridViewRow row in GvMonth.Rows)
                {
                    CheckBox chkdt = (CheckBox)row.FindControl("Select");
                    if (chkdt.Checked && chkdt != null)
                    {
                        Label date1 = (Label)row.FindControl("lblDate");
                        dt += date1.Text.ToString() + ",";
                    }
                }
                dt = dt.TrimEnd(',');
                Emp_Ids = Emp_Ids.TrimEnd(',');
                string date = lblDate1.Text.ToString();
                //DateTime oDate = Convert.ToDateTime(date.ToString());
                //long Tikcs = oDate.Ticks;
                //DateTime dtNew = new DateTime(Tikcs);
                //int day1 = (int)dtNew.DayOfWeek;
                string[] arrempid = Emp_Ids.Split(',');
                int dayid = 0;

                long Tikcs = 0;
                string[] dates = dt.Split(',');
                dates = dates.Where(s => !String.IsNullOrEmpty(s)).ToArray();
                //string[] dates = !String.IsNullOrEmpty(dt)).ToArray();
                if (dates != null && dates.Length == 0)
                {
                    DateTime oDate = Convert.ToDateTime(date.ToString());
                    Tikcs = oDate.Ticks;
                    //DateTime dtNew = new DateTime(Tikcs);
                    dayid = (int)oDate.DayOfWeek;
                    if (dayid == 0)
                    {
                        dayid = 7;
                    }
                }

                if (arrempid != null && arrempid.Length > 0)
                {
                    int Yes = 0;

                    for (int j = 0; j < arrempid.Length; j++)
                    {
                        string EmpID = arrempid[j].ToString();
                        if (dates != null && dates.Length == 0)
                        {
                            //DataRow[] dr = dtAllDate.Select("emp_id=" + EmpID + " And Day_id=" + dayid + " and Dateticks in('" + Tikcs + "')");
                            DataRow[] dr = dtAllDate.Select("emp_id=" + EmpID + " And Day_id=" + dayid + " and Dateticks=" + Tikcs + "");
                            if (dayid == 0)
                            {
                                dayid = 7;
                            }
                            if (dr.Length > 0)
                            {
                                Yes = objmysqldb.InsertUpdateDeleteData("update employeewise_punchtime_details_datewise set In_hour=" + In_hour + ",In_min=" + In_min + ",out_hour=" + out_hour + ",out_min=" + out_min + " ,Ext_Early_hour=" + Ext_Early_hour + ",Ext_Early_min=" + Ext_Early_min + ",Ext_Late_hour=" + Ext_Late_hour + ",Ext_Late_min=" + Ext_Late_min + ",modify_datetime=" + MySQLDB.GetIndianTime().Ticks.ToString() + ",IsUpdate=1, UserID= " + user_id + "  where emp_id=" + EmpID + " and Day_id=" + dayid);
                            }
                            else
                            {

                                Yes = objmysqldb.InsertUpdateDeleteData("insert into employeewise_punchtime_details_datewise(Dateticks,emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Late_hour,Ext_Late_min,Ext_Early_min,modify_datetime,IsDelete,DOC,IsUpdate,UserID)Values(" + Tikcs + "," + EmpID + "," + dayid + "," + In_hour + "," + In_min + "," + out_hour + "," + out_min + "," + Ext_Early_hour + "," + Ext_Late_hour + "," + Ext_Late_min + "," + Ext_Early_min + "," + MySQLDB.GetIndianTime().Ticks.ToString() + ",0," + MySQLDB.GetIndianTime().Ticks.ToString() + ",1," + user_id + ")");

                            }
                        }
                        else
                        {
                            for (int dat = 0; dat < dates.Length; dat++)
                            {
                                string dt1 = dates[dat].ToString();
                                DateTime oDate = Convert.ToDateTime(dt1.ToString());
                                Tikcs = oDate.Ticks;
                                dayid = (int)oDate.DayOfWeek;
                                if (dayid == 0)
                                {
                                    dayid = 7;
                                }
                                //DataRow[] dr = dtAllDate.Select("emp_id=" + EmpID + " And Day_id=" + dayid + " and Dateticks in('" + Tikcs + "')");
                                DataRow[] dr = dtAllDate.Select("emp_id=" + EmpID + " And Day_id=" + dayid + " and Dateticks=" + Tikcs + "");
                                if (dr.Length > 0)
                                {
                                    Yes = objmysqldb.InsertUpdateDeleteData("update employeewise_punchtime_details_datewise set In_hour=" + In_hour + ",In_min=" + In_min + ",out_hour=" + out_hour + ",out_min=" + out_min + " ,Ext_Early_hour=" + Ext_Early_hour + ",Ext_Early_min=" + Ext_Early_min + ",Ext_Late_hour=" + Ext_Late_hour + ",Ext_Late_min=" + Ext_Late_min + ",modify_datetime=" + MySQLDB.GetIndianTime().Ticks.ToString() + ",IsUpdate=1, UserID= " + user_id + "  where emp_id=" + EmpID + " and Day_id=" + dayid);
                                }
                                else
                                {
                                    Yes = objmysqldb.InsertUpdateDeleteData("insert into employeewise_punchtime_details_datewise(Dateticks,emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Late_hour,Ext_Late_min,Ext_Early_min,modify_datetime,IsDelete,DOC,IsUpdate,UserID)Values(" + Tikcs + "," + EmpID + "," + dayid + "," + In_hour + "," + In_min + "," + out_hour + "," + out_min + "," + Ext_Early_hour + "," + Ext_Late_hour + "," + Ext_Late_min + "," + Ext_Early_min + "," + MySQLDB.GetIndianTime().Ticks.ToString() + ",0," + MySQLDB.GetIndianTime().Ticks.ToString() + ",1," + user_id + ")");

                                }
                            }
                        }
                    }
                    if (Yes == -1)
                    {

                        ltrErr1.Text = "Error While Saving..";
                    }
                    else
                    {
                        ltrErr1.Text = "Time Configuration Saved...";

                    }
                }
                else
                {
                    ltrErr1.Text = "Select Atleast One Employee...";

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("TimeConfigForEmp BenSaveClick(): exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }



        protected void btnBack_Click1(object sender, EventArgs e)
        {
            // Response.Redirect(prevPage);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }


    }
}