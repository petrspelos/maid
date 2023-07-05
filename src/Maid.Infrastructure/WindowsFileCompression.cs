using System.Diagnostics;
using System.IO.Compression;
using Maid.Core.Boundaries;

namespace Maid.Infrastructure;

public class WindowsFileCompression : IFileCompression
{
    private readonly IFileSystem _fileSystem;

    public WindowsFileCompression(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void DecompressZipInPlace(string zipPath, string rootPath)
    {
        var extractPath = Path.Combine(rootPath, Path.GetFileNameWithoutExtension(zipPath));

        if (!_fileSystem.DirectoryExists(extractPath))
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath, overwriteFiles: true);
        }
    }

    public void Decompress7ZipInPlace(string zipPath, string rootPath)
    {
        var extractPath = Path.Combine(rootPath, Path.GetFileNameWithoutExtension(zipPath));

        if (!_fileSystem.DirectoryExists(extractPath))
        {
            ProcessStartInfo pro = new ProcessStartInfo();
            pro.WindowStyle = ProcessWindowStyle.Hidden;
            pro.FileName = "7z.exe";
            pro.Arguments = $"x \"-o{extractPath}\" \"{zipPath}\"";
            Process x = Process.Start(pro)!;
            x.WaitForExit();
        }
    }
}