
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data.SQLite;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ElectronicServices
{
    static class DatabaseHelper
    {
        private static bool success = false;
        private static readonly int classVersion = 4;
        private static readonly string dataFolder = "data", imagesFolder = $"{dataFolder}\\images\\", databaseFile = $"{dataFolder}\\ProgData.ds";
        private static readonly SQLiteConnection conn = new($"Data Source={databaseFile};Version=3;");
        private static readonly SQLiteCommand command = new(conn);
        private static SQLiteDataReader reader;
        private static bool copyData;

        public static int Version { get; private set; }
        public static DateTime CreateDate { get; private set; }
        public static string Comment { get; private set; }

        private static string WalletsTableColumnsNames = "maximum_withdrawal, maximum_deposit, withdrawal_remaining, deposit_remaining, balance, type, comment";

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
                command.CommandText = "CREATE TABLE metadata (version INTEGER PRIMARY KEY, create_date INTEGER, comment TEXT );" +
                                      "CREATE TABLE customers ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL );" +
                                      "CREATE TABLE payapp ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL );" +
                                      "CREATE TABLE transactions ( id INTEGER PRIMARY KEY AUTOINCREMENT, customer_id INTEGER REFERENCES customers(id) NOT NULL, date TEXT NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL, credit_payapp INTEGER REFERENCES payapp(id) NOT NULL, debit_payapp INTEGER REFERENCES payapp(id) NOT NULL, note TEXT NOT NULL );" +
                                      "CREATE TABLE payapp_closures ( id INTEGER PRIMARY KEY AUTOINCREMENT, date TEXT NOT NULL, closured INTEGER NOT NULL );" +
                                      "CREATE TABLE payapp_closures_details (closure_id INTEGER REFERENCES payapp_closures(id), payapp_id INTEGER REFERENCES payapp(id), balance REAL NOT NULL, PRIMARY KEY (closure_id, payapp_id) );" +
                                      "CREATE TABLE daily_closures ( date TEXT PRIMARY KEY, total_wallets REAL NOT NULL, total_cash REAL NOT NULL, total_electronic REAL NOT NULL, credit REAL NOT NULL, debit REAL NOT NULL, closure_id INTEGER REFERENCES payapp_closures(id) NOT NULL );" +
                                      "CREATE TABLE wallet_type ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE NOT NULL );" +
                                      "CREATE TABLE wallets ( phone TEXT PRIMARY KEY, maximum_withdrawal REAL NOT NULL, maximum_deposit REAL NOT NULL, withdrawal_remaining REAL NOT NULL, deposit_remaining REAL NOT NULL, balance REAL NOT NULL, type INTEGER REFERENCES wallet_type(id) NOT NULL, comment TEXT NOT NULL );" +
                                      "CREATE TABLE records ( phone TEXT REFERENCES wallets(phone), date TEXT, withdrawal_remaining REAL NOT NULL, deposit_remaining REAL NOT NULL, withdrawal_amount REAL NOT NULL, deposit_amount REAL NOT NULL, balance REAL NOT NULL, comment TEXT NOT NULL, PRIMARY KEY (phone, date) );" +
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
           => SelectRow($"SELECT id FROM customers WHERE name = '{name}'", () => reader.GetInt32(0));

        public static int SearchWithExactPayappName(string name)
           => SelectRow($"SELECT id FROM payapp WHERE name = '{name}'", () => reader.GetInt32(0));

        public static int SearchWithExactWalletType(string name)
           => SelectRow($"SELECT id FROM wallet_type WHERE name = '{name}'", () => reader.GetInt32(0));

        public static bool IsWalletExist(string phone)
           => SelectRow($"SELECT 1 FROM wallets WHERE phone = '{phone}'", () => reader.GetInt32(0) == 1);

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
            string sql = $"SELECT t.id, t.customer_id, c.name, t.date, t.credit, t.debit, t.credit_payapp, t.debit_payapp, t.note FROM customers c INNER JOIN transactions t ON t.customer_id = c.id {cond} ORDER BY t.date DESC";
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

            string sql = $"SELECT t.id, t.customer_id, c.name, t.date, t.credit, t.debit, t.credit_payapp, t.debit_payapp, t.note FROM customers c INNER JOIN transactions t ON t.customer_id = c.id {cond} ORDER BY t.date DESC";
            return SelectMultiRows(sql, GetTransactionData);
        }

        public static bool IsThereTransactions(int custId)
            => SelectRow($"SELECT 1 FROM transactions WHERE customer_id = {custId} LIMIT 1", () => reader.GetInt32(0) == 1);

        public static bool IsThereRecords(string phone)
            => SelectRow($"SELECT 1 FROM records WHERE phone = '{phone}' LIMIT 1", () => reader.GetInt32(0) == 1);

        public static WalletRowData[] GetWallets(string phone = "")
        {
            if (phone != "") phone = $"WHERE phone LIKE '%{phone}%'";
            return SelectMultiRows($"SELECT * FROM wallets {phone}", GetWalletData);
        }

        public static WalletRowData[] GetWallets(int type)
        {
            string cond = "";
            if (type >= 1) cond = $"WHERE type = {type}";
            return SelectMultiRows($"SELECT * FROM wallets {cond}", GetWalletData);
        }

        public static RecordRowData[] GetRecords(string phone = "")
        {
            if (phone != "") phone = $"WHERE phone = '{phone}'";
            return SelectMultiRows($"SELECT * FROM records {phone} ORDER BY date DESC", GetRecordData);
        }

        public static RecordRowData[] GetRecords(int type)
        {
            string cond = "";
            if (type >= 1) cond = $"WHERE w.type = {type}";
            return SelectMultiRows($"SELECT r.* FROM records r JOIN ( SELECT phone, MAX(date) AS max_date FROM records GROUP BY phone ) latest ON r.phone = latest.phone AND r.date = latest.max_date JOIN wallets w ON r.phone = w.phone {cond} ORDER BY r.date DESC", GetRecordData);
        }

        public static float GetTotalWalletsBalance(int type)
        {
            string cond = "";
            if (type >= 1) cond = $"WHERE type = {type}";
            return SelectRow($"SELECT SUM(balance) FROM wallets {cond}", () => reader.GetFloat(0));
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

        private static int GetPayappNextId()
            => GetTableNextId("payapp");

        public static string[] GetPayappsNames(bool withoutCash = false)
            => SelectMultiRows($"SELECT name FROM payapp WHERE id >{(withoutCash ? "" : "=")} 0", () => reader.GetString(0));
        
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
                sql = $"SELECT COALESCE( SUM( CASE WHEN credit_payapp = {payapp} THEN credit ELSE 0 END ), 0 ) FROM transactions WHERE date LIKE '{date}%'";
            else // take
                sql = $"SELECT COALESCE( SUM( CASE WHEN debit_payapp = {payapp} THEN debit ELSE 0 END ), 0 ) FROM transactions WHERE date LIKE '{date}%'";

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
                PayWith = reader.GetInt32(6),
                TakeWith = reader.GetInt32(7),
                Note = reader.GetString(8)
            };
        }

        private static WalletRowData GetWalletData()
        {
            return new WalletRowData
            {
                Phone = reader.GetString(0),
                MaximumWithdrawal = reader.GetFloat(1),
                MaximumDeposit = reader.GetFloat(2),
                WithdrawalRemaining = reader.GetFloat(3),
                DepositRemaining = reader.GetFloat(4),
                Balance = reader.GetFloat(5),
                Type = reader.GetInt32(6),
                Comment = reader.GetString(7)
            };
        }

        private static RecordRowData GetRecordData()
        {
            return new RecordRowData
            {
                Phone = reader.GetString(0),
                Date = reader.GetString(1),
                WithdrawalRemaining = reader.GetFloat(2),
                DepositRemaining = reader.GetFloat(3),
                Withdrawal = reader.GetFloat(4),
                Deposit = reader.GetFloat(5),
                Balance = reader.GetFloat(6),
                Comment = reader.GetString(7)
            };
        }

        public static FieldData[] CustomerFieldSearch()
        {
            string sql = $"SELECT name, 1 FROM customers ORDER BY name";

            return SelectMultiRows(sql, GetFieldData);
        }

        public static FieldData[] TransFieldSearch()
        {
            string sql = $"SELECT c.name, COUNT(t.customer_id) FROM customers c LEFT JOIN transactions t ON t.customer_id = c.id GROUP BY c.name ORDER BY c.name";

            return SelectMultiRows(sql, GetFieldData);
        }

        public static FieldData[] WalletFieldSearch()
        {
            string sql = $"SELECT phone, 1 FROM wallets ORDER BY phone";

            return SelectMultiRows(sql, GetFieldData);
        }

        public static FieldData[] WalletTypeFieldSearch()
        {
            string sql = $"SELECT t.name, COUNT(t.id) FROM wallets w JOIN wallet_type t ON w.type = t.id GROUP BY t.id ORDER BY t.name";

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
                Text = reader.GetString(0),
                Count = reader.GetInt32(1),
            };
        }

        private static SumDate GetSumDate()
        {
            return new SumDate
            {
                Id = reader.GetInt32(0),
                Date = reader.GetString(1),
                Sum = reader.GetFloat(2),
            };
        }

        private static SumDate GetSumDate2()
        {
            return new SumDate
            {
                Date = reader.GetString(0),
                Sum = reader.GetFloat(1),
            };
        }

        private static DailyClosureData GetDailyClosureData()
        {
            return new DailyClosureData
            {
                Date = reader.GetString(0),
                TotalWallets = reader.GetFloat(1),
                TotalCash = reader.GetFloat(2),
                TotalElectronic = reader.GetFloat(3),
                Credit = reader.GetFloat(4),
                Debit = reader.GetFloat(5),
                PayappClosureId = reader.GetInt32(6),
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

        public static float[] GetCreditAndDept(string date = "", bool prev = false)
        {
            if (!success) return [0f, 0f];
            try
            {
                string cond = date == "" ? "" : (prev ? $"WHERE date <= '{date}'" : $"WHERE date LIKE '{date}%'");
                conn.Open();
                command.CommandText = "SELECT SUM(CASE WHEN (debit - credit) > 0 THEN (debit - credit) ELSE 0 END) AS comp_credit, SUM(CASE WHEN (credit - debit) > 0 THEN (credit - debit) ELSE 0 END) AS comp_debit FROM " +
                                        $"( SELECT COALESCE(SUM(credit), 0) AS credit, COALESCE(SUM(debit), 0) AS debit FROM transactions {cond} GROUP BY customer_id )";
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

        public static int GetPayappClosuresNextId()
            => GetTableNextId("payapp_closures");

        public static float[] GetPayappClosure(int id)
            => SelectMultiRows($"SELECT balance FROM payapp_closures_details WHERE closure_id = {id} ORDER BY payapp_id", () => reader.GetFloat(0));

        public static float GetSumPrevPayappClosure(string date)
            => SelectRow($"SELECT COALESCE(SUM(balance), 0) FROM payapp_closures_details WHERE closure_id = (SELECT id FROM payapp_closures WHERE date < '{date}' ORDER BY date DESC LIMIT 1)", () => reader.GetFloat(0));
        
        public static SumDate[] GetPayappClosureDates()
            => SelectMultiRows("SELECT id, date, SUM(balance) FROM payapp_closures as c JOIN payapp_closures_details as d ON c.id = d.closure_id GROUP BY id ORDER BY date DESC", GetSumDate);

        public static float GetSumPayappClosure(int id)
            => SelectRow($"SELECT COALESCE(SUM(balance), 0) FROM payapp_closures_details WHERE closure_id = {id}", () => reader.GetFloat(0));
        
        public static int FindDayInPayappClosure(string date)
            => SelectRow($"SELECT id FROM payapp_closures WHERE date LIKE '{date}%' ORDER BY date DESC LIMIT 1", () => reader.GetInt32(0));

        public static bool IsNotUsed(int id)
            => SelectRow($"SELECT 1 FROM payapp_closures WHERE id = {id} AND closured = 0 LIMIT 1", () => reader.GetInt32(0)) == 1;


        public static DailyClosureData? GetDailyClosure(string date)
            => SelectRow($"SELECT date, total_wallets, total_cash, total_electronic, credit, debit, closure_id FROM daily_closures WHERE date = '{date}'", GetDailyClosureData);

        public static DailyClosureData[] GetDailyClosure()
            => SelectMultiRows($"SELECT date, total_wallets, total_cash, total_electronic, credit, debit, closure_id FROM daily_closures", GetDailyClosureData);

        public static float GetSumPrevDailyClosure(string date)
            => SelectRow($"SELECT total_wallets + total_cash + total_electronic + credit - debit FROM daily_closures WHERE date = (SELECT MAX(date) FROM daily_closures WHERE date < '{date}')", () => reader.GetFloat(0));

        public static SumDate[] GetDailyClosureDates()
            => SelectMultiRows("SELECT date, total_wallets + total_cash + total_electronic + credit - debit FROM daily_closures ORDER BY date DESC", GetSumDate2);

        public static float GetSumDailyClosure(string date)
            => SelectRow($"SELECT total_wallets + total_cash + total_electronic + credit - debit FROM daily_closures WHERE date = '{date}'", () => reader.GetFloat(0));


        public static (string Date, float[] Balances)[] GetPayappClosureDetails()
        {
            int payappCount = GetPayappNextId() - 1;
            if (payappCount < 1) return [];
            string sql = "SELECT c.date ";
            for (int i = 1; i <= payappCount; i++)
                sql += $",COALESCE( MAX( CASE WHEN cd.payapp_id = {i} THEN cd.balance END ), 0)";
            sql += " FROM payapp_closures c JOIN payapp_closures_details cd ON c.id = cd.closure_id WHERE cd.payapp_id >= 1 GROUP BY c.date";

            return SelectMultiRows(sql, () =>
            {
                string date = reader.GetString(0);
                float[] balances = new float[payappCount];
                for (int i = 0; i < payappCount; i++)
                    balances[i] = reader.GetFloat(i + 1);
                return (date, balances);
            });
        }

        public static string[] GetWalletTypes()
            => SelectMultiRows($"SELECT name FROM wallet_type", () => reader.GetString(0));


        private static T? SelectRow<T>(string sql, Func<T> method)
        {
            if (!success) return default;
            try
            {
                conn.Open();
                command.CommandText = sql;
                reader = command.ExecuteReader();
                if (reader.Read())
                    return method();
                return default;
            }
            catch (Exception ex)
            {
                LogError(ex, true);
                return default;
            }
            finally
            {
                reader.Close();
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

        public static bool AddPayappClosure(string date)
            => ExecuteNonQuery($"INSERT INTO payapp_closures (date, closured) VALUES ('{date}', 0)") >= 0;

        public static bool SetPayappClosured(int id)
            => ExecuteNonQuery($"UPDATE payapp_closures SET closured = 1 WHERE id = {id}") >= 0;

        public static bool SetPayappClosureDetails(int closureId, int payappId, float balance)
            => ExecuteNonQuery($"INSERT INTO payapp_closures_details (closure_id, payapp_id, balance) VALUES ({closureId}, {payappId}, {balance}) ON CONFLICT(closure_id, payapp_id) DO UPDATE SET balance = excluded.balance") >= 0;

        public static bool SetDailyClosure(DailyClosureData data)
            => ExecuteNonQuery($"INSERT INTO daily_closures (date, total_wallets, total_cash, total_electronic, credit, debit, closure_id) VALUES ('{data.Date}', {data.TotalWallets}, {data.TotalCash}, {data.TotalElectronic}, {data.Credit}, {data.Debit}, {data.PayappClosureId}) ON CONFLICT(date) DO UPDATE SET total_wallets = excluded.total_wallets, total_cash = excluded.total_cash, total_electronic = excluded.total_electronic, credit = excluded.credit, debit = excluded.debit") >= 0;

        public static bool DeleteLastDailyClosure()
            => ExecuteNonQuery($"DELETE FROM daily_closures WHERE date = ( SELECT MAX(date) FROM daily_closures )") >= 0;


        public static bool AddWalletType(string name)
            => ExecuteNonQuery($"INSERT INTO wallet_type (name) VALUES ('{name}')") >= 0;

        public static bool AddWallet(WalletRowData data)
            => ExecuteNonQuery($"INSERT INTO wallets VALUES ('{data.Phone}', {data})") >= 0;

        public static bool EditWallet(WalletRowData data)
            => ExecuteNonQuery($"UPDATE wallets SET ({WalletsTableColumnsNames}) = ({data}) WHERE phone = '{data.Phone}'") >= 0;

        public static bool ResetWallet(string phone)
            => ExecuteNonQuery($"DELETE FROM records WHERE phone = '{phone}'") >= 0;

        public static bool DeleteWallet(string phone)
            => ExecuteNonQuery($"DELETE FROM wallets WHERE phone = '{phone}'") >= 0;

        public static bool AddRecord(RecordRowData data)
            => ExecuteNonQuery($"INSERT INTO records VALUES ({data})") >= 0;

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
