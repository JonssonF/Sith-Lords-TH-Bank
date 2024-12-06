namespace TH_Bank
{

    public abstract class Loan
    {
        public string OwnerId { get; private set; }
        public decimal Amount { get; private set; }
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
