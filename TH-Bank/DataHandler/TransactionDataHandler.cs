using Microsoft.VisualBasic;
using System.Security.Principal;

namespace TH_Bank
{
    public class TransactionDataHandler : IMyDataHandler<Transaction>, IAggregateDataHandler<Transaction>
    {
        public string FilePath { get; set; }

        public TransactionDataHandler()
        {
            FilePath = FilePaths.TransactionPath;
        }

        public void Delete(Transaction deleteThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            int deleteRow = Array.IndexOf(openFile, deleteThis.ToString());
            for (int i = deleteRow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }
            File.WriteAllLines(FilePath, openFile);
        }

        public Transaction Load()
        {
            throw new NotImplementedException();
        }

        public Transaction Load(string id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> LoadAll()
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var transactions = new List<Transaction>();
            var accountDataHandler = new AccountDataHandler();
            foreach (string line in openFile)
            {
                    string[] variables = line.Split('|');
                    string id = variables[0];
                    string dateTime = variables[1];
                    decimal amount = Decimal.Parse(variables[2]);
                    string fromAccountNumber = variables[3];
                    string toAccountNumber = variables[4];

                Account fromAccount = accountDataHandler.Load(fromAccountNumber);
                Account toAccount = accountDataHandler.Load(toAccountNumber);

                    transactions.Add(new Transaction(id, dateTime, amount, fromAccount, toAccount));

            }

            return transactions;
        }

        public List<Transaction> LoadAll(string userid)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var transactions = new List<Transaction>();
            var accountDataHandler = new AccountDataHandler();

            foreach (string line in openFile)
            {
                if(line.Contains(userid))
                {
                string[] variables = line.Split('|');
                string id = variables[0];
                string dateTime = variables[1];
                decimal amount = Decimal.Parse(variables[2]);
                string fromAccountNumber = variables[3];
                string toAccountNumber = variables[4];

                Account fromAccount = accountDataHandler.Load(fromAccountNumber);
                Account toAccount = accountDataHandler.Load(toAccountNumber);

                transactions.Add(new Transaction(id, dateTime, amount, fromAccount, toAccount));

                }
            }

            return transactions;
        }

        public void Save(Transaction saveThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            if (!openFile.Contains(saveThis.Id))
            {
                openFile = openFile.Append(saveThis.ToString()).ToArray();
            }
            else
            {
                int overwrite = Array.IndexOf(openFile, saveThis.ToString());
                openFile[overwrite] = saveThis.ToString();
            }
            File.WriteAllLines(FilePath, openFile);
        }

        public void SaveAll(List<Transaction> saveList)
        {
            throw new NotImplementedException();
        }
    }
}