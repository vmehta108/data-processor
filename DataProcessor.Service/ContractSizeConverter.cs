using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using StringExtensions;

namespace DataProcessor.Service
{
    public class ContractSizeConverter  : DefaultTypeConverter
    {
        private const string BeginSearchString = "PriceMultiplier:";
        private const string EndSearchString = "|";

        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            string data = text.Between(BeginSearchString, EndSearchString, StringComparison.InvariantCulture);
            return data;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var data = value;
            return value.ToString();
        }
    }
}