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

using System.Text.RegularExpressions;
using Maid.Core.Boundaries;
using Maid.Core.Entities;
using Maid.Core.Utilities;

namespace Maid.Core;

public class FileSorter
{
    private readonly IFileSystem _fileSystem;

    private readonly ICollection<FileSortRule> _rules;

    public FileSorter(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;

        _rules = new List<FileSortRule>();
    }

    public void AddRule(FileSortRule rule) => _rules.Add(rule);

    public void SortDirectory(string directoryPath)
    {
        if (!_fileSystem.DirectoryExists(directoryPath))
            return;

        var filesToSort = _fileSystem.GetFiles(directoryPath);

        foreach(var file in filesToSort)
        {
            // NOTE(Peter): FirstOrDefault cannot be used because Rules are not nullable
            //              so no matching rule would still return an empty rule...
            if (!_rules.Any(r => Regex.Match(file, r.FileNamePattern, RegexOptions.Compiled).Success))
                continue;

            var rule = _rules.First(r => Regex.Match(file, r.FileNamePattern, RegexOptions.Compiled).Success);

            if (rule.SpecialRule == SpecialRule.Delete)
            {
                _fileSystem.Delete(file);
                continue;
            }

            _fileSystem.Move(file, _fileSystem.GetUniqueNameForFile(file, rule.DestinationDirectory));
        }
    }
}
