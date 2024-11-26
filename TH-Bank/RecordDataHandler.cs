namespace TH_Bank
{
    public class RecordDataHandler : IDataHandler<Record>
    {
        public string FilePath { get; set; }

        public RecordDataHandler()
        {
            FilePath = FilePaths.LogPath;
        }

        public void Delete(Record deleteThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            int deleteRow = Array.IndexOf(openFile, deleteThis.ToString());

            for (int i = deleteRow + 1; i<openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i]; 
            }

            File.WriteAllLines(FilePath, openFile);
        }

        public Record Load()
        {
            throw new NotImplementedException();
        }

        public Record Load(string id)
        {
            throw new NotImplementedException();
        }

        public List<Record> LoadAll()
        {
            throw new NotImplementedException();
        }

        public List<Record> LoadAll(string userid)
        {
            throw new NotImplementedException();
        }

        public void Save(Record saveThis)
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

        public void SaveAll(List<Record> saveList)
        {
            throw new NotImplementedException();
        }
    }
}
