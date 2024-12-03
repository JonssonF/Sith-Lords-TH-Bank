namespace TH_Bank
{
    internal class MortgageLoan : Loan
    {
        public List<Loan> loans;
        public override string LoanType { get; } = "Mortgage";

        public MortgageLoan(string id, decimal amount) : base(id, amount)
        {
            loans = new List<Loan>();
        }

        public override string ToString()
        {
            return $"{OwnerId}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }
    }
}
