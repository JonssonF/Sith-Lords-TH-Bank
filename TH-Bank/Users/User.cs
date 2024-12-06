namespace TH_Bank
{
    public abstract class User
    {
        public string Id { get; private set; }
        public string PassWord { get; private set; }
        public abstract string UserType { get; }

        public string UserName { get; private set; }

        public bool IsBlocked { get; set; }
        public decimal LoanLimit { get; private set; }

        public User(string id, string userName, string passWord)
        {
            Id = id;
            PassWord = passWord;
            UserName = userName;
            //LoanLimit = SetMaxLoan(); 
        }

        public abstract string ToString();

        public decimal SetMaxLoan()
        {
            decimal maxLoan = 0;
            decimal totalLoans = 0;
            var activeUser = new AccountDataHandler();
            List<Account> accounts = activeUser.LoadAll(Id);
            LoanDataHandler ldh = new LoanDataHandler();
            List<Loan> allLoans = ldh.LoadAll(Id);


            foreach (var loan in allLoans)
            {
                totalLoans += loan.Amount;
            }

            foreach (var acc in accounts)
            {
                if(acc.Currency == "USD")
                {
                    decimal UStoSE = ExchangeCurrency.Exchange(acc.Balance, "USD", "SEK");
                    maxLoan += UStoSE;
                }
                else if (acc.Currency == "EUR")
                {
                    decimal EUtoSE = ExchangeCurrency.Exchange(acc.Balance, "EUR", "SEK");
                    maxLoan += EUtoSE;
                }
                else
                {
                maxLoan += acc.Balance;
                }
            }

            decimal maxLoanAmount = maxLoan * 5;

            totalLoans = totalLoans * 6;

            LoanLimit = maxLoanAmount - totalLoans;

            if(LoanLimit < 0)
            {
                LoanLimit = 0;
            }


            return LoanLimit;
        }
    }
}
