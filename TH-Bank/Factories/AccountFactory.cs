using System.Security.Principal;

namespace TH_Bank
{
    public class AccountFactory
    {

        public Account CreateAccount(string ownerid, decimal balance, string currency, string userchoice)
        {
            int accountNumber = Format.UniqueAccountNo(userchoice);
            var accountDataHandler = new AccountDataHandler();
            
            if (userchoice == "SalaryAccount")

            {
                Account salAccount = new SalaryAccount(ownerid, balance, accountNumber, currency);                
                accountDataHandler.Save(salAccount);
                return salAccount;
            }
            else if (userchoice == "SavingsAccount")
            {
                Account saveAccount = new SavingsAccount(ownerid, balance, accountNumber, currency);
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
