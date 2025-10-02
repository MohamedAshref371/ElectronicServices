using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class WalletRow : UserControl
    {
        public WalletRow()
        {
            InitializeComponent();
            chooseBtn.Visible = false;
            editBtn.Visible = false;
            deleteBtn.Visible = false;
        }

        private WalletRowData data;
        public WalletRow(WalletRowData data)
        {
            InitializeComponent();
            SetWalletRowData(data);
        }

        public string Phone => data.Phone;

        public void SetWalletRowData(WalletRowData data)
        {
            this.data = data;
            phoneNumber.Text = data.Phone;
            walletType.Text = Program.Form.GetWalletType(data.Type);
            withdrawal.Text = data.WithdrawalRemaining.ToString();
            deposit.Text = data.DepositRemaining.ToString();
            balance.Text = data.Balance.ToString();
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
            => Program.Form.ChooseWalletBtn(data);

        private void EditBtn_Click(object sender, EventArgs e)
            => Program.Form.SetWalletData(data);

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            bool res = DatabaseHelper.IsThereRecords(data.Phone);

            if (res && MessageBox.Show("لا يمكن حذف هذه المحفظة لأنها مرتبطة بعمليات\nهل تريد تصفير هذه المحفظة أولا ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (res)
            {
                if (!DatabaseHelper.ResetWallet(data.Phone))
                {
                    MessageBox.Show("حدث خطأ أثناء حذف عمليات المحفظة. يرجى المحاولة مرة أخرى.");
                    return;
                }
                Program.Form.ResetWallet(data.Phone);
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من حذف هذه المحفظة ؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (!DatabaseHelper.DeleteWallet(data.Phone))
            {
                MessageBox.Show("حدث خطأ أثناء حذف المحفظة. يرجى المحاولة مرة أخرى.");
                return;
            }

            Program.Form.CheckWallet(data.Phone);
            data.Phone = "";
            this.Enabled = false;
        }

        #region Border Radius
        private readonly int borderRadius = 20;

        private GraphicsPath GetRoundedPath()
        {
            GraphicsPath path = new();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(Width - borderRadius, Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Region = new Region(GetRoundedPath());
        }
        #endregion

        private void ChooseBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(chooseBtn.Width, chooseBtn.Height);
            Size newSize = new(minSize, minSize);

            chooseBtn.ImageSize = newSize;
            editBtn.ImageSize = newSize;
            deleteBtn.ImageSize = newSize;
        }
    }
}
