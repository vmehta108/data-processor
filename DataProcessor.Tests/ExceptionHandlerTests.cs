using System.IO;
using DataProcessor.Service;
using NUnit.Framework;

namespace DataProcessor.Tests
{
    [TestFixture]
    public class ExceptionHandlerTests
    {
        [Test]
        public void ExceptionHandler_Should_Implement_IExceptionHandler()
        {
            ExceptionHandler exceptionHandler = new ExceptionHandler();

            var testInterface =  exceptionHandler as IExceptionHandler;

            Assert.IsNotNull(testInterface);
        }

        
        [Test]
        public void ExceptionHandler_Should_Throw_BubbleUp_Exception()
        {
            /* Arrange */
            ExceptionHandler exceptionHandler = new ExceptionHandler();
           

            /* Act */
            /* Assert */
            Assert.Throws<InvalidDataException>(() =>
            {
                exceptionHandler.HandleException("Test Message", new InvalidDataException());
            });

        }
    }
}