namespace DataProcessor.Service
{
    public class CsvDataHandler : ICsvDataHandler
    {
        #region Private Members

        private readonly IExceptionHandler _exceptionHandler;

        #endregion


        #region Public Properties

        public string OutputFile { get; private set; }

        public long ReportProcessTime()
        {
            throw new System.NotImplementedException();
        }

        #endregion

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