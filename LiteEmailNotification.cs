using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;

namespace TTliteUtil
{
    public class LiteEmailNotification
    {
        public static void SendStatusMeaage(string OldStatus, string CurrentStatusName, string UserName, string StoreName, int JobNo, string StatusMsg, string Email, string Subject, string CurentUserName)
        {
            try
            {
                StringBuilder NotificationMessage = new StringBuilder();
                NotificationMessage.AppendLine("<div style='font-family:Arial;font-size:12px;color:#808080'><p>Notification Message from: <a href='http://www.lite.loginworks.com'>RetailDataTracker<a/></p></div>");
                NotificationMessage.AppendLine(string.Format("<div>Hi {0},<br/><br/>I like to inform you that, status for {1} has been change from '{2}' to '{3}'. The change has been done by {4}.<br/><br/><br/>Regards,<br/>Retail Data Tracker Team</div>", UserName, StoreName, OldStatus, CurrentStatusName, CurentUserName));
                SendNotify(NotificationMessage, Subject, Email, "", "", "");
            }
            catch (Exception ex)
            {

                string emsg = "";
                StackTrace trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(trace.FrameCount - 1);
                string fileName = stackFrame.GetFileName();
                if (ex.ToString() != "" && ex.ToString() != null)
                    emsg = ex.ToString();
                else
                    emsg = ex.Message;
                InsertLogDetail(emsg, 0, ex.StackTrace.ToString(), fileName, Subject, "", Email, "");
            }
        }
        //
        //
        //Mark Watts <Mark.Watts@retaildatallc.com>, Dheeraj Juneja <djuneja@loginworks.com>, Ryan Strieter <Ryan.Strieter@retaildatallc.com>, rajat.prakash@loginworks.com, shoeb@loginworks.com, Miles.McKemy@retaildatallc.com, Shiniece.Hunt@retaildatallc.com, sanjeev.kumar@loginworks.com, Tamir.Sherif@retaildatallc.com, Kevin Hade <Kevin.Hade@retaildatallc.com>, Chris.Ferguson@retaildatallc.com


