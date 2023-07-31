using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
namespace WpfAppSql
{
    internal class DatabaseHelper
    {

        public SqlDataAdapter adapter;
        private DataSet dataSet = new DataSet();
        private string TableName;
        public void comboBoxTables(string ConnectionString, ComboBox comboBox)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Получаем список всех таблиц в базе данных
                    DataTable tablesSchema = conn.GetSchema("Tables");

                    // Создаем список для хранения имен таблиц
                    List<string> tableNames = new List<string> { };

                    // Добавляем имена таблиц в список
                    foreach (DataRow row in tablesSchema.Rows)
                    {
                        string tableName = (string)row["TABLE_NAME"];
                        tableNames.Add(tableName);
                    }

                    // Заполняем ComboBox списком таблиц
                    comboBox.ItemsSource = tableNames;


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error: " + ex.Message);
            }
        }
        public async void FillAsync( DataGrid dataGrid,  TextBox textBox, SqlConnection Connection)
        {
            try
            {
                await Connection.OpenAsync();
                
                // Очистка 
                dataGrid.Columns.Clear();
                dataSet.Clear();
                dataGrid.ItemsSource = null;

                // Получение текста SQL-запроса из TextBox
                string selectCommandText = textBox.Text;

                adapter = new SqlDataAdapter(selectCommandText, Connection);

                // Модификация команд адаптера
                ModifyUpdateCommand(Connection);
                ModifyDeleteCommand(Connection);
                ModifyInsertCommand(Connection);

                // Заполнение DataSet данными из базы данных
                adapter.Fill(dataSet);

                // Отображаем данные в DataGrid
                dataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");

            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    await Connection.CloseAsync();
            }
        }
        public async void SaveAsync(SqlConnection Connection)
        {
            try
            {

                await Connection.OpenAsync();
                // Обновление измененных данных из DataSet в базе данных
                DataTable t = dataSet.Tables[0];
                DataRow[] rows = t.Select(null, null, DataViewRowState.ModifiedCurrent | DataViewRowState.Added);
                adapter.Update(rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message: {ex.Message}");
                //Debug.WriteLine($"Message: {ex.Message}");
                //Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                    await Connection.CloseAsync();
            }
        }

        public string comboBox_SelectionNameTable(ComboBox comboBox, TextBox textBox, DataGrid dataGrid)
        {
            // Получаем имя выбранной таблицы
            TableName = comboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(TableName))
            {
                textBox.Text = "SELECT * FROM " + TableName;
            }

            // Очистка
            dataGrid.Columns.Clear();
            dataSet.Clear();
            dataGrid.ItemsSource = null;

            return TableName;
        }
        // Модификация команды UPDATE адаптера
        public void ModifyUpdateCommand(SqlConnection Connection)
        {
            if (TableName == "users")
            {
                // инициализация текста SQL-запроса для обновления данных в базе данных
                string query = @"
                    UPDATE users
                    SET email = @p_email, username = @p_username, gender = @p_gender, birthday = @p_birthday, 
                    role_id = @p_role_id, points = @p_points, 
                    department_id = @p_department_id, deleted_at = @p_deleted_at
                    WHERE id = @p_id";

                // Создание объекта SqlCommand с указанным текстом запроса и подключением к базе данных
                SqlCommand cmd = new SqlCommand(query, Connection);


                cmd.Parameters.Add(new SqlParameter("@p_email", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "email",
                    SourceVersion = DataRowVersion.Current
                });
                // Добавление параметров к команде, которые будут использоваться для передачи значений из DataSet
                cmd.Parameters.Add(new SqlParameter("@p_username", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "username",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_gender", SqlDbType.Char, 1)
                {
                    SourceColumn = "gender",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_birthday", SqlDbType.Date)
                {
                    SourceColumn = "birthday",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_role_id", SqlDbType.Int)
                {
                    SourceColumn = "role_id",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_points", SqlDbType.Int)
                {
                    SourceColumn = "points",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_department_id", SqlDbType.Int)
                {
                    SourceColumn = "department_id",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_deleted_at", SqlDbType.SmallDateTime)
                {
                    SourceColumn = "deleted_at",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });

                // Установка команды UPDATE для адаптера
                adapter.UpdateCommand = cmd;
            }
            else if (TableName == "albums")
            {
                // инициализация текста SQL-запроса для обновления данных в базе данных
                string query = @"
                    UPDATE albums
                    SET title = @p_title, description = @p_description, user_id = @p_user_id, rate = @p_rate
                    WHERE id = @p_id";

                // Создание объекта SqlCommand с указанным текстом запроса и подключением к базе данных
                SqlCommand cmd = new SqlCommand(query, Connection);

                // Добавление параметров к команде, которые будут использоваться для передачи значений из DataSet
                cmd.Parameters.Add(new SqlParameter("@p_title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_description", SqlDbType.NVarChar, 1024)
                {
                    SourceColumn = "description",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@p_user_id", SqlDbType.Int)
                {
                    SourceColumn = "user_id",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@p_rate", SqlDbType.Int)
                {
                    SourceColumn = "rate",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });
                // Установка команды UPDATE для адаптера
                adapter.UpdateCommand = cmd;
            }
            else if (TableName == "departments")
            {
                // инициализация текста SQL-запроса для обновления данных в базе данных
                string query = @"
                    UPDATE departments
                    SET title = @p_title
                    WHERE id = @p_id";

                // Создание объекта SqlCommand с указанным текстом запроса и подключением к базе данных
                SqlCommand cmd = new SqlCommand(query, Connection);

                // Добавление параметров к команде, которые будут использоваться для передачи значений из DataSet
                cmd.Parameters.Add(new SqlParameter("@p_title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });
                // Установка команды UPDATE для адаптера
                adapter.UpdateCommand = cmd;
            }
            else if (TableName == "roles")
            {
                // инициализация текста SQL-запроса для обновления данных в базе данных
                string query = @"
                    UPDATE roles
                    SET title = @p_title
                    WHERE id = @p_id";

                // Создание объекта SqlCommand с указанным текстом запроса и подключением к базе данных
                SqlCommand cmd = new SqlCommand(query, Connection);

                // Добавление параметров к команде, которые будут использоваться для передачи значений из DataSet
                cmd.Parameters.Add(new SqlParameter("@p_title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });
                // Установка команды UPDATE для адаптера
                adapter.UpdateCommand = cmd;
            }
            else { MessageBox.Show("error"); }
        }

        // Модификация команды DELETE адаптера
        public void ModifyDeleteCommand(SqlConnection Connection)
        {
            if (TableName == "users")
            {
                // инициализация текста SQL-запроса для обновления данных в базе данных
                string query = @"UPDATE users SET deleted_at = GETDATE() WHERE id = @p_id;";

                // Создание объекта SqlCommand с указанным текстом запроса и подключением к базе данных
                SqlCommand cmd = new SqlCommand(query, Connection);

                // Добавление параметра к команде, который будет использоваться для передачи значения из DataSet
                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });

                // Установка команды DELETE для адаптера
                adapter.DeleteCommand = cmd;
            }
            else if (TableName == "albums")
            {
                string query = @"UPDATE albums SET deleted_at = GETDATE() WHERE id = @p_id;";

                SqlCommand cmd = new SqlCommand(query, Connection);

                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });

                adapter.DeleteCommand = cmd;
            }
            else if (TableName == "departments")
            {
                string query = @"UPDATE departments SET deleted_at = GETDATE() WHERE id = @p_id;";

                SqlCommand cmd = new SqlCommand(query, Connection);

                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });

                adapter.DeleteCommand = cmd;
            }
            else if (TableName == "roles")
            {
                string query = @"UPDATE roles SET deleted_at = GETDATE() WHERE id = @p_id;";

                SqlCommand cmd = new SqlCommand(query, Connection);

                cmd.Parameters.Add(new SqlParameter("@p_id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });

                adapter.DeleteCommand = cmd;
            }
            else { MessageBox.Show("error"); }

        }

        // Модификация команды INSERT адаптера
        public void ModifyInsertCommand( SqlConnection Connection)
        {
            if (TableName == "users")
            {
                // Задание хранимой процедуры для вставки данных в базу данных
                SqlCommand cmd = new SqlCommand("uspInsertUser", Connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };



                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "username",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@gender", SqlDbType.Char, 1)
                {
                    SourceColumn = "gender",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@birthday", SqlDbType.Date)
                {
                    SourceColumn = "birthday",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@role_id", SqlDbType.Int)
                {
                    SourceColumn = "role_id",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@points", SqlDbType.Int)
                {
                    SourceColumn = "points",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@department_id", SqlDbType.Int)
                {
                    SourceColumn = "department_id",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@deleted_at", SqlDbType.SmallDateTime)
                {
                    SourceColumn = "deleted_at",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });
                // Установка команды INSERT для адаптера
                adapter.InsertCommand = cmd;
            }
            else if (TableName == "albums")
            {
                SqlCommand cmd = new SqlCommand("uspInsertAlbums", Connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };



                cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar, 1024)
                {
                    SourceColumn = "description",
                    SourceVersion = DataRowVersion.Current
                });

                cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int)
                {
                    SourceColumn = "user_id",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@rate", SqlDbType.Int)
                {
                    SourceColumn = "rate",
                    SourceVersion = DataRowVersion.Current
                });



                adapter.InsertCommand = cmd;
            }
            else if (TableName == "departments")
            {
                SqlCommand cmd = new SqlCommand("uspInsertDepartments", Connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };



                cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });



                adapter.InsertCommand = cmd;

            }
            else if (TableName == "roles")
            {
                SqlCommand cmd = new SqlCommand("uspInsertRoles", Connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };


                cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50)
                {
                    SourceColumn = "title",
                    SourceVersion = DataRowVersion.Current
                });
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)
                {
                    SourceColumn = "id",
                    SourceVersion = DataRowVersion.Original
                });


                adapter.InsertCommand = cmd;
            }
            else { MessageBox.Show("error"); }

        }



    }
}
