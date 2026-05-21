using VSModManager.Core.Models;
using System.Text.Json;
using System.IO.Compression;

namespace VSModManager.Core.Services
{
    public class ModInfoReader : IModInfoReader
    {
        private static readonly JsonSerializerOptions jso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true };

        public async Task<ModInfo?> ReadFromFolderAsync(string folderPath, CancellationToken ct)
        {
            ModInfo? modInfo;

            var jsonPath = Path.Combine(folderPath, "modinfo.json");
            await using (var json = File.OpenRead(jsonPath))
            {
                modInfo = await JsonSerializer.DeserializeAsync<ModInfo>(json, jso, ct);
            }

            return modInfo;
        }

        public async Task<ModInfo?> ReadFromZipAsync(string zipPath, CancellationToken ct)
        {
            ModInfo? modInfo;

            await using (ZipArchive? archive = ZipFile.OpenRead(zipPath))
            {
                ZipArchiveEntry? zipEntry = archive?.GetEntry("modinfo.json");

                await using (Stream? stream = zipEntry?.Open())
                {
                    modInfo = await JsonSerializer.DeserializeAsync<ModInfo?>(stream, jso, ct);
                }                
            }

            return modInfo;
        }

        public async Task<ModInfo?> ReadAsync(string path, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
