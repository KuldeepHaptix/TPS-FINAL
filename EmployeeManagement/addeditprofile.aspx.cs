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
    public partial class addeditprofile : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int profileid = 0;
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
                        header.Text = "Manage Query Profile";// "Add/Edit Profile";
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
                Logger.WriteCriticalLog("addeditprofile 50: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }


        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";

                DataTable dtempprofile = objmysqldb.GetData("Select * from employee_query_profile where IsDelete=0 order  by Profile_Id");
                if (dtempprofile != null && dtempprofile.Rows.Count > 0)
                {
                    grd.DataSource = dtempprofile;
                    grd.DataBind();

                }
                else
                {
                    ltrErr.Text = "No Data Found.";

                    grd.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addeditprofile 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
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

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addeditprofile 100: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                if (e.CommandName == "Edit")
                {
                    int.TryParse(e.CommandArgument.ToString(), out profileid);
                    if (profileid > 0)
                    {
                        Response.Redirect("~/addnewqueryprofile.aspx?profile=" + profileid + "", false);
                    }
                }
                else if (e.CommandName == "del")
                {

                    int.TryParse(e.CommandArgument.ToString(), out profileid);
                    DateTime curtime= Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    string query = "update employee_query_profile set IsDelete=1,modify_datetime=" + curtime.Ticks + ",IsUpdate=1,,UserID=" + user_id + " where Profile_Id=" + profileid + "";
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("addeditprofile 129 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Profile Deleted Successfully.";
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addeditprofile 142: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
        protected void btnadd_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/addnewqueryprofile.aspx?profile=0");
        }
    }
}