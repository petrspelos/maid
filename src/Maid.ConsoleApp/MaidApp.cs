using Maid.Core;
using Maid.Core.Boundaries;
using Maid.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Maid.ConsoleApp;

public sealed class MaidApp : IHostedService
{
    private readonly ILogger<MaidApp> _logger;
    private readonly IFileSystem _fileSystem;
    private readonly FileDecompressor _fileDecompressor;
    private readonly DirectoryFlattener _directoryFlattener;
    private readonly MaidOptions _cfg;

    public MaidApp(ILogger<MaidApp> logger, IFileSystem fileSystem, FileDecompressor fileDecompressor, DirectoryFlattener directoryFlattener, IOptions<MaidOptions> cfg)
    {
        _logger = logger;
        _fileSystem = fileSystem;
        _fileDecompressor = fileDecompressor;
        _directoryFlattener = directoryFlattener;
        _cfg = cfg.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var fileSystem = new OsFileSystem();

        if (string.IsNullOrWhiteSpace(_cfg.Command))
        {
            _logger.LogInformation("USAGE: maid.exe --command=[flat|sort|unzip] <options>");
            Environment.Exit(0);
        }

        if (!_fileSystem.DirectoryExists(_cfg.Path))
        {
            _logger.LogWarning($"The path doesn't exist: {_cfg.Path}");
            Environment.Exit(0);
        }

        _logger.LogInformation($"PATH: '{_cfg.Path}'");

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
            _logger.LogInformation($"Flatten: moveFiles: {_cfg.MoveDontCopy}, directoryToUniqueName: {_cfg.PathToName}");
            _directoryFlattener.Flatten(_cfg.Path, moveFiles: _cfg.MoveDontCopy, directoryToUniqueName: _cfg.PathToName);
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
            _logger.LogInformation($"Decompress: recursive: {_cfg.Recursive}");
            _fileDecompressor.Decompress(_cfg.Path, recursive: _cfg.Recursive);
        }

        _logger.LogInformation("Done");
        Environment.Exit(0);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
