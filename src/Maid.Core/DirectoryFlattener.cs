// Maid
// Copyright (C) 2022 Petr Sedláček
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

using Maid.Core.Utilities;

namespace Maid.Core;

public class DirectoryFlattener
{
    private readonly IFileSystem _fileSystem;

    public DirectoryFlattener(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Flatten(string rootPath, bool moveFiles = false)
    {
        var subdirectories = new Stack<string>(_fileSystem.GetSubdirectories(rootPath));
        var filesToCopy = new List<string>();

        while (subdirectories.Count > 0)
        {
            var currentDirectory = subdirectories.Pop();

            filesToCopy.AddRange(_fileSystem.GetFiles(currentDirectory));
            foreach (var sd in _fileSystem.GetSubdirectories(currentDirectory))
                subdirectories.Push(sd);
        }

        foreach (var file in filesToCopy)
        {
            var destinationFile = _fileSystem.GetUniqueNameForFile(file, rootPath);

            if (moveFiles)
                _fileSystem.Move(file, destinationFile);
            else
                _fileSystem.Copy(file, destinationFile);
        }
    }
}
