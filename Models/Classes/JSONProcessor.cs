using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class JSONProcessor() : IProcessor
{
  private int processedRows = 0;

  public void Process(string inputFile, string outputFile)
  {
    try
    {
      string JSONContent = File.ReadAllText(inputFile);
      var JSONArray = JArray.Parse(JSONContent);

      if (JSONArray.Count == 0)
      {
        throw new Exception("JSON file is empty");
      }

      var headers = ((JObject)JSONArray[0]).Properties().Select(p => p.Name).ToArray();

      using (var writer = new StreamWriter(outputFile))
      {
        foreach (var item in JSONArray)
        {
          var values = headers.Select(p => item[p]?.ToString() ?? string.Empty);
          writer.WriteLine(string.Join("|", values));
          processedRows++;
        }
      }
      Console.WriteLine($"Processed {processedRows} rows.");
    }
    catch (JsonException ex)
    {
      Console.WriteLine($"JSON Error: {ex.Message}");
      throw;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
      throw;
    }
  }
}