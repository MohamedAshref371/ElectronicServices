using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class WalletRow : UserControl
    {
        public const float MaxDailyWithdrawal = 60000f, MaxDailyDeposit = 60000f;
        public WalletRow()
        {
            InitializeComponent();
            chooseBtn.Visible = false;
            editBtn.Visible = false;
            deleteBtn.Visible = false;
            page.Visible = true;
        }

        private WalletRowData data;
        public WalletRow(WalletRowData data)
        {
            InitializeComponent();
            SetData(data);
        }

        public string Phone => data.Phone;

        public void SetData(WalletRowData data)
        {
            this.data = data;
            phoneNumber.Text = data.Phone;
            walletType.Text = Program.Form.GetWalletType(data.Type);
            withdrawal.Text = data.WithdrawalRemaining.ToString();
            deposit.Text = data.DepositRemaining.ToString();
            balance.Text = data.Balance.ToString();
        }

        public void SetPage(int current, int max)
        {
            page.Text = $"{current} / {max}";
        }

        private void Page_MouseClick(object sender, MouseEventArgs e)
        {
            Program.Form.SetPage(e.Button == MouseButtons.Left);
        }

        public void ResetRemaining()
        {
            data.WithdrawalRemaining = data.MaximumWithdrawal;
            data.DepositRemaining = data.MaximumDeposit - data.Balance;
            withdrawal.Text = data.WithdrawalRemaining.ToString();
            deposit.Text = data.DepositRemaining.ToString();
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            float withd = DatabaseHelper.GetWalletsWithdrawal(data.Phone, DateTime.Now.ToStandardString());
            
            float depo = data.Balance + withd;

            string message = "";
            if (withd >= MaxDailyWithdrawal)
                message += $"المحفظة تخطت السحب اليومي المسموح به";
            if (depo >= MaxDailyDeposit)
            {
                message += message == "" ? "المحفظة " : "\nو";
                message += $"تخطت الإيداع اليومي المسموح به";
            }
            if (message != "" && Form1.MessageForm(message, "تنبيه", MessageBoxButtons.OKCancel, MessageBoxIconV2.Warning) != DialogResult.OK)
                return;

            Program.Form.ChooseWalletBtn(data, [withd, depo]);
        }

        private void EditBtn_Click(object sender, EventArgs e)
            => Program.Form.SetWalletData(data);

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            bool res = DatabaseHelper.IsThereRecords(data.Phone);

            if (res && Form1.MessageForm("لا يمكن حذف هذه المحفظة لأنها مرتبطة بعمليات\nهل تريد حذف معاملات هذه المحفظة أولا ؟", "تأكيد حذف المعاملات", MessageBoxButtons.OKCancel, MessageBoxIconV2.Delete) != DialogResult.OK)
                return;

            if (res)
            {
                if (!DatabaseHelper.ResetWallet(data.Phone))
                {
                    Form1.MessageForm("حدث خطأ أثناء حذف عمليات المحفظة. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                    return;
                }
                Program.Form.ResetWallet(data.Phone);
                return;
            }

            if (Form1.MessageForm("هل أنت متأكد من حذف هذه المحفظة ؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIconV2.Delete) != DialogResult.Yes)
                return;

            if (!DatabaseHelper.DeleteWallet(data.Phone))
            {
                Form1.MessageForm("حدث خطأ أثناء حذف المحفظة. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
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
