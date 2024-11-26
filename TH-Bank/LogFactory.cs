namespace TH_Bank
{
    public abstract class LogFactory
    {
        public abstract Log PerformLog(LogDataHandler logDataHandler);
        public Log CreateLog()
        {
            return this.PerformLog(new LogDataHandler());
        }
    }
}
