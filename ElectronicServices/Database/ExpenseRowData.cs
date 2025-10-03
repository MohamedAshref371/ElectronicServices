
namespace ElectronicServices
{
    public class ExpenseRowData
    {
        public int Id;
        public string Date;
        public string Title;
        public float Amount;
        public string Attachment;
        public string Comment;

        public override string ToString()
            => $"'{Date}', '{Title}', {Amount}, '{Attachment}', '{Comment}'";
    }
}
