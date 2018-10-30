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
using System.Data.SqlClient;

namespace EmployeeManagement
{
    public partial class createuseraccount : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int groupid = 0;
        string empids = "0,";
        DataTable dtmodulelist = new DataTable();
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
                        header.Text = "Create New User Account";

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
                    //ddlemplist.Focus();
                    //Search();
                    //binduser();
                    //bindemployee();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("createuseraccount 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.CommandType = CommandType.StoredProcedure;
                cmdDelete.CommandText = "sp_save_eng_Details";
                
                cmdDelete.Parameters.AddWithValue("@name", txtname.Text.ToString());
                string gender = "";
                if (rdbFemale.Checked)
                {

                    gender = "Female";
                }
                else
                {
                    gender = "Male";
                }
                //if (txtcn.Text.ToString()!=null  && !txtcn.Text.Equals(""))
                //{
                //    ltrErr.Text = "No Can't be blank";
                //    return;
                //}
                //if(txtname.Text!=null && !txtname.Text.Equals(""))
                //{

                //    ltrErr.Text = "Name Can't be blank";
                //    return;
                //}

                cmdDelete.Parameters.AddWithValue("@gender", gender);
                cmdDelete.Parameters.AddWithValue("@mobile_no",txtcn.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@address", txtAddress.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@dob", txtdob.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@doj", txtdoj.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@email", txtemail.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@speclization",txtspe.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@remarks", txtremarks.Text.ToString());
                cmdDelete.Parameters.AddWithValue("@IsDelete ",0);
                cmdDelete.Parameters.AddWithValue("@addedby",user_id);
                cmdDelete.Connection = con;
                con.Open();
                int Yes = cmdDelete.ExecuteNonQuery();
                if(Yes > 0)
                {

                    ltrErr.Text = "Saved ";
                    cleardata();
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteCriticalLog("btnsave_Click 197: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void cleardata()
        {

            txtAddress.Text = "";
            txtdoj.Text = "";
            txtemail.Text = "";
            txtname.Text = "";
            txtremarks.Text = "";
            txtspe.Text = "";
            txtcn.Text = "";
        }
        protected void grduser_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Logger.WriteCriticalLog("btnsave_Click 222: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grduser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();

                int uid = 0;

                if (e.CommandName == "Edit")
                {
                    int.TryParse(e.CommandArgument.ToString(), out uid);
                
                }
                else if (e.CommandName == "del")
                {
                    //ltrErr.Text = "";
                    
                    
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                
            }
        }

        protected void grduser_RowEditing(object sender, GridViewEditEventArgs e)
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

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (((DataRowView)e.Row.DataItem)["Select"].ToString() == "1")
                    {
                        CheckBox chktmp = (CheckBox)e.Row.FindControl("cbSelect");
                        chktmp.Checked = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}