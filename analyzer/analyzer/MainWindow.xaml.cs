using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace analyzer
{
    public partial class MainWindow : Window
    {

        SqlConnection_ SqlConn = new SqlConnection_();
        MySqlConnection_ MySqlConn = new MySqlConnection_();
        SqlConnection connSql = new SqlConnection();
        MySqlConnection MySqlConnection = new MySqlConnection();
        string serverOutput = "";
        string serverInput = "";
        string TableCountString = "";
        string DbSizeString = "";
        string AnalysisDateString = "";
        string tableName = "Analyz";
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

                if (comboBox == FromServer)
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
                serverOutput = await con(comboBox, serverOutput);
            }

        }

        private async void ServerSave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            { serverInput = await con(comboBox, serverInput); }
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
            if (serverOutput == "Sql Srever")
            {
                AnalyzSql();
            }
            else if (serverOutput == "My Sql Srever")
            {
                AnalyzMySql();
            }



        }

        /// <summary>
        /// /////////////////////////////
        /// </summary>
        void AnalyzSql()
        {

            //            string ComText = @"
            //    DECLARE @TableCount INT; -- Объявление и инициализация TableCountString
            //    DECLARE @DbSizeMB FLOAT; -- Объявление и инициализация DbSizeString
            //    DECLARE @AnalysisDate VARCHAR(50); -- Объявление и инициализация AnalysisDateString

            //    -- Общее количество таблиц в базе данных
            //    DECLARE @TableCount INT
            //    SELECT @TableCount = COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'

            //    -- Размер базы данных на диске
            //    DECLARE @DbSizeMB FLOAT
            //    SET @DbSizeMB = (SELECT SUM(size * 8.0 / 1024) FROM sys.master_files WHERE type = 0)

            //    -- Дата анализа (текущее время)
            //    SET @AnalysisDate = CONVERT(VARCHAR(50), GETDATE(), 120)

            //    -- Присваиваем результаты анализа переменным OUTPUT
            //    SET @TableCountString = CAST(@TableCount AS VARCHAR(10))
            //    SET @DbSizeString = CAST(@DbSizeMB AS VARCHAR(20))
            //    SET @AnalysisDateString = @AnalysisDate
            //";

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
            SqlCommand command = new SqlCommand()
            {
                CommandText = ComText,
                Connection = connSql,
            };

            //параметры OUTPUT для получения результатов
            command.Parameters.Add(new SqlParameter("@TableCountString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@DbSizeString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new SqlParameter("@AnalysisDateString", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output });

            // Выполнение процедуры
            command.ExecuteNonQuery();

            TableCountString = command.Parameters["@TableCountString"].Value.ToString();
            DbSizeString = command.Parameters["@DbSizeString"].Value.ToString();
            AnalysisDateString = command.Parameters["@AnalysisDateString"].Value.ToString();


            List<AnalysisTable> AnalysisTableList = new List<AnalysisTable>
            {
                new AnalysisTable { Column1 = TableCountString,
                    Column2 = DbSizeString,
                    Column3 = AnalysisDateString},

            };

            dataGrid.ItemsSource = AnalysisTableList;





            renameTableHeader(0, "Total number of tables in the db");
            renameTableHeader(1, "size of db on disk (in MB)");
            renameTableHeader(2, "date of analysis");

            tableName = "Analyz" + "_" + connSql.Database;

        }

        void AnalyzMySql()
        {


            string ComText = @"
    SET @TableCountString = 0; -- Объявление и инициализация TableCountString
    SET @DbSizeString = 0;    -- Объявление и инициализация DbSizeString
    SET @AnalysisDateString = NOW(); -- Объявление и инициализация AnalysisDateString

    -- Общее количество таблиц в текущей схеме базы данных
    SELECT COUNT(*) INTO @TableCountString
    FROM information_schema.tables
    WHERE table_type = 'BASE TABLE' AND table_schema = DATABASE();

    -- Размер базы данных на диске
    SELECT SUM(data_length + index_length) / 1024 / 1024 INTO @DbSizeString
    FROM information_schema.tables
    WHERE table_type = 'BASE TABLE' AND table_schema = DATABASE();

    -- Дата анализа (текущее время)
    SET @AnalysisDateString = NOW();
";

            MySqlCommand command = new MySqlCommand()
            {
                CommandText = ComText,
                Connection = MySqlConnection,
            };




            // параметры OUTPUT
            command.Parameters.Add(new MySqlParameter("@TableCountString", MySqlDbType.VarChar, 10) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new MySqlParameter("@DbSizeString", MySqlDbType.VarChar, 20) { Direction = ParameterDirection.Output });
            command.Parameters.Add(new MySqlParameter("@AnalysisDateString", MySqlDbType.VarChar, 50) { Direction = ParameterDirection.Output });


            // Выполнение
            command.ExecuteNonQuery();

            TableCountString = command.Parameters["@TableCountString"].Value.ToString();
            DbSizeString = command.Parameters["@DbSizeString"].Value.ToString();
            AnalysisDateString = command.Parameters["@AnalysisDateString"].Value.ToString();

            List<AnalysisTable> AnalysisTableList = new List<AnalysisTable>
            {
                new AnalysisTable { Column1 = TableCountString,
                    Column2 = DbSizeString,
                    Column3 = AnalysisDateString},

            };

            dataGrid.ItemsSource = AnalysisTableList;





            renameTableHeader(0, "Total number of tables in the db");
            renameTableHeader(1, "size of db on disk (in MB)");
            renameTableHeader(2, "date of analysis");

            MySqlCommand command1 = new MySqlCommand("SELECT DATABASE();", MySqlConnection);
            tableName = "Analyz" + "_" + command1.ExecuteScalar().ToString();

        }

        void renameTableHeader(int ind, string newName)
        {
            DataGridColumn columnToChangeHeader = dataGrid.Columns[ind]; // Индекс столбца, который вы хотите изменить
            columnToChangeHeader.Header = newName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (serverInput == "Sql Srever")
            {
                SaveinSql();
            }
            else if (serverInput == "My Sql Srever")
            {
                SaveinMySql();
            }


        }


        void SaveinSql()
        {

            string SqlCom = $@"
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}')
    BEGIN
        CREATE TABLE {tableName} (
            TableCountString NVARCHAR(MAX),
            DbSizeString NVARCHAR(MAX),
            AnalysisDateString NVARCHAR(MAX)
        );
    END;

    -- Вставка данных
    INSERT INTO {tableName} (TableCountString, DbSizeString, AnalysisDateString)
    VALUES (@TableCountString, @DbSizeString, @AnalysisDateString);
";


            SqlCommand callCommand = new SqlCommand(SqlCom, connSql);

            callCommand.Parameters.AddWithValue("@TableCountString", TableCountString);
            callCommand.Parameters.AddWithValue("@DbSizeString", DbSizeString);
            callCommand.Parameters.AddWithValue("@AnalysisDateString", AnalysisDateString);

            callCommand.ExecuteNonQuery();

            MessageBox.Show($"data saved to table {tableName}");
        }

        void SaveinMySql()
        {

            string createTableSql = $@"
            CREATE TABLE IF NOT EXISTS {tableName} (
                TableCount TEXT,
                DbSize TEXT,
                AnalysisDate TEXT
            )";
            MySqlCommand createTableCommand = new MySqlCommand(createTableSql, MySqlConnection);
            createTableCommand.ExecuteNonQuery();


            string insertDataSql = $@"
        INSERT INTO {tableName} (TableCount, DbSize, AnalysisDate)
        VALUES (@TableCountString, @DbSizeString, @AnalysisDateString)";


            MySqlCommand insertDataCommand = new MySqlCommand(insertDataSql, MySqlConnection);

            insertDataCommand.Parameters.AddWithValue("@TableCountString", TableCountString);
            insertDataCommand.Parameters.AddWithValue("@DbSizeString", DbSizeString);
            insertDataCommand.Parameters.AddWithValue("@AnalysisDateString", AnalysisDateString);

            insertDataCommand.ExecuteNonQuery();

            MessageBox.Show($"data saved to table {tableName}");

        }


    }
}
