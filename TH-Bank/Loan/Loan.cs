namespace TH_Bank
{

    public abstract class Loan
    {

        private decimal _amount;
        private int _toAccount;
        private DateTime _loanStart;
        private string _Id;
        private double _interest;

        public string OwnerId { get; set; }
        //public Account ToAccount { get; set; }
        public decimal Amount { get; set; }
        public double Interest { get; set; }
        public DateTime LoanStart { get; set; }
        public abstract string LoanType { get; }


        public Loan(string id, decimal amount)
        {
            var ldh = new LoanDataHandler();
            Amount = amount;
            OwnerId = id;
            Interest = ldh.GetInterest(LoanType);
            LoanStart = DateTime.Now;
        }
        
        public abstract string ToString();

    }
}
