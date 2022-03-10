![](docs/header.png)

This piece of software is my personal file and backup management tool.

### So um... What does it do?

I'm still writing this tool and figuring out all of its uses.

Currently it has the following features:

- Directory Flattener

```cs
var flattener = new DirectoryFlattener(new OsFileSystem());

// Moves all files from directories (including subdirectories) in
// the provided path TO the provided path
flattener.Flatten("E:\\images");

// by default, Flatten copies files
// this behavior can be changed
flattener.Flatten("E:\\images", moveFiles: true);
```

> 💡 Will not overwrite other files because if the flattened file name collides with another it is renamed to a new GUID name instead.

- File Sorter

```cs
var fileSorter = new FileSorter(new OsFileSystem());

fileSorter.AddRule(new(CommonFilePatterns.ImageFiles, @"C:\Users\micro\Pictures\sorted"));
fileSorter.AddRule(new(CommonFilePatterns.SoundFiles, @"C:\Users\micro\Music"));
fileSorter.AddRule(new(CommonFilePatterns.GimpFiles, @"C:\Users\micro\Documents\Gimp projects"));
fileSorter.AddRule(new(CommonFilePatterns.PresentationFiles, @"C:\Users\micro\Documents\Presentations"));
fileSorter.AddRule(new(CommonFilePatterns.ArchiveFiles, @"C:\Users\micro\Documents\Archives"));
fileSorter.AddRule(new(CommonFilePatterns.WindowsShortcutFiles, string.Empty, SpecialRule.Delete));

// Moves files from the provided directory into other directories
// based on the rule set defined earlier.
// Files not matching any rule won't be moved.
fileSorter.SortDirectory(@"C:\Users\micro\Desktop");
```

> 💡 My personal use is to first Flatten my Downloads directory (removing folders) and then sort the files

### Can I help?

This is primarily a tool that I myself use, but if you have any suggestions, bug reports, or even pull requests, then go ahead. :relaxed:

### Where's the cool image from?

It's [one of the wallpapers](https://wallhaven.cc/w/vm8dmp) on wallhaven.
