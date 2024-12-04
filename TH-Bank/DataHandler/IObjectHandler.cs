namespace TH_Bank
{
    internal interface IObjectHandler<T>
    {

        public string FilePath { get; set; }
        public void Save(T t);
        public void Delete(T t);
    }
}
