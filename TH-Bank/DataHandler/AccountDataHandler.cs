using System.Security.Principal;


namespace TH_Bank
{
    public class AccountDataHandler : IMyDataHandler<Account>,IAggregateDataHandler<Account>
    {
        public string FilePath { get; set; }

        public AccountDataHandler()
        {
            FilePath = FilePaths.AccountPath;
        }

        public void Delete(Account deleteThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            // Finds the row that contains the account to be deleted.
            int deleterow = Array.IndexOf(openFile, deleteThis.ToString());

            // Loops trough accounts starting at deleted row, and shifts them one to the left,
            // writing over the deleted account info.
            for (int i = deleterow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }

            File.WriteAllLines(FilePath, openFile);


        }

        public Account Load(string accountNumber)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            foreach (string line in openFile)
            {
                // Makes sure the account that is put in the list is of correct type
                // Is the same in several methods, could probably be aggregated.
                if (line.Contains("SalaryAccount") && line.Length > 19 && line.Contains(accountNumber))
                {
                    string[] variables = line.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];

                    return new SalaryAccount(ownerid, balance, accountnumber, currency);
                }
                else if (line.Contains("SavingsAccount") && line.Length > 19 && line.Contains(accountNumber))
                {
                    string[] variables = line.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];
                    return new SavingsAccount(ownerid, balance, accountnumber, currency);
                }
            }
            throw new Exception("Invalid Account type, can't be Loaded");
        }
                

        public List<Account> LoadAll()
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var accounts = new List<Account>();

            foreach (string line in openFile)
            {
                
                if (line.Contains("SalaryAccount") && line.Length > 19)
                {
                    string[] variables = line.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];

                    accounts.Add(new SalaryAccount(ownerid, balance, accountnumber, currency));
                }
                else if (line.Contains("SavingsAccount") && line.Length > 19)
                {
                    string[] variables = line.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];
                    accounts.Add(new SavingsAccount(ownerid,balance, accountnumber, currency));
                }
            }

            return accounts;
        }

        public List<Account> LoadAll(string userid)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var accounts = new List<Account>();

            foreach (string lines in openFile)
            {
                if (lines.Contains(userid) && lines.Contains("SalaryAccount"))
                {
                    string[] variables = lines.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];

                    accounts.Add(new SalaryAccount(ownerid, balance, accountnumber, currency));
                }
                else if (lines.Contains(userid) && lines.Contains("SavingsAccount"))
                {
                    string[] variables = lines.Split('|');

                    string ownerid = variables[0];
                    int accountnumber = Int32.Parse(variables[1]);
                    decimal balance = decimal.Parse(variables[2]);
                    string currency = variables[3];

                    accounts.Add(new SavingsAccount(ownerid, balance, accountnumber, currency));
                }
            }
            return accounts;
        }

        public void Save(Account saveThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            string x = Array.Find(openFile, y => y.Contains(saveThis.AccountNumber.ToString()));

            if (x == null)
            {
                openFile = openFile.Append(saveThis.ToString()).ToArray();

            }
            else
            {
                openFile[Array.IndexOf(openFile, Array.Find(openFile, y => y.Contains(saveThis.AccountNumber.ToString())))] = saveThis.ToString();
            }

            File.WriteAllLines(FilePath, openFile);
        }

        public void SaveAll(List<Account> saveList)
        {
            throw new NotImplementedException();
        }

        // Gets the interest rate of an account type.
        public double GetInterest(string accountType)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            int startindex = Array.IndexOf(openFile, "///INTERESTRATES///");
            int endindex = Array.IndexOf(openFile, "///ENDINTERESTRATES///");

            for(int i = startindex; i < endindex; i++)
            {
                if (openFile[i].Contains(accountType))
                {
                    string[] split = openFile[i].Split('|');

                    return double.Parse(split[1]);
                }
            }

            throw new Exception("Account Type not found among interest rates!");
        }
    }
}
