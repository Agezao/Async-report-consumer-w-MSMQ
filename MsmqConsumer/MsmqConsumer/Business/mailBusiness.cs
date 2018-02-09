using EASendMail;
using MsmqConsumer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqConsumer.Business
{
    public class MailBusiness
    {
        public MailBusiness() {}

        /// <summary>
        /// Send email to the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="attachment"></param>
        public void SendMail(string destination, byte[] attachment = null) {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            oMail.From = ConfigHelper.MailFrom;
            oMail.To = destination;
            oMail.Subject = "Your report is ready!";
            oMail.TextBody = "Here's your async report.";

            if(attachment != null)
                oMail.AddAttachment("report.xls", attachment);

            SmtpServer oServer = new SmtpServer(ConfigHelper.SmtpAddress);
            oServer.User = ConfigHelper.SmtpUser;
            oServer.Password = ConfigHelper.SmtpPassword;
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            try
            {
                oSmtp.SendMail(oServer, oMail);
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }
    }
}
