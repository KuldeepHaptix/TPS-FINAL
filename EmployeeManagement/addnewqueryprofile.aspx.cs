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
    public partial class addnewqueryprofile : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        bool allowinsert = false;
        DataTable dtdata = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int profileid = 0;
                try
                {
                    //ViewState["p_id"] = 0;
                   // ViewState["msg"] = "";
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;

                        int.TryParse(Request.QueryString["profile"].ToString(), out profileid);
                        if (profileid == 0)
                        {
                            header.Text = "Add New Query Profile";
                            btnExport.Visible = false;
                        }
                        else
                        {
                            header.Text = "Update Query Profile";
                            btnExport.Visible = true;
                        }
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
                    
                    txtprofile.Focus();
                    if (profileid > 0)
                    {
                       
                        profile_idHidden.Value = profileid.ToString();
                        getprofiledata();
                        query_insert_update();                       
                        ltrErr.Text = (string)ViewState["msg"];
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addnewqueryprofile 76: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void getprofiledata()
        {

            DataTable dtprofile = new DataTable();
            try
            {
                objmysqldb.ConnectToDatabase();
                int profileid = 0;
                int.TryParse(profile_idHidden.Value.ToString(), out profileid);
                dtprofile = objmysqldb.GetData("select * from employee_query_profile where Profile_Id=" + profileid + " and IsDelete=0;");
                if (dtprofile != null && dtprofile.Rows.Count > 0)
                {
                    txtprofile.Text = dtprofile.Rows[0]["Profile_Name"].ToString();
                    txtquery.Text = dtprofile.Rows[0]["Profile_Text"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addnewqueryprofile 97: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void query_insert_update()
        {
            objmysqldb.ConnectToDatabase();
            //objmysqldb.OpenSQlConnection();
            try
            {
                if (!txtquery.Text.Trim().Equals(""))
                {
                    char[] strq = { '"' };
                    string SubQuerya = txtquery.Text.ToLower().TrimStart(strq);
                    char[] strarray1 = { ' ' };
                    string[] SubQuery1 = SubQuerya.ToLower().Split(strarray1, StringSplitOptions.RemoveEmptyEntries);
                    if (SubQuery1[0].ToLower().Equals("select"))
                    {
                        string[] strarray = { "from" };
                        string[] SubQuery = txtquery.Text.ToLower().Split(strarray, StringSplitOptions.RemoveEmptyEntries);
                        if (SubQuery[0].ToString().ToLower().Contains("empid") && SubQuery[0].ToString().ToLower().Contains("empstatusflag"))
                        {
                            string query = txtquery.Text;
                            dtdata = objmysqldb.GetData(query);
                            if (dtdata != null && dtdata.Rows.Count > 0)
                            {
                                allowinsert = true;
                                grd.DataSource = dtdata;
                                grd.DataBind();
                            }

                            ViewState["querydata"] = dtdata;
                        }
                        else
                        {
                            ltrErr.Text = "Query should contain empid & empstatusflag.";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("addnewqueryprofile 143: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                //objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
               
                query_insert_update();
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                if (allowinsert)
                {
                    int profileid = 0;
                    int.TryParse(profile_idHidden.Value.ToString(), out profileid);
                    if (txtprofile.Text == "" || txtquery.Text == "")
                    {
                        ltrErr.Text = "Please Enter all Data.";
                    }
                    else
                    {
                        DateTime currenttime = Logger.getIndiantimeDT();
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        DataTable dtdata = objmysqldb.GetData("Select * from employee_query_profile where Profile_Id=" + profileid +
    "");
                        DataTable dtprofile = objmysqldb.GetData("Select * from employee_query_profile  where Profile_Name='" + txtprofile.Text + "' and Profile_Id<>" + profileid + " and IsDelete=0");
                        if (dtdata != null && dtdata.Rows.Count > 0)
                        {
                            if (dtprofile != null && dtprofile.Rows.Count > 0)
                            {
                                ltrErr.Text = "Profile Name Already Exist.";
                                return;
                            }
                            else
                            {
                                objmysqldb.AddCommandParameter("profile", txtprofile.Text.ToString());
                                objmysqldb.AddCommandParameter("query", txtquery.Text.ToString());
                                string updatequery = "update employee_query_profile set Profile_Name=?profile,Profile_Text=?query,modify_datetime=" + currenttime.Ticks + ",UserID=" + user_id + ",IsDelete=0,IsUpdate=1 where Profile_Id=" + profileid + " ";
                                int resupdate = objmysqldb.InsertUpdateDeleteData(updatequery);
                                if (resupdate != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";

                                    Logger.WriteCriticalLog("Add New Query Profile 194 Update error.");
                                }
                                else
                                {
                                    long lastp_id = objmysqldb.LastNo("employee_query_profile", "Profile_Id", currenttime.Ticks);
                                    profile_idHidden.Value = lastp_id.ToString();                                  
                                    
                                   
                                    btnExport.Visible = true;
                                    ltrErr.Text = "Query Profile Updated Successfully";
                                    ViewState["p_id="] = profile_idHidden.Value;
                                    ViewState["msg"] = ltrErr.Text;
                                }
                            }
                        }
                        else
                        {
                            DataTable dtprofiles = objmysqldb.GetData("Select * from employee_query_profile  where Profile_Name='" + txtprofile.Text + "' and IsDelete=0");
                            if (dtprofiles != null && dtprofiles.Rows.Count > 0)
                            {
                                ltrErr.Text = "Profile Name Already Exist.";
                                return;
                            }
                            else
                            {
                                objmysqldb.AddCommandParameter("profile", txtprofile.Text.ToString());
                                objmysqldb.AddCommandParameter("query", txtquery.Text.ToString());
                                string query = "Insert into employee_query_profile(Profile_Name,Profile_Text,modify_datetime,DOC,UserID,IsDelete,IsUpdate)values(?profile,?query," + currenttime.Ticks + "," + currenttime.Ticks + "," + user_id + ",0,1)";
                                int res = objmysqldb.InsertUpdateDeleteData(query);
                                if (res != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";

                                    Logger.WriteCriticalLog("Add New Query Profile 227 Update error.");
                                }
                                else
                                {
                                    long lastp_id = objmysqldb.LastNo("employee_query_profile", "Profile_Id", currenttime.Ticks);
                                    profile_idHidden.Value = lastp_id.ToString();
                                    ltrErr.Text = "Query Profile Save Successfully";
                                   
                                    btnExport.Visible = true;
                                    ViewState["p_id="] = profile_idHidden.Value;
                                    ViewState["msg"] = ltrErr.Text;
                                }
                            }
                        }

                    }
                }
                else
                {
                    ltrErr.Text = "Query Not Run Successfully.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Add New Query Profile 251: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Add New Query Profile 275: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtdat = new DataTable();
                objmysqldb.disposeConnectionObj();
                if (ViewState["querydata"] != null)
                {
                    dtdata = (DataTable)ViewState["querydata"];
                }
                if (dtdata != null && dtdata.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtdata, "Query Result");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Add New Query Profile 303: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
    }
}