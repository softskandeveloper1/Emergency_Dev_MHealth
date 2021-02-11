using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL.Utils
{
    public static class DataAccess
    {
        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            return configuration.GetConnectionString("DefaultConnection");
        }

        public static DataSet GetDataset(NpgsqlCommand command)
        {
            try
            {
                var conn = new NpgsqlConnection(GetConnectionString());
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static DataTable GetDataTable(NpgsqlCommand command)
        {
            try
            {
                var conn = new NpgsqlConnection(GetConnectionString());
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
