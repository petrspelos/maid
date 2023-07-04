// Maid
// Copyright (C) 2023 Pet Sedláček
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
using Maid.Core.Entities;

namespace Maid.Core;

public class LogicalDriveProvider
{
    private readonly IDriveInfo _driveInfo;
    private readonly IDriveIdentifier _driveIdentifier;

    public LogicalDriveProvider(IDriveInfo driveInfo, IDriveIdentifier driveIdentifier)
    {
        _driveInfo = driveInfo;
        _driveIdentifier = driveIdentifier;
    }

    public IEnumerable<Drive> GetRecognizedDrives()
        => _driveInfo.GetDrivePaths().Where(dp => _driveIdentifier.IsRecognized(dp)).Select(dp => _driveIdentifier.GetDriveFromPath(dp)).Where(d => d is not null).ToList()!;

    public void RegisterNewDrive(string drivePath, string label = "My Maid Drive")
    {
        if (!_driveInfo.GetDrivePaths().Any(dp => dp == drivePath))
            throw new DriveNotFoundException($"The following path is not any connected drive's root path: {drivePath}");

        _driveIdentifier.RegisterDrive(new(Guid.NewGuid(), label, drivePath));
    }

    public void SaveDriveChanges(Drive drive)
    {
        if (!_driveInfo.GetDrivePaths().Any(dp => dp == drive.DrivePath))
            throw new DriveNotFoundException($"The following path is not any connected drive's root path: {drive.DrivePath}");

        if (_driveIdentifier.GetDriveFromPath(drive.DrivePath)?.DriveId != drive.DriveId)
            throw new InvalidOperationException("A DriveId cannot be changed.");

        _driveIdentifier.UpdateDrive(drive);
    }
}
