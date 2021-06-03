## Performs ETL on csv data. 

### DataProcessor.UnitTests
##### We have used two external libraries - NUnit (for unit test framework) & MOQ (for mocking)
##### Used TDD approach for developing this application.

#

### DataProcessor.Service
##### We have used two external libraries - CsvHelper (for parsing CSV files) & StringExtensions (for string manipulations)

##### ContractSizeConverter allows us to easily manage logic for complex string parsing and manipulation
##### OutputRecordMap allows us to handle changes in source file columns easily.
##### ExceptionHandler allows us to manage exception handling in a centralised manner. This can include logging.

##### Dependency Injection Principle- Exception handler is injected to DataHandler.
##### Open/Close principle - CsvHelper implements IDataHandler. That way we can introduce as many handlers as we like e.g. for each format type like xml, json etc. And the current application will work seemlessly because we called ExtractData on the type passsed in
##### Single Responsibility principle - OutputRecordMap is in charge of mapping correct fields while OutputRecord is the model. ContractSizeConverter takes care of parsing ContractSize from AlgoParams
##### Interface Segregation - we have seperated the handler in its repective interfaces for cleaner and modular code

#

### DataProcessor.Client
##### This can be any presentation layer i.e. console app, desktop app, web app or even mobile app
##### We have not used container but you would normally register types with container and resolved them at runtime. .netcore comes with built in container support. I have not added here only to keep code a bit simple and not overengineer it, but in production systems, we would resolve these types from container and inject them.
##### First param is file name which is passed in as parameter in debug mode. This is set in project properties when running in debug mode. In release mode, this would need to be supplied by user

