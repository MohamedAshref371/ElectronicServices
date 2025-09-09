
namespace ElectronicServices
{
    public partial class ElecListViewDialog : Form
    {
        public int SelectedIndex { get; private set; } = -1;

        public ElecListViewDialog()
        {
            InitializeComponent();

            if (data.Length >= 17)
            {
                ClientSize = new Size(ClientSize.Width + 20, ClientSize.Height);
                listView1.ClientSize = new Size(listView1.ClientSize.Width + 20, listView1.ClientSize.Height);
            }

            listView1.Columns.Add("تطبيق الدفع الإلكتروني", 320, HorizontalAlignment.Center);
            listView1.Columns.Add("الرصيد", listView1.ClientSize.Width - 321, HorizontalAlignment.Center);

            ListViewItem item; int total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                item = new ListViewItem(data[i].Text);
                total += data[i].Count;
                item.SubItems.Add(data[i].Count.ToString());
                listView1.Items.Add(item);
            }

            listView1.DoubleClick += (s, e) => ConfirmSelection();

            item = new ListViewItem("المجموع") { BackColor = Color.FromArgb(220, 255, 220) };
            item.SubItems.Add(total.ToString());
            listView1.Items.Add(item);
            listView1.ItemSelectionChanged += (s, e) =>
            {
                if (e.Item == item && e.IsSelected)
                    e.Item.Selected = false;
            };

            ListViewItem item2 = new ("الفرق بين اليوم السابق") { BackColor = Color.FromArgb(220, 255, 220) };
            item2.SubItems.Add(/*total*/.ToString());
            listView1.Items.Add(item2);
            listView1.ItemSelectionChanged += (s, e) =>
            {
                if (e.Item == item2 && e.IsSelected)
                    e.Item.Selected = false;
            };
        }

        private void ConfirmSelection()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                SelectedIndex = listView1.SelectedIndices[0];
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string query = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                listView1.SelectedItems.Clear();
                return;
            }

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
                else
                    item.Selected = false;
            }
        }

    }
}
