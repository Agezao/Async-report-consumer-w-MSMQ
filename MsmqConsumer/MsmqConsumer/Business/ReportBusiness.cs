using Microsoft.Reporting.WebForms;
using MsmqConsumer.Dac;
using MsmqConsumer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace MsmqConsumer.Business
{
    /// <summary>
    /// Class to process and generate a Report Request
    /// </summary>
    public class ReportBusiness
    {
        public ReportBusiness() { }

        /// <summary>
        /// Entry point for report processing
        /// </summary>
        /// <param name="reportParams">The report params/filters</param>
        public void ProcessReport(ReportParamsModel reportParams) {

            // Generate real reports
            var reportDac = new ReportDac();
            var reportItens = new List<ReportItem>();
            reportItens = reportDac.FetchReport(reportParams);
            
            // Get the XLS bytes
            var reportBytes = GenerateReport(reportItens);

            // Send bytes via email
            var mailBusiness = new MailBusiness();
            // For dev, disabling mail sending
            mailBusiness.SendMail(reportParams.DestinationMail, reportBytes);
        }

        /// <summary>
        /// Generate the Report Viewer item
        /// </summary>
        private byte[] GenerateReport(List<ReportItem> reportItens) {
            var reportViewer = new ReportViewer();
            ReportDataSource ReportDataSourceObject = new ReportDataSource("MyReport");
            reportViewer.LocalReport.ReportEmbeddedResource = "MsmqConsumer.Reports.MyReport.rdlc";

            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("GenerationDate", DateTime.Now.ToShortDateString());
            parameters[1] = new ReportParameter("GenerationUser", "Consumer User");
            reportViewer.LocalReport.SetParameters(parameters);

            var reportData = from item in reportItens
                             select new
                             {
                                 Id = item.Id,
                                 Name = item.Name,
                                 Active = item.Active,
                                 Date = item.Date
                             };
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource { Name = "MyReport", Value = reportData });

            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            var reportBytes = reportViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            // Saving to file system only when debuging. If not debuging, return the byte[] only.
            #if DEBUG
            using (FileStream fs = new FileStream(@"C:/testreport.xls", FileMode.Create))
            {
                fs.Write(reportBytes, 0, reportBytes.Length);
            }
            #endif

            return reportBytes;
        }
    }
}
