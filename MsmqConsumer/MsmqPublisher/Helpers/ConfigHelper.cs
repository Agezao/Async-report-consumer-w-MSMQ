using System.Configuration;

namespace MsmqPublisher.Helpers
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

        public static string DestinationMail
        {
            get { return ConfigurationManager.AppSettings["DestinationMail"]; }
        }
    }
}
