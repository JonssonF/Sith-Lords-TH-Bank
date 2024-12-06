namespace TH_Bank
{

    public abstract class Loan
    {
        public string OwnerId { get; private set; }
        public decimal Amount { get; private set; }
        public double Interest { get; set; }
        public DateTime LoanStart { get; private set; }
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
