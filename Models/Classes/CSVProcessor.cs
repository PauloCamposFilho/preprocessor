using System.Globalization;
using System.Text;
using CsvHelper;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class CSVProcessor(string inputFile, string outputFile = "output.txt") : IProcessor
{
  private string _inputFile = inputFile;
  private string _outputFile = outputFile;
  private StringBuilder _sb = new StringBuilder();
  private int processedRows = 0;
  public void Process()
  {
    using (var reader = new StreamReader(_inputFile))
    using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-US")))
    {
      csv.Read();
      csv.ReadHeader();
      string[]? header = csv.HeaderRecord;
      int expectedFieldCount = 0;
      if (header != null)
      {
        expectedFieldCount = header.Length;
      }
      while (csv.Read())
      {
        if (csv.Parser.Count != expectedFieldCount)
        {
          Console.WriteLine($"Skipping row {csv.Parser.Row}: expected {expectedFieldCount} fields, got {csv.Parser.Count}");
          continue;
        }
        List<string?> fields = new List<string?>();
        for (int i = 0; i < expectedFieldCount; i++)
        {
          string? field = csv.GetField(i);
          fields.Add(field);
        }
        string line = string.Join("|", fields);
        _sb.AppendLine(line);
        processedRows++;
      }
    }
  }

  public void Save()
  {
    using (var writer = new StreamWriter(_outputFile))
    {
      writer.Write(_sb);
    }
    Console.WriteLine($"Processed {processedRows} rows.");
  }
}