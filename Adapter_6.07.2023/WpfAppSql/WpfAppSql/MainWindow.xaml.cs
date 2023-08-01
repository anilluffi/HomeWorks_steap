using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        private string connString = string.Empty;
        private SqlConnection conn;

        public MainWindow()
        {
            InitializeComponent();

            connString = ConfigurationManager.ConnectionStrings["express"].ConnectionString;
            conn = new SqlConnection(connString);

            dbHelper.comboBoxTables(connString, comboBoxTables);
        }

        private void comboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dbHelper.comboBox_SelectionNameTable(comboBoxTables, TextBoxQuery, dataGrid);
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
            dbHelper.FillAsync(dataGrid, TextBoxQuery,  conn);

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            dbHelper.SaveAsync(conn);
        }


    }
}

    

