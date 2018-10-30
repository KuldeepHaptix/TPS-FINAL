using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;

using System.Threading;

namespace EmployeeManagement
{
    public class ExportToExcel
    {
        public ExportToExcel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public const string EXPORT_URL = "http://Localhost:17235/ExportExcel/";
        //public const string EXPORT_URL = "http://endeavor.vayuna.com:8888/ExportExcel/";
        public static string EXPORT_URL = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//ExportExcel//";

        public string ExportDataTableToExcel(DataTable dtData, string ExcelName)
        {
            string isCreated = "";
            try
            {
                string path = HttpContext.Current.Server.MapPath("~") + "//ExportExcel//";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullFileName = "";
                fullFileName = path;
                DateTime currenttime = Logger.getIndiantimeDT();
                string FileName = ExcelName + "_" + currenttime.Ticks.ToString() + ".xls";
                fullFileName += FileName;

                if (fullFileName.Trim() != "")
                {
                    // Export all the details to Excel

                    RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Win");
                    objExport.ExportDetails(dtData, RKLib.ExportData.Export.ExportFormat.Excel, fullFileName);
                }
                isCreated = FileName;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ExportToExcel 56: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            return isCreated;
        }
        
    }
}