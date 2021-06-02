using System;

namespace DataProcessor.Service
{
    public class ExceptionHandler : IExceptionHandler
    {
        public void HandleException(string message, Exception exception)
        {
            /* Opportunity to log the exception, generate alerts etc.*/
            Console.WriteLine(message);
           
            throw exception;
        }
    }
}