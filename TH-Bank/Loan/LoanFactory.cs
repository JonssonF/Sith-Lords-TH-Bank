namespace TH_Bank
{
    public class LoanFactory
    {


        public Loan NewLoan(string id, decimal amount,string userChoice)
        {
            var loanDataHandler = new LoanDataHandler();

            Loan loan = null;

            if(userChoice == "CarLoan")
            {
                loan = new CarLoan(id, amount);
                loanDataHandler.Save(loan);
            }
            else if (userChoice == "MortgageLoan")
            {
                loan = new MortgageLoan(id, amount);
                loanDataHandler.Save(loan);
            }
            else
            {
                throw new Exception("Invalid loan-type");
            }

            return loan;
        }


    }
}
