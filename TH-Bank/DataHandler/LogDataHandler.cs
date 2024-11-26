namespace TH_Bank
{
    public class LogDataHandler : IDataHandler<Record>
    {
        public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Delete(Record deleteThis)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SaveAll(List<Record> saveList)
        {
            throw new NotImplementedException();
        }
    }
}
