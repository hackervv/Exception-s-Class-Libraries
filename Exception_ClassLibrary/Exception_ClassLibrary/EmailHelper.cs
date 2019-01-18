using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Exception_ClassLibrary
{
    public class EmailHelper
    {
        //发送邮箱的host
        private static readonly string _host = "smtp.qq.com";
        //发件人
        private static readonly string _from = "461803297@qq.com";
        /// <summary>
        /// 发送邮件公用方法
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="cc">抄送</param>
        /// <param name="bcc">密抄</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">邮件内容是否为html</param>
        /// <param name="attachments">附件</param>
        public static void SendMail(string to,string cc,string bcc,string subject,string body,bool isBodyHtml,List<string> attachments)
        {
            try
            {
                using(var message = new MailMessage())
                {
                    message.From = new MailAddress(_from);
                    foreach(var item in to.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            message.To.Add(item);
                        }
                    }
                    foreach(var item in cc.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            message.CC.Add(item);
                        }
                    }
                    foreach (var item in bcc.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            message.Bcc.Add(item);
                        }
                    }
                    message.Subject = subject;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Body = body;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = isBodyHtml;

                    if(attachments != null)
                    {
                        foreach(var item in attachments)
                        {
                            message.Attachments.Add(new Attachment(item));
                        }
                    }

                    using (var smtpclient = new SmtpClient())
                    {
                        smtpclient.Host = _host;
                        smtpclient.Send(message);
                    }
                }
            }catch (Exception ex)
            {

            }
        }
    }
}
