namespace TH_Bank
{
    public class LoanDataHandler : IMyDataHandler<Loan>, IAggregateDataHandler<Loan>
    {
        public string FilePath { get; set; }

        public LoanDataHandler()
        {
            FilePath = FilePaths.LoanPath;
        }

        public Loan Load()
        {
            throw new NotImplementedException();
        }

        public Loan Load(string id)
        {
            string[] openFile = File.ReadAllLines(FilePath);


            foreach (string line in openFile)
            {
                if (line.Contains(id) && line.Contains("CarLoan"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    string name = variables[1];
                    decimal amount = Decimal.Parse(variables[2]);
                    double interest = Double.Parse(variables[3]);
                    string loanStart = variables[4];
                    var carLoan = new CarLoan(userId, amount, interest);
                    return carLoan;
                }
                else if (line.Contains(id) && line.Contains("Mortgage"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    string name = variables[1];
                    decimal amount = Decimal.Parse(variables[2]);
                    double interest = Double.Parse(variables[3]);
                    string loanStart = variables[4];
                    var mortgageLoan = new MortgageLoan(userId, amount, interest);
                    return mortgageLoan;
                }
            }
            throw new NotImplementedException("Är detta fel i Load string ID?");

        }

        public List<Loan> LoadAll()
        {
            
            throw new NotImplementedException();
        }

        public List<Loan> LoadAll(string userid)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var allLoans = new List<Loan>();

            foreach (string line in openFile)
            {
                if (line.Contains(userid) && line.Contains("CarLoan"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    string name = variables[1];
                    decimal amount = Decimal.Parse(variables[2]);
                    double interest = Double.Parse(variables[3]);
                    string loanStart = variables[4];
                    var loan = new CarLoan(userId, amount, interest);
                    allLoans.Add(loan);
                }
                else if (line.Contains(userid) && line.Contains("Mortgage"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    string name = variables[1];
                    decimal amount = Decimal.Parse(variables[2]);
                    double interest = Double.Parse(variables[3]);
                    string loanStart = variables[4];
                    var loan = new MortgageLoan(userId, amount, interest);
                    allLoans.Add(loan);
                }
            }
            return allLoans;
        }

        public void Delete(Loan deleteThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            int deleteRow = Array.IndexOf(openFile, deleteThis.ToString());
            for (int i = deleteRow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }
            File.WriteAllLines(FilePath, openFile);
        }

        public void Save(Loan saveThis)
        {
            List<string> openFile = File.ReadAllLines(FilePath).ToList();
            if (!openFile.Contains(saveThis.ToString()))
            {
                openFile.Add(saveThis.ToString());
            }
            else
            {
                int overwrite = openFile.IndexOf(saveThis.ToString());
                openFile[overwrite] = saveThis.ToString();
            }
            File.WriteAllLines(FilePath, openFile);
        }

        public void SaveAll(List<Loan> saveList)
        {
            throw new NotImplementedException();
        }
    }
}
