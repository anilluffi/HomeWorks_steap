using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        //отображает какие есть сервера и подключаеться к выбранному
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
                {
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

        // проверяет к кокому типу сервера сейчас подключена программа и показывает какие бд есть на этом сервере
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

        private void Analyz_Click(object sender, RoutedEventArgs e)
        {
            // Общее количество таблиц в базе данных
            // Total number of tables in the db
            //
            // Оценка размера базы данных на диске (в МБ):
            // size of db on disk (in MB)
            //
            // дата анализа
            // date of analysis

            //string TableCountString = "1";
            //string DbSizeString = "2";
            //string AnalysisDateString = "3";
            //IF NOT EXISTS
            string ComText = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnalyzeDatabase]') AND type in (N'P', N'PC'))
            BEGIN
                EXEC('
                CREATE PROCEDURE AnalyzeDatabase
                    @TableCountString VARCHAR(MAX) OUTPUT,
                @DbSizeString VARCHAR(MAX) OUTPUT,
                @AnalysisDateString VARCHAR(MAX) OUTPUT
            AS
            BEGIN
                -- Общее количество таблиц в базе данных
                DECLARE @TableCount INT
                SELECT @TableCount = COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = ''BASE TABLE''
        
                -- Размер базы данных на диске
                DECLARE @DbSizeMB FLOAT
                SET @DbSizeMB = (SELECT SUM(size * 8.0 / 1024) FROM sys.master_files WHERE type = 0)
        
                -- Дата анализа (текущее время)
                    DECLARE @AnalysisDate VARCHAR(50)
                    SET @AnalysisDate = CONVERT(VARCHAR(50), GETDATE(), 120)
            
                    -- Присваиваем результаты анализа переменным OUTPUT
                    SET @TableCountString = CAST(@TableCount AS VARCHAR(10))
                    SET @DbSizeString = CAST(@DbSizeMB AS VARCHAR(20))
                    SET @AnalysisDateString = @AnalysisDate
                END')
            END
            ";

            SqlCommand command1 = new SqlCommand()
            {
                CommandText = ComText,
                Connection = connSql,
            };

           command1.ExecuteNonQuery();


            // Создайте объект SqlCommand для вызова процедуры AnalyzeDatabase
            SqlCommand command2 = new SqlCommand("AnalyzeDatabase", connSql);
            command2.CommandType = CommandType.StoredProcedure;

            // Добавьте параметры OUTPUT для получения результатов
            command2.Parameters.Add(new SqlParameter("@TableCountString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });
            command2.Parameters.Add(new SqlParameter("@DbSizeString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });
            command2.Parameters.Add(new SqlParameter("@AnalysisDateString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });


            // Выполните процедуру
            command2.ExecuteNonQuery();

            string TableCountString = command2.Parameters["@TableCountString"].Value.ToString();
            string DbSizeString = command2.Parameters["@DbSizeString"].Value.ToString();
            string AnalysisDateString = command2.Parameters["@AnalysisDateString"].Value.ToString();

            List<AnalysisTable> AnalysisTableList = new List<AnalysisTable>
            {
                new AnalysisTable { Column1 = TableCountString,
                    Column2 = DbSizeString,
                    Column3 = AnalysisDateString},

            };

            dataGrid.ItemsSource = AnalysisTableList;

            DataGridColumn columnToChangeHeader = dataGrid.Columns[0]; // Индекс столбца, который вы хотите изменить
            columnToChangeHeader.Header = "Total number of tables in the db";


            columnToChangeHeader = dataGrid.Columns[1];
            columnToChangeHeader.Header = "size of db on disk (in MB)";

            columnToChangeHeader = dataGrid.Columns[2];
            columnToChangeHeader.Header = "date of analysis";

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
