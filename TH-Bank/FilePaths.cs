using System.Transactions;

namespace TH_Bank
{
    public static class FilePaths
    {
        public static string AccountPath { get { return "Accounts.txt"; } }
        public static string UserPath { get { return "Users.txt"; } }
        public static string LogPath { get { return "Logs.txt"; } }
        public static string TransactionPath { get { return "Transactions.txt";  } }

        public static void CreateFiles()
        {
            if(!File.Exists(AccountPath))
                File.Create(AccountPath);

            if (!File.Exists(UserPath))
                File.Create(UserPath);

            if (!File.Exists(LogPath))
                File.Create(LogPath);
            
            if (!File.Exists(TransactionPath))
                File.Create(TransactionPath);
        }
    }
}
