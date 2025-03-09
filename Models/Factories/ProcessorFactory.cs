using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class ProcessorFactory()
{
  public static IProcessor GetProcessor(string processorSelector)
  {
    switch(processorSelector)
    {
      case "-csv":
      {
        return new CSVProcessor();
      }
      default:
      {
        throw new NotImplementedException();
      }
    }
  }
}