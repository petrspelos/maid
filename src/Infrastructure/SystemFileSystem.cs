using MaidSharp.Core;
using System.IO;

namespace MaidSharp.Infrastructure
{
    public class SystemFileSystem : FileSystem
    {
        public override string[] GetAllDirectoryFiles(string path) => Directory.GetFiles(path);
    }
}
