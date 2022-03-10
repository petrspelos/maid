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

using System;
using System.IO;
using Maid.ConsoleApp;
using Maid.ConsoleApp.Abstractions;
using Moq;
using Xunit;

namespace Maid.Tests;

public class DirectoryFlattenerTest
{
    private readonly Mock<IFileSystem> _fileSystemMock;
    private readonly DirectoryFlattener _sut;

    public DirectoryFlattenerTest()
    {
        _fileSystemMock = new Mock<IFileSystem>();
        _sut = new DirectoryFlattener(_fileSystemMock.Object);
    }

    [Fact]
    public void NonExistentDirectory_ShouldThrow()
    {
        _fileSystemMock.Setup(fs => fs.GetSubdirectories(It.IsAny<string>())).Throws<DirectoryNotFoundException>();

        Assert.Throws<DirectoryNotFoundException>(() => _sut.Flatten("NonExistentDirectory"));
    }
}
