using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MySql.Data.MySqlClient;
using System.Data;

namespace analyzer
{
    internal class MySqlConnection_
    {
        ConfigurationBuilder cBuilder = new ConfigurationBuilder();

        public string? connectionString;

        public MySqlConnection_()
        {

        }
        public MySqlConnection_(string ServerName)
        {
            cBuilder.SetBasePath(Directory.GetCurrentDirectory());
            cBuilder.AddJsonFile("config.json");

            var config = cBuilder.Build();

            connectionString = config.GetConnectionString(ServerName);
        }

        async public void OpenConnectionAsync()
        {

            using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    MessageBox.Show("mysql Connection open");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening connection: " + ex.Message);
                }
            }
        }

        async public void GetDbNamesAsync()
        {
            using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
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

    }
}
