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

        SqlConnection connection = new SqlConnection();
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
            ServerSave.ItemsSource = items;
        }

        public async void con(ComboBox comboBox)
        {
            if (comboBox.SelectedItem.ToString() == "Sql Srever")
            {
                SqlConn = new SqlConnection_("SqlSrever");
                await SqlConn.OpenConnectionAsync(dbForAnalysis);

                //SqlConn.dbComboBoxFiller(dbForAnalysis);

            }
            else if (comboBox.SelectedItem.ToString() == "My Sql Srever")
            {
                MySqlConn = new MySqlConnection_("MySqlSrever");
                MySqlConn.OpenConnectionAsync(CBSaveAnalysis);
            }
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con(FromServer);

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SqlConn.dbComboBoxFiller(dbForAnalysis);
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ServerSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con(ServerSave);
        }
    }
}
