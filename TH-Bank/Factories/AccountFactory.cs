using System.Security.Principal;

namespace TH_Bank
{
    public class AccountFactory
    {

        public Account CreateAccount(string ownerid, string currency, string userchoice)
        {
            int accountNumber = Format.UniqueAccountNo(userchoice);
            var accountDataHandler = new AccountDataHandler();
            
            if (userchoice == "Salaryaccount")

            {
                Account salAccount = new SalaryAccount(ownerid, accountNumber, currency);                
                accountDataHandler.Save(salAccount);
                return salAccount;
            }
            else if (userchoice == "Savingsaccount")
            {
                Account saveAccount = new SavingsAccount(ownerid, accountNumber, currency);
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
