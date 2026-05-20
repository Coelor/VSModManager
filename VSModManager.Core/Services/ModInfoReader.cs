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
        public async Task<List<ModInfo?>?> ReadFromFolderAsync(string folderPath, CancellationToken ct)
        {
            return await Task.Run(() =>
            {
                List<ModInfo?> modInfos = new List<ModInfo?>();

                foreach (var zipPath in Directory.EnumerateFiles(folderPath, "*.zip", SearchOption.TopDirectoryOnly))
                {
                    using var archive = ZipFile.OpenRead(zipPath);
                    var json = archive.GetEntry("modinfo.json");
                    if (json == null) continue;

                    using var stream = json.Open();
                    var modInfo = JsonSerializer.Deserialize<ModInfo>(stream);

                    if (modInfo != null)
                        modInfos.Add(modInfo);
                }

                return modInfos;
            });
        }
    }
}
