
namespace ElectronicServices
{
    public class WalletRowData
    {
        public string Phone;
        public float MaximumWithdrawal;
        public float MaximumDeposit;
        public float WithdrawalRemaining;
        public float DepositRemaining;
        public float Balance;
        public int Type;
        public string Comment;

        public override string ToString() => $"{MaximumWithdrawal}, {MaximumDeposit}, {WithdrawalRemaining}, {DepositRemaining}, {Balance}, {Type}, '{Comment}'";
    }
}
