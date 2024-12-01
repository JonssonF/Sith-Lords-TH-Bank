namespace TH_Bank
{
    public class LoanFactory
    {


        public Loan NewLoan(string id, decimal amount, double interest,string userChoice)
        {
            var loanDataHandler = new LoanDataHandler();

            if(userChoice == "Carloan")
            {
                Loan carLoan = new CarLoan(id, amount, interest);
                loanDataHandler.Save(carLoan);
                return carLoan;
            }
            else if (userChoice == "Mortgage")
            {
                Loan mortLoan = new MortgageLoan(id, amount, interest);
                loanDataHandler.Save(mortLoan);
                return mortLoan;
            }
            else
            {
                throw new Exception("Invalid account-type");
            }

            
        }


    }
}
