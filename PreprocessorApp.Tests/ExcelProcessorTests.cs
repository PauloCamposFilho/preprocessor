using Xunit;
using System.IO;
using PreprocessorApp.Models.Classes;
using System;
namespace PreprocessorApp.Tests;

public class ExcelProcessorTests
{
    ExcelProcessor processor = new ExcelProcessor();

    [Fact]
    public void Process_ValidExcel_ReturnsFlatFileContent()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "excelInput.xlsx");
        string expectedOutputPath = Path.Combine("tests", "outputs", "outputExcel.txt");
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
    public void Process_InvalidExcelFile_ThrowsException()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "invalid.xlsx");
        string tempOutputPath = Path.GetTempFileName();

        // Act
        var exception = Assert.Throws<InvalidDataException>(() => processor.Process(inputPath, tempOutputPath));

        // Assert
        Assert.Contains("is not a valid", exception.Message.ToLower());

        // Cleanup
        File.Delete(tempOutputPath);
    }

    [Fact]
    public void Process_EmptyExcelFile_ThrowsException()
    {
        // Arrange
        string inputPath = Path.Combine("tests", "empty.xlsx");
        string expectedOutputPath = Path.Combine("tests", "output", "outputExcelEmpty.txt");
        string tempOutputPath = Path.GetTempFileName();

        // Act
        var exception = Assert.Throws<Exception>(() => processor.Process(inputPath, tempOutputPath));

        // Assert
        Assert.Contains("excel file empty", exception.Message.ToLower());

        // Cleanup
        File.Delete(tempOutputPath);
    }
}