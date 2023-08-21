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
        // conn.OpenConnectionAsync();
        public MainWindow()
        {
            InitializeComponent();



            List<string> items = new List<string>
            {
                "Sql Srever",
                "My Sql Srever",
            };

            FromServer.ItemsSource = items;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FromServer.SelectedItem.ToString() == "Sql Srever")
            {
                SqlConn = new SqlConnection_("SqlSrever");
                SqlConn.OpenConnectionAsync();
            }
            else if (FromServer.SelectedItem.ToString() == "My Sql Srever")
            {
                MySqlConn = new MySqlConnection_("MySqlSrever");
                MySqlConn.OpenConnectionAsync();
            }

            
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
