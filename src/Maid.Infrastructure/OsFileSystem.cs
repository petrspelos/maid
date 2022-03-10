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

using Maid.Core;

namespace Maid.Infrastructure;

public class OsFileSystem : IFileSystem
{
    public string ChangePathRoot(string file, string rootPath) => Path.Combine(rootPath, Path.GetFileName(file));

    public void Copy(string sourceFile, string destinationFile) => File.Copy(sourceFile, destinationFile, false);

    public IEnumerable<string> GetFiles(string path) => Directory.GetFiles(path);

    public bool FileWithSameNameExists(string file, string targetPath) => GetFiles(targetPath).Select(f => Path.GetFileName(f)).Contains(Path.GetFileName(file));

    public string GetPathWithChangedFileName(string file, string newName) => Path.Combine(Path.GetDirectoryName(file)!, newName + Path.GetExtension(file));

    public IEnumerable<string> GetSubdirectories(string path) => Directory.GetDirectories(path);
}
