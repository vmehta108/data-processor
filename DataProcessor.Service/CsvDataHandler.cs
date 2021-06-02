using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace DataProcessor.Service
{
    public class CsvDataHandler : ICsvDataHandler
    {
        #region Private Members

        private readonly IExceptionHandler _exceptionHandler;
        private string _filePath;
        private readonly string _outputPath = "CleanOutput.csv";

        #endregion


        #region Public Properties

        public string OutputFile { get; private set; }

        public long ReportProcessTime()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public CsvDataHandler(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void ValidateData(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessData()
        {
            using var reader = new StreamReader(_filePath);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<OutputRecordMap>();
            csvReader.Read();
            IEnumerable<OutputRecord> records = csvReader.GetRecords<OutputRecord>();

            using var writer = new StreamWriter(_outputPath);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.NextRecord();
            csvWriter.WriteHeader<OutputRecord>();
            csvWriter.NextRecord();

            foreach (OutputRecord outputRecord in records)
            {
                csvWriter.WriteRecord(outputRecord);
                csvWriter.NextRecord();
            }

        }
    }
}