using System.Configuration;

namespace MsmqConsumer.Helpers
{
    public static class ConfigHelper
    {
        public static string QueueName {
            get { return ConfigurationManager.AppSettings["QueueName"]; }
        }

        public static string QueueDescription
        {
            get { return ConfigurationManager.AppSettings["QueueDescription"]; }
        }

        public static string RefreshRate
        {
            get { return ConfigurationManager.AppSettings["RefreshRate"]; }
        }

        public static string MailFrom
        {
            get { return ConfigurationManager.AppSettings["MailFrom"]; }
        }

        public static string SmtpAddress
        {
            get { return ConfigurationManager.AppSettings["SmtpAddress"]; }
        }

        public static string SmtpUser
        {
            get { return ConfigurationManager.AppSettings["SmtpUser"]; }
        }

        public static string SmtpPassword
        {
            get { return ConfigurationManager.AppSettings["SmtpPassword"]; }
        }

        public static string ConnString
        {
            get { return ConfigurationManager.AppSettings["ConnString"]; }
        }
    }
}
