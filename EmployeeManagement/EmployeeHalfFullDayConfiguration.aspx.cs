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

    public partial class EmployeeHalfFullDayConfiguration : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                {
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    Label header = Master.FindControl("lbl_pageHeader") as Label;
                    header.Text = "Employee Half Full Configuration";
                }
                else
                {
                    Response.Redirect("~/login.aspx", false);
                }
                if (!Page.IsPostBack)
                {
                    // bindgrid();
                    showdata();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/login.aspx", false);
            }

        }

        //private void bindgrid()
        //{
        //    objmysqldb.ConnectToDatabase();
        //    try
        //    {

        //        DataTable dtgrplist = objmysqldb.GetData("select EmpId,concat(EmpFirstName,' ',EmpMiddleName,' ',EmpLastName) as EmployeeName from employee_management.employee_master  where EmpStatusFlag=0 and IsDelete=0;");
        //        dtgrplist.Columns.Add("Mon_Half");
        //        dtgrplist.Columns.Add("Mon_Full");
        //        dtgrplist.Columns.Add("Tue_Half");
        //        dtgrplist.Columns.Add("Tue_Full");
        //        dtgrplist.Columns.Add("Wed_Half");
        //        dtgrplist.Columns.Add("Wed_Full");
        //        dtgrplist.Columns.Add("Thu_Half");
        //        dtgrplist.Columns.Add("Thu_Full");
        //        dtgrplist.Columns.Add("Fri_Half");
        //        dtgrplist.Columns.Add("Fri_Full");
        //        dtgrplist.Columns.Add("Sat_Half");
        //        dtgrplist.Columns.Add("Sat_Full");
        //        dtgrplist.Columns.Add("Sun_Half");
        //        dtgrplist.Columns.Add("Sun_Full");

        //        if (dtgrplist != null)
        //        {
        //            grdEmplist.DataSource = dtgrplist;
        //            grdEmplist.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        objmysqldb.disposeConnectionObj();
        //    }
        //}

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
                Logger.WriteCriticalLog("EmployeeHalfFullDayConfiguration_grdEmplist_RowDataBound() 101: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }

        }

        protected void savedata()
        {
            try
            {
                int colpos = 1;
                int val = 0;
                int firstindex = 0;
                int dayid = 0;

                System.Collections.Hashtable hsLeavePos = new System.Collections.Hashtable();
                colpos++;
                hsLeavePos.Add(colpos, 1);
                colpos++;
                hsLeavePos.Add(colpos, 1);
                colpos++;
                hsLeavePos.Add(colpos, 2);
                colpos++;
                hsLeavePos.Add(colpos, 2);
                colpos++;
                hsLeavePos.Add(colpos, 3);
                colpos++;
                hsLeavePos.Add(colpos, 3);
                colpos++;
                hsLeavePos.Add(colpos, 4);
                colpos++;
                hsLeavePos.Add(colpos, 4);
                colpos++;
                hsLeavePos.Add(colpos, 5);
                colpos++;
                hsLeavePos.Add(colpos, 5);
                colpos++;
                hsLeavePos.Add(colpos, 6);
                colpos++;
                hsLeavePos.Add(colpos, 6);
                colpos++;
                hsLeavePos.Add(colpos, 7);
                colpos++;
                hsLeavePos.Add(colpos, 7);
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                foreach (GridViewRow row in grdEmplist.Rows)
                {
                    Label EmpId = (Label)grdEmplist.Rows[row.RowIndex].FindControl("lblEmp_id");
                    Label Empname = (Label)grdEmplist.Rows[row.RowIndex].FindControl("lblEmp_Name");
                    TextBox txtmonhalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtmonhalf");
                    TextBox txtmonfull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtmonfull");
                    TextBox txttuehalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txttuehalf");
                    TextBox txttuefull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txttuefull");
                    TextBox txtwedhalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtwedhalf");
                    TextBox txtwedfull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtwedfull");
                    TextBox txtthuhalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtthuhalf");
                    TextBox txtthufull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtthufull");
                    TextBox txtfrihalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtfrihalf");
                    TextBox txtfrifull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtfrifull");
                    TextBox txtsathalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtsathalf");
                    TextBox txtsatfull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtsatfull");
                    TextBox txtsunhalf = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtsunhalf");
                    TextBox txtsunfull = (TextBox)grdEmplist.Rows[row.RowIndex].FindControl("txtsunfull");
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int empidint = int.Parse(EmpId.Text.ToString());
                    string monhalf = txtmonhalf.Text.ToString();
                    string monfull = txtmonfull.Text.ToString();
                    string tuehalf = txttuehalf.Text.ToString();
                    string tuefull = txttuefull.Text.ToString();
                    string wedhalf = txtwedhalf.Text.ToString();
                    string wedfull = txtwedfull.Text.ToString();
                    string thuhalf = txtthuhalf.Text.ToString();
                    string thufull = txtthufull.Text.ToString();
                    string frihalf = txtfrihalf.Text.ToString();
                    string frifull = txtfrifull.Text.ToString();
                    string sathalf = txtsathalf.Text.ToString();
                    string satfull = txtsatfull.Text.ToString();
                    string sunhalf = txtsunhalf.Text.ToString();
                    string sunfull = txtsunfull.Text.ToString();
                    string Total_half = "";
                    string Total_full = "";
                    if (txtmonhalf.Text != "" || txtmonfull.Text != "" || txttuehalf.Text != "" || txttuefull.Text != "" || txtwedhalf.Text != "" || txtwedfull.Text != "" || txtthuhalf.Text != "" || txtthufull.Text != "" || txtfrihalf.Text != "" || txtfrifull.Text != "" || txtsathalf.Text != "" || txtsatfull.Text != "" || txtsunhalf.Text != "" || txtsunfull.Text != "")
                    {
                        foreach (DictionaryEntry KeyId in hsLeavePos)
                        {
                            dayid = (int)KeyId.Key;
                            firstindex = (int)KeyId.Value;
                            string ColName = grdEmplist.Columns[dayid].HeaderText.ToString();


                            if (ColName == "Mon Half")
                            {
                                Total_half = monhalf;
                            }
                            if (ColName == "Mon Full")
                            {
                                Total_full = monfull;
                            }
                            if (ColName == "Tue Half")
                            {
                                Total_half = tuehalf;
                            }
                            if (ColName == "Tue Full")
                            {
                                Total_full = tuefull;
                            }
                            if (ColName == "Wed Half")
                            {
                                Total_half = wedhalf;
                            }
                            if (ColName == "Wed Full")
                            {
                                Total_full = wedfull;
                            }
                            if (ColName == "Thu Half")
                            {
                                Total_half = thuhalf;
                            }
                            if (ColName == "Thu Full")
                            {
                                Total_full = thufull;
                            }
                            if (ColName == "Fri Half")
                            {
                                Total_half = frihalf;
                            }
                            if (ColName == "Fri Full")
                            {
                                Total_full = frifull;
                            }
                            if (ColName == "Sat Half")
                            {
                                Total_half = sathalf;
                            }
                            if (ColName == "Sat Full")
                            {
                                Total_full = satfull;
                            }
                            if (ColName == "Sun Half")
                            {
                                Total_half = sunhalf;
                            }
                            if (ColName == "Sun Full")
                            {
                                Total_full = sunfull;
                            }
                            if (Total_full != "" || Total_half != "")
                            {
                                val = objmysqldb.InsertUpdateDeleteData("update employee_half_full_day_configuration set empid=" + empidint + ",half_hour='" + Total_half + "',full_hour='" + Total_full + "',modify_datetime=" + currenttime.Ticks + ",IsUpdate=1 where empid=" + empidint + " and dayid=" + firstindex + "");

                                if (val == 0)
                                {
                                    val = objmysqldb.InsertUpdateDeleteData("insert into employee_half_full_day_configuration(empid,dayid,half_hour,full_hour,modify_datetime,DOC,IsUpdate,UserID)values(" + empidint + "," + firstindex + ",'" + Total_half + "','" + Total_full + "'," + currenttime.Ticks + "," + currenttime.Ticks + ",0," + user_id + ")");

                                }
                                if (val == 1)
                                {
                                    ltrErr.Text = "Data Save Successfully..";
                                }
                            }
                        }
                    }
                    else
                    {

                    }

                }


            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeHalfFullDayConfiguration_savedata() 275: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            savedata();
            showdata();
        }

        protected void showdata()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtshowdata = objmysqldb.GetData("select EmpId,concat(EmpFirstName,' ',EmpMiddleName,' ',EmpLastName) as EmployeeName from employee_management.employee_master  where EmpStatusFlag=0 and IsDelete=0;");
                dtshowdata.Columns.Add("Mon_Half");
                dtshowdata.Columns.Add("Mon_Full");
                dtshowdata.Columns.Add("Tue_Half");
                dtshowdata.Columns.Add("Tue_Full");
                dtshowdata.Columns.Add("Wed_Half");
                dtshowdata.Columns.Add("Wed_Full");
                dtshowdata.Columns.Add("Thu_Half");
                dtshowdata.Columns.Add("Thu_Full");
                dtshowdata.Columns.Add("Fri_Half");
                dtshowdata.Columns.Add("Fri_Full");
                dtshowdata.Columns.Add("Sat_Half");
                dtshowdata.Columns.Add("Sat_Full");
                dtshowdata.Columns.Add("Sun_Half");
                dtshowdata.Columns.Add("Sun_Full");
                int empid = 0;
                int id = 0;
                DataTable dtdata = objmysqldb.GetData("SELECT * FROM employee_management.employee_half_full_day_configuration;");

                for (int i = 0; i < dtshowdata.Rows.Count; i++)
                {
                    empid = int.Parse(dtshowdata.Rows[i]["EmpId"].ToString());
                    DataRow[] dr = dtdata.Select("empid=" + empid);
                    if (dr.Length > 0)
                    {
                        DataTable dt = dr.CopyToDataTable();
                        // for (int k = 0; k < dt.Rows.Count; k++) { 

                        for (int j = 1; j <= 7; j++)
                        {
                            string halfday = "";
                            string fullday = "";
                            //id = int.Parse(dt.Rows[k]["dayid"].ToString());
                            //string halfday = dt.Rows[k]["half_hour"].ToString();
                            //string fullday = dt.Rows[k]["full_hour"].ToString();
                            DataRow[] drselect = dt.Select("dayid=" + j);
                            if (drselect.Length > 0)
                            {
                                halfday = drselect[0]["half_hour"].ToString();
                                fullday = drselect[0]["full_hour"].ToString();
                                if (drselect[0]["dayid"].ToString().Equals("1"))
                                {
                                    dtshowdata.Rows[i]["Mon_Half"] = halfday;
                                    dtshowdata.Rows[i]["Mon_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Mon_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Mon_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("2"))
                                {
                                    dtshowdata.Rows[i]["Tue_Half"] = halfday;
                                    dtshowdata.Rows[i]["Tue_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Tue_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Tue_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("3"))
                                {
                                    dtshowdata.Rows[i]["Wed_Half"] = halfday;
                                    dtshowdata.Rows[i]["Wed_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Wed_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Wed_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("4"))
                                {
                                    dtshowdata.Rows[i]["Thu_Half"] = halfday;
                                    dtshowdata.Rows[i]["Thu_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Thu_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Thu_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("5"))
                                {
                                    dtshowdata.Rows[i]["Fri_Half"] = halfday;
                                    dtshowdata.Rows[i]["Fri_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Fri_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Fri_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("6"))
                                {
                                    dtshowdata.Rows[i]["Sat_Half"] = halfday;
                                    dtshowdata.Rows[i]["Sat_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Sat_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Sat_Full"] = "0";
                                    //}
                                }
                                if (drselect[0]["dayid"].ToString().Equals("7"))
                                {
                                    dtshowdata.Rows[i]["Sun_Half"] = halfday;
                                    dtshowdata.Rows[i]["Sun_Full"] = fullday;
                                    //if (halfday == "" || fullday == "")
                                    //{
                                    //    dtshowdata.Rows[i]["Sun_Half"] = "0";
                                    //    dtshowdata.Rows[i]["Sun_Full"] = "0";
                                    //}
                                }

                            }
                            //}
                        }
                    }
                }
                if (dtshowdata != null)
                {
                    grdEmplist.DataSource = dtshowdata;
                    grdEmplist.DataBind();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeHalfFullDayConfiguration_showdata() 420: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}