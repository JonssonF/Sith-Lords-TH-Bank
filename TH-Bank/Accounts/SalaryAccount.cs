namespace TH_Bank
{
    internal class SalaryAccount : Account
    {
        public override string AccountType { get; } = "SalaryAccount";
        public SalaryAccount(string ownerID, decimal balance, int accountNumber, string currency)
            : base(ownerID, balance, accountNumber, currency)
        {
           
        }
    }
}
