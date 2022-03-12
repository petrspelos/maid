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

using System.Text.Json;
using Maid.Core.Boundaries;
using Maid.Core.Entities;

namespace Maid.Infrastructure;

public class JsonFileDriveIdentifier : IDriveIdentifier
{
    private const string IdentifierFileName = "MaidDriveId.json";

    public Drive? GetDriveFromPath(string drivePath)
    {
        var drive = JsonSerializer.Deserialize<Drive>(File.ReadAllText(Path.Combine(drivePath, IdentifierFileName)));

        if (drive is null)
            return null;

        if (drive.DrivePath != drivePath)
        {
            var updateDrive = new Drive(drive.DriveId, drive.DriveLabel, drivePath);
            WriteDriveInfo(updateDrive);
            drive = updateDrive;
        }

        return drive;
    }

    public bool IsRecognized(string drivePath)
    {
        var idFilePath = Path.Combine(drivePath, IdentifierFileName);

        if (!File.Exists(idFilePath))
            return false;

        return JsonSerializer.Deserialize<Drive>(File.ReadAllText(idFilePath)) is not null;
    }

    public void RegisterDrive(Drive drive) => WriteDriveInfo(drive);

    private static void WriteDriveInfo(Drive drive)
    {
        var json = JsonSerializer.Serialize(drive);
        File.WriteAllText(Path.Combine(drive.DrivePath, IdentifierFileName), json);
    }
}
