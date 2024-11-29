namespace TH_Bank
{
    public class LoanFactory
    {
        public Loan NewLoan(decimal Amount, Account ToAccount, string Id)
        {
            Loan loan = new Loan(Amount, ToAccount, Id);
            var loanDataHandler = new LoanDataHandler();
            loanDataHandler.Save(loan);
            return loan;
        }
    }
}
