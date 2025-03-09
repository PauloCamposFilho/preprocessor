using System.Globalization;
using CsvHelper;

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

      using (var reader = new StreamReader(inputFile))
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
        using (var writer = new StreamWriter(outputFile))
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
          Console.WriteLine($"Processed {rowCount} rows.");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error: " + ex.Message);
    }
  }
}