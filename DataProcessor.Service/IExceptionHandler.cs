using System;

namespace DataProcessor.Service
{
    public interface IExceptionHandler
    {
        void HandleException(string message, Exception exception);
    }
}