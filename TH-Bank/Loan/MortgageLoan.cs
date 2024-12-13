namespace TH_Bank
{
    internal class MortgageLoan : Loan
    {

        public override string LoanType { get; } = "Mortgage";

        public MortgageLoan(string id, decimal amount, string start,string expire) : base(id, amount, start, expire)
        {

        }

   
    }
}
