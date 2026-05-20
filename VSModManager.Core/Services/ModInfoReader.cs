using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VSModManager.Core.Models;
using System.IO.Compression;
using System.Text.Json;

namespace VSModManager.Core.Services
{
    public class ModInfoReader : IModInfoReader
    {
        public async Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct)
        {

            ModInfo? modInfo;

            var jsonPath = Path.Combine(folderPath, "modinfo.json");
            var json = File.ReadAllText(jsonPath);
            modInfo = JsonSerializer.Deserialize<ModInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            

            return modInfo;
        }
    }
}
