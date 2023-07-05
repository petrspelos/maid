// Maid
// Copyright (C) 2023 Pet Sedláček
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.


using Maid.Core;
using Maid.Core.Entities;
using Maid.Infrastructure;

var fileSystem = new OsFileSystem();

if (args.Length < 2)
{
    Console.WriteLine("USAGE: maid.exe c:/path_to_clean [command]");
    Environment.Exit(0);
}

var rootPath = args[0];

if (!fileSystem.DirectoryExists(rootPath))
{
    Console.WriteLine($"The path: '{rootPath}' does not exist");
    Environment.Exit(-1);
}

PrintCol($"PATH: '{rootPath}'", ConsoleColor.Black, ConsoleColor.Blue);

var cmd = args[1];
PrintCol($"CMD: '{cmd}'", ConsoleColor.Black, ConsoleColor.Green);

switch (cmd)
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
    PrintCol($"Flatten: moveFiles: {ArgIsSet("--move-dont-copy")}, directoryToUniqueName: {ArgIsSet("--path-to-name")}", ConsoleColor.Black, ConsoleColor.Yellow);
    var flattener = new DirectoryFlattener(fileSystem);
    flattener.Flatten(rootPath, moveFiles: ArgIsSet("--move-dont-copy"), directoryToUniqueName: ArgIsSet("--path-to-name"));
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
    PrintCol($"Decompress: recursive: {ArgIsSet("--recursive")}", ConsoleColor.Black, ConsoleColor.Yellow);
    var decompressor = new FileDecompressor(fileSystem, new WindowsFileCompression(fileSystem));
    decompressor.Logger += (msg) => PrintCol(msg, ConsoleColor.DarkBlue, ConsoleColor.White);
    decompressor.Decompress(rootPath, recursive: ArgIsSet("--recursive"));
}

bool ArgIsSet(string arg) => args.Any(a => a == arg);

string GetArgVal(string arg) => args.FirstOrDefault(a => a.StartsWith(arg))?.Substring(arg.Length) ?? string.Empty; 

void PrintCol(string message, ConsoleColor fg, ConsoleColor bg)
{
    Console.BackgroundColor = bg;
    Console.ForegroundColor = fg;
    Console.WriteLine(message);
    Console.ResetColor();
}

Console.WriteLine("Done");
