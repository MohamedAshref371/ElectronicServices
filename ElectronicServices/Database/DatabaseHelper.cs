using System.Data.SQLite;

namespace ElectronicServices
{
    static class DatabaseHelper
    {
        private static bool success = false;
        private static readonly int classVersion = 1;
        private static readonly string dataFolder = "data", imagesFolder = $"{dataFolder}\\images\\", databaseFile = $"{dataFolder}\\ProgData.ds";
        private static readonly SQLiteConnection conn = new($"Data Source={databaseFile};Version=3;");
        private static readonly SQLiteCommand command = new(conn);
        private static SQLiteDataReader reader;
        private static bool copyData;

        public static int Version { get; private set; }
        public static DateTime CreateDate { get; private set; }
        public static string Comment { get; private set; }

        static DatabaseHelper() => SafetyExamination();

        static void SafetyExamination()
        {
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            bool created = true;
            if (File.Exists(databaseFile))
                copyData = true;
            else
                created = CreateDatabase();

            if (created) ReadMetadata();
        }

        static bool CreateDatabase()
        {
            try
            {
                conn.Open();
                command.CommandText = "CREATE TABLE metadata (version INTEGER PRIMARY KEY, create_date INTEGER, comment TEXT);" +
                                      "CREATE TABLE customers ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL );" +
                                      "CREATE TABLE payapp ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL);" +
                                      "CREATE TABLE transactions ( id INTEGER PRIMARY KEY AUTOINCREMENT, customer_id INTEGER NOT NULL, date TEXT NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL, credit_payapp INTEGER NOT NULL, debit_payapp INTEGER NOT NULL, note TEXT, FOREIGN KEY(customer_id) REFERENCES customers(id), FOREIGN KEY(credit_payapp) REFERENCES payapp(id), FOREIGN KEY(debit_payapp) REFERENCES payapp(id) );" +
                                      "CREATE TABLE payapp_closures (date TEXT NOT NULL, payapp_id INTEGER NOT NULL, balance REAL NOT NULL, FOREIGN KEY(payapp_id) REFERENCES payapp(id), PRIMARY KEY (date, payapp_id) );" +
                                      "CREATE TABLE daily_closures ( date TEXT NOT NULL PRIMARY KEY AUTOINCREMENT, total_wallets REAL NOT NULL, total_cash REAL NOT NULL, total_electronic REAL NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL );" +
                                      "INSERT INTO payapp VALUES (-1, '');" +
                                      "INSERT INTO payapp VALUES (0, 'نقدا');" +
                                      $"INSERT INTO metadata VALUES ({classVersion}, '{DateTime.Now.Ticks}', 'https://github.com/MohamedAshref371');";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError(ex, true);
                return false;
            }
            finally { conn.Close(); }
            return true;
        }

