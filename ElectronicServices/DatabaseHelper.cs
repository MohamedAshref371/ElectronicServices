using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
                                      "CREATE TABLE transactions ( id INTEGER PRIMARY KEY AUTOINCREMENT, customer_id INTEGER NOT NULL, date INTEGER NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL, note TEXT, FOREIGN KEY(customer_id) REFERENCES customers(id) );" +
                                      "CREATE TABLE wallets ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL);" +
                                      "CREATE TABLE wallet_closures ( id INTEGER PRIMARY KEY AUTOINCREMENT, date TEXT NOT NULL );" +
                                      "CREATE TABLE wallet_closure_details ( id INTEGER PRIMARY KEY AUTOINCREMENT, closure_id INTEGER NOT NULL, wallet_id INTEGER NOT NULL, balance REAL NOT NULL, FOREIGN KEY(closure_id) REFERENCES wallet_closures(id), FOREIGN KEY(wallet_id) REFERENCES wallets(id) );" +
                                      "CREATE TABLE daily_closures ( id INTEGER PRIMARY KEY AUTOINCREMENT, date TEXT NOT NULL, total_cash REAL NOT NULL, total_wallets REAL NOT NULL, total_electronic REAL NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL );" +
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
        {
            if (!success) return -1;
            try
            {
                conn.Open();
                command.CommandText = "SELECT MAX(id) FROM customers;";
                reader = command.ExecuteReader();
                if (!reader.Read()) return -1;
                if (reader.IsDBNull(0)) return 1; // If no customers exist, return 1
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
        public static bool SearchWithExactCustomerName(string name)
        {
            if (!success) return false;
            try
            {
                conn.Open();
                command.CommandText = $"SELECT * FROM customers WHERE name = '{name}'";
                reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception ex)
            {
                Program.LogError(ex, true);
                return false;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        public static CustomerRowData[] GetCustomers(string name = "")
        {
            if (name != "") name = $"WHERE c.name LIKE '%{name}%'";
            string sql = $"SELECT c.id, c.name, COALESCE(SUM(t.credit), 0) AS Pay, COALESCE(SUM(t.debit), 0) AS Take FROM customers c LEFT JOIN transactions t ON t.customer_id = c.id {name} GROUP BY c.id, c.name";
            return SelectMultiRows(sql, GetCustomerData);
        }


        private static CustomerRowData GetCustomerData()
        {
            return new CustomerRowData
            {
                Code = reader.GetInt32(0),
                Name = reader.GetString(1),
                Pay = reader.GetFloat(2),
                Take = reader.GetFloat(3),
            };
        }


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

        public static bool AddTransaction(TransactionRowData data)
            => ExecuteNonQuery($"INSERT INTO transactions (customer_id, date, credit, debit, note) VALUES ({data.Code}, {data.Date.Ticks}, {data.Pay}, {data.Take}, '{data.Note}')") >= 0;

        public static bool EditTransaction(int id, float pay, float take)
            => ExecuteNonQuery($"UPDATE transactions SET credit = {pay}, debit = {take} WHERE id = {id}") >= 0;

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
