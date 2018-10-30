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
using System.IO;
using System.Text;
using System.Net.Mail;
/// <summary>
/// Summary description for Logger
/// </summary>
public class Logger
{
    public Logger()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int SendMailForgetPassword(string subject, string bmessage, string emailid)
    {
        string FromEmailAddress = "info@xpditesolutions.com";
        string ToEmailAddress = emailid;
        string UserEmailAddress = emailid;
        string EmailSubject = subject;

        string EmailMessage = bmessage;

        SmtpClient Client = new SmtpClient();
        MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);

        try
        {
            message.Subject = EmailSubject;
            //Body can be Html or text format
            //Specify true if it  is html message
            message.IsBodyHtml = true;
            //arranging the email body in proper format.Also appending the user email inside the body
            //Due to the security reasons on goddaddy.com server..from email address can only from 
            // domains hosted on godaddy.com
            message.Priority = MailPriority.High;
            string str;
            str = EmailMessage;
            if (UserEmailAddress != "")
            {
                str = null;
                str = UserEmailAddress + "<br/><br/>";
                str = str + EmailMessage;
            }
            message.Body = str;
            message.Bcc.Add("bhavin.patel@xpditesolutions.com");
            Client.Send(message);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public int SendBugMail(string exMessage, string exStack)
    {
        //string FromEmailAddress, string ToEmailAddress, string UserEmailAddress, string EmailSubject, string EmailMessage
        string FromEmailAddress = "info@xpditesolutions.com";
        string ToEmailAddress = "bugs@xpditesolutions.com";
        string UserEmailAddress = "BUG";
        string EmailSubject = "AUTOMATED BUG MESSAGE FROM CM";

        string EmailMessage = "";
        EmailMessage = "<br/><br/>";
        EmailMessage += "A new bug has been found with the following Details";
        EmailMessage += "<br/><br/>";
        EmailMessage += "ERROR MESSAGE :" + exMessage + "<br/>"; ;
        EmailMessage += "ERROR STACKTRACE :" + exStack + "<br/>"; ;

        SmtpClient Client = new SmtpClient();
        MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);
        try
        {
            message.Subject = EmailSubject;
            //Body can be Html or text format
            //Specify true if it  is html message
            message.IsBodyHtml = true;
            //arranging the email body in proper format.Also appending the user email inside the body
            //Due to the security reasons on goddaddy.com server..from email address can only from 
            // domains hosted on godaddy.com
            message.Priority = MailPriority.High;
            string str;
            str = EmailMessage;
            if (UserEmailAddress != "")
            {
                str = null;
                str = UserEmailAddress + "<br/><br/>";
                str = str + EmailMessage;
            }
            message.Body = str;
            //message.Bcc.Add("bugs@xpditesolutions.com");
            //message.Bcc.Add("yashvant@xpditesolutions.com");
            Client.Send(message);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }


    private static StreamWriter wrRequest = null;
    private static string pathRequest = HttpContext.Current.Server.MapPath("~") + "\\Log\\RequestLogs.txt";
    // private static int intCriticalSkip = LoggerConstantMode.intCriticalSkip;//10;
    private static int intRequestCount = 0;

   

    private static StreamWriter wrsmslog = null;
    private static string pathsms = HttpContext.Current.Server.MapPath("~") + "\\Log\\SendSMSLogs.txt";

   

    /// <summary>
    /// for esp
    /// 30/06/2011
    /// </summary>
    /// <param name="FromMail"></param>
    /// <param name="ToMail"></param>
    /// <param name="Message"></param>
    /// <returns></returns>
    public int SendEMail(string FromMail, string ToMail, string Message)
    {
        string FromEmailAddress = FromMail;
        string ToEmailAddress = ToMail;
        string UserEmailAddress = "";
        string EmailSubject = "Greet:";
        SmtpClient Client = new SmtpClient();
        MailMessage message = new MailMessage(FromMail, ToMail);
        try
        {
            message.Subject = EmailSubject;

            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            string str;
            str = Message;
            message.Body = str;
            Client.Send(message);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static void CriticalBugMail(string message, string exception, string source)
    {
        try
        {
            if (HttpContext.Current.Session["UserName"] != null)
            {
                SendCriticalBugMail("Cricital Bugs from Vayuna", "IP: " + HttpContext.Current.Request.UserHostAddress + " <br/> " + "User: " + HttpContext.Current.Session["UserName"].ToString() + " <br/> " + "Exception Message: " + message + " <br/> " + "StackTrace: " + exception + " <br/> " + "Source: " + source, "");
            }
            else
            {
                SendCriticalBugMail("Cricital Bugs from Vayuna", "IP: " + HttpContext.Current.Request.UserHostAddress + " <br/> " + "User: " + "Session Null" + " <br/> " + "Exception Message: " + message + " <br/> " + "StackTrace: " + exception + " <br/> " + "Source: " + source, "");
            }
        }
        catch (Exception ex)
        {
        }
    }




    /// <summary>
    /// Critical Bug Mail
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="bmessage"></param>
    /// <param name="emailid"></param>
    /// <returns></returns>
    public static int SendCriticalBugMail(string subject, string bmessage, string emailid)
    {
        string FromEmailAddress = "info@xpditesolutions.com";
        string ToEmailAddress = "jaydeep@xpditesolutions.com";
        string UserEmailAddress = emailid;
        string EmailSubject = subject;

        string EmailMessage = bmessage;

        SmtpClient Client = new SmtpClient();
        MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);

        try
        {
            message.Subject = EmailSubject;
            //Body can be Html or text format
            //Specify true if it  is html message
            message.IsBodyHtml = true;
            //arranging the email body in proper format.Also appending the user email inside the body
            //Due to the security reasons on goddaddy.com server..from email address can only from 
            // domains hosted on godaddy.com
            message.Priority = MailPriority.High;
            string str;
            str = EmailMessage;
            message.Body = str;
            //sssmessage.Bcc.Add("nileshrai78gmail.com");
            message.CC.Add("rajputkuldeep7819@gmail.com");

            Client.Send(message);
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static void WriteCriticalLog(string log)
    {
        try
        {

            string pSubject = "The Perfect Solutions Critical Bug Mail.";
            System.Web.Mail.MailMessage myMail = new System.Web.Mail.MailMessage();
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpserver",
            "smtp.gmail.com");
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpserverport",
            "465");
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/sendusing",
            "2");
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //Use 0 for anonymous
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/sendusername",
           "tpsbugs@gmail.com");
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/sendpassword",
            "9662180957");
            myMail.Fields.Add
            ("http://schemas.microsoft.com/cdo/configuration/smtpusessl",
            "true");
            log = "Time:" + getIndiantimeDT().ToString() + "\nLog:" + log + "";
            myMail.From = "tpsbugs@gmail.com";//espsmsservice@gmail.com
           myMail.To = "nileshrai78@gmail.com";
          
            myMail.Subject = pSubject;
            myMail.BodyFormat = System.Web.Mail.MailFormat.Text;
            myMail.Body = log;
            myMail.Cc = "rajputkuldeep7819@gmail.com";
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465";
            System.Web.Mail.SmtpMail.Send(myMail);
        }
        catch (Exception ex)
        {
        }
    }




    public static void SendForgotPwdEmail(string email, string subject, string body)
    {
        try
        {
            EmployeeManagement.EmailClass email1 = new EmployeeManagement.EmailClass();
            email1.SendEmail(subject, body, System.Web.Mail.MailFormat.Html, "", email);
        }
        catch (Exception err)
        {
            string errmsg = err.StackTrace.ToString();
        }
    }

    


    public static DateTime getIndiantimeDT()
    {
        try
        {
            DateTime nonISD = DateTime.Now;
            TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
            return ISDTime;
        }
        catch (Exception ex)
        {
            return DateTime.Now;
        }
    }


}
