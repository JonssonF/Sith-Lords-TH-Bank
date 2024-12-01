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
            var allLoans = new List<Loan>();

            foreach(var loan in allLoans)
            {
                Console.WriteLine(loan);
            }
            //return allLoans;
            throw new NotImplementedException();
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
            string[] openFile = File.ReadAllLines(FilePath);
            if (!openFile.Contains(saveThis.Id))
            {
                openFile.Append(saveThis.ToString());
            }
            else
            {
                int overwrite = Array.IndexOf(openFile, saveThis.ToString());
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
