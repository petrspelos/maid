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

namespace Maid.Core;

public static class CommonFilePatterns
{
    public const string ArchiveFiles = @".*?\.(a|ar|cpio|shar|lbr|iso|lbr|mar|sbx|tar|bz2|gz|lz|lz4|lzma|lzo|rz|sfark|sz|xz|zst|7z|s7z|ace|afa|alz|cab|car|cpt|dar|dmg|ear|rar|war|xar|zz|zpaq|zip|zipx)";
    public const string PresentationFiles = @".*?\.(odp|pptx|pptm|ppt|potx|potm|pot|ppsx|ppsm|pps)";
    public const string GimpFiles = @".*?\.xcf";
    public const string SoundFiles = @".*?\.(3gp|aa|aac|aax|act|aiff|alac|amr|ape|au|awb|dss|dvf|flac|gsm|iklax|ivs|m4a|m4b|m4p|mmf|mp3|mpc|msv|nmf|ogg|oga|mogg|opus|ra|rm|rf64|tta|voc|vox|wav|wma|wv|8svx|cda)";
    public const string ImageFiles = @".*?\.(gif|jpe?g|tiff?|png|webp|bmp)";
    public const string WindowsShortcutFiles = @".*?\.lnk";
}
