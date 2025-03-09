using System.Text;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class JSONProcessor() : IProcessor
{
  private StringBuilder _sb = new StringBuilder();
  private int processedRows = 0;

    public void Process(string inputFile)
    {
        throw new NotImplementedException();
    }

    public void Save(string outputFile)
    {
        throw new NotImplementedException();
    }
}