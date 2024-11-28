namespace TH_Bank
{
    internal class SalaryAccount : Account
    {
        public override string AccountType { get; } = "SalaryAccount";
        public SalaryAccount(string ownerID, int accountNumber, decimal balance, string currency)
            : base(ownerID, accountNumber, balance, currency)
        {
            Interest = GetInterest();
        }

        public override decimal GetInterest()
        {
            return 1;
        }

    }
}
