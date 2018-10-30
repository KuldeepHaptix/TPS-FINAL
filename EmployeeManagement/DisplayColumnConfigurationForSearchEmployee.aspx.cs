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
    public partial class DisplayColumnConfigurationForSearchEmployee : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int count = 0;
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
                        header.Text = "DisplayColumnConfigurationForSearchEmployee";
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
                count = grd.Rows.Count;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtfieldlist = objmysqldb.GetData("Select Employee_Field_id,FieldName,FieldDisplayName,Is_Show,SortNumber from employee_search_field_config order by Employee_Field_id");
                objmysqldb.disposeConnectionObj();
                if (dtfieldlist != null)
                {
                    grd.DataSource = dtfieldlist;
                    grd.DataBind();
                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    //btnExport.Visible = false;
                    grd.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 80: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                { }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.FindControl("txtDisplay_Name") != null)
                    {
                        TextBox txtfocus = (TextBox)e.Row.FindControl("txtDisplay_Name");
                        txtfocus.TextChanged += new EventHandler(txtDisplay_Name_TextChanged);
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (((DataRowView)e.Row.DataItem)["Is_Show"].ToString() == "1")
                    {
                        CheckBox chktmp = (CheckBox)e.Row.FindControl("cbSelect");
                        chktmp.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 115: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        void txtDisplay_Name_TextChanged(object sender, EventArgs e)
        {
            //get current textbox
            TextBox txt = (TextBox)sender;
            //find other controls in the same row
            GridViewRow gvr = (GridViewRow)txt.Parent.Parent;
            TextBox txtone = (TextBox)gvr.FindControl("txtDisplay_Name");
            //update to database
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            ltrErr.Text = "";
            try
            {
                objmysqldb.ConnectToDatabase();
                int fid = 0;
                int sortnumber = 0;
                //for (int i = 0; i < grd.Rows.Count; i++)
                //{
                //    DataTable dataTable = (DataTable)grd.DataSource;
                //    foreach (DataRow dr in dataTable.Rows)
                //    {
                //        TextBox txtsno = (TextBox)grd.Rows[i].Cells[1].FindControl("txtsortnumber");
                //        if (dr["SortNumber"].ToString() == txtsno.Text)
                //        {
                //            ltrErr.Text = "Sort Number already exist";
                //            break;
                //        }
                //    }
                //}
                objmysqldb.OpenSQlConnection();
                foreach (GridViewRow row in grd.Rows)
                {
                    CheckBox chk1 = (CheckBox)row.FindControl("cbSelect");
                    TextBox txtfield = new TextBox();
                    Label lblfid = new Label();
                    TextBox txtsort = new TextBox();
                    txtfield = (TextBox)row.FindControl("txtDisplay_Name");
                    lblfid = (Label)row.FindControl("lblEmployee_Field_id");
                    txtsort = (TextBox)row.FindControl("txtsortnumber");
                    string fieldname = txtfield.Text.ToString().Trim();
                    int.TryParse(txtsort.Text.ToString(), out sortnumber);
                    int.TryParse(lblfid.Text.ToString(), out fid);
                    DateTime currenttime = Logger.getIndiantimeDT();
                    DataTable dtsort = objmysqldb.GetData("Select * from employee_search_field_config where SortNumber=" + sortnumber + " and Is_Show=1 and Employee_field_id<>"+fid+"");
                    if (chk1.Checked)
                    {
                        if (fieldname == "")
                        {
                            ltrErr.Text = "Please Enter Field Display Name.";
                            return;
                        }
                        else
                        {
                            if (dtsort.Rows.Count > 0 && dtsort != null)
                            {
                                ltrErr.Text = "Please Enter Different Sort Number";
                                return;
                            }
                            else
                            {
                                //objmysqldb.ConnectToDatabase();
                                objmysqldb.AddCommandParameter("fieldname", fieldname);
                                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                                string query = "update employee_search_field_config set FieldDisplayName=?fieldname,Is_Show=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + ", SortNumber=" + sortnumber + " where Employee_Field_id=" + fid + "";
                               
                                int res = objmysqldb.InsertUpdateDeleteData(query);
                                if (res != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";
                                    Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 188 Update error.");
                                    return;
                                }
                                else
                                {
                                    grd.EditIndex = -1;
                                    chk1.Checked = true;
                                    ltrErr.Text = "Save successfully.";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fieldname == "")
                        {
                            ltrErr.Text = "Please Enter Field Display Name.";
                            return;
                        }
                        else
                        {
                           // objmysqldb.ConnectToDatabase();
                            objmysqldb.AddCommandParameter("fieldname", fieldname);
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            string query = "update employee_search_field_config set FieldDisplayName=?fieldname,Is_Show=0,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " ,SortNumber="+sortnumber+" where Employee_Field_id=" + fid + "";
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";
                                Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 218 Update error.");
                                return;
                            }
                            else
                            {
                                grd.EditIndex = -1;
                                chk1.Checked = false;
                                ltrErr.Text = "Save successfully.";
                            }
                        }
                    }
                }
                showgrid();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("DisplayColumnConfigurationForSearchEmployee 234: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }



    }
}