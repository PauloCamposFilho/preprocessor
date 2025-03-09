namespace PreprocessorApp.Models.Interfaces
{
    public interface IProcessor
    {
      public void Process(string inputFile);
      public void Save(string outputFile);
    }
}