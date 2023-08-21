using Microsoft.Extensions.Configuration;
using System;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace analyzer
{
    //internal class Connection
    //{
    //}
    public class SqlConnection_
    {

        ConfigurationBuilder cBuilder = new ConfigurationBuilder();
        
        public string? connectionString;

        public SqlConnection_()
        {

        }
        public SqlConnection_(string ServerName)
        {
            cBuilder.SetBasePath(Directory.GetCurrentDirectory());
            cBuilder.AddJsonFile("config.json");

            var config = cBuilder.Build();

            connectionString = config.GetConnectionString(ServerName);
        }

        

        async public void OpenConnectionAsync()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    MessageBox.Show("sql Connection open ");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening connection: " + ex.Message);
                }
            }
        }

        async public void GetDbNamesAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    DataTable databases = connection.GetSchema("Databases");

                    List<string> names = new List<string>();


                    foreach (DataRow database in databases.Rows)
                    {
                        string databaseName = database["database_name"].ToString();
                        names.Add(databaseName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening connection: " + ex.Message);
                }
            }
        }

        //public void CloseConnection()
        //{

        //}
    }
}
