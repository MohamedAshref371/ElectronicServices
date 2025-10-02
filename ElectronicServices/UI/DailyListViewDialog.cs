
namespace ElectronicServices
{
    public partial class DailyListViewDialog : Form
    {
        bool isDated; bool sizeChanged; bool isNew;
        public DailyListViewDialog(DateTime? date, bool changeDate, bool isNew)
        {
            InitializeComponent();
            this.isNew = isNew;
            if (date is null)
            {
                datePicker.Value = DateTime.Now;
                datePicker.ValueChanged += DatePicker_ValueChanged;
                listView1.DoubleClick += (s, e) => ConfirmSelection();

                saveDataBtn.Text = "إضافة";

                listView1.Columns.Add("            التاريخ", 300, HorizontalAlignment.Center);
                listView1.Columns.Add("المجموع", listView1.ClientSize.Width - 301, HorizontalAlignment.Center);

                AddDailyClosures();
            }
            else
            {
                isDated = true;
                datePicker.Value = (DateTime)date;
                datePicker.ValueChanged += DatePicker_ValueChanged;
                datePicker.Enabled = changeDate;

                listView1.Columns.Add("تفاصيل التقفيل اليومي", 300, HorizontalAlignment.Center);
                listView1.Columns.Add("الرصيد", listView1.ClientSize.Width - 301, HorizontalAlignment.Center);

                ListViewItem item;

                item = new ListViewItem("");
                item.SubItems.Add("");
                listView1.Items.Add(item);

                item = new ListViewItem("التاريخ") { BackColor = Color.FromArgb(255, 255, 255) };
                item.SubItems.Add("");
                listView1.Items.Add(item);

                item = new ListViewItem("");
                item.SubItems.Add("");
                listView1.Items.Add(item);

                item = new ListViewItem("رصيد المحافظ") { BackColor = Color.FromArgb(255, 255, 255) };
                item.SubItems.Add("0");
                listView1.Items.Add(item);

                ListViewItem item2 = new ListViewItem("السيولة");
                item2.SubItems.Add("0");
                listView1.Items.Add(item2);

                item = new ListViewItem("المدفوعات الإلكترونية") { BackColor = Color.FromArgb(255, 255, 255) };
                item.SubItems.Add("0");
                listView1.Items.Add(item);

                item = new ListViewItem("");
                item.SubItems.Add("");
                listView1.Items.Add(item);

                item = new ListViewItem("لنا") { BackColor = Color.FromArgb(255, 255, 255) };
                item.SubItems.Add("0");
                listView1.Items.Add(item);

                item = new ListViewItem("علينا") { BackColor = Color.FromArgb(255, 255, 255) };
                item.SubItems.Add("0");
                listView1.Items.Add(item);

                item = new ListViewItem("");
                item.SubItems.Add("");
                listView1.Items.Add(item);

                item = new ListViewItem("المجموع") { BackColor = Color.FromArgb(220, 255, 220) };
                item.SubItems.Add("0");
                listView1.Items.Add(item);

                diff = new ListViewItem("الفرق بين اليوم السابق") { BackColor = Color.FromArgb(220, 220, 220) };
                diff.SubItems.Add("0");
                listView1.Items.Add(diff);

                listView1.ItemSelectionChanged += (s, e) =>
                {
                    if (e.Item != item2 && e.IsSelected)
                        e.Item.Selected = false;
                };

                DatePicker_ValueChanged(this, EventArgs.Empty);
            }
        }
        ListViewItem diff;

        private void ConfirmSelection()
        {
            if (listView1.SelectedIndices.Count == 0) return;
            var itm = listView1.SelectedItems[0];
            string date = itm.SubItems[0].Text[5..].Replace("   ", " ");

            DailyListViewDialog elvd = new(date.ToCompleteStandardDateTime(), false, false);
            elvd.ShowDialog();

            float sum = DatabaseHelper.GetSumDailyClosure(date);
            itm.SubItems[1].Text = sum.ToString();
        }

        private void ListView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isDated || listView1.SelectedIndices.Count == 0) return;

            var itms = listView1.SelectedItems[0].SubItems[1];
            string text = itms.Text;

            if (e.KeyChar == (char)Keys.Back)
                itms.Text = text.Length >= 2 ? text[..^1] : "0";

            else if ((e.KeyChar == '.' || e.KeyChar == ',') && !text.Contains('.'))
                itms.Text += text == "-" ? "0." : ".";

            else if (e.KeyChar == '-' && text == "0")
                itms.Text = "-";

            else if (e.KeyChar >= '0' && e.KeyChar <= '9')
                itms.Text = text == "0" ? e.KeyChar.ToString() : (itms.Text + e.KeyChar);

