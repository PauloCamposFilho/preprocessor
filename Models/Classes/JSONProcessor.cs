using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class JSONProcessor() : IProcessor
{
  private StringBuilder _sb = new StringBuilder();
  private int processedRows = 0;

  public void Process(string inputFile)
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

      foreach (var item in JSONArray)
      {
        var values = headers.Select(p => item[p]?.ToString() ?? string.Empty);
        _sb.AppendLine(string.Join("|", values));
        processedRows++;
      }
    }
    catch(JsonException ex)
    {
      Console.WriteLine($"JSON Error: {ex.Message}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }

  public void Save(string outputFile)
  {
    throw new NotImplementedException();
  }
}