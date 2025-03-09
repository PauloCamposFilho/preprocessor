using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class ProcessFactory(string processorSelector, string inputFile, string outputFile = "output.txt")
{
  private string _inputFile = inputFile;
  private string _outputFile = outputFile;
  private string _processorSelector = processorSelector;

  public IProcessor InstantiateProcessor()
  {
    switch(_processorSelector)
    {
      case "csv":
      {
        return new CSVProcessor(_inputFile, _outputFile);
      }
      case "excel":
      {
        throw new NotImplementedException();
      }
    }
    throw new KeyNotFoundException();
  }
}