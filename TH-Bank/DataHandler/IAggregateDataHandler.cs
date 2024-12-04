namespace TH_Bank
{
    internal interface IAggregateDataHandler<T> : IObjectHandler<T>
    {
        public List<T> LoadAll();
        public void SaveAll(List<T> saveList);

    }
}
