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
    public partial class ManageEmployeeGroupReportsHeaderImage : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int repgrp = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                ltrA4L.Text = "";
                ltrLP.Text = "";
                ltrLL.Text = "";
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;

                        int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);

                        header.Text = "Manage Employee Report Group Header Image";

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
                    ViewState["repgrpId"] = repgrp.ToString();
                    report_grp.Value = (string)ViewState["repgrpId"];
                    BindImg(repgrp);

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 63: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        private void BindImg(int repgrp)
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                DataTable dtdata = objmysqldb.GetData("SELECT A4L_Img,LegelP_Img,LegelL_Img FROM report_group_list where report_grp_id=" + repgrp + " ");
                string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                IMgA4L.ImageUrl = test + "//HeaderImages.jpg";
                imglegelP.ImageUrl = test + "//HeaderImages.jpg";
                ImgLegelL.ImageUrl = test + "//HeaderImages.jpg";
                string uploadFolderPath = "~/HeaderImages/";
                if (dtdata != null && dtdata.Rows.Count > 0)
                {
                    if (!dtdata.Rows[0]["A4L_Img"].ToString().Equals(""))//A4
                    {
                        string pathSign = HttpContext.Current.Server.MapPath(uploadFolderPath) + "\\" + dtdata.Rows[0]["A4L_Img"].ToString();
                        if (File.Exists(pathSign))
                        {
                            test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//HeaderImages";
                            IMgA4L.ImageUrl = test + "//" + dtdata.Rows[0]["A4L_Img"].ToString();
                        }
                    }
                    if (!dtdata.Rows[0]["LegelP_Img"].ToString().Equals(""))  //Legel port
                    {
                        string pathSign = HttpContext.Current.Server.MapPath(uploadFolderPath) + "\\" + dtdata.Rows[0]["LegelP_Img"].ToString();
                        if (File.Exists(pathSign))
                        {
                            test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//HeaderImages";
                            imglegelP.ImageUrl = test + "//" + dtdata.Rows[0]["LegelP_Img"].ToString();
                        }
                    }
                    if (!dtdata.Rows[0]["LegelL_Img"].ToString().Equals("")) //Legel Lan
                    {
                        string pathSign = HttpContext.Current.Server.MapPath(uploadFolderPath) + "\\" + dtdata.Rows[0]["LegelL_Img"].ToString();
                        if (File.Exists(pathSign))
                        {
                            test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//HeaderImages";
                            ImgLegelL.ImageUrl = test + "//" + dtdata.Rows[0]["LegelL_Img"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 111: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnsaveA4L_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                if (fileA4L.HasFile)
                {
                    string uploadFolderPath = "~/HeaderImages/";
                    string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    if (fileA4L.FileName.ToString().ToLower().Equals("HeaderImages.jpg"))
                    {

                    }
                    else
                    {
                        string ext = System.IO.Path.GetExtension(fileA4L.FileName.ToString());
                        long mode = Logger.getIndiantimeDT().Ticks;
                        int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);
                        string fileName = repgrp.ToString() + "_" + "A4Landscape" + "_" + mode + ext;

                        fileA4L.SaveAs(filePath + "\\" + fileName);
                        string imgPath;
                        imgPath = System.IO.Path.Combine(filePath, fileName);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
                        IMgA4L.ImageUrl = "~/HeaderImages/" + "/" + fileName.ToString();

                        objmysqldb.OpenSQlConnection();
                        objmysqldb.AddCommandParameter("A4L_Img", fileName);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set A4L_Img=?A4L_Img,modify_datetime=" + mode + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                        if (retval > 0)
                        {
                            ltrA4L.Text = "Header Image Update Successfully";
                        }
                        else
                        {
                            ltrA4L.Text = "Header Image Not Update Successfully";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 169: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnremoveA4L_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set A4L_Img='',modify_datetime=" + Logger.getIndiantimeDT().Ticks + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                if (retval > 0)
                {
                    ltrA4L.Text = "Header Image Update Successfully";
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    IMgA4L.ImageUrl = test + "//HeaderImages.jpg";
                }
                else
                {
                    ltrA4L.Text = "Header Image Not Update Successfully";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 67: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnSaveLegelP_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                if (fileLegelP.HasFile)
                {
                    string uploadFolderPath = "~/HeaderImages/";
                    string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    if (fileLegelP.FileName.ToString().ToLower().Equals("HeaderImages.jpg"))
                    {

                    }
                    else
                    {
                        string ext = System.IO.Path.GetExtension(fileLegelP.FileName.ToString());
                        long mode = Logger.getIndiantimeDT().Ticks;
                        int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);
                        string fileName = repgrp.ToString() + "_" + "LegelPortrait" + "_" + mode + ext;

                        fileLegelP.SaveAs(filePath + "\\" + fileName);
                        string imgPath;
                        imgPath = System.IO.Path.Combine(filePath, fileName);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
                        imglegelP.ImageUrl = "~/HeaderImages/" + "/" + fileName.ToString();

                        objmysqldb.OpenSQlConnection();
                        objmysqldb.AddCommandParameter("LegelP_Img", fileName);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set LegelP_Img=?LegelP_Img,modify_datetime=" + mode + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                        if (retval > 0)
                        {
                            ltrLP.Text = "Header Image Update Successfully";
                        }
                        else
                        {
                            ltrLP.Text = "Header Image Not Update Successfully";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 261: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnRemoveLegelP_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set LegelP_Img='',modify_datetime=" + Logger.getIndiantimeDT().Ticks + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                if (retval > 0)
                {
                    ltrLP.Text = "Header Image Update Successfully";
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    imglegelP.ImageUrl = test + "//HeaderImages.jpg";
                }
                else
                {
                    ltrLP.Text = "Header Image Not Update Successfully";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 293: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnsaveLegelL_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                if (fileLegelL.HasFile)
                {
                    string uploadFolderPath = "~/HeaderImages/";
                    string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    if (fileLegelL.FileName.ToString().ToLower().Equals("HeaderImages.jpg"))
                    {

                    }
                    else
                    {
                        string ext = System.IO.Path.GetExtension(fileLegelL.FileName.ToString());
                        long mode = Logger.getIndiantimeDT().Ticks;
                        int.TryParse(Request.QueryString["repgrp"].ToString(), out repgrp);
                        string fileName = repgrp.ToString() + "_" + "LegelLandscape" + "_" + mode + ext;

                        fileLegelL.SaveAs(filePath + "\\" + fileName);
                        string imgPath;
                        imgPath = System.IO.Path.Combine(filePath, fileName);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
                        ImgLegelL.ImageUrl = "~/HeaderImages/" + "/" + fileName.ToString();

                        objmysqldb.OpenSQlConnection();
                        objmysqldb.AddCommandParameter("LegelL_Img", fileName);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set LegelL_Img=?LegelL_Img,modify_datetime=" + mode + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                        if (retval > 0)
                        {
                            ltrLL.Text = "Header Image Update Successfully";
                        }
                        else
                        {
                            ltrLL.Text = "Header Image Not Update Successfully";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 353: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnsaveRemoveL_Click(object sender, EventArgs e)
        {
            try
            {
                ltrA4L.Text = "";
                ltrLL.Text = "";
                ltrLP.Text = "";
                objmysqldb.ConnectToDatabase();
                objmysqldb.OpenSQlConnection();
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                int retval = objmysqldb.InsertUpdateDeleteData("update report_group_list set LegelL_Img='',modify_datetime=" + Logger.getIndiantimeDT().Ticks + ",IsUpdate=1,UserID=" + user_id + " where report_grp_id=" + repgrp + " ");
                if (retval > 0)
                {
                    ltrLL.Text = "Header Image Update Successfully";
                    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
                    ImgLegelL.ImageUrl = test + "//HeaderImages.jpg";
                }
                else
                {
                    ltrLL.Text = "Header Image Not Update Successfully";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployeeGroupReportsHeaderImage 386: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}