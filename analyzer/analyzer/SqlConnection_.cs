using Microsoft.Extensions.Configuration;
using System;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace analyzer
{
    //internal class Connection
    //{
    //}
    public class SqlConnection_
    {

        ConfigurationBuilder cBuilder = new ConfigurationBuilder();
        SqlConnection connection = new SqlConnection();
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

        public async Task<SqlConnection> OpenConnectionAsync(SqlConnection connection)
        {
            connection = new SqlConnection(connectionString);
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

        public void ComboBoxFiller(ComboBox comboBox, SqlConnection connection)
        {
            try
            {
                //перед заполнением очистка
                comboBox.Items.Clear();

                SqlCommand command = new SqlCommand("SELECT name FROM sys.databases", connection);
                using (SqlDataReader reader = command.ExecuteReader())
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

        async public void GetDbNamesAsync()
        {
            using (connection = new SqlConnection(connectionString))
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

        public async Task<SqlConnection> DbConnectionAsync(ComboBox comboBox, SqlConnection connection)
        {
            string databaseName = comboBox.SelectedItem.ToString();
            string useDatabaseQuery = $"USE {databaseName}";
            try
            {
                using (SqlCommand command = new SqlCommand(useDatabaseQuery, connection))
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

        //public async  Task OpenConnectionAsync(ComboBox comboBox)
        //{

        //    using (connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            await connection.OpenAsync();

        //            MessageBox.Show("sql Connection open ");

        //            SqlCommand command = new SqlCommand("SELECT name FROM sys.databases", connection);
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string dbName = reader.GetString(0);
        //                    comboBox.Items.Add(dbName); // Добавляем имя базы данных в ComboBox
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error opening connection: " + ex.Message);
        //        }
        //    }
        //}

        //public void dbComboBoxFiller(ComboBox comboBox)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error opening connection: " + ex.Message);
        //    }
        //}



        public void dbSqllistinComboBox()
        {
            
        }



        //public void CloseConnection()
        //{

        //}
    }
}
