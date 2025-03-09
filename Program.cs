using System.Globalization;
using CsvHelper;
using PreprocessorApp.Models.Classes;

class Program
{
  static void Main(string[] args)
  {
    // invalid usage, error and abort
    if (args.Length != 2)
    {
      Console.WriteLine("Usage: preprocessor filename.csv output.txt");
      return; // end program
    }

    string inputFile = args[0];
    string outputFile = args[1];

    try
    {
      if (Path.GetExtension(inputFile).ToLower() != ".csv")
      {
        throw new Exception("Wrong File Type");
      }
      CSVProcessor processor = new CSVProcessor(inputFile, outputFile);
      processor.Process();
      processor.Save();
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error: " + ex.Message);
    }
  }
}