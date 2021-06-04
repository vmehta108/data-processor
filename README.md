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

#

### Assumptions & Additional points to note
* We would normally create and work on a feature branch. So all RED tests added during TDD will not break the CI\CD pipeline.
* Stopwatch was not asked as part of the task but was added assuming that profiling should be a key component of any ETL task
* OutputRecord class contains annotations for Name which can be refactored to use single using statement on top for namespace thereby avoiding putting fully qualified namespace for each attribute
* ExceptionHandler is using Log and Rethrow strategy. This can further be improved to return a bool value to caller and let caller decide to rethrow or not based on bool value. That way the stacktrace doesn't have any unnecessary information added.
* We have assumed that input csv has no quoted values i.e. all data is without double or single quotes and we have assumed that header is present on second line. With this assumptions, we can add more validations to check if at least one "," is present in data. It also allows us to then check that no of headers match no of data values. If csv values are allowed to be quoted, then we need to refactor current code to make sure we ignore comma inside quoted values.
* A trade off is chosen here between IEnumerable collection and List i.e. memory optimisation over speed. IEnumerable is good for very large files size where we defer enumeration and only enumerate one by one record and write it immediately. List is good for small dataset where it won't take up lot of memory and we can read all data in an in-momory collection(e.g. List) first and then write it, which is much faster. On this occassion, we have chosen IEnumerable assuming that we will be extracting very large files. 
* Another trade off is chosen here between code cohesion and readabililty. Due to restriction from CsvHelper which requires open readers(CsvReader & StreamReader), if we want to seperate Reading and Writing code in seperate methods or classes, we have to pass those readers around(by keep them open and not disposing). Or we can have all code in one place and utilise __*using*__ statement for automatic disposing(as its done currently). On this occassion, seperate code looks messier so we have kept all code together. The code is more readable this way.
