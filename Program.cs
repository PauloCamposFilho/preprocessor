using System;
using PreprocessorApp.Models.Classes;
using PreprocessorApp.Models.Interfaces;

class Program
{
  static void Main(string[] args)
  {
    // invalid usage, error and abort
    if (args.Length != 3)
    {
      Console.WriteLine("Usage: preprocessor -X filename.csv output.txt");
      Console.WriteLine("Accepted first parameters: -csv");
      return; // end program
    }

    string processorSelector = args[0];
    string inputFile = args[1];
    string outputFile = args[2];

    try
    {
      IProcessor processor = ProcessorFactory.GetProcessor(processorSelector);
      processor.Process(inputFile, outputFile);
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error: " + ex.Message);
    }
  }
}