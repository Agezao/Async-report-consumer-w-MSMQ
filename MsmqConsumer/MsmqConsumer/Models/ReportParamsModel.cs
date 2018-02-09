using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqConsumer.Models
{
    /// <summary>
    /// Example of a report filters/params that will be received via the queue
    /// </summary>
    public class ReportParamsModel
    {
        public string DestinationMail { get; set; }
        public int ClientId { get; set; }
        public string Protocol { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
