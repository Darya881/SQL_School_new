using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SQL_School_new.Classes.DataAdapter
{
    public class StudentsDA
    {
        // для подключения к БД
        const string SQL_SERVER = @"(localdb)\MSSQLLocalDB";
        const string BD = "University";

        // Создаем объект (который реализует методы с ADO.NET ) для выборки / изменения даных в БД Db_School
        private Sql sql = new Sql();

        // Строка соединения
        private String connectionString = null;


        // переменная для передачи сообщения об SQL ошибке по выполнению последнего SQL скрипта
        public string SqlErrorMessage { get; private set; } = "";
        // свойство показывающее есть ли SQL ошибка или нет 
        public bool IsSqlError => SqlErrorMessage.Length > 0;


        /// <summary>
        /// Таблица студ.групп для привязки к комбобокс на форме .
        /// При создании она пустая, нет столбцов и нет строк. 
        /// Метод Fill из класса SqlDataAdapter (который реализован в классе Sql) создаст строки и столбцы 
        /// </summary>
        public DataTable TabGroups { get; private set; }

        /// <summary>
        /// Таблица студентов для привязки 
        /// </summary>
        public DataTable TabStuds { get; private set; }

        /// <summary>
        /// Таблица для добавления нового студента. Содержит id_stud если удачно, -1 если неудачно
        /// </summary>
        public DataTable NewStuds { get; private set; }


        /// <summary>
        /// Таблица для оценок студента
        /// </summary>
        public DataTable TabGrades { get; private set; }


        /// <summary>
        /// Таблица для оценок студента
        /// </summary>
        public DataTable TabAverageSubjectsGrades { get; private set; }


        public StudentsDA()
        {
            // выделяем память 
            TabGroups = new DataTable();
            TabStuds = new DataTable();
            NewStuds = new DataTable();
            TabGrades = new DataTable();
            TabAverageSubjectsGrades = new DataTable();


            // формируем строку подключения
            connectionString = sql.CreateConnectionString(SQL_SERVER, BD);

        }

        /// <summary>
        /// Перезаписывает таблицу групп
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool GetTabGroups()
        {
            SqlErrorMessage = "";

            if(TabGroups != null) 
                TabGroups.Clear();

            if (sql.Open(connectionString))
            {
                // Помещаем выборку в таблицу
                TabGroups = sql.ExecTab("SELECT [id_group],[group_number],[id_prof],[professor_name]  FROM [dbo].[v_groups_professors]  order by [group_number]");

                if (TabGroups == null || !sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при чтении данных SQL [dbo].[v_groups_professors]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }


        /// <summary>
        /// Перезаписывает таблицу студентов 
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool GetTabStuds(int id_group)
        {
            SqlErrorMessage = "";

            if (TabStuds != null) 
                TabStuds.Clear();

            if (sql.Open(connectionString))
            {
                // Помещаем выборку в таблицу
                TabStuds = sql.ExecTab("SELECT *, age = [dbo].[fn_GetAgeDetails]([birthdate])  FROM [dbo].[view_groups] where id_group = " + id_group + "  order by [student_name]");

                if (TabStuds == null || !sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при чтении данных SQL [dbo].[view_groups]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }


        /// <summary>
        /// Удаляет студента из БД по id студента 
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool DeleteStuds(int id_stud)
        {
            SqlErrorMessage = "";


            if (sql.Open(connectionString))
            {
                sql.Exec("DELETE FROM [dbo].[Students] where id_stud = " + id_stud);

                if (!sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при удалении данных SQL [dbo].[Students]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }


        /// <summary>
        /// Attempts to add a new student record with the specified name, birthdate, and group identifier.
        /// </summary>
        /// <remarks>If the operation fails due to a database access issue or a SQL execution error, the
        /// method returns false and sets the SqlErrorMessage property with details about the failure.</remarks>
        /// <param name="stud_name">The full name of the student to add. Cannot be null or empty.</param>
        /// <param name="birthdate">The birthdate of the student, represented as a string. The expected format should match the database
        /// requirements.</param>
        /// <param name="id_group">The identifier of the group to which the student will be assigned.</param>
        /// <returns>true if the student was successfully added; otherwise, false.</returns>
        public bool AddStuds(string stud_name, DateTime birthdate, int id_group)
        {
            SqlErrorMessage = "";

            if(NewStuds != null) 
                NewStuds.Clear();


            if (sql.Open(connectionString))
            {
                NewStuds = sql.ExecTab("EXEC dbo.AddStudent " +
                                       "@stud_name = '" + stud_name + "', " +
                                       "@birthdate = '" + birthdate.ToString("yyyy-MM-dd") + "', " +
                                       "@id_group = " + id_group);


                if (NewStuds == null || !sql.LastExecIsOK || Convert.ToInt32(NewStuds.Rows[0][0]) == -1)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при добавлении данных SQL [dbo].[Students]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }




        /// <summary>
        /// Обновляет имя студента по id 
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool UpdateNameStudent(int id_stud, string new_name)
        {
            SqlErrorMessage = "";


            if (sql.Open(connectionString))
            {
                sql.Exec("UPDATE [dbo].[Students] SET [student_name] = '" + new_name + "' WHERE id_stud = " + id_stud);

                if (!sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при обновлении данных SQL [dbo].[Students]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }


        /// <summary>
        /// Перезаписывает таблицу оценок студента по id
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool GetTabGrades(int id_stud)
        {
            SqlErrorMessage = "";

            if (TabGrades != null)
                TabGrades.Clear();

            if (sql.Open(connectionString))
            {
                // Помещаем выборку в таблицу
                TabGrades = sql.ExecTab("SELECT * FROM [dbo].[v_grades] where id_stud = " + id_stud + "  order by [time_test] DESC");

                if (TabGrades == null || !sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при чтении данных SQL [dbo].[view_groups]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }




        /// <summary>
        /// Перезаписывает таблицу студентов 
        /// </summary>
        /// <returns>True - все OK, False - ошибка SQL, см. SqlErrorMessage  </returns>
        public bool GetTabAverageSubjectsGrades(int id_stud, int months)
        {
            SqlErrorMessage = "";

            if (TabAverageSubjectsGrades != null)
                TabAverageSubjectsGrades.Clear();

            if (sql.Open(connectionString))
            {
                // Помещаем выборку в таблицу
                TabAverageSubjectsGrades = sql.ExecTab("SELECT * FROM [dbo].[fn_AverageSubjectGrade](" + id_stud + "," + months + ")");

                if (TabAverageSubjectsGrades == null || !sql.LastExecIsOK)
                {
                    sql.Close();
                    SqlErrorMessage = ("Ошибка при чтении данных SQL [dbo].[Students]\n\n" + sql.pSqlErrorMessage);
                    return false;
                }

            }
            else
            {
                SqlErrorMessage = ("Нет доступа к SQL БД \n\n" + sql.pSqlErrorMessage);
                return false;
            }

            sql.Close();

            return true;
        }





    }

}
