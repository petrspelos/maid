using Maid.Core;
using Maid.Core.Boundaries;
using Maid.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Maid.ConsoleApp;

public sealed class MaidApp : IHostedService
{
    private readonly IFileSystem _fileSystem;
    private readonly FileDecompressor _fileDecompressor;
    private readonly MaidOptions _cfg;

    public MaidApp(IFileSystem fileSystem, FileDecompressor fileDecompressor, IOptions<MaidOptions> cfg)
    {
        _fileSystem = fileSystem;
        _fileDecompressor = fileDecompressor;
        _cfg = cfg.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var fileSystem = new OsFileSystem();

        if (string.IsNullOrWhiteSpace(_cfg.Command))
        {
            Console.WriteLine("USAGE: maid.exe --command=[flat|sort|unzip] <options>");
            Environment.Exit(0);
        }

        if (!_fileSystem.DirectoryExists(_cfg.Path))
        {
            Console.WriteLine($"The path doesn't exist: {_cfg.Path}");
            Environment.Exit(0);
        }

        PrintCol($"PATH: '{_cfg.Path}'", ConsoleColor.Black, ConsoleColor.Blue);

        switch (_cfg.Command)
        {
            case "flat":
                RunFlatten();
                break;
            case "sort":
                RunSorting();
                break;
            case "unzip":
                RunDecompress();
                break;
        }

        void RunFlatten()
        {
            PrintCol($"Flatten: moveFiles: {_cfg.MoveDontCopy}, directoryToUniqueName: {_cfg.PathToName}", ConsoleColor.Black, ConsoleColor.Yellow);
            var flattener = new DirectoryFlattener(fileSystem);
            flattener.Flatten(_cfg.Path, moveFiles: _cfg.MoveDontCopy, directoryToUniqueName: _cfg.PathToName);
        }

        void RunSorting()
        {
            throw new NotImplementedException("Sorting configuration not yet implemented");
            // var fileSorter = new FileSorter(new OsFileSystem());

            // fileSorter.AddRule(new(CommonFilePatterns.ImageFiles, @"C:\Users\micro\Pictures\sorted"));
            // fileSorter.AddRule(new(CommonFilePatterns.SoundFiles, @"C:\Users\micro\Music"));
            // fileSorter.AddRule(new(CommonFilePatterns.GimpFiles, @"C:\Users\micro\Documents\Gimp projects"));
            // fileSorter.AddRule(new(CommonFilePatterns.PresentationFiles, @"C:\Users\micro\Documents\Presentations"));
            // fileSorter.AddRule(new(CommonFilePatterns.ArchiveFiles, @"C:\Users\micro\Documents\Archives"));
            // fileSorter.AddRule(new(CommonFilePatterns.WindowsShortcutFiles, string.Empty, SpecialRule.Delete));

            // fileSorter.SortDirectory(rootPath);
        }

        void RunDecompress()
        {
            PrintCol($"Decompress: recursive: {_cfg.Recursive}", ConsoleColor.Black, ConsoleColor.Yellow);
            _fileDecompressor.Logger += (msg) => PrintCol(msg, ConsoleColor.DarkBlue, ConsoleColor.White);
            _fileDecompressor.Decompress(_cfg.Path, recursive: _cfg.Recursive);
        }

        void PrintCol(string message, ConsoleColor fg, ConsoleColor bg)
        {
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        Console.WriteLine("Done");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
