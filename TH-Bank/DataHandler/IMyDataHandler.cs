namespace TH_Bank
{ 
    internal interface IMyDataHandler<T> : IObjectHandler<T>
    {
        public List<T> LoadAll(string id);
        public T Load(string id);
    }
}
