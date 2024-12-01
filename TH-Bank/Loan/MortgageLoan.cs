namespace TH_Bank
{
    internal class MortgageLoan : Loan
    {
        public List<Account> accounts;
        public override string LoanType { get; } = "Mortgage";

        public MortgageLoan(string id, decimal amount, double interest) : base(id, amount, interest)
        {
            accounts = new List<Account>();
        }

        public override string ToString()
        {
            return $"{Id}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }
    }
}
