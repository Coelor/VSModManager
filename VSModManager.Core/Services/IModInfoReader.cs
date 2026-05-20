using System;
using System.Collections.Generic;
using System.Text;
using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    interface IModInfoReader
    {
        Task<List<ModInfo?>?> ReadFromFolderAsync(string folderPath, CancellationToken ct = default);
    }
}
