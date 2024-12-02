namespace TH_Bank
{
    internal class CarLoan : Loan
    {

        public List<Loan> loans;
        public override string LoanType { get; } = "CarLoan";

        public CarLoan(string id, decimal amount, double interest) : base(id, amount, interest)
        {
            loans = new List<Loan>();
        }

        public override string ToString()
        {
            return $"{Id}|{LoanType}|{Amount}|{Interest}|{LoanStart}";
        }

    }
}
