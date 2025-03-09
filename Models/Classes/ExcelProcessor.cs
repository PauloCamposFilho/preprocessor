using System.Text;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class ExcelProcessor(string inputFile, string outputFile = "output.txt") : IProcessor
{
  private string _inputFile = inputFile;
  private string _outputFile = outputFile;
  private StringBuilder _sb = new StringBuilder();
  private int processedRows = 0;

  public void Process()
  {

  }
  public void Save()
  {
    
  }
}