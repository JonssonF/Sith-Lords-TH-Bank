using System.Transactions;

namespace TH_Bank
{
    public static class FilePaths
    {
        public static string AccountPath { get { return "Accounts.txt"; } }
        public static string UserPath { get { return "Users.txt"; } }
        public static string LogPath { get { return "Logs.txt"; } }
        public static string TransactionPath { get { return "Transactions.txt";  } }
        public static string SystemPath { get { return "System.txt";  } }

    }
}
