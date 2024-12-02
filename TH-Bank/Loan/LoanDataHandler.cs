namespace TH_Bank
{
    public class LoanDataHandler : IDataHandler<Loan>
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
                if (line.Contains(id) && line.Contains("Carloan"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    decimal amount = Decimal.Parse(variables[1]);
                    double interest = Double.Parse(variables[2]);
                    var carLoan = new CarLoan(userId, amount, interest);
                    return carLoan;
                }
                else if (line.Contains(id) && line.Contains("Mortgage"))
                {
                    string[] variables = line.Split('|');
                    string userId = variables[0];
                    decimal amount = Decimal.Parse(variables[1]);
                    double interest = Double.Parse(variables[2]);
                    var mortgageLoan = new MortgageLoan(userId, amount, interest);
                    return mortgageLoan;
                }
            }
            throw new Exception("Invalid loan type.");
        }

        public List<Loan> LoadAll()
        {
            throw new NotImplementedException();
        }

        public List<Loan> LoadAll(string userid)
        {
            throw new NotImplementedException();
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
