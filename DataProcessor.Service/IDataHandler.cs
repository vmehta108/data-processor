namespace DataProcessor.Service
{
    public interface IDataHandler
    {
        void ValidateData(string filePath);
        void ProcessData();
    }
}