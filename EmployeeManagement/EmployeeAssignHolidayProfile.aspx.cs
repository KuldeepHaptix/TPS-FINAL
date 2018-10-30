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
    public partial class EmployeeAssignHolidayProfile : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int holid = 0;
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
                        header.Text = "Assign Holiday Profile To Employee";
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

                BtnSave.Visible = false;
                Button1.Visible = false;
                if (!Page.IsPostBack)
                {
                    holidayProfile();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void holidayProfile()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtbindholiday = objmysqldb.GetData("SELECT * FROM  holiday_profile_master;");
                ddlholidaylist.DataSource = dtbindholiday;
                ddlholidaylist.DataTextField = "Holiday_Profile_Name";
                ddlholidaylist.DataValueField = "Holiday_Profile_Id";
                ddlholidaylist.DataBind();
                ddlholidaylist.Items.Insert(0, new ListItem("Select Holiday Profile", "0"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 74: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void ddlholidaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(ddlholidaylist.SelectedValue.ToString(), out holid);
                //bindgvdata(grpid);
                bindgrid(holid);
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 91: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        public void bindgrid(int holid)
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                if (holid == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ltrErr.Text = "Select Valid Holiday Profile..";
                    return;
                }
                else
                {
                    DataTable EmpDetail1 = objmysqldb.GetData("SELECT employee_master.EmpId As emp_id ,concat(EmpFirstName,' ',EmpMiddleName,'  ',EmpLastName)As FullName,if(1=1,0,1)as selects FROM employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0");
                    DataTable EmpDetail = new DataTable();

                    DataTable dtselectEmp = objmysqldb.GetData("SELECT emp_id FROM assign_holidayprofile_employee where IsDelete=0 and holiday_profile_id=" + holid + " ");
                    //if (dtselectEmp.Rows.Count > 0)
                    {
                        DataTable dtEmpnotinholidayprofid = objmysqldb.GetData("SELECT emp_id FROM assign_holidayprofile_employee where assign_holidayprofile_employee.IsDelete=0 and holiday_profile_id not in(" + holid + " )");
                        string unempid = "";
                        for (int j = 0; j < dtEmpnotinholidayprofid.Rows.Count; j++)
                        {
                            unempid += dtEmpnotinholidayprofid.Rows[j]["emp_id"].ToString() + ",";
                        }
                        unempid = unempid.TrimEnd(',');
                        //DataRow[] dremp = EmpDetail1.Select("emp_id not in(" + unempid + ")");
                        //if (dremp.Length > 0)
                        //{ EmpDetail = dremp.CopyToDataTable(); }
                        if (unempid.Equals(""))
                        { EmpDetail = objmysqldb.GetData("SELECT employee_master.EmpId As emp_id ,concat(EmpFirstName,' ',EmpMiddleName,'  ',EmpLastName)As FullName,if(1=1,0,1)as selects FROM employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0"); }
                        else
                        {
                            EmpDetail = objmysqldb.GetData("SELECT employee_master.EmpId As emp_id ,concat(EmpFirstName,' ',EmpMiddleName,'  ',EmpLastName)As FullName,if(1=1,0,1)as selects FROM employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0 and EmpId not in(" + unempid + ")");
                        }

                        for (int i = 0; i < dtselectEmp.Rows.Count; i++)
                        {
                            DataRow[] drselect = EmpDetail.Select("emp_id = " + int.Parse(dtselectEmp.Rows[i]["emp_id"].ToString()) + "");
                            foreach (DataRow dr in drselect)
                            {
                                dr["selects"] = "1";
                            }
                        }

                        if (EmpDetail != null && EmpDetail.Rows.Count > 0)
                        {
                            GridView1.DataSource = EmpDetail;
                            GridView1.DataBind();
                            BtnSave.Visible = true;
                            Button1.Visible = true;
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            BtnSave.Visible = false;
                            Button1.Visible = false;
                        }
                    }
                    //else
                    //{
                    //    GridView1.DataSource = EmpDetail1;
                    //    GridView1.DataBind();
                    //    BtnSave.Visible = true;
                    //}

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 166: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (((DataRowView)e.Row.DataItem)["selects"].ToString() == "1")
                    {
                        CheckBox chkAssign = (CheckBox)e.Row.FindControl("cbSelect");
                        chkAssign.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 194: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void saveData()
        {
            ltrErr.Text = "";
            int success = 0;
            objmysqldb.ConnectToDatabase();
            try
            {
                int.TryParse(ddlholidaylist.SelectedValue.ToString(), out holid);
                string Emp_Ids = "";
                objmysqldb.OpenSQlConnection();
                objmysqldb.BeginSQLTransaction();
                DataTable dtselectEmp = objmysqldb.GetData("SELECT emp_id FROM assign_holidayprofile_employee where IsDelete=0 and holiday_profile_id=" + holid + " ");
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox chkAssign = (CheckBox)row.FindControl("cbSelect");

                    if (chkAssign.Checked)
                    {
                        int emp_id = 0;
                        Label emp = (Label)row.FindControl("lblemp_id");
                        int.TryParse(emp.Text.ToString(), out emp_id);
                        Emp_Ids += emp.Text.ToString() + ",";
                        DateTime currentTime = Logger.getIndiantimeDT();
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        //objmysqldb.OpenSQlConnection();
                        success = objmysqldb.InsertUpdateDeleteData("update assign_holidayprofile_employee set  modify_datetime=" + currentTime.Ticks + ",IsUpdate=0,IsDelete=0,UserID=" + user_id + " where holiday_profile_id=" + holid + " and emp_id=" + emp_id + "");
                        if (success == 0)
                        {
                            success = objmysqldb.InsertUpdateDeleteData("INSERT INTO assign_holidayprofile_employee (emp_id,holiday_profile_id, DOC, IsUpdate,IsDelete,modify_datetime,UserID) VALUES (" + emp_id + ", " + holid + "," + currentTime.Ticks + ", '1', '0'," + currentTime.Ticks + "," + user_id + ");");

                            if (success == -1)
                            {
                                ltrErr.Text = "Error While Updating..";
                                objmysqldb.RollBackSQLTransaction();
                                return;

                            }
                        }
                    }
                }
                Emp_Ids = Emp_Ids.TrimEnd(',');
                string[] arrempid = Emp_Ids.Split(',');
                if (!Emp_Ids.Equals("") && dtselectEmp != null && dtselectEmp.Rows.Count > 0)
                {
                    DataRow[] drdata = dtselectEmp.Select("emp_id NOt IN (" + Emp_Ids + ")");
                    dtselectEmp = dtselectEmp.Clone();
                    if (drdata.Length > 0)
                    {
                        dtselectEmp = drdata.CopyToDataTable();
                    }
                }
                foreach (DataRow dr in dtselectEmp.Rows)
                {
                    DateTime currentTime = Logger.getIndiantimeDT();
                    success = objmysqldb.InsertUpdateDeleteData("update assign_holidayprofile_employee set  modify_datetime=" + currentTime.Ticks + ",IsUpdate=1,IsDelete=1,UserID=" + user_id + " where holiday_profile_id=" + holid + " and emp_id=" + +int.Parse(dr["emp_id"].ToString()) + "");
                }
                if (success == 1)
                {
                    ltrErr.Text = "Holiday Profile Assigned Successfully...";
                    objmysqldb.EndSQLTransaction();
                    bindgrid(holid);
                }
            }
            catch (Exception ex)
            {
                objmysqldb.RollBackSQLTransaction();
                Logger.WriteCriticalLog("EmployeeAssignHolidayProfile 264: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();

            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            saveData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            saveData();
        }
    }
}