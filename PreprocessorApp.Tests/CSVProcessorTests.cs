using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
namespace PreprocessorApp.Tests;

public class CSVProcesorTests
{
    [Fact]
    public void Process_ValidCSV_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "input.csv");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputCSV.txt");
        string tempOutputPath = Path.GetTempFileName();
        var processor = new CSVProcessor();

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