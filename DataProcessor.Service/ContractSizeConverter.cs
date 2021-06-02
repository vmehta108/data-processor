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
           throw new NotImplementedException();
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}