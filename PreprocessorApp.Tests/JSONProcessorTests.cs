using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
using System;
namespace PreprocessorApp.Tests;

public class JSONProcessorTests
{
    JSONProcessor processor = new JSONProcessor();

    [Fact]
    public void Process_ValidJSON_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "input.json");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputJSON.txt");
        string tempOutputPath = Path.GetTempFileName();
        // var processor = new JSONProcessor();

        // Act
        processor.Process(inputPath, tempOutputPath);

        // Assert
        string actualOutput = File.ReadAllText(tempOutputPath);
        string expectedOutput = File.ReadAllText(expectedOutputPath);
        Assert.Equal(expectedOutput, actualOutput);

        // Cleanup
        File.Delete(tempOutputPath);
    }

    [Fact]
    public void Process_InvalidJsonFile_ThrowsException()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "invalid.json");
        string tempOutputPath = Path.GetTempFileName();

        // Act
        var exception = Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => processor.Process(inputPath, tempOutputPath));
        Assert.Contains("error reading jarray", exception.Message.ToLower());

        // Cleanup
        File.Delete(tempOutputPath);
    }

    [Fact]
    public void Process_EmptyJSONFile_ThrowsException()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "empty.json");
        string tempOutputPath = Path.GetTempFileName();

        // Act
        var exception = Assert.Throws<Exception>(() => processor.Process(inputPath, tempOutputPath));

        // Assert
        Assert.Contains("empty", exception.Message.ToLower());

        // Cleanup
        File.Delete(tempOutputPath);
    }
}