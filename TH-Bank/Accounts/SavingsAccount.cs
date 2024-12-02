namespace TH_Bank
{
    internal class SavingsAccount : Account
    {
        public override string AccountType { get; } = "SavingsAccount";

        public SavingsAccount(string ownerID, decimal balance, int accountNumber, string currency)
            : base(ownerID, balance, accountNumber, currency)
        {
            Interest = GetInterest();
        }

        public override decimal GetInterest()
        {
            return 1;
        }
    }
}
