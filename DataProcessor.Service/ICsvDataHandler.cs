namespace DataProcessor.Service
{
    public interface ICsvDataHandler : IDataHandler
    {
        string OutputFile { get; }
        long ReportProcessTime();
    }
}