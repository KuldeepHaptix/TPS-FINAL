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
    public partial class employee_module_group : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int groupid = 0;
        DataTable dt1 = new DataTable();
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
                        header.Text = "Manage Module Group";//"Add/Edit Employee Module Group";
                        txtgroup.Focus();
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

                    Search();
                    showgrid();
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employee_module_group 55: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void Search()
        {
            objmysqldb.ConnectToDatabase();
            try
            {

                ltrErr.Text = "";
                DataTable dtmodule = objmysqldb.GetData("Select module_id,module_name from employee_module_master where IsDelete=0");
                DataTable dtcategory = objmysqldb.GetData("Select module_group_id,module_group_name,module_ids from employee_app_module_group where IsDelete=0");
                objmysqldb.disposeConnectionObj();
                dtcategory.Columns.Add("module_name");
                for (int i = 0; i < dtcategory.Rows.Count; i++)
                {
                    string module_name = " ";
                    string abc = dtcategory.Rows[i]["module_ids"].ToString();
                    string[] ar = abc.Split(',');
                    for (int a = 0; a < ar.Length; a++)
                    {
                        DataRow[] dr1 = dtmodule.Select("module_id ='" + ar[a].Trim() + "'");
                        if (dr1.Length > 0)
                        {
                            foreach (DataRow dr in dr1)
                            {
                                module_name += dr["module_name"].ToString() + ",";
                            }
                            dtcategory.Rows[i]["module_name"] = module_name.TrimEnd(',');
                        }
                    }
                }
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
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employee_module_group 102: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                objmysqldb.ConnectToDatabase();
                ltrErr.Text = "";
                if (e.CommandName == "Edit")
                {
                    DataTable dtnew = dt1.Copy();
                    string arg = e.CommandArgument.ToString();
                    int.TryParse(e.CommandArgument.ToString(), out groupid);
                    grp_idHidden.Value = groupid.ToString();
                    DataTable dtlist = objmysqldb.GetData("Select * from employee_app_module_group where module_group_id=" + groupid + "");
                    DataTable dtmodulelist = new DataTable();
                    dtmodulelist = objmysqldb.GetData("select * from employee_module_master where IsDelete=0");
                    dtmodulelist.Columns.Add("Select");
                    dtmodulelist.Columns.Add("sort", typeof(Int32));
                    if (dtmodulelist.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtmodulelist.Rows.Count; x++)
                        {
                            dtmodulelist.Rows[x]["Select"] = 0;
                        }
                    }
                    txtgroup.Text = dtlist.Rows[0]["module_group_name"].ToString();
                    for (int i = 0; i < dtlist.Rows.Count; i++)
                    {
                        string mids = dtlist.Rows[i]["module_ids"].ToString();
                        string[] ar = mids.Split(',');
                        //for (int a = 0; a < dtmodulelist.Rows.Count; a++)
                        //{
                        //    if (mids.Contains(dtmodulelist.Rows[a]["module_id"].ToString()))
                        //    {
                        //        dtmodulelist.Rows[a]["Select"] = 1;
                        //    }
                        //    else
                        //    {
                        //        dtmodulelist.Rows[a]["Select"] = 0;
                        //    }
                        //}
                        for (int a = 0; a < ar.Length; a++)
                        {
                            DataRow[] dr = dtmodulelist.Select("module_id = " + int.Parse(ar[a]) + " ");
                          
                            foreach (DataRow drr in dr)
                            {
                                drr["Select"] = 1;
                                drr["sort"] = a + 1;
                            }

                        }
                    }
                    DataView dv = dtmodulelist.DefaultView;
                    dv.Sort = "select DESC,sort ASC ,module_id ASC";
                    dtmodulelist = dv.ToTable();
                    GridView1.DataSource = dtmodulelist;
                    GridView1.DataBind();
                }
                else if (e.CommandName == "del")
                {
                    ltrErr.Text = "";
                    int mid = 0;
                    string arg = e.CommandArgument.ToString();
                    int.TryParse(e.CommandArgument.ToString(), out groupid);
                    DataTable dt = objmysqldb.GetData("Select * from employee_app_module_rights where module_group_id=" + groupid + "");
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        ltrErr.Text = "Group is Already assgin to employee,you can't Delete it.";
                    }
                    else
                    {
                        string query = "update employee_app_module_group set IsDelete=1,isupdate=1 where module_group_id=" + groupid + "";
                        objmysqldb.OpenSQlConnection();
                        int res = objmysqldb.InsertUpdateDeleteData(query);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";

                            Logger.WriteCriticalLog("employee_module_group 183 Update error.");
                        }
                        else
                        {
                            Search();
                            txtgroup.Text = "";
                            showgrid();
                            ltrErr.Text = "Group Deleted Successfully";

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employee_module_group 199: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                ltrErr.Text = "";
                int grpid = 0;
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(grp_idHidden.Value.ToString(), out grpid);
                DataTable dt = objmysqldb.GetData("Select * from employee_app_module_group where module_group_id=" + grpid + " ");
                DataTable dtgname = objmysqldb.GetData("Select * from employee_app_module_group where module_group_name='" + txtgroup.Text + "' and module_group_id <> " + grpid + " and IsDelete=0");
                if (dt != null && dt.Rows.Count > 0)
                {
                    upadteGeneralDetails();
                    return;
                }
                else
                {
                    dt1.Columns.Add("module_id", typeof(Int32));
                    dt1.Columns.Add("module_name");
                    dt1.Columns.Add("SortNumber", typeof(Int32));
                    string mname = "";
                    string gid = "";
                    string sno = "";
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox chk1 = (CheckBox)row.FindControl("cbSelect");
                        Label lblmid = new Label();
                        Label lblmname = new Label();
                        TextBox txtsort = new TextBox();
                        lblmid = (Label)row.FindControl("lblModule_id");
                        lblmname = (Label)row.FindControl("lblmoduledName");
                        txtsort = (TextBox)row.FindControl("txtsortnumber");
                        string a = txtsort.Text.ToString().Trim();
                        if (chk1.Checked)
                        {
                            gid = lblmid.Text;
                            sno = txtsort.Text;
                            mname = lblmname.Text;
                            dt1.Rows.Add();
                            dt1.Rows[i]["module_id"] = int.Parse(gid);
                            dt1.Rows[i]["module_name"] = mname;

                            int sort = 0;
                            int.TryParse(sno.ToString(), out sort);
                            dt1.Rows[i]["SortNumber"] = sort;
                            if (sno.Equals(""))
                            {
                                dt1.Rows[i]["SortNumber"] = 1000;
                            }
                            i++;
                        }
                    }
                    DataView dv = dt1.DefaultView;
                    dv.Sort = "SortNumber,module_id";
                    dt1 = dv.ToTable();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("module_group_name", txtgroup.Text.ToString());
                    string id = "";
                    for (int b = 0; b < dt1.Rows.Count; b++)
                    {
                        id += dt1.Rows[b]["module_id"].ToString() + ",";
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    if (txtgroup.Text == "")
                    {
                        ltrErr.Text = "Please Fill all Details.";
                    }
                    else
                    {
                        if (dtgname.Rows.Count > 0 && dtgname != null)
                        {
                            ltrErr.Text = "Same Group Name Is Already Exist";
                        }
                        else
                        {
                            string query = "Insert into  employee_app_module_group(module_group_name,module_ids,modify_datetime,DOC,IsDelete,IsUpdate,UserID) values (?module_group_name,'" + id.TrimEnd(',') + "'," + currenttime.Ticks + "," + currenttime.Ticks + ",0,1,1)";
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("employee_module_group 290 Update error.");
                            }
                            else
                            {
                                txtgroup.Text = "";
                                showgrid();
                                Search();
                                ltrErr.Text = "Group Save Successfully.";


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Module_Group 307: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                DataTable dtfieldlist = objmysqldb.GetData("Select module_id,module_name from employee_module_master order by module_id");
                dtfieldlist.Columns.Add("sort");
                objmysqldb.disposeConnectionObj();
                if (dtfieldlist != null)
                {
                    GridView1.DataSource = dtfieldlist;
                    GridView1.DataBind();
                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    GridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employee_module_group 337: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        private void upadteGeneralDetails()
        {
            try
            {
                int grpid = 0;
                int i = 0;
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();

                // DataTable dt1 = new DataTable();
                dt1.Columns.Add("module_id", typeof(Int32));
                dt1.Columns.Add("module_name");
                dt1.Columns.Add("SortNumber", typeof(Int32));
                string mname = "";
                string gid = "";
                string sno = "";
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox chk1 = (CheckBox)row.FindControl("cbSelect");
                    Label lblmid = new Label();
                    Label lblmname = new Label();
                    TextBox txtsort = new TextBox();
                    lblmid = (Label)row.FindControl("lblModule_id");
                    lblmname = (Label)row.FindControl("lblmoduledName");
                    txtsort = (TextBox)row.FindControl("txtsortnumber");
                    string a = txtsort.Text.ToString().Trim();
                    if (chk1.Checked)
                    {
                        gid = lblmid.Text;
                        sno = txtsort.Text;
                        mname = lblmname.Text;
                        int sort = 0;
                        int.TryParse(sno.ToString(), out sort);
                        dt1.Rows.Add();
                        dt1.Rows[i]["module_id"] = int.Parse(gid);
                        dt1.Rows[i]["module_name"] = mname;
                        dt1.Rows[i]["SortNumber"] = sort;
                        if (sno.Equals(""))
                        {
                            dt1.Rows[i]["SortNumber"] = 1000;
                        }

                        i++;
                    }
                }
                DataView dv = dt1.DefaultView;
                dv.Sort = "SortNumber,module_id";
                dt1 = dv.ToTable();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int.TryParse(grp_idHidden.Value.ToString(), out grpid);

                DataTable dtgname = objmysqldb.GetData("Select * from employee_app_module_group where module_group_name='" + txtgroup.Text + "'and module_group_id <> " + grpid + " and IsDelete=0");

                string id = "";
                for (int b = 0; b < dt1.Rows.Count; b++)
                {
                    id += dt1.Rows[b]["module_id"].ToString() + ",";
                }
                DateTime currenttime = Logger.getIndiantimeDT();
                if (txtgroup.Text == "")
                {
                    ltrErr.Text = "Please Fill All Details";
                    return;
                }
                else
                {
                    if (dtgname.Rows.Count > 0 && dtgname != null)
                    {
                        ltrErr.Text = "Same Group Name Is Already Exist.";
                    }
                    else
                    {
                        objmysqldb.AddCommandParameter("module_group_name", txtgroup.Text.ToString());
                        string query = "update employee_app_module_group set module_group_name=?module_group_name,module_ids='" + id.TrimEnd(',') + "', modify_datetime=" + currenttime.Ticks + ",IsDelete=0,IsUpdate=1,UserId=" + user_id + " where module_group_id=" + grpid + "";

                        int res = objmysqldb.InsertUpdateDeleteData(query);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";

                            Logger.WriteCriticalLog("employee_module_group 420 Update error.");
                        }
                        else
                        {
                            txtgroup.Text = "";
                            showgrid();
                            Search();
                            ltrErr.Text = "Group Update Successfully.";


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Employee_Module_Group 436: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                {

                }
            }
            catch (Exception aa)
            {

            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtmodule = objmysqldb.GetData("Select module_id,module_name from employee_module_master where IsDelete=0");
                DataTable dtcategory = objmysqldb.GetData("Select module_group_id,module_group_name,module_ids from employee_app_module_group where IsDelete=0");
                objmysqldb.disposeConnectionObj();
                dtcategory.Columns.Add("module_name");
                for (int i = 0; i < dtcategory.Rows.Count; i++)
                {
                    string module_name = " ";
                    string abc = dtcategory.Rows[i]["module_ids"].ToString();
                    string[] ar = abc.Split(',');
                    for (int a = 0; a < ar.Length; a++)
                    {
                        DataRow[] dr1 = dtmodule.Select("module_id ='" + ar[a].Trim() + "'");
                        if (dr1.Length > 0)
                        {
                            foreach (DataRow dr in dr1)
                            {
                                module_name += dr["module_name"].ToString() + ",";
                            }
                            dtcategory.Rows[i]["module_name"] = module_name.TrimEnd(',');
                        }
                    }
                }
                dtcategory.Columns.Remove("module_ids");
                if (dtcategory != null && dtcategory.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtcategory, "List_Of_Employee_Module_Group");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists.";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("employee_module_group 509: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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

        protected void sellectAll(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkb1");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbSelect");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkHeader");
            if (chkHeader.Checked)
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    CheckBox chkRow = (CheckBox)gvrow.FindControl("cbSelect");
                    chkRow.Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    CheckBox chkRow = (CheckBox)gvrow.FindControl("cbSelect");
                    chkRow.Checked = false;
                }
            }

        }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            int totalRowCountGrid = GridView1.Rows.Count;

            CheckBox chkHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkHeader");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chkRow = (CheckBox)gvrow.FindControl("cbSelect");
                if (chkRow.Checked)
                {
                    count++;
                }
            }

            if (count == totalRowCountGrid)
            {
                chkHeader.Checked = true;
            }
            else
            {
                chkHeader.Checked = false;
            }
        }

    }
}