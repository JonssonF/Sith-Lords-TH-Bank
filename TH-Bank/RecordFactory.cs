namespace TH_Bank
{
    public abstract class RecordFactory
    {
        public abstract Record PerformRecord(RecordDataHandler logDataHandler);
        public Record CreateRecord()
        {
            return this.PerformRecord(new RecordDataHandler());
        }
    }
}
