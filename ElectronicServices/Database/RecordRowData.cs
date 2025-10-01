
namespace ElectronicServices
{
    public class RecordRowData
    {
        public string Phone;
        public string Date;
        public float WithdrawalRemaining;
        public float DepositRemaining;
        public float Withdrawal;
        public float Deposit;
        public float Balance;
        public string Comment;

        public override string ToString() => $"'{Phone}', '{Date}', {WithdrawalRemaining}, {DepositRemaining}, {Withdrawal}, {Deposit}, {Balance}, '{Comment}'";
    }
}
