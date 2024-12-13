namespace TH_Bank
{
    public class LoanFactory
    {


        public Loan NewLoan(string id, decimal amount, string expire, string userChoice)
        {
            var loanDataHandler = new LoanDataHandler();
            var timeNow = DateTime.Now.ToString("yyyy-MM-dd");
            Loan loan = null;

            if(userChoice == "CarLoan")
            {
                loan = new CarLoan(id, amount, timeNow, expire);
                loanDataHandler.Save(loan);
            }
            else if (userChoice == "MortgageLoan")
            {
                loan = new MortgageLoan(id, amount, timeNow, expire);
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
