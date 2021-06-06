using CsvHelper.Configuration.Attributes;

namespace DataProcessor.Service
{
    public class OutputRecord
    {
        [Name("ISIN")] 
        public string Isin { get; set; }
        
        [Name("CFICode")] 
        public string CfiCode { get; set; }
        
        [Name("Venue")] 
        public string Venue { get; set; }
        
        [Name("Contract Size")] 
        public string AlgoParam { get; set; }   
    }
}