            else if (e.KeyChar == (char)Keys.Delete)
                itms.Text = "0";
        }

        bool changeWithSave = false;
        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (changeWithSave) return;
            string date = datePicker.Value.ToCompleteStandardString();

            if (!isDated)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Text[5..15] == date[..10])
                    {
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        break;
                    }
                    else
                        item.Selected = false;
                }

                return;
            }

            DailyClosureData? data = DatabaseHelper.GetDailyClosure(date);

            if (data is null)
            {
                float totalElec = 0f;
                int id = DatabaseHelper.FindDayInPayappClosure(date[..10]);
                if (id == 0)
                {
                    if (MessageBox.Show("لم يتم إقفال تطبيقات الدفع الإلكتروني لهذا اليوم\nهل تريد الإستمرار ؟.", "تنبيه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        Close();
                        return;
                    }
                }
                else if (!DatabaseHelper.IsNotUsed(id))
                {
                    DialogResult res = MessageBox.Show("هناك اقفال يومي بالفعل لآخر إقفال لتطبيقات الدفع الإلكتروني\nهل تريد استعماله لهذا الإقفال أيضا ؟.", "تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (res == DialogResult.Cancel)
                    {
                        Close();
                        return;
                    }
                    else if (res == DialogResult.Yes)
                        totalElec = DatabaseHelper.GetSumPayappClosure(id);
                    else
                        id = 0;
                }
                else
                    totalElec = DatabaseHelper.GetSumPayappClosure(id);

                float[] creditDebit = DatabaseHelper.GetCreditAndDept(date, true);

                float totalWallets = DatabaseHelper.GetTotalWalletsBalance(0);

                data = new DailyClosureData
                {
                    Date = date,
                    TotalWallets = totalWallets,
                    TotalCash = 0,
                    TotalElectronic = totalElec,
                    Credit = creditDebit[0],
                    Debit = creditDebit[1],
                    PayappClosureId = id
                };
            }

            float prev = DatabaseHelper.GetSumPrevDailyClosure(date);
            float total = data.Sum;

            listView1.Items[1].SubItems[1].Text = "\u200E" + data.Date;
            listView1.Items[3].SubItems[1].Text = data.TotalWallets.ToString();
            listView1.Items[4].SubItems[1].Text = data.TotalCash.ToString();
            listView1.Items[5].SubItems[1].Text = data.TotalElectronic.ToString();
            listView1.Items[7].SubItems[1].Text = data.Credit.ToString();
            listView1.Items[8].SubItems[1].Text = data.Debit.ToString();
            listView1.Items[10].SubItems[1].Text = total.ToString();

            if (total - prev < 0)
                diff.BackColor = Color.FromArgb(255, 220, 220);
            else if (total - prev > 0)
                diff.BackColor = Color.FromArgb(220, 220, 255);
            else
                diff.BackColor = Color.FromArgb(240, 240, 240);

            listView1.Items[11].SubItems[1].Text = (total - prev).ToString();
            id = data.PayappClosureId;
        }
        int id;

        private void AddDailyClosures()
        {
            SumDate[] dates = DatabaseHelper.GetDailyClosureDates();

            if (!sizeChanged && dates.Length >= 19)
            {
                ClientSize = new Size(ClientSize.Width + 20, ClientSize.Height);
                listView1.ClientSize = new Size(listView1.ClientSize.Width + 20, listView1.ClientSize.Height);
                sizeChanged = true;
            }

            listView1.Items.Clear();

            ListViewItem item;
            for (int i = 0; i < dates.Length; i++)
            {
                item = new ListViewItem("    \u200E" + dates[i].Date.Replace(" ", "   "));
                item.SubItems.Add(dates[i].Sum.ToString());
                listView1.Items.Add(item);
            }
        }

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            if (!isDated)
            {
                DailyListViewDialog elvd = new(DateTime.Now, false, true);
                if (!elvd.IsDisposed)
                {
                    elvd.ShowDialog();
                    AddDailyClosures();
                }
                return;
            }


            if (isNew)
            {
                changeWithSave = true;
                datePicker.Value = DateTime.Now;
                changeWithSave = false;
            }

            string date = datePicker.Value.ToCompleteStandardString();

            float totalElec = DatabaseHelper.GetSumPayappClosure(id);

            float[] creditDebit = DatabaseHelper.GetCreditAndDept(date, true);

            float.TryParse(listView1.Items[3].SubItems[1].Text, out float wallets);
            float.TryParse(listView1.Items[4].SubItems[1].Text, out float cash);

            DailyClosureData data = new DailyClosureData
            {
                Date = date,
                TotalWallets = wallets,
                TotalCash = cash,
                TotalElectronic = totalElec,
                Credit = creditDebit[0],
                Debit = creditDebit[1],
                PayappClosureId = id
            };

            DatabaseHelper.SetDailyClosure(data);
            DatabaseHelper.SetPayappClosured(id);

            DatePicker_ValueChanged(this, EventArgs.Empty);
        }

        private void DateLabel_DoubleClick(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("هل تريد حذف آخر تقفيل يومي ؟!", "?!؟", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);

            if (res == DialogResult.No) return;

            DatabaseHelper.DeleteLastDailyClosure();

            AddDailyClosures();
        }
        
    }
}
