using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace analyzer
{
    public partial class MainWindow : Window
    {

        SqlConnection_ SqlConn = new SqlConnection_();
        MySqlConnection_ MySqlConn= new MySqlConnection_();
        SqlConnection connSql = new SqlConnection();
        MySqlConnection MySqlConnection = new MySqlConnection();
        string serverOutput = "";
        string serverInput = "";

        public MainWindow()
        {
            InitializeComponent();



            List<string> items = new List<string>
            {
                "Sql Srever",
                "My Sql Srever",
            };

            FromServer.ItemsSource = items;
            ServerSave.ItemsSource = items;
        }

        public async Task<string> con(ComboBox comboBox, string server)
        {
            server = comboBox.SelectedItem.ToString();
            if (server == "Sql Srever")
            {
                
                SqlConn = new SqlConnection_("SqlSrever");

                if(comboBox == FromServer)
                {
                    connSql = await SqlConn.OpenConnectionAsync(connSql);
                    SqlConn.ComboBoxFiller(dbForAnalysis, connSql);
                }
                else if (comboBox == ServerSave)
                {
                    connSql = await SqlConn.OpenConnectionAsync(connSql);
                    SqlConn.ComboBoxFiller(CBSaveAnalysis, connSql);
                }
                return server;

            }
            else if (server == "My Sql Srever")
            {
                MySqlConn = new MySqlConnection_("MySqlSrever"); 
                if (comboBox == FromServer)
                {//
                    MySqlConnection = await MySqlConn.MySqlOpenConnectionAsync(MySqlConnection);
                    MySqlConn.ComboBoxFiller(dbForAnalysis, MySqlConnection);
                }
                else if (comboBox == ServerSave)
                {
                    MySqlConnection = await MySqlConn.MySqlOpenConnectionAsync(MySqlConnection);
                    MySqlConn.ComboBoxFiller(CBSaveAnalysis, MySqlConnection);
                }

                return server;
            }

            return server;
        }
        public async void f(ComboBox comboBox, string server)
        {
            if (server == "Sql Srever")
            {
                connSql = await SqlConn.DbConnectionAsync(comboBox, connSql);
            }
            else if (server == "My Sql Srever")
            {
                MySqlConnection = await MySqlConn.DbConnectionAsync(comboBox, MySqlConnection);
                
            }


            //using (SqlCommand command = new SqlCommand("SELECT DB_NAME() AS CurrentDatabase", connSql))
            //{
            //    string currentDatabase = (string)command.ExecuteScalar();
            //    MessageBox.Show($"Connected to database: {currentDatabase}");
            //}
        }
        
        public async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                serverOutput = await con(comboBox,  serverOutput);
            }

        }

        private async void ServerSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            { serverInput = await con(comboBox,  serverInput); }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                f(comboBox, serverOutput);
            }
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                f(comboBox, serverInput);
               
                
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
