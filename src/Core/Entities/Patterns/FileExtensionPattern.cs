using System.IO;

namespace MaidSharp.Core.Entities.Patterns
{
    public class FileExtensionPattern : FilePattern
    {
        private readonly string _targetExtension;

        public FileExtensionPattern(string extension)
        {
            _targetExtension = extension;
        }

        public override bool Matches(string filePath)
            => Path.GetExtension(filePath) == _targetExtension;
    }
}
