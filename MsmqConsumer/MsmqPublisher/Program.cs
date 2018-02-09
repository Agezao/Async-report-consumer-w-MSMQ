using MsmqPublisher.Helpers;
using MsmqPublisher.Messaging;
using MsmqPublisher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Used for test purposes
            // Use this program to send messages so you can test if the MsmqConsumer is working properly

            var mQueue = new MQueue<ReportParamsModel>();

            // Sending a couple of messages for testing

            for (var i = 0; i < 3; i++) {
                ReportParamsModel messageTest = new ReportParamsModel
                {
                    DestinationMail = ConfigHelper.DestinationMail,
                    ClientId = i * 3,
                    Protocol = (i * 42).ToString(),
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue
                };

                mQueue.Queue.Send(messageTest);
            }
        }
    }
}
