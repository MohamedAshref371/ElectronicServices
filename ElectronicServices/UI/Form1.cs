using ClosedXML.Excel;
using Guna.UI2.WinForms;

namespace ElectronicServices
{
    public partial class Form1 : Form
    {
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
            phoneNumber.MouseDown += meh;


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
            transDate.Value = DateTime.Now;
            excelDate.Value = DateTime.Now;

            UpdatePayappComboBox();
            UpdateCreditAndDept();
        }
        int rowPadding = 7;

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            //string f = "C:\\Users\\Mohamed\\Pictures\\logo.png";
            //string f1 = "C:\\Users\\Mohamed\\Pictures\\logo2.png";
            //TintPng(new Bitmap(f), Color.Orange).Save(f1);
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

        //public static Bitmap TintPng(Bitmap original, Color tintColor)
        //{
        //    Bitmap tinted = new Bitmap(original.Width, original.Height);
        //    for (int y = 0; y < original.Height; y++)
        //    {
        //        for (int x = 0; x < original.Width; x++)
        //        {
        //            Color pixel = original.GetPixel(x, y);
        //            if (pixel.A > 0) // ›ﬁÿ ·Ê «·»ﬂ”· „‘ ‘›«›
        //            {
        //                tinted.SetPixel(x, y, Color.FromArgb(pixel.A, tintColor.R, tintColor.G, tintColor.B));
        //            }
        //            else
        //            {
        //                tinted.SetPixel(x, y, Color.Transparent);
        //            }
        //        }
        //    }
        //    return tinted;
        //}

        private void CustomersBtn_Click(object sender, EventArgs e)
        {
            customersPanel.Visible = true;
            addCustomersPanel.Visible = true;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
        }

        private void TransactionsBtn_Click(object sender, EventArgs e)
        {
            transactionsPanel.Visible = true;
            addTransactionsPanel.Visible = true;
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            string custName = customerName.Text.Trim();

            if (custName == "") return;

            int res = DatabaseHelper.SearchWithExactCustomerName(custName);
            if (res < 0)
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ﬁ—«¡… «·»Ì«‰« ", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (res >= 1)
            {
                MessageBox.Show("·ﬁœ  „  ≈÷«›… Â–« «·⁄„Ì· „‰ ﬁ»·", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddCustomer(custName))
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ≈÷«›… «·⁄„Ì·\n«·—Ã«¡ «·„Õ«Ê·… „—… √Œ—Ï", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Date = DateTime.Now.ToStandardString(),
                    Pay = (float)custCreditAmount.Value,
                    Take = (float)custDebitAmount.Value,
                    PayWith = -1,
                    TakeWith = -1,
                    Note = "",
                };

                if (!DatabaseHelper.AddTransaction(data))
                    MessageBox.Show("ÕœÀ Œÿ√ «À‰«¡ ≈÷«›… «·ﬁÌ„ «·«» œ«∆Ì… ··⁄„Ì·", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);

                addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            }

