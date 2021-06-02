using DataProcessor.Service;
using Moq;
using NUnit.Framework;

namespace DataProcessor.Tests
{
    [TestFixture]
    public class CsvDataHandlerTests
    {
        
        [Test]
        public void CsvDataHandler_Should_Implement_IDataHandler()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            CsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);

            var csvDataHandlerInterface =  csvDataHandler as IDataHandler;

            Assert.IsNotNull(csvDataHandlerInterface);

        }
    }
}