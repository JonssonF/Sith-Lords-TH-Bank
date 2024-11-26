
namespace TH_Bank
{
    public class Record
    {
        DateTime _timeStamp;
        private decimal _amount;
        private int _fromAccount;
        private int _toAccount;

        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public string Id { get; set; }

        public Record(decimal amount, int fromAccount, int toAccount, string id)
        {
            TimeStamp = DateTime.Now;
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Id = id;
        }

        public override string ToString()
        {
            return $"{TimeStamp}|{Amount}|{FromAccount}|{ToAccount}";
        }
    }
}
