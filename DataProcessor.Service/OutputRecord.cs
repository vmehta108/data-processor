namespace DataProcessor.Service
{
    public class OutputRecord
    {
        [CsvHelper.Configuration.Attributes.Name("ISIN")] 
        public string Isin { get; set; }
        
        [CsvHelper.Configuration.Attributes.Name("CFICode")] 
        public string CfiCode { get; set; }
        
        [CsvHelper.Configuration.Attributes.Name("Venue")] 
        public string Venue { get; set; }
        
        [CsvHelper.Configuration.Attributes.Name("Contract Size")] 
        public string AlgoParam { get; set; }   
    }
}