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
        public decimal Amount { get; set; }
        public double Interest { get; set; }
        public string LoanStart { get; private set; }
        public string LoanExp { get; private set; }
        public abstract string LoanType { get; }


        public Loan(string id, decimal amount,string starts, string expire)
        {
            var ldh = new LoanDataHandler();
            Amount = amount;
            OwnerId = id;
            Interest = ldh.GetInterest(LoanType);
            LoanStart = starts;
            LoanExp = expire;
        }

        public override string ToString()
        {

            return $"{OwnerId}|{LoanType}|{Amount}|{Interest}|{LoanStart}|{LoanExp}";
        }

    }
}
