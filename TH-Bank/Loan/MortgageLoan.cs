namespace TH_Bank
{
    internal class MortgageLoan : Loan
    {
        public List<Loan> loans;
        public override string LoanType { get; } = "Mortgage";

        public MortgageLoan(string id, decimal amount, double interest) : base(id, amount, interest)
        {
            loans = new List<Loan>();
        }

        public override string ToString()
        {
            return $"{Id}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }
    }
}
