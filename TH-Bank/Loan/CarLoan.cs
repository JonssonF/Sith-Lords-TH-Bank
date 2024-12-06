namespace TH_Bank
{
    internal class CarLoan : Loan
    {

        public List<Loan> loans;
        public override string LoanType { get; } = "CarLoan";

        public CarLoan(string id, decimal amount, string start, string expire) : base(id, amount, start, expire)
        {
            loans = new List<Loan>();
        }

    

    }
}
