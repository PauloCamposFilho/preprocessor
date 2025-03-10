using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
namespace PreprocessorApp.Tests;

public class ExcelProcessorTests
{
    [Fact]
    public void Process_ValidExcel_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "excelInput.xlsx");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputExcel.txt");
        string tempOutputPath = Path.GetTempFileName();
        var processor = new ExcelProcessor();

        // Act
        processor.Process(inputPath, tempOutputPath);

        // Assert
        string actualOutput = File.ReadAllText(tempOutputPath);
        string expectedOutput = File.ReadAllText(expectedOutputPath);
        Assert.Equal(expectedOutput, actualOutput);

        // Cleanup
        File.Delete(tempOutputPath);
    }
}