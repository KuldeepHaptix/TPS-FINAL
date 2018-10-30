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
    public partial class EmpWiseTimeConfig : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();

        int count = 0;
        Label lblDateTime;
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (base.Request.Cookies.AllKeys.Contains("LoginCookies") && base.Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(base.Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label label = base.Master.FindControl("lbl_pageHeader") as Label;
                        label.Text = "EmployeeWiseTimeConfig";
                        btnSave.Visible = false;
                        divText.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                catch (Exception varAC0)
                {
                    Response.Redirect("~/login.aspx", false);
                }
                if (!Page.IsPostBack)
                {
                    bindMonth();
                    getAllEmployee();
                    if (base.Request.Cookies.AllKeys.Contains("getdata") && base.Request.Cookies["getdata"] != null)
                    {
                        int eid = 0;
                        int Mid = 0;
                        //give index

                        //get value
                        int.TryParse(base.Request.Cookies["getdata"]["empid"].ToString(), out eid);
                        int.TryParse(base.Request.Cookies["getdata"]["month"].ToString(), out Mid);
                        ddlEmp.SelectedIndex = eid;
                        ddlmonthlist.SelectedIndex = Mid;
                        HttpCookie nameCookie = Request.Cookies["getdata"];
                        nameCookie.Expires = DateTime.Now.AddDays(-1);

                        //Update the Cookie in Browser.
                        Response.Cookies.Add(nameCookie);
                        int.TryParse(this.ddlEmp.SelectedValue.ToString(), out eid);
                        int.TryParse(this.ddlmonthlist.SelectedValue.ToString(), out Mid);
                        bindData(eid, Mid);
                    }
                }

            }
            catch (Exception ex)
            { Logger.WriteCriticalLog("EmployeeWiseTimeConfig_Page_Load: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
              
                ddlmonthlist.SelectedValue = MySQLDB.GetIndianTime().Month.ToString();

            }
            catch (Exception aa)
            { Logger.WriteCriticalLog("EmpLeaveConfiguration 123: exception:" + aa.Message + "::::::::" + aa.StackTrace); }

        }
        public DataTable getAllEmployee()
        {
            DataTable dtEmp = new DataTable();
            try
            {
                objmysqldb.ConnectToDatabase();
                dtEmp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and employee_master.FP_Id>0 order by  empid");
                if (dtEmp != null && dtEmp.Rows.Count > 0)
                {
                    ddlEmp.DataTextField = "FullName";
                    ddlEmp.DataValueField = "empid";
                    ddlEmp.DataSource = dtEmp;
                    ddlEmp.DataBind();
                    ddlEmp.Items.Insert(0, new ListItem("Select  Employee", "-1"));
                }
            }
            catch (Exception Ex)
            {
                //Exception exception = varAK0;
                Logger.WriteCriticalLog("getEmployee 90: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
            return dtEmp;
        }
        protected void ddlmonthlist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public void bindData(int emp_id, int month)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                int Year = 0;
                int.TryParse(DateTime.Today.Year.ToString(), out Year);
                DateTime startOfMonth = new DateTime(Year, month, 1);
                DateTime endOfMonth = new DateTime(Year, month, DateTime.DaysInMonth(Year, month));
                long startdtTicks = startOfMonth.Ticks;
                long enddtTicks = endOfMonth.Ticks;
                int Totaldays = DateTime.DaysInMonth(Year, month);
                DataTable dtEmplist = objmysqldb.GetData("SELECT Dateticks,   emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min FROM employee_management.employeewise_punchtime_details_datewise where emp_id=" + emp_id + " AND Dateticks >= " + startdtTicks + "  AND Dateticks <= " + enddtTicks + "");
                dtEmplist.Columns.Add("Date");
                dtEmplist.Columns.Add("Intime");
                dtEmplist.Columns.Add("OutTime");
                dtEmplist.Columns.Add("ExtremeEarly");
                dtEmplist.Columns.Add("ExtremeLate");
                DataTable dtList = new DataTable();
                dtList = dtEmplist.Clone();
                for (int d = 1; d <= Totaldays; d++)
                {
                    string FinalDate = d + "-" + month + "-" + Year;
                    DataRow dr = dtList.NewRow();
                    dr["Date"] = FinalDate;
                    dtList.Rows.Add(dr);

                }
                if (dtEmplist != null && dtEmplist.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmplist.Rows.Count; i++)
                    {
                        if (dtEmplist.Rows[i]["Dateticks"].ToString() != "" && dtEmplist.Rows[i]["Dateticks"].ToString() != null)
                        {
                            long Ticks = long.Parse(dtEmplist.Rows[i]["Dateticks"].ToString());
                            if (Ticks > 0)
                            {
                                DateTime dateTime3 = new DateTime(Ticks);
                                string value2 = string.Format("{0:dd-MM-yyyy}", dateTime3);
                                dtEmplist.Rows[i]["Date"] = value2;
                            }
                        }
                    }
                }
                IEnumerator enumerator = dtEmplist.Rows.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        DataRow dataRow = (DataRow)enumerator.Current;
                        long Ticks = long.Parse(dataRow["Dateticks"].ToString());
                        DataRow[] arr_ = dtEmplist.Select("Dateticks in('" + Ticks + "')");
                        if (arr_.Length > 0)
                        {

                            dataRow["Intime"] = arr_[0]["In_hour"] + ":" + arr_[0]["In_min"];
                            dataRow["OutTime"] = arr_[0]["out_hour"] + ":" + arr_[0]["out_min"];
                            dataRow["ExtremeEarly"] = arr_[0]["Ext_Early_hour"] + ":" + arr_[0]["Ext_Early_min"];
                            dataRow["ExtremeLate"] = arr_[0]["Ext_Late_hour"] + ":" + arr_[0]["Ext_Late_min"];
                           
                        }
                    }
                }
                finally
                {
                    objmysqldb.disposeConnectionObj();
                }
                
                if (dtEmplist != null && dtEmplist.Rows.Count > 0)
                {
                    divText.Visible = true;
                    grdData.DataSource = dtEmplist;
                    grdData.DataBind();
                    btnSave.Visible = true;
                }
                DataTable dtNew = new DataTable();
                dtNew = dtList.Clone();
                enumerator = dtList.Rows.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        DataRow dataRow = (DataRow)enumerator.Current;
                        string Date = dataRow["Date"].ToString();
                        if (Date != null && Date != "")
                        {
                            DateTime dateTime4 = Convert.ToDateTime(Date);
                            long ticks3 = dateTime4.Ticks;
                            if (ticks3 > 0)
                            {
                                DataRow[] arr_ = dtEmplist.Select("Dateticks  in('" + ticks3 + "')");
                                if (arr_.Length <= 0)
                                {
                                    dtNew.ImportRow(dataRow);
                                }
                            }
                        }
                    }
                }
                catch(Exception Ex)
                {
                    Logger.WriteCriticalLog("getEmployee 90: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
                    
                }

                // DataTable dataTable3 = new DataTable();
                dtNew.Columns.Remove("Intime");
                dtNew.Columns.Remove("OutTime");
                dtNew.Columns.Remove("ExtremeEarly");
                dtNew.Columns.Remove("ExtremeLate");
                dtNew.Columns.Remove("emp_id");
                dtNew.Columns.Remove("Day_id");
                dtNew.Columns.Remove("In_hour");
                dtNew.Columns.Remove("In_min");
                dtNew.Columns.Remove("out_hour");
                dtNew.Columns.Remove("out_min");
                dtNew.Columns.Remove("Ext_Early_hour");
                dtNew.Columns.Remove("Ext_Early_min");
                dtNew.Columns.Remove("Ext_Late_hour");
                dtNew.Columns.Remove("Ext_Late_min");
                dtNew.Columns.Remove("Dateticks");
                //DataTable dataTable4 = new DataTable();
                // dataTable4.Columns.Add(dataTable2.Columns[0].ColumnName);
                var tblPivot = new DataTable();
                tblPivot.Columns.Add(dtNew.Columns[0].ColumnName);
                for (int i = 1; i < dtNew.Rows.Count; i++)
                {

                    tblPivot.Columns.Add(Convert.ToString(i));
                }
                for (int col = 0; col < dtNew.Columns.Count; col++)
                {
                    var r = tblPivot.NewRow();
                    r[0] = dtNew.Columns[col].ToString();
                    for (int j = 0; j < dtNew.Rows.Count; j++)
                        r[j] = dtNew.Rows[j][col];

                    tblPivot.Rows.Add(r);
                }
                DataTable dt1 = tblPivot.Clone();
                
                if (tblPivot != null && tblPivot.Rows.Count > 0)
                {                    //Response.Write("");
                   
                    count = tblPivot.Columns.Count;
                    if (count > 1)
                    {
                        GrdAttenConfig.Visible = true;
                        divText.Visible = true;
                        GrdAttenConfig.DataSource = tblPivot;
                        GrdAttenConfig.DataBind();
                    }
                    else
                    {
                        divText.Visible = false;
                        GrdAttenConfig.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                // Exception exception = varVVW0;
                Logger.WriteCriticalLog("getEmployee 90: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
            }
            finally
            {
                //objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int month = 0;
            int emp_id = 0;
            try
            {
                int.TryParse(this.ddlEmp.SelectedValue.ToString(), out emp_id);
                int.TryParse(this.ddlmonthlist.SelectedValue.ToString(), out month);
                HttpCookie cookie = new HttpCookie("getdata");
                cookie["empid"] = ddlEmp.SelectedIndex.ToString();
                cookie["month"] = ddlmonthlist.SelectedIndex.ToString();
                cookie.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(cookie);
                bindData(emp_id, month);

            }
            catch (Exception exception)
            {
                
                Logger.WriteCriticalLog("btnSearchClick 194: exception:" + exception.Message + "::::::::" + exception.StackTrace);
            }
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                }
            }
            catch (Exception Ex)
            {
                Logger.WriteCriticalLog("EmpWiseTimeConfig 215: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
               objmysqldb.OpenSQlConnection();
                int yes = 0;
                if (this.ddlEmp.SelectedIndex > 0)
                {
                    string emp_id = this.ddlEmp.SelectedValue.ToString();
                    DataTable data = this.objmysqldb.GetData("SELECT Dateticks,   emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Early_min,Ext_Late_hour,Ext_Late_min FROM employee_management.employeewise_punchtime_details_datewise where emp_id=" + int.Parse(emp_id) + " ");
                    if (data != null && data.Rows.Count > 0)
                    {
                        IEnumerator enumerator = this.grdData.Rows.GetEnumerator();
                        try
                        {
                            while (enumerator.MoveNext())
                            {
                                GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                                Label lbldate = (Label)gridViewRow.FindControl("lblDate");
                                Label dayid = (Label)gridViewRow.FindControl("lbldayid");
                                TextBox txtintime = (TextBox)gridViewRow.FindControl("txtIntime");
                                TextBox txtouttime = (TextBox)gridViewRow.FindControl("txtoutTime");
                                TextBox txtearly = (TextBox)gridViewRow.FindControl("txtearly");
                                TextBox txtlate = (TextBox)gridViewRow.FindControl("txtlate");
                                string[] arr_ = txtintime.Text.ToString().Split(new char[]
                                            {
                                                 ':'
                                            }, StringSplitOptions.RemoveEmptyEntries);
                                string[] arr_2 = txtouttime.Text.ToString().Split(new char[]
                                            {
                                                 ':'
                                            }, StringSplitOptions.RemoveEmptyEntries);
                                string[] arr_3 = txtearly.Text.ToString().Split(new char[]
                                            {
                                                 ':'
                                            }, StringSplitOptions.RemoveEmptyEntries);
                                string[] arr_4 = txtlate.Text.ToString().Split(new char[]
                                            {
                                                 ':'
                                            }, StringSplitOptions.RemoveEmptyEntries);
                                int day = 0;
                                int.TryParse(dayid.Text.ToString(), out day);
                                if (day == 0)
                                {
                                    day = 7;
                                }
                                int inmin = 0;
                                int inhour = 0;
                                int outmin = 0;
                                int outhour = 0;
                                int earlymin = 0;
                                int earlyhour = 0;
                                int latemin = 0;
                                int latehour = 0;
                                int.TryParse(arr_[0], out inmin);
                                int.TryParse(arr_[1], out inhour);
                                int.TryParse(arr_2[0], out outmin);
                                int.TryParse(arr_2[1], out outhour);
                                int.TryParse(arr_3[0], out earlymin);
                                int.TryParse(arr_3[1], out earlyhour);
                                int.TryParse(arr_4[0], out latemin);
                                int.TryParse(arr_4[1], out latehour);
                                DateTime indiantimeDT = Logger.getIndiantimeDT();
                                if (inmin > 23 || inhour > 59 || outmin > 23 || outhour > 59 || earlymin > 23 || earlyhour > 59 || latemin > 23 || latehour > 59)
                                {
                                    this.ltrErr.Text = "Please enter valid time ";
                                    return;
                                }                         
                                DateTime dateTime = Convert.ToDateTime(lbldate.Text.ToString());
                                long ticks = dateTime.Ticks;

                                DataRow[] dr = data.Select("emp_id=" + emp_id + "and Day_id=" + day + " and DateTicks in ('" +ticks+"')" );
                               if (dr.Length > 0)
                                {
                                    string in_hour = dr[0]["In_hour"].ToString();
                                    string in_min = dr[0]["In_min"].ToString();
                                    string out_hour = dr[0]["out_hour"].ToString();
                                    string out_min = dr[0]["out_min"].ToString();
                                    string early_hour = dr[0]["Ext_Early_hour"].ToString();
                                    string early_min = dr[0]["Ext_Early_min"].ToString();
                                    string late_hour = dr[0]["Ext_Late_hour"].ToString();
                                    string late_min = dr[0]["Ext_Late_min"].ToString();
                                    if (!in_hour.Equals("inhour") || !in_min.Equals("inmin") || !out_hour.Equals("outhour") || !out_min.Equals("outmin") || !early_hour.Equals("earlyhour") || !early_min.Equals("earlymin") || !late_hour.Equals("latehour") || !late_min.Equals("latemin"))
                                    {

                                        yes = objmysqldb.InsertUpdateDeleteData("update employeewise_punchtime_details_datewise set In_hour=" + inhour + ",In_min=" + inmin + ",out_hour=" + outhour + ",out_min=" + outmin + " ,Ext_Early_hour=" + earlyhour + ",Ext_Early_min=" + earlymin + ",Ext_Late_hour=" + latehour + ",Ext_Late_min=" + latemin + ",modify_datetime=" + MySQLDB.GetIndianTime().Ticks.ToString() + ",IsUpdate=1, UserID= " + user_id + "  where emp_id=" + emp_id + " and Day_id=" + day + " and DateTicks=" + ticks + "");

                                    }
                                }
                                else
                                {
                                    DateTime dateTime2 = new DateTime(ticks);
                                    int dayOfWeek = (int)dateTime2.DayOfWeek;
                                
                                    yes = objmysqldb.InsertUpdateDeleteData("insert into employeewise_punchtime_details_datewise(Dateticks,emp_id,Day_id,In_hour,In_min,out_hour,out_min,Ext_Early_hour,Ext_Late_hour,Ext_Late_min,Ext_Early_min,modify_datetime,IsDelete,DOC,IsUpdate,UserID)Values(" + ticks + "," + emp_id + "," + dayid + "," + inhour + "," + inmin + "," + outhour + "," + outmin + "," + earlyhour + "," + latehour + "," + latemin + "," + earlymin + "," + MySQLDB.GetIndianTime().Ticks.ToString() + ",0," + MySQLDB.GetIndianTime().Ticks.ToString() + ",1," + user_id + ")");

                                }
                            }
                        }
                        finally
                        {
                            objmysqldb.CloseSQlConnection();
                            objmysqldb.disposeConnectionObj();
                        }
                        if (yes > 0)
                        {
                            this.ltrErr.Text = "Time Detail Saved...";
                        }
                        else
                        {
                            this.ltrErr.Text = "Error...";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //Exception exception = varVVJ0;
                Logger.WriteCriticalLog("EmpWiseTimeConfig 307: exception:" + exception.Message + "::::::::" + exception.StackTrace);
            }
            
        }
        
        protected void GrdAttenConfig_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // GridViewRow row = GrdAttenConfig.SelectedRow;
                //Response.Redirect();
                int id=0;
                int.TryParse(ddlEmp.SelectedValue.ToString(),out id);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    for (int i = 1; i <= count; i++)
                    {
                        HyperLink hyperLink = new HyperLink();
                        string date = e.Row.Cells[i].Text;
                        hyperLink.Text = date;
                        e.Row.Cells[i].Controls.Add(hyperLink);
                       // Session["date"] = date;
                        hyperLink.NavigateUrl = "~/TimeConfigForEmp.aspx?Date=" + date+"&Emp_id="+id;
                        //Response.Redirect("~/TimeConfigForEmp.aspx?Date=" + date + "&Emp_id=" + id);
                    }
                }
            }
            catch (Exception Ex)
            {
                Logger.WriteCriticalLog("EmpWiseTimeConfig 497: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
            }
        } 
    }
}
