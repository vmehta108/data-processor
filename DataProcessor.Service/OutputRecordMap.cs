using CsvHelper.Configuration;

namespace DataProcessor.Service
{
    public class OutputRecordMap : ClassMap<OutputRecord>
    {
        public OutputRecordMap()
        {
            Map(m => m.Isin).Index(0).Name("ISIN");
            Map(m => m.CfiCode).Index(1).Name("CFICode");
            Map(m => m.Venue).Index(2).Name("Venue");
            Map(m => m.AlgoParam).Index(3).Name("AlgoParams").TypeConverter<ContractSizeConverter>();
        }
    }
}