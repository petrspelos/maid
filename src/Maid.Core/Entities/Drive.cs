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

namespace Maid.Core.Entities;

public record class Drive
{
    public Drive(Guid driveId, string driveLabel, string drivePath)
    {
        DriveId = driveId;
        DriveLabel = driveLabel;
        DrivePath = drivePath;
    }

    public Guid DriveId { get; init; }

    public string DriveLabel { get; init; } = string.Empty;

    public string DrivePath { get; init; } = string.Empty;

    public ICollection<Gallery> Galleries { get; init; } = new List<Gallery>();
}
