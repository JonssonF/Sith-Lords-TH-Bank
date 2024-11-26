namespace TH_Bank
{
    public class TransactionDataHandler : IDataHandler<Transaction>
    {
        public string FilePath { get; set; }

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
            throw new NotImplementedException();
        }

        public List<Transaction> LoadAll(string userid)
        {
            throw new NotImplementedException();
        }

        public void Save(Transaction saveThis)
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

        public void SaveAll(List<Transaction> saveList)
        {
            throw new NotImplementedException();
        }
    }
}