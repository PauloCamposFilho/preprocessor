using System.Globalization;
using CsvHelper;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class CSVProcessor(string inputFile, string outputFile = "output.txt") : IProcessor
{
  private string _inputFile = inputFile;
  private string _outputFile = outputFile;
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
      using (var writer = new StreamWriter(_outputFile))
      {
        int rowCount = 0;
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
          writer.WriteLine(line);
          rowCount++;
        }
        Console.WriteLine($"Processed ${rowCount} rows.");
      }
    }
  }

  public void Save()
  {
    throw new NotImplementedException();
  }
}