        public static void SendCompletedStatusMessForClient(string StoreName, string ToMail, bool IsClients, string upldBy)
        {
            try
            {
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessagemain = new StringBuilder();
                StringBuilder NotificationMessageFinalll = new StringBuilder();
                StringBuilder NotificationMessagemainals = new StringBuilder();
                string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                string MailSubject = string.Empty;
                string Mailtmsg = string.Empty;
                string[] StoreList = StoreName.Split(',');
                string StoreFormattedList = string.Empty;
                int number = 0;
                if (StoreList.Length > 1)
                {
                    foreach (string ositem in StoreList)
                    {
                        number = number + 1;
                        string[] Splititem = ositem.Split('(');
                        StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                    }
                    //StoreFormattedList = string.Format("<ul>{0}</ul>", StoreFormattedList);
                    // NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>We have uploaded data files for following stores on FTP server:<br/><br/>{0}<br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreFormattedList));
                    MailSubject = "New data available on FTP [Ads]";
                    Mailtmsg = "This is a notification to let you know that New data available on FTP [Ads] ";
                    NotificationMessage.AppendFormat(NotifyMsg.Responsibledeliverdmessageo, Mailtmsg, StoreFormattedList);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessage, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

                }
                else
                {
                    //NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>{0} data is now available on the FTP server.<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreName));
                    MailSubject = string.Format("{0} data is ready [Ads]", StoreName);
                    //  NotificationMessagemainals.AppendFormat(NotifyMsg.Responsiblecomplmsg, StoreName);
                    NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewcompleteads, StoreName);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

                }
                if (IsClients)
                {

                    string ToTamil = ConfigurationManager.AppSettings["ToCompletedMail"];

                    string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMail"];
                    if (upldBy != "")
                    {
                        if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                        else
                            ArrayCc = ArrayCc + "," + upldBy;
                    }

                    SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                string emsg = "";
                StackTrace trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(trace.FrameCount - 1);
                string fileName = stackFrame.GetFileName();
                if (ex.ToString() != "" && ex.ToString() != null)
                    emsg = ex.ToString();
                else
                    emsg = ex.Message;
                // InsertLogDetail(emsg, 0, ex.StackTrace.ToString(), fileName, subject, "", emailTo, Cc);
                InsertLogDetail(emsg, 0, ex.StackTrace.ToString(), fileName, "", "Error in SendCompletedStatusMessForClient method", ConfigurationManager.AppSettings["ToCompletedMail"], ConfigurationManager.AppSettings["CcCompletedMail"]);

            }


        }
        //
        //Change detected for ads
        // 
        public static void SendChangedetectedStatusMessForClient(string StoreName, string ToMail, bool IsClients,string upldBy)
        {
            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessagemain = new StringBuilder();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            StringBuilder NotificationMessagemainals = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            string MailSubject = string.Empty;
            string Mailtmsg = string.Empty;
            string[] StoreList = StoreName.Split(',');
            string StoreFormattedList = string.Empty;
            int number = 0;
            if (StoreList.Length > 1)
            {
                foreach (string ositem in StoreList)
                {
                    number = number + 1;
                    string[] Splititem = ositem.Split('(');
                    StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                }
                //StoreFormattedList = string.Format("<ul>{0}</ul>", StoreFormattedList);
                //NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>It seems there are some changes in the following website(s), this may cause some delay in data delivery.<br/><br/>{0}<br/><br/><br/>We are working on fixing these, shall keep you posted.<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreFormattedList));
                MailSubject = "Change detected in website(s) [Ads]";
                Mailtmsg = "This is a notification to let you know that Change detected in website(s) [Ads] ";
                NotificationMessage.AppendFormat(NotifyMsg.Responsibledeliverdmessageo, Mailtmsg, StoreFormattedList);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessage, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

            }
            else
            {
                //NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>It seems there are some changes in the {0} website, this may cause some delay in data delivery.<br/><br/>We are working on fixing this, shall keep you posted.<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreName));
                MailSubject = string.Format("{0} website changed [Ads]", StoreName);
                // NotificationMessagemainals.AppendFormat(NotifyMsg.Responsiblechangemsg, StoreName);
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewchangedectads, StoreName);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

            }
            if (IsClients)
            {

                string ToTamil = ConfigurationManager.AppSettings["ToCompletedMail"];

                string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMail"];
                if (upldBy != "")
                {
                    if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                    else
                        ArrayCc = ArrayCc + "," + upldBy;
                }
                SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
            }
            else
            {

            }


        }
        public static void SendAnalysisStatusMessForClient(string StoreName, string ToMail, bool IsClients, string upldBy)
        {

            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessagemain = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            StringBuilder NotificationMessagemainals = new StringBuilder();
            string Mailtmsg = string.Empty;
            string MailSubject = string.Empty;
            string[] StoreList = StoreName.Split(',');
            string StoreFormattedList = string.Empty;
            int number = 0;
            if (StoreList.Length > 1)
            {
                foreach (string ositem in StoreList)
                {
                    number = number + 1;
                    string[] Splititem = ositem.Split('(');
                    StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                }
                //StoreFormattedList = string.Format("<ul>{0}</ul>", StoreFormattedList);
                //NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>Script for <br/><br/>{0}<br/><br/>failed, either there is a change in website or the script has encountered a condition it was not able to handle.<br/><br/>We are anlysing the script and will get back to you on this very soon.<br/><br/>Note: There could be a possible delay in delivery for this website.<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreFormattedList));
                MailSubject = string.Format("{0} script in Analysis [Ads]", "Multiple");
                // MailSubject = "Primary Ads data available on FTP [Ads]";
                Mailtmsg = "This is a notification to let you know that script in Analysis [Ads] ";
                NotificationMessage.AppendFormat(NotifyMsg.Responelsmsgnewanalysads, StoreFormattedList);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessage, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

            }
            else
            {
                //NotificationMessage.AppendLine(string.Format("<div>Hi,<br/><br/>Script for {0} failed, either there is a change in website or the script has encountered a condition it was not able to handle.<br/><br/>We are anlysing the script and will get back to you on this very soon.<br/><br/>Note: There could be a possible delay in delivery for this website.<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreName));
                MailSubject = string.Format("{0} script in Analysis [Ads]", StoreName);
                // NotificationMessagemainals.AppendFormat(NotifyMsg.Responsibleanalysdmsg, StoreName);
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewanalysads, StoreName);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            }
            if (IsClients)
            {

                string ToTamil = ConfigurationManager.AppSettings["ToCompletedMail"];

                string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMail"];
                if (upldBy != "")
                {
                    if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                    else
                        ArrayCc = ArrayCc + "," + upldBy;
                }
                SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
            }
            else
            {

            }


        }

        //
        // PrimaryAdsdelivered
        //
        public static void SendPrimaryAdsdeliveredMessFor(string StoreName, string ToMail, bool IsClients)
        {
            try
            {
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessagemain = new StringBuilder();
                StringBuilder NotificationMessagemainals = new StringBuilder();
                StringBuilder NotificationMessageFinalll = new StringBuilder();
                string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                string MailSubject = string.Empty;
                string Mailtmsg = string.Empty;
                string[] StoreList = StoreName.Split(',');
                string StoreFormattedList = string.Empty;
                int number = 0;
                if (StoreList.Length > 1)
                {
                    foreach (string ositem in StoreList)
                    {
                        number = number + 1;
                        string[] Splititem = ositem.Split('(');
                        StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                    }
                    //StoreFormattedList = string.Format("<ul>{0}</ul>", StoreFormattedList);
                    MailSubject = "Primary Ads data available on FTP [Ads]";
                    // Mailtmsg = "This is a notification to let you know that Primary Ads data available on FTP [Ads] ";
                    NotificationMessage.AppendFormat(NotifyMsg.Responelsmsgnewcompleteads, StoreFormattedList);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessage, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);

                }
                else
                {
                    NotificationMessagemainals.AppendFormat(NotifyMsg.Responsibledeliverdmsg, StoreName);
                    // MailSubject = string.Format("{0} Primary Ads delivered secondary awaited [Ads]", StoreName);
                    NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewcompleteads, StoreName);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                }
                if (IsClients)
                {

                    string ToTamil = ConfigurationManager.AppSettings["ToCompletedMail"];
                    string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMail"];
                    SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
                }
                else
                {

                }
            }
            catch (Exception ex)
            { }


        }

        //
        // change regular analysis
        //
        public static void AnalysisNotiyforRegular(string StoreName, string ToMail, string AccountManager, string AccountID, string ToOtherEmail, string CcOther, bool IsOther,string upldBy)
        {

            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessagemain = new StringBuilder();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            string MailSubject = string.Empty;
            string[] StoreList = StoreName.Split(',');
            string StoreFormattedList = string.Empty;
            int number = 0;
            if (StoreList.Length > 1)
            {
                foreach (string ositem in StoreList)
                {
                    number = number + 1;
                    string[] Splititem = ositem.Split('(');
                    StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                }
                // NotificationMessage.AppendLine(string.Format("<div>Script for <span style='color:#2865C8'>{0}</span> failed, either there is a change in website or the script has encountered a condition and was unable to handle.<br/><br/>We are anlysing the script and will get back to you shortly.<br/><br/>Note: There could be a possible delay in delivery for this website.<br/><br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreFormattedList, AccountManager, AccountID));
                MailSubject = string.Format("{0} script in Analysis [Webscrapes]", StoreFormattedList);
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewanalys, StoreFormattedList, AccountManager, AccountID);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            }
            else
            {
                // NotificationMessage.AppendLine(string.Format("<div>Script for <span style='color:#2865C8'>{0}</span> failed, either there is a change in website or the script has encountered a condition and was unable to handle.<br/><br/>We are analysing the script and will get back to you shortly.<br/><br/>Note: There could be a possible delay in delivery for this website.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreName, AccountManager, AccountID));
                MailSubject = string.Format("{0} script in Analysis [Webscrapes]", StoreName);
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewanalys, StoreName, AccountManager, AccountID);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            }
            if (IsOther)
            {
                if (ToOtherEmail != "")
                    SendNotify(NotificationMessageFinalll, MailSubject, ToOtherEmail, "", "", CcOther);
            }
            else
            {
                string ToTamil = ConfigurationManager.AppSettings["ToCompletedMailRegular"];

                string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMailRegular"];
                if (upldBy != "")
                {
                    if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                    else
                        ArrayCc = ArrayCc + "," + upldBy;
                }
                SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
            }

        }

        //
        //  Changed detected notify
        //

        public static void ChangedDetectednotifyForRegular(string StoreName, string ToMail, string AccountManager, string AccountID, string ToOtherEmail, string CcOther, bool IsOther,string upldBy)
        {

            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessagemain = new StringBuilder();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            string MailSubject = string.Empty;
            string[] StoreList = StoreName.Split(',');
            string StoreFormattedList = string.Empty;
            int number = 0;
            if (StoreList.Length > 1)
            {
                foreach (string ositem in StoreList)
                {
                    number = number + 1;
                    string[] Splititem = ositem.Split('(');
                    StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                }
                //StoreFormattedList = string.Format("<ul>{0}</ul>", StoreFormattedList);
                //NotificationMessage.AppendLine(string.Format("<div>It seems there are some changes in the following website(s), this may cause some delay in data delivery.<br/><br/><span style='color:#2865C8'>{0}</span><br/><br/><br/>We are working on fixing these, shall keep you posted.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreFormattedList, AccountManager, AccountID));
                MailSubject = "Change detected in website(s) [Webscrapes]";
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnew, StoreFormattedList, AccountManager, AccountID);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            }
            else
            {
                NotificationMessage.AppendLine(string.Format("<div>It seems there are some changes in the <span style='color:#2865C8'>{0}</span> website, this may cause some delay in data delivery.<br/><br/>We are working on fixing this, shall keep you posted.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreName, AccountManager, AccountID));
                MailSubject = string.Format("{0} website changed [Webscrapes]", StoreName);
                NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnew, StoreName, AccountManager, AccountID);
                NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            }

            if (IsOther)
            {
                if (ToOtherEmail != "")
                    SendNotify(NotificationMessageFinalll, MailSubject, ToOtherEmail, "", "", CcOther);
            }
            else
            {
                string ToTamil = ConfigurationManager.AppSettings["ToCompletedMailRegular"];

                string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMailRegular"];
                if (upldBy != "")
                {
                    if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                    else
                        ArrayCc = ArrayCc + "," + upldBy;
                }
                SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
            }



        }

        //
        // Completed regular
        //
        public static void CompletedNotifyForRegular(string StoreName, string ToMail, string AccountManager, string AccountID, string ToOtherEmail, string CcOther, bool IsOther, String upldBy)
        {
            try
            {
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessagemain = new StringBuilder();
                StringBuilder NotificationMessageFinalll = new StringBuilder();
                string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                string MailSubject = string.Empty;
                string[] StoreList = StoreName.Split(',');
                string StoreFormattedList = string.Empty;
                int number = 0;
                if (StoreList.Length > 1)
                {
                    foreach (string ositem in StoreList)
                    {
                        number = number + 1;
                        string[] Splititem = ositem.Split('(');
                        StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                    }

                    NotificationMessage.AppendLine(string.Format("<div>We have uploaded data files for following stores on FTP server:<br/><br/><span style='color:#2865C8'>{0}</span><br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreFormattedList, AccountManager, AccountID));
                    MailSubject = "New data available on FTP [Webscrapes]";
                    NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsg, NotificationMessage);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                }
                else
                {
                    // NotificationMessage.AppendLine(string.Format("<div><span style='color:#2865C8'>{0}</span> data is now available on the FTP server.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/></div>", StoreName, AccountManager, AccountID));
                    MailSubject = string.Format("{0} data is ready [Webscrapes]", StoreName);
                    NotificationMessagemain.AppendFormat(NotifyMsg.Responelsmsgnewcompleteregular, StoreName, AccountManager, AccountID);
                    NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdeliverynew, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                }



                if (IsOther)
                {
                    if (ToOtherEmail != "")
                    {
                        if (upldBy != "" && CcOther!="")
                        {
                            if (ToOtherEmail.ToLower().Contains(upldBy.ToLower()) || CcOther.ToLower().Contains(upldBy.ToLower())) { }
                            else
                                CcOther = CcOther + "," + upldBy;
                        }
                        SendNotify(NotificationMessageFinalll, MailSubject, ToOtherEmail, "", "", CcOther);
                    }
                }
                else
                {
                    string ToTamil = ConfigurationManager.AppSettings["ToCompletedMailRegular"];

                    string ArrayCc = ConfigurationManager.AppSettings["CcCompletedMailRegular"];
                    if (upldBy != "")
                    {
                        if (ArrayCc.ToLower().Contains(upldBy.ToLower()) || ToTamil.ToLower().Contains(upldBy.ToLower())) { }
                        else
                            ArrayCc = ArrayCc + "," + upldBy;
                    }
                    SendNotify(NotificationMessageFinalll, MailSubject, ToTamil, "", "", ArrayCc);
                }
            }
            catch (Exception ex)
            { }


        }
        //
        // Completed other tasks
        //
        public static void NotifyForOtherTask(string title, string WebSite, string AssignedBy, string AssignedTo, string Status, string pri, string TargetDate, bool IsUpdate, bool OnlyStatus)
        {
            try
            {
                StringBuilder NotificationMessage = new StringBuilder();
                string MailSubject = string.Empty;
                string[] WebsiteList = WebSite.Split(',');
                string StoreFormattedList = string.Empty;
                int number = 0;
                if (IsUpdate)
                {
                    if (WebsiteList.Length > 1)
                    {
                        foreach (string ositem in WebsiteList)
                        {
                            number = number + 1;
                            string Splititem = ositem;//.Split('(');
                            StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem);

                        }

                        // NotificationMessage.AppendLine(string.Format("<div>Dear,<br/><br/>We have uploaded data files for following stores on FTP server:<br/><br/>{0}<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreFormattedList, AccountManager, AccountID));
                        MailSubject = "New data available on FTP [Webscrapes]";
                    }
                    else
                    {
                        // NotificationMessage.AppendLine(string.Format("<div>Dear,<br/><br/>{0} data is now available on the FTP server.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreName, AccountManager, AccountID));
                        MailSubject = string.Format("{0} data is ready [Webscrapes]", WebsiteList);
                    }
                }
                else
                {


                    if (WebsiteList.Length > 1)
                    {
                        foreach (string ositem in WebsiteList)
                        {
                            number = number + 1;
                            string Splititem = ositem;//.Split('(');
                            StoreFormattedList += string.Format("{0}. {1}<br/>", number, Splititem[0]);

                        }

                        //NotificationMessage.AppendLine(string.Format("<div>Dear,<br/><br/>We have uploaded data files for following stores on FTP server:<br/><br/>{0}<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreFormattedList, AccountManager, AccountID));
                        MailSubject = "New data available on FTP [Webscrapes]";
                    }
                    else
                    {
                        // NotificationMessage.AppendLine(string.Format("<div>Dear,<br/><br/>{0} data is now available on the FTP server.<br/><br/>Account Manager: {1}<br/>Account ID: {2}<br/><br/>Regards,<br/>Loginworks Team</div><br/><div style='margin:top:25px'><a style='text-decoration:none;color:#808080'  href='http://lite.loginworks.com/'   target='_blank' title='TaskTrek lite'>Sent by TaskTrek Lite</a></div>", StoreName, AccountManager, AccountID));
                        MailSubject = string.Format("{0} data is ready [Webscrapes]", WebsiteList);
                    }

                }


                SendNotify(NotificationMessage, MailSubject, AssignedTo, "", "", "");
            }
            catch (Exception ex)
            { }


        }

        //
        //  Changed password
        //
        public static void ChangedPasswordNotify(string ToEmail, string Password)
        {
            StringBuilder NotificationMessagemain = new StringBuilder();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            StringBuilder NotificationMessage = new StringBuilder();
            string MailSubject = string.Empty;
            string Titlee = string.Empty;
            Titlee = "To log on to the site, use the following details:";
            NotificationMessage.AppendLine(string.Format("<b>UserName:</b> {0}<br/><b>Password:</b> {1}<br/>", ToEmail, Password));
            MailSubject = "Your password has been changed";
            NotificationMessagemain.AppendFormat(NotifyMsg.Responsibleuploadsche, Titlee, NotificationMessage);
            NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            SendNotify(NotificationMessageFinalll, MailSubject, ToEmail, "", "", "");
        }
        //
        // Forget password
        //
        public static void ForgetPasswordNotify(string ToEmail, string Password)
        {

            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessageFinalll = new StringBuilder();
            StringBuilder NotificationMessagemain = new StringBuilder();
            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
            string MailSubject = string.Empty;
            string Titlee = string.Empty;
            Titlee = "Your account details are:";
            NotificationMessage.AppendLine(string.Format("<b>UserName:</b> {0}<br/><b>Password:</b> {1}<br/>", ToEmail, Password));
            MailSubject = "Your login detail";
            NotificationMessagemain.AppendFormat(NotifyMsg.Responsibleuploadsche, Titlee, NotificationMessage);
            NotificationMessageFinalll.AppendFormat(NotifyMsg.Messageforadsdelivery, AccountNameee, AccountNameee, MailSubject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountNameee, NotificationMessagemain, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
            SendNotify(NotificationMessageFinalll, MailSubject, ToEmail, "", "", "");
        }

        //Send notification
        //
        public static void SendNotify(StringBuilder Message, string subject, string emailTo, string AttchmentPath, string AttachmentName, string Cc)
        {

            // ConfigurationManager.AppSettings["UserName"].ToString()
            try
            {

                System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
                string[] toAddressList = emailTo.Split(',');

                //Loads the To address field
                foreach (string address in toAddressList)
                {
                    if (address.Length > 0)
                    {
                        oMessage.To.Add(address);
                    }
                }

                // MailAddress coTopy = new MailAddress(emailTo);
                //  oMessage.To.Add(coTopy);
                MailAddress FromMail = new MailAddress(ConfigurationManager.AppSettings["FromMail"].ToString());
                oMessage.From = (FromMail);
                //  oMessage.From = ;
                //   new MailAddress(ConfigurationManager.AppSettings["FromMail"].ToString()), new MailAddress(emailTo));
                oMessage.Body = Message.ToString();
                if (Cc != null && Cc != "")
                {
                    //  MailAddress copy = new MailAddress(Cc);

                    string[] CcAddressList = Cc.Split(',');
                    foreach (string Ccaddress in CcAddressList)
                    {
                        if (Ccaddress.Length > 0)
                        {
                            oMessage.CC.Add(Ccaddress);
                        }
                    }
                    // oMessage.CC.Add(copy);
                }
                oMessage.IsBodyHtml = true;
                if (AttchmentPath != "" && AttachmentName != "")
                {
                    Attachment Atch = new Attachment(AttchmentPath);
                    Atch.Name = AttachmentName;
                    oMessage.Attachments.Add(Atch);

                }
                oMessage.Subject = subject;
                SmtpClient oClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString()));//)ConfigurationManager.AppSettings["SmtpClient"].ToString());
                NetworkCredential objCredentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Pwd"].ToString());//ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Pwd"].ToString());
                oClient.UseDefaultCredentials = false;
                oClient.Credentials = objCredentials;
                // objCredentials.EnableSsl = true;
                // objCredentials.DeliveryMethod = SmtpDeliveryMethod.Network;
                // objCredentials.UseDefaultCredentials = false;
                Object state = oMessage;
                // temp comment           
                oClient.SendAsync(oMessage, state);
            }
            catch (Exception ex)
            {
                string emsg = "";
                StackTrace trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(trace.FrameCount - 1);
                string fileName = stackFrame.GetFileName();
                if (ex.ToString() != "" && ex.ToString() != null)
                    emsg = ex.ToString();
                else
                    emsg = ex.Message;
                InsertLogDetail(emsg, 0, ex.StackTrace.ToString(), fileName, subject, "", emailTo, Cc);

            }
        }
        public static void SendNotifyWithPriority(StringBuilder Message, string subject, string emailTo, string AttchmentPath, string AttachmentName, string Cc, bool IsPri)
        {

            // ConfigurationManager.AppSettings["UserName"].ToString()
            try
            {

                System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();

                string[] toAddressList = emailTo.Split(',');

                //Loads the To address field
                foreach (string address in toAddressList)
                {
                    if (address.Length > 0)
                    {
                        oMessage.To.Add(address);
                    }
                }

                // MailAddress coTopy = new MailAddress(emailTo);
                //  oMessage.To.Add(coTopy);
                MailAddress FromMail = new MailAddress(ConfigurationManager.AppSettings["FromMail"].ToString());
                oMessage.From = (FromMail);
                //  oMessage.From = ;
                //   new MailAddress(ConfigurationManager.AppSettings["FromMail"].ToString()), new MailAddress(emailTo));
                oMessage.Body = Message.ToString();
                if (Cc != null && Cc != "")
                {
                    //  MailAddress copy = new MailAddress(Cc);

                    string[] CcAddressList = Cc.Split(',');
                    foreach (string Ccaddress in CcAddressList)
                    {
                        if (Ccaddress.Length > 0)
                        {
                            oMessage.CC.Add(Ccaddress);
                        }
                    }
                    // oMessage.CC.Add(copy);
                }
                oMessage.IsBodyHtml = true;
                if (IsPri)
                {
                    // System.Web.Mail.MailPriority obj=new System.Web.Mail.MailPriority ();
                    oMessage.Priority = System.Net.Mail.MailPriority.High;
                }
                if (AttchmentPath != "" && AttachmentName != "")
                {
                    Attachment Atch = new Attachment(AttchmentPath);
                    Atch.Name = AttachmentName;
                    oMessage.Attachments.Add(Atch);

                }
                oMessage.Subject = subject;
                SmtpClient oClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString()));//)ConfigurationManager.AppSettings["SmtpClient"].ToString());
                NetworkCredential objCredentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Pwd"].ToString());//ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Pwd"].ToString());
                oClient.UseDefaultCredentials = false;
                oClient.Credentials = objCredentials;
                // objCredentials.EnableSsl = true;
                // objCredentials.DeliveryMethod = SmtpDeliveryMethod.Network;
                // objCredentials.UseDefaultCredentials = false;
                Object state = oMessage;
                // temp comment           
                oClient.SendAsync(oMessage, state);
            }
            catch (Exception ex)
            {
                string emsg = "";
                StackTrace trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(trace.FrameCount - 1);
                string fileName = stackFrame.GetFileName();
                if (ex.InnerException.ToString() != "" && ex.InnerException.ToString() != null)
                    emsg = ex.InnerException.ToString();
                else
                    emsg = ex.Message;
                InsertLogDetail(emsg, 0, ex.StackTrace.ToString(), fileName, subject, "", emailTo, Cc);

            }
        }
        public static void NotifyTaskUpdated(int TaskID, int UpdatedBY, int ActionTypeId, string dName)
        {
            string RedirectPath = string.Empty;
            string RedirectPathmobi = string.Empty;
            string AccountName = "";
            if (dName != "")
                AccountName = dName;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string UserName = "";
            string UName = "";
            string Uemail = "";
            string LastestPreComment = "";
            string LastestComment = "";
            string Owner = string.Empty;
            string EmailSubject = string.Empty;
            string Msg = string.Empty;

            // DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("select t.*,st.Status from tasks as t inner join status as st on t.StatusID=st.StatusID where t.TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);

                UName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UpdatedBY));
                Uemail = MySqlClass.ReturnSingleValue(string.Format("select Email from users where UserID={0}", UpdatedBY));
                if (UName.Trim() != "")
                    UserName = UName;
                else
                    UserName = Uemail;

                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Title"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> has changed the task title", UserName);
                    //EmailSubject = string.Format("WI-"+TaskID+"Your task title has changed");
                    string comsg = "WI-" + TaskID + ": " + UserName + " has changed task title";
                    EmailSubject = string.Format(comsg);
                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Description"]))
                {
                    Msg = string.Format("Your task has a new description by <span style='color:#0669CB' >{0}</span>.", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has updated task description";
                    EmailSubject = string.Format(comsg);
                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Priority"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> changed your task priority", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has changed task priority";
                    EmailSubject = string.Format(comsg);

                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Feasibility"]))
                {
                    Msg = string.Format("This is a notification to let you know that your task feasibility has been successfully changed.");
                    string comsg = "WI-" + TaskID + ": " + UserName + " feasibility for task updated";
                    EmailSubject = string.Format(comsg, dtTask.Rows[0]["Title"].ToString());

                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Status"]))
                {
                    // string CHCTask = ConfigurationManager.AppSettings["NewStatus"].ToString() + "," + ConfigurationManager.AppSettings["InProgressStatus"].ToString() + "," + ConfigurationManager.AppSettings["AnalysisStatus"].ToString() + "," + ConfigurationManager.AppSettings["NewStatus"].ToString() + "," + ConfigurationManager.AppSettings["CompletedStatus"].ToString() + "," + ConfigurationManager.AppSettings["OnHoldStatus"].ToString() + "," + ConfigurationManager.AppSettings["CancelStatus"].ToString();
                    if (dtTask.Rows[0]["StatusID"].ToString() == (ConfigurationManager.AppSettings["CompletedStatus"]))
                    {
                        Msg = string.Format("The task was completed by <span style='color:#0669CB' >{0}</span>", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has completed your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["StatusOpen"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> changed the task status to Open", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + "has open your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["NewStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has created a new task for you", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has created a new task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["InProgressStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has started working on the task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has start working";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["AnalysisStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> is analysing your Task.", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " is analysing your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["OnHoldStatus"])
                    {
                        Msg = string.Format("The Task is On-Hold.");
                        string comsg = "WI-" + TaskID + ": " + UserName + " has put your task 'On-Hold'";
                        EmailSubject = string.Format(comsg);

                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["CancelStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has cancelled your task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has cancelled your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["Reopentask"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has reopened your task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has reopened your task";
                        EmailSubject = string.Format(comsg);
                    }



                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["UpdateAction"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> has updated your Task.", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has updated your task";
                    EmailSubject = string.Format(comsg);
                }
                //
                //  
                //
                #region Mail to all task team
                string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
                DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UpdatedBY);
                string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
                string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                string highpri = "";
                if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
                {
                    highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
                }
                
                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {

                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    //  NotificationMessageTeam.AppendFormat(NotifyMsg.TaskUpdateMsg + NotifyMsg.OpenMsg, trail["UserName"].ToString(), Msg, dtTask.Rows[0]["Title"].ToString(), RedirectPath, RedirectPath);
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                        }
                        else
                        {
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);
                        }

                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn,RedirectPath, AccountName);
                        }
                        else
                        {
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                        }
                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);
                    SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");

                }
            }
                #endregion
        }


        public static void NotifyTaskUpdatedforcompletedandcancel(int TaskID, int UpdatedBY, int ActionTypeId, string dName)
        {
            string RedirectPath = string.Empty;
            string RedirectPathmobi = string.Empty;
            string AccountName = "";
            if (dName != "")
                AccountName = dName;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string UserName = "";
            string UName = "";
            string Uemail = "";
            string LastestPreComment = "";
            string LastestComment = "";
            string Owner = string.Empty;
            string EmailSubject = string.Empty;
            string Msg = string.Empty;

            // DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("select t.*,st.Status from tasks as t inner join status as st on t.StatusID=st.StatusID where t.TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);

                UName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UpdatedBY));
                Uemail = MySqlClass.ReturnSingleValue(string.Format("select Email from users where UserID={0}", UpdatedBY));
                if (UName.Trim() != "")
                    UserName = UName;
                else
                    UserName = Uemail;

                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Title"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> has changed the task title", UserName);
                    //EmailSubject = string.Format("WI-"+TaskID+"Your task title has changed");
                    string comsg = "WI-" + TaskID + ": " + UserName + " has changed task title";
                    EmailSubject = string.Format(comsg);
                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Description"]))
                {
                    Msg = string.Format("Your task has a new description by <span style='color:#0669CB' >{0}</span>.", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has updated task description";
                    EmailSubject = string.Format(comsg);
                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Priority"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> changed your task priority", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has changed task priority";
                    EmailSubject = string.Format(comsg);

                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Feasibility"]))
                {
                    Msg = string.Format("This is a notification to let you know that your task feasibility has been successfully changed.");
                    string comsg = "WI-" + TaskID + ": " + UserName + " feasibility for task updated";
                    EmailSubject = string.Format(comsg, dtTask.Rows[0]["Title"].ToString());

                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["Status"]))
                {
                    // string CHCTask = ConfigurationManager.AppSettings["NewStatus"].ToString() + "," + ConfigurationManager.AppSettings["InProgressStatus"].ToString() + "," + ConfigurationManager.AppSettings["AnalysisStatus"].ToString() + "," + ConfigurationManager.AppSettings["NewStatus"].ToString() + "," + ConfigurationManager.AppSettings["CompletedStatus"].ToString() + "," + ConfigurationManager.AppSettings["OnHoldStatus"].ToString() + "," + ConfigurationManager.AppSettings["CancelStatus"].ToString();
                    if (dtTask.Rows[0]["StatusID"].ToString() == (ConfigurationManager.AppSettings["CompletedStatus"]))
                    {
                        Msg = string.Format("The task was completed by <span style='color:#0669CB' >{0}</span>", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has completed your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["StatusOpen"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> changed the task status to Open", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + "has open your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["NewStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has created a new task for you", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has created a new task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["InProgressStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has started working on the task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has start working";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["AnalysisStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> is analysing your Task.", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " is analysing your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["OnHoldStatus"])
                    {
                        Msg = string.Format("The Task is On-Hold.");
                        string comsg = "WI-" + TaskID + ": " + UserName + " has put your task 'On-Hold'";
                        EmailSubject = string.Format(comsg);

                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["CancelStatus"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has cancelled your task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has cancelled your task";
                        EmailSubject = string.Format(comsg);
                    }
                    if (dtTask.Rows[0]["StatusID"].ToString() == ConfigurationManager.AppSettings["Reopentask"])
                    {
                        Msg = string.Format("<span style='color:#0669CB' >{0}</span> has reopened your task", UserName);
                        string comsg = "WI-" + TaskID + ": " + UserName + " has reopened your task";
                        EmailSubject = string.Format(comsg);
                    }



                }
                if (ActionTypeId == Convert.ToInt32(ConfigurationManager.AppSettings["UpdateAction"]))
                {
                    Msg = string.Format("<span style='color:#0669CB' >{0}</span> has updated your Task.", UserName);
                    string comsg = "WI-" + TaskID + ": " + UserName + " has updated your task";
                    EmailSubject = string.Format(comsg);
                }
                //
                //  
                //
                #region Mail to all task team
                string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
                DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UpdatedBY);
                string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
                string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                string highpri = "";
                if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
                {
                    highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
                }

                DataTable dtComment = MySqlClass.ReturnDataTable(string.Format("SELECT Comments from taskcomments where TaskID={0} and CommentedBy={1} order by CommentedDate desc limit 1;", TaskID, UpdatedBY));
                if (dtComment.Rows.Count > 0)
                {
                    LastestPreComment = dtComment.Rows[0]["Comments"].ToString();
                    LastestComment = LastestPreComment.Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                }


                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {

                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    //  NotificationMessageTeam.AppendFormat(NotifyMsg.TaskUpdateMsg + NotifyMsg.OpenMsg, trail["UserName"].ToString(), Msg, dtTask.Rows[0]["Title"].ToString(), RedirectPath, RedirectPath);
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriberforcompleteandcancel, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers,UserName,LastestComment);
                        }
                        else
                        {
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriberforcompleteandcancel, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers, UserName, LastestComment);
                        }

                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforcompleteandcancel, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, UserName, LastestComment);
                        }
                        else
                        {
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforcompleteandcancel, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, UserName, LastestComment);
                        }
                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);
                    SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");

                }
            }
                #endregion
        }

        //
        // mail for  work item files
        //
        public static void NotifyNewTaskDocument(int TaskID, int UserId, string CDomain)
        {
            string AccountName = "";
            if (CDomain != "")
                AccountName = CDomain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            //DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("Select * from Tasks where TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            #region set Email subject
            string EmailSubject = string.Empty;
            string CUserName = "";

            //         NotificationMessageTeam.AppendFormat(NotifyMsg.NewTaskDocumentMsg, trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);



            #endregion
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);


                string CName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UserId));
                string CUsermail = MySqlClass.ReturnSingleValue(string.Format("select Email  from users where UserID={0}", UserId));
                if (CName.Trim() != "")
                    CUserName = CName;
                else
                    CUserName = CUsermail;
                EmailSubject = string.Format(NotifyMsg.NewTaskDocumentSubject, TaskID, CUserName);
                string Attachment = string.Format("<span style='font-family:Arial, Helvetica, sans-serif; font-size:13px; float:left'>{0}</span> <span style='float:left; margin-left:5px'>{1}</span>", string.Format(NotifyMsg.NewTaskDocumentMsg, CUserName), Util.GetAllEmailfile(TaskID, AccountName));
                #region Mail to All Task Team
                // DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
                string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
                DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
                string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
                string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                string highpri = "";
                if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
                {
                    highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
                }
                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {
                    //
                    //  get the email id for this user
                    //             
                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Attachment, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Attachment, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);

                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Attachment, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Attachment, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);

                    // NotificationMessageTeam.AppendFormat(NotifyMsg., trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                    // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessageTeam, AccountName, AccountName, AccountName);
                    SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");
                }
            }
                #endregion
        }
        //
        // mail for  work item files
        //
        public static void NotifyNewTaskMessage(int TaskID, int UserId, string Domain)
        {
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            //DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("Select * from Tasks where TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
            string CUserName = "";

            string highpri = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
            }
            string Msg = "";
            string LastestPreComment = "";
            string LastestComment = "";
            #region set Email subject
            string EmailSubject = string.Empty;
            #endregion
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none' target='_blank'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                DataTable dtComment = MySqlClass.ReturnDataTable(string.Format("SELECT Comments from taskcomments where TaskID={0} and CommentedBy={1} order by CommentedDate desc limit 1;", TaskID, UserId));
                if (dtComment.Rows.Count > 0)
                    LastestPreComment = dtComment.Rows[0]["Comments"].ToString();
                LastestComment = LastestPreComment.Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");


                string CName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UserId));
                string Cmail = MySqlClass.ReturnSingleValue(string.Format("select Email  from users where UserID={0}", UserId));
                if (CName.Trim() != "")
                    CUserName = CName;
                else
                    CUserName = Cmail;

                EmailSubject = string.Format(NotifyMsg.NewCommentSubject, TaskID, CUserName);
                Msg = string.Format(NotifyMsg.NewCommentMsg, CUserName);
                #region Mail to All Task Team
                // string Message = MySqlClass.ReturnSingleValue(string.Format("select Comments  from taskcomments where CommentID={0}", MessageID));
                //  DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {
                    //
                    //  get the email id for this user
                    //             
                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithNewCommentMsgforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, CUserName, LastestComment, RedirectPath, AccountName, AllSubscriberToUsers);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescriptionforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, CUserName, LastestComment, RedirectPath, AccountName, AllSubscriberToUsers);

                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithNewCommentMsg, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, CUserName, LastestComment, RedirectPath, AccountName);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescription, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, CUserName, LastestComment, RedirectPath, AccountName);
                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);


                    // NotificationMessageTeam.AppendFormat(NotifyMsg.NewCommentMsg + NotifyMsg.OpenMsg, trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                    // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Replace("TaskTrek Lite:  ", "").Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessageTeam, AccountName, AccountName, AccountName);
                    SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");
                }
            }
                #endregion
        }
        //
        // mail for  work item assigment
        //
        public static void NotifyNewTaskAssignedTo(int TaskID, int UserId, string Domain)
        {
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            //DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("Select * from Tasks where TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetOnlyOwnerList", TaskID, UserId);
            DataTable dtSubscribeListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetSubscribedUserList", TaskID, UserId);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
            string CUserName = "";

            string highpri = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
            }
            #region set Email subject
            string EmailSubject = string.Empty;
            string EmailSubjectforsubscriber = string.Empty;
            #endregion
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                EmailSubject = string.Format(NotifyMsg.TaskResponsibleSubject);
                EmailSubjectforsubscriber = string.Format(NotifyMsg.TaskResponsibleSubjectforsubscriber);
                string CName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UserId));
                string Cmail = MySqlClass.ReturnSingleValue(string.Format("select Email  from users where UserID={0}", UserId));
                if (CName.Trim() != "")
                    CUserName = CName;
                else
                    CUserName = Cmail;

                string Msg = string.Format(NotifyMsg.TaskResponsibleMsg, CUserName);
                string Msgforsubscribe = string.Format(NotifyMsg.TaskResponsibleMsgforsubscrib, CUserName);

                #region Mail to All Task Team
                // string Message = MySqlClass.ReturnSingleValue(string.Format("select Comments  from taskcomments where CommentID={0}", MessageID));
                // DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {
                    //
                    //  get the email id for this user
                    //

                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);


                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);


                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);
                    // NotificationMessageTeam.AppendFormat(NotifyMsg.TaskResponsibleMsg + NotifyMsg.OpenMsg, trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                    // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Replace("TaskTrek Lite:  ", "").Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessageTeam, AccountName, AccountName, AccountName);
                    if (highpri != "")
                        SendNotifyWithPriority(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "", true);
                    else
                        SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");
                }

                foreach (DataRow trail in dtSubscribeListWithCreatedBy.Rows)
                {
                    //
                    //  get the email id for this user
                    //

                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n", "<br/>");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msgforsubscribe, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msgforsubscribe, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);


                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n", "<br/>");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msgforsubscribe, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msgforsubscribe, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);

                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubjectforsubscriber.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);
                    // NotificationMessageTeam.AppendFormat(NotifyMsg.TaskResponsibleMsg + NotifyMsg.OpenMsg, trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                    // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Replace("TaskTrek Lite:  ", "").Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessageTeam, AccountName, AccountName, AccountName);
                    if (highpri != "")
                        SendNotifyWithPriority(NotificationMessageFinal, EmailSubjectforsubscriber, trail["Email"].ToString(), "", "", "", true);
                    else
                        SendNotify(NotificationMessageFinal, EmailSubjectforsubscriber, trail["Email"].ToString(), "", "", "");
                }

            }
                #endregion
        }
        public static void NotifyTaskTimesheetUpdated(int TaskID, int UserId, string Domain)
        {
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);
            string CUserName = "";

            string highpri = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
            }
            #region set Email subject
            string EmailSubject = string.Empty;
            #endregion
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                EmailSubject = string.Format(NotifyMsg.TaskTimeSheetUpdatedSubject, dtTask.Rows[0]["Title"]);
                string CName = MySqlClass.ReturnSingleValue(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName'  from users where UserID={0}", UserId));
                string Cmail = MySqlClass.ReturnSingleValue(string.Format("select Email  from users where UserID={0}", UserId));
                if (CName.Trim() != "")
                    CUserName = CName;
                else
                    CUserName = Cmail;

                string Msg = string.Format(NotifyMsg.TaskTimeSheetUpdatedMsg, CUserName);
                #region Mail to All Task Team
                // string Message = MySqlClass.ReturnSingleValue(string.Format("select Comments  from taskcomments where CommentID={0}", MessageID));
                //  DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserId);
                foreach (DataRow trail in dtAssignedListWithCreatedBy.Rows)
                {
                    //
                    //  get the email id for this user
                    //             
                    StringBuilder NotificationMessageTeam = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    // NotificationMessageTeam.AppendFormat(, trail["UserName"].ToString(), dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                    // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Replace("TaskTrek Lite:  ", "").Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessageTeam, AccountName, AccountName, AccountName);
                    if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);

                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);

                    }
                    else
                    {
                        if (dtTask.Rows[0]["Description"].ToString() != "")
                        {
                            string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);

                        }
                        else
                            NotificationMessageTeam.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                    }
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, trail["UserName"].ToString(), NotificationMessageTeam, AccountName, AccountName, AccountName, AccountName, AccountName);
                    SendNotify(NotificationMessageFinal, EmailSubject, trail["Email"].ToString(), "", "", "");
                }
            }
                #endregion
        }
        //
        // Task Removel email
        //
        public static void NotifyTaskResponsibleRemoval(int TaskID, int UserID, int CUserID, string Domain)
        {
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessageFinal = new StringBuilder();

            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            //  DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserID);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);

            string highpri = "";
            string Msg = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' />", AccountName);
            }


            string EmailSubject = string.Empty;
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                DataTable dtuser = MySqlClass.ReturnDataTable(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName',Email  from users where UserID={0}", CUserID));
                DataTable dtRemovalUser = MySqlClass.ReturnDataTable(string.Format("select coalesce(Fname,'') as 'UserName',Email  from users where UserID={0}", UserID));

                EmailSubject = string.Format(NotifyMsg.ResponsibleRemovalSubject);
                if (dtuser.Rows.Count > 0)
                {
                    if (dtuser.Rows[0]["UserName"].ToString().Trim() != "")
                        Msg = string.Format(NotifyMsg.ResponsibleRemovalMsg, dtuser.Rows[0]["UserName"].ToString());
                    else
                        Msg = string.Format(NotifyMsg.ResponsibleRemovalMsg, dtuser.Rows[0]["Email"].ToString());
                }
                if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, "", DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);

                }
                else
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                }
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, dtRemovalUser.Rows[0]["UserName"].ToString(), NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);

                // NotificationMessage.AppendFormat(NotifyMsg.ResponsibleRemovalMsg + NotifyMsg.OpenMsg, dtuser.Rows[0]["UserName"], dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Replace("TaskTrek Lite:  ", "").Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessage, AccountName, AccountName, AccountName);
                if (CUserID != UserID)
                {
                    SendNotify(NotificationMessageFinal, EmailSubject, dtRemovalUser.Rows[0]["Email"].ToString(), "", "", "");
                }

            }
        }
        //
        //due date notification
        //
        //due date notification
        //
        public static void NotifyTaskDueDate()
        {
            string Cdate = DateTime.Now.ToString("yyyy-MM-dd");
            //DataTable taskdtt = new DataTable();
            //taskdtt = MySqlClass.ReturnOverDueDateTasksTable("GetDueDateTask", Cdate);
            DataTable preduetask = new DataTable();
            preduetask = MySqlClass.ReturnOverDueDateTasksTable("GetPreviousDueDateTask", Cdate);
            var ListdtobjTasks = preduetask.AsEnumerable().ToList();

            if (ListdtobjTasks != null)
            {
                foreach (DataRow sche in ListdtobjTasks)
                {
                    string TitleFormattedListpremul = string.Empty;
                    string TitleFormattedListttpreone = string.Empty;
                    int Assignedtoidd = Convert.ToInt32(sche["AssignedTo"]);
                    DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("select t.TaskID from taskassignmenttrail a inner join tasks t inner join users u on a.TaskID=t.TaskID and a.AssignedTo=u.UserID where DATE_FORMAT(duedate,'%Y-%m-%d') = '{2}' and a.AssignedTo ={0} and StatusID not in{1} ;", Assignedtoidd, "(12,13,14)", Cdate));
                    DataTable predtTaskdt = MySqlClass.ReturnDataTable(string.Format("select t.TaskID from taskassignmenttrail a inner join tasks t inner join users u on a.TaskID=t.TaskID and a.AssignedTo=u.UserID where DATE_FORMAT(duedate,'%Y-%m-%d') > '{2}' and a.AssignedTo ={0} and StatusID not in{1} ;", Assignedtoidd, "(12,13,14)", Cdate));
                    DataTable dtuserdetail = MySqlClass.ReturnDataTable(string.Format("select coalesce(Fname,'')  as 'UserName',Email  from users where UserID={0}", Assignedtoidd));
                    var ListdtobjdtTask = dtTask.AsEnumerable().ToList();
                    var Listpreduetask = predtTaskdt.AsEnumerable().ToList();

                    if (predtTaskdt.Rows.Count != 0)
                    {
                        if (predtTaskdt.Rows.Count > 1)
                        {

                            string RedirectPath = string.Empty;
                            string AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();

                            int number = 0;
                            string final = string.Empty;
                            foreach (DataRow dtrwpre in Listpreduetask)
                            {
                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrwpre["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, Convert.ToInt32(dtrwpre["TaskID"]));

                                final += string.Format("<a href='{0}' style='color: #686868' target='_blank'>{1}</a>", RedirectPath, datatitle) + "|";
                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {
                                    number = number + 1;
                                    TitleFormattedListpremul += string.Format("{0}. {1}<br/>", number, termsTaskID[i]);

                                }

                            }
                        }
                        else
                        {
                            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string RedirectPathhh = string.Empty;

                            string final = string.Empty;
                            foreach (DataRow dtrwpre in Listpreduetask)
                            {
                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrwpre["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPathhh = string.Format("http://{0}/Login.aspx?t={1}", AccountNameee, Convert.ToInt32(dtrwpre["TaskID"]));
                                final += datatitle + "|";
                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {

                                    TitleFormattedListttpreone += string.Format("{0}<br/>", termsTaskID[i]);

                                }

                            }
                        }
                    }
                    ///////////////
                    if (dtTask.Rows.Count != 0 && predtTaskdt.Rows.Count != 0)
                    {
                        if (dtTask.Rows.Count > 1)
                        {
                            string AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string RedirectPath = string.Empty;
                            StringBuilder NotificationMessage = new StringBuilder();
                            StringBuilder NotificationMessageFinal = new StringBuilder();
                            string EmailSubject = string.Empty;
                            string TitleFormattedList = string.Empty;
                            int number = 0;
                            string final = string.Empty;
                            foreach (DataRow dtrw in ListdtobjdtTask)
                            {

                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrw["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, Convert.ToInt32(dtrw["TaskID"]));
                                //final += datatitle +  RedirectPath+"|";//string.Format("{0}. {1}<br/>", number, termsTaskID[i])
                                final += string.Format("<a href='{0}' style='color: #686868' target='_blank'>{1}</a>", RedirectPath, datatitle) + "|";

                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {
                                    number = number + 1;
                                    TitleFormattedList += string.Format("{0}. {1}<br/>", number, termsTaskID[i]);

                                }

                            }
                            string highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
                            EmailSubject = string.Format(NotifyMsg.ResponsibleDueDateSubject);
                            if (predtTaskdt.Rows.Count > 1)
                            {
                                NotificationMessage.AppendFormat(NotifyMsg.MessageforPendingmany, NotifyMsg.ResponsibleDueDateMsg, TitleFormattedList, RedirectPath, AccountName, TitleFormattedListpremul);
                            }
                            else
                            {
                                NotificationMessage.AppendFormat(NotifyMsg.MessageforPending, NotifyMsg.ResponsibleDueDateMsg, TitleFormattedList, RedirectPath, AccountName, TitleFormattedListttpreone);
                            }
                            NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, dtuserdetail.Rows[0]["UserName"], NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);
                            SendNotifyWithPriority(NotificationMessageFinal, EmailSubject, dtuserdetail.Rows[0]["Email"].ToString(), "", "", "", true);
                        }
                        else
                        {
                            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string RedirectPathhh = string.Empty;
                            StringBuilder NotificationMessageee = new StringBuilder();
                            StringBuilder NotificationMessageFinalll = new StringBuilder();
                            string EmailSubjecttt = string.Empty;
                            string TitleFormattedListtt = string.Empty;
                            string listoftask = string.Empty;
                            // int number = 0;
                            string final = string.Empty;
                            foreach (DataRow dtrw in ListdtobjdtTask)
                            {
                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrw["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPathhh = string.Format("http://{0}/Login.aspx?t={1}", AccountNameee, Convert.ToInt32(dtrw["TaskID"]));
                                final += datatitle + "|";
                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {
                                    // number = number + 1;
                                    TitleFormattedListtt += string.Format("{0}<br/>", termsTaskID[i]);

                                }

                            }
                            string highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' />", AccountNameee);
                            listoftask = string.Format("<a href='{1}' style='color: #686868' target='_blank'>{0}</a>", TitleFormattedListtt, RedirectPathhh);
                            EmailSubjecttt = string.Format(NotifyMsg.ResponsibleDueDateSubjectone);
                            if (predtTaskdt.Rows.Count == 1)
                            {
                                NotificationMessageee.AppendFormat(NotifyMsg.MessageforPending, NotifyMsg.ResponsibleDueDateMsgton, listoftask, RedirectPathhh, AccountNameee, TitleFormattedListttpreone);
                            }
                            else
                            {
                                NotificationMessageee.AppendFormat(NotifyMsg.MessageforPendingmany, NotifyMsg.ResponsibleDueDateMsgton, listoftask, RedirectPathhh, AccountNameee, TitleFormattedListpremul);
                            }
                            NotificationMessageFinalll.AppendFormat(NotifyMsg.FinalMailerString, AccountNameee, AccountNameee, EmailSubjecttt.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountNameee, dtuserdetail.Rows[0]["UserName"], NotificationMessageee, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                            SendNotifyWithPriority(NotificationMessageFinalll, EmailSubjecttt, dtuserdetail.Rows[0]["Email"].ToString(), "", "", "", true);

                        }

                    }
                    else if (dtTask.Rows.Count == 0 && predtTaskdt.Rows.Count != 0)
                    {
                        string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                        string RedirectPathhh = string.Empty;
                        StringBuilder NotificationMessageee = new StringBuilder();
                        StringBuilder NotificationMessageFinalll = new StringBuilder();
                        string EmailSubjecttt = string.Empty;
                        string Subjecttt = string.Empty;
                        string listoftask = string.Empty;

                        string highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' />", AccountNameee);

                        if (predtTaskdt.Rows.Count == 1)
                        {
                            Subjecttt = "Over due task";
                            EmailSubjecttt = string.Format(NotifyMsg.MessageforPendingprev);
                            NotificationMessageee.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.MessageforPendingprev, TitleFormattedListttpreone, RedirectPathhh, AccountNameee);
                        }
                        else
                        {
                            Subjecttt = "Over due tasks";
                            EmailSubjecttt = string.Format(NotifyMsg.MessageforPendingsummul);
                            NotificationMessageee.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.MessageforPendingsummul, TitleFormattedListpremul, RedirectPathhh, AccountNameee);
                        }
                        NotificationMessageFinalll.AppendFormat(NotifyMsg.FinalMailerString, AccountNameee, AccountNameee, Subjecttt.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountNameee, dtuserdetail.Rows[0]["UserName"], NotificationMessageee, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                        SendNotifyWithPriority(NotificationMessageFinalll, Subjecttt, dtuserdetail.Rows[0]["Email"].ToString(), "", "", "", true);


                    }
                    else if (dtTask.Rows.Count != 0 && predtTaskdt.Rows.Count == 0)
                    {

                        if (dtTask.Rows.Count > 1)
                        {
                            string AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string RedirectPath = string.Empty;
                            StringBuilder NotificationMessage = new StringBuilder();
                            StringBuilder NotificationMessageFinal = new StringBuilder();
                            string EmailSubject = string.Empty;
                            string TitleFormattedList = string.Empty;
                            int number = 0;
                            string final = string.Empty;
                            foreach (DataRow dtrw in ListdtobjdtTask)
                            {

                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrw["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, Convert.ToInt32(dtrw["TaskID"]));
                                //final += datatitle +  RedirectPath+"|";//string.Format("{0}. {1}<br/>", number, termsTaskID[i])
                                final += string.Format("<a href='{0}' style='color: #686868' target='_blank'>{1}</a>", RedirectPath, datatitle) + "|";

                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {
                                    number = number + 1;
                                    TitleFormattedList += string.Format("{0}. {1}<br/>", number, termsTaskID[i]);

                                }

                            }
                            string highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle' />", AccountName);
                            EmailSubject = string.Format(NotifyMsg.ResponsibleDueDateSubject);
                            if (predtTaskdt.Rows.Count > 1)
                            {
                                NotificationMessage.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.ResponsibleDueDateMsg, TitleFormattedList, RedirectPath, AccountName);
                            }
                            else
                            {
                                NotificationMessage.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.ResponsibleDueDateMsg, TitleFormattedList, RedirectPath, AccountName);
                            }
                            NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, dtuserdetail.Rows[0]["UserName"], NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);
                            SendNotifyWithPriority(NotificationMessageFinal, EmailSubject, dtuserdetail.Rows[0]["Email"].ToString(), "", "", "", true);
                        }
                        else
                        {
                            string AccountNameee = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string RedirectPathhh = string.Empty;
                            StringBuilder NotificationMessageee = new StringBuilder();
                            StringBuilder NotificationMessageFinalll = new StringBuilder();
                            string EmailSubjecttt = string.Empty;
                            string TitleFormattedListtt = string.Empty;
                            string listoftask = string.Empty;
                            // int number = 0;
                            string final = string.Empty;
                            foreach (DataRow dtrw in ListdtobjdtTask)
                            {
                                DataTable dtTaskallinfo = MySqlClass.ReturnDataTable(string.Format("select * from tasks where TaskID={0};", Convert.ToInt32(dtrw["TaskID"])));
                                var datatitle = dtTaskallinfo.Rows[0]["Title"].ToString();
                                RedirectPathhh = string.Format("http://{0}/Login.aspx?t={1}", AccountNameee, Convert.ToInt32(dtrw["TaskID"]));
                                final += datatitle + "|";
                            }
                            string[] termsTaskID = final.Split('|');
                            if (termsTaskID.Length > 1)
                            {
                                for (int i = 0; i < termsTaskID.Count() - 1; i++)
                                {
                                    // number = number + 1;
                                    TitleFormattedListtt += string.Format("{0}<br/>", termsTaskID[i]);

                                }

                            }
                            string highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' />", AccountNameee);
                            listoftask = string.Format("<a href='{1}' style='color: #686868' target='_blank'>{0}</a>", TitleFormattedListtt, RedirectPathhh);
                            EmailSubjecttt = string.Format(NotifyMsg.ResponsibleDueDateSubjectone);
                            if (predtTaskdt.Rows.Count == 1)
                            {
                                NotificationMessageee.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.ResponsibleDueDateMsgton, listoftask, RedirectPathhh, AccountNameee);
                            }
                            else
                            {
                                NotificationMessageee.AppendFormat(NotifyMsg.MessageforPendingsingle, NotifyMsg.ResponsibleDueDateMsgton, listoftask, RedirectPathhh, AccountNameee);
                            }
                            NotificationMessageFinalll.AppendFormat(NotifyMsg.FinalMailerString, AccountNameee, AccountNameee, EmailSubjecttt.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountNameee, dtuserdetail.Rows[0]["UserName"], NotificationMessageee, AccountNameee, AccountNameee, AccountNameee, AccountNameee, AccountNameee);
                            SendNotifyWithPriority(NotificationMessageFinalll, EmailSubjecttt, dtuserdetail.Rows[0]["Email"].ToString(), "", "", "", true);

                        }

                    }
                    else
                    {
                        //
                    }

                }
            }
        }
        //
        //

        // tASK responsible mail
        //
        public static void NotifyTaskResponsible(int TaskID, int UserID, int CUserID, string Domain)
        {
            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessageFinal = new StringBuilder();
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            // DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("Select * from Tasks where TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            // DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserID);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);

            string highpri = "";
            string Msg = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle'  />", AccountName);
            }
            string EmailSubject = string.Empty;
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                DataTable dtuser = MySqlClass.ReturnDataTable(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName',Email  from users where UserID={0}", CUserID));
                DataTable dtRemovalUser = MySqlClass.ReturnDataTable(string.Format("select coalesce(Fname,'') as 'UserName',Email  from users where UserID={0}", UserID));
                // EmailSubject = string.Format(NotifyMsg.TaskResponsibleSubject, dtTask.Rows[0]["Title"]);
                if (dtuser.Rows[0]["UserName"].ToString().Trim() != "")
                    Msg = string.Format(NotifyMsg.TaskResponsibleMsg, dtuser.Rows[0]["UserName"].ToString().Trim());
                else
                    Msg = string.Format(NotifyMsg.TaskResponsibleMsg, dtuser.Rows[0]["Email"].ToString());

                EmailSubject = string.Format(NotifyMsg.TaskResponsibleSubject);
                if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);

                }
                else
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n ", "\n&nbsp;").Replace("\n", "<br/>").Replace("  ", "&nbsp;&nbsp;");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                }
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, dtRemovalUser.Rows[0]["UserName"].ToString(), NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);



                // NotificationMessage.AppendFormat(NotifyMsg.    NotifyMsg.TaskResponsibleMsg , dtuser.Rows[0]["UserName"], dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessage, AccountName, AccountName, AccountName);
                if (CUserID != UserID)
                {
                    if (highpri != "")
                        SendNotifyWithPriority(NotificationMessageFinal, EmailSubject, dtRemovalUser.Rows[0]["Email"].ToString(), "", "", "", true);
                    else
                        SendNotify(NotificationMessageFinal, EmailSubject, dtRemovalUser.Rows[0]["Email"].ToString(), "", "", "");
                }

            }
        }
        //
        // tASK Subscriberresponsible mail
        //
        public static void NotifyTaskResponsibleSubscriber(int TaskID, int UserID, int CUserID, string Domain)
        {
            StringBuilder NotificationMessage = new StringBuilder();
            StringBuilder NotificationMessageFinal = new StringBuilder();
            string AccountName = "";
            if (Domain != "")
                AccountName = Domain;
            else
                AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
            string RedirectPath = string.Empty;
            // DataTable dtTask = MySqlClass.ReturnDataTable(string.Format("Select * from Tasks where TaskID={0};", TaskID));
            DataTable dtTask = MySqlClass.RetrunTaskDetailsById("GetTaskdetailbyId", TaskID);
            string DueDate = Convert.ToDateTime(dtTask.Rows[0]["DueDate"].ToString()).ToShortDateString();
            // DataTable dtAssignedListWithCreatedBy = MySqlClass.ReturnDtAssignedUserList("GetAssignedUserList", TaskID, UserID);
            string AllAssinedToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasep", TaskID);
            string AllSubscriberToUsers = MySqlClass.RetrunAssignedToById("GetAllAssignedLstwithCommasepforsubscriber", TaskID);

            string highpri = "";
            string Msg = "";
            if (dtTask.Rows[0]["Priority"].ToString().Trim() == "1 day")
            {
                highpri = string.Format("<img src='http://{0}/Images/Icon/alerticon.png' border='0' alt='' style='vertical-align:middle'  />", AccountName);
            }
            string EmailSubject = string.Empty;
            if (dtTask.Rows.Count > 0)
            {
                RedirectPath = string.Format("http://{0}/Login.aspx?t={1}", AccountName, TaskID);
                string titlelink = string.Format("<a href='{0}' style='color:#6A6A6A;color:#A6A6A6;font-size:12px; text-decoration:none'>{1}</a>", RedirectPath, dtTask.Rows[0]["Title"].ToString());
                DataTable dtuser = MySqlClass.ReturnDataTable(string.Format("select CONCAT(coalesce(Fname,''),' ',coalesce(Lname,'')) as 'UserName',Email  from users where UserID={0}", CUserID));
                DataTable dtRemovalUser = MySqlClass.ReturnDataTable(string.Format("select coalesce(Fname,'') as 'UserName',Email  from users where UserID={0}", UserID));
                // EmailSubject = string.Format(NotifyMsg.TaskResponsibleSubject, dtTask.Rows[0]["Title"]);
                if (dtuser.Rows[0]["UserName"].ToString().Trim() != "")
                    Msg = string.Format(NotifyMsg.TaskResponsibleMsgforsubscrib, dtuser.Rows[0]["UserName"].ToString().Trim());
                else
                    Msg = string.Format(NotifyMsg.TaskResponsibleMsgforsubscrib, dtuser.Rows[0]["Email"].ToString());

                EmailSubject = string.Format(NotifyMsg.TaskResponsibleSubjectforsubscriber);
                if (AllSubscriberToUsers != "" && AllSubscriberToUsers != null)
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n", "<br/>");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName, AllSubscriberToUsers);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBoxforsubscriber, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName, AllSubscriberToUsers);

                }
                else
                {
                    if (dtTask.Rows[0]["Description"].ToString() != "")
                    {
                        string descriptionnn = dtTask.Rows[0]["Description"].ToString().Replace("\n", "<br/>");
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, descriptionnn, RedirectPath, AccountName);
                    }
                    else
                        NotificationMessage.AppendFormat(NotifyMsg.MessageWithoutDescAndCommentBox, Msg, titlelink, dtTask.Rows[0]["CreatedBy"].ToString(), DueDate, dtTask.Rows[0]["Priority"].ToString(), AllAssinedToUsers, RedirectPath, AccountName);
                }
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, AccountName, AccountName, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), highpri, AccountName, dtRemovalUser.Rows[0]["UserName"].ToString(), NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);



                // NotificationMessage.AppendFormat(NotifyMsg.    NotifyMsg.TaskResponsibleMsg , dtuser.Rows[0]["UserName"], dtTask.Rows[0]["Title"], RedirectPath, RedirectPath);
                // NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerString, EmailSubject.Trim(), Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), NotificationMessage, AccountName, AccountName, AccountName);
                if (CUserID != UserID)
                {
                    if (highpri != "")
                        SendNotifyWithPriority(NotificationMessageFinal, EmailSubject, dtRemovalUser.Rows[0]["Email"].ToString(), "", "", "", true);
                    else
                        SendNotify(NotificationMessageFinal, EmailSubject, dtRemovalUser.Rows[0]["Email"].ToString(), "", "", "");
                }

            }
        }
        //sendnotification invite user
        public static void InviteUserNotification(string email, string pswd)
        {
            try
            {
                string AccountName = ConfigurationManager.AppSettings["DomainName"].ToString();
                string subject = string.Empty;
                // DataTable TaskDetail = MySqlClass.ReturnDataTable(string.Format("select *  from tasks where TaskID={0}", ));
                string Title = string.Empty;
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessageFinal = new StringBuilder();
                string litelink = string.Empty;
                string loginworkslink = string.Empty;
                litelink = "<a href='http://lite.loginworks.com' style='color: Blue;text-decoration:none' target='_blank'>http://lite.loginworks.com</a>";
                loginworkslink = "<a href='mailto:contact@loginworks.com' style='color: #686868;text-decoration:none' target='_blank'>contact@loginworks.com</a>";
                Title = "Welcome to TaskTrek Lite, an online collaboration software dedicated to task management.";
                subject = "Welcome to TaskTrek Lite";

                NotificationMessage.AppendFormat(NotifyMsg.Inviteusernotifymsg, Title, litelink, email, pswd, loginworkslink);
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerStringforInviteUser, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName);

                SendNotify(NotificationMessageFinal, subject, email, "", "", "");
            }
            catch (Exception ex)
            {

            }


        }
        //randompassword
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        ////Invite User
        public static int InsertInviteUser(string E_mail)
        {
            string uname = "";
            string pwd = "";
            string email = "";
            string crdate = "";
            int tid = 17;
            string tzone = "+05:30";
            int user_id = 0;
            try
            {
                pwd = CreatePassword(6);
                crdate = DateTime.Now.ToString("yyyy-MM-dd");
                //string[] aruname = E_mail.Split('@');
                //string[] finaluname = (aruname[0].Replace(".", "_").Replace("-","_").Split('_'));
                //string nnames = finaluname[0];
                //uname = nnames.Substring(0,5);


                if (E_mail != null && pwd != null)
                {
                    int result = MySqlClass.Insert_Update1(string.Format("Insert into users (UserName,Password,Email,CreatedDate,TypeID,TimeZone) values('{0}','{1}','{2}','{3}',{4},'{5}')", "", pwd, E_mail, crdate, tid, tzone));
                    if (result == 0)
                        user_id = InsertUserprefer(E_mail, tid);

                    if (user_id > 0)
                        InviteUserNotification(E_mail, pwd);

                }

            }
            catch (Exception ex)
            {

            }
            return user_id;


        }

        //insertnewuser in userpreference
        public static int InsertUserprefer(string email, int tid)
        {
            try
            {
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select UserID  from users where Email='{0}' and TypeID={1}", email, tid));
                int u_id = Convert.ToInt32(dtpreference.Rows[0]["UserID"]);
                int result = MySqlClass.Insert_Update1(string.Format("INSERT INTO userpreferences (UserID, IsAllowAdTab, IsAllowOnlyWebScrapes) values('{0}',{1},{2})", u_id, 0, 0));
                return u_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //update user pending status
        public static void UpdatePendingStatus(int u_id, int type_id)
        {
            int result = MySqlClass.Insert_Update1(string.Format("update users set IsPending=0 where UserID={0} and TypeID={1}", u_id, type_id));
        }

        //inviteuserwithform
        public static int InsertInviteUserusingForm(string email, string name, string timezone, int teamid, bool ads, bool webscrap)
        {

            string pwd = "";
            string fname = "";
            string lname = "";
            string crdate = "";
            int tid = 17;
            int user_id = 0;
            string[] namearray = name.Split(' ');
            if (namearray.Length > 1)
            {
                fname = namearray[0];
                lname = namearray[1];
            }
            else
            {
                fname = name;
                lname = "";
            }
            try
            {
                pwd = CreatePassword(6);
                crdate = DateTime.Now.ToString("yyyy-MM-dd");


                if (email != null && pwd != null)
                {
                    int result = MySqlClass.Insert_Update1(string.Format("Insert into users (UserName,Password,Email,CreatedDate,TypeID,TimeZone,TeamID,Fname,Lname) values('{0}','{1}','{2}','{3}',{4},'{5}',{6},'{7}','{8}')", name, pwd, email, crdate, tid, timezone, teamid, fname, lname));
                    if (result == 0)
                        user_id = InsertUserpreferrwithform(email, tid, ads, webscrap);

                    if (user_id > 0)
                        InviteUserNotificationwithform(email, pwd, name);

                }

            }
            catch (Exception ex)
            {

            }
            return user_id;


        }
        ////InsertUserpreferrwithform
        public static int InsertUserpreferrwithform(string email, int tid, bool ads, bool webscrap)
        {
            try
            {
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select UserID  from users where Email='{0}' and TypeID={1}", email, tid));
                int u_id = Convert.ToInt32(dtpreference.Rows[0]["UserID"]);
                int result = MySqlClass.Insert_Update1(string.Format("INSERT INTO userpreferences (UserID, IsAllowAdTab, IsAllowOnlyWebScrapes) values('{0}',{1},{2})", u_id, ads, webscrap));
                return u_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //sendnotification invite userwithform
        public static void InviteUserNotificationwithform(string email, string pswd, string name)
        {
            try
            {
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString();
                string subject = string.Empty;
                // DataTable TaskDetail = MySqlClass.ReturnDataTable(string.Format("select *  from tasks where TaskID={0}", ));
                string Title = string.Empty;
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessageFinal = new StringBuilder();
                string litelink = string.Empty;
                string loginworkslink = string.Empty;
                litelink = "<a href='http://tasktrek.in' style='color: Blue;text-decoration:none' target='_blank'>http://tasktrek.in</a>";
                loginworkslink = "<a href='mailto:contact@loginworks.com' style='color: #686868;text-decoration:none' target='_blank'>contact@loginworks.com</a>";
                //Title = "Welcome to TaskTrek Lite, an online collaboration software dedicated to task management.";
                subject = "TTLite | Invitation ";

                NotificationMessage.AppendFormat(NotifyMsg.inviteuseronLWS, name, litelink, email, pswd, loginworkslink);
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                SendNotify(NotificationMessageFinal, subject, email, "", "", "");
            }
            catch (Exception ex)
            {

            }


        }

        public static void InsertLogDetail(string Emsg, int uid, string eline, string filename, string mailsub, string mailmsg, string Tomid, string Ccmid)
        {
            string ipp = GetLocalIPAddress();
            MySqlClass.Insert_Update(string.Format("Insert into uploadscheduleerrorlog (CreatedDate,ErrorMsg,UserId,MethodName,ErrorAtLine,TargetFileName,MailSubject,MailMessage,CS_IP,MailTo,MailCc) values ('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Emsg, uid, "", eline, filename, mailsub, mailmsg, ipp, Tomid, Ccmid));

        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        /// Created by:- Manoranjan Tiwari
        /// Created Date:- 05-Oct-2016
        /// Description:-Use for send a mail to invite a non existing user on board.
        public static int InviteNonExistingUserOnBoardNotify(string ToEmail, int BoardId)
        {

            try
            {
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select *  from users where Email='{0}'", ToEmail));
                int u_id = Convert.ToInt32(dtpreference.Rows[0]["UserID"]);
                string UserName = Convert.ToString(dtpreference.Rows[0]["Email"]);
                string Password = Convert.ToString(dtpreference.Rows[0]["Password"]);
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString();
                string subject = string.Empty;
                string Title = string.Empty;
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessageFinal = new StringBuilder();
                string RedirectPath = string.Empty;
                string loginworkslink = string.Empty;
                RedirectPath = string.Format("http://{0}/Login.aspx?BID={1}", AccountName, BoardId);
                string titlelink = string.Format("<a href='{0}' style='color:#DC143C;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", RedirectPath, "Invited on board");
                // Title = "Welcome to TaskTrek Lite, an online collaboration software dedicated to task management.";
                loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                subject = "TTLite | Board Invitation ";

                NotificationMessage.AppendFormat(NotifyMsg.InviteuserOnBoardnotifyCustommsg, titlelink, ToEmail, Password, loginworkslink);
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                SendNotify(NotificationMessageFinal, subject, ToEmail, "", "", "");
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// Created by:- Manoranjan Tiwari
        /// Created Date:- 05-Oct-2016
        /// Description:-Use for send a mail to existing invitee user on board.
        public static void InviteExistingUserOnBoardNotify(string ToEmail, int BoardId)
        {

            try
            {
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select *  from users where Email='{0}'", ToEmail));
                int u_id = Convert.ToInt32(dtpreference.Rows[0]["UserID"]);
                string UserName = Convert.ToString(dtpreference.Rows[0]["Email"]);
                string Password = Convert.ToString(dtpreference.Rows[0]["Password"]);
                string fname = Convert.ToString(dtpreference.Rows[0]["fname"]);
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";
                string subject = string.Empty;
                string Title = string.Empty;
                StringBuilder NotificationMessage = new StringBuilder();
                StringBuilder NotificationMessageFinal = new StringBuilder();
                string RedirectPath = string.Empty;
                string loginworkslink = string.Empty;
                RedirectPath = string.Format("http://{0}/Login.aspx?BID={1}", AccountName, BoardId);
                string titlelink = string.Format("<a href='{0}' style='color:#DC143C;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", RedirectPath, "Invited on board");
                // Title = "Welcome to TaskTrek Lite, an online collaboration software dedicated to task management.";
                loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                subject = "TTLite | Board Invitation ";

                NotificationMessage.AppendFormat(NotifyMsg.InviteExistinguserOnBoardnotifyCustommsg, titlelink, UserName, fname, loginworkslink);
                NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                SendNotify(NotificationMessageFinal, subject, ToEmail, "", "", "");
            }
            catch (Exception ex)
            {

            }
        }

        /// Created by:-Deepak sharma
        /// Created Date:- 16-March-2017
        /// Description:-Use for send a mail of attachment added on board.
        public static void SendMailForBoardAttachment(string boarname, string filename, int boardid, string firstname)
        {

            try
            {
                string subject = "TTLite  | Attachment Added on Board";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select fname,email from assignedmembertoteam t join users u on t.MemberID=u.UserID where t.TeamId  in (select TeamId from boards where boardid={0});", boardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnBoardnotifyAttachment, fname, boarname, filename, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"));
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }


        /// Created by:-Deepak sharma
        /// Created Date:- 17-March-2017
        /// Description:-Use for send a mail of attachment added on Card.
        public static void SendMailForCardAttachment(string boarname, string filename, int boardid, string firstname, string cardname, int taskid)
        {

            try
            {
                string subject = "TTLite | New Attachment Added to Card";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM taskassignmenttrail t join users u on t.AssignedTo=u.userid where taskid={0};", taskid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyAttachment, fname, boarname, filename, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"), cardname);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }


        /// Created by:-Deepak sharma
        /// Created Date:- 16-March-2017
        /// Description:-Use for send a mail of message added on board.
        public static void SendMailForBoardMessage(string boarname, string msg, int boardid, string firstname)
        {

            try
            {
                string subject = "TTLite | New Message Added to Board";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select fname,email from assignedmembertoteam t join users u on t.MemberID=u.UserID where t.TeamId  in (select TeamId from boards where boardid={0});", boardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnBoardnotifyMessage, fname, boarname, msg, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"));
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }


        /// Created by:-Deepak sharma
        /// Created Date:- 17-March-2017
        /// Description:-Use for send a mail of message added on card.
        public static void SendMailForCardMessage(string boarname, string cardname, int cardid, string firstname, string msg)
        {

            try
            {
                string subject = " TTLite | New Comment Added to Card";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM taskassignmenttrail t join users u on t.AssignedTo=u.userid where taskid={0};", cardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyMessage, fname, boarname, msg, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"), cardname);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }


        /// Created by:-Deepak sharma
        /// Created Date:- 18-March-2017
        /// Description:-Use for send a mail of message added on card.
        public static void SendMailForCardMemberAdded(string boarname, string cardname, int cardid, string firstname, string member)
        {

            try
            {
                string subject = "TTLite | New Card Assigned";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM taskassignmenttrail t join users u on t.AssignedTo=u.userid where taskid={0};", cardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyMember, fname, boarname, member, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"), cardname);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }



        /// Created by:-Deepak sharma
        /// Created Date:- 18-March-2017
        /// Description:-Use for send a mail of Archive  Board.
        public static void SendMailForArchiveBoard(string boarname, int boardid, string firstname, string reason)
        {

            try
            {
                string subject = "TTLite | Board Archived";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select fname,email from assignedmembertoteam t join users u on t.MemberID=u.UserID where t.TeamId  in (select TeamId from boards where boardid={0})union select fname,email from boards b join users u on b.ownerid=u.userid where  boardid={0} union select fname,email from boards b join users u on b.createdby=u.userid where  boardid={0};", boardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyArchiveBoard, fname, boarname, firstname, loginworkslink, DateTime.Now.ToString("dd/MM/yyyy"), reason);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// Created by:-Deepak sharma
        /// Created Date:- 20-March-2017
        /// Description:-Use for send a mail of Complete  Board.
        public static void SendMailForCompleteBoard(string boarname, int boardid, string firstname)
        {

            try
            {
                string subject = "TTLite | Board Archived Completely";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("select fname,email from assignedmembertoteam t join users u on t.MemberID=u.UserID where t.TeamId  in (select TeamId from boards where boardid={0})union select fname,email from boards b join users u on b.ownerid=u.userid where  boardid={0} union select fname,email from boards b join users u on b.createdby=u.userid where  boardid={0};", boardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyCompleteBoard, fname, boarname, firstname, loginworkslink, DateTime.Now.ToString("dd/MM/yyyy"));
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// Created by:-Deepak sharma
        /// Created Date:- 23-March-2017
        /// Description:-Use for send a mail of change team.
        public static void SendMailForChangeTeam(string boarname, int teamid, string firstname, string teamname)
        {

            try
            {
                string subject = "TTLite | Board Team Added / Updated";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";

                String emailsOfUsers = "";

                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM assignedmembertoteam t join users u on t.MemberID=u.userid where t.teamid={0} group by MemberID;", teamid));

                for (int i = 0; i < dtpreference.Rows.Count; i++)
                {
                    if (i < dtpreference.Rows.Count - 1)
                    {
                        emailsOfUsers += Convert.ToString(dtpreference.Rows[i].ItemArray[1]) + ", ";
                    }
                    else
                    {
                        emailsOfUsers += Convert.ToString(dtpreference.Rows[i].ItemArray[1]);
                    }
                }

                foreach (DataRow data in dtpreference.Rows)
                {

                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.SendMailForChangeTeam, fname, boarname, firstname, teamname, loginworkslink, emailsOfUsers);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }
		


        /// Created by:-Deepak sharma
        /// Created Date:- 18-March-2017
        /// Description:-Use for send a mail of Archived  card.
        public static void SendMailForCardArchived(string boarname, string cardname, int cardid, string firstname, int boardid)
        {

            try
            {
                string subject = "TTLite | Archived Card on Board";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";


                DataTable ownerinfo = MySqlClass.ReturnDataTable(string.Format("select Email from boards b join users u on b.ownerid=u.userid where  boardid={0});", boardid));

                foreach (DataRow data in ownerinfo.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyArchivedCard, fname, boarname, cardname, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"));
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }




                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM taskassignmenttrail t join users u on t.AssignedTo=u.userid where taskid={0};", cardid));

                foreach (DataRow data in dtpreference.Rows)
                {
                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.sendMailToUserOnCardnotifyArchivedCard, fname, boarname, cardname, loginworkslink, firstname, DateTime.Now.ToString("dd/MM/yyyy"));
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// Created by:-Deepak sharma
        /// Created Date:- 31-March-2017
        /// Description:-Use for send a mail for New Board Created.
        public static void SendMailForboardcreateToTeam(string boarname, int teamid, string firstname)
        {

            try
            {
                string subject = "TTLite | New Board Created";
                string loginworkslink = string.Format("<a href='{0}' style='color:#2196F3;color:#DC143C;font-size:12px; text-decoration:underline'>{1}</a>", "customerservice@loginworks.com", "customerservice@loginworks.com");
                string AccountName = ConfigurationManager.AppSettings["RedictionUrl"].ToString(); //"localhost:53412";
                DataTable dtpreference = MySqlClass.ReturnDataTable(string.Format("SELECT fname,email FROM assignedmembertoteam t join users u on t.MemberID=u.userid where t.teamid={0} group by MemberID;", teamid));

                foreach (DataRow data in dtpreference.Rows)
                {

                    string fname = Convert.ToString(data["fname"]);
                    string email = Convert.ToString(data["email"]);
                    StringBuilder NotificationMessage = new StringBuilder();
                    StringBuilder NotificationMessageFinal = new StringBuilder();

                    NotificationMessage.AppendFormat(NotifyMsg.ResponsibleSendemail, fname, boarname, firstname, DateTime.Now.ToString("dd/MM/yyyy"), loginworkslink);
                    NotificationMessageFinal.AppendFormat(NotifyMsg.FinalMailerCustomStringforInviteUserOnBoard, AccountName, AccountName, subject, Util.ReturnfomattedDate(DateTime.Now, "dddd, dnn MMMM yyyy"), AccountName, NotificationMessage, AccountName, AccountName, AccountName, AccountName, AccountName, "");

                    SendNotify(NotificationMessageFinal, subject, email, "", "", "");
                }

            }
            catch (Exception ex)
            {

            }
        }

    }
}
