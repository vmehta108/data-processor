using System;
using System.IO;
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

        [Test]
        public void ICsvDataHandler_Should_Implement_IDataHandler()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            CsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);

            var csvDataHandlerInterface = csvDataHandler as ICsvDataHandler;

            Assert.IsNotNull(csvDataHandlerInterface);

        }

        [Test]
        public void CsvDataHandler_Should_Process_Correct_No_Of_Records()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            ICsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);


            /* Act */
            csvDataHandler.ValidateData("ValidTestData.csv");
            csvDataHandler.ProcessData();
            var outputFile = csvDataHandler.OutputFile;

            int counter=-1; /* First line is for header */
            using var reader = new StreamReader(outputFile);
            
            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                counter++;
            }

            /* Assert */
            Assert.AreEqual(3, counter);
        }


        [Test]
        public void CsvDataHandler_Should_Throw_Exception_For_Invalid_CsvFile_NoData()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            CsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);
            mockExceptionHandlerMock
                .Setup(e => e.HandleException(It.IsAny<string>(), It.IsAny<Exception>()))
                .Throws(new InvalidDataException());

            /* Act */
            /* Assert */
            Assert.Throws<InvalidDataException>(() =>
            {
                csvDataHandler.ValidateData("InvalidTestData_EmptyFile.csv");
                csvDataHandler.ProcessData();
            });
            
        }

     

        [Test]
        public void CsvDataHandler_Should_Throw_Exception_For_Invalid_CsvFile_Incorrect_Values()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            CsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);
            mockExceptionHandlerMock
                .Setup(e => e.HandleException(It.IsAny<string>(), It.IsAny<Exception>()))
                .Throws(new InvalidDataException());

            /* Act */
            /* Assert */
            Assert.Throws<InvalidDataException>(() =>
            {
                csvDataHandler.ValidateData("InvalidTestData_IncorrectValues.csv");
                csvDataHandler.ProcessData();
            });
           
        }


        [Test]
        public void CsvDataHandler_Should_Throw_Exception_For_Invalid_CsvFile_MissingDelimiter()
        {
            /* Arrange */
            Mock<IExceptionHandler> mockExceptionHandlerMock = new Mock<IExceptionHandler>();
            CsvDataHandler csvDataHandler = new CsvDataHandler(mockExceptionHandlerMock.Object);
            mockExceptionHandlerMock
                .Setup(e => e.HandleException(It.IsAny<string>(), It.IsAny<Exception>()))
                .Throws(new InvalidDataException());

            /* Act */
            /* Assert */
            Assert.Throws<InvalidDataException>(() =>
            {
                csvDataHandler.ValidateData("InvalidTestData_MissingDelimiter.csv");
                csvDataHandler.ProcessData();
            });
            
        }
    }
}