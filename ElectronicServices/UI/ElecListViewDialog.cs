
namespace ElectronicServices
{
    public partial class ElecListViewDialog : Form
    {
        int payappsLength; bool isDated; bool sizeChanged;
        public ElecListViewDialog(DateTime? date, bool changeDate)
        {
            InitializeComponent();
            if (date is null)
            {
                datePicker.Value = DateTime.Now;
                datePicker.ValueChanged += DatePicker_ValueChanged;
                listView1.DoubleClick += (s, e) => ConfirmSelection();

                saveDataBtn.Text = "إضافة جديد";

                SumDate[] dates = DatabaseHelper.GetPayappClosureDates();

                if (dates.Length >= 17)
                {
                    ClientSize = new Size(ClientSize.Width + 20, ClientSize.Height);
                    listView1.ClientSize = new Size(listView1.ClientSize.Width + 20, listView1.ClientSize.Height);
                    sizeChanged = true;
                }

                listView1.Columns.Add("التاريخ", 300, HorizontalAlignment.Center);
                listView1.Columns.Add("المجموع", listView1.ClientSize.Width - 301, HorizontalAlignment.Center);

                ListViewItem item;
                for (int i = 0; i < dates.Length; i++)
                {
                    item = new ListViewItem(dates[i].Date);
                    item.SubItems.Add(dates[i].Sum.ToString("0.##"));
                    listView1.Items.Add(item);
                }
            }
            else
            {
                isDated = true;
                datePicker.Value = (DateTime)date;
                datePicker.ValueChanged += DatePicker_ValueChanged;
                datePicker.Enabled = changeDate;

                string[] payapps = DatabaseHelper.GetPayappsNames(true);
                payappsLength = payapps.Length;

                if (payapps.Length >= 17)
                {
                    ClientSize = new Size(ClientSize.Width + 20, ClientSize.Height);
                    listView1.ClientSize = new Size(listView1.ClientSize.Width + 20, listView1.ClientSize.Height);
                }

                listView1.Columns.Add("تطبيق الدفع الإلكتروني", 300, HorizontalAlignment.Center);
                listView1.Columns.Add("الرصيد", listView1.ClientSize.Width - 301, HorizontalAlignment.Center);

                ListViewItem item;
                for (int i = 0; i < payapps.Length; i++)
                {
                    item = new ListViewItem(payapps[i]);
                    item.SubItems.Add("0");
                    listView1.Items.Add(item);
                }

                item = new ListViewItem("المجموع") { BackColor = Color.FromArgb(220, 255, 220) };
                item.SubItems.Add("0.00");
                listView1.Items.Add(item);

                diff = new("الفرق بين اليوم السابق") { BackColor = Color.FromArgb(220, 220, 220) };
                diff.SubItems.Add("0.00");
                listView1.Items.Add(diff);

                listView1.ItemSelectionChanged += (s, e) =>
                {
                    if ((e.Item == item || e.Item == diff) && e.IsSelected)
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
            string date = itm.SubItems[0].Text;

            ElecListViewDialog elvd = new(date.ToStandardDateTime(), false);
            elvd.ShowDialog();

            float sum = DatabaseHelper.GetSumPayappClosure(date);
            itm.SubItems[1].Text = sum.ToString("0.##");
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

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            string date = datePicker.Value.ToStandardString();

            if (!isDated)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Text == date)
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

            float[] values = DatabaseHelper.GetPayappClosure(date);
            for (int i = 0; i < values.Length; i++)
                listView1.Items[i].SubItems[1].Text = values[i].ToString();
            for (int i = values.Length; i < payappsLength; i++)
                listView1.Items[i].SubItems[1].Text = "0";

            float total = values.Sum();
            listView1.Items[payappsLength].SubItems[1].Text = total.ToString("0.##");

            float prevTotal = DatabaseHelper.GetSumPrevPayappClosure(date);

            if (total - prevTotal < 0)
                diff.BackColor = Color.FromArgb(255, 220, 220);
            else if (total - prevTotal > 0)
                diff.BackColor = Color.FromArgb(220, 220, 255);
            else
                diff.BackColor = Color.FromArgb(240, 240, 240);

            listView1.Items[payappsLength + 1].SubItems[1].Text = (total - prevTotal).ToString("0.##");
        }

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            if (!isDated)
            {
                ElecListViewDialog elvd = new(DateTime.Now, true);
                elvd.ShowDialog();

                SumDate[] dates = DatabaseHelper.GetPayappClosureDates();

                if (!sizeChanged && dates.Length >= 17)
                {
                    ClientSize = new Size(ClientSize.Width + 20, ClientSize.Height);
                    listView1.ClientSize = new Size(listView1.ClientSize.Width + 20, listView1.ClientSize.Height);
                    sizeChanged = true;
                }

                listView1.Items.Clear();

                ListViewItem item;
                for (int i = 0; i < dates.Length; i++)
                {
                    item = new ListViewItem(dates[i].Date);
                    item.SubItems.Add(dates[i].Sum.ToString("0.##"));
                    listView1.Items.Add(item);
                }

                return;
            }

            string date = datePicker.Value.ToStandardString();

            for (int i = 0; i < payappsLength; i++)
            {
                float.TryParse(listView1.Items[i].SubItems[1].Text, out float val);
                DatabaseHelper.SetPayappClosure(date, i + 1, val);
            }

            DatePicker_ValueChanged(this, EventArgs.Empty);
        }

    }
}
