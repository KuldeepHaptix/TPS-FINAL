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
    public partial class ManageEmployeeGroupReports : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int repgrp = 0;
                ltrErr.Text = "";
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;

                        int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);
                        if (repgrp == 0)
                        {
                            header.Text = "Add Employee Report Group";
                        }
                        else
                        {
                            header.Text = "Update Employee Report Group";
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
                    txtgroup.Focus();
                    ViewState["repgrpId"] = repgrp.ToString();
                    report_grp.Value = (string)ViewState["repgrpId"];
                    chkleft.Checked = false;
                    bindgvdata(repgrp, 0);

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReports 67: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void bindgvdata(int grpid, int flg)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                Button1.Visible = false; Button2.Visible = false;
                DataTable dtbindGrp = new DataTable();
                if (flg == 1)
                {
                    dtbindGrp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,if(1=1,0,1)as selects from  employee_master where employee_master.IsDelete=0  order by  empid");
                }
                else
                {
                    dtbindGrp = objmysqldb.GetData("select empid,Concat(if(Employee_master.EmpFirstName is null,'',Employee_master.EmpFirstName) , ' ' , if(Employee_master.EmpMiddleName is null,'',Employee_master.EmpMiddleName) , ' ' , if(Employee_master.EmpLastName is null,'',Employee_master.EmpLastName))  AS FullName,if(1=1,0,1)as selects from  employee_master where employee_master.IsDelete=0 and EmpStatusFlag=0  order by  empid");
                }

                if (dtbindGrp != null && dtbindGrp.Rows.Count > 0)
                {
                    DataTable dtselectEmp = objmysqldb.GetData("SELECT emp_ids,report_grp_name,Is_Default FROM report_group_list where IsDelete=0 and report_grp_id=" + grpid + " ");
                    if (dtselectEmp != null && dtselectEmp.Rows.Count > 0)
                    {
                        //for (int i = 0; i < dtselectEmp.Rows.Count; i++)
                        //{
                        txtgroup.Text = dtselectEmp.Rows[0]["report_grp_name"].ToString();
                        chkIsDefault.Checked = false;
                        if (dtselectEmp.Rows[0]["Is_Default"].ToString().Equals("1"))
                        {
                            chkIsDefault.Checked = true;
                        }
                        DataRow[] drselect = dtbindGrp.Select("empid IN(" + dtselectEmp.Rows[0]["emp_ids"].ToString() + ")");
                        foreach (DataRow dr in drselect)
                        {
                            dr["selects"] = "1";
                        }
                        //}
                    }

                    GridView1.DataSource = dtbindGrp;
                    GridView1.DataBind();
                    Button1.Visible = true; Button2.Visible = true;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ltrErr.Text = "Receord does not exsits for selected criteria";
                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReports 123: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
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
                {

                    if (((DataRowView)e.Row.DataItem)["selects"].ToString() == "1")
                    {
                        CheckBox chkkselect = (CheckBox)e.Row.FindControl("cbSelect");
                        chkkselect.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReports 151: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            savedata();
        }

        private void savedata()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                int rep_grp = 0;
                int.TryParse(report_grp.Value.ToString(), out rep_grp);
                if (txtgroup.Text.Equals(""))
                {
                    ltrErr.Text = "Please enter Report Group Name";
                    return;
                }
                DataTable dtprevData = objmysqldb.GetData("select report_grp_id from report_group_list where IsDelete=0 and report_grp_name = '" + txtgroup.Text.ToString() + "' ");
                //if (dtprevData != null && dtprevData.Rows.Count > 0)
                //{
                //    ltrErr.Text = "Report Group Name Is Already Exsits";
                //    return;
                //}
                string emp_ids = "";
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox chkshow = (CheckBox)row.FindControl("cbSelect");
                    if (chkshow.Checked)
                    {
                        int emp_id = 0;
                        Label emp = (Label)row.FindControl("lblemp_id");
                        int.TryParse(emp.Text.ToString(), out emp_id);
                        emp_ids += emp_id + ",";
                    }
                }
                if (emp_ids.Equals(""))
                {
                    ltrErr.Text = "Please select atleast one emoloyee";
                    return;
                }
                else
                {
                    emp_ids = emp_ids.TrimEnd(',');
                    DateTime currentTime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.OpenSQlConnection();
                    objmysqldb.AddCommandParameter("report_grp_name", txtgroup.Text.ToString());
                    int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set modify_datetime=" + currentTime.Ticks + ",IsDelete=0,IsUpdate=1,UserID=" + user_id + ",emp_ids='" + emp_ids + "',Is_Default=0,report_grp_name=?report_grp_name where report_grp_id=" + rep_grp + "  ");

                    if (retval == 0)
                    {
                        long mode = Logger.getIndiantimeDT().Ticks;
                        objmysqldb.AddCommandParameter("report_grp_name", txtgroup.Text.ToString());
                        if (dtprevData != null && dtprevData.Rows.Count > 0)
                        {
                            ltrErr.Text = "Report Group Name Is Already Exsits";
                            return;
                        }
                        retval = objmysqldb.InsertUpdateDeleteData("insert into report_group_list (emp_ids,modify_datetime,DOC,IsDelete,IsUpdate,UserID,report_grp_name,Is_Default) values ('" + emp_ids + "'," + mode + "," + currentTime.Ticks + ",0,1," + user_id + ",?report_grp_name,0) ");

                        if (retval != 1)
                        {
                            ltrErr.Text = "please try again";

                            Logger.WriteCriticalLog("ManageEmployeeGroupReports 204 Update error.");
                            return;
                        }
                        rep_grp = int.Parse(objmysqldb.LastNo("report_group_list", "report_grp_id", mode).ToString());
                        if (rep_grp == 1)
                        {
                            objmysqldb.InsertUpdateDeleteData("update report_group_list set Is_Default=1 where report_grp_id=" + rep_grp + "  ");
                        }

                    }
                    if (chkIsDefault.Checked)
                    {
                        objmysqldb.InsertUpdateDeleteData("update report_group_list set modify_datetime=" + currentTime.Ticks + ",IsDelete=0,IsUpdate=1,UserID=" + user_id + ",Is_Default=0  ");
                        objmysqldb.InsertUpdateDeleteData("update report_group_list set Is_Default=1 where report_grp_id=" + rep_grp + " ");
                    }
                }

                int flg = 0;
                if (chkleft.Checked)
                {
                    flg = 1;
                }
                bindgvdata(rep_grp, flg);
                ltrErr.Text = "Report group Assign to employee Successfully.";
            }
            catch (Exception ex)
            {

                Logger.WriteCriticalLog("ManageEmployeeGroupReports 243: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            savedata();
        }

        protected void chkleft_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int flg = 0;
                if (chkleft.Checked)
                {
                    flg = 1;
                }
                int repgrp = 0;
                int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);
                bindgvdata(repgrp, flg);
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReports 272: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
    }
}