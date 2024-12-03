namespace TH_Bank
{
    internal class CarLoan : Loan
    {

        public List<Loan> loans;
        public override string LoanType { get; } = "CarLoan";

        public CarLoan(string id, decimal amount) : base(id, amount)
        {
            loans = new List<Loan>();
        }

        public override string ToString()
        {
            return $"{OwnerId}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }

    }
}
