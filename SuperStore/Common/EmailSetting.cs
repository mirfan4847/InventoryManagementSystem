using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SuperStore.Common
{
    public class EmailSetting
    {
        public static void SendEmailReg(string FromEmail, string ToEmail, StringBuilder SubjectEmail, StringBuilder BodyEmail, string replyTo = "", params string[] ccEmail)
        {

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromEmail);
                string subEmail = SubjectEmail.ToString();
                string BoEmail = BodyEmail.ToString();
                message.Subject = subEmail;
                message.Body = BoEmail;
                string CustomEmailEnabled = ConfigurationManager.AppSettings["CustomEmailEnabled"];
                if (!string.IsNullOrEmpty(CustomEmailEnabled))
                {
                    message.To.Add(new MailAddress(CustomEmailEnabled, ToEmail));
                }
                else
                {
                    message.To.Add(new MailAddress(ToEmail));
                }
                if (ccEmail != null)
                {
                    if (ccEmail.Length > 0)
                    {
                        foreach (var ccE in ccEmail)
                        {
                            if (!string.IsNullOrEmpty(ccE))
                            {
                                message.CC.Add(ccE);
                            }
                        }
                    }
                }
                //subject.Replace("\r\n", " ");
                //MailMessage message = new MailMessage(FromEmail, ToEmail, subEmail, BoEmail);
                if (!string.IsNullOrEmpty(replyTo))
                {
                    message.ReplyToList.Add(new MailAddress(replyTo));
                }
                //message.Bcc.Add(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SignUpBcc"]));
                //Dim mes As New MailMessage(
                SmtpClient emailClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["smtpServer"]);
                emailClient.Port = 587;
                // Dim SMTPUserInfo As New System.Net.NetworkCredential("tickets@liveadmins.com", "liveTickets123")
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["UserNameReg"], System.Configuration.ConfigurationManager.AppSettings["PasswordReg"]);

                emailClient.UseDefaultCredentials = true;
                emailClient.Credentials = SMTPUserInfo;
                emailClient.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]);
                message.IsBodyHtml = true;
                var testResult = System.Configuration.ConfigurationManager.AppSettings["IsEmailEnabled"].ToString();

                if (testResult == "True")
                    emailClient.Send(message);
            }
            catch (Exception blExceptions)
            {
                throw (blExceptions);
            }
        }

    }
}