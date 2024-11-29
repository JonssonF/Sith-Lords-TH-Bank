namespace TH_Bank
{

    public class Loan
    {
        private decimal _amount;
        private int _fromAccount;
        private int _toAccount;
        private DateTime _transferDate;

        public decimal Amount { get; set; }
        public Account ToAccount { get; set; }
        public DateTime LoanStart { get; set; }
        public string Id { get; set; }

        public Loan(decimal amount, Account toAccount, string id)
        {
            Amount = amount;
            ToAccount = toAccount;
            Id = id;
            LoanStart = DateTime.Now;
        }
    }
}
