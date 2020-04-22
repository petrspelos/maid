namespace MaidSharp.Core.Entities.Patterns
{
    public abstract class FilePattern
    {
        public abstract bool Matches(string filePath);
    }    
}
