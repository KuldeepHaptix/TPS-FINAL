using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Data;

namespace EmployeeManagement
{
    public partial class Manage_Category : System.Web.UI.Page
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
                        header.Text = "Manage Category";
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
                    txtcategorynames.Focus();
                    showgrid();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Category 50: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtcategory = objmysqldb.GetData("Select category_id,category_name,IsMasterData from category_master where IsDelete=0 order by category_id");
                objmysqldb.disposeConnectionObj();
                if (dtcategory != null)
                {
                    grd.DataSource = dtcategory;
                    grd.DataBind();
                    btnExport.Visible = true;

                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Category 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (!txtcategorynames.Text.ToString().Equals(""))
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtcategory = objmysqldb.GetData("Select * from category_master WHERE (category_name like '" + txtcategorynames.Text.ToString() + "') and  IsDelete=0  ");
                    if (dtcategory != null && dtcategory.Rows.Count > 0)
                    {
                        ltrErr.Text = " Category Is Already Exist";
                        return;
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("categoryname", txtcategorynames.Text.ToString());
                    string query = "Insert into  category_master (category_name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete) values (?categoryname," + user_id + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1,1,0) ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("Manage_Category 106 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        txtcategorynames.Text = "";
                        ltrErr.Text = "Category Save Successfully.";
                        
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
                Logger.WriteCriticalLog("Manage_Category 125: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (e.CommandName == "del")
                {
                    string[] arg = e.CommandArgument.ToString().Split(':');
                    if (arg != null && arg.Length == 2)
                    {
                        int catid = 0;
                       
                        int.TryParse(arg[1], out catid);
                        objmysqldb.ConnectToDatabase();
                        DataTable dtcategory = objmysqldb.GetData("Select EmpCategory from employee_master  WHERE  EmpCategory =" + catid + "  ");
                        DataTable dt = objmysqldb.GetData("select * from category_master where category_id=" + catid + "");
                        if (dtcategory != null && dtcategory.Rows.Count > 0)
                        {

                            ltrErr.Text = "Employee Assign to selected Category so you can't delete it.";
                            return;
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            
                                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                                DateTime currenttime = Logger.getIndiantimeDT();
                                string query = "Update category_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where category_id=" + int.Parse(arg[0]) + " ";
                                objmysqldb.OpenSQlConnection();
                                int res = objmysqldb.InsertUpdateDeleteData(query);
                                if (res != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";

                                    Logger.WriteCriticalLog("Manage_Category 168 Update error.");
                                }
                                else
                                {
                                    grd.EditIndex = -1;
                                    showgrid();
                                    ltrErr.Text = "Category Deleted Successfully.";
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
                Logger.WriteCriticalLog("Manage_Category 192: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void grd_RowDataBound(object sender,GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                { }

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
                Logger.WriteCriticalLog("Manage_Category 224: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }
        protected void grd_RowUpdating(object sender,GridViewUpdateEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                int masterdata=0;
                TextBox txtcategorynames = (TextBox)grd.Rows[e.RowIndex].FindControl("txtcategory_name");
               // TextBox txtcategory_id = (TextBox)grd.Rows[e.RowIndex].FindControl("txtcategory_id");
                Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lblcategory_id");
                string catListId = lbid.Text;
                string catName = txtcategorynames.Text.ToString().Trim();
                int catid = 0;
                int.TryParse(catListId, out catid);
                DateTime currenttime = Logger.getIndiantimeDT();
                if (catName != "" && catid > 0)
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtcategory = objmysqldb.GetData("Select * from category_master WHERE IsDelete=0 and category_name like '" + catName + "' and category_id<>" + int.Parse(catListId) + "  ");

                    DataTable dtcatDetails = objmysqldb.GetData("Select category_id,IsMasterData from category_master WHERE category_id= " + int.Parse(catListId) + "  ");

                    
                    if (dtcategory != null && dtcategory.Rows.Count > 0)
                    {
                        ltrErr.Text = "Same Category Name Is Already Exist.";
                        return;
                    }
                    if (dtcatDetails != null && dtcatDetails.Rows.Count > 0)
                    {              

                            objmysqldb.AddCommandParameter("categoryname", catName);
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            string query = "Update category_master set category_name=?categoryname,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + "  where category_id=" + int.Parse(catListId) + " ";
                            objmysqldb.OpenSQlConnection();
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("Manage_Category 251 Update error.");
                                return;
                            }
                            grd.EditIndex = -1;
                            showgrid();
                            ltrErr.Text = "Category Name Updated Successfully.";                     
                    }
                    else
                    {
                        ltrErr.Text = "Please Try Again.";
                        return;
                    }

                }
                else
                {
                    //showgrid();
                    ltrErr.Text = "Please Enter All Details.";

                }

                grd.EditIndex = -1;

            }
            catch (Exception ex)
            {
                ltrErr.Text = "Please Try Again.";
                Logger.WriteCriticalLog("Manage_Category 294: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowEditing(object sender,GridViewEditEventArgs e)
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
                Logger.WriteCriticalLog("Manage_Category 316: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Manage_Category 328: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtcate = objmysqldb.GetData("Select category_id,category_name,IsMasterData from category_master where IsDelete=0 order by category_id");
                objmysqldb.disposeConnectionObj();
                if (dtcate != null && dtcate.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtcate, "List_Of_Category");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Category 353: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

       
    }
}