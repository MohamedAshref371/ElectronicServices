using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                      "CREATE TABLE customers ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL );" +
                                      "CREATE TABLE transactions ( id INTEGER PRIMARY KEY AUTOINCREMENT, customer_id INTEGER NOT NULL, date TEXT NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL, note TEXT, FOREIGN KEY(customer_id) REFERENCES customers(id) );" +
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
