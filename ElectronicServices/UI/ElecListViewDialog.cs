
namespace ElectronicServices
{
    public partial class ElecListViewDialog : Form
    {
        int payappsLength;
        public ElecListViewDialog()
        {
            InitializeComponent();
            datePicker.Value = DateTime.Now;
            datePicker.ValueChanged += DatePicker_ValueChanged;

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
            item.SubItems.Add("0");
            listView1.Items.Add(item);

            diff = new("الفرق بين اليوم السابق") { BackColor = Color.FromArgb(220, 220, 220) };
            diff.SubItems.Add("0");
            listView1.Items.Add(diff);

            listView1.ItemSelectionChanged += (s, e) =>
            {
                if ((e.Item == item || e.Item == diff) && e.IsSelected)
                    e.Item.Selected = false;
            };

            DatePicker_ValueChanged(this, EventArgs.Empty);
        }
        ListViewItem diff;

        private void ListView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;

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

            float[] values = DatabaseHelper.GetPayappClosure(date);
            for (int i = 0; i < values.Length; i++)
                listView1.Items[i].SubItems[1].Text = values[i].ToString();

            float total = values.Sum();
            listView1.Items[payappsLength].SubItems[1].Text = total.ToString();

            float? prevTotal = DatabaseHelper.GetSumPrevPayappClosure(date);

            if (prevTotal == null)
            {
                diff.BackColor = Color.FromArgb(220, 220, 220);
                listView1.Items[payappsLength + 1].SubItems[1].Text = "لا يوجد";
                return;
            }

            if (total - prevTotal < 0)
                diff.BackColor = Color.FromArgb(255, 220, 220);
            else if (total - prevTotal > 0)
                diff.BackColor = Color.FromArgb(220, 220, 255);
            else
                diff.BackColor = Color.FromArgb(220, 220, 220);

            listView1.Items[payappsLength + 1].SubItems[1].Text = (total - prevTotal).ToString();
        }

        private void SaveDataBtn_Click(object sender, EventArgs e)
        {
            string date = datePicker.Value.ToStandardString();

            for (int i = 0; i < payappsLength; i++)
            {
                int.TryParse(listView1.Items[i].SubItems[1].Text, out int val);
                DatabaseHelper.SetPayappClosure(date, i + 1, val);
            }
        }

    }
}
