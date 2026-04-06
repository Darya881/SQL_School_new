using System;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace SQL_School_new.Classes
{
    public class Sql
    {
        /// <summary>
        /// Имя SQL сервера
        /// </summary>
        public string pSqlServer { get; private set; }
        /// <summary>
        /// Имя Базы Данных
        /// </summary>
        public string pSqlBd { get; private set; }
        /// <summary>
        /// Наименование устройства (компьютера) пользователя
        /// </summary>
        public string pSqlLocalHost { get; private set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string pSqlUser { get; private set; }
        /// <summary>
        /// Строка запроса в виде SQL скрипта для выполнения в БД
        /// </summary>
        public string pSqlCommand { get; private set; }
        /// <summary>
        /// SQL соединение с БД
        /// </summary>
        public SqlConnection SqlConnection {get; private set; }
        /// <summary>
        /// Транзакция для выполнения скрипта в БД
        /// </summary>
        public SqlTransaction pSQLtransaction { get; private set; }
        /// <summary>
        /// Строка сообщения об ошибке возникшей при 
        /// выполнения последнего запроса пользователя на SQL сервере.
        /// Без ошибки - пустое значение.
        /// </summary>
        public string pSqlErrorMessage { get; private set; }
        
        /// <summary>
        /// Последний метод выполняющий TSQL скрипт на сервере выполнен без ошибок - true 
        /// </summary>
        public bool LastExecIsOK { get { return pSqlErrorMessage.Length > 0 ? false : true; } }
        
		
		///  Конструктор в трех видах (пример свойства ООП - полиморфизма) 
        public Sql()
        { pSqlErrorMessage = ""; pSqlLocalHost = Environment.UserDomainName; pSqlUser = Environment.UserName; }

        public Sql(string User)
        { pSqlErrorMessage = ""; pSqlLocalHost = Environment.UserDomainName; pSqlUser = User; }

        public Sql(string LocalHost, string User)
        { pSqlErrorMessage = ""; pSqlLocalHost = LocalHost; pSqlUser = User; }

        /// <summary>
        /// Создает символьную строку подключения с доступом для текущего логина Windows
        /// </summary>
        /// <param name="sqlServer"></param>
        /// <param name="sqlBD"></param>
        /// <returns></returns>
        public string CreateConnectionString(string sqlServer, string sqlBD)
        {
            return string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=True", sqlServer, sqlBD);
        }



        /// <summary>
        /// Подключение к SQL серверу без настроек, в случае ошибки записывается  
        /// сообщение в pSqlErrorMessage и возвращается NULL       
        /// </summary>
        /// <param name="conStr">Строка подключения </param>
        /// <returns></returns>
        public bool Open(string conStr)
        {
            SqlConnection = new SqlConnection(conStr);
            try
            {
                SqlConnection.Open();
                pSqlServer = SqlConnection.DataSource.ToString();
                pSqlBd = SqlConnection.Database.ToString();

                DataTable table = new DataTable();
                table = ExecTab("select SYSTEM_USER as userName");
                if (table != null && table.Rows.Count > 0)
                    pSqlUser = table.Rows[0][0].ToString();
            }
            catch
            {
                pSqlErrorMessage = ("Невозможно подключится к SQL серверу " + "\n" + "\n" +
                    "со строкой подключения :" + conStr + "\n" + "\n" +
                    "Возможные причины:" + "\n" +
                    "		1. Нет сети" + "\n" +
                    "		2. Не работает сервер" + "\n" +
                    "		3. Нет разрешения на вход для пользователя " + "\n" +
                    "		4. Неправильно указано имя (параметры) SQL сервера ");
                return false;
            }
            return true;
        }

        /// <summary>
        ///  Выполнение моих настройек подключения к SQL, 
        ///  если удачно возвращается TRUE иначе FALSE
        /// </summary>
        /// <returns></returns>
        public bool Sets()
        {
            bool _ret = true;
            //	 Инициализация параметров сессии
            //	 останавливает пересылку с сервера сообщений типа "29 row(s) affected"
            if (!ExecSet("SET NOCOUNT ON"))  { _ret = false; goto exit; }

            //    отключение режима неявного начала транзакции, т.е. устанавливается автоматическая;
            //    транзакция на основные команды SQL
            if (!ExecSet("SET IMPLICIT_TRANSACTIONS OFF")) { _ret = false; goto exit; }

            //    устанавливает формат дат, представленных в символьной форме в виде День-Месяц-Год
            if (!ExecSet("SET DATEFORMAT dmy")) { _ret = false; goto exit; }

            //    установка первого дня с понидельника
            if (!ExecSet("SET DATEFIRST 1")) { _ret = false; goto exit; }

            //     устанавливает время ожидания освобождения требуемого ресурса;
            //     в случае наложении на него не совместимой блокировки другим процессом, мс.
            if (!ExecSet("SET LOCK_TIMEOUT 10000")) { _ret = false; goto exit; }

            //     установлен уровень изоляции транзакций - READ COMMITTED - запрет "грязного" чтения;
            //     (другой процесс может читать данные только из завершенных транзакций).
            if (!ExecSet("SET TRANSACTION ISOLATION LEVEL READ COMMITTED")) { _ret = false; goto exit; }

            //     установка определяет, каким образом сервер будет интерпретировать символ " (двойная кавычка).;
            //     Если параметр установлен в ON - двойная кавычка может заменять символы [] для ссылки на объекты,;
            //     содержащие в имени пробелы или совпадающие с зарезервированными словами.;
            //     Установка параметра в OFF позволяет использовать двойные кавычки для определения строковых;
            //     констант в качестве синонима ' (одиночного апострофа). 
            //     Для работы триггеров логирования (испол.XML) - ON
            if (!ExecSet("SET QUOTED_IDENTIFIER ON")) { _ret = false; goto exit; }

            //     устанавливает режим сравнения значений NULL. В режиме ON эта установка сессии означает,;
            //     что NULL всегда не равен NULL
            if (!ExecSet("SET ANSI_NULLS ON")) { _ret = false; goto exit; }

            //      установка определяет, надо ли прерывать выполнение запроса в случае деления на ноль;
            //      или арифметического переполнения. Для работы триггеров логирования (испол.XML) - ON
            if (!ExecSet("SET ARITHABORT ON")) { _ret = false; goto exit; }

            //      если ON - выдача предупреждающих сообщений в случае применении агрегатных функций;
            //      над значением NULL, в случае арифметического переполнения и усечения строк и т.п., т.е ошибка
            if (!ExecSet("SET ANSI_WARNINGS ON")) { _ret = false; goto exit; }

            //      разрешает дополнять пробелами поля типа CHAR и нулями поля типа VARBINARY;
            //      если ширина поля превосходит размер вставляемой величины
            if (!ExecSet("SET ANSI_PADDING ON")) { _ret = false; goto exit; }

            //      Если выполнена инструкция SET XACT_ABORT ON и инструкция языка Transact-SQL вызывает ошибку, 
            //      вся транзакция завершается и выполняется ее откат.
            //      Если выполнена инструкция SET XACT_ABORT OFF, в некоторых случаях выполняется откат только
            //      вызвавшей ошибку инструкции языка Transact-SQL, а обработка транзакции продолжается.
            //      В зависимости от серьезности ошибки возможен откат всей транзакции при выполненной
            //      инструкции SET XACT_ABORT OFF. OFF — установка по умолчанию.
            if (!ExecSet("SET XACT_ABORT ON")) { _ret = false;  }

            exit:
            return _ret;
        }

        /// <summary>
        /// Выполнение SQL скрипта начинающегося на SET для установки параметра соединения 
        /// </summary>
        /// <param name="setCommand"></param>
        /// <returns>true - выполнение успешно, иначе false</returns>
        private bool ExecSet(string setCommand)
        {
            bool ret = true;  pSqlErrorMessage = "";
            SqlCommand sqlCommmand = new SqlCommand(setCommand, SqlConnection);
            if (pSQLtransaction != null)
                sqlCommmand.Transaction = pSQLtransaction;
            try
            {
                sqlCommmand.ExecuteNonQuery();
            }
            catch
            {
                ret = false;
                pSqlErrorMessage = getSetSqlErrorMessage(sqlCommmand.CommandText);
            }
            return ret;
        }

        /// <summary>
        /// генерация сообщения об ошибке при установке настроек для соединения
        /// </summary>
        /// <param name="nameSqlParametr"></param>
        /// <returns></returns>
        private string getSetSqlErrorMessage(string nameSqlParametr )
        {
            string ret = string.Format("Невозможно установить свойство на подключение к серверу: \n {0}", 
                nameSqlParametr);
            return ret;
        }

        /// <summary>
        /// Возвращает таблицу в виде списока с полным наименованием разрешений и отметкой об разрешении (1 или 0) 
        /// </summary>
        /// <param name="typePermissions">Список краткого наименования разрешений через запятую</param>
        /// <returns></returns>
        public DataTable GetDatabasePermissions(string typePermissions)
        {
            //typePermissons='AL','ALTG','ALSM','CRPR','CRSM','CRTB','CRVW','DL','EX','IN','SL','UP'
            string query = string.Format("SELECT perm =convert(bit,HAS_PERMS_BY_NAME(null, 'DATABASE', permission_name )), permission_name  " +
                "FROM sys.fn_builtin_permissions(default) " +
                "where class_desc = 'DATABASE' and [type] in ({0}) order by permission_name ", typePermissions);
            return ExecTab(query);
        }

        /// <summary>
        /// Возвращает таблицу с одой строкой, содержажую информацию об сервере и текущей БД
        /// </summary>
        /// <returns></returns>
        public DataTable GetDbInfo()
        {
            string query = string.Format("SELECT  ServerName = @@SERVERNAME , "+
                "ServerVersion = @@VERSION, ServerStarted = (select create_date from sys.databases where name = 'tempdb'), "+
                "DaysRunning = (select DATEDIFF(DAY, create_date, GETDATE())  from sys.databases where name = 'tempdb') , "+
                "name AS DbCurrentName, recovery_model_Desc AS DbRecoveryModel, Compatibility_level AS DbCompatiblityLevel, create_date as DbCreateDate, "+
                "state_desc as DbState FROM sys.databases where name = DB_NAME()");
            return ExecTab(query);
        }

        /// <summary>
        /// Возвращает информационную таблицу об сделаных бэкапах БД
        /// Таблица содержит два столбца: дата время создания бэкапа и полный путь файла(на сервере)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDbBackups()
        {
            string query = string.Format("SELECT  b.Backup_finish_date  as finishDate, bmf.Physical_Device_name as pathFile FROM sys.databases d "+
                "INNER JOIN msdb..backupset b ON b.database_name = d.name AND b.[type] = 'D' "+
                "INNER JOIN msdb.dbo.backupmediafamily bmf ON b.media_set_id = bmf.media_set_id "+
                "where d.Name = DB_NAME() ORDER BY b.Backup_finish_date DESC ");
            return ExecTab(query);
        }

        /// <summary>
        /// По коду уровня - Возвращает торговое название Sql сервера 
        /// </summary>
        /// <param name="levelDesignation">SQL код уровня </param>
        /// <returns></returns>
        public string GetNameOfSqlProduct(int levelDesignation)
        {
            string product = "не определен";
            switch (levelDesignation)
            {
                case 80:
                    product = "SQL Server 2000";
                    break;
                case 90:
                    product = "SQL Server 2005";
                    break;
                case 100:
                    product = "SQL Server 2008";
                    break;
                case 110:
                    product = "SQL Server 2012";
                    break;
                case 120:
                    product = "SQL Server 2014";
                    break;
                case 130:
                    product = "SQL Server 2016";
                    break;
                case 140:
                    product = "SQL Server 2017";
                    break;
            }
            return string.Format("{0} ({1})",product, levelDesignation);
        }

        /// <summary>
        /// Возвращает строка со списком полей таблицы базы данных через запятую ,
        /// которые допускаются к выборке для структуры XML
        /// </summary>
        /// <param name="object_id">ID таблицы в SQL сервере</param>
        /// <returns></returns>
        public string GetFieldsTable(int object_id)
        {
            DataTable table = new DataTable();
            string query = string.Format("select fields = (STUFF((select ','+name from sys.columns where object_id={0} and system_type_id not in (34,35,99,173)  " +
                "and system_type_id<240 order by column_id FOR XML PATH('')),1,1,'') )", object_id);
            table = ExecTab(query);
            if (table == null || table.Rows.Count == 0)
                return "";
            else
                return table.Rows[0][0].ToString().Trim();
        }

        /// <summary>
        /// Возвращает TRUE если задание разрешение имеется
        /// у пользователя на уровне SERVER
        /// </summary>
        /// <param name="FulNamePermission"></param>
        /// <returns></returns>        
        public bool IsExistServerPermission(string FulNamePermission)
        {
            return IsReturnRowsExist(string.Format("SELECT * FROM fn_my_permissions(NULL, 'SERVER') where permission_name = '{0}'", FulNamePermission));
        }

        /// <summary>
        /// Возвращает логическое true, если Sql запрос возвратил хотябы одну строку в таблице,
        /// иначе - false
        /// </summary>
        /// <param name="sqlCommand">Sql запрос</param>
        /// <returns></returns>
        private bool IsReturnRowsExist(string sqlCommand)
        {
            DataTable table = new DataTable();
            table = ExecTab(sqlCommand);
            if (table == null || table.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Возвращает true если заданое имя представление 
        /// имеется в БД
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public bool IsExsistView(string viewName)
        {
            return IsExsist(viewName, "V");
        }

        /// <summary>
        /// Возвращает true если заданое имя таблицы  
        /// имеется в БД
        /// </summary>
        /// <param name="schemaName">схема таблицы</param>
        /// <param name="tableName">таблица</param>
        /// <returns></returns>
        public bool IsExsistTable(string schemaName, string tableName)
        {
            return IsReturnRowsExist(string.Format("select tab.object_id FROM sys.tables tab left join sys.schemas sc on tab.schema_id = sc.schema_id " +
                "where rtrim(sc.name)='{0}' and rtrim(tab.name)='{1}' ", schemaName, tableName));
        }

        /// <summary>
        /// Возвращает true если заданое имя объекта(без схемы)
        /// имеется в БД
        /// </summary>
        /// <param name="objName">Имя объекта (без схемы)</param>
        /// <param name="sqlShortType">SQL Тип объекта</param>
        /// <returns></returns>
        public bool IsExsist(string objName, string sqlShortType)
        {
            return IsReturnRowsExist(string.Format("SELECT id FROM sys.SYSOBJECTS WHERE xtype = '{1}' and name = '{0}' ", objName, sqlShortType));
        }

        /// <summary>
        /// Возвращает true если заданое имя схемы
        /// имеется в БД
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public bool IsExsistSchema(string schemaName)
        {
            return IsReturnRowsExist(string.Format("SELECT * FROM sys.schemas where name='{0}'", schemaName));
        }

        /// <summary>
        /// Начало транзакции для SQL соединения (подерживается ТОЛЬКО ОДНА для объекта !!!)
        /// при повторном вызове без закрытия транзакции - игнорирование
        /// </summary>
        public void BeginTransaction()
        {
            if (pSQLtransaction == null)
                pSQLtransaction = SqlConnection.BeginTransaction("One");
        }

        /// <summary>
        /// Завершить транзакцию для SQL соединения, если она была вызвана ранее
        /// </summary>
        public void CommitTransaction()
        {
            if (pSQLtransaction != null)
                pSQLtransaction.Commit();
            pSQLtransaction = null;
        }

        /// <summary>
        ///  Откатить (отмена ранее сделаных измов) транзакцию для SQL соединения, если она была вызвана ранее
        /// </summary>
        public void RollbackTransaction()
        {
            if (pSQLtransaction != null)
                pSQLtransaction.Rollback();
            pSQLtransaction = null;
        }

        /// <summary>
        /// Выполнение SQL инструкции на сервере без принятия результата,
        /// если удачно возвращается TRUE иначе FALSE
        /// </summary>
        /// <param name="query">T SQL инструкция</param>
        /// <returns></returns>
        public bool Exec(string query)
        {
            pSqlErrorMessage = "";
            if (query == null || (query.Trim()) == "")
            {
                pSqlErrorMessage = ("Ошибка при формировании SQL запроса: Сформирована пустоя (или NULL) команда для SQL сервера.");
                return false;
            }

            pSqlCommand = query;
            SqlCommand sqlCommmand = new SqlCommand(query, SqlConnection);

            if (pSQLtransaction != null)
                sqlCommmand.Transaction = pSQLtransaction;

            pSqlServer = SqlConnection.DataSource.ToString();
            pSqlBd = SqlConnection.Database.ToString();

            try
            {
                sqlCommmand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                LogSqlErrors(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Выполнение SQL инструкции на сервере в НЕ АСИНХРОННОМ РЕЖИМЕ, результат возвращается в виде таблицы  DataTable,
        /// если неудачно возвращается NULL
        /// </summary>
        /// <param name="query">T SQL инструкция</param>
        /// <returns></returns>
        public DataTable ExecTab(string query)
        {
            pSqlErrorMessage = "";

            if (query == null | SqlConnection == null | (query.Trim()) == "")
            {
                pSqlErrorMessage = ("Ошибка при формировании SQL запроса: Сформирована пустоя (или NULL) команда для SQL сервера.");
                return null;
            }
            pSqlCommand = query;
            DataTable tableQuery = new DataTable();
            SqlCommand sqlCommmand = new SqlCommand(query, SqlConnection);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommmand);
            if (pSQLtransaction != null)
                sqlCommmand.Transaction = pSQLtransaction;

            pSqlServer = SqlConnection.DataSource.ToString();
            pSqlBd = SqlConnection.Database.ToString();

            try
            {
                // заполняем таблицу
                sqlAdapter.Fill(tableQuery);
            }
            catch (SqlException ex)
            {
                LogSqlErrors(ex);
                return null;
            }
            catch (Exception ex)
            {
                pSqlErrorMessage = String.Format("Ошибка при выполнении запроса к SQL:/n{0}", ex.Message);
                return null;
            }
            return tableQuery;
        }

        /// <summary>
        /// формируем текст информации по SQL ошибке, а затем записываем в лог SqlError.log
        /// в каталог запуска программы /log. Если нет каталога log - создаем, если нет файла - создаем, 
        /// если превышен размер (500 kB) - переименовываем  в oldSqlError_dd_mm_yy.log
        /// </summary>
        /// <param name="exception"></param>
        private void LogSqlErrors(SqlException exception)
        {
            string err_msg = String.Format("********** {0:dd.MM.yyyy HH:mm:ss} **********", DateTime.Now);
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                err_msg += "\n" + "# " + (i + 1).ToString() + "\n";
                err_msg += exception.Errors[i].Source.ToString() + "   вернул ошибку № " + exception.Errors[i].Number.ToString() + "\n";
                if (i == 0) err_msg += "Сервер      : " + pSqlServer + "      База данных : " + pSqlBd + "\n";
                err_msg += "Ошибка      : " + exception.Errors[i].Message.ToString() + "\n";
                if (i == 0) err_msg += "Пользователь: " + pSqlLocalHost + " # " + pSqlUser + "\n";
                if (i == 0) err_msg += "Команда     : " + pSqlCommand + "\n";
                try
                {
                    string msg= exception.Errors[i].Procedure != null ? exception.Errors[i].Procedure.ToString() : "---   нет доступа к SQL соединению   ---" + "\n";
                    err_msg += msg;
                }
                catch { }
            }

            pSqlErrorMessage = String.Format("Ошибка на SQL сервере.{0}", err_msg);
            string Home_Dir_APP = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (!Directory.Exists(Home_Dir_APP + "/LOG"))
                {
                    Directory.CreateDirectory(Home_Dir_APP + "/LOG");
                }
                FileStream fs1 = new FileStream(Home_Dir_APP + "/LOG/SqlError.log", FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= 500000) // - предельный размер лог-файла в байтах
                {
                    File.Move(Home_Dir_APP + "/LOG/SqlError.log", Home_Dir_APP + "/LOG/oldSqlError_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + @".log");
                }
                FileStream fs2 = new FileStream(Home_Dir_APP + "/LOG/SqlError.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2, System.Text.Encoding.Default);
                sw.WriteLine(err_msg);
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            { pSqlErrorMessage = String.Format("Ошибка на локальном компьюторе.\nНе удалось записать сообщение " +
                "об ошибки SQL сервера в локальный лог файл.\n{0}", ex.Message); }
        }

        /// <summary>
        /// Закрывает открытое SQL соединение, 
        /// если удачно возвращается TRUE иначе FALSE
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            CommitTransaction(); 
            if (SqlConnection == null)
                return true;
            try
            {
                SqlConnection.Close();
                pSqlServer = "";
                pSqlBd = "";
                pSqlCommand = "";
            }
            catch
            {
                SqlConnection = null;
                return false;
            }
            return true;
        }


    }
}
