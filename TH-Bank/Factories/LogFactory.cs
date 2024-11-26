namespace TH_Bank
{
    public abstract class LogFactory
    {
        public abstract Record PerformRecord(LogDataHandler logDataHandler);
        public Record CreateRecord()
        {
            return this.PerformRecord(new LogDataHandler());
        }
    }
}
