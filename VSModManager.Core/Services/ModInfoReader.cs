using VSModManager.Core.Models;
using System.Text.Json;
using System.IO.Compression;

namespace VSModManager.Core.Services
{
    public class ModInfoReader : IModInfoReader
    {
        private static readonly JsonSerializerOptions jso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public async Task<ModInfo?> ReadFromZipAsync(string zipPath, CancellationToken ct)
        {
            ModInfo? modInfo;

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                ZipArchiveEntry? zipEntry = archive.GetEntry("modinfo.json");
                if (zipEntry != null)
                    using (Stream stream = zipEntry.Open())
                        modInfo = await JsonSerializer.DeserializeAsync<ModInfo?>(utf8Json: stream, jso, ct);
                
                else
                    modInfo = null;
            }

            return modInfo;
        }
    }
}
