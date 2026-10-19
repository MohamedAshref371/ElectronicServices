
namespace ElectronicServices
{
    public partial class CashCalculatorForm : Form
    {
        public CashCalculatorForm()
        {
            InitializeComponent();
        }

        private void Number_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(one.Text, out int oneCount);
            int.TryParse(five.Text, out int fiveCount);
            int.TryParse(ten.Text, out int tenCount);
            int.TryParse(twenty.Text, out int twentyCount);
            int.TryParse(fifty.Text, out int fiftyCount);
            int.TryParse(hundred.Text, out int hundredCount);
            int.TryParse(twoHundred.Text, out int twoHundredCount);

            result.Text = (oneCount + fiveCount * 5 + tenCount * 10 + twentyCount * 20 + fiftyCount * 50 + hundredCount * 100 + twoHundredCount * 200).ToString();
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            one.Text = "";
            five.Text = "";
            ten.Text = "";
            twenty.Text = "";
            fifty.Text = "";
            hundred.Text = "";
            twoHundred.Text = "";
            result.Text = "0";
        }
    }
}
