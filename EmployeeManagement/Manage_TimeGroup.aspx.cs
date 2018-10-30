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
    public partial class Manage_TimeGroup : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int timegrp = 0;
                ltrErr.Text = "";
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;

                        int.TryParse(Request.QueryString["TimegrpId"].ToString(), out timegrp);
                        if (timegrp == 0)
                        {
                            header.Text = "Add Time Group Details";
                        }
                        else
                        {
                            header.Text = "Update Time Group Details";
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

                if (!IsPostBack)
                {
                    txtgroup.Focus();
                    ViewState["TimegrpId"] = timegrp.ToString();
                    time_grp.Value = (string)ViewState["TimegrpId"];

                    DateTime dtt = MySQLDB.GetIndianTime();
                    string todaydate = (dtt.Day.ToString().Length == 1 ? "0" + dtt.Day.ToString() : dtt.Day.ToString()) +
                        "/" +
                        (dtt.Month.ToString().Length == 1 ? "0" + dtt.Month.ToString() : dtt.Month.ToString())
                        + "/" +
                        dtt.Year;

                    date.Text = todaydate;
                    getBinddata(timegrp);

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_TimeGroup 75: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                int grp_id = 0;
                int.TryParse(time_grp.Value.ToString(), out grp_id);
                if (txtgroup.Text.Equals("") || SMSintime.Value.ToString().Equals("") || smsout.Value.ToString().Equals(""))
                {
                    ltrErr.Text = "Please enter all details";
                    return;
                }
                long dateticks = 0;
                try
                {
                    string[] arrchangeDate = date.Text.Split('/');

                    dateticks = new DateTime(int.Parse(arrchangeDate[2]), int.Parse(arrchangeDate[1]), int.Parse(arrchangeDate[0])).Ticks;
                }
                catch (Exception aa)
                {

                }
                if (dateticks == 0)
                {
                    ltrErr.Text = "Please Enter valid  Applicable change date";
                    return;
                }
                DataTable dtPreviousTimeConfig = objmysqldb.GetData("select GroupWise_Time_Id,Group_Time_Id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min from groupwise_time_config where Group_Time_Id=" + grp_id + " ");

                DataTable dtPreviousgrpdata = objmysqldb.GetData("select Group_id,Group_Name,Absent_SMS_After,OutPuch_SMS_After,Changes_Applicable_Date from group_master");

                if (dtPreviousTimeConfig != null && dtPreviousgrpdata != null)
                {

                    DataRow[] drgrp = dtPreviousgrpdata.Select("Group_Name ='" + txtgroup.Text.ToString() + "' and  Group_id <> " + grp_id + " ");
                    if (drgrp.Length > 0)
                    {
                        ltrErr.Text = "Group name already exists";
                        return;
                    }
                    DataTable dtdata = new DataTable();
                    dtdata.Columns.Add("Day_id", typeof(Int32));
                    dtdata.Columns.Add("In_hour", typeof(Int32));
                    dtdata.Columns.Add("In_min", typeof(Int32));
                    dtdata.Columns.Add("out_hour", typeof(Int32));
                    dtdata.Columns.Add("out_min", typeof(Int32));
                    dtdata.Columns.Add("Ext_Early_hour", typeof(Int32));
                    dtdata.Columns.Add("Ext_Early_min", typeof(Int32));
                    dtdata.Columns.Add("Ext_Late_hour", typeof(Int32));
                    dtdata.Columns.Add("Ext_Late_min", typeof(Int32));
                    #region time check
                    foreach (GridViewRow row in grd.Rows)
                    {
                        Label dayid = (Label)row.FindControl("lbldayid");
                        TextBox txtIn = (TextBox)row.FindControl("txtIntime");
                        TextBox txtOut = (TextBox)row.FindControl("txtoutTime");
                        TextBox txtEarly = (TextBox)row.FindControl("txtearly");
                        TextBox txtLate = (TextBox)row.FindControl("txtlate");
                        if (txtIn.Text.ToString().Equals("") || txtOut.Text.ToString().Equals("") || txtEarly.Text.ToString().Equals("") || txtLate.Text.ToString().Equals(""))
                        {
                            ltrErr.Text = "Please enter all time slot";
                            return;
                        }
                        string[] intime = txtIn.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] outtime = txtOut.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] Earlytime = txtEarly.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] latetime = txtLate.Text.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        if (intime.Length != 2 || outtime.Length != 2 || Earlytime.Length != 2 || latetime.Length != 2)
                        {
                            ltrErr.Text = "Please enter valid time ";
                            return;
                        }
                        int day = 0;
                        int.TryParse(dayid.Text.ToString(), out day);
                        int In_hour = 0;
                        int In_min = 0;
                        int out_hour = 0;
                        int out_min = 0;
                        int Ext_Early_hour = 0;
                        int Ext_Early_min = 0;
                        int Ext_Late_hour = 0;
                        int Ext_Late_min = 0;
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
                            ltrErr.Text = "Please enter valid time ";
                            return;
                        }
                        dtdata.Rows.Add(day, In_hour, In_min, out_hour, out_min, Ext_Early_hour, Ext_Early_min, Ext_Late_hour, Ext_Late_min);
                    }
                    #endregion

                    objmysqldb.OpenSQlConnection();
                    objmysqldb.BeginSQLTransaction();


                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("Group_Name", txtgroup.Text.ToString());
                    int absent = 0;
                    int outpunch = 0;
                    int.TryParse(SMSintime.Value.ToString(), out absent);
                    int.TryParse(smsout.Value.ToString(), out outpunch);
                    DateTime currenttime = Logger.getIndiantimeDT();
                    if (grp_id > 0)
                    {
                        objmysqldb.InsertUpdateDeleteData("update group_master set Group_Name=?Group_Name,Absent_SMS_After=" + absent + ",OutPuch_SMS_After=" + outpunch + ",Changes_Applicable_Date=" + dateticks + ",modify_datetime=" + currenttime.Ticks + ", IsUpdate=1, UserID=" + user_id + " where  Group_id = " + grp_id + "");
                    }
                    else
                    {
                        long mode = currenttime.Ticks;
                        int res = objmysqldb.InsertUpdateDeleteData("insert into group_master (Group_Name,Absent_SMS_After,OutPuch_SMS_After,Changes_Applicable_Date,modify_datetime,DOC,IsUpdate,UserID,IsDelete) values (?Group_Name," + absent + "," + outpunch + "," + dateticks + "," + mode + "," + mode + ",1," + user_id + ",0) ");
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";
                            objmysqldb.RollBackSQLTransaction();
                            Logger.WriteCriticalLog("Manage_TimeGroup 205 Update error.");
                            return;
                        }
                        else
                        {
                            long lastgrpid = objmysqldb.LastNo("group_master", "Group_id", mode);
                            ViewState["TimegrpId"] = lastgrpid.ToString();
                            time_grp.Value = (string)ViewState["TimegrpId"];
                            int.TryParse(time_grp.Value.ToString(), out grp_id);
                            //Emp_idHidden.Value = lastEmpid.ToString();
                            //Response.Redirect("~/ManageEmployee.aspx?Emp=" + Emp_idHidden.Value.ToString() + "", false);
                        }
                    }

                    foreach (DataRow dr in dtdata.Rows)
                    {
                        int retval = objmysqldb.InsertUpdateDeleteData("update groupwise_time_config set In_hour=" + int.Parse(dr["In_hour"].ToString()) + ",In_min=" + int.Parse(dr["In_min"].ToString()) + ",out_hour=" + int.Parse(dr["out_hour"].ToString()) + ",out_min=" + int.Parse(dr["out_min"].ToString()) + ",Ext_Early_hour=" + int.Parse(dr["Ext_Early_hour"].ToString()) + ",Ext_Early_min=" + int.Parse(dr["Ext_Early_min"].ToString()) + ",Ext_Late_hour=" + int.Parse(dr["Ext_Late_hour"].ToString()) + ",Ext_Late_min=" + int.Parse(dr["Ext_Late_min"].ToString()) + ",modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where Group_Time_Id=" + grp_id + " and Day_id=" + int.Parse(dr["Day_id"].ToString()) + " ");

                        if (retval == 0)
                        {
                            retval = objmysqldb.InsertUpdateDeleteData("insert into groupwise_time_config (Group_Time_Id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min,modify_datetime,DOC,IsUpdate,UserID) values (" + grp_id + "," + int.Parse(dr["Day_id"].ToString()) + "," + int.Parse(dr["In_hour"].ToString()) + "," + int.Parse(dr["In_min"].ToString()) + "," + int.Parse(dr["out_hour"].ToString()) + "," + int.Parse(dr["out_min"].ToString()) + "," + int.Parse(dr["Ext_Early_hour"].ToString()) + "," + int.Parse(dr["Ext_Early_min"].ToString()) + "," + int.Parse(dr["Ext_Late_hour"].ToString()) + "," + int.Parse(dr["Ext_Late_min"].ToString()) + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1," + user_id + ")");
                        }
                    }
                    // Applicable Date change
                    string[] arrdob = date.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime dtcur = MySQLDB.GetIndianTime();
                    if (arrdob.Length == 3)
                    {
                        DateTime dtchangeDate = new DateTime(int.Parse(arrdob[2]), int.Parse(arrdob[1]), int.Parse(arrdob[0]));

                        DateTime dtcurrent = new DateTime(dtcur.Year, dtcur.Month, dtcur.Day);
                        TimeSpan dtDiff = dtchangeDate - dtcurrent;
                        DateTime dtstartdate= dtcurrent;
                        if(dtDiff.Days < 0)
                        {
                            dtDiff = dtcurrent - dtchangeDate;
                            dtstartdate = dtchangeDate;
                        }

                        #region  datewise entry
                        DateTime currenttimes = Logger.getIndiantimeDT();
                        for (int d = 0; d <= dtDiff.Days; d++)
                        {
                            DateTime dtt = dtstartdate.AddDays(d);
                            //foreach (DataRow dr in dtdata.Rows)
                            int days = (int)dtt.DayOfWeek;
                            if (days == 0)
                            {
                                days = 7;
                            }
                            DataRow[] drdaywise = dtdata.Select("Day_id =" + days + " ");
                            foreach (DataRow dr in drdaywise)
                            {
                                int retval = objmysqldb.InsertUpdateDeleteData("update employee_punchtime_details_datewise set In_hour=" + int.Parse(dr["In_hour"].ToString()) + ",In_min=" + int.Parse(dr["In_min"].ToString()) + ",out_hour=" + int.Parse(dr["out_hour"].ToString()) + ",out_min=" + int.Parse(dr["out_min"].ToString()) + ",Ext_Early_hour=" + int.Parse(dr["Ext_Early_hour"].ToString()) + ",Ext_Early_min=" + int.Parse(dr["Ext_Early_min"].ToString()) + ",Ext_Late_hour=" + int.Parse(dr["Ext_Late_hour"].ToString()) + ",Ext_Late_min=" + int.Parse(dr["Ext_Late_min"].ToString()) + ",modify_datetime=" + currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_id + " where time_grp_id=" + grp_id + " and Day_id=" + int.Parse(dr["Day_id"].ToString()) + " and Dateticks=" + dtt.Ticks + " ");

                                if (retval == 0)
                                {
                                    retval = objmysqldb.InsertUpdateDeleteData("insert into employee_punchtime_details_datewise (Dateticks,time_grp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min,modify_datetime,DOC,IsUpdate,UserID,IsDelete) values (" + dtt.Ticks + "," + grp_id + "," + int.Parse(dr["Day_id"].ToString()) + "," + int.Parse(dr["In_hour"].ToString()) + "," + int.Parse(dr["In_min"].ToString()) + "," + int.Parse(dr["out_hour"].ToString()) + "," + int.Parse(dr["out_min"].ToString()) + "," + int.Parse(dr["Ext_Early_hour"].ToString()) + "," + int.Parse(dr["Ext_Early_min"].ToString()) + "," + int.Parse(dr["Ext_Late_hour"].ToString()) + "," + int.Parse(dr["Ext_Late_min"].ToString()) + "," + currenttimes.Ticks + "," + currenttimes.Ticks + ",1," + user_id + ",0)");
                                }
                            }
                        }
                            DataTable dtgroupdata = new DataTable();
                            dtgroupdata = objmysqldb.GetData("select * from employee_management.time_group_assign_emplyee_wise where Group_id=" + grp_id + "");
                            if (dtgroupdata.Rows.Count > 0 && dtgroupdata != null)
                            {
                                for (int d1 = 0; d1 <= dtDiff.Days; d1++)
                                {
                                    DateTime dtt1 = dtstartdate.AddDays(d1);
                                    //foreach (DataRow dr in dtdata.Rows)
                                    int days1 = (int)dtt1.DayOfWeek;
                                    if (days1 == 0)
                                    {
                                        days1 = 7;
                                    }
                                    DataRow[] drdaywise1 = dtdata.Select("Day_id =" + days1 + " ");
                                    for (int emp = 0; emp < dtgroupdata.Rows.Count; emp++)
                                    {
                                        foreach (DataRow dr in drdaywise1)
                                        {
                                            int retval = objmysqldb.InsertUpdateDeleteData("update employeewise_punchtime_details_datewise set In_hour=" + int.Parse(dr["In_hour"].ToString()) + ",In_min=" + int.Parse(dr["In_min"].ToString()) + ",out_hour=" + int.Parse(dr["out_hour"].ToString()) + ",out_min=" + int.Parse(dr["out_min"].ToString()) + ",Ext_Early_hour=" + int.Parse(dr["Ext_Early_hour"].ToString()) + ",Ext_Early_min=" + int.Parse(dr["Ext_Early_min"].ToString()) + ",Ext_Late_hour=" + int.Parse(dr["Ext_Late_hour"].ToString()) + ",Ext_Late_min=" + int.Parse(dr["Ext_Late_min"].ToString()) + ",modify_datetime=" + currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_id + " where emp_id=" + int.Parse(dtgroupdata.Rows[emp]["emp_id"].ToString()) + " and Day_id=" + int.Parse(dr["Day_id"].ToString()) + " and Dateticks=" + dtt1.Ticks + " ");
                                            if (retval == 0)
                                            {
                                                retval = objmysqldb.InsertUpdateDeleteData("insert into employeewise_punchtime_details_datewise (Dateticks,emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min,modify_datetime,DOC,IsUpdate,UserID,IsDelete) values (" + dtt1.Ticks + "," + int.Parse(dtgroupdata.Rows[emp]["emp_id"].ToString()) + "," + int.Parse(dr["Day_id"].ToString()) + "," + int.Parse(dr["In_hour"].ToString()) + "," + int.Parse(dr["In_min"].ToString()) + "," + int.Parse(dr["out_hour"].ToString()) + "," + int.Parse(dr["out_min"].ToString()) + "," + int.Parse(dr["Ext_Early_hour"].ToString()) + "," + int.Parse(dr["Ext_Early_min"].ToString()) + "," + int.Parse(dr["Ext_Late_hour"].ToString()) + "," + int.Parse(dr["Ext_Late_min"].ToString()) + "," + currenttimes.Ticks + "," + currenttimes.Ticks + ",1," + user_id + ",0)");
                                            }
                                        }
                                    }
                                }
                            }

                        #endregion

                            ltrErr.Text = "Time Group Details Save Successfully";
                        objmysqldb.EndSQLTransaction();
                        
                        }
                    else
                    {
                        ltrErr.Text = "Please Enter valid  Applicable change date";
                        objmysqldb.RollBackSQLTransaction();
                    }
                   
                }
                else
                {
                    ltrErr.Text = "Please try again";
                    return;
                }
            }
            catch (Exception ex)
            {
                objmysqldb.RollBackSQLTransaction();
                Logger.WriteCriticalLog("Manage_TimeGroup 288: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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

        private void getBinddata(int timeGrp)
        {
            ltrErr.Text = "";
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dttimegrp = new DataTable();
                dttimegrp.Columns.Add("Day_id");
                dttimegrp.Columns.Add("day");
                dttimegrp.Columns.Add("Intime");
                dttimegrp.Columns.Add("Outtime");
                dttimegrp.Columns.Add("Early");
                dttimegrp.Columns.Add("late");
                dttimegrp.Rows.Add("1", "Monday", "", "", "", "");
                dttimegrp.Rows.Add("2", "Tuesday", "", "", "", "");
                dttimegrp.Rows.Add("3", "Wednesday", "", "", "", "");
                dttimegrp.Rows.Add("4", "Thursday", "", "", "", "");
                dttimegrp.Rows.Add("5", "Friday", "", "", "", "");
                dttimegrp.Rows.Add("6", "Saturday", "", "", "", "");
                dttimegrp.Rows.Add("7", "Sunday", "", "", "", "");

                int grp_id = 0;
                int.TryParse(time_grp.Value.ToString(), out grp_id);
                if (grp_id > 0)
                {
                    DataTable dtPreviousTimeConfig = objmysqldb.GetData("select GroupWise_Time_Id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min,Group_id,Group_Name,Absent_SMS_After,OutPuch_SMS_After,Changes_Applicable_Date from groupwise_time_config inner join group_master on groupwise_time_config.Group_Time_Id=group_master.Group_id where group_master.Group_id=" + grp_id + " ");

                    if (dtPreviousTimeConfig != null && dtPreviousTimeConfig.Rows.Count > 0)
                    {
                        txtgroup.Text = dtPreviousTimeConfig.Rows[0]["Group_Name"].ToString();
                        SMSintime.Value = dtPreviousTimeConfig.Rows[0]["Absent_SMS_After"].ToString();
                        smsout.Value = dtPreviousTimeConfig.Rows[0]["OutPuch_SMS_After"].ToString();
                        DateTime dtt= new DateTime(long.Parse(dtPreviousTimeConfig.Rows[0]["Changes_Applicable_Date"].ToString()));
                        string todaydate = (dtt.Day.ToString().Length == 1 ? "0" + dtt.Day.ToString() : dtt.Day.ToString()) +
                      "/" +
                      (dtt.Month.ToString().Length == 1 ? "0" + dtt.Month.ToString() : dtt.Month.ToString())
                      + "/" +
                     dtt.Year;
                        date.Text = todaydate;
                        foreach (DataRow dr in dtPreviousTimeConfig.Rows)
                        {
                            DataRow[] drdata = dttimegrp.Select("Day_id= " + dr["Day_id"] + " ");
                            if (drdata.Length > 0)
                            {
                                drdata[0]["Intime"] = dr["In_hour"] + ":" + dr["In_min"];
                                drdata[0]["Outtime"] = dr["out_hour"] + ":" + dr["out_min"];
                                drdata[0]["Early"] = dr["Ext_Early_hour"] + ":" + dr["Ext_Early_min"];
                                drdata[0]["late"] = dr["Ext_Late_hour"] + ":" + dr["Ext_Late_min"];

                            }
                        }
                    }
                }
                if (dttimegrp != null)
                {
                    grd.DataSource = dttimegrp;
                    grd.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_TimeGroup 370:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}