using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Web;


namespace TxlMvc.Helper
{
    /// <summary>
    /// 描述：发送邮件类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="to">收件人</param>
        /// <param name="from">发件人，可以为空</param>
        public static void SendMail(string subject, string body, string to, string from)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(to));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    if (!string.IsNullOrEmpty(from))
                        mail.From = new MailAddress(from);

                    SmtpClient sc = new SmtpClient();
                    try
                    {
                        sc.Send(mail);
                    }
                    catch (SmtpException ex)
                    {
                        //Log4netHelper.Error(string.Format("发送邮件失败 SmtpException！\\nTO:{0}\\nSubject:{1}\\nFROM:{2}", to, subject, from), ex);

                        if (ex.StatusCode == SmtpStatusCode.InsufficientStorage)
                        {
                            sc.Send(mail);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script type='text/javascrip'>alert('发送失败" + ex.Message + "')</script>");
                    //Log4netHelper.Error(string.Format("发送邮件失败！\\nTO:{0}\\nSubject:{1}\\nFROM:{2}", to, subject, from), ex);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="to">收件人</param>
        public static void SendMail(string subject, string body, string to)
        {
            SendMail(subject, body, to, null);
        }
    }
}
