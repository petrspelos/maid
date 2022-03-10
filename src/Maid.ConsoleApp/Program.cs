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
using Maid.Core.Entities;
using Maid.Infrastructure;

var fileSorter = new FileSorter(new OsFileSystem());
fileSorter.AddRule(new(CommonFilePatterns.ImageFiles, @"C:\Users\micro\Pictures\sorted"));
fileSorter.AddRule(new(CommonFilePatterns.SoundFiles, @"C:\Users\micro\Music"));
fileSorter.AddRule(new(CommonFilePatterns.VideoFiles, @"C:\Users\micro\Videos"));
fileSorter.AddRule(new(CommonFilePatterns.GimpFiles, @"C:\Users\micro\Documents\Gimp projects"));
fileSorter.AddRule(new(CommonFilePatterns.PresentationFiles, @"C:\Users\micro\Documents\Presentations"));
fileSorter.AddRule(new(CommonFilePatterns.ArchiveFiles, @"C:\Users\micro\Documents\Archives"));
fileSorter.AddRule(new(CommonFilePatterns.WindowsShortcutFiles, string.Empty, SpecialRule.Delete));
fileSorter.AddRule(new(@".*?\.(msi|nfo|txt)", string.Empty, SpecialRule.Delete));


var directoryFlattener = new DirectoryFlattener(new OsFileSystem());

directoryFlattener.Flatten(@"C:\Users\micro\Downloads", moveFiles: true);
fileSorter.SortDirectory(@"C:\Users\micro\Downloads");

Console.WriteLine("Done");
