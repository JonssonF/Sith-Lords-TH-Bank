namespace TH_Bank
{
    internal class MortgageLoan : Loan
    {
        public override string LoanType { get; } = "Mortgage";

        public MortgageLoan(string id, decimal amount) : base(id, amount)
        {

        }

        public override string ToString()
        {
            return $"{OwnerId}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }
    }
}
