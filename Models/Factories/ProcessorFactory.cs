using System;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class ProcessorFactory()
{
  public static IProcessor GetProcessor(string processorSelector)
  {
    switch(processorSelector.ToLower())
    {
      case "-csv":
      {
        return new CSVProcessor();
      }
      case "-excel":
      {
        return new ExcelProcessor();
      }
      case "-json":
      {
        return new JSONProcessor();
      }
      default:
      {
        throw new NotImplementedException();
      }
    }
  }
}