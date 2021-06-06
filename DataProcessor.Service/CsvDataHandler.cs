using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace DataProcessor.Service
{
    public class CsvDataHandler : ICsvDataHandler
    {

        #region Constructor

        public CsvDataHandler(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
            _stopwatch = new Stopwatch();
        }

        #endregion


        #region Private Members

        private readonly Stopwatch _stopwatch;
        private readonly IExceptionHandler _exceptionHandler;
        private string _filePath;
        private readonly string _outputPath = "CleanOutput.csv";

        #endregion


        #region Public Properties

        public string OutputFile { get; private set; }

        public long ReportProcessTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        #endregion


        #region Public Methods
        
        public void ValidateData(string filePath)
        {
            _stopwatch.Start();
            _filePath = filePath;

            try
            {
                using var reader = new StreamReader(_filePath);

                if (reader.EndOfStream) throw new InvalidDataException("Invalid CSV Data: File Empty");
                int headerCounter = 0;
                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    if(line == null) throw new InvalidDataException("Invalid CSV Data");

                    if (headerCounter == 0) headerCounter = line.Split(',').Length;

                    /* Check for valid number of delimited values */
                    if (line.Split(',').Length != headerCounter)
                        throw new InvalidDataException(
                            "Missing CSV Data - Number of delimited values doesn't match headers.");
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                /* Centralised exception handling */
                _exceptionHandler.HandleException("File not found, Can't proceed without valid input file." , fileNotFoundException);
            }
            catch (Exception exception)
            {
                /* Centralised exception handling */
                _exceptionHandler.HandleException("Errors Encountered." , exception);
            }

        }
        
        /// <summary>
        /// We use deferred execution to promote efficiency. With very big file sizes, this is be 
        /// lot more efficient then using in-memory collections
        /// </summary>
        public void ProcessData()
        {
           
            try
            {
                /* First we will read the data as IEnumerable to leverage deferred execution */
               
                var readers = ReadData();

                WriteData(readers.Item1);


                OutputFile = _outputPath;

                readers.Item1?.Dispose();
                readers.Item2?.Dispose();
            }
            catch (Exception exception)
            {
                /* Centralised exception handling */
                _exceptionHandler.HandleException("Failed processing data", exception);
            }
            
        }

        private Tuple<CsvReader,StreamReader> ReadData()
        {
            var reader = new StreamReader(_filePath);
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<OutputRecordMap>();
            csvReader.Read();
            return Tuple.Create(csvReader,reader);
        }

        private void WriteData(CsvReader csvReader)
        {
            IEnumerable<OutputRecord> records = csvReader.GetRecords<OutputRecord>();

            /* The data is now appended to the out put file */
            using var writer = new StreamWriter(_outputPath);
            CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.NextRecord();
            csvWriter.WriteHeader<OutputRecord>();
            csvWriter.NextRecord();
            
            foreach (OutputRecord outputRecord in records)
            {
                csvWriter.WriteRecord(outputRecord);
                csvWriter.NextRecord();
            }
        }

        /// <summary>
        /// This will validate the data and then perform the ETL process.
        /// </summary>
        /// <param name="filePath"></param>
        public void ExtractData(string filePath)
        {
            ValidateData(filePath);
            ProcessData();
        }

        #endregion
    }
}