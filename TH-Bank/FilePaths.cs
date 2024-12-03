using System.Transactions;

namespace TH_Bank
{
    public static class FilePaths
    {
        public static string AccountPath { get { return "Accounts.txt"; } }
        public static string UserPath { get { return "Users.txt"; } }
        public static string TransactionPath { get { return "Transactions.txt";  } }
        public static string SystemPath { get { return "System.txt";  } }
        public static string LoanPath { get { return "Loans.txt";  } }
        public static string CurrencyPath { get { return "Currencies.txt";  } }

    }
}
