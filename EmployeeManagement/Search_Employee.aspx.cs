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
using System.Web.Services;
using System.Data.SqlClient;

namespace EmployeeManagement
{
    public partial class Search_Employee : System.Web.UI.Page
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
                        header.Text = "Search Employee";
                        ViewState["user"] = user_id;

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
                    
                    BindSearchCriteria();
                    Search();
                   
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 65: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void BindSearchCriteria()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtprofile = objmysqldb.GetData("Select Profile_Id,Profile_Name,Profile_Text from employee_query_profile where IsDelete=0 order by Profile_Id");

                ddlprofile.DataSource = dtprofile;
                ddlprofile.DataTextField = "Profile_Name";
                ddlprofile.DataValueField = "Profile_Id";
                ddlprofile.DataBind();
                ddlprofile.Items.Insert(0, new ListItem("Select Profile", "-1"));

                DataTable dtyear = new DataTable();
                //dtyear.Columns.Add("Year_id", typeof(int));
                dtyear.Columns.Add("Year", typeof(int));
                DataTable dtdoj = objmysqldb.GetData("select distinct EmpDOJ from employee_master where EmpDOJ !=null or EmpDOJ!='';");
                dtdoj.Columns.Add("year");
                for (int i = 0; i < dtdoj.Rows.Count; i++)
                {
                    string doj = dtdoj.Rows[i]["EmpDOJ"].ToString();
                    if (doj != null && doj != "")
                    {
                        string[] doj1 = doj.Split('/');
                        dtdoj.Rows[i]["year"] = doj1[2].ToString();
                    }
                }
                dtyear = dtdoj.DefaultView.ToTable(true, "year");

                DataView dv = dtyear.DefaultView;
                dv.Sort = "year";

                ddlyear.DataSource = dtyear;
                ddlyear.DataTextField = "Year";
                ddlyear.DataBind();
                ddlyear.Items.Insert(0, new ListItem("Select Year", "-1"));

                DataTable dtschList = objmysqldb.GetData("Select School_List_ID,School_Name,School_Id from school_list where IsDelete=0 order by School_List_ID");

                ddlschList.DataSource = dtschList;
                ddlschList.DataTextField = "School_Name";
                ddlschList.DataValueField = "School_List_ID";
                ddlschList.DataBind();
                ddlschList.Items.Insert(0, new ListItem("Select Organization", "-1"));

                DataTable dtdepartment = objmysqldb.GetData("Select department_id,Department_Name from department_master where IsDelete=0 order by department_id");

                ddldepart.DataSource = dtdepartment;
                ddldepart.DataTextField = "Department_Name";
                ddldepart.DataValueField = "department_id";
                ddldepart.DataBind();
                ddldepart.Items.Insert(0, new ListItem("Select Department", "1000"));


                DataTable dtdesignation = objmysqldb.GetData("Select designation_id,Designation_Name from designation_master where IsDelete=0 order by designation_id");
                ddldesig.DataSource = dtdesignation;
                ddldesig.DataTextField = "Designation_Name";
                ddldesig.DataValueField = "designation_id";
                ddldesig.DataBind();

                ddldesig.Items.Insert(0, new ListItem("Select Designation", "1000"));
                objmysqldb.disposeConnectionObj();


