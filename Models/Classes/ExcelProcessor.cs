using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using PreprocessorApp.Models.Interfaces;

namespace PreprocessorApp.Models.Classes;

public class ExcelProcessor() : IProcessor
{
  private int processedRows = 0;

  public void Process(string inputFile, string outputFile)
  {
    try
    {
      // Set the License Context
      ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

      using (var package = new ExcelPackage(new FileInfo(inputFile)))
      {
        // Get the first worksheet
        var worksheet = package.Workbook.Worksheets[0];

        // Get number of rows / columns
        if (worksheet.Dimension == null)
        {
          throw new Exception("Excel file empty.");
        }

        int rowCount = worksheet.Dimension.Rows;
        int colCount = worksheet.Dimension.Columns;

        // Get headers
        var headers = Enumerable.Range(1, colCount)
          .Select(col => worksheet.Cells[1, col].Text)
          .ToArray();

        // Iterate through the rows to get the data
        using (var writer = new StreamWriter(outputFile))
        {
          for (int row = 2; row <= rowCount; row++)
          {
            var fields = Enumerable.Range(1, colCount)
              .Select(col => worksheet.Cells[row, col].Text)
              .ToArray();
            writer.WriteLine(string.Join("|", fields));
            processedRows++;
          }
        }
        Console.WriteLine($"Processed {processedRows} rows.");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error processing Excel file: {ex.Message}");
      throw;
    }
  }
}