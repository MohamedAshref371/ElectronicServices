using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class RecordRow : UserControl
    {
        public RecordRow()
        {
            InitializeComponent();
        }

        public RecordRow(RecordRowData data)
        {
            InitializeComponent();
            SetData(data);
        }

        private string comment;
        public void SetData(RecordRowData data)
        {
            phoneNumber.Text = data.Phone;
            date.Text = data.Date[..10];
            date.Tag = data.Date[11..];
            withdRema.Text = data.WithdrawalRemaining.ToString();
            depoRema.Text = data.DepositRemaining.ToString();
            withdrawal.Text = data.Withdrawal.ToString();
            deposit.Text = data.Deposit.ToString();
            balance.Text = data.Balance.ToString();
            comment = data.Comment;
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

        private void PhoneNumber_DoubleClick(object sender, EventArgs e)
        {
            if (comment != null && comment != "")
                Form1.MessageForm(comment, "ملاحظات العملية", MessageBoxButtons.OK, MessageBoxIconV2.Information);
        }

        private void Date_DoubleClick(object sender, EventArgs e)
        {
            if (date.Tag is string dt && dt != null)
                Form1.MessageForm(dt, "وقت العملية", MessageBoxButtons.OK, MessageBoxIconV2.Information);
        }
    }
}
