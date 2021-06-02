using System;
using System.IO;
using DataProcessor.Service;

namespace DataProcessor.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /* Parse command line params */
                var filepath = args[0];

                /* Raise exception for invalid file */
                if(string.IsNullOrEmpty(filepath)) throw new FileNotFoundException("Invalid File Name");

                /* This would typically be resolved from container */
                IExceptionHandler exceptionHandler = new ExceptionHandler();

                /* Passing the dependency to CsvDataHandler */
                CsvDataHandler csvDataHandler = new CsvDataHandler(exceptionHandler);

                /* Process the data */
                csvDataHandler.ExtractData(filepath);

                /* Print the output file name and the processing time. */
                Console.WriteLine($"Data extracted in {csvDataHandler.ReportProcessTime()} milliseconds.");
                Console.WriteLine($"Output File: {Path.GetFullPath(csvDataHandler.OutputFile)}");

            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Console.WriteLine(fileNotFoundException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Read();
        }
    }
}
