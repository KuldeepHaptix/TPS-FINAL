using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class AcademicYearSetUp : System.Web.UI.Page
    {
        int user_id = 0;
        DateTime dtStart = new DateTime();
        DateTime dtend = new DateTime();
        MySQLDB objmysqldb = new MySQLDB();
        DataTable dtYearList = new DataTable();
        DataTable dtYear = new DataTable();
        DataTable ChkYear = new DataTable();
        int year_id = 0;
        //string val = "Select Year";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    ltrErr.Text = "";
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        header.Text = "Academic Year Setup ";
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
                    fillWithData();
                    ltrErr.Text = "";
                    bindCombo();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }
        protected void txtstartdate_TextChanged(object sender, EventArgs e)
        {
            CountNoOFDays();

        }
        protected void txtenddate_TextChanged(object sender, EventArgs e)
        {
            CountNoOFDays();
        }
        private double CountNoOFDays()
        {
            double NoOfDays = 0.0;
            try
            {
                char[] ch = { '/' };
                string[] spltstart = txtstartdate.Text.Split(ch);
                string[] spltend = txtenddate.Text.Split(ch);
                if (spltstart.Length == 3)
                {
                    dtStart = new DateTime(int.Parse(spltstart[2]), int.Parse(spltstart[1]), int.Parse(spltstart[0]));
                }
                if (spltend.Length == 3)
                {
                    dtend = new DateTime(int.Parse(spltend[2]), int.Parse(spltend[1]), int.Parse(spltend[0]));
                }
                if (spltstart.Length == 3 && spltend.Length == 3)
                {
                    TimeSpan ts = dtend.Subtract(dtStart);
                    txttotaldays.Text = ts.Days.ToString();
                    NoOfDays = ts.Days;
                }
                else
                {
                    txttotaldays.Text = "0";
                    NoOfDays =0;
                }

            }
            catch (Exception ee)
            {
                Logger.WriteCriticalLog("Academic Year Set Up 85: exception:" + ee.Message + "::::::::" + ee.StackTrace);

            }

            return NoOfDays;
        }

        public void fillWithData()
        {
            objmysqldb.ConnectToDatabase();
            // BindAcademicYear();
            try
            {
                DataTable dtData = objmysqldb.GetData("SELECT year_id,Academic_Year,Start_Date as startdate,End_Date as enddate FROM academicyear where IsDelete=0;");
                if (dtData.Rows.Count > 0 && dtData != null)
                {
                    dtData.Columns.Add("Start_Date");
                    dtData.Columns.Add("End_Date");
                    int c = dtData.Rows.Count;

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        //string startDate = dtData.Rows[i]["startdate"].ToString();
                        long startdate = long.Parse(dtData.Rows[i]["startdate"].ToString());
                        DateTime startdt = new DateTime(startdate);
                        //String myString_new = startdt.ToString().Replace("00:00:00", "");
                        //String myString_new = String.Format("{0:MM/dd/yyyy}", startdt);
                        String myString_new = String.Format("{0:dd/MM/yyyy}", startdt);
                        dtData.Rows[i]["Start_Date"] = myString_new;

                        long enddate = long.Parse(dtData.Rows[i]["enddate"].ToString());
                        DateTime enddt = new DateTime(enddate);
                        //String myString_new1 = enddt.ToString().Replace("00:00:00", "");

                        String myString_new1 = String.Format("{0:dd/MM/yyyy}", enddt);
                        dtData.Rows[i]["End_Date"] = myString_new1;
                    }
                    // dtYear.AcceptChanges();
                    //dlYear.DataSource = dtYear;
                    //dlYear.DataBind();
                    //dlYear.Items.Insert(0, new ListItem("Select Year", "-2"));
                    grdyearlist.AutoGenerateColumns = false;
                    grdyearlist.DataSource = dtData;
                    grdyearlist.DataBind();

                }
                else
                {
                    //BindAcademicYear();
                    grdyearlist.AutoGenerateColumns = false;
                    grdyearlist.DataSource = dtData.Clone();
                    grdyearlist.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Academic Year Set Up 141: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";

                if (txtstartdate.Text == "" && txtenddate.Text == "")
                {
                    ltrErr.Text = "Enter Valid Date..";
                    //ltrErr.Text = "Select Valid Dates..";
                    return;
                }
                else
                {
                    string academicYear = dlYear.Items[dlYear.SelectedIndex].Text;
                    string[] AcademicYear = academicYear.Split('-');
                    double totalDays = CountNoOFDays();
                    char[] ch = { '/' };
                    //String myString_new1 = String.Format("{0:MM/dd/yyyy}", enddt);
                    //dtData.Rows[i]["End_Date"] = myString_new1;
                    string[] spltstart = txtstartdate.Text.Split(ch);
                    string[] spltend = txtenddate.Text.Split(ch);

                    dtStart = new DateTime(int.Parse(spltstart[2]), int.Parse(spltstart[1]), int.Parse(spltstart[0]));
                    dtend = new DateTime(int.Parse(spltend[2]), int.Parse(spltend[1]), int.Parse(spltend[0]));

                    int startmonth = int.Parse(spltstart[1]);
                    int endmonth = int.Parse(spltend[1]);

                    int startyr = int.Parse(spltstart[2]);
                    int endyear = int.Parse(spltend[2]);

                    //if ((startyr == Convert.ToInt32(AcademicYear[0])) && (endyear <= Convert.ToInt32(AcademicYear[1])) && (endyear >= Convert.ToInt32(AcademicYear[0])))
                    //if (startyr != Convert.ToInt32(AcademicYear[0]) && (endyear != Convert.ToInt32(AcademicYear[1])))
                    if ((startyr >= Convert.ToInt32(AcademicYear[0])) && (endyear <= Convert.ToInt32(AcademicYear[1])) && (startyr <= endyear))
                    {

                        if (totalDays > 366 || totalDays <= 0)
                        {
                            ltrErr.Text = "Enter Valid Date..";
                            //ltrErr.Text = "Choose VAlid Dates.";
                            return;
                        }
                        else
                        {
                            try
                            {
                                DateTime currenttime = Logger.getIndiantimeDT();
                                int val = 0;
                                objmysqldb.ConnectToDatabase();
                                objmysqldb.OpenSQlConnection();

                                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                                val = objmysqldb.InsertUpdateDeleteData("insert into AcademicYear(Academic_Year,modify_datetime,DOC,Start_Date,End_Date,IsUpdate,IsDelete,UserID)values('" + academicYear + "'," + currenttime.Ticks + "," + currenttime.Ticks + " ," + dtStart.Ticks + ",'" + dtend.Ticks + "',0,0," + user_id + ")");
                                if (val == 1)
                                {

                                    ltrErr.Text = "Academic Year Saved Suceessfully...";
                                    txtstartdate.Text = "";
                                    txtenddate.Text = "";
                                    txttotaldays.Text = "";
                                    fillWithData();
                                    bindCombo();
                                }
                            }

                            //}
                            catch (Exception ex)
                            {
                                Logger.WriteCriticalLog("AcademicYearSetUp 280: exception:" + ex.Message + "::::::::" + ex.StackTrace);

                            }
                            finally
                            {
                                objmysqldb.CloseSQlConnection();
                                objmysqldb.disposeConnectionObj();
                            }

                        }

                    }

                    else
                    {
                        ltrErr.Text = "Enter Valid Input..";
                        // ltrErr.Text = "Selected Year MustMatch With year in Seleted Date";
                        //ltrErr.Text = "";

                    }
                }
            }

            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Academic Year SetUp 244: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grdyearlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ltrErr.Text = "";
            try
            {
                if (e.CommandName == "del")
                {
                    int year_id = 0;
                    string[] arg = e.CommandArgument.ToString().Split(':');
                    if (arg != null && arg.Length == 2)
                    {
                        int.TryParse(arg[1], out year_id);
                        objmysqldb.ConnectToDatabase();
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        DateTime currenttime = Logger.getIndiantimeDT();
                        string query = "Update academicyear set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where year_id=" + year_id + " ";
                        objmysqldb.OpenSQlConnection();
                        int res = objmysqldb.InsertUpdateDeleteData(query);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";

                            Logger.WriteCriticalLog("Academic Year SetUp 344 Update error.");
                        }
                        else
                        {
                            grdyearlist.EditIndex = -1;
                            ltrErr.Text = "Academic Year Delete Successfully.";
                            fillWithData();
                            bindCombo();
                        }
                    }
                }

                else if (e.CommandName == "Edit")
                {

                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 291: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void grdyearlist_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string command = e.ToString();
                grdyearlist.EditIndex = e.NewEditIndex;
                fillWithData();

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 313: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grdyearlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //txtstartdate.Enabled = false;
            //txtenddate.Enabled = false;

            //string academicYear = Convert.ToString(dlYearlist.SelectedItem);
            ltrErr.Text = "";
            string academicYear = "";
            try
            {
                Label lblyearid = (Label)grdyearlist.Rows[e.RowIndex].FindControl("lblyearid");
                string yr = lblyearid.Text.ToString();
                int.TryParse(yr.ToString(), out year_id);
                //TextBox academicYear = (TextBox)grdyearlist.Rows[e.RowIndex].FindControl("txtAcademicYear1");
                Label academicYear1 = (Label)grdyearlist.Rows[e.RowIndex].FindControl("txtAcademicYear1");
                TextBox txtStartdate = (TextBox)grdyearlist.Rows[e.RowIndex].FindControl("txtStartdate");
                TextBox txtEnddate = (TextBox)grdyearlist.Rows[e.RowIndex].FindControl("txtEnddate");
                //DateTime strat=new DateTime(txtstartdate.Text);
                //startdt=long.TryParse(txtstartdate.Text.ToString(),out startdt)
                //academicYear = academicYear.Text.ToString();
                double NoOfDays = 0.0;
                char[] ch = { '/' };
                string[] spltstart = txtStartdate.Text.Split(ch);
                string[] spltend = txtEnddate.Text.Split(ch);
                String myString_new = String.Format("{0:MM/dd/yyyy}", txtstartdate.Text);
                dtStart = new DateTime(int.Parse(spltstart[2]), int.Parse(spltstart[1]), int.Parse(spltstart[0]));
                dtend = new DateTime(int.Parse(spltend[2]), int.Parse(spltend[1]), int.Parse(spltend[0]));

                TimeSpan ts = dtend.Subtract(dtStart);
                txttotaldays.Text = ts.Days.ToString();
                NoOfDays = ts.Days;

                dtStart = new DateTime(int.Parse(spltstart[2]), int.Parse(spltstart[1]), int.Parse(spltstart[0]));
                dtend = new DateTime(int.Parse(spltend[2]), int.Parse(spltend[1]), int.Parse(spltend[0]));

                int startmonth = int.Parse(spltstart[1]);
                int endmonth = int.Parse(spltend[1]);

                int startyr = int.Parse(spltstart[2]);
                int endyear = int.Parse(spltend[2]);

                string[] AcademicYear = academicYear1.Text.Split('-');

                if ((startyr >= Convert.ToInt32(AcademicYear[0])) && (endyear <= Convert.ToInt32(AcademicYear[1])) && (startyr <= endyear))
                {
                    if (NoOfDays > 366 || NoOfDays <= 0)
                    {
                        ltrErr.Text = "Enter Valid Input..";
                    }

                    else
                    {
                        objmysqldb.ConnectToDatabase();
                        objmysqldb.OpenSQlConnection();

                        try
                        {
                            DateTime currenttime = Logger.getIndiantimeDT();
                            int val = 0;
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            val = objmysqldb.InsertUpdateDeleteData("update  academicyear set Academic_Year='" + academicYear1.Text.ToString() + "',modify_datetime=" + currenttime.Ticks + ",Start_Date=" + dtStart.Ticks + ",End_Date='" + dtend.Ticks + "',IsUpdate=1,UserID=" + user_id + " where year_id=" + year_id + " ");

                            if (val == 1)
                            {
                                grdyearlist.EditIndex = -1;
                                fillWithData();
                                ltrErr.Text = "Academic Year Updated...";
                                txttotaldays.Text = " ";
                                //txtstartdate.Enabled = true;
                                //txtstartdate.Enabled = true;
                                //dlYear.Ena = true;
                            }
                            else
                            {
                                ltrErr.Text = "Error While Academic Year Updated...";
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteCriticalLog("AcademicYearSetUp 495: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                        }
                        finally
                        {
                            objmysqldb.CloseSQlConnection();
                            objmysqldb.disposeConnectionObj();
                        }
                        // grdyearlist.EditIndex = -1;
                    }

                }
                else
                {

                    ltrErr.Text = "Enter Valid Input..";
                    //ltrErr.Text = "";

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 416: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grdyearlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdyearlist.EditIndex = -1;
                fillWithData();

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 429: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }
        protected void grdyearlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                { }
                //dlYear.Items.FindByValue(val).Selected = true;
            }
            catch (Exception aa)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 447: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }
        }
        public void bindCombo()
        {
            try
            {
                dtYear.Columns.AddRange(new DataColumn[2] { new DataColumn("ID"), new DataColumn("AcademicYear") });
                dtYear.Rows.Add("1", "2005-2006");
                dtYear.Rows.Add("2", "2006-2007");
                dtYear.Rows.Add("3", "2007-2008");
                dtYear.Rows.Add("4", "2008-2009");
                dtYear.Rows.Add("5", "2009-2010");
                dtYear.Rows.Add("6", "2010-2011");
                dtYear.Rows.Add("7", "2011-2012");
                dtYear.Rows.Add("8", "2012-2013");
                dtYear.Rows.Add("9", "2013-2014");
                dtYear.Rows.Add("10", "2014-2015");
                dtYear.Rows.Add("11", "2015-2016");
                dtYear.Rows.Add("12", "2016-2017");
                dtYear.Rows.Add("13", "2017-2018");
                dtYear.Rows.Add("14", "2018-2019");
                dtYear.Rows.Add("15", "2019-2020");
                dtYear.Rows.Add("16", "2020-2021");
                dtYear.Rows.Add("17", "2021-2022");
                dtYear.Rows.Add("18", "2022-2023");
                dtYear.Rows.Add("19", "2023-2024");
                dtYear.Rows.Add("20", "2024-2025");
                dtYear.Rows.Add("21", "2025-2026");
                dtYear.Rows.Add("22", "2026-2027");
                dtYear.Rows.Add("23", "2027-2028");
                dtYear.Rows.Add("24", "2028-2029");
                dtYear.Rows.Add("25", "2029-2030");
                dtYear.Rows.Add("26", "2030-2031");
                dtYear.Rows.Add("27", "2031-2032");
                dtYear.Rows.Add("28", "2032-2033");
                dtYear.Rows.Add("29", "2033-2034");
                dtYear.Rows.Add("30", "2034-2035");
                dtYear.Rows.Add("31", "2035-2036");
                dtYear.Rows.Add("32", "2036-2037");
                dtYear.Rows.Add("33", "2037-2038");
                dtYear.Rows.Add("34", "2038-2039");
                dtYear.Rows.Add("35", "2039-2040");
                // dlYear.DataSource = dtYear;
                dlYear.DataTextField = "AcademicYear";
                dlYear.DataValueField = "ID";
                //dlYear.DataBind();
                //int val = dlYear.Items.Count;
                for (int i = 0; i < grdyearlist.Rows.Count; i++)
                {
                    Label academicYear = (Label)grdyearlist.Rows[i].FindControl("txtAcademicYear1");

                    string gvyear = academicYear.Text.ToString();
                    DataRow[] yearrow = dtYear.Select("AcademicYear='" + gvyear + "'");
                    if (yearrow.Length > 0)
                    {
                        for (int j = 0; j < yearrow.Length; j++)
                        {
                            yearrow[j].Delete();
                        }
                    }

                }
                dtYear.AcceptChanges();
                dlYear.DataSource = dtYear;
                dlYear.DataBind();
                dlYear.Items.Insert(0, new ListItem("Select Year", "-2"));
                //dlYear.Items.FindByValue(val).Selected = true;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("AcademicYearSetUp 518: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
        }
    }
}
