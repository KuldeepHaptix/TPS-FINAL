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
    public partial class Manage_Organization1 : System.Web.UI.Page
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
                    txtschnames.Focus();
                    showgrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 52: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtschList = objmysqldb.GetData("Select School_List_ID,School_Name,School_Id from school_list where IsDelete=0 order by School_List_ID");
                objmysqldb.disposeConnectionObj();
                if (dtschList != null)
                {
                    string schid = "";
                    for (int i = 0; i < dtschList.Rows.Count; i++)
                    {
                        schid += dtschList.Rows[i]["School_Id"].ToString() + " - " + dtschList.Rows[i]["School_Name"].ToString()+",";
                    }
                    Application["schId"] = schid.TrimEnd(',');
                    grd.DataSource = dtschList;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 84: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (!txtschID.Text.ToString().Equals("") && !txtschnames.Text.ToString().Equals(""))
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtschList = objmysqldb.GetData("Select * from school_list WHERE IsDelete=0 and (School_Name like '" + txtschnames.Text.ToString() + "' OR School_Id = " + int.Parse(txtschID.Text.ToString()) + ")  ");
                    if (dtschList != null && dtschList.Rows.Count > 0)
                    {
                        ltrErr.Text = " Organization  Is Already Exist";
                        return;
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("schanme", txtschnames.Text.ToString());
                    string query = "Insert into  school_list (School_Name,School_Id,DOC,UserID,IsDelete,IsUpdate,modify_datetime) values (?schanme," + int.Parse(txtschID.Text.ToString()) + "," + currenttime.Ticks + "," + user_id + ",0,1," + currenttime.Ticks + ") ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("Manage_Organization 110 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Organization Save Successfully";
                        txtschID.Text = "";
                        txtschnames.Text = "";
                    }
                }
                else
                {
                    ltrErr.Text = "Please Enter All Details.";
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 130: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (e.CommandName == "del")
                {
                    string[] arg = e.CommandArgument.ToString().Split(':');
                    if (arg != null && arg.Length == 2)
                    {
                        int schid = 0;
                        int.TryParse(arg[1], out schid);
                        objmysqldb.ConnectToDatabase();
                        DataTable dtschList = objmysqldb.GetData("Select Employee_ID from employee_assign_schoolwise  WHERE  School_ID =" + schid + "  ");

                        if (dtschList != null && dtschList.Rows.Count > 0)
                        {
                            ltrErr.Text = "Employee Assign to select organization so can't delete it";
                            return;
                        }
                        else
                        {
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            DateTime currenttime = Logger.getIndiantimeDT();
                            string query = "Update school_list set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where School_List_ID=" + int.Parse(arg[0]) + " ";
                            objmysqldb.OpenSQlConnection();
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";
                                Logger.WriteCriticalLog("Manage_Organization 168 Update error.");
                            }
                            else
                            {
                                grd.EditIndex = -1;
                                showgrid();
                                ltrErr.Text = "Organization Delete Successfully";
                            }
                        }
                    }
                    else
                    {
                        showgrid();
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
                Logger.WriteCriticalLog("Manage_Organization 192: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
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
                Logger.WriteCriticalLog("Manage_Organization 215: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                TextBox txtschname = (TextBox)grd.Rows[e.RowIndex].FindControl("txtSchool_Name");
                TextBox txtschid = (TextBox)grd.Rows[e.RowIndex].FindControl("txtSchool_Id");
                Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lblSchool_List_ID");
                string schListId = lbid.Text;
                string schName = txtschname.Text.ToString().Trim();
                int schid = 0;
                int.TryParse(txtschid.Text.ToString().Trim(), out schid);
                DateTime currenttime = Logger.getIndiantimeDT();
                if (schName != "" && schid > 0)
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtschList = objmysqldb.GetData("Select * from school_list WHERE IsDelete=0 and School_List_ID <> " + int.Parse(schListId) + " and School_Id=" + schid + "  ");
                    DataTable dtschool = objmysqldb.GetData("Select * from School_list where IsDelete=0 and (School_Name like '" + schName + "') and School_List_ID <> " + int.Parse(schListId) + " and School_Id<>" + schid + "");
                    DataTable dtschDetails = objmysqldb.GetData("Select School_List_ID,School_Id from school_list WHERE School_List_ID= " + int.Parse(schListId) + "  ");
                    if (dtschList != null && dtschList.Rows.Count > 0 || dtschool != null && dtschool.Rows.Count > 0)
                    {
                        ltrErr.Text = "Same Organization Is Already Exist";
                        return;
                    }
                    int oldschid = 0;
                    if (dtschDetails != null && dtschDetails.Rows.Count > 0)
                    {
                        int.TryParse(dtschDetails.Rows[0]["School_Id"].ToString(), out oldschid);
                        objmysqldb.AddCommandParameter("schanme", schName);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        string query = "Update school_list set School_Name=?schanme,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + ",School_Id=" + schid + " where School_List_ID=" + int.Parse(schListId) + " ";
                        objmysqldb.OpenSQlConnection();
                        int res = objmysqldb.InsertUpdateDeleteData(query);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";
                            Logger.WriteCriticalLog("Manage_Organization 248 Update error.");
                            return;
                        }
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Organization Name Updated Successfully";
                    }
                    else
                    {
                        ltrErr.Text = "Please Try Again.";
                        return;
                    }
                }
                else
                {
                    ltrErr.Text = "Please Enter All Details.";
                }
                grd.EditIndex = -1;
            }
            catch (Exception ex)
            {
                ltrErr.Text = "Please Try Again.";
                Logger.WriteCriticalLog("Manage_Organization 276: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string command = e.ToString();
                grd.EditIndex = e.NewEditIndex;
                showgrid();
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 295: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grd.EditIndex = -1;
                showgrid();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 307: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtorganization = objmysqldb.GetData("Select School_List_ID,School_Name,School_Id from school_list where IsDelete=0 order by School_List_ID");
                objmysqldb.disposeConnectionObj();
                if (dtorganization != null && dtorganization.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtorganization, "List_Of_Organization");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Organization 331: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
    }
}