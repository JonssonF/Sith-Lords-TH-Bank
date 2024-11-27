namespace TH_Bank
{
    public class AccountFactory
    {
        public Account CreateAccount(decimal balance, string currency, int accountNumber, string id)
        {
            Account account = new Account(balance, currency, accountNumber, id);
            var accountDataHandler = new AccountDataHandler();
            accountDataHandler.Save(account);
            return account;
        }
    }
}
