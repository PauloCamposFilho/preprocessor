using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
namespace PreprocessorApp.Tests;

public class JSONProcessorTests
{
    [Fact]
    public void Process_ValidJSON_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "input.json");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputJSON.txt");
        string tempOutputPath = Path.GetTempFileName();
        var processor = new JSONProcessor();

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