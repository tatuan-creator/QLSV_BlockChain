using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace QLSV_BlockChain.Common
{
    public class Mail
    {
        public void SendMail() { 
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Host = "smtp.gmail.com";
                //Cổng SMTP
                smtp.Port = 587;
                //SMTP yêu cầu mã hóa dữ liệu theo SSL
                smtp.EnableSsl = true;
                //UserName và Password của mail
                smtp.Credentials = new NetworkCredential("tatuan.hutech@gmail.com", "Tuan870611");

                smtp.Send("tatuan.hutech@gmail.com", "kenlx8706@gmail.com", "thử", "abc");
            }
            catch(Exception ex)
            {

            }
        }
    }
}