                DataTable dtstatus = new DataTable();
                dtstatus.Columns.Add("Status_id");
                dtstatus.Columns.Add("Status");
                ddlStatus.DataSource = dtstatus;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "Status_id";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("Select Status", "-1"));
                ddlStatus.Items.Insert(1, new ListItem("Working", "0"));
                ddlStatus.Items.Insert(2, new ListItem("Left", "1"));

                DataTable dtcolList = new DataTable();
                dtcolList.Columns.Add("col_id");
                dtcolList.Columns.Add("col_name");
                ddlColumnList.DataSource = dtcolList;
                ddlColumnList.DataTextField = "col_id";
                ddlColumnList.DataValueField = "col_name";
                ddlColumnList.DataBind();

                ddlColumnList.Items.Insert(0, new ListItem("Select Filter", "0"));
                ddlColumnList.Items.Insert(1, new ListItem("Employee Name", "1"));
                ddlColumnList.Items.Insert(2, new ListItem("Employee DOJ", "2"));
                ddlColumnList.Items.Insert(3, new ListItem("Employee DOB", "3"));
                ddlColumnList.Items.Insert(4, new ListItem("Employee Gender", "5"));
                ddlColumnList.Items.Insert(5, new ListItem("Employee Phone", "6"));

                DataTable dtAssignStatus = new DataTable();
                dtAssignStatus.Columns.Add("Status_id");
                dtAssignStatus.Columns.Add("Status");
                ddlstatusAssign.DataSource = dtAssignStatus;
                ddlstatusAssign.DataTextField = "Status";
                ddlstatusAssign.DataValueField = "Status_id";
                ddlstatusAssign.DataBind();
                ddlstatusAssign.Items.Insert(0, new ListItem("All Employee", "-1"));
                ddlstatusAssign.Items.Insert(1, new ListItem("Assign Employee", "0"));
                ddlstatusAssign.Items.Insert(2, new ListItem("Un Assign Employee", "1"));
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 165: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
            if (ddlstatusAssign.SelectedIndex == 1)
            {
                ddlschList.Disabled = false;
            }
            else
            {
                ddlschList.Disabled = true;
            }
            if (ddlyear.SelectedIndex > 0)
            {
                cbSelect.Enabled = true;
                cbSelect.Checked = false;
            }
            else
            {
                cbSelect.Enabled = false;
                cbSelect.Checked = false;
            }
        }
        private void Search()
        {
            try
            {
                ltrErr.Text = "";
                ltrErr.Visible = false;
                //if (user_id != 1)
                //{
                //    grd.Columns[1].Visible = false;
                //}

                DataTable dtEmpList = BindGrid();
                if (dtEmpList != null )
                {
                    grd.DataSource = dtEmpList;
                    grd.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    ltrErr.Visible = true;
                    ltrErr.Text = "Record dose not exists for selected criteria";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 203: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
       
        private DataTable BindGrid()
        {
            DataTable dtComplaintList = new DataTable();
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                ltrErr.Text = "";
                ltrErr.Visible = false;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllComplaintDetails";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtComplaintList);
      
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 240: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                con.Close();
                //objmysqldb.disposeConnectionObj();
            }
            return dtComplaintList;
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
                DataTable dtEmpList = BindGrid();
                objmysqldb.disposeConnectionObj();
                if (dtEmpList != null && dtEmpList.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtEmpList, "List_Of_Complaints");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 482: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string arg = e.CommandArgument.ToString();
                    int Comp_id = 0;
                    int.TryParse(e.CommandArgument.ToString(), out Comp_id);
                    if (Comp_id > 0)
                    {
                        //Response.Redirect("~/ManageEmployee.aspx?Emp=" + emp_id + "", false);
                        Response.Redirect("~/ManageEmployee.aspx?Comp=" + Comp_id + "", false);
                    }
                }
                else if (e.CommandName == "Delete")
                {
                    string[] arg = e.CommandArgument.ToString().Split(':');
                    if (arg.Length == 2)
                    {
                        //objmysqldb.ConnectToDatabase();
                        int emp_id = 0;
                        int.TryParse(arg[0].ToString(), out emp_id);
                        String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(strConnString);
                        try
                        {
                            SqlCommand cmdDelete = new SqlCommand();
                            cmdDelete.CommandType = CommandType.StoredProcedure;
                            cmdDelete.CommandText = "DeleteComplaintById";
                            cmdDelete.Parameters.AddWithValue("@complaint_id", emp_id);
                            cmdDelete.Connection = con;
                            con.Open();
                            int Yes = cmdDelete.ExecuteNonQuery();
                            if(Yes > 0)
                            {
                                //ltrErr.Text = "Status Update Successfully";

                                grd.EditIndex = -1;
                                Search();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteCriticalLog("Search_Employee 331 Update error.");
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                        
                        }

                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 545:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                
            }
        }
        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Search_Employee 565:: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

    }
}