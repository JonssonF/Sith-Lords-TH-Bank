namespace TH_Bank
{
    internal interface IAggregateDataHandler<T> : IObjectHandler<T>
    {
        // All Datahandlers that needs to load or save all instances
        // of a certain object use this interface
        public List<T> LoadAll();
        public void SaveAll(List<T> saveList);

    }
}
