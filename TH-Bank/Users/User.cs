namespace TH_Bank
{
    public abstract class User
    {
        private string _id;
        private string _passWord;


        public string Id { get; set; }
        public string PassWord { get; set; }
        public abstract string UserType { get; }
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }

        public bool IsBlocked { get; set; }

        public decimal LoanLimit { get; private set; }
        public User(string id, string userName, string passWord)
        {
            Id = id;
            PassWord = passWord;
            IsLoggedIn = false;
            UserName = userName;
            LoanLimit = SetMaxLoan(); 
        }

        public abstract string ToString();

        public decimal SetMaxLoan()
        {
            
            decimal maxLoan = 0;
            var activeUser = new AccountDataHandler();
            List<Account> accounts = activeUser.LoadAll(Id);
            
            
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

            return maxLoanAmount;
        }

        public decimal MaxLoanDecrease(decimal decreasedLoan)
        {
            decreasedLoan -= LoanLimit;

            return decreasedLoan;
        }
    }
}
