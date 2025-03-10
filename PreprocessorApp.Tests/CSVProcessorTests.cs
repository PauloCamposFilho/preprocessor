using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
using System;
namespace PreprocessorApp.Tests;

public class CSVProcesorTests
{
    CSVProcessor processor = new CSVProcessor();

    [Fact]
    public void Process_ValidCSV_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "input.csv");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputCSV.txt");
        string tempOutputPath = Path.GetTempFileName();

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
    public void Process_NoRowsCSVFile_WritesEmptyOutput()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "empty.csv");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputCSVEmpty.txt");
        string tempOutputPath = Path.GetTempFileName();

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
    public void Process_EmptyCSVFile_ThrowsException()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "invalid.csv");
        string tempOutputPath = Path.GetTempFileName();

        // Act
        var exception = Assert.Throws<CsvHelper.ReaderException>(() => processor.Process(inputPath, tempOutputPath));

        // Assert
        Assert.Contains("no header record", exception.Message.ToLower());

        // Cleanup
        File.Delete(tempOutputPath);
    }
}