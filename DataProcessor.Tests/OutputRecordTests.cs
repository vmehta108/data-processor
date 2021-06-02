using System.Linq;
using System.Reflection;
using DataProcessor.Service;
using NUnit.Framework;

namespace DataProcessor.Tests
{
    [TestFixture]
    public class OutputRecordTests
    {
        [Test]
        public void OutputRecord_Should_Have_Necessary_Properties()
        {
            /* Arrange */
            OutputRecord outputRecord = new OutputRecord();

            /* Act */
            PropertyInfo[] propertyInfos = outputRecord.GetType().GetProperties();
            
            /* Assert */
            Assert.AreEqual(4, propertyInfos.Length);
            Assert.IsTrue(propertyInfos.Any(p => p.Name == "Isin"));
            Assert.IsTrue(propertyInfos.Any(p => p.Name == "CfiCode"));
            Assert.IsTrue(propertyInfos.Any(p => p.Name == "Venue"));
            Assert.IsTrue(propertyInfos.Any(p => p.Name == "AlgoParam"));
        }
    }
}