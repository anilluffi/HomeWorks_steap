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
using Microsoft.Data.SqlClient;
using System.Windows.Controls;

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



        public async Task<MySqlConnection> MySqlOpenConnectionAsync(MySqlConnection connection)
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();
                //MessageBox.Show($"sql Connection {connection.State}");
                return connection;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening connection: " + ex.Message);
                return null;
            }


        }

        public void ComboBoxFiller(ComboBox comboBox, MySqlConnection connection)
        {
            try
            {
                //перед заполнением очистка
                comboBox.Items.Clear();

                MySqlCommand command = new MySqlCommand("SHOW DATABASES", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dbName = reader.GetString(0);
                        comboBox.Items.Add(dbName); // Добавляем имя базы данных в ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public async Task<MySqlConnection> DbConnectionAsync(ComboBox comboBox, MySqlConnection connection)
        {
            string databaseName = comboBox.SelectedItem.ToString();
            string useDatabaseQuery = $"USE {databaseName}";
            try
            {
                using (MySqlCommand command = new MySqlCommand(useDatabaseQuery, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }

                return connection;
            }
            catch
            {
                return null;
            }



        }


        //        public async 
        //        Task
        //OpenConnectionAsync(ComboBox comboBox)
        //        {

        //            using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        //            {
        //                try
        //                {
        //                    await connection.OpenAsync();
        //                    MySqlCommand command = new MySqlCommand("SHOW DATABASES", connection);
        //                    using (MySqlDataReader reader = command.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            string dbName = reader.GetString(0);
        //                            comboBox.Items.Add(dbName); // Добавляем имя базы данных в ComboBox
        //                        }
        //                    }
        //                    MessageBox.Show("mysql Connection open");
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show("Error opening connection: " + ex.Message);
        //                }
        //            }
        //        }

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
