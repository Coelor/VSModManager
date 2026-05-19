using System;
using System.Collections.Generic;
using System.Text;
using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    internal interface IModInfoReader
    {
        Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct = default);
    }
}
