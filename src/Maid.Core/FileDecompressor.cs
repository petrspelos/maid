// Maid
// Copyright (C) 2023 Petr Sedláček
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

using Maid.Core.Boundaries;

namespace Maid.Core;

public class FileDecompressor
{
    private readonly IFileSystem _fileSystem;
    private readonly IFileCompression _compression;

    public FileDecompressor(IFileSystem fileSystem, IFileCompression fileCompression)
    {
        _fileSystem = fileSystem;
        _compression = fileCompression;
    }

    public Action<string>? Logger { get; set; }

    public void Decompress(string rootPath, bool recursive = false)
    {
        var files = GetFiles(rootPath, recursive);

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file);

            switch (extension)
            {
                case ".zip":
                    Logger?.Invoke($".zip strategy chosen for: {file}");
                    _compression.DecompressZipInPlace(file, rootPath);
                    break;
                case ".7z":
                    Logger?.Invoke($"7zip strategy chosen for: {file}");
                    _compression.Decompress7ZipInPlace(file, rootPath);
                    break;
                default:
                    continue;
            }
        }
    }

    private IEnumerable<string> GetFiles(string rootPath, bool recursive)
    {
        if (!_fileSystem.DirectoryExists(rootPath))
        {
            throw new ArgumentException("Provided root path does not exist.");
        }

        return recursive ? _fileSystem.GetFilesRecursive(rootPath) : _fileSystem.GetFiles(rootPath);
    }
}

