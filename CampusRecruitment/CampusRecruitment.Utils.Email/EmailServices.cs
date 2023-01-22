using CampusRecruitment.EmailService.Helper;
using CampusRecruitment.Utils.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CampusRecruitment.EmailService
{

    public static class EmailService
    {

        public static void SendEmail(EmailInfo emailParam)
        {
            Thread T = new Thread(delegate ()
            {
                sendEmailAsync(emailParam);
            });
            T.Start();
        }


        private static void sendEmailAsync(EmailInfo emailParam)
        {
            string fromEmail = string.IsNullOrEmpty(emailParam.FromEmail) ? AppSetting.GetConfigValue("SmtpFromEmail") : emailParam.FromEmail;
            string fromDisplayName = string.IsNullOrEmpty(emailParam.FromDisplayName) ? AppSetting.GetConfigValue("SmtpFromDisplayName") : emailParam.FromDisplayName;
            string statusMsg = string.Empty;

            validateEmailLogInfo(emailParam, fromEmail);

            SmtpClient client = EmailServiceHelper.GetEmailSender();
            MailMessage mailMessage = new MailMessage();

            foreach (var ccEmail in emailParam.CCEmail)
            {
                mailMessage.CC.Add(ccEmail);
            }

            emailParam.BCCEmail = EmailServiceHelper.GetCCEmailList(emailParam.BCCEmail, true);
            foreach (var bccEmail in emailParam.BCCEmail)
            {
                mailMessage.Bcc.Add(bccEmail);
            }

            if (emailParam.AttachmentPath != null)
            {
                foreach (var attachement in emailParam.AttachmentPath)
                {
                    Attachment attach = new Attachment(attachement);
                    mailMessage.Attachments.Add(attach);
                }
            }

            foreach (var toEmail in emailParam.ToEmail)
            {
                mailMessage.To.Add(toEmail);
            }

            mailMessage.From = new MailAddress(fromEmail, fromDisplayName);
            mailMessage.Body = emailParam.Body;
            mailMessage.IsBodyHtml = emailParam.IsBodyHtml;
            mailMessage.Subject = emailParam.Subject;


            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

            EmailLogInfo logInfo = new EmailLogInfo();
            logInfo.MailMessage = mailMessage;


            try
            {
                client.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    statusMsg += string.Format("Failed to deliver message to {0}", ex.InnerExceptions[i].FailedRecipient);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private static void sendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            EmailLogInfo mail = e.UserState as EmailLogInfo;
            short emailStatus = (short)EmailStatus.Success;
            string statusMsg = "Mail Send Successfully";

            if (e.Error != null)
            {
                emailStatus = (short)EmailStatus.Failed;
                statusMsg = e.Error.Message;
            }

            //EmailServiceHelper.UpdateLogEmail(mail.LogID, emailStatus, statusMsg);
        }


        private static void validateEmailLogInfo(EmailInfo emailParam, string fromEmail)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (emailParam == null)
                throw new ArgumentNullException("EmailInfo");
            else if (string.IsNullOrEmpty(fromEmail.Trim()))
                throw new ArgumentNullException("FromEmail");
            else if (!Regex.IsMatch(fromEmail.Trim(), pattern))
                throw new Exception("FromEmail not valid");
            else if (emailParam.ToEmail != null && emailParam.ToEmail.Count <= 0)
                throw new ArgumentNullException("ToEmail");
            else if (string.IsNullOrEmpty(emailParam.Subject.Trim()))
                throw new ArgumentNullException("Email Subject");
            else if (string.IsNullOrEmpty(emailParam.Body.Trim()))
                throw new ArgumentNullException("Email Body");
        }

    }


    public class EmailInfo
    {
        public EmailInfo()
        {
            BCCEmail = new List<string>();
            CCEmail = new List<string>();
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string ReferenceKey1 { get; set; }
        public string ReferenceKey2 { get; set; }
        public string ReferenceKey3 { get; set; }
        public string ReferenceType1 { get; set; }
        public string ReferenceType2 { get; set; }
        public string ReferenceType3 { get; set; }
        public List<string> BCCEmail { get; set; }
        public List<string> CCEmail { get; set; }
        public List<string> ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string FromDisplayName { get; set; }
        public List<string> AttachmentPath { get; set; }
        public bool IsBodyHtml { get; set; }
        public int? ResendCount { get; set; }
    }
}