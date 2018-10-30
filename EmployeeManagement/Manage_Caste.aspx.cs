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
    public partial class Manage_Caste : System.Web.UI.Page
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
                        header.Text = "Manage Caste";
                        
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
                    txtcstnames.Focus();
                    showgrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Caste 53: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtcasteList = objmysqldb.GetData("select caste_id,caste_name,IsMasterData from caste_master WHERE IsDelete=0 order by caste_id");
                objmysqldb.disposeConnectionObj();
                if (dtcasteList != null)
                {
                    grd.DataSource = dtcasteList;
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
                Logger.WriteCriticalLog("Manage_Caste 79: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (!txtcstnames.Text.ToString().Equals(""))
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtcasteList = objmysqldb.GetData("Select * from caste_master WHERE IsDelete=0 and (caste_name like '" + txtcstnames.Text.ToString() + "')");
                    if (dtcasteList != null && dtcasteList.Rows.Count > 0)
                    {
                        ltrErr.Text = " Caste  Is Already Exist";
                        return;
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("cstname", txtcstnames.Text.ToString());
                    string query = "Insert into  caste_master (caste_name,DOC,UserID,modify_datetime,IsUpdate,IsMasterData,IsDelete) values (?cstname, " + currenttime.Ticks + "," + user_id + " ," + currenttime.Ticks + ",1,1,0) ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("Manage_Caste 107 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Caste Save Successfully";
                        txtcstnames.Text = "";
                    }
                }
                else
                {
                    ltrErr.Text = "Please Enter All Details.";
                    return;
                }
                txtcstnames.Text = "";
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Caste 126: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (((DataRowView)e.Row.DataItem)["IsMasterData"].ToString() == "0")
                    {
                        e.Row.Enabled = false;
                        var linkButton = e.Row.FindControl("lnkdelete") as LinkButton;
                        linkButton.Enabled = false;
                        linkButton.OnClientClick = "";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Caste 160: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Manage_Caste 172: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                        int cstid = 0;
                        int.TryParse(arg[1], out cstid);
                        
                        objmysqldb.ConnectToDatabase();
                        DataTable dtcasteList = objmysqldb.GetData("Select EmpCaste from employee_master  WHERE  EmpCaste=" + cstid + "  ");
                        DataTable dtcaste = objmysqldb.GetData("Select * from caste_master where caste_id=" + cstid + "");
                        if (dtcasteList != null && dtcasteList.Rows.Count > 0)
                        {
                            ltrErr.Text = "Employee Assign to select Caste so you can't delete it.";
                            return;
                        }
                        if (dtcaste != null && dtcaste.Rows.Count > 0)
                        {
                            
                                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out   user_id);
                                DateTime currenttime = Logger.getIndiantimeDT();
                                string query = "Update caste_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where caste_id=" + int.Parse(arg[0]) + " ";
                                objmysqldb.OpenSQlConnection();
                                int res = objmysqldb.InsertUpdateDeleteData(query);
                                if (res != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";

                                    Logger.WriteCriticalLog("Manage_Caste 208 Update error.");
                                }
                                else
                                {
                                    grd.EditIndex = -1;
                                    showgrid();
                                    ltrErr.Text = "Caste Deleted Successfully.";
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
                Logger.WriteCriticalLog("Manage_Caste 233: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Manage_Caste 252: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                TextBox txtcstname = (TextBox)grd.Rows[e.RowIndex].FindControl("txtCaste_Name");
                //TextBox txtschid = (TextBox)grd.Rows[e.RowIndex].FindControl("txtSchool_Id");
                Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lblcaste_id");
                string casteid = lbid.Text;
                string castename = txtcstname.Text.ToString().Trim();
                int schid = 0;
                //int.TryParse(txtschid.Text.ToString().Trim(), out schid);
                DateTime currenttime = Logger.getIndiantimeDT();
                if (castename != "")
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtcastelist = objmysqldb.GetData("Select * from caste_master WHERE IsDelete=0 and caste_name like '" + castename + "' and caste_id <> " + int.Parse(casteid) + "");

                    DataTable dtschDetails = objmysqldb.GetData("Select caste_id,IsMasterData from caste_master WHERE caste_id= " + int.Parse(casteid) + "  ");
                    if (dtcastelist != null && dtcastelist.Rows.Count > 0)
                    {
                        ltrErr.Text = "Same Caste Name Is Already Exist.";
                        return;
                    }
                    
                    if (dtschDetails != null && dtschDetails.Rows.Count > 0)
                    {                      
                        
                        objmysqldb.AddCommandParameter("castename", castename);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        string query = "Update caste_master set caste_name=?castename,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where caste_id=" + int.Parse(casteid) + " ";
                        objmysqldb.OpenSQlConnection();
                       
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("Manage_Caste 293 Update error.");
                                return;
                            }
                            else
                            {
                                grd.EditIndex = -1;
                                showgrid();
                                ltrErr.Text = "Caste Name Updated Successfully.";
                            }
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
                Logger.WriteCriticalLog("Manage_Caste 318: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtcaste = objmysqldb.GetData("Select caste_id,caste_name,IsMasterData from caste_master where IsDelete=0 order by caste_id");
                objmysqldb.disposeConnectionObj();
                if (dtcaste != null && dtcaste.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtcaste, "List_Of_Caste");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Caste 348: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

    }
}
