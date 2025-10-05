using ClosedXML.Excel;

namespace ElectronicServices
{
    public partial class Form1 : Form
    {
        #region Form
        private int rowPadding = 7;
        private readonly int SizeX = 950, SizeY = 700;
        private int NewSizeX = 950, NewSizeY = 700;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (addCustomersPanel.Visible)
                Customers_KeyUp(e);
            else if (addTransactionsPanel.Visible)
                Transactions_KeyUp(e);
            else if (addWalletsPanel.Visible)
                Wallets_KeyUp(e);
            else if (addRecordsPanel.Visible)
                /*Records_KeyUp(e)*/
                ;
            else if (addExpensesPanel.Visible)
                Expenses_KeyUp(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new() { Interval = 10 };

            timer.Tick += (s, e1) =>
            {
                timer.Stop();

                if (Control.MouseButtons == MouseButtons.None)
                    this.Opacity = 1.0;
            };


            MouseEventHandler meh = (s, e1) =>
            {
                if (WindowState != FormWindowState.Maximized && e1.Button == MouseButtons.Left)
                {
                    this.Opacity = 0.9;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                    timer.Start();
                }
            };

            headerPanel.MouseDown += meh; ;
            formIcon.MouseDown += meh;
            formTitle.MouseDown += meh;
            CompanyPhone.MouseDown += meh;


            CustomerRow cust = new();
            cust.Location = new Point(cust.Location.X + rowPadding, 5);
            customersPanel.Controls.Add(cust);
            customerCode.Tag = DatabaseHelper.GetCustomerNextId();
            customerCode.Text = customerCode.Tag.ToString();
            customerName.Text = string.Empty;

            TransactionRow trans = new();
            trans.Location = new Point(trans.Location.X + rowPadding, 5);
            transactionsPanel.Controls.Add(trans);
            addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            customersComboBox.DisplayMember = "Value";
            customersComboBox.ValueMember = "Key";
            UpdateCustomersComboBox();
            DateLabel_DoubleClick(this, EventArgs.Empty);
            UpdatePayappComboBox();

            WalletRow wallet = new();
            wallet.Location = new Point(wallet.Location.X + rowPadding, 5);
            walletsPanel.Controls.Add(wallet);
            UpdateWalletTypeComboBox();

            RecordRow record = new();
            record.Location = new Point(record.Location.X + rowPadding, 5);
            recordsPanel.Controls.Add(record);

            ExpenseRow expenseR = new();
            expenseR.Location = new Point(expenseR.Location.X + rowPadding, 5);
            expensesPanel.Controls.Add(expenseR);

            DateTime now = DateTime.Now;
            DateTime firstDay = new(now.Year, now.Month, 1);
            excelDate.Value = now;
            dateFrom.Value = firstDay;
            dateTo.Value = firstDay.AddMonths(1).AddDays(-1);

            UpdateCreditAndDept();
            expense.Text = DatabaseHelper.GetExpensesAmount(now.ToStandardString()).ToString();
            walletData = new WalletRowData { Phone = "", Type = 0 };
            Timer1_Tick(null, null);
            timer1.Start();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        FormSize? fs;
        private void MaximizeBtn_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                maximizeBtn.Image = Properties.Resources.Fullscreen;
                fs = new FormSize(NewSizeX, NewSizeY, SizeX, SizeY);
                fs.SetControls(Controls);
                fs = null;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                maximizeBtn.Image = Properties.Resources.FullscreenExit;
                NewSizeX = Size.Width; NewSizeY = Size.Height;
                fs = new FormSize(SizeX, SizeY, NewSizeX, NewSizeY);
                fs.SetControls(Controls);
            }

            if (WindowState != FormWindowState.Minimized)
            {
                int minSize = Math.Min(minimizeBtn.Width, minimizeBtn.Height);
                Size newSize = new(minSize, minSize);
                minimizeBtn.ImageSize = newSize;
                maximizeBtn.ImageSize = newSize;
                exitBtn.ImageSize = newSize;
            }
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            System.Globalization.CultureInfo ar = new("ar-EG");
            string date = now.ToStandardString();
            string month = now.ToString("MMMM", ar);
            string day = now.ToString("dddd", ar);
            string time = now.ToString("hh:mm:ss tt", ar);
            dateNow.Text = $"{date}\n{month}\n{day}\n{time}";
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
                e.Handled = true;
        }
        #endregion

