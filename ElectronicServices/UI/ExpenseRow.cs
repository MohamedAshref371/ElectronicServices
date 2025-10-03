using DocumentFormat.OpenXml.Office2010.Excel;
using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class ExpenseRow : UserControl
    {
        public ExpenseRow()
        {
            InitializeComponent();
            editBtn.Visible = false;
            deleteBtn.Visible = false;
        }

        private int id;
        public ExpenseRow(ExpenseRowData data)
        {
            InitializeComponent();
            id = data.Id;
            title.Text = data.Title;
            date.Text = data.Date;
            amount.Text = data.Amount.ToString();
            amount.Tag = (decimal)data.Amount;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (amount.Visible)
            {
                amountEdit.Value = (decimal)amount.Tag;
                amount.Visible = false;
                amountEdit.Visible = true;
                editBtn.Image = Properties.Resources.check_mark_button;
            }
            else
            {
                amount.Visible = true;
                amountEdit.Visible = false;
                editBtn.Image = Properties.Resources.editIcon;

                float val = (float)amountEdit.Value;
                if (!DatabaseHelper.EditExpense(id, val))
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ البيانات\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                amount.Text = val.ToString();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من حذف هذا البند ؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (!DatabaseHelper.DeleteExpense(id))
            {
                MessageBox.Show("حدث خطأ أثناء حذف البند. يرجى المحاولة مرة أخرى.");
                return;
            }

            id = 0;
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

        private void EditBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(editBtn.Width, editBtn.Height);
            Size newSize = new(minSize, minSize);

            editBtn.ImageSize = newSize;
            deleteBtn.ImageSize = newSize;
        }
    }
}
