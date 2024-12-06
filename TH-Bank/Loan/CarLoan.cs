namespace TH_Bank
{
    internal class CarLoan : Loan
    {
        public override string LoanType { get; } = "CarLoan";

        public CarLoan(string id, decimal amount, string start, string expire) : base(id, amount, start, expire)
        {

        }

    

    }
}
