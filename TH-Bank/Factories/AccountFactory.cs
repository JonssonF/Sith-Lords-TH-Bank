using System.Security.Principal;

namespace TH_Bank
{
    public class AccountFactory
    {
        public Account CreateAccount(decimal balance, string currency, int accountNumber, string id)
        {
            var accountDataHandler = new AccountDataHandler();
            var userInput = "";
            if (userInput == "Salaryaccount")
            {
                Account salAccount = new SalaryAccount(balance, currency, accountNumber, id);                
                accountDataHandler.Save(salAccount);
                return salAccount;
            }
            else if (userInput == "Savingsaccount")
            {
                Account saveAccount = new SavingsAccount(balance, currency, accountNumber, id);
                accountDataHandler.Save(saveAccount);
                return saveAccount;
            }
            else
            {
                throw new Exception("Invalid account-type");
            }
            

        }
    }
}
