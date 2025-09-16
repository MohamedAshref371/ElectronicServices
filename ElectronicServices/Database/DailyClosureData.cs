
namespace ElectronicServices
{
    internal class DailyClosureData
    {
        public string Date;
        public float TotalWallets;
        public float TotalCash;
        public float TotalElectronic;
        public float Credit;
        public float Debit;
        public int PayappClosureId;
        public float Sum => TotalWallets + TotalCash + TotalElectronic + Credit - Debit;
    }
}
