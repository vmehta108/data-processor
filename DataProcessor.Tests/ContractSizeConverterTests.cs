using DataProcessor.Service;
using NUnit.Framework;

namespace DataProcessor.Tests
{
    [TestFixture]
    public class ContractSizeConverterTests
    {
        [Test]
        public void ContractSizeConverter_Should_Parse_And_Extract_Correct_Contract_Size()
        {
            /* Arrange */
            ContractSizeConverter converter = new ContractSizeConverter();
            var sampleData =  ";InstFullName:Wig 20 Index|;InstClassification:FFICSX|;NotionalCurr:PLN|;PriceMultiplier:25.0|;UnderlInstCode:PL9991234567|";
          

            /* Act */
            var contractSize = converter.ConvertFromString(sampleData, null, null);
            

            /* Assert */
            Assert.AreEqual("25.0", contractSize);
        }
    }
}