        static void ReadMetadata()
        {
            try
            {
                conn.Open();
                command.CommandText = "SELECT version, create_date, comment FROM metadata";
                reader = command.ExecuteReader();
                if (!reader.Read()) return;
                Version = reader.GetInt32(0);
                if (Version != classVersion) return;
                CreateDate = new DateTime(reader.GetInt64(1));
                Comment = reader.GetString(2);
                reader.Close();

                success = true;
            }
            catch (Exception ex)
            {
                LogError(ex, true);
                success = false;
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }
        
        public static int GetCustomerNextId()
            => GetTableNextId("customers");

        public static int SearchWithExactCustomerName(string name)
        {
            if (!success) return -1;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT id FROM customers WHERE name = '{name}'";
                reader = command.ExecuteReader();
                if (!reader.Read()) return 0;
                return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return -1;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static int SearchWithExactPayappName(string name)
        {
            if (!success) return -1;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT id FROM payapp WHERE name = '{name}'";
                reader = command.ExecuteReader();
                if (!reader.Read()) return 0;
                return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return -1;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static int GetTransactionNextId()
            => GetTableNextId("transactions");

        public static CustomerRowData[] GetCustomers(string name = "")
        {
            if (name != "") name = $"WHERE c.name LIKE '%{name}%'";
            string sql = $"SELECT c.id, c.name, COALESCE(SUM(t.credit), 0) AS Pay, COALESCE(SUM(t.debit), 0) AS Take FROM customers c LEFT JOIN transactions t ON t.customer_id = c.id {name} GROUP BY c.id, c.name";
            return SelectMultiRows(sql, GetCustomerData);
        }

        public static TransactionRowData[] GetTransactions(int custId)
        {
            string cond = custId >= 1 ? $"WHERE customer_id = {custId}" : "";
            string sql = $"SELECT t.id, t.customer_id, c.name, t.date, t.credit, t.debit, t.note FROM customers c INNER JOIN transactions t ON t.customer_id = c.id {cond} ORDER BY t.date";
            return SelectMultiRows(sql, GetTransactionData);
        }

        public static TransactionRowData[] GetTransactions(int custId, string date, int method)
        {
            string dt = method switch
            {
                6 => "%Y-%m-%d",
                7 => "%Y-%m",
                _ => "%Y"
            };
            string cond = custId >= 1 ? $"WHERE customer_id = {custId}" : "";
            if (cond == "") cond = "WHERE ";
            else cond += " AND";
            cond += $" strftime('{dt}', date) = '{date}'";

            string sql = $"SELECT t.id, t.customer_id, c.name, t.date, t.credit, t.debit, t.note FROM customers c INNER JOIN transactions t ON t.customer_id = c.id {cond} ORDER BY t.date";
            return SelectMultiRows(sql, GetTransactionData);
        }

        public static int IsThereTransactions(int custId)
        {
            if (!success) return -1;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT 1 FROM transactions WHERE customer_id = {custId} LIMIT 1";
                reader = command.ExecuteReader();;
                return reader.HasRows ? 1 : 0;
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return -1;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        private static int GetTableNextId(string table)
        {
            if (!success) return -1;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT seq FROM sqlite_sequence WHERE name = '{table}'";
                reader = command.ExecuteReader();
                if (!reader.Read()) return 1;
                if (reader.IsDBNull(0)) return 1;
                return reader.GetInt32(0) + 1;
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return -1;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static KeyValuePair<int, string>[] GetCustomersNames()
        {
            string sql = $"SELECT id, name FROM customers ORDER BY name";
            return SelectMultiRows(sql, () => new KeyValuePair<int, string>(reader.GetInt32(0), reader.GetString(1)) );
        }

        public static string[] GetPayappsNames(bool withoutCash = false)
        {
            string sql = $"SELECT name FROM payapp WHERE id >{(withoutCash ? "" : "=")} 0";
            return SelectMultiRows(sql, () => reader.GetString(0));
        }

        public static string[] GetTransactionsDate(bool last5Rows = true)
        {
            string sql;
            if (last5Rows)
                sql = "SELECT * FROM ( SELECT date FROM transactions ORDER BY id DESC LIMIT 5 ) ORDER BY id ASC";
            else
                sql = "SELECT date FROM transactions ORDER BY id ASC";

            return SelectMultiRows(sql, () => reader.GetString(0));
        }

        public static float GetPayappDateField(int payapp, string date, bool pay)
        {
            string sql; 
            if (pay)
                sql = $"SELECT COALESCE( SUM( CASE WHEN credit_payapp = {payapp} THEN credit ELSE 0 END ), 0 ) FROM transactions WHERE date = '{date}'";
            else // take
                sql = $"SELECT COALESCE( SUM( CASE WHEN debit_payapp = {payapp} THEN debit ELSE 0 END ), 0 ) FROM transactions WHERE date = '{date}'";

            return SelectMultiRows(sql, () => reader.GetFloat(0))[0];
        }

        public static float GetCerditCashField(string date)
        {
            string sql = $"SELECT COALESCE( SUM( CASE WHEN credit_payapp = 0 THEN credit ELSE 0 END ), 0 ) FROM transactions WHERE date = '{date}'";

            return SelectMultiRows(sql, () => reader.GetFloat(0))[0];
        }

        public static float GetDebitCashField(string date)
        {
            string sql = $"SELECT COALESCE( SUM( CASE WHEN debit_payapp = 0 THEN debit ELSE 0 END ), 0 ) FROM transactions WHERE date = '{date}'";

            return SelectMultiRows(sql, () => reader.GetFloat(0))[0];
        }

        private static CustomerRowData GetCustomerData()
        {
            return new CustomerRowData
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Pay = reader.GetFloat(2),
                Take = reader.GetFloat(3),
            };
        }

        private static TransactionRowData GetTransactionData()
        {
            return new TransactionRowData
            {
                Id = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                Name = reader.GetString(2),
                Date = reader.GetString(3),
                Pay = reader.GetFloat(4),
                Take = reader.GetFloat(5),
                Note = reader.GetString(6)
            };
        }

        public static FieldData[] CustomerFieldSearch()
        {
            string sql = $"SELECT name AS text, COUNT(name) AS count FROM customers GROUP BY name ORDER BY text";

            return SelectMultiRows(sql, GetFieldData);
        }

        public static FieldData[] TransFieldSearch()
        {
            string sql = $"SELECT c.name AS text, COUNT(t.customer_id) AS count FROM customers c LEFT JOIN transactions t ON t.customer_id = c.id GROUP BY c.name ORDER BY text";

            return SelectMultiRows(sql, GetFieldData);
        }

        public static FieldData[] TransFieldSearch(int custId, int method)
        {
            string dt = method switch
            {
                6 => "%Y-%m-%d",
                7 => "%Y-%m",
                _ => "%Y"
            };
            string cond = custId >= 1 ? $"WHERE customer_id = {custId}" : "";

            string sql = $"SELECT strftime('{dt}', date) AS text, COUNT(*) AS count FROM transactions {cond} GROUP BY text ORDER BY text";

            return SelectMultiRows(sql, GetFieldData);
        }

        private static FieldData GetFieldData()
        {
            return new FieldData
            {
                Text = (string)reader["text"],
                Count = reader.GetInt32(1),
            };
        }

        public static float? GetTransactionBefore(int id, int customerId)
        {
            if (!success) return null;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT SUM(credit - debit) FROM transactions WHERE customer_id = {customerId} AND id < {id}";
                reader = command.ExecuteReader();
                if (!reader.Read()) return 0f;
                return reader.IsDBNull(0) ? 0f : reader.GetFloat(0);
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return null;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static float[] GetCreditAndDept()
        {
            if (!success) return [0f, 0f];
            try
            {
                conn.Open();
                command.CommandText = "SELECT SUM(CASE WHEN (debit - credit) > 0 THEN (debit - credit) ELSE 0 END) AS comp_credit, SUM(CASE WHEN (credit - debit) > 0 THEN (credit - debit) ELSE 0 END) AS comp_debit FROM " +
                                        "( SELECT COALESCE(SUM(credit), 0) AS credit, COALESCE(SUM(debit), 0) AS debit FROM transactions GROUP BY customer_id )";
                reader = command.ExecuteReader();
                if (!reader.Read()) return [0f, 0f];
                return [reader.IsDBNull(0) ? 0f : reader.GetFloat(0), reader.IsDBNull(1) ? 0f : reader.GetFloat(1)];
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return [0f, 0f];
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static float[] GetCreditAndDept(string date)
        {
            if (!success) return [0f, 0f];
            try
            {
                conn.Open();
                command.CommandText = "SELECT SUM(CASE WHEN (debit - credit) > 0 THEN (debit - credit) ELSE 0 END) AS comp_credit, SUM(CASE WHEN (credit - debit) > 0 THEN (credit - debit) ELSE 0 END) AS comp_debit FROM " +
                                        $"( SELECT COALESCE(SUM(credit), 0) AS credit, COALESCE(SUM(debit), 0) AS debit FROM transactions WHERE date = '{date}' GROUP BY customer_id )";
                reader = command.ExecuteReader();
                if (!reader.Read()) return [0f, 0f];
                return [reader.IsDBNull(0) ? 0f : reader.GetFloat(0), reader.IsDBNull(1) ? 0f : reader.GetFloat(1)];
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return [0f, 0f];
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static float[] GetPayappClosure(string date)
            => SelectMultiRows($"SELECT balance FROM payapp_closures WHERE date = '{date}' ORDER BY payapp_id", () => reader.GetFloat(0));

        public static float GetSumPrevPayappClosure(string date)
            => SelectMultiRows($"SELECT COALESCE(SUM(balance), 0) FROM payapp_closures WHERE date = (SELECT MAX(date) FROM payapp_closures WHERE date < '{date}')", () => reader.GetFloat(0))[0];

        private static T[] SelectMultiRows<T>(string sql, Func<T> method)
        {
            if (!success) return [];
            try
            {
                conn.Open();
                command.CommandText = sql;
                reader = command.ExecuteReader();
                List<T> list = [];
                while (reader.Read())
                    list.Add(method());
                return [.. list];
            }
            catch (Exception ex)
            {
                LogError(ex, true);
                return [];
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static bool AddCustomer(string name)
            => ExecuteNonQuery($"INSERT INTO customers (name) VALUES ('{name}')") >= 0;

        public static bool AddPayapp(string name)
            => ExecuteNonQuery($"INSERT INTO payapp (name) VALUES ('{name}')") >= 0;

        public static bool EditCustomer(int id, string name)
            => ExecuteNonQuery($"UPDATE customers SET name = '{name}' WHERE id = {id}") >= 0;

        public static bool ResetCustomer(int id)
            => ExecuteNonQuery($"DELETE FROM transactions WHERE customer_id = {id}") >= 0;

        public static bool DeleteCustomer(int id)
            => ExecuteNonQuery($"DELETE FROM customers WHERE id = {id}") >= 0;

        public static bool AddTransaction(TransactionRowData data)
            => ExecuteNonQuery($"INSERT INTO transactions (customer_id, date, credit, debit, credit_payapp, debit_payapp, note) VALUES ({data.CustomerId}, '{data.Date}', {data.Pay}, {data.Take}, {data.PayWith}, {data.TakeWith}, '{data.Note}')") >= 0;

        public static bool EditTransaction(int id, float pay, float take)
            => ExecuteNonQuery($"UPDATE transactions SET credit = {pay}, debit = {take} WHERE id = {id}") >= 0;

        public static bool DeleteTransaction(int id)
            => ExecuteNonQuery($"DELETE FROM transactions WHERE id = {id}") >= 0;

        public static bool SetPayappClosure(string date, int payappId, float balance)
            => ExecuteNonQuery($"INSERT INTO payapp_closures (date, payapp_id, balance) VALUES ('{date}', {payappId}, {balance}) ON CONFLICT(date, payapp_id) DO UPDATE SET balance = excluded.balance") >= 0;

        private static int ExecuteNonQuery(string sql)
        {
            if (!success || sql is null || sql.Trim() == "") return -1;
            if (copyData)
            {
                copyData = false;
                DatabaseBackup();
            }

            try
            {
                conn.Open();
                command.CommandText = sql;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError(ex, true);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        private static void DatabaseBackup()
        {
            if (!success) return;

            if (!Directory.Exists($"{dataFolder}\\backup"))
                Directory.CreateDirectory($"{dataFolder}\\backup");

            if (File.Exists(databaseFile))
                File.Copy(databaseFile, $"{dataFolder}\\backup\\{DateTime.Now.Ticks}.ds");
        }

        public static void LogError(Exception ex, bool inTryCatch = false)
            => File.AppendAllText("Errors.txt", $"{DateTime.Now}{(inTryCatch ? "  -  Inside Custom Try-Catch Block" : "")}\n{ex.Message}\n{ex.StackTrace}\n------------------\n\n");

    }
}