            custCreditAmount.Value = 0;
            custDebitAmount.Value = 0;
            customerCode.Tag = DatabaseHelper.GetCustomerNextId();
            customerCode.Text = customerCode.Tag.ToString();
            customerName.Text = string.Empty;
        }

        private void UpdateCustomersComboBox()
        {
            customersComboBox.Items.Clear();
            customersComboBox.Items.Add(new KeyValuePair<int, string>(0, "«Œ — „‰ «·ﬁ«∆„…"));
            var customers = DatabaseHelper.GetCustomersNames();
            foreach (var customer in customers)
                customersComboBox.Items.Add(customer);
            customersComboBox.SelectedIndex = 0;
        }

        private void UpdateCreditAndDept()
        {
            float[] company = DatabaseHelper.GetCreditAndDept();
            creditAmount.Text = company[0].ToString("N2");
            debitAmount.Text = company[1].ToString("N2");
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

        private void AddTransactionBtn_Click(object sender, EventArgs e)
        {
            if (customersComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("«·—Ã«¡ «Œ Ì«— ⁄„Ì· „‰ «·ﬁ«∆„…", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (payAmount.Value == 0 && takeAmount.Value == 0)
            {
                MessageBox.Show("«·—Ã«¡ ≈œŒ«· ﬁÌ„… «·œ›⁄ √Ê «·”Õ»", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TransactionRowData data = new()
            {
                Id = (int)addTransactionsPanel.Tag,
                CustomerId = ((KeyValuePair<int, string>)customersComboBox.Items[customersComboBox.SelectedIndex]).Key,
                Name = customersComboBox.Text,
                Date = transDate.Value.ToStandardString(),
                Pay = (float)payAmount.Value,
                Take = (float)takeAmount.Value,
                PayWith = payWith.Enabled ? payWith.SelectedIndex : -1,
                TakeWith = takeWith.Enabled ? takeWith.SelectedIndex : -1,
                Note = transNote.Text.Trim(),
            };

            if (!DatabaseHelper.AddTransaction(data))
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ≈÷«›… «·„⁄«„·….\n«·—Ã«¡ «·„Õ«Ê·… „—… √Œ—Ï", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // transDate.Value = DateTime.Now;
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

        private void CustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FieldData[] data = DatabaseHelper.CustomerFieldSearch();
                if (data is null) return;
                ListViewDialog lvd = new("«”„ «·⁄„Ì·", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                customerName.Text = data[lvd.SelectedIndex].Text;
            }
        }

        private void CustomersComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            int key = (int)e.KeyCode - 111;
            FieldData[] data;
            ListViewDialog lvd;
            if (key == 1)
            {
                data = DatabaseHelper.TransFieldSearch();
                if (data is null) return;
                lvd = new("«”„ «·⁄„Ì·", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                customersComboBox.SelectedItem = customersComboBox.Items.Cast<KeyValuePair<int, string>>().FirstOrDefault(c => c.Value == data[lvd.SelectedIndex].Text);
            }
            else if (key >= 6 && key <= 8)
            {
                int custId = ((KeyValuePair<int, string>)customersComboBox.Items[customersComboBox.SelectedIndex]).Key;
                data = DatabaseHelper.TransFieldSearch(custId, key);
                if (data is null) return;
                lvd = new(" «—ÌŒ «·„⁄«„·…", data);
                if (lvd.ShowDialog() != DialogResult.OK || lvd.SelectedIndex == -1) return;

                TransactionRowData[] transactions = DatabaseHelper.GetTransactions(custId, data[lvd.SelectedIndex].Text, key);
                AddTransactionsRows(transactions);
            }
        }

        private void MainBtn_Click(object sender, EventArgs e)
        {
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
        }

        private void AddPayappBtn_Click(object sender, EventArgs e)
        {
            string payappName = payApp.Text.Trim();

            if (payappName == "") return;

            int res = DatabaseHelper.SearchWithExactPayappName(payappName);
            if (res < 0)
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ﬁ—«¡… «·»Ì«‰« ", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (res >= 1)
            {
                MessageBox.Show("·ﬁœ  „  ≈÷«›…  ÿ»Ìﬁ «·œ›⁄ Â–« „‰ ﬁ»·", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddPayapp(payappName))
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ≈÷«›…  ÿ»Ìﬁ «·œ›⁄\n«·—Ã«¡ «·„Õ«Ê·… „—… √Œ—Ï", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            payApp.Text = "";
            UpdatePayappComboBox();
        }

        private void UpdatePayappComboBox()
        {
            payWith.Items.Clear();
            takeWith.Items.Clear();
            var payApps = DatabaseHelper.GetPayappsNames();
            foreach (var payApp in payApps)
            {
                payWith.Items.Add(payApp);
                takeWith.Items.Add(payApp);
            }
            payWith.SelectedIndex = 0;
            takeWith.SelectedIndex = 0;
        }

        private void PayAmount_ValueChanged(object sender, EventArgs e)
        {
            payWith.Enabled = payAmount.Value > 0;
        }

        private void TakeAmount_ValueChanged(object sender, EventArgs e)
        {
            takeWith.Enabled = takeAmount.Value > 0;
        }

        private void UpdateCreditDepitBtn_Click(object sender, EventArgs e)
        {
            UpdateCreditAndDept();
        }

        private void DateLabel_DoubleClick(object sender, EventArgs e)
        {
            transDate.Value = DateTime.Now;
        }

        private void ExcelBtn_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ClosedXML.dll"))
            {
                MessageBox.Show("„ﬂ »«  «·«Ìﬂ”· €Ì— „ÊÃÊœ…");
                return;
            }
            if (saveExcelFileDialog.ShowDialog() != DialogResult.OK) return;

            ExtractExcel(saveExcelFileDialog.FileName);
        }

        public void ExtractExcel(string path)
        {
            using XLWorkbook workbook = new();
            workbook.RightToLeft = true;
            IXLWorksheet sheet = workbook.Worksheets.Add("Daily inventory");
            string date = excelDate.Value.ToStandardString();

            string[] payapps = DatabaseHelper.GetPayappsNames(true);
            char c = (char)('A' + payapps.Length + 1);
            if (c > 'Z') c = 'Z';
            sheet.Range($"A1:{c}1").Merge().Value = "„·Œ’ ÌÊ„     " + date;
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
                sheet.Cell(2, i + 2).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            }

            sheet.Column(payapps.Length + 2).Width = 12;
            sheet.Cell(2, payapps.Length + 2).Value = "«·„Ã„Ê⁄";
            sheet.Cell(2, payapps.Length + 2).Style.Fill.BackgroundColor = XLColor.LightBlue;

            sheet.Cell(3, 1).Value = "œ›⁄";
            sheet.Cell(4, 1).Value = "√Œ–";
            sheet.Cell(5, 1).Value = "«·„Ã„Ê⁄";

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


            float cashCredit = DatabaseHelper.GetCerditCashField(date), cashDebit = DatabaseHelper.GetDebitCashField(date);
            sheet.Cell(8, 1).Value = "";

            sheet.Cell(8, 2).Value = "«” ·«„ ‰ﬁœÌ";
            sheet.Cell(8, 3).Value = " ”·Ì„ ‰ﬁœÌ";
            sheet.Cell(8, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(8, 3).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(9, 2).Value = cashCredit;
            sheet.Cell(9, 3).Value = cashDebit;

            sheet.Cell(8, 4).Value = "«·„Ã„Ê⁄";
            sheet.Cell(8, 5).Value = "«·›—ﬁ";
            sheet.Cell(8, 4).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(8, 5).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(9, 4).Value = cashCredit + cashDebit;
            sheet.Cell(9, 5).Value = cashCredit - cashDebit;


            float[] creditDebit = DatabaseHelper.GetCreditAndDept(date);

            sheet.Cell(12, 2).Value = "·‰«";
            sheet.Cell(12, 3).Value = "⁄·Ì‰«";
            sheet.Cell(12, 2).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(12, 3).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(13, 2).Value = creditDebit[0];
            sheet.Cell(13, 3).Value = creditDebit[1];

            sheet.Cell(12, 4).Value = "«·„Ã„Ê⁄";
            sheet.Cell(12, 5).Value = "«·›—ﬁ";
            sheet.Cell(12, 4).Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
            sheet.Cell(12, 5).Style.Fill.BackgroundColor = XLColor.LightBlue;
            sheet.Cell(13, 4).Value = creditDebit[0] + creditDebit[1];
            sheet.Cell(13, 5).Value = creditDebit[0] - creditDebit[1];


            try
            {
                workbook.SaveAs(path);
                MessageBox.Show(" „ «” Œ—«Ã «·»Ì«‰«  »‰Ã«Õ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ «·„·›\n" + ex.Message, "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ElecPayBtn_Click(object sender, EventArgs e)
        {
            ElecListViewDialog elvd = new();
            elvd.ShowDialog();
        }
    }
}
