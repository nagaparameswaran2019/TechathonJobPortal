using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CampusRecruitment.Utils.Common;

namespace CampusRecruitment.EmailService.Helper
{

    public class EmailServiceHelper
    {

        public static SmtpClient GetEmailSender()
        {
            string fromEmail = AppSetting.GetConfigValue("SmtpFromEmail");
            string hostAddress = AppSetting.GetConfigValue("SmtpHostAddress");
            int hostPort = AppSetting.GetConfigValue<int>("SmtpHostPort");
            string userName = AppSetting.GetConfigValue("SmtpUserName");
            string password = AppSetting.GetConfigValue("SmtpPassword");
            bool sslEnabled = AppSetting.GetConfigValue<bool>("SmtpSslEnabled");

            if (String.IsNullOrEmpty(fromEmail) || String.IsNullOrEmpty(hostAddress))
            {
                string errorMsg = "Invalid configuration for email setting.Please check 'SmtpFromEmail' and 'SmtpHostAddress' entries are available and valid in application configuration file.";
                throw new ArgumentException(errorMsg);
            }

            SmtpClient smtpClient = null;
            if (hostPort > 0)
                smtpClient = new SmtpClient(hostAddress, hostPort);
            else
                smtpClient = new SmtpClient(hostAddress);

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                smtpClient.Credentials = new NetworkCredential(userName, password);
            }

            smtpClient.EnableSsl = sslEnabled;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            return smtpClient;
        }


        public static List<string> GetCCEmailList(List<string> email, bool isBCCEmail)
        {
            string defaultEmail = string.Empty;
            if (isBCCEmail)
                defaultEmail = AppSetting.GetConfigValue("DefaultBccEmails");
            else
                defaultEmail = AppSetting.GetConfigValue("DefaultCcEmails");

            if (!string.IsNullOrEmpty(defaultEmail))
            {
                defaultEmail = defaultEmail.Replace(";", ",");

                foreach (var item in defaultEmail.Split(','))
                {
                    email.Add(item);
                }
            }

            return email;
        }
    }

    #region Classes       


    public class EmailLogInfo
    {

        public MailMessage MailMessage { get; set; }

        public long LogID { get; set; }
    }

    #endregion

    #region Enumerations

    public enum EmailStatus
    {
        InProgress = 1,
        Success = 2,
        Failed = 3
    }
    #endregion
}