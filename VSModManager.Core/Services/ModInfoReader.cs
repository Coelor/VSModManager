using System;
using System.Collections.Generic;
using System.Text;
using VSModManager.Core.Models;

namespace VSModManager.Core.Services
{
    internal class ModInfoReader : IModInfoReader
    {
        Task<ModInfo?> IModInfoReader.ReadFromFolderAsync(string folderPath, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
