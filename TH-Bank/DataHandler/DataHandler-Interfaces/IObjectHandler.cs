namespace TH_Bank
{
    internal interface IObjectHandler<T>
    {
        // Base interface, all datahandlers that handle objects
        // inherit from this.
        public string FilePath { get; set; }
        public void Save(T t);
        public void Delete(T t);
    }
}
