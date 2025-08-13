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
            cust.Location = new Point(cust.Location.X + 15, 5);
            customersPanel.Controls.Add(cust);
            customerCode.Tag = DatabaseHelper.GetCustomerNextId();
            customerCode.Text = customerCode.Tag.ToString();
            customerName.Text = string.Empty;

            TransactionRow trans = new();
            trans.Location = new Point(trans.Location.X + 15, 5);
            transactionsPanel.Controls.Add(trans);
            addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            customersComboBox.Items.Add("«Œ — „‰ «·ﬁ«∆„…");
            customersComboBox.Items.AddRange(DatabaseHelper.GetCustomersNames());
            transDate.Value = DateTime.Now;
        }

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



        public static Bitmap TintPng(Bitmap original, Color tintColor)
        {
            Bitmap tinted = new Bitmap(original.Width, original.Height);
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    if (pixel.A > 0) // ›ﬁÿ ·Ê «·»ﬂ”· „‘ ‘›«›
                    {
                        tinted.SetPixel(x, y, Color.FromArgb(pixel.A, tintColor.R, tintColor.G, tintColor.B));
                    }
                    else
                    {
                        tinted.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
            return tinted;
        }

        private void CustomersBtn_Click(object sender, EventArgs e)
        {
            transactionsPanel.Visible = false;
            addTransactionsPanel.Visible = false;
            customersPanel.Visible = true;
            addCustomersPanel.Visible = true;
        }

        private void TransactionsBtn_Click(object sender, EventArgs e)
        {
            customersPanel.Visible = false;
            addCustomersPanel.Visible = false;
            transactionsPanel.Visible = true;
            addTransactionsPanel.Visible = true;
        }

        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            string custName = customerName.Text.Trim();

            if (custName == "") return;

            if (DatabaseHelper.SearchWithExactCustomerName(custName))
            {
                MessageBox.Show("·ﬁœ  „  ≈÷«›… Â–« «·⁄„Ì· „‰ ﬁ»·", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.AddCustomer(custName))
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ≈÷«›… «·⁄„Ì·.\n«·—Ã«¡ «·„Õ«Ê·… „—… √Œ—Ï", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = customersPanel.Controls.Count;
            CustomerRow cust = new(new CustomerRowData
            {
                Code = (int)customerCode.Tag,
                Name = custName,
                Pay = 0f,
                Take = 0f,
            });
            cust.Location = new Point(cust.Location.X + 15, (cust.Size.Height + 3) * count + 5);
            customersPanel.Controls.Add(cust);

            customerCode.Tag = DatabaseHelper.GetCustomerNextId();
            customerCode.Text = customerCode.Tag.ToString();
            customerName.Text = string.Empty;
        }

        private void CustomerSearchBtn_Click(object sender, EventArgs e)
        {
            CustomerRowData[] customers = DatabaseHelper.GetCustomers(customerName.Text.Trim() == "" ? "" : customerName.Text);

            customersPanel.Controls.Clear();
            var cust = new CustomerRow();
            cust.Location = new Point(cust.Location.X + 15, 5);
            customersPanel.Controls.Add(cust);

            CustomerRow row;
            for (int i = 0; i < customers.Length; i++)
            {
                row = new(customers[i]);
                row.Location = new Point(row.Location.X + 15, (row.Size.Height + 3) * (i + 1) + 5);
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
                CustomerId = customersComboBox.SelectedIndex,
                Name = customersComboBox.Text,
                Date = transDate.Value,
                Pay = (float)payAmount.Value,
                Take = (float)takeAmount.Value,
                Note = transNote.Text.Trim(),
            };

            if (!DatabaseHelper.AddTransaction(data))
            {
                MessageBox.Show("ÕœÀ Œÿ√ √À‰«¡ ≈÷«›… «·„⁄«„·….\n«·—Ã«¡ «·„Õ«Ê·… „—… √Œ—Ï", "Œÿ√", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = transactionsPanel.Controls.Count;
            TransactionRow row = new(data);
            row.Location = new Point(row.Location.X + 15, (row.Size.Height + 3) * count + 5);
            transactionsPanel.Controls.Add(row);

            addTransactionsPanel.Tag = DatabaseHelper.GetTransactionNextId();
            payAmount.Value = 0; takeAmount.Value = 0;
        }

        private void TransSearchBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
