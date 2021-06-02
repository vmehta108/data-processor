using System;

namespace DataProcessor.Service
{
    public class ExceptionHandler : IExceptionHandler
    {
        public void HandleException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}