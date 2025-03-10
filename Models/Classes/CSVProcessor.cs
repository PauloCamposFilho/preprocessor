using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class CSVProcessor() : IProcessor
{
  private int processedRows = 0;
  public void Process(string inputFile, string outputFile)
  {
    using (var reader = new StreamReader(inputFile))
    using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-US")))
    {
      csv.Read();
      csv.ReadHeader();
      string[] header = csv.HeaderRecord;
      int expectedFieldCount = 0;
      if (header != null)
      {
        expectedFieldCount = header.Length;
      }
      using (var writer = new StreamWriter(outputFile))
      {
        while (csv.Read())
        {
          if (csv.Parser.Count != expectedFieldCount)
          {
            Console.WriteLine($"Skipping row {csv.Parser.Row}: expected {expectedFieldCount} fields, got {csv.Parser.Count}");
            continue;
          }
          List<string> fields = new List<string>();
          for (int i = 0; i < expectedFieldCount; i++)
          {
            string field = csv.GetField(i);
            fields.Add(field);
          }
          string line = string.Join("|", fields);
          writer.WriteLine(line);
          processedRows++;
        }
      }
      Console.WriteLine($"Processed {processedRows} rows.");
    }
  }
}