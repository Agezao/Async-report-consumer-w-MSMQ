using MsmqConsumer.Business;
using MsmqConsumer.Helpers;
using MsmqConsumer.Messaging;
using MsmqConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MsmqConsumer
{
    class Program
    {
        private static Timer timer;

        static void Main(string[] args)
        {
            SetTimer();

            Console.Write("To close this consumer press any key.");
            Console.ReadLine();
        }

        public static void ProcessMessages(object source, ElapsedEventArgs e) {
            // Stopping timer to prevent double getting
            timer.Stop();

            var mQueue = new MQueue<ReportParamsModel>();
            Message[] messages = mQueue.Queue.GetAllMessages();

            if (messages.Count() > 0) {
                var reportBusiness = new ReportBusiness();

                foreach (Message m in messages)
                {
                    try
                    {
                        var reportParams = (ReportParamsModel)m.Body;
                        reportBusiness.ProcessReport(reportParams);
                    }
                    catch (MessageQueueException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    mQueue.Queue.ReceiveById(m.Id);
                }
            }

            // Reseting the timer
            SetTimer();
        }

        private static void SetTimer()
        {
            timer = new Timer(double.Parse(ConfigHelper.RefreshRate));
            timer.Elapsed += new ElapsedEventHandler(ProcessMessages);
            timer.Start();
        }
    }
}
