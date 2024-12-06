namespace TH_Bank
{ 
    internal interface IMyDataHandler<T> : IObjectHandler<T>
    {
        // All DataHandlers that need to access a certain instance
        // or all instances that belong to another instance use this interface.
        public List<T> LoadAll(string id);
        public T Load(string id);
    }
}
