namespace TH_Bank
{
    internal class CarLoan : Loan
    {
        public override string LoanType { get; } = "CarLoan";

        public CarLoan(string id, decimal amount) : base(id, amount)
        {

        }

        public override string ToString()
        {
            return $"{OwnerId}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }

    }
}
