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
    public partial class Holiday_Setup : System.Web.UI.Page
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
                        userids.Value = user_id.ToString();
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        header.Text = "Holiday Setup";
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
                    bindholidayprofile();

                    DataTable dtholidayType = new DataTable();
                    dtholidayType.Columns.Add("id");
                    dtholidayType.Columns.Add("Name");
                    dtholidayType.Rows.Add("1", "Public Holiday");
                    dtholidayType.Rows.Add("2", "Week off Holiday");
                    dtholidayType.Rows.Add("3", "Other Holiday");

                    ddltype.DataSource = dtholidayType;
                    ddltype.DataTextField = "Name";
                    ddltype.DataValueField = "id";
                    ddltype.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Holiday_Setup 65: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        private void bindholidayprofile()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtHolidayProfile = objmysqldb.GetData("SELECT Holiday_Profile_Id,Holiday_Profile_Name FROM holiday_profile_master ");
                ddlholiday.DataSource = dtHolidayProfile;
                ddlholiday.DataTextField = "Holiday_Profile_Name";
                ddlholiday.DataValueField = "Holiday_Profile_Id";
                ddlholiday.DataBind();
                ddlholiday.Items.Insert(0, new ListItem("Select Holiday Profile", "-1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Holiday_Setup 83: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        [System.Web.Services.WebMethod]
        public static string saveHolidayProfile(string id, string Name, string userid, string holiday_day)
        {
            MySQLDB dbc = new MySQLDB();
            try
            {

                string msg = "";
                int ids = 0;
                int.TryParse(id, out ids);

                dbc.ConnectToDatabase();
                DataTable dtHolidayProfile = dbc.GetData("SELECT Holiday_Profile_Id,Holiday_Profile_Name,Holiday_day_id FROM holiday_profile_master ");

                string response = "";
                DataRow[] drdata = dtHolidayProfile.Select("Holiday_Profile_Name ='" + Name + "' and  Holiday_Profile_Id <> " + id + " ");
                dbc.OpenSQlConnection();
                dbc.BeginSQLTransaction();
                if (drdata.Length == 0)
                {
                    holiday_day = holiday_day.TrimEnd(',');

                    DateTime currenttime = Logger.getIndiantimeDT();
                    int user_ids = 0;
                    int.TryParse(userid, out user_ids);
                    int retval = dbc.InsertUpdateDeleteData("update holiday_profile_master set Holiday_Profile_Name='" + Name + "',modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_ids + ",Holiday_day_id='" + holiday_day + "' where Holiday_Profile_Id=" + id + " ");
                    if (retval == 0)
                    {
                        retval = dbc.InsertUpdateDeleteData("insert into holiday_profile_master (Holiday_Profile_Name,modify_datetime,DOC,IsUpdate,UserID,Holiday_day_id) values ('" + Name + "'," + currenttime.Ticks + "," + currenttime.Ticks + ",1," + user_ids + ",'" + holiday_day + "')");
                        if (retval != 1)
                        {
                            msg = "please try again";
                            Logger.WriteCriticalLog("Holiday_Setup 123 Update error.");
                        }
                        else
                        {
                            msg = "Save sucessfully";
                        }
                    }
                    else
                    {
                        //Holiday setup code
                        DataRow[] drdatas = dtHolidayProfile.Select("Holiday_Profile_Id = " + id + " ");
                        if (drdatas.Length > 0)
                        {
                            string[] old_Weekoff = drdatas[0]["Holiday_day_id"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string[] New_Weekoff = holiday_day.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (old_Weekoff.Length > 0)
                            {
                                string weekoff = "";
                                for (int k = 0; k < old_Weekoff.Length; k++)
                                {
                                    if (!New_Weekoff.Contains(old_Weekoff[k]))
                                    {
                                        weekoff += old_Weekoff[k] + ",";
                                    }
                                }

                                string[] Weekoffday = weekoff.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (Weekoffday.Length > 0)
                                {
                                    DataTable dtholidaySetup = dbc.GetData("select distinct Holiday_Month,Holiday_Year from holiday_setup where Holiday_For_Id=2 and Holiday_Profile_Id=" + id + " and IsDelete=0;");
                                    if (dtholidaySetup != null && dtholidaySetup.Rows.Count > 0)
                                    {
                                        int mon = 0;
                                        int year = 0;
                                        for (int mo = 0; mo < dtholidaySetup.Rows.Count; mo++)
                                        {
                                            int.TryParse(dtholidaySetup.Rows[mo]["Holiday_Month"].ToString(), out mon);
                                            int.TryParse(dtholidaySetup.Rows[mo]["Holiday_Year"].ToString(), out year);
                                            int LastDate = DateTime.DaysInMonth(year, mon);
                                            string dates = DateOfGivenDay(Weekoffday, mon, year, LastDate);
                                            dates = dates.TrimEnd(',');
                                            if (!dates.Equals(""))
                                            {
                                                DateTime currenttimes = Logger.getIndiantimeDT();
                                                retval = dbc.InsertUpdateDeleteData("update holiday_setup set modify_datetime=" + currenttimes.Ticks + ",IsUpdate=1,UserID=" + user_ids + ",IsDelete=1 where Holiday_Day IN(" + dates + ") and Holiday_Month=" + mon + " and Holiday_Year=" + year + " and Holiday_Profile_Id=" + id + "  and Holiday_For_Id=2");
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        msg = "Save sucessfully";
                    }

                    dtHolidayProfile = dbc.GetData("SELECT Holiday_Profile_Id,Holiday_Profile_Name FROM holiday_profile_master ");
                    for (int i = 0; i < dtHolidayProfile.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            response = response + "-1,";
                            response = response + "Select Holiday Profile ###";
                        }
                        response = response + dtHolidayProfile.Rows[i]["Holiday_Profile_Id"].ToString() + ",";
                        response = response + dtHolidayProfile.Rows[i]["Holiday_Profile_Name"].ToString() + "###";
                    }
                    response = response.TrimEnd('#');
                    response = response + "@@@" + msg;

                }
                else
                {
                    msg = "Holiday profile name is already exists";
                    response = "@@@" + msg;
                }
                dbc.EndSQLTransaction();
                return response;

            }
            catch (Exception ex)
            {
                dbc.RollBackSQLTransaction();
                Logger.WriteCriticalLog("Holiday_Setup 206: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                return "@@@please try again";
            }
            finally
            {
                dbc.CloseSQlConnection();
                dbc.disposeConnectionObj();
            }
        }

        [System.Web.Services.WebMethod]
        public static string getHolidayList(string id)
        {
            MySQLDB dbc = new MySQLDB();
            try
            {

                int ids = 0;
                int.TryParse(id, out ids);

                dbc.ConnectToDatabase();


                DataTable dtholiday = dbc.GetData("SELECT Holiday_day_id FROM employee_management.holiday_profile_master  where  Holiday_Profile_Id=" + id + " ");
                string response = "";
                if (dtholiday != null && dtholiday.Rows.Count > 0)
                {
                    response = dtholiday.Rows[0]["Holiday_day_id"].ToString() + "@@@";
                }
                //DataTable dtHolidaydetails = dbc.GetData("SELECT Holiday_Day,Holiday_Month,Holiday_Year,Holiday_Reason,Holiday_For_Id FROM holiday_setup where IsDelete=0 and Holiday_Profile_Id=" + id + " ");
                //for (int i = 0; i < dtHolidaydetails.Rows.Count; i++)
                //{
                //    response += dtHolidaydetails.Rows[i]["Holiday_Day"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Month"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Year"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Reason"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_For_Id"].ToString() + "###";
                //}


                response = List_of_Holiday(id, dbc, response);
                response = response.TrimEnd('#');
                return response;

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Holiday_Setup 249: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                return "";
            }
            finally
            {
                dbc.CloseSQlConnection();
                dbc.disposeConnectionObj();
            }
        }


        [System.Web.Services.WebMethod]
        public static string saveDefaultSetup(string id, string month, string userid)
        {
            MySQLDB dbc = new MySQLDB();
            try
            {

                int ids = 0;
                int.TryParse(id, out ids);
                string[] months = month.Split('@');
                int mon = 0;
                int year = 0;
                int.TryParse(months[0], out mon);
                int.TryParse(months[1], out year);

                int LastDate = DateTime.DaysInMonth(year, mon);
                dbc.ConnectToDatabase();
                int user_ids = 0;
                int.TryParse(userid, out user_ids);

                DataTable dtholiday = dbc.GetData("SELECT Holiday_day_id FROM employee_management.holiday_profile_master where Holiday_Profile_Id=" + id + " ");
                string response = "";

                if (dtholiday != null && dtholiday.Rows.Count > 0)
                {
                    string[] Weekoff = dtholiday.Rows[0]["Holiday_day_id"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    string dates = DateOfGivenDay(Weekoff, mon, year, LastDate);
                    dates = dates.TrimEnd(',');
                    string[] date = dates.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    dbc.OpenSQlConnection();
                    dbc.BeginSQLTransaction();
                    for (int i = 0; i < date.Length; i++)
                    {
                        DateTime currenttime = Logger.getIndiantimeDT();
                        int retval = dbc.InsertUpdateDeleteData("update holiday_setup set modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_ids + " where Holiday_Day=" + int.Parse(date[i].ToString()) + " and Holiday_Month=" + mon + " and Holiday_Year=" + year + " and Holiday_Profile_Id=" + id + "  and IsDelete=0 ");
                        if (retval == 0)
                        {
                            retval = dbc.InsertUpdateDeleteData("insert into holiday_setup (Holiday_Day,Holiday_Month,Holiday_Year,Holiday_Reason,Holiday_For_Id,Holiday_Profile_Id,modify_datetime,DOC,IsUpdate,IsDelete,UserID) values (" + int.Parse(date[i].ToString()) + "," + mon + "," + year + " ,'Week off',2," + id + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1,0," + user_ids + ")");
                            if (retval != 1)
                            {
                                dbc.RollBackSQLTransaction();
                                Logger.WriteCriticalLog("Holiday_Setup 302 Update error.");
                                return response;
                            }
                        }

                    }

                    response = dtholiday.Rows[0]["Holiday_day_id"].ToString() + "@@@";

                }
                dbc.EndSQLTransaction();
                response = List_of_Holiday(id, dbc, response);
                response = response.TrimEnd('#');

                return response;

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Holiday_Setup 321: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                return "";
            }
            finally
            {
                dbc.CloseSQlConnection();
                dbc.disposeConnectionObj();
            }
        }

        private static string List_of_Holiday(string id, MySQLDB dbc, string response)
        {
            DataTable dtHolidaydetails = dbc.GetData("SELECT Holiday_Day,Holiday_Month,Holiday_Year,Holiday_Reason,Holiday_For_Id FROM holiday_setup where IsDelete=0 and Holiday_Profile_Id=" + id + " ");
            for (int i = 0; i < dtHolidaydetails.Rows.Count; i++)
            {
                response += dtHolidaydetails.Rows[i]["Holiday_Day"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Month"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Year"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_Reason"].ToString() + "@" + dtHolidaydetails.Rows[i]["Holiday_For_Id"].ToString() + "###";
            }
            return response;
        }

        public static string DateOfGivenDay(string[] weekofday, int month, int year, int lastday)
        {
            string dates = "";
            for (int i = 0; i < weekofday.Length; i++)
            {
                int day = 0;
                int.TryParse(weekofday[i], out day);
                for (int j = 1; j <= lastday; j++)
                {
                    DateTime dt = new DateTime(year, month, j);
                    int day1 = (int)dt.DayOfWeek;
                    if (day == 7)
                    {
                        if (day1 == 0)
                        {
                            dates += j + ",";
                        }
                    }
                    else
                    {
                        if (day1 == day)
                        {
                            dates += j + ",";
                        }
                    }
                }

            }
            return dates;
        }

        [System.Web.Services.WebMethod]
        public static string MangeHolidays(string id, string type, string date, string reason, string range, string flag, string userid)
        {
            MySQLDB dbc = new MySQLDB();
            try
            {

                int ids = 0;
                int.TryParse(id, out ids);
                int typ = 0;
                int.TryParse(type, out typ);

                int fla = 0;
                int.TryParse(flag, out fla);
                string msg = "";
                string[] dates = date.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int day = 0;
                int mon = 0;
                int yea = 0;
                dbc.ConnectToDatabase();
                int user_ids = 0;
                int.TryParse(userid, out user_ids);
                if (dates.Length == 3)
                {
                    dbc.OpenSQlConnection();
                    int.TryParse(dates[0], out day);
                    int.TryParse(dates[1], out mon);
                    int.TryParse(dates[2], out yea);
                    if (fla == 1) //delete
                    {
                        DateTime currenttime = Logger.getIndiantimeDT();
                        int retval = dbc.InsertUpdateDeleteData("update holiday_setup set modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_ids + ",IsDelete=1 where Holiday_Day=" + day + " and Holiday_Month=" + mon + " and Holiday_Year=" + yea + " and Holiday_Profile_Id=" + ids + " ");
                       
                            msg = "Holiday remove Successfully";
                     
                    }
                    else
                    {
                        string[] rangedates = range.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        int start = 0;
                        int end = 0;
                        if (rangedates.Length == 2)
                        {
                            int.TryParse(rangedates[0], out start);
                            int.TryParse(rangedates[1], out end);
                        }
                        int diff = end -start;
                        for (int i = 0; i <= diff; i++)
                        {
                            DateTime currenttime = Logger.getIndiantimeDT();
                        
                            dbc.AddCommandParameter("Holiday_Reason", reason);
                            int retval = dbc.InsertUpdateDeleteData("update holiday_setup set modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_ids + ",Holiday_For_Id=" + typ + ",Holiday_Reason=?Holiday_Reason where Holiday_Day=" + day + " and Holiday_Month=" + mon + " and Holiday_Year=" + yea + " and Holiday_Profile_Id=" + id + "  and IsDelete=0 ");
                            if (retval == 0)
                            {
                               
                                dbc.AddCommandParameter("Holiday_Reason", reason);
                                retval = dbc.InsertUpdateDeleteData("insert into holiday_setup (Holiday_Day,Holiday_Month,Holiday_Year,Holiday_Reason,Holiday_For_Id,Holiday_Profile_Id,modify_datetime,DOC,IsUpdate,IsDelete,UserID) values (" + day + "," + mon + "," + yea + " ,?Holiday_Reason,"+typ+"," + id + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1,0," + user_ids + ")");
                                if (retval != 1)
                                {
                                    msg = "Please try again";
                                    Logger.WriteCriticalLog("Holiday_Setup 433 Update error.");
                                    break;
                                }
                            }
                            day++;
                        }
                        if (msg.Equals(""))
                        {
                            msg = "Holiday save Successfully";
                        }
                    }
                    
                }
                else
                {
                    msg = "Please Select Valid Date";
                }


                DataTable dtholiday = dbc.GetData("SELECT Holiday_day_id FROM employee_management.holiday_profile_master where  Holiday_Profile_Id=" + id + " ");
                string response = "";

                if (dtholiday != null && dtholiday.Rows.Count > 0)
                {
                    response = dtholiday.Rows[0]["Holiday_day_id"].ToString() + "@@@";

                }
                response = List_of_Holiday(id, dbc, response);
                response = response.TrimEnd('#');
                response = response + "@@##" + msg;
                return response;

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Holiday_Setup 468: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                return "";
            }
            finally
            {
                dbc.CloseSQlConnection();
                dbc.disposeConnectionObj();
            }
        }
    }
}