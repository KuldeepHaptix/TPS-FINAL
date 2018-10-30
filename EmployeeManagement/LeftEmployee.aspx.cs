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
using System.Drawing;

namespace EmployeeManagement
{
    public partial class LeftEmployee : System.Web.UI.Page
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
                        header.Text = "Manage Organization";
                       
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
                    showgrid();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("LeftEmployee 53:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                if (txtleftDate.Text.ToString().Equals(""))
                {
                    ltrErr.Text = "Please enter left date";
                    return;
                }
                else
                {
                    string[] arrlft = txtleftDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrlft.Length == 3)
                    {
                        string lftdate = ((arrlft[0]).ToString().Length == 1 ? "0" + (arrlft[0]).ToString() : (arrlft[0]).ToString()) + "/" + ((arrlft[1]).ToString().Length == 1 ? "0" + (arrlft[1]).ToString() : (arrlft[1]).ToString()) + "/" + ((arrlft[2]).ToString());
                        txtleftDate.Text = lftdate;
                    }
                    else
                    {
                        ltrErr.Text = "Left Date is not in proper format.";
                        return;
                    }
                }
                int emp_id = 0;
                int.TryParse(Request.QueryString["Emp"].ToString(), out emp_id);
                int user_ids = 0;
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_ids);
                DateTime currenttime = Logger.getIndiantimeDT();
                string query = "Update employee_master set EmpStatusFlag=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_ids + " where EmpId=" + emp_id + " ";
                objmysqldb.OpenSQlConnection();
                int res = objmysqldb.InsertUpdateDeleteData(query);
                if (res == 1)
                {
                    objmysqldb.AddCommandParameter("Left_Date", txtleftDate.Text.ToString());
                    objmysqldb.AddCommandParameter("Left_Reason", txtreason.Text.ToString());
                    res = objmysqldb.InsertUpdateDeleteData("insert into left_employee (Emp_Id,Left_Date,Left_Reason,UserID,modify_datetime,DOC,IsUpdate,IsDelete) values(" + emp_id + ",?Left_Date,?Left_Reason," + user_ids + " ," + currenttime.Ticks + "," + currenttime.Ticks + ",1,0) ");

                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";
                        Logger.WriteCriticalLog("LeftEmployee 98 Update error.");

                    }
                    else
                    {
                        ltrErr.Text = "Status Update Successfully";
                        txtleftDate.Text = "";
                        txtreason.Text = "";
                    }
                    grd.EditIndex = -1;
                    showgrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("LeftEmployee 113:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }

        }

        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtleftData = objmysqldb.GetData("Select concat(if(EmpFirstName is null,'',EmpFirstName),' ' ,if(EmpLastName is null,'',EmpLastName)) AS EmpFullName,Left_Employee_id,Emp_Id,Left_Date,Left_Reason from left_employee inner join employee_master on left_employee.Emp_Id=employee_master.EmpId  where left_employee.IsDelete=0 order by Left_Employee_id");
                objmysqldb.disposeConnectionObj();
                if (dtleftData != null)
                {
                    grd.DataSource = dtleftData;
                    grd.DataBind();
                    btnExport.Visible = true;

                }
                else
                {
                    //ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("LeftEmployee 148: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                if (e.CommandName == "status")
                {
                    string[] arg = e.CommandArgument.ToString().Split(':');
                    if (arg != null && arg.Length == 2)
                    {
                        int emp_id = 0;
                        int.TryParse(arg[1].ToString(), out emp_id);
                        int rowid = 0;
                        int.TryParse(arg[0].ToString(), out rowid);

                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        DateTime currenttime = Logger.getIndiantimeDT();
                        string query = "Update employee_master set EmpStatusFlag=0,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where EmpId=" + emp_id + " ";
                        objmysqldb.OpenSQlConnection();
                        int res = objmysqldb.InsertUpdateDeleteData(query);

                        if (res == 1)
                        {
                            res = objmysqldb.InsertUpdateDeleteData("Update left_employee set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where Left_Employee_id=" + rowid + "  ");

                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";
                                Logger.WriteCriticalLog("LeftEmployee 181 Update error.");
                                return;
                            }
                            else
                            {
                                ltrErr.Text = "Status Update Successfully";
                            }
                            grd.EditIndex = -1;
                            showgrid();

                        }
                    }
                    else
                    {
                        //showgrid();
                        ltrErr.Text = "Please Try Again.";
                    }
                    grd.EditIndex = -1;
                }
                else if (e.CommandName == "Edit")
                {

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("LeftEmployee 207: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {

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
                { }
            }
            catch (Exception aa)
            {

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtleft = objmysqldb.GetData("Select concat(if(EmpFirstName is null,'',EmpFirstName),' ' ,if(EmpLastName is null,'',EmpLastName)) AS EmpFullName,Left_Employee_id,Emp_Id,Left_Date,Left_Reason from left_employee inner join employee_master on left_employee.Emp_Id=employee_master.EmpId  where left_employee.IsDelete=0 order by Left_Employee_id");
                objmysqldb.disposeConnectionObj();
                if (dtleft != null && dtleft.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtleft, "List_Of_LeftEmployee");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("LeftEmployee 259: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
       
    }
}