        #region Footer Panel
        private void MainBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Timer1_Tick(null, null);
            timer1.Start();
            expense.Text = DatabaseHelper.GetExpensesAmount(DateTime.Now.ToStandardString()).ToString();
            UpdateCreditAndDept();
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            walletsPanel.Visible = false;
            addWalletsPanel.Visible = false;
            recordsPanel.Visible = false;
            addRecordsPanel.Visible = false;
            addExpensesPanel.Visible = false;
            expensesPanel.Visible = false;
        }

        private void CustomersBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            customersPanel.Visible = true;
            addCustomersPanel.Visible = true;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            walletsPanel.Visible = false;
            addWalletsPanel.Visible = false;
            recordsPanel.Visible = false;
            addRecordsPanel.Visible = false;
            addExpensesPanel.Visible = false;
            expensesPanel.Visible = false;
        }

        private void TransactionsBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            transactionsPanel.Visible = true;
            addTransactionsPanel.Visible = true;
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            walletsPanel.Visible = false;
            addWalletsPanel.Visible = false;
            recordsPanel.Visible = false;
            addRecordsPanel.Visible = false;
            addExpensesPanel.Visible = false;
            expensesPanel.Visible = false;
        }

        private void WalletsBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            walletsPanel.Visible = true;
            addWalletsPanel.Visible = true;
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            recordsPanel.Visible = false;
            addRecordsPanel.Visible = false;
            addExpensesPanel.Visible = false;
            expensesPanel.Visible = false;
        }

        private void RecordsBtn_Click(object sender, EventArgs e)
        {
            if (walletData.Phone == "")
                balance2.Text = DatabaseHelper.GetTotalWalletsBalance(walletData.Type).ToString();

            timer1.Stop();
            recordsPanel.Visible = true;
            addRecordsPanel.Visible = true;
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            walletsPanel.Visible = false;
            addWalletsPanel.Visible = false;
            addExpensesPanel.Visible = false;
            expensesPanel.Visible = false;
        }

        private void ExpensesBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            addExpensesPanel.Visible = true;
            expensesPanel.Visible = true;
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            walletsPanel.Visible = false;
            addWalletsPanel.Visible = false;
            recordsPanel.Visible = false;
            addRecordsPanel.Visible = false;
        }
        #endregion

        #region Daily Inventory
        private void ElecPayBtn_Click(object sender, EventArgs e)
        {
            ElecListViewDialog elvd = new(null, true, -1);
            elvd.ShowDialog();
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            DailyListViewDialog dlvd = new(null, true, false);
            dlvd.ShowDialog();
        }
        #endregion

        #region Credit & Dept
        private void UpdateCreditAndDept()
        {
            float[] company = DatabaseHelper.GetCreditAndDept();
            creditAmount.Text = company[0].ToString();
            debitAmount.Text = company[1].ToString();
        }
        #endregion

        #region Customers & Transactions & Payapps
        private void AddCustExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("مكتبات الايكسل غير موجودة");
                return;
            }

            if (openExcelFileDialog.ShowDialog() != DialogResult.OK) return;

            ReadCustomerExcelFile();
            MessageBox.Show("تمت إضافة العملاء من ملف الايكسل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReadCustomerExcelFile()
        {
            using var workbook = new XLWorkbook(openExcelFileDialog.FileName);
            var worksheet = workbook.Worksheet(1);

            string name;
            foreach (var row in worksheet.RowsUsed())
            {
                name = row.Cell(1).GetValue<string>();
                row.Cell(2).TryGetValue(out float credit);
                row.Cell(3).TryGetValue(out float debit);
                customerName.Text = name;
                custCreditAmount.Value = (decimal)credit;
                custDebitAmount.Value = (decimal)debit;
                AddCustomerBtn_Click(null, null);
            }
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            string custName = customerName.Text.Trim().Replace("'", "");

            if (custName == "") return;

            int res = DatabaseHelper.SearchWithExactCustomerName(custName);
            if (res < 0)
            {
                MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (res >= 1)
            {
                MessageBox.Show($"لقد تمت إضافة هذا العميل من قبل\n{custName}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddCustomer(custName))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة العميل\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = customersPanel.Controls.Count, bottom;
            CustomerRow cust = new(new CustomerRowData
            {
                Id = (int)customerCode.Tag,
                Name = custName,
                Pay = (float)custCreditAmount.Value,
                Take = (float)custDebitAmount.Value,
            });
            if (count > 0)
                bottom = customersPanel.Controls[count - 1].Bottom;
            else
                bottom = 2;

            cust.Location = new Point(cust.Location.X + rowPadding, 0);
            fs?.SetControl(cust);
            fs?.SetControls(cust.Controls);
            cust.Location = new Point(cust.Location.X, bottom + (fs?.GetNewY(3) ?? 3));
            customersPanel.Controls.Add(cust);
            UpdateCustomersComboBox();

            if (custCreditAmount.Value > 0 || custDebitAmount.Value > 0)
            {
                TransactionRowData data = new()
                {
                    CustomerId = (int)customerCode.Tag,
                    Date = DateTime.Now.ToCompleteStandardString(),
                    Pay = (float)custCreditAmount.Value,
                    Take = (float)custDebitAmount.Value,
                    PayWith = -1,
                    TakeWith = -1,
                    Note = "",
                };

                if (!DatabaseHelper.AddTransaction(data))
                    MessageBox.Show("حدث خطأ اثناء إضافة القيم الابتدائية للعميل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            }

            custCreditAmount.Value = 0;
            custDebitAmount.Value = 0;
            customerCode.Tag = DatabaseHelper.GetCustomerNextId();
            customerCode.Text = customerCode.Tag.ToString();
            customerName.Text = string.Empty;
        }

        private void CustomerSearchBtn_Click(object sender, EventArgs e)
        {
            CustomerRowData[] customers = DatabaseHelper.GetCustomers(customerName.Text.Trim() == "" ? "" : customerName.Text);

            customersPanel.Controls.Clear();
            CustomerRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            customersPanel.Controls.Add(row);

            for (int i = 0; i < customers.Length; i++)
            {
                row = new(customers[i]);
                row.Location = new Point(row.Location.X + rowPadding, (row.Size.Height + 3) * (i + 1) + 5);
                fs?.SetControl(row);
                fs?.SetControls(row.Controls);
                customersPanel.Controls.Add(row);
            }
        }

        private void UpdateCustomersComboBox()
        {
            customersComboBox.Items.Clear();
            customersComboBox.Items.Add(new KeyValuePair<int, string>(0, "اختر من القائمة"));
            var customers = DatabaseHelper.GetCustomersNames();
            foreach (var customer in customers)
                customersComboBox.Items.Add(customer);
            customersComboBox.SelectedIndex = 0;
        }

        private void CustomerLabel_DoubleClick(object sender, EventArgs e)
        {
            customersComboBox.SelectedIndex = 0;
        }

        private void DateLabel_DoubleClick(object sender, EventArgs e)
        {
            isInternal = true; isAutoDate = true;
            transDate.Value = DateTime.Now;
            isInternal = false;
        }

        bool isAutoDate = true, isInternal = false;
        private void TransDate_ValueChanged(object sender, EventArgs e)
        {
            if (isInternal) return;
            isAutoDate = false;
            if (!isAutoDate && DateTime.Now.ToStandardString() == transDate.Value.ToStandardString()) isAutoDate = true;
            if (!isAutoDate)
            {
                isInternal = true;
                transDate.Value = transDate.Value.Date;
                isInternal = false;
            }
        }

        private void AddTransactionBtn_Click(object sender, EventArgs e)
        {
            if (isAutoDate)
                DateLabel_DoubleClick(this, EventArgs.Empty);

            if (customersComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("الرجاء اختيار عميل من القائمة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (payAmount.Value == 0 && takeAmount.Value == 0)
            {
                MessageBox.Show("الرجاء إدخال قيمة الدفع أو السحب", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float pay = (float)payAmount.Value, take = (float)takeAmount.Value;
            TransactionRowData data = new()
            {
                Id = (int)addTransactionsPanel.Tag,
                CustomerId = ((KeyValuePair<int, string>)customersComboBox.Items[customersComboBox.SelectedIndex]).Key,
                Name = customersComboBox.Text,
                Date = transDate.Value.ToCompleteStandardString(),
                Pay = pay,
                Take = take,
                PayWith = payWith.Enabled ? payWith.SelectedIndex : -1,
                TakeWith = takeWith.Enabled ? takeWith.SelectedIndex : -1,
                Note = transNote.Text.Trim(),
            };

            if (!DatabaseHelper.AddTransaction(data))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة المعاملة.\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = transactionsPanel.Controls.Count, bottom;
            if (count > 0)
                bottom = transactionsPanel.Controls[count - 1].Bottom;
            else
                bottom = 2;

            TransactionRow row = new(data);
            row.Location = new Point(row.Location.X + rowPadding, 0);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            row.Location = new Point(row.Location.X, bottom + (fs?.GetNewY(3) ?? 3));
            transactionsPanel.Controls.Add(row);

            addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            payAmount.Value = 0; takeAmount.Value = 0;

            UpdateCustomerRow(data.CustomerId, pay, take);
        }

        public void UpdateCustomerRow(int id, float pay, float take)
        {
            for (int i = 1; i < customersPanel.Controls.Count; i++)
            {
                if (customersPanel.Controls[i] is CustomerRow cr && cr.Id == id)
                {
                    cr.SetPayTakePlus(pay, take);
                    return;
                }
            }
        }

        public void DeleteTransactions(int customerId)
        {
            for (int i = 1; i < transactionsPanel.Controls.Count; i++)
            {
                if (transactionsPanel.Controls[i] is TransactionRow tr && tr.CustomerId == customerId)
                    tr.Enabled = false;
            }
        }

        private void TransSearchBtn_Click(object sender, EventArgs e)
        {
            int custId = ((KeyValuePair<int, string>)customersComboBox.Items[customersComboBox.SelectedIndex]).Key;
            TransactionRowData[] transactions = DatabaseHelper.GetTransactions(custId);

            AddTransactionsRows(transactions);
        }

        private void AddTransactionsRows(TransactionRowData[] transactions)
        {
            transactionsPanel.Controls.Clear();
            TransactionRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            transactionsPanel.Controls.Add(row);

            for (int i = 0; i < transactions.Length; i++)
            {
                row = new(transactions[i]);
                row.Location = new Point(row.Location.X + rowPadding, (row.Size.Height + 3) * (i + 1) + 5);
                fs?.SetControl(row);
                fs?.SetControls(row.Controls);
                transactionsPanel.Controls.Add(row);
            }
        }

        public void CustomerBtnClickInTransactionRow(string custName)
        {
            CustomersBtn_Click(null, null);
            customerName.Text = custName;
            CustomerSearchBtn_Click(null, null);
        }

        public void CustomerTransactionsBtnInCustomerRow(int custId)
        {
            TransactionsBtn_Click(null, null);
            customersComboBox.SelectedItem = customersComboBox.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(c => c.Key == custId);
            TransSearchBtn_Click(null, null);
        }

        private void Customers_KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FieldData[] data = DatabaseHelper.CustomerFieldSearch();
                if (data is null) return;
                ListViewDialog lvd = new("اسم العميل", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                customerName.Text = data[lvd.SelectedIndex].Text;
            }
        }

        private void Transactions_KeyUp(KeyEventArgs e)
        {
            int key = (int)e.KeyCode - 111;
            FieldData[] data;
            ListViewDialog lvd;
            if (key == 1)
            {
                data = DatabaseHelper.TransFieldSearch();
                if (data is null) return;
                lvd = new("اسم العميل", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                customersComboBox.SelectedItem = customersComboBox.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(c => c.Value == data[lvd.SelectedIndex].Text);
            }
            else if (key >= 6 && key <= 8)
            {
                int custId = ((KeyValuePair<int, string>)customersComboBox.Items[customersComboBox.SelectedIndex]).Key;
                data = DatabaseHelper.TransFieldSearch(custId, key);
                if (data is null) return;
                lvd = new("تاريخ المعاملة", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                TransactionRowData[] transactions = DatabaseHelper.GetTransactions(custId, data[lvd.SelectedIndex].Text, key);
                AddTransactionsRows(transactions);
            }
        }

        private void PayAmount_ValueChanged(object sender, EventArgs e)
        {
            payWith.Enabled = payAmount.Value > 0;
        }

        private void TakeAmount_ValueChanged(object sender, EventArgs e)
        {
            takeWith.Enabled = takeAmount.Value > 0;
        }

        private void UpdatePayappComboBox()
        {
            payWith.Items.Clear();
            takeWith.Items.Clear();
            string[] payApps = DatabaseHelper.GetPayappsNames();
            foreach (string payApp in payApps)
            {
                payWith.Items.Add(payApp);
                takeWith.Items.Add(payApp);
            }
            payWith.SelectedIndex = 0;
            takeWith.SelectedIndex = 0;
        }

        private void AddPayappBtn_Click(object sender, EventArgs e)
        {
            string payappName = payApp.Text.Trim();
            if (payappName == "") return;

            int res = DatabaseHelper.SearchWithExactPayappName(payappName);
            if (res < 0)
            {
                MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (res >= 1)
            {
                MessageBox.Show("لقد تمت إضافة تطبيق الدفع هذا من قبل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddPayapp(payappName))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة تطبيق الدفع\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            payApp.Text = "";
            UpdatePayappComboBox();
        }
        #endregion

        #region Wallets & Operations
        private void AddWalletTypeBtn_Click(object sender, EventArgs e)
        {
            string type = walletType.Text.Trim();
            if (type == "") return;

            int res = DatabaseHelper.SearchWithExactWalletType(type);
            if (res < 0)
            {
                MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (res >= 1)
            {
                MessageBox.Show("لقد تمت إضافة نوع المحفظة هذا من قبل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddWalletType(type))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة نوع المحفظة\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            walletType.Text = "";
            UpdateWalletTypeComboBox();
        }

        private void UpdateWalletTypeComboBox()
        {
            walletTypeComboBox.Items.Clear();
            walletTypeComboBox.Items.Add("اختر من القائمة");
            string[] types = DatabaseHelper.GetWalletTypes();
            foreach (string type in types)
                walletTypeComboBox.Items.Add(type);
            walletTypeComboBox.SelectedIndex = 0;
        }

        private void WalletSearchBtn_Click(object sender, EventArgs e)
        {
            WalletRowData[] wallets = DatabaseHelper.GetWallets(phoneNumber.Text);
            AddWalletsInPanel(wallets);
        }

        private void AddWalletsInPanel(WalletRowData[] wallets)
        {
            walletsPanel.Controls.Clear();
            WalletRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            walletsPanel.Controls.Add(row);

            for (int i = 0; i < wallets.Length; i++)
            {
                row = new(wallets[i]);
                row.Location = new Point(row.Location.X + rowPadding, (row.Size.Height + 3) * (i + 1) + 5);
                fs?.SetControl(row);
                fs?.SetControls(row.Controls);
                walletsPanel.Controls.Add(row);
            }
        }

        private void WalletSaveBtn_Click(object sender, EventArgs e)
        {
            if (phoneNumber.Text == "") return;

            if (walletTypeComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("الرجاء اختيار نوع المحفظة من القائمة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isEqualMax.Checked)
            {
                withdrawalRemaining.Value = maxWithdrawal.Value;
                depositRemaining.Value = maxDeposit.Value;
            }

            WalletRowData data = new()
            {
                Phone = phoneNumber.Text,
                MaximumWithdrawal = (float)maxWithdrawal.Value,
                MaximumDeposit = (float)maxDeposit.Value,
                WithdrawalRemaining = (float)withdrawalRemaining.Value,
                DepositRemaining = (float)depositRemaining.Value,
                Balance = (float)balance.Value,
                Type = walletTypeComboBox.SelectedIndex,
                Comment = walletComment.Text.Trim(),
            };

            bool res = DatabaseHelper.IsWalletExist(data.Phone);

            if (res)
            {
                if (MessageBox.Show($"هل تريد تحديث بيانات هذه المحفظة ؟\n{data.Phone}", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                if (!DatabaseHelper.EditWallet(data))
                {
                    MessageBox.Show("حدث خطأ أثناء تحديث بيانات المحفظة\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (!DatabaseHelper.AddWallet(data))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة المحفظة\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (walletData.Phone == data.Phone)
            {
                walletData = data;
                maxWithd.Text = data.MaximumWithdrawal.ToString();
                maxDepo.Text = data.MaximumDeposit.ToString();
                withdRema.Text = data.WithdrawalRemaining.ToString();
                depoRema.Text = data.DepositRemaining.ToString();
                balance2.Text = data.Balance.ToString();
                walletType2.Text = GetWalletType(data.Type);
            }

            for (int i = 1; i < walletsPanel.Controls.Count; i++)
            {
                if (walletsPanel.Controls[i] is WalletRow wr && wr.Phone == data.Phone)
                {
                    wr.SetWalletRowData(data);
                    return;
                }
            }

            int count = walletsPanel.Controls.Count, bottom;
            if (count > 0)
                bottom = walletsPanel.Controls[count - 1].Bottom;
            else
                bottom = 2;

            WalletRow row = new(data);
            row.Location = new Point(row.Location.X + rowPadding, 0);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            row.Location = new Point(row.Location.X, bottom + (fs?.GetNewY(3) ?? 3));
            walletsPanel.Controls.Add(row);
        }

        private void WalletEmptyBtn_Click(object sender, EventArgs e)
        {
            phoneNumber.Text = "";
            maxWithdrawal.Value = 0;
            maxDeposit.Value = 0;
            isEqualMax.Checked = true;
            withdrawalRemaining.Value = 0;
            depositRemaining.Value = 0;
            balance.Value = 0;
            walletTypeComboBox.SelectedIndex = 0;
            walletComment.Text = "";
        }

        private void AddWalletExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("مكتبات الايكسل غير موجودة");
                return;
            }

            if (walletTypeComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("الرجاء اختيار نوع المحفظة من القائمة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (openExcelFileDialog.ShowDialog() != DialogResult.OK) return;

            isEqualMax.Checked = false;
            walletComment.Text = "تمت الإضافة من ملف الايكسل";

            ReadWalletExcelFile();
            MessageBox.Show("تمت إضافة المحافظ من ملف الايكسل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReadWalletExcelFile()
        {
            using var workbook = new XLWorkbook(openExcelFileDialog.FileName);
            var worksheet = workbook.Worksheet(1);

            string phone;
            foreach (var row in worksheet.RowsUsed())
            {
                string normalized = row.Cell(1).GetValue<string>().Select(c =>
                {
                    if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.DecimalDigitNumber)
                    {
                        return (char)('0' + char.GetNumericValue(c));
                    }
                    return c;
                }).Aggregate("", (current, next) => current + next);

                phone = new string([.. normalized.Where(char.IsDigit)]);
                row.Cell(2).TryGetValue(out float withd);
                row.Cell(3).TryGetValue(out float depo);
                row.Cell(4).TryGetValue(out float bala);
                phoneNumber.Text = phone;
                withdrawalRemaining.Value = (decimal)withd;
                depositRemaining.Value = (decimal)depo;
                balance.Value = (decimal)bala;
                WalletSaveBtn_Click(null, null);
            }
        }

        private void Wallets_KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FieldData[] data = DatabaseHelper.WalletFieldSearch();
                if (data is null) return;
                ListViewDialog lvd = new("رقم المحفظة", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                phoneNumber.Text = data[lvd.SelectedIndex].Text;
            }
            else if (e.KeyCode == Keys.F2)
            {
                FieldData[] data = DatabaseHelper.WalletTypeFieldSearch();
                if (data is null) return;
                ListViewDialog lvd = new("نوع المحفظة", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                phoneNumber.Text = "";
                walletTypeComboBox.SelectedItem = data[lvd.SelectedIndex].Text;
            }
            else if (e.KeyCode == Keys.F3)
            {
                WalletRowData[] data = DatabaseHelper.GetWallets(walletTypeComboBox.SelectedIndex);
                if (data is null) return;

                phoneNumber.Text = "";
                AddWalletsInPanel(data);
            }
            else if (e.KeyCode == Keys.F4)
            {
                int type = walletTypeComboBox.SelectedIndex;
                float totalBalance = DatabaseHelper.GetTotalWalletsBalance(type);
                RecordRowData[] records = DatabaseHelper.GetRecords(type);
                walletData = new WalletRowData { Phone = "", Type = type };
                phoneNumber2.Text = "";
                maxWithd.Text = "";
                maxDepo.Text = "";
                withdRema.Text = "";
                depoRema.Text = "";
                balance2.Text = totalBalance.ToString();
                walletType2.Text = type == 0 ? "" : GetWalletType(type);
                withdrawal.Value = 0;
                deposit.Value = 0;
                operComment.Text = "";
                operSaveBtn.Enabled = false;
                AddRecordsInPanel(records);
                RecordsBtn_Click(null, null);
            }
            else if (e.KeyCode == Keys.F5)
            {
                ResetWalletsRemaning();
            }
        }

        WalletRowData walletData;
        public void ChooseWalletBtn(WalletRowData data)
        {
            walletData = data;
            phoneNumber2.Text = data.Phone;
            maxWithd.Text = data.MaximumWithdrawal.ToString();
            maxDepo.Text = data.MaximumDeposit.ToString();
            withdRema.Text = data.WithdrawalRemaining.ToString();
            depoRema.Text = data.DepositRemaining.ToString();
            balance2.Text = data.Balance.ToString();
            walletType2.Text = GetWalletType(data.Type);
            withdrawal.Value = 0;
            deposit.Value = 0;
            operComment.Text = "";
            operSaveBtn.Enabled = true;
            RecordRowData[] records = DatabaseHelper.GetRecords(data.Phone);
            AddRecordsInPanel(records);
            RecordsBtn_Click(null, null);
        }

        public void SetWalletData(WalletRowData data)
        {
            phoneNumber.Text = data.Phone;
            maxWithdrawal.Value = (decimal)data.MaximumWithdrawal;
            maxDeposit.Value = (decimal)data.MaximumDeposit;
            isEqualMax.Checked = false;
            withdrawalRemaining.Value = (decimal)data.WithdrawalRemaining;
            depositRemaining.Value = (decimal)data.DepositRemaining;
            balance.Value = (decimal)data.Balance;
            walletTypeComboBox.SelectedIndex = data.Type;
            walletComment.Text = data.Comment;
        }

        public void ResetWallet(string phone)
        {
            if (walletData.Phone != phone) return;
            recordsPanel.Controls.Clear();
            RecordRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            recordsPanel.Controls.Add(row);
        }

        public void ResetWalletsRemaining()
        {
            DialogResult res = MessageBox.Show("هل أنت متأكد من إعادة تعيين المتبقي للسحب والإيداع لجميع المحافظ ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No) return;

            if (!DatabaseHelper.ResetWalletsRemaining())
            {
                MessageBox.Show("حدث خطأ أثناء إعادة تعيين المتبقي للسحب والإيداع\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 1; i < walletsPanel.Controls.Count; i++)
                if (walletsPanel.Controls[i] is WalletRow wr)
                    wr.ResetRemaining();

            phoneNumber.Text = "";

            if (walletData.Phone != "")
            {
                walletData.WithdrawalRemaining = walletData.MaximumWithdrawal;
                walletData.DepositRemaining = walletData.DepositRemaining < 0 ? walletData.DepositRemaining + walletData.MaximumDeposit : walletData.MaximumDeposit;
                withdRema.Text = walletData.WithdrawalRemaining.ToString();
                depoRema.Text = walletData.DepositRemaining.ToString();
            }

            MessageBox.Show("تمت إعادة تعيين المتبقي للسحب والإيداع لجميع المحافظ بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CheckWallet(string phone)
        {
            if (walletData.Phone == phone)
            {
                walletData.Phone = "";
                phoneNumber2.Text = "";
                maxWithd.Text = "";
                maxDepo.Text = "";
                withdRema.Text = "";
                depoRema.Text = "";
                operSaveBtn.Enabled = false;
            }
        }

        public string GetWalletType(int type)
            => walletTypeComboBox.Items[type].ToString();

        private void PhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void IsEqualMax_CheckedChanged(object sender, EventArgs e)
        {
            withdrawalRemaining.Enabled = !isEqualMax.Checked;
            depositRemaining.Enabled = !isEqualMax.Checked;
        }

        private void AddRecordsInPanel(RecordRowData[] records)
        {
            recordsPanel.Controls.Clear();
            RecordRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            recordsPanel.Controls.Add(row);

            for (int i = 0; i < records.Length; i++)
            {
                row = new(records[i]);
                row.Location = new Point(row.Location.X + rowPadding, (row.Size.Height + 3) * (i + 1) + 5);
                fs?.SetControl(row);
                fs?.SetControls(row.Controls);
                recordsPanel.Controls.Add(row);
            }
        }

        private void OperSaveBtn_Click(object sender, EventArgs e)
        {
            if (withdrawal.Value == 0 && deposit.Value == 0)
            {
                MessageBox.Show("الرجاء إدخال قيمة السحب أو الإيداع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((float)withdrawal.Value > walletData.WithdrawalRemaining)
            {
                MessageBox.Show("قيمة السحب أكبر من المتبقي للسحب", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((float)deposit.Value > walletData.DepositRemaining && MessageBox.Show("قيمة الإيداع أكبر من المتبقي للإيداع\nهل انت متأكد من الاستمرار ؟", "خطأ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            
            if (walletData.Balance - (float)withdrawal.Value + (float)deposit.Value < 0)
            {
                MessageBox.Show("الرصيد لا يمكن أن يكون سالباً", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            walletData.WithdrawalRemaining -= (float)withdrawal.Value;
            walletData.DepositRemaining -= (float)deposit.Value;
            walletData.Balance = walletData.Balance - (float)withdrawal.Value + (float)deposit.Value;

            withdRema.Text = walletData.WithdrawalRemaining.ToString();
            depoRema.Text = walletData.DepositRemaining.ToString();
            balance2.Text = walletData.Balance.ToString();

            if (!DatabaseHelper.EditWallet(walletData))
            {
                MessageBox.Show("حدث خطأ أثناء تحديث بيانات المحفظة\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 1; i < walletsPanel.Controls.Count; i++)
            {
                if (walletsPanel.Controls[i] is WalletRow wr && wr.Phone == walletData.Phone)
                {
                    wr.SetWalletRowData(walletData);
                    break;
                }
            }

            RecordRowData data = new()
            {
                Phone = walletData.Phone,
                Date = DateTime.Now.ToCompleteStandardString(),
                WithdrawalRemaining = walletData.WithdrawalRemaining,
                DepositRemaining = walletData.DepositRemaining,
                Withdrawal = (float)withdrawal.Value,
                Deposit = (float)deposit.Value,
                Balance = walletData.Balance,
                Comment = walletComment.Text.Trim(),
            };

            withdrawal.Value = 0;
            deposit.Value = 0;

            if (!DatabaseHelper.AddRecord(data))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة العملية\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RecordRowData[] records = DatabaseHelper.GetRecords(data.Phone);
            AddRecordsInPanel(records);
        }

        private void WalletTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (walletTypeComboBox.SelectedIndex <= 0) return;
            WalletRowData[] data = DatabaseHelper.GetWallets(walletTypeComboBox.SelectedIndex);
            if (data is null) return;

            AddWalletsInPanel(data);
        }
        #endregion

        #region Expenses
        private void ChooseAttachmentBtn_Click(object sender, EventArgs e)
        {
            if (attachmentDialog.ShowDialog() == DialogResult.OK)
                attachmentPath.Text = Path.GetFullPath(attachmentDialog.FileName);
        }

        public string AttachmentPath()
        {
            if (attachmentDialog.ShowDialog() == DialogResult.OK)
                return Path.GetFullPath(attachmentDialog.FileName);
            return "";
        }

        private void ExpenseSearchBtn_Click(object sender, EventArgs e)
        {
            ExpenseRowData[] expenses = DatabaseHelper.GetExpenses(expenseTitle.Text.Trim() == "" ? "" : expenseTitle.Text);

            AddExpensesRows(expenses);
        }

        private void AddExpensesRows(ExpenseRowData[] expenses)
        {
            expensesPanel.Controls.Clear();
            ExpenseRow row = new();
            row.Location = new Point(row.Location.X + rowPadding, 5);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            expensesPanel.Controls.Add(row);

            for (int i = 0; i < expenses.Length; i++)
            {
                row = new(expenses[i]);
                row.Location = new Point(row.Location.X + rowPadding, (row.Size.Height + 3) * (i + 1) + 5);
                fs?.SetControl(row);
                fs?.SetControls(row.Controls);
                expensesPanel.Controls.Add(row);
            }
        }

        private void Expenses_KeyUp(KeyEventArgs e)
        {
            int key = (int)e.KeyCode - 111;
            FieldData[] data;
            ListViewDialog lvd;

            if (key == 1)
            {
                data = DatabaseHelper.ExpenseFieldSearch();
                if (data is null) return;
                lvd = new("المصروفات", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                expenseTitle.Text = data[lvd.SelectedIndex].Text;
            }
            else if (key >= 6 && key <= 8)
            {
                data = DatabaseHelper.ExpenseFieldSearch(key, false);
                if (data is null) return;
                lvd = new("تاريخ المصروفات", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                ExpenseRowData[] expenses = DatabaseHelper.GetExpensesWithDate(data[lvd.SelectedIndex].Text);
                AddExpensesRows(expenses);
            }
        }

        private const string attachmentPathReset = "... لم يتم اختيار مرفق";
        private void AddExpenseBtn_Click(object sender, EventArgs e)
        {
            string title = expenseTitle.Text.Trim();
            if (title == "") return;

            if (expenseAmount.Value == 0 && MessageBox.Show("قيمة المصروف صفر\nهل انت متأكد من الاستمرار ؟", "خطأ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string attachment = attachmentPath.Text == attachmentPathReset ? "" : attachmentPath.Text;
            ExpenseRowData data = new()
            {
                Title = title,
                Date = DateTime.Now.ToCompleteStandardString(),
                Amount = (float)expenseAmount.Value,
                Attachment = attachment,
                Comment = expenseComment.Text.Trim(),
            };

            if (!DatabaseHelper.AddExpense(data))
            {
                MessageBox.Show("حدث خطأ أثناء إضافة المصروفات.\nالرجاء المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            expenseTitle.Text = "";
            expenseAmount.Value = 0;
            attachmentPath.Text = attachmentPathReset;

            int count = expensesPanel.Controls.Count, bottom;
            if (count > 0)
                bottom = expensesPanel.Controls[count - 1].Bottom;
            else
                bottom = 2;

            ExpenseRow row = new(data);
            row.Location = new Point(row.Location.X + rowPadding, 0);
            fs?.SetControl(row);
            fs?.SetControls(row.Controls);
            row.Location = new Point(row.Location.X, bottom + (fs?.GetNewY(3) ?? 3));
            expensesPanel.Controls.Add(row);
        }

        private void StatisticsExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("مكتبات الايكسل غير موجودة");
                return;
            }

            string date = DateTime.Now.ToCompleteStandardString();
            saveExcelFileDialog.FileName = date.Replace(":", "");
            if (saveExcelFileDialog.ShowDialog() != DialogResult.OK) return;

            ExtractStatisticsExcel(saveExcelFileDialog.FileName);
        }

        private void ExtractStatisticsExcel(string path)
        {
            using XLWorkbook workbook = new();
            workbook.RightToLeft = true;

            IXLWorksheet day = workbook.Worksheets.Add("Daily Expense Statistics");
            day.Range("A1:D1").Merge().Value = "إحصائيات المصروفات اليومية";
            day.Row(1).Height = 30;
            day.Cell("A1").Style.Font.Bold = true;
            day.Cell("A1").Style.Font.FontSize = 16;
            day.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            day.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            day.Column(1).Width = 14; day.Cell(2, 1).Value = "التاريخ";
            day.Column(2).Width = 10; day.Cell(2, 2).Value = "اليوم";
            day.Column(3).Width = 15; day.Cell(2, 3).Value = "عدد المصروفات";
            day.Column(4).Width = 15; day.Cell(2, 4).Value = "المبلغ الإجمالي";

            Field3Data[] data = DatabaseHelper.GetExpenseStatistics(6);
            for (int i = 0; i < data.Length; i++)
            {
                day.Cell(i + 3, 1).Value = data[i].Text;
                day.Cell(i + 3, 2).Value = data[i].Text.GetArabic(day: true);
                day.Cell(i + 3, 3).Value = data[i].Count;
                day.Cell(i + 3, 4).Value = data[i].Sum;
            }

            IXLWorksheet month = workbook.Worksheets.Add("Monthly Expense Statistics");
            month.Range("A1:D1").Merge().Value = "إحصائيات المصروفات الشهرية";
            month.Row(1).Height = 30;
            month.Cell("A1").Style.Font.Bold = true;
            month.Cell("A1").Style.Font.FontSize = 16;
            month.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            month.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            month.Column(1).Width = 14; month.Cell(2, 1).Value = "التاريخ";
            month.Column(2).Width = 10; month.Cell(2, 2).Value = "الشهر";
            month.Column(3).Width = 15; month.Cell(2, 3).Value = "عدد المصروفات";
            month.Column(4).Width = 15; month.Cell(2, 4).Value = "المبلغ الإجمالي";

            data = DatabaseHelper.GetExpenseStatistics(7);
            for (int i = 0; i < data.Length; i++)
            {
                month.Cell(i + 3, 1).Value = data[i].Text;
                month.Cell(i + 3, 2).Value = data[i].Text.GetArabic(day: false);
                month.Cell(i + 3, 3).Value = data[i].Count;
                month.Cell(i + 3, 4).Value = data[i].Sum;
            }

            IXLWorksheet year = workbook.Worksheets.Add("Yearly Expense Statistics");
            year.Range("A1:D1").Merge().Value = "إحصائيات المصروفات السنوية";
            year.Row(1).Height = 30;
            year.Cell("A1").Style.Font.Bold = true;
            year.Cell("A1").Style.Font.FontSize = 16;
            year.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            year.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            year.Column(1).Width = 14; year.Cell(2, 1).Value = "التاريخ";
            year.Column(2).Width = 10; year.Cell(2, 2).Value = "السنة";
            year.Column(3).Width = 15; year.Cell(2, 3).Value = "عدد المصروفات";
            year.Column(4).Width = 15; year.Cell(2, 4).Value = "المبلغ الإجمالي";

            data = DatabaseHelper.GetExpenseStatistics(8);
            for (int i = 0; i < data.Length; i++)
            {
                year.Cell(i + 3, 1).Value = data[i].Text;
                year.Cell(i + 3, 2).Value = "";
                year.Cell(i + 3, 3).Value = data[i].Count;
                year.Cell(i + 3, 4).Value = data[i].Sum;
            }


            try
            {
                workbook.SaveAs(path);
                MessageBox.Show("تم استخراج البيانات بنجاح");
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الملف\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Extract Excel Files
        private void ExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("مكتبات الايكسل غير موجودة");
                return;
            }

            string date = excelDate.Value.ToStandardString();
            saveExcelFileDialog.FileName = date;
            if (saveExcelFileDialog.ShowDialog() != DialogResult.OK) return;

            ExtractExcel(date, saveExcelFileDialog.FileName);
        }

        private void ExtractExcel(string date, string path)
        {
            using XLWorkbook workbook = new();
            workbook.RightToLeft = true;
            IXLWorksheet sheet = workbook.Worksheets.Add("Daily Summary");

            string[] payapps = DatabaseHelper.GetPayappsNames(true);
            char c = (char)('A' + payapps.Length + 1);
            if (c > 'Z') c = 'Z';
            sheet.Range($"A1:{c}1").Merge().Value = "ملخص يوم     " + date;
            sheet.Row(1).Height = 30;
            sheet.Cell("A1").Style.Font.Bold = true;
            sheet.Cell("A1").Style.Font.FontSize = 18;
            sheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            sheet.Column(1).Width = 12.5;

            for (int i = 0; i < payapps.Length; i++)
            {
                sheet.Column(i + 2).Width = 12;
                sheet.Cell(2, i + 2).Value = payapps[i];
                sheet.Cell(2, i + 2).Style.Fill.BackgroundColor = XLColor.LightYellow1;
            }

            sheet.Column(payapps.Length + 2).Width = 12;
            sheet.Cell(2, payapps.Length + 2).Value = "المجموع";
            sheet.Cell(2, payapps.Length + 2).Style.Fill.BackgroundColor = XLColor.LightBlue;

            sheet.Cell(3, 1).Value = "دفع";
            sheet.Cell(4, 1).Value = "أخذ";
            sheet.Cell(5, 1).Value = "المجموع";
            sheet.Cell(5, 1).Style.Fill.BackgroundColor = XLColor.LightBlue;

            float sum = 0, sum2 = 0, val, val2;
            for (int i = 0; i < payapps.Length; i++)
            {
                val = DatabaseHelper.GetPayappDateField(i + 1, date, pay: true);
                sheet.Cell(3, i + 2).Value = val;
                sum += val;

                val2 = DatabaseHelper.GetPayappDateField(i + 1, date, pay: false);
                sheet.Cell(4, i + 2).Value = val2;
                sum2 += val2;

                sheet.Cell(5, i + 2).Value = val + val2;
            }
            sheet.Cell(3, payapps.Length + 2).Value = sum;
            sheet.Cell(4, payapps.Length + 2).Value = sum2;
            sheet.Cell(5, payapps.Length + 2).Value = sum + sum2;


            float cashCredit = DatabaseHelper.GetPayappDateField(0, date, pay: true), cashDebit = DatabaseHelper.GetPayappDateField(0, date, pay: false);
            sheet.Cell(8, 1).Value = "";

            sheet.Cell(8, 2).Value = "استلام نقدي";
            sheet.Cell(8, 3).Value = "تسليم نقدي";
            sheet.Cell(8, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(8, 3).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(9, 2).Value = cashCredit;
            sheet.Cell(9, 3).Value = cashDebit;

            sheet.Cell(8, 4).Value = "المجموع";
            sheet.Cell(8, 5).Value = "الفرق";
            sheet.Cell(8, 4).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(8, 5).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(9, 4).Value = cashCredit + cashDebit;
            sheet.Cell(9, 5).Value = cashCredit - cashDebit;


            float[] creditDebit = DatabaseHelper.GetCreditAndDept(date);

            sheet.Cell(12, 2).Value = "لنا";
            sheet.Cell(12, 3).Value = "علينا";
            sheet.Cell(12, 2).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(12, 3).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(13, 2).Value = creditDebit[0];
            sheet.Cell(13, 3).Value = creditDebit[1];

            sheet.Cell(12, 4).Value = "المجموع";
            sheet.Cell(12, 5).Value = "الفرق";
            sheet.Cell(12, 4).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(12, 5).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(13, 4).Value = creditDebit[0] + creditDebit[1];
            sheet.Cell(13, 5).Value = creditDebit[0] - creditDebit[1];


            float[] withDepo = DatabaseHelper.GetWalletsWithdDepo(date);
            sheet.Cell(16, 1).Value = "المحافظ";
            sheet.Cell(16, 2).Value = "سحب";
            sheet.Cell(16, 3).Value = "إيداع";
            sheet.Cell(16, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(17, 2).Value = withDepo[0];
            sheet.Cell(17, 3).Value = withDepo[1];

            sheet.Cell(16, 4).Value = "المجموع";
            sheet.Cell(16, 5).Value = "الفرق";
            sheet.Cell(16, 4).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(17, 4).Value = withDepo[0] + withDepo[1];
            sheet.Cell(17, 5).Value = withDepo[0] - withDepo[1];


            float[] expense = DatabaseHelper.ExpenseAmount(date);
            sheet.Cell(20, 1).Value = "المصاريف";
            sheet.Cell(20, 2).Value = "العدد";
            sheet.Cell(20, 3).Value = "الإجمالي";
            sheet.Cell(20, 2).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(20, 3).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(21, 2).Value = expense[0];
            sheet.Cell(21, 3).Value = expense[1];


            try
            {
                workbook.SaveAs(path);
                MessageBox.Show("تم استخراج البيانات بنجاح");
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الملف\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateChoose_CheckedChanged(object sender, EventArgs e)
        {
            dateFrom.Enabled = dateChoose.Checked;
            dateTo.Enabled = dateChoose.Checked;
        }

        private void ExtraExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("مكتبات الايكسل غير موجودة");
                return;
            }
            isRunning = true;
            extraExcelBtn.Image = Properties.Resources.head_bandage;
            Invalidate(); Update();

            if (dateChoose.Checked && dateFrom.Value > dateTo.Value)
            {
                MessageBox.Show("تاريخ البداية أكبر من تاريخ النهاية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                extraExcelBtn.Image = Properties.Resources.neutral_face;
                isRunning = false;
                return;
            }

            string from, to;
            if (dateChoose.Checked)
            {
                from = dateFrom.Value.ToStandardString();
                to = dateTo.Value.ToStandardString();
                saveExcelFileDialog.FileName = $"{from} - {to}";
            }
            else
            {
                from = "";
                to = "";
                saveExcelFileDialog.FileName = DateTime.Now.ToCompleteStandardString().Replace(":", "");
            }

            if (saveExcelFileDialog.ShowDialog() == DialogResult.OK)
                ExtractExtraExcel(saveExcelFileDialog.FileName, from, to);

            extraExcelBtn.Image = Properties.Resources.neutral_face;
            isRunning = false;
        }
        bool isRunning = false;

        private void ExtractExtraExcel(string path, string from, string to)
        {
            using XLWorkbook workbook = new();
            workbook.RightToLeft = true;

            IXLWorksheet custSheet = workbook.Worksheets.Add("Customers");

            custSheet.Range("A1:E1").Merge().Value = "العملاء";
            custSheet.Row(1).Height = 30;
            custSheet.Cell("A1").Style.Font.Bold = true;
            custSheet.Cell("A1").Style.Font.FontSize = 18;
            custSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            custSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            custSheet.Column(1).Width = 10; custSheet.Cell(2, 1).Value = "كود العميل";
            custSheet.Column(2).Width = 30; custSheet.Cell(2, 2).Value = "اسم العميل";
            custSheet.Column(3).Width = 15; custSheet.Cell(2, 3).Value = "دفع";
            custSheet.Column(4).Width = 15; custSheet.Cell(2, 4).Value = "أخذ";
            custSheet.Column(5).Width = 15; custSheet.Cell(2, 5).Value = "الرصيد";

            CustomerRowData[] customers = DatabaseHelper.GetCustomers();
            for (int i = 0; i < customers.Length; i++)
            {
                custSheet.Cell(i + 3, 1).Value = customers[i].Id;
                custSheet.Cell(i + 3, 2).Value = customers[i].Name;
                custSheet.Cell(i + 3, 3).Value = customers[i].Pay;
                custSheet.Cell(i + 3, 4).Value = customers[i].Take;
                custSheet.Cell(i + 3, 5).Value = customers[i].Balance;
            }


            IXLWorksheet transSheet = workbook.Worksheets.Add("Transactions");
            transSheet.Range("A1:I1").Merge().Value = "حركة العملاء";
            transSheet.Row(1).Height = 30;
            transSheet.Cell("A1").Style.Font.Bold = true;
            transSheet.Cell("A1").Style.Font.FontSize = 18;
            transSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            transSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            transSheet.Column(1).Width = 30; transSheet.Cell(2, 1).Value = "اسم العميل";
            transSheet.Column(2).Width = 14; transSheet.Cell(2, 2).Value = "التاريخ";
            transSheet.Column(3).Width = 13; transSheet.Cell(2, 3).Value = "الوقت";
            transSheet.Column(4).Width = 15; transSheet.Cell(2, 4).Value = "دفع";
            transSheet.Column(5).Width = 16; transSheet.Cell(2, 5).Value = "دفع بواسطة";
            transSheet.Column(6).Width = 15; transSheet.Cell(2, 6).Value = "أخذ";
            transSheet.Column(7).Width = 16; transSheet.Cell(2, 7).Value = "أخذ بواسطة";
            transSheet.Column(8).Width = 15; transSheet.Cell(2, 8).Value = "الرصيد";
            transSheet.Column(9).Width = 30; transSheet.Cell(2, 9).Value = "ملاحظة";

            string[] payApps = DatabaseHelper.GetPayappsNames();
            string[] datetime;
            TransactionRowData[] transactions = dateChoose.Checked ? DatabaseHelper.GetTransactions(from, to) : DatabaseHelper.GetTransactions(0);
            for (int i = 0; i < transactions.Length; i++)
            {
                transSheet.Cell(i + 3, 1).Value = transactions[i].Name;
                datetime = transactions[i].Date.Split(' ');
                transSheet.Cell(i + 3, 2).Value = datetime[0];
                transSheet.Cell(i + 3, 3).Value = datetime[1];
                transSheet.Cell(i + 3, 4).Value = transactions[i].Pay;
                transSheet.Cell(i + 3, 5).Value = transactions[i].PayWith < 0 ? "" : payApps[transactions[i].PayWith];
                transSheet.Cell(i + 3, 6).Value = transactions[i].Take;
                transSheet.Cell(i + 3, 7).Value = transactions[i].TakeWith < 0 ? "" : payApps[transactions[i].TakeWith];
                transSheet.Cell(i + 3, 8).Value = transactions[i].Balance;
                transSheet.Cell(i + 3, 9).Value = transactions[i].Note;
            }


            IXLWorksheet elecSheet = workbook.Worksheets.Add("Electronic Payments");
            char c = (char)('A' + payApps.Length + 1);
            if (c > 'Z') c = 'Z';
            elecSheet.Range($"A1:{c}1").Merge().Value = "المدفوعات الإلكترونية";
            elecSheet.Row(1).Height = 30;
            elecSheet.Cell("A1").Style.Font.Bold = true;
            elecSheet.Cell("A1").Style.Font.FontSize = 18;
            elecSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            elecSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            elecSheet.Column(1).Width = 14; elecSheet.Cell(2, 1).Value = "التاريخ";
            elecSheet.Column(2).Width = 13; elecSheet.Cell(2, 2).Value = "الوقت";
            for (int i = 1; i < payApps.Length; i++)
            {
                elecSheet.Column(i + 2).Width = 15;
                elecSheet.Cell(2, i + 2).Value = payApps[i];
                elecSheet.Cell(2, i + 2).Style.Fill.BackgroundColor = XLColor.LightYellow1;
            }
            elecSheet.Column(payApps.Length + 2).Width = 15;
            elecSheet.Cell(2, payApps.Length + 2).Value = "المجموع";
            elecSheet.Cell(2, payApps.Length + 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
            var details = DatabaseHelper.GetPayappClosureDetails(dateChoose.Checked, from, to);
            float total, val;
            for (int i = 0; i < details.Length; i++)
            {
                total = 0;
                datetime = details[i].Date.Split(' ');
                elecSheet.Cell(i + 3, 1).Value = datetime[0];
                elecSheet.Cell(i + 3, 2).Value = datetime[1];
                for (int j = 1; j < payApps.Length; j++)
                {
                    val = details[i].Balances[j - 1];
                    elecSheet.Cell(i + 3, j + 2).Value = val;
                    total += val;
                }
                elecSheet.Cell(i + 3, payApps.Length + 2).Value = total;
            }


            IXLWorksheet dailySheet = workbook.Worksheets.Add("Daily Inventory");
            dailySheet.Range("A1:J1").Merge().Value = "التقفيل اليومي";
            dailySheet.Row(1).Height = 30;
            dailySheet.Cell("A1").Style.Font.Bold = true;
            dailySheet.Cell("A1").Style.Font.FontSize = 18;
            dailySheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dailySheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            dailySheet.Column(1).Width = 14; dailySheet.Cell(2, 1).Value = "التاريخ";
            dailySheet.Column(2).Width = 10; dailySheet.Cell(2, 2).Value = "اليوم";
            dailySheet.Column(3).Width = 13; dailySheet.Cell(2, 3).Value = "الوقت";
            dailySheet.Column(4).Width = 15; dailySheet.Cell(2, 4).Value = "محافظ";
            dailySheet.Column(5).Width = 15; dailySheet.Cell(2, 5).Value = "سيولة";
            dailySheet.Column(6).Width = 15; dailySheet.Cell(2, 6).Value = "مدفوعات إلكترونية";
            dailySheet.Column(7).Width = 15; dailySheet.Cell(2, 7).Value = "لنا";
            dailySheet.Column(8).Width = 15; dailySheet.Cell(2, 8).Value = "علينا";
            dailySheet.Column(9).Width = 15; dailySheet.Cell(2, 9).Value = "المجموع";
            dailySheet.Column(10).Width = 15; dailySheet.Cell(2, 10).Value = "الفرق";

            DailyClosureData[] data = DatabaseHelper.GetDailyClosure(dateChoose.Checked, from, to);
            float Sum, prevSum = 0;
            System.Globalization.CultureInfo ar = new("ar-EG");
            for (int i = 0; i < data.Length; i++)
            {
                datetime = data[i].Date.Split(' ');
                dailySheet.Cell(i + 3, 1).Value = datetime[0];
                dailySheet.Cell(i + 3, 2).Value = datetime[0].ToStandardDateTime().ToString("dddd", ar);
                dailySheet.Cell(i + 3, 3).Value = datetime[1];
                dailySheet.Cell(i + 3, 4).Value = data[i].TotalWallets;
                dailySheet.Cell(i + 3, 5).Value = data[i].TotalCash;
                dailySheet.Cell(i + 3, 6).Value = data[i].TotalElectronic;
                dailySheet.Cell(i + 3, 7).Value = data[i].Credit;
                dailySheet.Cell(i + 3, 8).Value = data[i].Debit;
                Sum = data[i].Sum;
                dailySheet.Cell(i + 3, 9).Value = Sum;
                dailySheet.Cell(i + 3, 10).Value = Sum - prevSum;
                prevSum = Sum;
            }


            IXLWorksheet walletsSheet = workbook.Worksheets.Add("Wallets");
            walletsSheet.Range("A1:H1").Merge().Value = "المحافظ";
            walletsSheet.Row(1).Height = 30;
            walletsSheet.Cell("A1").Style.Font.Bold = true;
            walletsSheet.Cell("A1").Style.Font.FontSize = 18;
            walletsSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            walletsSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            walletsSheet.Column(1).Width = 15; walletsSheet.Cell(2, 1).Value = "رقم الهاتف";
            walletsSheet.Column(2).Width = 15; walletsSheet.Cell(2, 2).Value = "أقصى السحب";
            walletsSheet.Column(3).Width = 15; walletsSheet.Cell(2, 3).Value = "أقصى الإيداع";
            walletsSheet.Column(4).Width = 15; walletsSheet.Cell(2, 4).Value = "باقي السحب";
            walletsSheet.Column(5).Width = 15; walletsSheet.Cell(2, 5).Value = "باقي الإيداع";
            walletsSheet.Column(6).Width = 15; walletsSheet.Cell(2, 6).Value = "الرصيد";
            walletsSheet.Column(7).Width = 10; walletsSheet.Cell(2, 7).Value = "النوع";
            walletsSheet.Column(8).Width = 30; walletsSheet.Cell(2, 8).Value = "تعليق";
            WalletRowData[] wallets = DatabaseHelper.GetWallets();
            for (int i = 0; i < wallets.Length; i++)
            {
                walletsSheet.Cell(i + 3, 1).Value = wallets[i].Phone;
                walletsSheet.Cell(i + 3, 2).Value = wallets[i].MaximumWithdrawal;
                walletsSheet.Cell(i + 3, 3).Value = wallets[i].MaximumDeposit;
                walletsSheet.Cell(i + 3, 4).Value = wallets[i].WithdrawalRemaining;
                walletsSheet.Cell(i + 3, 5).Value = wallets[i].DepositRemaining;
                walletsSheet.Cell(i + 3, 6).Value = wallets[i].Balance;
                walletsSheet.Cell(i + 3, 7).Value = GetWalletType(wallets[i].Type);
                walletsSheet.Cell(i + 3, 8).Value = wallets[i].Comment;
            }


            IXLWorksheet recordsSheet = workbook.Worksheets.Add("Wallets Records");
            recordsSheet.Range("A1:I1").Merge().Value = "عمليات المحافظ";
            recordsSheet.Row(1).Height = 30;
            recordsSheet.Cell("A1").Style.Font.Bold = true;
            recordsSheet.Cell("A1").Style.Font.FontSize = 18;
            recordsSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            recordsSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            recordsSheet.Column(1).Width = 14; recordsSheet.Cell(2, 1).Value = "التاريخ";
            recordsSheet.Column(2).Width = 13; recordsSheet.Cell(2, 2).Value = "الوقت";
            recordsSheet.Column(3).Width = 15; recordsSheet.Cell(2, 3).Value = "رقم الهاتف";
            recordsSheet.Column(4).Width = 15; recordsSheet.Cell(2, 4).Value = "باقي السحب";
            recordsSheet.Column(5).Width = 15; recordsSheet.Cell(2, 5).Value = "باقي الإيداع";
            recordsSheet.Column(6).Width = 15; recordsSheet.Cell(2, 6).Value = "مبلغ السحب";
            recordsSheet.Column(7).Width = 15; recordsSheet.Cell(2, 7).Value = "مبلغ الإيداع";
            recordsSheet.Column(8).Width = 15; recordsSheet.Cell(2, 8).Value = "الرصيد";
            recordsSheet.Column(9).Width = 30; recordsSheet.Cell(2, 9).Value = "ملاحظة";
            RecordRowData[] records = dateChoose.Checked ? DatabaseHelper.GetRecords(from, to) : DatabaseHelper.GetRecords();
            for (int i = 0; i < records.Length; i++)
            {
                datetime = records[i].Date.Split(' ');
                recordsSheet.Cell(i + 3, 1).Value = datetime[0];
                recordsSheet.Cell(i + 3, 2).Value = datetime[1];
                recordsSheet.Cell(i + 3, 3).Value = records[i].Phone;
                recordsSheet.Cell(i + 3, 4).Value = records[i].WithdrawalRemaining;
                recordsSheet.Cell(i + 3, 5).Value = records[i].DepositRemaining;
                recordsSheet.Cell(i + 3, 6).Value = records[i].Withdrawal;
                recordsSheet.Cell(i + 3, 7).Value = records[i].Deposit;
                recordsSheet.Cell(i + 3, 8).Value = records[i].Balance;
                recordsSheet.Cell(i + 3, 9).Value = records[i].Comment;
            }


            IXLWorksheet expensesSheet = workbook.Worksheets.Add("Expenses");
            expensesSheet.Range("A1:F1").Merge().Value = "المصروفات";
            expensesSheet.Row(1).Height = 30;
            expensesSheet.Cell("A1").Style.Font.Bold = true;
            expensesSheet.Cell("A1").Style.Font.FontSize = 18;
            expensesSheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            expensesSheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            expensesSheet.Column(1).Width = 14; expensesSheet.Cell(2, 1).Value = "التاريخ";
            expensesSheet.Column(2).Width = 13; expensesSheet.Cell(2, 2).Value = "الوقت";
            expensesSheet.Column(3).Width = 30; expensesSheet.Cell(2, 3).Value = "العنوان";
            expensesSheet.Column(4).Width = 15; expensesSheet.Cell(2, 4).Value = "المبلغ";
            expensesSheet.Column(5).Width = 30; expensesSheet.Cell(2, 5).Value = "مسار المرفق";
            expensesSheet.Column(6).Width = 30; expensesSheet.Cell(2, 6).Value = "ملاحظة";
            ExpenseRowData[] expenses = dateChoose.Checked ? DatabaseHelper.GetExpenses(from, to) : DatabaseHelper.GetExpenses();
            for (int i = 0; i < expenses.Length; i++)
            {
                datetime = expenses[i].Date.Split(' ');
                expensesSheet.Cell(i + 3, 1).Value = datetime[0];
                expensesSheet.Cell(i + 3, 2).Value = datetime[1];
                expensesSheet.Cell(i + 3, 3).Value = expenses[i].Title;
                expensesSheet.Cell(i + 3, 4).Value = expenses[i].Amount;
                expensesSheet.Cell(i + 3, 5).Value = expenses[i].Attachment;
                expensesSheet.Cell(i + 3, 6).Value = expenses[i].Comment;
            }


            try
            {
                workbook.SaveAs(path);
                MessageBox.Show("تم استخراج البيانات بنجاح");
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الملف\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExtraExcelBtn_MouseEnter(object sender, EventArgs e)
        {
            if (!isRunning) extraExcelBtn.Image = Properties.Resources.diagonal_mouth;
        }

        private void ExtraExcelBtn_MouseLeave(object sender, EventArgs e)
        {
            if (!isRunning) extraExcelBtn.Image = Properties.Resources.neutral_face;
        }

        private void ExtraExcelBtn_MouseDown(object sender, MouseEventArgs e)
        {
            extraExcelBtn.Image = Properties.Resources.confused_face;
        }
        #endregion

    }
}
