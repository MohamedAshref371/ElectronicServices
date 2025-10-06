
namespace ElectronicServices
{
    public partial class MessageForm : Form
    {
        public MessageForm(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIconV2 icon = MessageBoxIconV2.None)
        {
            InitializeComponent();
            this.text.Text = text;
            this.Text = caption == "" ? "رسالة" : caption;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    yesBtn.DialogResult = DialogResult.OK;
                    yesBtn.Text = "موافق";
                    noBtn.Visible = false;
                    cancelBtn.Visible = false;
                    break;

                case MessageBoxButtons.OKCancel:
                    yesBtn.DialogResult = DialogResult.OK;
                    yesBtn.Text = "موافق";
                    noBtn.Visible = false;
                    cancelBtn.Location = noBtn.Location;
                    break;

                case MessageBoxButtons.YesNo:
                    CancelButton = noBtn;
                    cancelBtn.Visible = false;
                    break;
            }

            switch (icon)
            {
                case MessageBoxIconV2.Error:
                    iconBox.BackgroundImage = Properties.Resources.error;
                    break;

                case MessageBoxIconV2.Question:
                    iconBox.BackgroundImage = Properties.Resources.question;
                    break;

                case MessageBoxIconV2.Warning:
                    iconBox.BackgroundImage = Properties.Resources.warning;
                    break;

                case MessageBoxIconV2.Information:
                    iconBox.BackgroundImage = Properties.Resources.information;
                    break;

                case MessageBoxIconV2.Correct:
                    iconBox.BackgroundImage = Properties.Resources.correct;
                    break;

                case MessageBoxIconV2.Delete:
                    iconBox.BackgroundImage = Properties.Resources.delete;
                    break;

                case MessageBoxIconV2.None:
                default:
                    Size = new Size(Size.Width - iconBox.Size.Width - 10, Size.Height);
                    break;
            }
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {

        }
    }
}
