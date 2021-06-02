namespace DataProcessor.Service
{
    public class CsvDataHandler : ICsvDataHandler
    {
        private readonly IExceptionHandler _exceptionHandler;

        public CsvDataHandler(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void ValidateData(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessData()
        {
            throw new System.NotImplementedException();
        }
    }
}