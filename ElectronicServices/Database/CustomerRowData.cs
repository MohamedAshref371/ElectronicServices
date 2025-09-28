
namespace ElectronicServices
{
    public class CustomerRowData
    {
        public int Id;
        public string Name;
        public float Pay;
        public float Take;
        public float Balance => Pay - Take;
    }
}
