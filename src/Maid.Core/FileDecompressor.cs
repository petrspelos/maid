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
using Maid.Core.Utilities;

namespace Maid.Core;

public class FileDecompressor
{
    private readonly IFileSystem _fileSystem;

    public FileDecompressor(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Decompress(string rootPath, bool recursive = false)
    {

    }
}
