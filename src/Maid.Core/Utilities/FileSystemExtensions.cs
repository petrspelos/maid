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

using Maid.Core.Boundaries;

namespace Maid.Core.Utilities;

internal static class FileSystemExtensions
{
    internal static string GetUniqueNameForFile(this IFileSystem fileSystem, string file, string rootPath)
    {
        if (!fileSystem.FileWithSameNameExists(file, rootPath))
            return fileSystem.ChangePathRoot(file, rootPath);

        var newFileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
        return fileSystem.ChangePathRoot(fileSystem.GetPathWithChangedFileName(file, newFileName), rootPath);
    }
}
