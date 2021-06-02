using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
            _filePath = filePath;

            using var reader = new StreamReader(_filePath);

            if (reader.EndOfStream) throw new InvalidDataException("Invalid CSV Data: File Empty");
            int headerCounter = 0;
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                if (headerCounter == 0) headerCounter = line.Split(',').Length;

                /* Check for delimiters */
                var foundDelimiter = line.Any(x => x == ',');
                if (!foundDelimiter) throw new InvalidDataException("Invalid CSV Data: Delimiter Missing");

                /* Check for valid number of delimited values */
                if (line.Split(',').Length != headerCounter)
                    throw new InvalidDataException(
                        "Missing CSV Data - Number of delimited values doesn't match headers.");
            }


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
            OutputFile = _outputPath;
        }
    }
}