namespace TH_Bank
{

    public abstract class Loan
    {

        private decimal _amount;
        private int _toAccount;
        private DateTime _transferDate;
        private string _Id;
        private double _interest;

        public string Id { get; set; }
        //public Account ToAccount { get; set; }
        public decimal Amount { get; set; }
        public double Interest { get; set; }
        public DateTime LoanStart { get; set; }
        public abstract string LoanType { get; }


        public Loan(string id, decimal amount, double interest)
        {
            Amount = amount;
            Id = id;
            Interest = interest;
            LoanStart = DateTime.Now;
        }
        
        public abstract string ToString();

    }
}
