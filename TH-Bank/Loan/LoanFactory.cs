namespace TH_Bank
{
    public class LoanFactory
    {


        public Loan NewLoan(string id, decimal amount, double interest,string userChoice)
        {
            var loanDataHandler = new LoanDataHandler();

            Loan loan = null;

            if(userChoice == "Carloan")
            {
                loan = new CarLoan(id, amount, interest);
                loanDataHandler.Save(loan);
            }
            else if (userChoice == "Mortgage")
            {
                loan = new MortgageLoan(id, amount, interest);
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
