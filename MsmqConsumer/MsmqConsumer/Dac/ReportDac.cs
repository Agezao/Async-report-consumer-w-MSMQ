using MsmqConsumer.Helpers;
using MsmqConsumer.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqConsumer.Dac
{
    public class ReportDac
    {
        private NpgsqlConnection _conn { get; set; }

        public ReportDac() {
            _conn = new NpgsqlConnection(ConfigHelper.ConnString);
            _conn.Open();
        }

        ~ReportDac() {
            _conn.Close();
            _conn.Dispose();
        }

        public List<ReportItem> FetchReport(ReportParamsModel reportParams) {
            var reportItens = new List<ReportItem>();

            string query = @"
                    SELECT 
                        * 
                    FROM MockInformation";

            using (NpgsqlCommand command = new NpgsqlCommand(query, _conn))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    reportItens.Add(new ReportItem {
                        Id = (int)reader["Id"],
                        Active = reader["Active"].ToString() == "1" ? true : false,
                        Name = reader["Name"].ToString(),
                        Date = DateTime.Parse(reader["Date"].ToString())
                    });
                }
            }

            return reportItens;
        }
    }
}
