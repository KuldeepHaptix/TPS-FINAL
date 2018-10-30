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
using System.Net.Mail;

namespace EmployeeManagement
{
    public class EmailClass
    {
        public EmailClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SendBugMail(string exMessage, string exStack)
        {
            string FromEmailAddress = "info@xpditesolutions.com";
            string ToEmailAddress = "nileshrai78@gmail.com ";
            string UserEmailAddress = "nileshrai78@gmail.com";
            string EmailSubject = "AUTOMATED BUG MESSAGE FROM SIP WEB-SITE";

            string EmailMessage = "";
            EmailMessage = "<br/><br/>";
            EmailMessage += "A new bug has been found with the following Details";
            EmailMessage += "<br/><br/>";
            EmailMessage += "ERROR MESSAGE :" + exMessage + "<br/>";
            EmailMessage += DateTime.Now.ToString() + "ERROR STACKTRACE :" + exStack + "<br/>";

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
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                // Logger.WriteEntry(ex.StackTrace + "/n" + ex.Message);
                Logger.WriteCriticalLog(ex.StackTrace + "/n " + ex.Message);
                return 0;
            }
        }
        public bool SendEmail(string pSubject, string pBody, System.Web.Mail.MailFormat pFormat, string pAttachmentPath, string pEmailTo)
        {
            try
            {
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
                myMail.From = "tpsbugs@gmail.com";//espsmsservice@gmail.com
                myMail.To = pEmailTo;
                //myMail.Cc = "nileshrai78@gmail.com";
                myMail.Subject = pSubject;
                myMail.BodyFormat = pFormat;
                myMail.Body = pBody;
                if (pAttachmentPath.Trim() != "")
                {
                    //System.Web.Mail.MailAttachment MyAttachment =
                    //new System.Web.Mail.MailAttachment(pAttachmentPath);
                    //myMail.Attachments.Add(MyAttachment);
                    //myMail.Priority = System.Web.Mail.MailPriority.High;
                }

                System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465";
                System.Web.Mail.SmtpMail.Send(myMail);
                return true;
            }
            catch (Exception ex)
            {
                // throw;
                return false;
            }
            finally
            {
                //SendEMail(KeysGlobal.pGmailEmail, KeysGlobal.pTo, pSubject, pBody, pAttachmentPath);
            }
        }
        public int SendCriticalBugMail(string bmessage)
        {
            string FromEmailAddress = "nileshrai78@gmail.com";
            string ToEmailAddress = "nileshrai78@gmail.com";
            string UserEmailAddress = "nileshrai78@gmail.com";
            string EmailSubject = "CRITICAL BUG MESSAGE FROM Send SMS WebService";

            string EmailMessage = "";
            EmailMessage = "<br/><br/>";
            EmailMessage += "A new critical bug has been found with the following Details";
            EmailMessage += "<br/><br/>";
            EmailMessage += bmessage;
            EmailMessage += getIndiantime();

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
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                // Logger.WriteEntry(ex.StackTrace + "/n" + ex.Message);
                Logger.WriteCriticalLog(ex.StackTrace + "/n " + ex.Message);
                return 0;
            }
        }
        public static string getIndiantime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
                string temp = ISDTime.ToString();
                return temp;
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString();
            }

        }
        public static DateTime getindiantime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
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
}